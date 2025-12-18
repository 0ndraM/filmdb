<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); 
header('Access-Control-Allow-Methods: GET, POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization');

// Při požadavku OPTIONS (preflight) ihned ukončíme
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit();
}

// Vyžadujeme autoload pro JWT a připojení k DB
require 'vendor/autoload.php';
use Firebase\JWT\JWT;
use Firebase\JWT\Key;

require 'hlphp/db.php';

// --- Konfigurace ---
$secret_key = "TVUJ_VELMI_TAJNY_APLIKACNI_KLIC_123456_NAHRAZ_ME_OPRAVDOVYM_KLICEM";

// --- 1. AUTORIZACE A KONTROLA ROLE ---
$token = null;
$headers = getallheaders();
if (isset($headers['Authorization']) && preg_match('/Bearer\s(\S+)/', $headers['Authorization'], $matches)) {
    $token = $matches[1];
}

if (!$token) {
    http_response_code(401);
    die(json_encode(["success" => false, "message" => "Neautorizovaný přístup. Chybí token."]));
}

try {
    // Dekódování tokenu
    $decoded = JWT::decode($token, new Key($secret_key, 'HS256'));
    $userRole = $decoded->data->role ?? 'user';
    $loggedInUsername = $decoded->data->username ?? 'unknown';

    // Striktní kontrola role: Pouze admin nebo owner
    if ($userRole !== 'admin' && $userRole !== 'owner') {
        http_response_code(403);
        die(json_encode(["success" => false, "message" => "Nedostatečná oprávnění."]));
    }
} catch (Exception $e) {
    http_response_code(401);
    die(json_encode(["success" => false, "message" => "Neplatný nebo vypršelý token."]));
}

// --- 2. ZPRACOVÁNÍ POŽADAVKŮ ---

// A) GET: Načtení seznamu filmů
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $type = $_GET['type'] ?? 'unapproved';

    if ($type === 'all') {
        // Seřazení všech filmů podle ID sestupně
        $query = "SELECT id, nazev, rok, autor, schvaleno, vytvoreno FROM filmy ORDER BY id DESC";
    } else {
        // Seřazení neschválených filmů podle ID vzestupně (nejstarší kousky k vyřízení první)
        $query = "SELECT id, nazev, rok, autor, vytvoreno FROM filmy WHERE schvaleno = 0 ORDER BY id ASC";
    }

    $result = $conn->query($query);

    $result = $conn->query($query);
    $films = [];
    
    while ($row = $result->fetch_assoc()) {
        $films[] = $row;
    }

    echo json_encode([
        "success" => true,
        "films" => $films
    ]);
} 

// B) POST: Změna stavu (Schválení / Odschválení)
else if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Načtení JSON těla požadavku
    $input = json_decode(file_get_contents("php://input"), true);
    
    $id = $input['id'] ?? null;
    $action = $input['action'] ?? 'approve'; // 'approve' nebo 'reject'

    if (!$id) {
        http_response_code(400);
        die(json_encode(["success" => false, "message" => "Chybí ID filmu."]));
    }

    // Určení nového stavu: approve = 1, reject = 0
    $newStatus = ($action === 'approve') ? 1 : 0;

    $stmt = $conn->prepare("UPDATE filmy SET schvaleno = ? WHERE id = ?");
    $stmt->bind_param("ii", $newStatus, $id);

    if ($stmt->execute()) {
        $message = ($newStatus === 1) ? "Film byl úspěšně schválen." : "Schválení filmu bylo zrušeno.";
        echo json_encode([
            "success" => true, 
            "message" => $message,
            "newStatus" => $newStatus
        ]);
    } else {
        http_response_code(500);
        echo json_encode(["success" => false, "message" => "Chyba při aktualizaci databáze."]);
    }
} 

// Ostatní metody nepovolujeme
else {
    http_response_code(405);
    echo json_encode(["success" => false, "message" => "Metoda není povolena."]);
}
?>