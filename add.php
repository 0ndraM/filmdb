<?php
session_start();
require 'db.php';

if (!isset($_SESSION['role'])) {
    header('Location: login.php');
    exit();
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $nazev = $_POST['nazev'];
    $rok = $_POST['rok'];
    $zanr = $_POST['zanr'];
    $reziser = $_POST['reziser'];
    $hodnoceni = $_POST['hodnoceni'];
    $popis = $_POST['popis'];

    // Nejprve vlož film do DB bez plakátu, aby získal ID
    $stmt = $conn->prepare("INSERT INTO filmy (nazev, rok, zanr, reziser, hodnoceni, popis, schvaleno) VALUES (?, ?, ?, ?, ?, ?, 0)");
    $stmt->bind_param("sissds", $nazev, $rok, $zanr, $reziser, $hodnoceni, $popis);
    $stmt->execute();

    // Získání ID právě vloženého filmu
    $id = $conn->insert_id;

    // Zpracování plakátu
    if (!empty($_FILES['plakat']['name']) && mime_content_type($_FILES['plakat']['tmp_name']) === 'image/jpeg') {
        $target_dir = "plakaty/";
        $target_file = $target_dir . $id . ".jpg";
        move_uploaded_file($_FILES['plakat']['tmp_name'], $target_file);
    }

    header('Location: index.php');
    exit();
}
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
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
    <a class="button" href="index.php">← Zpět na filmy</a> 
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
        <input type="text" name="zanr" placeholder="Žánr" required>

        <label>Režisér:</label>
        <input type="text" name="reziser" placeholder="Režisér" required>

        <label>Hodnocení:</label>
        <input type="number" step="0.1" name="hodnoceni" placeholder="Hodnocení" required>

        <label>Popis:</label>
        <textarea name="popis" placeholder="Popis" required></textarea>

        <label>Plakát (.jpg):</label>
        <input type="file" name="plakat" accept=".jpg,.jpeg" required>

        <button type="submit">📤 Odeslat ke schválení</button>
    </form>
</div>
</body>
    <footer>
       <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
    </footer>
</html>
