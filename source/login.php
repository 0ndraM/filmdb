<?php
session_start();
require 'hlphp/db.php';

function logLoginAttempt($username, $status) {
    global $conn;
    $ip = $_SERVER['REMOTE_ADDR'];
    $stmt = $conn->prepare("INSERT INTO acces_logy (autor, akce) VALUES (?, ?)");
    $akce = "Přihlášení uživatele '$username' - $status (IP: $ip)";
    $stmt->bind_param("ss", $username, $akce);
    $stmt->execute();
}

$chyba = '';

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_POST['username'];
    $password = $_POST['password'];

    // Bezpečné vyhledání uživatele
    $stmt = $conn->prepare("SELECT * FROM uzivatele WHERE username = ?");
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $vysledek = $stmt->get_result();

    if ($vysledek->num_rows === 1) {
        $uzivatel = $vysledek->fetch_assoc();

        // Ověření hesla pomocí password_verify
        if (password_verify($password, $uzivatel['password'])) {
            $_SESSION['username'] = $uzivatel['username'];
            $_SESSION['role'] = $uzivatel['role'];
            $_SESSION['ip'] = $_SERVER['REMOTE_ADDR'];
            logLoginAttempt($username, 'úspěšné');
            header("Location: index.php");
            exit();
        } else {
            $chyba = "Špatné heslo.";
            logLoginAttempt($username, 'neúspěšné - špatné heslo');
        }
    } else {
        $chyba = "Uživatel nenalezen.";
        logLoginAttempt($username, 'neúspěšné - uživatel nenalezen');
    }
}
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">  
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Přihlášení do databáze filmů. Umožňuje uživatelům přístup k jejich účtům a správu filmů.">
    <meta name="keywords" content="přihlášení, databáze filmů, uživatelský účet, správa filmů">
    <meta name="author" content="0ndra_m_">
    <link rel="icon" type="image/svg" href="logo.svg">
    <title>Přihlášení</title>
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
         <h1>Přihlášení</h1>
      </header>
      <nav>
         <a class="button" href="index.php">⬅️ Zpět na filmy</a>
         <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
      </nav>
<div class="credentials-container">
    <?php if ($chyba): ?>
        <div class="form-error"><?= htmlspecialchars($chyba) ?></div>
    <?php endif; ?>

    <form method="post" class="credentials-form">
        <label class="form-label">Uživatelské jméno:</label>
        <input type="text" name="username" class="form-input" required>

        <label class="form-label">Heslo:</label>
        <input type="password" name="password" class="form-input" required>

        <button type="submit" class="form-button">Přihlásit se</button>
    </form>
    <p>Nemáte účet? <a href="register.php">Zaregistrujte se</a></p>
</div>
</body>
    <footer>
       <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
    </footer>
</html>
