<?php
session_start();
require 'db.php';

if (!isset($_SESSION['username'])) {
    header('Location: login.php');
    exit();
}

if (!isset($_GET['id'])) {
    header('Location: admin.php');
    exit();
}

$id = intval($_GET['id']);
$stmt = $conn->prepare("SELECT * FROM filmy WHERE id = ?");
$stmt->bind_param("i", $id);
$stmt->execute();
$film = $stmt->get_result()->fetch_assoc();

if (!$film) {
    echo "Film nenalezen.";
    exit();
}

// Přístupová kontrola
$aktualniUzivatel = $_SESSION['username'];
$jeAutor = $film['autor'] === $aktualniUzivatel;
$jeAdmin = in_array($_SESSION['role'], ['admin', 'owner']);

if (!$jeAutor && !$jeAdmin) {
    echo "Nemáte oprávnění upravovat tento film.";
    exit();
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $id = $_POST['id'];
    $nazev = $_POST['nazev'];
    $rok = $_POST['rok'];
    $zanr = $_POST['zanr'];
    $reziser = $_POST['reziser'];
    $hodnoceni = $_POST['hodnoceni'];
    $popis = $_POST['popis'];

    // Uložit změny bez změny autora
    $stmt = $conn->prepare("UPDATE filmy SET nazev=?, rok=?, zanr=?, reziser=?, hodnoceni=?, popis=?, vytvoreno=NOW() WHERE id=?");
    $stmt->bind_param("sissdsi", $nazev, $rok, $zanr, $reziser, $hodnoceni, $popis, $id);
    $stmt->execute();

    // Nahrání nového plakátu (volitelné)
    if (!empty($_FILES['plakat']['name']) && mime_content_type($_FILES['plakat']['tmp_name']) === 'image/jpeg') {
        $target_dir = "plakaty/";
        $target_file = $target_dir . $id . ".jpg";
        move_uploaded_file($_FILES['plakat']['tmp_name'], $target_file);
    }

    // Uložení do logu (zachování aktuálního autora z tabulky, ale uložení "kdo to změnil")
    $upravujici = $_SESSION['username'];
    $stmt_log = $conn->prepare("INSERT INTO filmy_log (film_id, nazev, rok, zanr, reziser, hodnoceni, popis, autor) VALUES (?, ?, ?, ?, ?, ?, ?, ?)");
    $stmt_log->bind_param("isissdss", $id, $nazev, $rok, $zanr, $reziser, $hodnoceni, $popis, $upravujici);
    $stmt_log->execute();
    $stmt_log->close();

    header('Location: admin.php');
    exit();
}
?>

<!DOCTYPE html>
<html lang="cs">
   <head>
      <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <meta name="description" content="Upravit film v databázi. Umožňuje administrátorům a autorům upravit údaje o filmech.">
      <meta name="keywords" content="upravit film, databáze filmů, úprava filmu, správa filmů">
      <meta name="author" content="0ndra_m_">
      <link rel="icon" href="favicon.ico" type="image/x-icon">
      <title>Upravit film</title>
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
         <h1>Upravit film</h1>
      </header>
      <nav> 
         <a class="button" href="admin.php">⬅️ Zpět do admin sekce</a>
         <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
      </nav>
      <div class="container">
         <p>Upravte údaje o filmu a klikněte na "Uložit změny".</p>
         <p>Pokud chcete změnit plakát, nahrajte nový soubor.</p>
         <form method="post" enctype="multipart/form-data" class="add-form">
            <label>Název:</label>
            <input type="text" name="nazev" value="<?= htmlspecialchars($film['nazev']) ?>" required>
            <label>Rok:</label>
            <input type="number" name="rok" value="<?= $film['rok'] ?>" required>
            <label>Žánr:</label>
            <input type="text" name="zanr" value="<?= htmlspecialchars($film['zanr']) ?>" >
            <label>Režisér:</label>
            <input type="text" name="reziser" value="<?= htmlspecialchars($film['reziser']) ?>" >
            <label>Hodnocení:</label>
            <input type="number" step="0.1" name="hodnoceni" value="<?= $film['hodnoceni'] ?>" >
            <label>Popis:</label>
            <textarea name="popis"><?= htmlspecialchars($film['popis']) ?></textarea>

            <?php
            // Check if the film has a poster (plakat)
            $plakatPath = "plakaty/" . $film['id'] . ".jpg";
            if (file_exists($plakatPath)) {
                echo '<p>Aktuální plakát:</p>';
                echo '<img src="' . $plakatPath . '" alt="Plakát filmu" style="max-width: 200px; max-height: 300px;">';
                echo '<br>';
            }
            ?>

            <label>Nahrát nový plakát:</label>
            <input type="file" name="plakat" accept=".jpg,.jpeg">
            <input type="hidden" name="id" value="<?= $film['id'] ?>">
            <button type="submit">💾 Uložit změny</button>
         </form>
      </div>
   </body>
   <footer>
      <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
   </footer>
</html>