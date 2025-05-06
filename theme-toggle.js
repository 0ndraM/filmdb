// Přepínač motivu
function toggleTheme() {
  const isDark = document.body.classList.toggle('dark-mode');
  localStorage.setItem('dark-mode', isDark ? 'true' : 'false');
}

// Při načtení stránky nastav motiv
window.addEventListener('DOMContentLoaded', () => {
  const savedTheme = localStorage.getItem('dark-mode');
  if (savedTheme === 'true') {
    document.body.classList.add('dark-mode');
  }
});
