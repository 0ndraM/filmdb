const THEME_COLORS = {
  light: '#f3f5f8',
  dark: '#0d141f'
};

function ensureMeta(name) {
  let meta = document.querySelector(`meta[name="${name}"]`);
  if (!meta) {
    meta = document.createElement('meta');
    meta.setAttribute('name', name);
    document.head.appendChild(meta);
  }
  return meta;
}

function updateBrowserThemeColor(isDark) {
  const themeMeta = ensureMeta('theme-color');
  themeMeta.setAttribute('content', isDark ? THEME_COLORS.dark : THEME_COLORS.light);
}

function applyTheme(isDark) {
  document.documentElement.classList.toggle('dark-mode', isDark);
  document.body.classList.toggle('dark-mode', isDark);
  document.documentElement.style.colorScheme = isDark ? 'dark' : 'light';
  updateBrowserThemeColor(isDark);
}

// Přepínač motivu
function toggleTheme() {
  const isDark = !document.body.classList.contains('dark-mode');
  applyTheme(isDark);
  localStorage.setItem('dark-mode', isDark ? 'true' : 'false');
}

// Při načtení stránky nastav motiv
window.addEventListener('DOMContentLoaded', () => {
  const savedTheme = localStorage.getItem('dark-mode') === 'true';
  applyTheme(savedTheme);
});
