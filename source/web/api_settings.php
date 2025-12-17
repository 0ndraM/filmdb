<?php
// Nastavení hlaviček pro API
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); 
header('Access-Control-Allow-Methods: GET, POST, PUT, OPTIONS'); // Povolujeme metody
header('Access-Control-Allow-Headers: Content-Type, Authorization');

// Zahrnutí Composer a DB
require 'vendor/autoload.php';
use Firebase\JWT\JWT;
use Firebase\JWT\Key;
use Firebase\JWT\ExpiredException;

require 'hlphp/db.php'; 

// --- KONFIGURACE ---
$secret_key = "TVUJ_VELMI_TAJNY_APLIKACNI_KLIC_123456_NAHRAZ_ME_OPRAVDOVYM_KLICEM";
// --- FUNKCE PRO ODPOVĚDI ---
function successResponse($data = []) {
    echo json_encode(array_merge(["success" => true], $data));
    exit();
}
function errorResponse($message, $http_code = 400) {
    http_response_code($http_code);
    echo json_encode(["success" => false, "message" => $message]);
    exit();
}

// --- AUTORIZACE POMOCÍ JWT ---
$token = null;
$headers = getallheaders();
if (isset($headers['Authorization']) && preg_match('/Bearer\s(\S+)/', $headers['Authorization'], $matches)) {
    $token = $matches[1];
}

if (!$token) {
    errorResponse("Neautorizovaný přístup. Chybí autorizační token.", 401);
}

try {
    $decoded = JWT::decode($token, new Key($secret_key, 'HS256'));
    $loggedInUsername = $decoded->data->username ?? null;
    $userRole = $decoded->data->role ?? null;
    
    if (empty($loggedInUsername)) {
         errorResponse("Token neobsahuje platné uživatelské jméno.", 401);
    }
    
} catch (Exception $e) {
    errorResponse("Neplatný nebo vypršelý autorizační token.", 401);
}

// Data pro PUT/POST by měla přijít jako JSON, načteme je univerzálně
$input = json_decode(file_get_contents("php://input"), true) ?? [];

// -----------------------------------------------------
// --- ZPRACOVÁNÍ RŮZNÝCH AKCÍ ---
// -----------------------------------------------------

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    // AKCE: Získání filmů přidaných uživatelem (pro zobrazení v sekci "Moje filmy")
    
    $films_result = $conn->prepare("SELECT id, nazev, rok, schvaleno FROM filmy WHERE autor = ? ORDER BY vytvoreno DESC");
    $films_result->bind_param("s", $loggedInUsername);
    $films_result->execute();
    $result = $films_result->get_result();

    $films = [];
    while ($film = $result->fetch_assoc()) {
        $films[] = $film;
    }
    
    successResponse(["films" => $films, "username" => $loggedInUsername]);


} elseif ($_SERVER['REQUEST_METHOD'] == 'POST' || $_SERVER['REQUEST_METHOD'] == 'PUT') {
    
    // AKCE: Změna hesla
    if (isset($input['action']) && $input['action'] == 'change_password') {
        $new_password = $input['new_password'] ?? null;
        $confirm_password = $input['confirm_password'] ?? null;
        
        if (empty($new_password) || empty($confirm_password)) {
            errorResponse("Nové heslo a potvrzení jsou povinné.");
        }
        if ($new_password !== $confirm_password) {
            errorResponse("Nové heslo se neshoduje s potvrzením.");
        }
        
        $hashed_password = password_hash($new_password, PASSWORD_DEFAULT);
        
        $stmt = $conn->prepare("UPDATE uzivatele SET password = ? WHERE username = ?");
        $stmt->bind_param("ss", $hashed_password, $loggedInUsername);
        
        if ($stmt->execute()) {
            successResponse(["message" => "Heslo bylo úspěšně změněno."]);
        } else {
            errorResponse("Chyba při aktualizaci hesla.", 500);
        }
    }
   // AKCE: Změna jména
    elseif (isset($input['action']) && $input['action'] == 'change_username') {
        $new_username = $input['new_username'] ?? null;
        
        if (empty($new_username)) {
            errorResponse("Nové uživatelské jméno je povinné.");
        }
        if ($new_username == $loggedInUsername) {
            errorResponse("Nové jméno je stejné jako stávající.");
        }
        
        // 1. Kontrola, zda nové jméno již neexistuje (Používáme COUNT pro spolehlivost)
        $check_stmt = $conn->prepare("SELECT COUNT(id) FROM uzivatele WHERE username = ?");
        $check_stmt->bind_param("s", $new_username);
        $check_stmt->execute();
        $check_stmt->bind_result($count); // Musíme získat výsledek
        $check_stmt->fetch();
        $check_stmt->close();
        
        if ($count > 0) {
            errorResponse("Uživatelské jméno '$new_username' je již obsazeno.");
        }
        
        // --- TRANSAKCE: ZAJISTÍ, ŽE SE NIKDY NEPROVEDE POUZE ČÁST ZMĚNY ---
        $conn->begin_transaction();
        
        try {
            // 2. Aktualizace Jména v tabulce 'uzivatele'
            $stmt_user = $conn->prepare("UPDATE uzivatele SET username = ? WHERE username = ?");
            if (!$stmt_user->bind_param("ss", $new_username, $loggedInUsername) || !$stmt_user->execute()) {
                throw new Exception("Chyba při aktualizaci uživatele.");
            }
            
            // 3. Aktualizace pole 'autor' v tabulce 'filmy'
            $stmt_films = $conn->prepare("UPDATE filmy SET autor = ? WHERE autor = ?");
            if (!$stmt_films->bind_param("ss", $new_username, $loggedInUsername) || !$stmt_films->execute()) {
                throw new Exception("Chyba při aktualizaci filmů.");
            }
            
            // Dokončení transakce
            $conn->commit();

            // UPOZORNĚNÍ: Klient se musí znovu přihlásit pro získání nového JWT tokenu s novým jménem!
            successResponse(["message" => "Uživatelské jméno a filmy byly úspěšně aktualizovány.", "new_username" => $new_username]);
            
        } catch (Exception $e) {
            // Při chybě vrátíme transakci zpět
            $conn->rollback(); 
            // Vracíme obecnou chybu, abychom neprozradili detaily DB
            errorResponse("Chyba při aktualizaci jména: " . $e->getMessage(), 500);
        }
    }
    
    else {
        // Pokud metoda není ani GET, ani POST/PUT
        http_response_code(405);
        echo json_encode(["success" => false, "message" => "Metoda není povolena."]);
    }
}
?>