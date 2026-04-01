<?php
session_start();
require 'hlphp/db.php';

// 1. Ověření role
if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: settings.php');
    exit();
}

$username = $_SESSION['username'];

// 2. Získání údajů pomocí Prepared Statement
$stmt = $conn->prepare("SELECT * FROM uzivatele WHERE username = ?");
$stmt->bind_param("s", $username);
$stmt->execute();
$user_result = $stmt->get_result();

if ($user_result->num_rows > 0) {
    $user = $user_result->fetch_assoc();
} else {
    die("Uživatel nenalezen.");
}

// 3. Zpracování POST požadavku
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    
    // Změna jména
    if (!empty($_POST['new_username'])) {
        $new_username = $_POST['new_username'];
        $upd_name = $conn->prepare("UPDATE uzivatele SET username = ? WHERE username = ?");
        $upd_name->bind_param("ss", $new_username, $username);
        $upd_name->execute();
        
        $_SESSION['username'] = $new_username;
        $username = $new_username; // Pro případný následný update hesla
    }

    // Změna hesla (pouze pokud není prázdné)
    if (!empty($_POST['new_password'])) {
        $new_password = password_hash($_POST['new_password'], PASSWORD_DEFAULT);
        $upd_pass = $conn->prepare("UPDATE uzivatele SET password = ? WHERE username = ?");
        $upd_pass->bind_param("ss", $new_password, $username);
        $upd_pass->execute();
    }

    header('Location: admin.php?success=1');
    exit();
}

// Načtení dat pro tabulky
$filmy = $conn->query("SELECT * FROM filmy ORDER BY schvaleno ASC, nazev ASC");
$uzivatele = $conn->query("SELECT * FROM uzivatele ORDER BY role DESC, username ASC");
?>
<!DOCTYPE html>
<html lang="cs">
   <head>
      <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0, viewport-fit=cover">
      <meta name="description" content="Admin sekce pro správu filmů a uživatelů. Umožňuje schvalovat, upravovat a mazat filmy a uživatele.">
      <meta name="keywords" content="admin, správa filmů, správa uživatelů, schvalování filmů, úprava filmů, mazání filmů">
      <meta name="author" content="0ndra_m_">
      <link rel="icon" type="image/svg" href="logo.svg">
      <title>Admin sekce</title>
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
         <h1>Admin sekce</h1>
      </header>
      <nav>
         <a class="button" href="index.php">⬅️ Zpět na filmy</a>
         <?php if ($_SESSION['role'] == 'owner'): ?>
         <a class="button" href="logs.php">📜 Logy</a>
         <?php endif?>
         <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
      </nav>
      <div class="container">

         <h2>🎬 Správa filmů</h2>
         <div class="table-wrapper">
            <table class="admin-table">
               <thead>
                  <tr>
                     <th>Název</th>
                     <th>Autor</th>
                     <th>Přidáno</th>
                     <th>Akce</th>
                  </tr>
               </thead>
               <tbody>
                  <?php while ($film = $filmy->fetch_assoc()): ?>
                  <tr>
                     <td><?= htmlspecialchars($film['nazev']) ?></td>
                     <td><?= htmlspecialchars($film['autor']) ?></td>
                     <td><?= htmlspecialchars($film['vytvoreno']) ?></td>
                     <td>
                        <a class="btn btn-view"  href="info.php?id=<?= $film['id'] ?>">👁️Zobrazit</a>
                        <a class="btn" href="edit.php?id=<?= $film['id'] ?>">✏️Upravit</a>
                        <?php if (!$film['schvaleno']): ?>
                        <a class="btn btn-approve" href="hlphp/schvalit.php?id=<?= $film['id'] ?>">✅Schválit</a>
                        <?php else: ?>
                        <a class="btn btn-warning" href="hlphp/odschvalit.php?id=<?= $film['id'] ?>">↩️Skrýt</a>
                        <?php endif; ?>
                        <a class="btn btn-danger" href="hlphp/smazat.php?id=<?= $film['id'] ?>" onclick="return confirm('Opravdu smazat?')">🗑️Smazat</a>
                     </td>
                  </tr>
                  <?php endwhile; ?>
               </tbody>
            </table>
         </div>
         <h2>👤 Správa uživatelů</h2>
         <div class="table-wrapper">
            <table class="admin-table">
               <thead>
                  <tr>
                     <th>Uživatelské jméno</th>
                     <th>Role</th>
                     <th>Akce</th>
                  </tr>
               </thead>
               <tbody>
                  <?php while ($uzivatel = $uzivatele->fetch_assoc()): ?>
                  <tr>
                     <td><?= htmlspecialchars($uzivatel['username']) ?></td>
                     <td><?= $uzivatel['role'] ?></td>
                     <td>
                        <?php
                           $jeOwner = $_SESSION['role'] === 'owner';
                           $jeAdmin = $_SESSION['role'] === 'admin';
                           $jeJinyUzivatel = $uzivatel['username'] !== $_SESSION['username'];
                           ?>
                        <?php if ($jeJinyUzivatel): ?>
                        <?php if ($jeOwner): ?>
                        <?php if ($uzivatel['role'] === 'user'): ?>
                        <a class="btn" href="hlphp/promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=admin">⬆️ Udělat adminem</a>
                        <?php elseif ($uzivatel['role'] === 'admin'): ?>
                        <a class="btn" href="hlphp/promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=user">⬇️ Odebrat admina</a>
                        <a class="btn" href="hlphp/promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=owner">👑 Udělat ownerem</a>
                        <?php elseif ($uzivatel['role'] === 'owner'): ?>
                        <span>👑 Owner</span>
                        <?php endif; ?>
                        <?php elseif ($jeAdmin): ?>
                        <?php if ($uzivatel['role'] === 'admin'): ?>
                        <a class="btn" href="hlphp/promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=user">⬇️ Odebrat admina</a>
                        <?php endif; ?>
                        <?php endif; ?>
                        <?php if ($uzivatel['role'] !== 'owner'): ?>
                        <a class="btn btn-danger" href="hlphp/smazat_uzivatele.php?id=<?= $uzivatel['id'] ?>" onclick="return confirm('Smazat uživatele?')">🗑️ Smazat</a>
                        <?php endif; ?>
                        <?php else: ?>
                        <em>(vy)</em>
                        <?php endif; ?>
                     </td>
                  </tr>
                  <?php endwhile; ?>
               </tbody>
            </table>
         </div>
         <h2>🖊️ Změnit jméno a heslo</h2>
         <form method="POST" action="admin.php" class="credentials-form">
            <label class="form-label">Nové uživatelské jméno:</label>
            <input type="text" name="new_username" class="form-input" value="<?= htmlspecialchars($user['username']) ?>" required>
            <label class="form-label">Nové heslo:</label>
            <input type="password" name="new_password" class="form-input">
            <button type="submit" class="form-button">Uložit změny</button>
         </form>
      </div>
   </body>
   <footer>
      <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
   </footer>
</html>