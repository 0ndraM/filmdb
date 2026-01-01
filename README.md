
# 🎬 Správa filmové databáze

Tento webový projekt slouží pro správu databáze filmů. Umožňuje uživatelům přidávat, upravovat a prohlížet filmy. Má oddělené přístupy pro běžné uživatele, autory, administrátory a vlastníky systému. Produkční web [**zde**](https://0ndra.maweb.eu/FilmDB/index.php)  


## 🔧 Funkce

- Přihlášení/registrace uživatelů
- Role: `user`, `admin`, `owner`
- Přidávání a úprava filmů
  - Autoři mohou upravovat pouze své filmy
  - Admin a Owner mohou upravovat všechny filmy
- Schvalování filmů (admin/owner)
- Logování změn do tabulky `filmy_log`
- Nahrávání plakátů filmů (.jpg)
- Prohlížení i neschválených filmů (pouze autor, admin, owner)
- Možnost změny jména a hesla
- Tmavý / světlý motiv

## 🧱 Struktura databáze

### `filmy`
| Sloupec     | Typ           | Popis                          |
|-------------|----------------|--------------------------------|
| id          | INT AUTO_INCREMENT | Primární klíč             |
| nazev       | VARCHAR(255)  | Název filmu                    |
| rok         | INT           | Rok vydání                     |
| zanr        | VARCHAR(100)  | Žánr                           |
| reziser     | VARCHAR(255)  | Režisér                        |
| hodnoceni   | DECIMAL(3,1)  | Hodnocení (např. 8.5)          |
| popis       | TEXT          | Popis filmu                    |
| autor       | VARCHAR(50)   | Autor záznamu (uživatel)       |
| schvaleno   | BOOLEAN       | Stav schválení filmu           |
| vytvoreno   | DATETIME      | Datum vytvoření/upravení       |

### `filmy_log`
Logovací tabulka ukládající každou změnu filmu.

| Sloupec     | Typ           | Popis                              |
|-------------|----------------|------------------------------------|
| id          | INT AUTO_INCREMENT | Primární klíč                |
| film_id     | INT           | Odkaz na `filmy.id`               |
| ...         | ...           | Kopie ostatních polí z `filmy`    |
| autor       | VARCHAR(50)   | Uživatel, který provedl změnu     |
| zmeneno     | TIMESTAMP     | Čas změny                         |

### `uzivatele`
| Sloupec   | Typ                           | Popis                              |
|-----------|-------------------------------|------------------------------------|
| id        | INT                           | Primární klíč                      |
| username  | VARCHAR(50)                   | Uživatelské jméno                  |
| password  | VARCHAR(255)                  | Hashované heslo                    |
| role      | ENUM('owner','admin','user')  | Role uživatele (výchozí: `user`)   |

### `logy`
Logovací tabulka ukládající každou změnu oprávnění.


| Sloupec | Typ         | Popis                              |
|---------|-------------|------------------------------------|
| id      | INT         | Primární klíč                      |
| autor   | VARCHAR(50) | Uživatelské jméno autora akce     |
| akce    | TEXT        | Popis provedené akce               |
| cas     | DATETIME    | Čas záznamu (výchozí: NOW)         |

## 🗂️ Struktura souborů

```
📁source/
├── 📁 hlphp/
│   ├── db.php               # Připojení k databázi
│   ├── export_filmy_log.php # Export logů správy filmů
│   ├── export_log.php       # Export logů správy uživatelů
│   ├── filmy_api.php        # Api pro ajax
│   ├── logout.php           # Odhlášení
│   ├── odschvalit.php       # Odschválení filmu
│   ├── promenit_roli.php    # Úprava rolí
│   ├── schvalit.php         # Schválení filmu
│   ├── smazat.php           # Smazání filmu (admin nebo owner)
│   └── smazat_uzivatele.php # Smazání uživatele
├── 📁 plakaty/              # Složka pro nahrané plakáty (.jpg)
├── 📁 vendor/               # Composer balíčky (Firebase/JWT)
├── add_api.php              # Endpoint pro přidání filmu (Multipart Form Data)
├── api_login.php            # Endpoint pro získání JWT tokenu
├── api_settings.php         # Endpoint pro změnu hesla a jména (transakční)
├── api_get_genres.php       # Endpoint pro získání žánrů
├── approve_api.php          # Endpoint pro administrátorské schvalování
├── edit_api.php             # Endpoint pro úpravu filmů (Multipart Form Data)
├── filmy_api.php            # Hlavní API pro načítání filmů do winformu
├── add.php                  # Přidání filmu
├── admin.php                # Admin sekce pro správu a schvalování
├── edit.php                 # Úprava filmu (s kontrolou práv)
├── filmy_db.sql             # SQL skript pro vytvoření databáze
├── index.php                # Úvodní stránka s AJAX filtrováním
├── info.php                 # Detail filmu 
├── login.php                # Přihlášení
├── logo.svg                 # Favicon
├── logs.php                 # Logy (pouze owner)
├── register.php             # Registrace
├── script.js                # AJAX skripty pro vyhledávání
├── settings.php             # Změna jména a hesla
├── styles.css               # Stylování
├── theme-toggle.js          # Přepínání motivu
└── README.md                # Tento soubor
```

## 🔐 Role a přístupová práva

| Role   | Popis                                      |
|--------|--------------------------------------------|
| user   | Může přidat filmy, upravit své vlastní     |
| admin  | Může upravovat vše, schvalovat             |
| owner  | Má stejná práva jako admin + případná rozšíření |


## 🧪 Testovací účty

```
user / user
admin / admin
owner / owner
```

## 📌 Poznámky

- Plakáty se ukládají do složky `plakaty/` ve formátu `ID.jpg`.
- Nepovolený přístup je automaticky přesměrován na přihlášení.
- Neschválené filmy jsou viditelné pouze autorovi a administrátorům.



# 🖥️ WinForm Aplikace (C# Client)
Desktopový klient umožňuje efektivní správu databáze přímo z prostředí Windows.

## Technické parametry:
- Architektura: .NET Framework / .NET s využitím HttpClient pro REST komunikaci.

- Autentizace: JWT (JSON Web Token) uložený v AppContext. Token je přikládán ke každému požadavku v hlavičce Authorization: Bearer.

- Dynamic UI: Položky menu a akce se vykreslují na základě oprávnění uživatele (Role-Based Access Control).

## Přehled formulářů
 - **Mainform**: Hlavní dashboard s dynamickým Gridem, který podporuje řazení, po dvojkliku na název filmu zobrazí detail.
 
- **FilmDetailForm**: Deatail filmu, po kliknutí na plakát se zvětší, formulář se zavře po  stisku klávesy Esc.

- **SettingsForm**: Modální okno pro změnu jména/hesla s automatickým odhlášením při změně identity.

- **AdminForm**: Rozhraní pro schvalování a "odschvalování" filmů s filtrem "Zobrazit vše".

- **LoginForm**:	Zajišťuje vstup do systému. Po úspěšném přihlášení ukládá token do AppContext.

- **AddFilmForm**: Formulář pro přidání filmu.Slouží k zápisu dat.

- **EditFilmForm**: Inteligentní editor, který na straně klienta kontroluje, zda má uživatel právo editovat daný záznam.

**Theme Management**
Aplikace obsahuje `ThemeManager`, který rekurzivně prochází všechny ovládací prvky (`Button`, `TextBo`x, `DataGridView`, `MenuStrip`) a aplikuje tmavé barvy definované ve třídě `DarkMenuColorTable`.

## 🚀Instalace a požadavky

1. **Server**: PHP 8.0+, MySQL.

2. **Composer**: Spustit composer require firebase/php-jwt pro funkčnost API.

3. **Database**: Importovat přiložený soubor filmy_db.sql.

4. **Client**: Visual Studio pro kompilaci WinForm aplikace, vyžaduje knihovnu Newtonsoft.Json.

---

> 📣 Autor: [0ndra_m_](https://github.com/0ndraM)  
 



