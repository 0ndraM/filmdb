<?php
   session_start();
   require 'db.php';
   
   if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
      header('Location: settings.php');
      exit();
   }
   
   // Filmy
   $filmy = $conn->query("SELECT * FROM filmy ORDER BY schvaleno ASC, rok DESC");
   
   // Uživatelé
   $uzivatele = $conn->query("SELECT * FROM uzivatele ORDER BY role DESC, username ASC");
   ?>
<!DOCTYPE html>
<html lang="cs">
   <head>
      <meta charset="UTF-8">
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
         <a class="button" href="index.php">← Zpět na filmy</a>
         <a class="button" href="logs.php"> Logy</a>
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
                        <a class="btn btn-view"  href="info.php?id=<?= $film['id'] ?>">👁️ Zobrazit</a>
                        <a class="btn" href="edit.php?id=<?= $film['id'] ?>">✏️ Upravit</a>
                        <?php if (!$film['schvaleno']): ?>
                        <a class="btn btn-approve" href="schvalit.php?id=<?= $film['id'] ?>">✅ Schválit</a>
                        <?php else: ?>
                        <a class="btn btn-warning" href="odschvalit.php?id=<?= $film['id'] ?>">↩️ Odschválit</a>
                        <?php endif; ?>
                        <a class="btn btn-danger" href="smazat.php?id=<?= $film['id'] ?>" onclick="return confirm('Opravdu smazat?')">🗑️ Smazat</a>
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
                        <a class="btn" href="promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=admin">⬆️ Udělat adminem</a>
                        <?php elseif ($uzivatel['role'] === 'admin'): ?>
                        <a class="btn" href="promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=user">⬇️ Odebrat admina</a>
                        <a class="btn" href="promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=owner">👑 Udělat ownerem</a>
                        <?php elseif ($uzivatel['role'] === 'owner'): ?>
                        <span>👑 Owner</span>
                        <?php endif; ?>
                        <?php elseif ($jeAdmin): ?>
                        <?php if ($uzivatel['role'] === 'admin'): ?>
                        <a class="btn" href="promenit_roli.php?id=<?= $uzivatel['id'] ?>&role=user">⬇️ Odebrat admina</a>
                        <?php endif; ?>
                        <?php endif; ?>
                        <?php if ($uzivatel['role'] !== 'owner'): ?>
                        <a class="btn btn-danger" href="smazat_uzivatele.php?id=<?= $uzivatel['id'] ?>" onclick="return confirm('Smazat uživatele?')">🗑️ Smazat</a>
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
      </div>
   </body>
   <footer>
      <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
   </footer>
</html>