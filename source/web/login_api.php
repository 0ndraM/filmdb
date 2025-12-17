<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); 
header('Access-Control-Allow-Methods: POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization');

// Při požadavku OPTIONS (preflight) odejdeme
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit();
}

// Vyžadujeme autoload pro JWT knihovnu
require 'vendor/autoload.php';
use Firebase\JWT\JWT;

// Zahrneme připojení k databázi
session_start();
require 'hlphp/db.php';

// --- Konfigurace JWT ---
$secret_key = "TVUJ_VELMI_TAJNY_APLIKACNI_KLIC_123456_NAHRAZ_ME_OPRAVDOVYM_KLICEM";
$issuer = "http://vasa-domena.cz"; 
$audience = "http://vasa-domena.cz";
$expiration_time = time() + (3600 * 24); // Platnost 1 den 
// -----------------------

// Pomocná funkce pro logování pokusů
function logLoginAttempt($username, $status) {
    global $conn;
    $ip = $_SERVER['REMOTE_ADDR'];
    $stmt = $conn->prepare("INSERT INTO acces_logy (autor, akce) VALUES (?, ?)");
    $akce = "Přihlášení uživatele '$username' - $status (IP: $ip)";
    $stmt->bind_param("ss", $username, $akce);
    $stmt->execute();
}

// Odpověď pro úspěšný login
function successResponse($username, $role, $token) { // Přidáno $role
    echo json_encode([
        "success" => true,
        "username" => $username,
        "role" => $role, // NOVÉ: Vrácení role v JSON odpovědi
        "token" => $token
    ]);
    exit();
}

// Odpověď pro neúspěšný login
function errorResponse($message, $username = "Neznámý") {
    logLoginAttempt($username, 'neúspěšné - ' . $message);
    
    http_response_code(401); 
    echo json_encode([
        "success" => false,
        "message" => $message
    ]);
    exit();
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = '';
    $password = '';
    
    // Zkusíme data z $_POST (klasický formulář/FormUrlEncodedContent)
    if (isset($_POST['username']) && isset($_POST['password'])) {
        $username = $_POST['username'];
        $password = $_POST['password'];
    }
    
    // Pokud se v $_POST nic nenašlo, zkusíme JSON body
    if (empty($username) || empty($password)) {
        $json_data = file_get_contents("php://input");
        $data = json_decode($json_data, true);
        
        if (isset($data['username']) && isset($data['password'])) {
            $username = $data['username'];
            $password = $data['password'];
        }
    }

    if (empty($username) || empty($password)) {
        errorResponse("Chybí uživatelské jméno nebo heslo.");
    }

    // Bezpečné vyhledání uživatele
    $stmt = $conn->prepare("SELECT * FROM uzivatele WHERE username = ?");
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $vysledek = $stmt->get_result();

    if ($vysledek->num_rows === 1) {
        $uzivatel = $vysledek->fetch_assoc();

        // Ověření hesla
        if (password_verify($password, $uzivatel['password'])) {
            
            // --- GENERUJEME JWT TOKEN ---
            $payload = [
                'iss' => $issuer,
                'aud' => $audience,
                'iat' => time(),
                'exp' => $expiration_time,
                'data' => [
                    'username' => $uzivatel['username'],
                    'role' => $uzivatel['role'] // NOVÉ: Přidání role do payloadu
                ]
            ];
            
            $jwt = JWT::encode($payload, $secret_key, 'HS256');
            
            logLoginAttempt($uzivatel['username'], 'úspěšné');

            // ÚSPĚŠNÁ ODPOVĚĎ s tokenem a rolí
            successResponse($uzivatel['username'], $uzivatel['role'], $jwt); // Předáváme $uzivatel['role']

        } else {
            // NEÚSPĚCH: Špatné heslo
            errorResponse("Špatné uživatelské jméno nebo heslo.", $username);
        }
    } else {
        // NEÚSPĚCH: Uživatel nenalezen
        errorResponse("Špatné uživatelské jméno nebo heslo.", $username);
    }
} else {
    // Pokud metoda není POST
    http_response_code(405);
    echo json_encode([
        "success" => false, 
        "message" => "Metoda není povolena. Očekává se POST."
    ]);
}
?>