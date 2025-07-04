<?php
session_start();
require 'hlphp/db.php';

if (!isset($_SESSION['role'])) {
    header('Location: login.php');
    exit();
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $autor = $_SESSION['username'] ?? 'neznámý';

    $nazev = $_POST['nazev'];
    $rok = $_POST['rok'];
    $zanr = $_POST['zanr'];
    $reziser = $_POST['reziser'];
    $hodnoceni = $_POST['hodnoceni'];
    $popis = $_POST['popis'];

    // Nejprve vlož film do DB bez plakátu, aby získal ID
    $stmt = $conn->prepare("INSERT INTO filmy (nazev, rok, zanr, reziser, hodnoceni, popis, schvaleno, autor) VALUES (?, ?, ?, ?, ?, ?, 0, ?)");
    $stmt->bind_param("sissdss", $nazev, $rok, $zanr, $reziser, $hodnoceni, $popis, $autor);
    $stmt->execute();

    // Získání ID právě vloženého filmu
    $id = $conn->insert_id;

    // Zpracování plakátu
    if (!empty($_FILES['plakat']['name'])) {
        $plakatTmp = $_FILES['plakat']['tmp_name'];
        $plakatName = $_FILES['plakat']['name'];
        $extension = strtolower(pathinfo($plakatName, PATHINFO_EXTENSION));

        if (in_array($extension, ['jpg', 'jpeg'])) {
            $target_dir = "plakaty/";
            $target_file = $target_dir . $id . ".jpg";

            // Kontrola, zda adresář existuje
            if (!is_dir($target_dir)) {
                mkdir($target_dir, 0755, true);
            }

            // Pokus o přesun
            if (move_uploaded_file($plakatTmp, $target_file)) {
                // OK
            } else {
                echo "Nepodařilo se uložit plakát.";
                exit();
            }
        } else {
            echo "Nepodporovaný formát obrázku. Použijte .jpg nebo .jpeg.";
            exit();
        }
    }

    header('Location: index.php');
    exit();
}

// Načtení dostupných žánrů pro dropdown
$zanry = [];
$result = $conn->query("SELECT DISTINCT zanr FROM filmy ORDER BY zanr ASC");
while ($row = $result->fetch_assoc()) {
    if (!empty($row['zanr'])) {
        $zanry[] = $row['zanr'];
    }
}
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">  
    <meta name="description" content="Přidání nového filmu do databáze. Filmy přidáváte ke schválení administrátorovi. Po schválení se film zobrazí v seznamu.">
    <meta name="keywords" content="filmy, přidat film, databáze filmů, přidání filmu, schválení filmu">
    <meta name="author" content="0ndra_m_">
    <link rel="icon" type="image/svg" href="logo.svg">
    <title>Přidat film</title>
    <script src="theme-toggle.js"></script>
    <script>
         if (localStorage.getItem('dark-mode') === 'true') {
          document.documentElement.classList.add('dark-mode');
         }
    </script>
    <link rel="stylesheet" href="styles.css">
</head>
<body>

<header>
<h1>Přidat nový film</h1>
    </header>
    <nav>
    <a class="button" href="index.php">⬅️ Zpět na filmy</a> 
    <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
</nav>
    <div class="container">
    <p>Filmy přidáváte ke schválení administrátorovi. Po schválení se film zobrazí v seznamu.</p>

    <form method="post" enctype="multipart/form-data" class="add-form">
        <label>Název filmu:</label>
        <input type="text" name="nazev" placeholder="Název filmu" required>

        <label>Rok:</label>
        <input type="number" name="rok" placeholder="Rok" required>

        <label>Žánr:</label>
        <input list="zanry" type="text" name="zanr" placeholder="Vyber nebo napiš žánr" required>
        <datalist id="zanry">
            <?php foreach ($zanry as $zanrOption): ?>
                <option value="<?= htmlspecialchars($zanrOption) ?>">
            <?php endforeach; ?>
        </datalist>

        <label>Režisér:</label>
        <input type="text" name="reziser" placeholder="Režisér" >

        <label>Hodnocení:</label>
        <input type="number" step="0.1" name="hodnoceni" placeholder="Hodnocení" >

        <label>Popis:</label>
        <textarea name="popis" placeholder="Popis" ></textarea>

        <label>Plakát (.jpg):</label>
        <input type="file" name="plakat" accept=".jpg,.jpeg" >

        <button type="submit">📤 Odeslat ke schválení</button>
    </form>
</div>
</body>
    <footer>
       <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
    </footer>
</html>
