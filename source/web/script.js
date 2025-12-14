document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("filter-form");
  const grid = document.getElementById("movie-grid");

  const loadMovies = () => {
    const search = document.getElementById("search").value;
    const orderBy = document.getElementById("order_by").value;

    const params = new URLSearchParams({ search, order_by: orderBy });

    fetch(`hlphp/filmy_api.php?${params.toString()}`)
      .then(response => response.text())
      .then(html => {
        grid.innerHTML = html;
      });
  };

  // Při odeslání formuláře
  form.addEventListener("submit", e => {
    e.preventDefault();
    loadMovies();
  });

  // Načti filmy po načtení stránky
  loadMovies();
});
