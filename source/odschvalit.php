<?php
session_start();
require 'db.php';

if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: login.php');
    exit();
 }
 

if (isset($_GET['id'])) {
    $id = intval($_GET['id']);
    $stmt = $conn->prepare("UPDATE filmy SET schvaleno = 0 WHERE id = ?");
    $stmt->bind_param("i", $id);
    $stmt->execute();
}

header('Location: admin.php');
exit();
?>
