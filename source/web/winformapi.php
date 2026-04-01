<?php
// Nastavíme hlavičku, aby klient věděl, že dostává JSON
header('Content-Type: application/json; charset=utf-8');

// Zahrneme soubor s připojením k databázi
include 'hlphp/db.php';

// Získání a ověření parametru pro řazení
$order_by = $_GET['order_by'] ?? 'nazev';
$search = $_GET['search'] ?? '';

$allowed_columns = ['nazev', 'rok', 'zanr', 'reziser', 'hodnoceni'];
// Zabráníme SQL injection v ORDER BY klauzuli
if (!in_array($order_by, $allowed_columns)) {
    $order_by = 'nazev';
}

// Ošetření vstupního řetězce pro vyhledávání
$search_safe = $conn->real_escape_string($search);

// Sestavení SQL dotazu
// Vybereme všechny sloupce, které potřebujeme pro JSON výstup
$sql = "SELECT id, nazev, rok, zanr, reziser, hodnoceni, popis, schvaleno, autor 
        FROM filmy 
        WHERE nazev LIKE '%$search_safe%' AND schvaleno = 1 
        ORDER BY $order_by";

$result = $conn->query($sql);

$filmy = []; // Inicializace pole pro uložení výsledků

// Zpracování výsledků
if ($result) {
    while ($row = $result->fetch_assoc()) {
        // Konverze logické hodnoty schvaleno (předpokládáme 0/1 v DB)
        $schvaleno = (bool)$row['schvaleno'];

        // Generování hodnoty pro "poster"
        // Používáme id, jak to bylo v původním kódu
        $poster_path = "plakaty/" . htmlspecialchars($row['id']) . ".jpg";
        
        // Vytvoření objektu filmu pro JSON
        $film_data = [
            "id" => (int)$row['id'],
            "nazev" => htmlspecialchars($row['nazev']),
            "rok" => (int)$row['rok'],
            "zanr" => htmlspecialchars($row['zanr']),
            "reziser" => htmlspecialchars($row['reziser']),
            "hodnoceni" => (float)$row['hodnoceni'],
            "popis" => htmlspecialchars($row['popis']),
            "schvaleno" => $schvaleno,
            "autor" => htmlspecialchars($row['autor']),
            "poster" => $poster_path
        ];
        
        $filmy[] = $film_data;
    }
    
    // Uvolnění výsledků
    $result->free();
} else {
    // V případě chyby dotazu, můžeme poslat prázdné pole nebo chybovou zprávu
    // Zde posíláme prázdné pole, ale můžete přidat i logování/error zprávu
    // http_response_code(500); // Nastavení HTTP kódu chyby
    // echo json_encode(["error" => $conn->error]);
}

// Uzavření připojení k databázi
$conn->close();

// Vypsání výsledného pole jako JSON
echo json_encode($filmy, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);

?>