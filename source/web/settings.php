<?php
session_start();
require 'hlphp/db.php'; // Předpokládá existenci $conn (MySQLi objekt)

// --- FUNKCE PRO LOGOVÁNÍ A ZPRÁVY ---
$chyba = '';
$uspech = '';

function setError($msg) {
    global $chyba;
    $chyba = $msg;
}

function setSuccess($msg) {
    global $uspech;
}
// --- KONEC FUNKCÍ PRO LOGOVÁNÍ A ZPRÁVY ---

if (!isset($_SESSION['role'])) {
    header('Location: index.php');
    exit();
}

$username = $_SESSION['username'];
$user_result = $conn->query("SELECT * FROM uzivatele WHERE username = '$username'");

if ($user_result->num_rows > 0) {
    $user = $user_result->fetch_assoc();
} else {
    die("Uživatel nenalezen.");
}

// ---------------------------------------------------------------------
// --- ZPRACOVÁNÍ POST POŽADAVKŮ ---
// ---------------------------------------------------------------------

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $puvodni_username = $_SESSION['username'];
    $provedena_zmena = false;
    
    // Zpracování změny jména
    if (isset($_POST['new_username']) && !empty($_POST['new_username'])) {
        $new_username = trim($_POST['new_username']);
        
        if ($new_username != $puvodni_username) {
            
            // 1. Kontrola, zda nové jméno již neexistuje
            $check_stmt = $conn->prepare("SELECT id FROM uzivatele WHERE username = ?");
            $check_stmt->bind_param("s", $new_username);
            $check_stmt->execute();
            if ($check_stmt->get_result()->num_rows > 0) {
                setError("Uživatelské jméno '$new_username' je již obsazeno.");
            } else {
                // 2. Provedení transakce pro atomickou změnu Jména + Autora filmů
                $conn->begin_transaction();
                try {
                    // A. Aktualizace Jména v tabulce 'uzivatele'
                    $stmt_user = $conn->prepare("UPDATE uzivatele SET username = ? WHERE username = ?");
                    $stmt_user->bind_param("ss", $new_username, $puvodni_username);
                    $stmt_user->execute();
                    
                    // B. Aktualizace pole 'autor' v tabulce 'filmy'
                    $stmt_films = $conn->prepare("UPDATE filmy SET autor = ? WHERE autor = ?");
                    $stmt_films->bind_param("ss", $new_username, $puvodni_username);
                    $stmt_films->execute();
                    
                    $conn->commit();
                    
                    // Aktualizace stavu Session a lokálních proměnných
                    $_SESSION['username'] = $new_username;
                    $username = $new_username;
                    $uspech = "Uživatelské jméno bylo úspěšně změněno na '$new_username'.";
                    $provedena_zmena = true;
                    
                } catch (mysqli_sql_exception $e) {
                    $conn->rollback();
                    setError("Chyba při změně jména a filmů: " . $e->getMessage());
                }
            }
        }
    }

    // Zpracování změny hesla
    if (isset($_POST['new_password']) && !empty($_POST['new_password'])) {
        $new_password = $_POST['new_password'];
        $confirm_password = $_POST['confirm_password']; // Nové pole pro potvrzení

        if ($new_password !== $confirm_password) {
            setError("Heslo a potvrzení se neshodují!");
        } elseif (strlen($new_password) < 8) { // Jednoduchá kontrola délky
             setError("Heslo musí mít alespoň 8 znaků.");
        } else {
            $hashed_password = password_hash($new_password, PASSWORD_DEFAULT);
            $stmt = $conn->prepare("UPDATE uzivatele SET password = ? WHERE username = ?");
            $stmt->bind_param("ss", $hashed_password, $username);
            
            if ($stmt->execute()) {
                $uspech = empty($uspech) ? "Heslo bylo úspěšně změněno." : $uspech . " Heslo bylo úspěšně změněno.";
                $provedena_zmena = true;
            } else {
                setError("Chyba při aktualizaci hesla.");
            }
        }
    }
    
    // Po dokončení změn se přesměrujeme (pokud neproběhla chyba), aby se aktualizoval formulář
    if (empty($chyba) && $provedena_zmena) {
        // Přidání úspěšné zprávy do URL, aby přežila přesměrování
        header("Location: settings.php?msg=" . urlencode($uspech));
        exit();
    }
}

// Zpracování zprávy po přesměrování (GET parametr)
if (isset($_GET['msg'])) {
    $uspech = htmlspecialchars($_GET['msg']);
}

// Získání aktuálních údajů o uživatelském účtu (pro případ, že se jméno změnilo)
// Zde bychom museli znovu načíst uživatele, aby se aktualizoval stav na stránce po redirectu
// Ale protože se spoléháme na $_SESSION['username'], stačí nechat původní kód nahoře
// a pracovat s $username, který je případně aktualizován
// Nicméně, pro aktuální data, získáme aktuální uživatelský řádek znovu:
$user_result = $conn->query("SELECT * FROM uzivatele WHERE username = '$username'");
if ($user_result->num_rows > 0) {
    $user = $user_result->fetch_assoc();
}


// Seznam filmů, které uživatel přidal (i neschválené)
$films_result = $conn->prepare("SELECT * FROM filmy WHERE autor = ? ORDER BY vytvoreno DESC");
$films_result->bind_param("s", $username);
$films_result->execute();
$films_list = $films_result->get_result();
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
         <?php if ($chyba): ?>
            <div class="form-error"><?= htmlspecialchars($chyba) ?></div>
         <?php endif; ?>
         <?php if ($uspech): ?>
            <div class="form-success"><?= htmlspecialchars($uspech) ?></div>
         <?php endif; ?>

         <h2>🖊️ Změnit jméno a heslo</h2>
         <form method="POST" action="settings.php" class="credentials-form">
            <label class="form-label">Nové uživatelské jméno:</label>
            <input type="text" name="new_username" class="form-input" value="<?= htmlspecialchars($user['username']) ?>" required>
            
            <label class="form-label">Nové heslo (nevyplňovat pro zachování stávajícího):</label>
            <input type="password" name="new_password" class="form-input">
            
            <label class="form-label">Potvrzení nového hesla:</label>
            <input type="password" name="confirm_password" class="form-input">
            
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
                  <?php while ($film = $films_list->fetch_assoc()): ?>
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