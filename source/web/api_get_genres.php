<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); 

require 'hlphp/db.php'; // Předpokládáme stejné připojení k DB

$zanry = [];

// Načtení dostupných žánrů z databáze
$result = $conn->query("SELECT DISTINCT zanr FROM filmy ORDER BY zanr ASC");

if ($result) {
    while ($row = $result->fetch_assoc()) {
        if (!empty($row['zanr'])) {
            $zanry[] = htmlspecialchars($row['zanr']);
        }
    }
} else {
    // Chyba při dotazu do DB
    http_response_code(500);
    echo json_encode(["success" => false, "message" => "Chyba při čtení databáze."]);
    exit();
}

// Vrácení seznamu žánrů
echo json_encode([
    "success" => true,
    "genres" => $zanry // Pole řetězců (např. ["Akční", "Komedie", "Sci-Fi"])
]);
?>