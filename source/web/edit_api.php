<?php
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization');

require 'vendor/autoload.php';
use Firebase\JWT\JWT;
use Firebase\JWT\Key;
require 'hlphp/db.php';

$secret_key = $_ENV['JWT_SECRET'] ?? 'fallback_pro_jistotu';
// 1. Ověření tokenu
$headers = getallheaders();
$token = (isset($headers['Authorization']) && preg_match('/Bearer\s(\S+)/', $headers['Authorization'], $matches)) ? $matches[1] : null;

if (!$token) { die(json_encode(["success" => false, "message" => "Chybí token"])); }

try {
    $decoded = JWT::decode($token, new Key($secret_key, 'HS256'));
    $loggedInUser = $decoded->data->username;
} catch (Exception $e) {
    die(json_encode(["success" => false, "message" => "Neplatný token"]));
}

// Získání aktuálního autora filmu z DB před úpravou
$check_stmt = $conn->prepare("SELECT autor FROM filmy WHERE id = ?");
$check_stmt->bind_param("i", $id);
$check_stmt->execute();
$res = $check_stmt->get_result();
$film_data = $res->fetch_assoc();

$is_admin = ($userRole === 'admin' || $userRole === 'owner');
$is_author = ($film_data['autor'] === $loggedInUsername);

if (!$is_admin && !$is_author) {
    die(json_encode(["success" => false, "message" => "Nedostatečná oprávnění pro úpravu tohoto filmu."]));
}

// 2. Příjem dat
$id = $_POST['id'] ?? null;
$nazev = $_POST['nazev'] ?? null;
$rok = $_POST['rok'] ?? null;
$zanr = $_POST['zanr'] ?? null;
$reziser = $_POST['reziser'] ?? null;
$hodnoceni = $_POST['hodnoceni'] ?? null;
$popis = $_POST['popis'] ?? null;

if (!$id || !$nazev) { die(json_encode(["success" => false, "message" => "Chybí ID nebo název"])); }

// 3. Aktualizace (kontrola, zda je uživatel autor nebo admin/owner může být přidána)
$stmt = $conn->prepare("UPDATE filmy SET nazev=?, rok=?, zanr=?, reziser=?, hodnoceni=?, popis=? WHERE id=?");
$stmt->bind_param("sissdsi", $nazev, $rok, $zanr, $reziser, $hodnoceni, $popis, $id);

if ($stmt->execute()) {
    echo json_encode(["success" => true, "message" => "Film upraven"]);
  // Uložení do logu (zachování aktuálního autora z tabulky, ale uložení "kdo to změnil")
    $upravujici = $_SESSION['username'];
    $stmt_log = $conn->prepare("INSERT INTO filmy_log (film_id, nazev, rok, zanr, reziser, hodnoceni, popis, autor) VALUES (?, ?, ?, ?, ?, ?, ?, ?)");
    $stmt_log->bind_param("isissdss", $id, $nazev, $rok, $zanr, $reziser, $hodnoceni, $popis, $username);
    $stmt_log->execute();
    $stmt_log->close();

} else {
    echo json_encode(["success" => false, "message" => "Chyba DB"]);
}
  
?>