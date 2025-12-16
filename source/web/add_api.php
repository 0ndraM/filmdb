<?php
// Nastavení hlaviček pro API odpověď
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); // Ponecháno pro kompatibilitu, ale zvažte omezení
header('Access-Control-Allow-Methods: POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization, X-Requested-With');

// Přeskočíme session_start(), jelikož autorizace probíhá přes POST data.
require 'hlphp/db.php';

// Pomocná funkce pro úspěšnou JSON odpověď
function successResponse() {
    echo json_encode(["success" => true]);
    exit();
}

// Pomocná funkce pro chybovou JSON odpověď
function errorResponse($message, $http_code = 400) {
    http_response_code($http_code);
    echo json_encode(["success" => false, "message" => $message]);
    exit();
}

// Zkontrolujeme, zda je metoda POST
if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    errorResponse("Metoda není povolena. Očekává se POST.", 405);
}

// 1. Extrahování dat
$autor = $_POST['autor'] ?? null; // TOTO JE KLÍČOVÁ ZMĚNA: Bereme autora z POST dat
$nazev = $_POST['nazev'] ?? null;
$rok = $_POST['rok'] ?? null;
$zanr = $_POST['zanr'] ?? null;
$reziser = $_POST['reziser'] ?? null;
$hodnoceni = $_POST['hodnoceni'] ?? null;
$popis = $_POST['popis'] ?? null;

// 2. Základní validace dat (autor je nyní povinný)
if (empty($autor) || empty($nazev) || empty($rok) || empty($zanr) || empty($popis)) {
    errorResponse("Chybí povinná pole (autor, název, rok, žánr, popis).");
}
// Kontrola typu a rozmezí pro rok
if (!is_numeric($rok) || $rok < 1888 || $rok > (date("Y") + 1)) {
     errorResponse("Neplatná hodnota roku.");
}

// --- ZDE BY MĚLA BÝT BEZPEČNOSTNÍ KONTROLA AUTORA, např. Token ---
// Bezpečnostní upozornění: Váš C# kód neověřuje uživatele! Kdokoli může zaslat POST požadavek 
// s jakýmkoliv jménem autora. Doporučuji používat JWT token pro API autentizaci.
// ---

// 3. Vložení filmu do DB (bez plakátu)
try {
    // Převedení hodnocení na float (pro 'd' v bind_param), pokud existuje, jinak null
    // Používáme ternary operátor pro bezpečné zpracování, i když C# posílá string
    $hodnoceni_float = is_numeric($hodnoceni) ? floatval($hodnoceni) : null;
    
    // Používáme připravený dotaz s určenými datovými typy: sissdss
    $stmt = $conn->prepare("INSERT INTO filmy (nazev, rok, zanr, reziser, hodnoceni, popis, schvaleno, autor) VALUES (?, ?, ?, ?, ?, ?, 0, ?)");
    
    // Vázání parametrů
    if (!$stmt->bind_param("sissdss", $nazev, $rok, $zanr, $reziser, $hodnoceni_float, $popis, $autor)) {
        errorResponse("Chyba při vazbě parametrů.", 500);
    }
    
    if (!$stmt->execute()) {
        errorResponse("Chyba při vkládání do databáze: " . $conn->error, 500);
    }
    $stmt->close();

    // Získání ID právě vloženého filmu
    $id = $conn->insert_id;
    
} catch (Exception $e) {
    errorResponse("Interní chyba databáze.", 500);
}


// 4. Zpracování plakátu
if (isset($_FILES['plakat']) && $_FILES['plakat']['error'] === UPLOAD_ERR_OK) {
    $plakatTmp = $_FILES['plakat']['tmp_name'];
    $plakatName = $_FILES['plakat']['name'];
    $extension = strtolower(pathinfo($plakatName, PATHINFO_EXTENSION));

    if (in_array($extension, ['jpg', 'jpeg'])) {
        $target_dir = "plakaty/";
        $target_file = $target_dir . $id . ".jpg";

        // Kontrola a vytvoření adresáře
        if (!is_dir($target_dir)) {
            if (!mkdir($target_dir, 0755, true)) {
                errorResponse("Chyba serveru: Nelze vytvořit adresář pro plakáty.", 500);
            }
        }

        // Pokus o přesun
        if (!move_uploaded_file($plakatTmp, $target_file)) {
            errorResponse("Nepodařilo se uložit plakát (chyba přesunu souboru).", 500);
        }
    } else {
        errorResponse("Nepodporovaný formát obrázku. Použijte .jpg nebo .jpeg.");
    }
}

// 5. ÚSPĚŠNÁ ODPOVĚĎ
successResponse();

?>