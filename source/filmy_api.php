<?php
include 'db.php';

$order_by = $_GET['order_by'] ?? 'nazev';
$search = $_GET['search'] ?? '';

$allowed_columns = ['nazev', 'rok', 'zanr', 'reziser', 'hodnoceni'];
if (!in_array($order_by, $allowed_columns)) {
    $order_by = 'nazev';
}

$search_safe = $conn->real_escape_string($search);

$sql = "SELECT * FROM filmy WHERE nazev LIKE '%$search_safe%' AND schvaleno = 1 ORDER BY $order_by";
$result = $conn->query($sql);

while ($row = $result->fetch_assoc()):
?>
<div class="movie-card">
   <a href="info.php?id=<?= $row['id'] ?>">
      <img src="plakaty/<?= urlencode($row['id']) ?>.jpg" alt="Plakát" class="movie-poster">
      <div class="movie-title"><?= htmlspecialchars($row['nazev']) ?> (<?= $row['rok'] ?>)</div>
   </a>
</div>
<?php endwhile; ?>
