<?php
session_start();
require 'hlphp/db.php';

// Ověření role - přístup mají jen admini a owner
if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: login.php');
    exit();
}

// 1. Načtení blokovaných IP adres JEDNOU na začátku (efektivita)
$blocked_file = __DIR__ . '/blocked_ips.txt';
$blocked_ips = [];
if (file_exists($blocked_file)) {
    $blocked_ips = file($blocked_file, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
}

// 2. Optimalizované dotazy
$logy_role = $conn->query("SELECT * FROM logy ORDER BY cas DESC LIMIT 20");
$logy_filmy = $conn->query("SELECT * FROM filmy_log ORDER BY zmeneno DESC LIMIT 20");
$logy_login = $conn->query("SELECT * FROM acces_logy ORDER BY cas DESC LIMIT 20");
?>
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
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
    <script src="theme-toggle.js"></script>
    <style>
        /* Rychlé styly pro lepší přehlednost logů */
        .row-warning { background-color: rgba(255, 0, 0, 0.1); } /* Červený nádech pro chyby */
        .ip-badge { font-family: monospace; background: #eee; padding: 2px 5px; border-radius: 4px; color: #333; }
        .dark-mode .ip-badge { background: #333; color: #eee; }
    </style>
</head>
<body>
    <header><h1>📜 Logy systému</h1></header>
    <nav>
        <a class="button" href="admin.php">⬅️ Zpět na admin sekci</a>
        <a class="button" onclick="toggleTheme()">🌓 Přepnout motiv</a>
    </nav>
    
    <div class="container">

        <h2>🔐 Log přihlášení (Access Log)</h2>
        <div class="table-wrapper">
            <table class="admin-table">
                <thead>
                    <tr>
                        <th>Autor</th>
                        <th>Akce</th>
                        <th>Čas</th>
                        <th>Správa IP</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($log = $logy_login->fetch_assoc()): 
                        // Detekce chyby v logu pro obarvení řádku
                        $isError = (stripos($log['akce'], 'chyba') !== false || stripos($log['akce'], 'failed') !== false);
                    ?>
                    <tr class="<?= $isError ? 'row-warning' : '' ?>">
                        <td><strong><?= htmlspecialchars($log['autor']) ?></strong></td>
                        <td><?= htmlspecialchars($log['akce']) ?></td>
                        <td><?= htmlspecialchars($log['cas']) ?></td>
                        <td>
                            <?php
                            if (preg_match('/IP: ([0-9\.]+)/', $log['akce'], $matches)) {
                                $ip = $matches[1];
                                if (in_array($ip, $blocked_ips)) {
                                    echo "<a class='btn btn-success' href='hlphp/odblokuj_ip.php?ip=" . urlencode($ip) . "'>✅ Odblokovat</a>";
                                } else {
                                    echo "<a class='btn btn-danger' href='hlphp/blokuj_ip.php?ip=" . urlencode($ip) . "' onclick=\"return confirm('Zablokovat IP $ip?')\">🚫 Blokovat</a>";
                                }
                            }
                            ?>
                        </td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>

        <form method="post" action="hlphp/export_acces_logy.php" style="margin-bottom: 20px;">
            <button type="submit" class="button">⬇️ Exportovat přístupy do CSV</button>
        </form>

        <h2>👥 Log změn uživatelských rolí</h2>
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

        <form method="post" action="hlphp/export_log.php">
            <button type="submit" class="button">⬇️ Exportovat role do CSV</button>
        </form>

        <h2>🎬 Log změn filmů</h2>
        <div class="table-wrapper">
            <table class="admin-table">
                <thead>
                    <tr>
                        <th>Film</th>
                        <th>Info</th>
                        <th>Autor změny</th>
                        <th>Čas</th>
                    </tr>
                </thead>
                <tbody>
                    <?php while ($film_log = $logy_filmy->fetch_assoc()): ?>
                    <tr>
                        <td><strong><?= htmlspecialchars($film_log['nazev']) ?></strong></td>
                        <td>
                            <small>
                                Rok: <?= $film_log['rok'] ?>, 
                                Žánr: <?= htmlspecialchars($film_log['zanr']) ?>, 
                                Hodnocení: <?= $film_log['hodnoceni'] ?>/10
                            </small>
                        </td>
                        <td><?= htmlspecialchars($film_log['autor']) ?></td>
                        <td><?= $film_log['zmeneno'] ?></td>
                    </tr>
                    <?php endwhile; ?>
                </tbody>
            </table>
        </div>

        <form method="post" action="hlphp/export_filmy_log.php" style="margin-top: 20px;">
            <button type="submit" class="button">⬇️ Exportovat filmy do CSV</button>
        </form>

    </div>
    <footer>
        <a href="https://github.com/0ndraM">©0ndra_m_ 2020–<?= date("Y"); ?></a>
    </footer>
</body>
</html>