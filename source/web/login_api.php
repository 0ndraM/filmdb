<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); // Může být omezena na konkrétní domény pro vyšší bezpečnost
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
session_start(); // Session můžeme nechat pro zpětnou kompatibilitu, ale API by ji nemělo používat pro autorizaci
require 'hlphp/db.php';

// --- Konfigurace JWT ---
// DŮLEŽITÉ: Uložte tento klíč BEZPEČNĚ a mimo veřejný kód!
$secret_key = "TVUJ_VELMI_TAJNY_APLIKACNI_KLIC_123456_NAHRAZ_ME_OPRAVDOVYM_KLICEM";
$issuer = "http://vasa-domena.cz"; // Vydavatel
$audience = "http://vasa-domena.cz"; // Publikum
$expiration_time = time() + (3600 * 24); // Platnost 1 den (3600 sekund * 24 hodin)
// -----------------------

// Pomocná funkce pro logování pokusů (ponecháno z tvého původního kódu)
function logLoginAttempt($username, $status) {
    global $conn;
    $ip = $_SERVER['REMOTE_ADDR'];
    $stmt = $conn->prepare("INSERT INTO acces_logy (autor, akce) VALUES (?, ?)");
    $akce = "Přihlášení uživatele '$username' - $status (IP: $ip)";
    $stmt->bind_param("ss", $username, $akce);
    // Potlačení chyby, aby nebránila odpovědi API
    $stmt->execute();
}

// Odpověď pro úspěšný login
function successResponse($username, $token) {
    echo json_encode([
        "success" => true,
        "username" => $username,
        "token" => $token // Vracíme token klientovi
    ]);
    exit();
}

// Odpověď pro neúspěšný login
function errorResponse($message, $username = "Neznámý") {
    // Logování chyby před odesláním odpovědi
    logLoginAttempt($username, 'neúspěšné - ' . $message);
    
    http_response_code(401); // 401 Unauthorized
    echo json_encode([
        "success" => false,
        "message" => $message
    ]);
    exit();
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Pro API je často lepší číst JSON body, ale zůstaneme u $_POST, 
    // protože tvůj klient ho může používat přes Content-Type: application/x-www-form-urlencoded, 
    // nebo přes multipart/form-data.
    
    $username = $_POST['username'] ?? '';
    $password = $_POST['password'] ?? '';

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

        // Ověření hesla pomocí password_verify
        if (password_verify($password, $uzivatel['password'])) {
            
            // --- GENERUJEME JWT TOKEN ---
            $payload = [
                'iss' => $issuer,
                'aud' => $audience,
                'iat' => time(),
                'exp' => $expiration_time,
                'data' => [
                    'username' => $uzivatel['username'],
                    'role' => $uzivatel['role'] // Důležité pro budoucí autorizační kontroly
                ]
            ];
            
            // Vytvoření tokenu (HS256 je standardní algoritmus)
            $jwt = JWT::encode($payload, $secret_key, 'HS256');
            
            // Logování úspěchu
            logLoginAttempt($uzivatel['username'], 'úspěšné');

            // ÚSPĚŠNÁ ODPOVĚĎ s tokenem
            successResponse($uzivatel['username'], $jwt);

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
    http_response_code(405); // Metoda není povolena
    echo json_encode([
        "success" => false, 
        "message" => "Metoda není povolena. Očekává se POST."
    ]);
}
?>