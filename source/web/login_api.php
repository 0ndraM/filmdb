<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); // Může být omezena na konkrétní domény
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization');

// Odpověď pro úspěšný login
function successResponse($username) {
    echo json_encode([
        "success" => true,
        "username" => $username
    ]);
    exit();
}

// Odpověď pro neúspěšný login
function errorResponse($message) {
    // Nastavení stavového kódu HTTP na 401 Unauthorized
    http_response_code(401);
    echo json_encode([
        "success" => false,
        "message" => $message
    ]);
    exit();
}

// Zbytek tvého původního kódu
session_start();
require 'hlphp/db.php';

function logLoginAttempt($username, $status) {
    global $conn;
    $ip = $_SERVER['REMOTE_ADDR'];
    // Použij 'Neznámý' nebo podobně, pokud username selže před načtením z DB
    $log_username = $username ? $username : 'Neznámý'; 
    $stmt = $conn->prepare("INSERT INTO acces_logy (autor, akce) VALUES (?, ?)");
    $akce = "Přihlášení uživatele '$log_username' - $status (IP: $ip)";
    $stmt->bind_param("ss", $log_username, $akce);
    // Potlačení chyby v případě, že logování selže, aby nebránilo odpovědi API
    $stmt->execute(); 
}

// Zkontrolujeme, zda je metoda POST a zda byla data odeslána
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Pro API je lepší očekávat JSON, ale protože tvůj původní kód používal $_POST, necháme pro kompatibilitu.
    // Pro čisté API by se mělo používat: $data = json_decode(file_get_contents("php://input"), true);
    
    // Použijeme tvůj původní přístup přes $_POST
    $username = $_POST['username'] ?? '';
    $password = $_POST['password'] ?? '';
    
    // Rychlá kontrola, zda data dorazila
    if (empty($username) || empty($password)) {
        logLoginAttempt($username, 'neúspěšné - chybí data');
        errorResponse("Chybí uživatelské jméno nebo heslo.");
    }

    // Bezpečné vyhledání uživatele
    $stmt = $conn->prepare("SELECT * FROM uzivatele WHERE username = ?");
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $vysledek = $stmt->get_result();

    if ($vysledek->num_rows === 1) {
        $uzivatel = $vysledek->fetch_assoc();

        // Ověření hesla pomocí password_verify
        if (password_verify($password, $uzivatel['password'])) {
            // Při API loginu nemusíš nutně nastavovat SESSION, pokud používáš tokeny (např. JWT).
            // Nicméně, pokud je API voláno ze stejné domény pro udržení PHP session, můžeš to nechat.
            // Prozatím to ponecháme pro konzistenci s tvým kódem:
            $_SESSION['username'] = $uzivatel['username'];
            $_SESSION['role'] = $uzivatel['role'];
            $_SESSION['ip'] = $_SERVER['REMOTE_ADDR'];
            
            logLoginAttempt($uzivatel['username'], 'úspěšné');
            
            // ÚSPĚŠNÁ ODPOVĚĎ
            successResponse($uzivatel['username']);

        } else {
            logLoginAttempt($username, 'neúspěšné - špatné heslo');
            // NEÚSPĚŠNÁ ODPOVĚĎ
            errorResponse("Špatné heslo.");
        }
    } else {
        logLoginAttempt($username, 'neúspěšné - uživatel nenalezen');
        // NEÚSPĚŠNÁ ODPOVĚĎ
        errorResponse("Uživatel nenalezen.");
    }
} else {
    // Pokud metoda není POST
    http_response_code(405); // Metoda není povolena
    echo json_encode([
        "success" => false, 
        "message" => "Metoda není povolena. Očekává se POST."
    ]);
}
?>