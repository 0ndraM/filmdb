<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "filmydb";

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Chyba připojení: " . $conn->connect_error);
}

$conn->set_charset("utf8mb4");
?>
