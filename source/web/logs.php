<?php
session_start();
require 'hlphp/db.php';

if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: login.php');
    exit();
}

// Dotazy
$logy_role = $conn->query("SELECT * FROM logy ORDER BY cas DESC LIMIT 50");
$logy_filmy = $conn->query("SELECT * FROM filmy_log ORDER BY zmeneno DESC LIMIT 50");
$logy_login = $conn->query("SELECT * FROM acces_logy ORDER BY cas DESC LIMIT 50");
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, viewport-fit=cover">
    <meta name="description" content="Logy systému pro správu filmů. Zobrazují změny uživatelských rolí a úpravy filmů.">
    <meta name="keywords" content="logy, správa filmů, změny uživatelských rolí, úpravy filmů">
    <meta name="author" content="0ndra_m_">
    <link rel="icon" type="image/svg" href="logo.svg">
    <title>Logy systému</title>
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
        <h1>📜 Logy systému</h1>
    </header>
    <nav>
        <a class="button" href="admin.php">⬅️ Zpět na admin sekci</a>
        <a class="button" onclick="toggleTheme()">🌓 Přepnout motiv</a>
    </nav>
    <div class="container">


    <h2>🔐 Log přihlášení</h2>
        <?php if (!$logy_login): ?>
            <p style='color:red;'>Chyba při načítání logu přihlášení: <?= $conn->error ?></p>
        <?php else: ?>
            <div class="table-wrapper">
                <table class="admin-table">
                    <thead>
                        <tr>
                            <th>Autor</th>
                            <th>Akce</th>
                            <th>Čas</th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php while ($log = $logy_login->fetch_assoc()): ?>
                            <tr>
                                <td><?= htmlspecialchars($log['autor']) ?></td>
                                <td><?= htmlspecialchars($log['akce']) ?></td>
                                <td>
                                    <?= htmlspecialchars($log['cas']) ?>
                                    <?php
                                    if (preg_match('/IP: ([0-9\.]+)/', $log['akce'], $matches)) {
                                        $ip = $matches[1];
                                        $file = __DIR__ . '/blocked_ips.txt';
                                        $blocked = file_exists($file) ? file($file, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES) : [];
                                        if (in_array($ip, $blocked)) {
                                            echo " <a class='btn btn-success' href='hlphp/odblokuj_ip.php?ip=" . urlencode($ip) . "' onclick=\"return confirm('Odblokovat IP $ip?')\">Odblokovat IP</a>";
                                        } else {
                                            echo " <a class='btn btn-danger' href='hlphp/blokuj_ip.php?ip=" . urlencode($ip) . "' onclick=\"return confirm('Zablokovat IP $ip?')\">Blokovat IP</a>";
                                        }
                                    }
                                    ?>
                                </td>
                            </tr>
                        <?php endwhile; ?>
                    </tbody>
                </table>
            </div>
        <?php endif; ?>
        <h2>👥 Log změn uživatelských rolí</h2>
        <?php if (!$logy_role): ?>
            <p style='color:red;'>Chyba při načítání logu rolí: <?= $conn->error ?></p>
        <?php else: ?>
            <div class="table-wrapper">
                <table class="admin-table">
                    <thead>
                        <tr>
                            <th>Autor</th>
                            <th>Akce</th>
                            <th>Čas</th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php while ($log = $logy_role->fetch_assoc()): ?>
                            <tr>
                                <td><?= htmlspecialchars($log['autor']) ?></td>
                                <td><?= htmlspecialchars($log['akce']) ?></td>
                                <td><?= htmlspecialchars($log['cas']) ?></td>
                            </tr>
                        <?php endwhile; ?>
                    </tbody>
                </table>
            </div>
        <?php endif; ?>

        <form method="post" action="hlphp/export_log.php" style="margin-top: 20px;">
            <button type="submit" class="button">⬇️ Exportovat log rolí do CSV</button>
        </form>

        <h2>🎬 Log změn filmů</h2>
        <?php if (!$logy_filmy): ?>
            <p style='color:red;'>Chyba při načítání logu filmů: <?= $conn->error ?></p>
        <?php else: ?>
            <div class="table-wrapper">
                <table class="admin-table">
                    <thead>
                        <tr>
                            <th>Film (aktuální název)</th>
                            <th>Rok</th>
                            <th>Žánr</th>
                            <th>Režisér</th>
                            <th>Hodnocení</th>
                            <th>Autor změny</th>
                            <th>Čas změny</th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php while ($film_log = $logy_filmy->fetch_assoc()): ?>
                            <tr>
                                <td><?= htmlspecialchars($film_log['nazev']) ?></td>
                                <td><?= $film_log['rok'] ?></td>
                                <td><?= htmlspecialchars($film_log['zanr']) ?></td>
                                <td><?= htmlspecialchars($film_log['reziser']) ?></td>
                                <td><?= $film_log['hodnoceni'] ?></td>
                                <td><?= htmlspecialchars($film_log['autor']) ?></td>
                                <td><?= $film_log['zmeneno'] ?></td>
                            </tr>
                        <?php endwhile; ?>
                    </tbody>
                </table>
            </div>
        <?php endif; ?>

        <form method="post" action="hlphp/export_filmy_log.php" style="margin-top: 20px;">
            <button type="submit" class="button">⬇️ Exportovat log úpravy filmů do CSV</button>
        </form>

        

    </div>
</body>
<footer>
    <a href="https://github.com/0ndraM">©0ndra_m_ 2020–<?= date("Y"); ?></a>
</footer>
</html>
