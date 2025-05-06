<?php
require 'db.php';

if (!isset($_GET['id'])) {
    header('Location: index.php');
    exit();
}

$id = intval($_GET['id']);
$stmt = $conn->prepare("SELECT * FROM filmy WHERE id = ? AND schvaleno = 1");
$stmt->bind_param("i", $id);
$stmt->execute();
$result = $stmt->get_result();
$film = $result->fetch_assoc();

if (!$film) {
    echo "Film nenalezen nebo neschválen.";
    exit();
}
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
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
        <a class="button" href="index.php">← Zpět na seznam filmů</a>
        <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
    </nav>
    <div class="movie-card" style="max-width: 800px; margin: 0 auto; ">
        <img src="plakaty/<?= urlencode($film['id']) ?>.jpg" alt="Plakát" class="movie-poster">
        <div class="movie-info">
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
