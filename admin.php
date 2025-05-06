<?php
   session_start();
   require 'db.php';
   
   if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
      header('Location: login.php');
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
         <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
      </nav>
      <div class="container">
         <h2>🎬 Správa filmů</h2>
         <div class="table-wrapper">
            <table class="admin-table">
               <thead>
                  <tr>
                     <th>Název</th>
                     <th>Rok</th>
                     <th>Schváleno</th>
                     <th>Akce</th>
                  </tr>
               </thead>
               <tbody>
                  <?php while ($film = $filmy->fetch_assoc()): ?>
                  <tr>
                     <td><?= htmlspecialchars($film['nazev']) ?></td>
                     <td><?= htmlspecialchars($film['rok']) ?></td>
                     <td><?= $film['schvaleno'] ? 'Ano' : 'Ne' ?></td>
                     <td>
                        <?php if (!$film['schvaleno']): ?>
                        <a class="btn btn-view-dsbld" >👁️ Zobrazit</a>
                        <?php else: ?>
                        <a class="btn btn-view"  href="info.php?id=<?= $film['id'] ?>">👁️ Zobrazit</a>
                        <?php endif; ?>
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
         <?php
            $logy = $conn->query("SELECT * FROM logy ORDER BY cas DESC LIMIT 50");
            
            if (!$logy) {
                echo "<p style='color:red;'>Chyba při načítání logu: " . $conn->error . "</p>";
            } else {
            ?>
         <h2>📜 Log změn rolí</h2>
         <table class="admin-table">
            <thead>
               <tr>
                  <th>Autor</th>
                  <th>Akce</th>
                  <th>Čas</th>
               </tr>
            </thead>
            <tbody>
               <?php while ($log = $logy->fetch_assoc()): ?>
               <tr>
                  <td><?= htmlspecialchars($log['autor']) ?></td>
                  <td><?= htmlspecialchars($log['akce']) ?></td>
                  <td><?= htmlspecialchars($log['cas']) ?></td>
               </tr>
               <?php endwhile; ?>
            </tbody>
         </table>
         <?php } ?>
         <form method="post" action="export_log.php" style="margin-bottom: 20px;">
            <button type="submit" class="button">⬇️ Exportovat log do CSV</button>
         </form>
      </div>
   </body>
   <footer>
      <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
   </footer>
</html>