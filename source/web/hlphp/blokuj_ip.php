<?php
session_start();
require_once 'db.php';

if ($_SESSION['role'] !== 'owner') {
    http_response_code(403);
    exit("Přístup zamítnut.");
}

if (!isset($_GET['ip'])) {
    exit("IP adresa nebyla zadána.");
}

$ip = $_GET['ip'];
$ip = filter_var($ip, FILTER_VALIDATE_IP);

if (!$ip) {
    exit("Neplatná IP adresa.");
}

$file = __DIR__ . '/blocked_ips.txt';
file_put_contents($file, $ip . PHP_EOL, FILE_APPEND | LOCK_EX);

header('Location: logs.php');
exit();