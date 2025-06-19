<?php
session_start();
require 'db.php';

if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: ../login.php');
    exit();
 }

$id = $_GET['id'] ?? null;

if ($id) {
    $stmt = $conn->prepare("DELETE FROM uzivatele WHERE id = ?");
    $stmt->bind_param("i", $id);
    $stmt->execute();
}

header("Location: ..úadmin.php");
exit();
