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

$file = __DIR__ . '/../blocked_ips.txt';

if (file_exists($file)) {
    $ips = file($file, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
    $ips = array_map('trim', $ips);
    $filtered = array_filter($ips, function($i) use ($ip) {
        return $i !== $ip;
    });
    file_put_contents($file, implode(PHP_EOL, $filtered) . PHP_EOL);
}

header('Location: ../logs.php');
exit();