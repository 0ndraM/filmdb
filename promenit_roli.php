<?php
session_start();
require 'db.php';

if (!isset($_SESSION['role']) || !in_array($_SESSION['role'], ['admin', 'owner'])) {
    header('Location: login.php');
    exit();
}

$id = intval($_GET['id']);
$novaRole = $_GET['role'];

// Získání původního uživatele
$stmt = $conn->prepare("SELECT username, role FROM uzivatele WHERE id = ?");
$stmt->bind_param("i", $id);
$stmt->execute();
$puvodni = $stmt->get_result()->fetch_assoc();

if (!$puvodni) {
    die("Uživatel nenalezen.");
}

// Změna role
$stmt = $conn->prepare("UPDATE uzivatele SET role = ? WHERE id = ?");
$stmt->bind_param("si", $novaRole, $id);
$stmt->execute();

// Zápis do logu
$autor = $_SESSION['username'];
$akce = "Změnil roli uživateli '{$puvodni['username']}' z '{$puvodni['role']}' na '{$novaRole}'";
$stmtLog = $conn->prepare("INSERT INTO logy (autor, akce) VALUES (?, ?)");
$stmtLog->bind_param("ss", $autor, $akce);
$stmtLog->execute();

header("Location: admin.php");
exit();
?>
