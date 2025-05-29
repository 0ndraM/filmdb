<?php
session_start();
require 'db.php';

$id = intval($_GET['id']);
$film = null;

// Pokud film existuje
$stmt = $conn->prepare("SELECT * FROM filmy WHERE id = ?");
$stmt->bind_param("i", $id);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    $film = $result->fetch_assoc();
}

// Pokud film existuje
if ($film) {
    // Pokud je film schválený, není potřeba přihlášení
    if ($film['schvaleno'] == 1) {
        // Film je schválený, takže se může zobrazit bez přihlášení
    } else {
        // Pokud film není schválený, musíme zkontrolovat přihlášení a roli
        if (!isset($_SESSION['username']) || !in_array($_SESSION['role'], ['admin', 'owner']) && $film['autor'] != $_SESSION['username']) {
            // Pokud není uživatel přihlášený nebo není admin/owner a není autorem, přesměrujeme na login
            header('Location: login.php');
            exit();
        }
    }
} else {
    echo "Film nenalezen.";
    exit();
}
?>

<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">  
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Detail filmu <?= htmlspecialchars($film['nazev']) ?>. Zobrazí informace o filmu, jako je název, rok, žánr, režisér a hodnocení.">
    <meta name="keywords" content="film, <?= htmlspecialchars($film['nazev']) ?>, detail filmu, informace o filmu, <?= htmlspecialchars($film['zanr']) ?>, <?= htmlspecialchars($film['reziser']) ?>">
    <meta name="author" content="0ndra_m_">
    <link rel="icon" type="image/svg" href="logo.svg">
    <title><?= htmlspecialchars($film['nazev']) ?></title>
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
        <h1><?= htmlspecialchars($film['nazev']) ?></h1>
    </header>
    <nav>
        <a class="button" href="index.php">⬅️ Zpět na seznam filmů</a>
        <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
    </nav>
    <div class="movie-card detail-card">
        <div class="poster-wrapper">
            <img src="plakaty/<?= urlencode($film['id']) ?>.jpg" alt="Plakát" class="movie-poster detail-poster">
        </div>
        <div class="movie-info detail-info">
            <p><strong>Rok:</strong> <?= $film['rok'] ?></p>
            <p><strong>Žánr:</strong> <?= htmlspecialchars($film['zanr']) ?></p>
            <p><strong>Režisér:</strong> <?= htmlspecialchars($film['reziser']) ?></p>
            <p><strong>Hodnocení:</strong> <?= $film['hodnoceni'] ?></p>
            <p><strong>Popis:</strong><br><?= nl2br(htmlspecialchars($film['popis'])) ?></p>
        </div>
    </div>
</body>
    <footer>
       <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
    </footer>
</html>
