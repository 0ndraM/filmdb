<?php
// Zkusíme pouze nastavit hlavičku a vypsat JSON
header('Content-Type: application/json; charset=utf-8');

$test_data = [
    "status" => "OK",
    "message" => "PHP běží, ale DB připojení není ověřeno."
];

echo json_encode($test_data);
?>