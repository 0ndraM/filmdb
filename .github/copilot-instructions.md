# Copilot instructions for FilmDB (projekt)

Krátký, praktický přehled pro AI agenty pracující v tomto repozitáři.

## Big picture
- Backend: PHP (s použitím session pro web UI a JWT pro API). Hlavní webové soubory jsou v `source/web/`.
- Desktop client: WinForms C# (projekt v `source/winform/filmdb/`) který volá API (see `Services/ApiServicces.cs`).
- Databáze: MySQL. Schéma je v `source/web/filmy_db.sql` (tabulky `filmy`, `filmy_log`, `uzivatele`, `acces_logy`).

## Autentizace a autorizace
- Web UI (PHP pages): používá `session_start()` (např. `login.php`).
- API: používá JWT (login API v `source/web/login_api.php`). JWT secret je čten z `JWT_SECRET` env var; použij `vendor/firebase/php-jwt` pro ověření.
- Endpoints chráněné JWT: `add_api.php` (autor z tokenu), další API v `source/web/` očekávají `Authorization: Bearer <token>`.

## Důležitá API a vzory použití
- Přihlášení (desktop): POST `source/web/login_api.php` s JSON nebo form body `{username, password}` → odpověď obsahuje `token` a `role`.
- Seznam filmů pro WinForm: GET `source/web/winformapi.php?order_by=<nazev|rok|zanr|reziser|hodnoceni>&search=<q>` → JSON pole (každý objekt obsahuje `poster` jako `plakaty/<ID>.jpg`).
- Přidání filmu (desktop/web): POST `source/web/add_api.php` jako `multipart/form-data`, hlavička `Authorization: Bearer <token>`, pole: `nazev, rok, zanr, reziser, hodnoceni, popis`, soubor `plakat` → server uloží plakát jako `plakaty/<id>.jpg`.

## Konvence v kódu
- Používejte připravené SQL dotazy tam, kde to backend již dělá (viz `add_api.php`). Pokud dotaz sestavujete dynamicky, whitelistujte hodnoty (viz `winformapi.php` a `hlphp/filmy_api.php` pro `order_by`).
- Vstupy pro výstup (UI/API) jsou escapovány (`htmlspecialchars`)—pokračujte v tomto vzoru.
- Plakáty: vždy `plakaty/<id>.jpg`. Neměňte strukturu ani názvy sloupců v DB bez důkladné kontroly.
- Blokované IP jsou v `source/web/hlphp/blocked_ips.txt` a kontroluje je `hlphp/db.php` voláním `blokuj_ip()`.

## Závislosti a build
- PHP: doporučeno 8.x; Composer závislost: `firebase/php-jwt` (viz `source/web/composer.json`).
- Desktop: Visual Studio / .NET; NuGet: `Newtonsoft.Json`.
- DB: importovat `source/web/filmy_db.sql` pro lokální vývoj.

## Co sledovat při úpravách
- Zachovat JWT ověřování a hlavičky CORS na API endpointy (`Access-Control-Allow-*`).
- Nezasahovat do způsobu ukládání plakátů (přepis path), ani do jmen sloupců v DB bez migrace.
- Ověřovat rozsahy (rok filmy) a používat whitelist pro `ORDER BY`.

## Rychlé odkazy (nejdůležitější soubory)
- README (projekt): `README.md`
- DB connect / bezpečnost: `source/web/hlphp/db.php`
- Login API (JWT): `source/web/login_api.php`
- Add API (multipart + JWT): `source/web/add_api.php`
- WinForm API (JSON): `source/web/winformapi.php`
- WinForm client ApiService: `source/winform/filmdb/Services/ApiServicces.cs`
- DB schema: `source/web/filmy_db.sql`

Pokud něco není jasné nebo chcete, abych rozšířil některou sekci (např. příklady požadavků s curl/HTTP), dejte vědět.
