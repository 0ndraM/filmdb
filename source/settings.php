<?php
   session_start();
   require 'db.php';
   
   if (!isset($_SESSION['role'])) {
       header('Location: index.php');
       exit();
   }
   
   // Získání aktuálních údajů o uživatelském účtu
   $username = $_SESSION['username'];
   $user_result = $conn->query("SELECT * FROM uzivatele WHERE username = '$username'");
   
   if ($user_result->num_rows > 0) {
       $user = $user_result->fetch_assoc();
   } else {
       die("Uživatel nenalezen.");
   }
   
   // Změna jména a hesla
   if ($_SERVER['REQUEST_METHOD'] == 'POST') {
       // Změna jména
       if (isset($_POST['new_username'])) {
           $new_username = $_POST['new_username'];
           $conn->query("UPDATE uzivatele SET username = '$new_username' WHERE username = '$username'");
           $_SESSION['username'] = $new_username; // Aktualizace session proměnné
           $username = $new_username;
       }
   
       // Změna hesla
       if (isset($_POST['new_password'])) {
           $new_password = password_hash($_POST['new_password'], PASSWORD_DEFAULT);
           $conn->query("UPDATE uzivatele SET password = '$new_password' WHERE username = '$username'");
       }
       
       // Přesměrování na stránku pro zobrazení změn
       header('Location: settings.php');
       exit();
   }
   
   // Seznam filmů, které uživatel přidal (i neschválené)
   $films_result = $conn->query("SELECT * FROM filmy WHERE autor = '$username' ORDER BY vytvoreno DESC");
   ?>
<!DOCTYPE html>
<html lang="cs">
   <head>
      <meta charset="UTF-8">  
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <meta name="description" content="Úprava účtu uživatele. Umožňuje změnu uživatelského jména a hesla. Zobrazují se zde také filmy, které uživatel přidal.">
      <meta name="keywords" content="úprava účtu, změna hesla, správa uživatelského účtu, filmy uživatele">
      <meta name="author" content="0ndra_m_">
      <link rel="icon" type="image/svg" href="logo.svg">
      <title>Úprava účtu</title>
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
         <h1>Úprava účtu</h1>
      </header>
      <nav>
         <a class="button" href="index.php">⬅️ Zpět na filmy</a>
         <a class="button" onclick="toggleTheme()">🌓 Přepnout motiv</a>
      </nav>
      <div class="container">
         <h2>🖊️ Změnit jméno a heslo</h2>
         <form method="POST" action="settings.php" class="credentials-form">
            <label class="form-label">Nové uživatelské jméno:</label>
            <input type="text" name="new_username" class="form-input" value="<?= htmlspecialchars($user['username']) ?>" required>
            <label class="form-label">Nové heslo:</label>
            <input type="password" name="new_password" class="form-input">
            <button type="submit" class="form-button">Uložit změny</button>
         </form>
         <h2>🎬 Filmy, které jste přidali</h2>
         <div class="table-wrapper">
            <table class="admin-table">
               <thead>
                  <tr>
                     <th>Název</th>
                     <th>Rok</th>
                     <th>Schválený</th>
                     <th>Akce</th>
                  </tr>
               </thead>
               <tbody>
                  <?php while ($film = $films_result->fetch_assoc()): ?>
                  <tr>
                     <td><?= htmlspecialchars($film['nazev']) ?></td>
                     <td><?= htmlspecialchars($film['rok']) ?></td>
                     <td><?= $film['schvaleno'] ? 'Ano' : 'Ne' ?></td>
                     <td>
                        <a class="btn btn-view" href="info.php?id=<?= $film['id'] ?>">👁️ Zobrazit</a>
                        <a class="btn" href="edit.php?id=<?= $film['id'] ?>">✏️ Upravit</a>
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