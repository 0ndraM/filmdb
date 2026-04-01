<?php
session_start();
require_once 'hlphp/db.php';
blokuj_ip();
?>
<!DOCTYPE html>
<html lang="cs">
<head>
   <meta charset="UTF-8">
   <meta name="viewport" content="width=device-width, initial-scale=1.0, viewport-fit=cover">
   <meta name="description" content="Seznam filmů. Umožňuje procházet filmy podle názvu, roku, žánru, režiséra a hodnocení.">
   <meta name="keywords" content="filmy, seznam filmů, databáze filmů, procházení filmů, hodnocení filmů">
   <meta name="author" content="0ndra_m_">
   <link rel="icon" type="image/svg" href="logo.svg">
   <title>Filmy</title>
   <script src="theme-toggle.js"></script>
   <script>
      if (localStorage.getItem('dark-mode') === 'true') {
         document.documentElement.classList.add('dark-mode');
      }
   </script>
   <link rel="stylesheet" href="styles.css">
   <style>
      .movie-card:hover {
         transform: scale(1.05);
      }
   </style>
</head>
<body>
   <header>
      <h1>Seznam filmů</h1>
   </header>
   <nav>
      <?php if (isset($_SESSION['role'])): ?>
         <a class="button" href="add.php">➕ Přidat film</a>
         <a class="button" onclick="toggleTheme()">🌓 Přepnout motiv</a>

         <?php if ($_SESSION['role'] == 'admin' || $_SESSION['role'] == 'owner'): ?>
            <a class="button" href="admin.php">🛠️ Admin sekce</a>
         <?php else: ?>
            <a class="button" href="settings.php">⚙️ Nastavení</a>
         <?php endif; ?>

         <a class="button" href="hlphp/logout.php">🚪 Odhlásit se</a>
      <?php else: ?>
         <a class="button" onclick="toggleTheme()">🌓 Přepnout motiv</a>
         <a class="button" href="login.php">🔑 Přihlásit se</a>
      <?php endif; ?>
   </nav>

   <form class="search-form" id="filter-form">
      <input type="text" name="search" id="search" placeholder="Hledat...">
      <select name="order_by" id="order_by">
         <option value="nazev">Název</option>
         <option value="rok">Rok</option>
         <option value="zanr">Žánr</option>
         <option value="reziser">Režisér</option>
         <option value="hodnoceni">Hodnocení</option>
      </select>
      <button type="submit">Hledat / Řadit</button>
   </form>

   <div class="movie-grid" id="movie-grid">
      <!-- Výsledky filmů se načtou sem -->
   </div>
   
   <script src="script.js"></script>
</body>
<footer>
   <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a>
</footer>
</html>
