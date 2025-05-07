<?php
session_start();
require 'db.php';

if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: login.php');
    exit();
}

// Získání dat z tabulky logů filmů
$logy_filmy = $conn->query("SELECT * FROM filmy_log ORDER BY zmeneno DESC");

if (!$logy_filmy) {
    die("Chyba při načítání logů filmů: " . $conn->error);
}

// Nastavení hlaviček pro stažení CSV souboru
header('Content-Type: text/csv');
header('Content-Disposition: attachment; filename="filmy_log.csv"');

// Otevření souboru pro zápis
$output = fopen('php://output', 'w');

// Nastavení názvů sloupců
fputcsv($output, ['Film ID', 'Název', 'Rok', 'Žánr', 'Režisér', 'Hodnocení', 'Popis', 'Autor změny', 'Čas změny']);

// Zapsání dat do CSV souboru
while ($row = $logy_filmy->fetch_assoc()) {
    fputcsv($output, [
        $row['film_id'],
        $row['nazev'],
        $row['rok'],
        $row['zanr'],
        $row['reziser'],
        $row['hodnoceni'],
        $row['popis'],
        $row['autor'],
        $row['zmeneno']
    ]);
}

// Zavření souboru
fclose($output);
exit();
?>
