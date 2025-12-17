<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); // Zvažte omezení na konkrétní doménu klienta
header('Access-Control-Allow-Methods: POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization, X-Requested-With');

// Přeskočení preflight požadavků
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit();
}

// Vyžadujeme JWT knihovnu
require 'vendor/autoload.php';
use Firebase\JWT\JWT;
use Firebase\JWT\Key;
use Firebase\JWT\ExpiredException;

// Zahrneme připojení k databázi
require 'hlphp/db.php';

// --- Konfigurace JWT (Musí odpovídat nastavení v login_api.php!) ---
// DŮLEŽITÉ: Uložte tento klíč BEZPEČNĚ!
$secret_key = "TVUJ_VELMI_TAJNY_APLIKACNI_KLIC_123456_NAHRAZ_ME_OPRAVDOVYM_KLICEM";
$secret_key = getenv('JWT_SECRET_KEY');
if ($secret_key === false) {
    // Log the error and stop execution if the key is not set.
    error_log("JWT_SECRET_KEY not set.");
    errorResponse("Chyba konfigurace serveru.", 500);
}
// -------------------------------------------------------------------

// --- POMOCNÉ FUNKCE ---

// Pomocná funkce pro úspěšnou JSON odpověď
function successResponse() {
    echo json_encode(["success" => true]);
    exit();
}

// Pomocná funkce pro chybovou JSON odpověď
function errorResponse($message, $http_code = 400) {
    http_response_code($http_code);
    echo json_encode(["success" => false, "message" => $message]);
    exit();
}

// 1. KONTROLA METODY
if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    errorResponse("Metoda není povolena. Očekává se POST.", 405);
}


// 2. AUTORIZACE: Získání a ověření JWT tokenu

$token = null;
$headers = getallheaders();

// Získání tokenu z hlavičky Authorization: Bearer [token]
if (isset($headers['Authorization'])) {
    $auth_header = $headers['Authorization'];
    if (preg_match('/Bearer\s(\S+)/', $auth_header, $matches)) {
        $token = $matches[1];
    }
}

if (!$token) {
    errorResponse("Neautorizovaný přístup. Chybí autorizační token.", 401);
}

try {
    // Dekódování a ověření tokenu (kontroluje platnost, podpis a čas vypršení)
    $decoded = JWT::decode($token, new Key($secret_key, 'HS256'));
    
    // Získání uživatelského jména z payloadu (payload->data->username)
    $autor = $decoded->data->username ?? null;
    
    if (empty($autor)) {
         errorResponse("Token neobsahuje platné uživatelské jméno.", 401);
    }
    
} catch (ExpiredException $e) {
    // Chyta vypršení platnosti tokenu
    errorResponse("Autorizační token vypršel.", 401);
} catch (Exception $e) {
    // Chyta ostatních chyb (špatný podpis, neplatná struktura atd.)
    errorResponse("Neplatný autorizační token.", 401);
}

// --- Autorizace úspěšná. Proměnná $autor je nastavena z tokenu. ---


// 3. Extrahování dat z POST (multipart/form-data)
$nazev = $_POST['nazev'] ?? null;
$rok = $_POST['rok'] ?? null;
$zanr = $_POST['zanr'] ?? null;
$reziser = $_POST['reziser'] ?? null;
$hodnoceni = $_POST['hodnoceni'] ?? null;
$popis = $_POST['popis'] ?? null;

// Důležité: $_POST['autor'] NEPOUŽÍVÁME, bereme ho z $autor získaného z tokenu.


// 4. Základní validace dat
if (empty($nazev) || empty($rok) || empty($zanr) || empty($popis)) {
    errorResponse("Chybí povinná pole (název, rok, žánr, popis).");
}
if (!is_numeric($rok) || $rok < 1888 || $rok > (date("Y") + 1)) {
     errorResponse("Neplatná hodnota roku.");
}

// 5. Vložení filmu do DB
try {
    $hodnoceni_float = is_numeric($hodnoceni) ? floatval($hodnoceni) : null;
    
    // Používáme připravený dotaz: sissdss (s=string, i=int, d=double/float)
    $stmt = $conn->prepare("INSERT INTO filmy (nazev, rok, zanr, reziser, hodnoceni, popis, schvaleno, autor) VALUES (?, ?, ?, ?, ?, ?, 0, ?)");
    
    // Vázání parametrů
    if (!$stmt->bind_param("sissdss", $nazev, $rok, $zanr, $reziser, $hodnoceni_float, $popis, $autor)) {
        errorResponse("Chyba při vazbě parametrů.", 500);
    }
    
    if (!$stmt->execute()) {
        errorResponse("Chyba při vkládání do databáze: " . $conn->error, 500);
    }
    $stmt->close();

    // Získání ID právě vloženého filmu
    $id = $conn->insert_id;
    
} catch (Exception $e) {
    errorResponse("Interní chyba databáze.", 500);
}


// 6. Zpracování plakátu
if (isset($_FILES['plakat']) && $_FILES['plakat']['error'] === UPLOAD_ERR_OK) {
    $plakatTmp = $_FILES['plakat']['tmp_name'];
    $plakatName = $_FILES['plakat']['name'];
    $extension = strtolower(pathinfo($plakatName, PATHINFO_EXTENSION));

    if (in_array($extension, ['jpg', 'jpeg'])) {
        $target_dir = "plakaty/";
        $target_file = $target_dir . $id . ".jpg";

        // Kontrola a vytvoření adresáře
        if (!is_dir($target_dir)) {
            if (!mkdir($target_dir, 0755, true)) {
                errorResponse("Chyba serveru: Nelze vytvořit adresář pro plakáty.", 500);
            }
        }

        // Pokus o přesun
        if (!move_uploaded_file($plakatTmp, $target_file)) {
            errorResponse("Nepodařilo se uložit plakát (chyba přesunu souboru).", 500);
        }
    } else {
        errorResponse("Nepodporovaný formát obrázku. Použijte .jpg nebo .jpeg.");
    }
}

// 7. ÚSPĚŠNÁ ODPOVĚĎ
successResponse();

?>