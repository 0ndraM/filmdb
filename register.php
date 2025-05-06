<?php
session_start();
require 'db.php';

$chyba = '';
$zprava = '';

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = trim($_POST['username']);
    $password = $_POST['password'];
    $confirm = $_POST['confirm'];

    // Kontrola prázdných polí
    if (empty($username) || empty($password) || empty($confirm)) {
        $chyba = "Všechna pole jsou povinná.";
    } elseif ($password !== $confirm) {
        $chyba = "Hesla se neshodují.";
    } else {
        // Zkontroluj, zda uživatel již existuje
        $stmt = $conn->prepare("SELECT id FROM uzivatele WHERE username = ?");
        $stmt->bind_param("s", $username);
        $stmt->execute();
        $stmt->store_result();

        if ($stmt->num_rows > 0) {
            $chyba = "Uživatelské jméno je již obsazené.";
        } else {
            // Vložení nového uživatele
            $hashed = password_hash($password, PASSWORD_DEFAULT);

            $role = 'user'; // výchozí role

            $stmt = $conn->prepare("INSERT INTO uzivatele (username, password, role) VALUES (?, ?, ?)");
            $stmt->bind_param("sss", $username, $hashed, $role);

            if ($stmt->execute()) {
                $zprava = "Registrace byla úspěšná. <a href='login.php'>Přihlaste se</a>.";
            } else {
                $chyba = "Registrace selhala. Zkuste to znovu.";
            }
        }
    }
}
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <title>Registrace</title>
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
         <h1>Registrace</h1>
      </header>
      <nav>
         <a class="button" href="index.php">← Zpět na filmy</a>
         <a class="button"  onclick="toggleTheme()">🌓 Přepnout motiv</a>
      </nav>
<div class="container">

    <?php if ($chyba): ?>
        <div class="error"><?= htmlspecialchars($chyba) ?></div>
    <?php elseif ($zprava): ?>
        <div class="success"><?= $zprava ?></div>
    <?php endif; ?>

    <form method="post">
        <label>Uživatelské jméno:</label>
        <input type="text" name="username" required>

        <label>Heslo:</label>
        <input type="password" name="password" required>

        <label>Potvrzení hesla:</label>
        <input type="password" name="confirm" required>

        <button type="submit">Registrovat se</button>
    </form>
    <p>Máte již účet? <a href="login.php">Přihlaste se</a></p>

</div>
</body>
    <footer>
       <a href="https://github.com/0ndraM">©0ndra_m_  2020-<?php echo date("Y");?></a> 
    </footer>
</html>
