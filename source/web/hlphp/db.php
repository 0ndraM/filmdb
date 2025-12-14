<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "filmydb";

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Chyba připojení: " . $conn->connect_error);
}

$conn->set_charset("utf8mb4");

function blokuj_ip() {
    $file = __DIR__ . '/../blocked_ips.txt';
    $zakazane_ip = file_exists($file) ? file($file, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES) : [];

    $ip = $_SERVER['REMOTE_ADDR'];
    if (in_array($ip, $zakazane_ip)) {
        header('HTTP/1.1 403 Forbidden');
        echo "403 Zakázán přístup z této IP adresy.";
        exit();
    }
}

blokuj_ip();
?>
