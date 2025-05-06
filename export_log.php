<?php
session_start();
require 'db.php';

if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: login.php');
    exit();
}

// Hlavičky pro CSV výstup
header('Content-Type: text/csv; charset=utf-8');
header('Content-Disposition: attachment; filename=logy.csv');

// Otevře výstupní tok
$output = fopen('php://output', 'w');

// Hlavička CSV
fputcsv($output, ['Autor', 'Akce', 'Čas']);

// Načti logy
$sql = "SELECT autor, akce, cas FROM logy ORDER BY cas DESC";
$result = $conn->query($sql);

// Záznamy
if ($result && $result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        fputcsv($output, $row);
    }
}

fclose($output);
exit();
