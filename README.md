
# 🎬 Správa filmové databáze

Tento webový projekt slouží pro správu databáze filmů. Umožňuje uživatelům přidávat, upravovat a prohlížet filmy. Má oddělené přístupy pro běžné uživatele, autory, administrátory a vlastníky systému.

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
📁 projekt/
 source/
├── 📁 hlphp/
│   ├── db.php               # Připojení k databázi
│   ├── export_filmy_log.php # AJAX endpoint pro načítání filmů
│   ├── export_log.php       # Export logů správy uživatelů
│   ├── filmy_api.php
│   ├── logout.php # Odhlášení
│   ├── odschvalit.php       # Odschválení filmu
│   ├── promenit_roli.php    # Úprava rolí
│   ├── schvalit.php         # Schválení filmu
│   ├── smazat.php           # Smazání filmu (admin nebo owner)
│   └── smazat_uzivatele.php # Smazání uživatele
├── 📁 plakaty/               # Složka pro nahrané plakáty (.jpg)
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

## 🛠️ Požadavky

- PHP 7.4+
- MySQL/MariaDB
- Webserver (např. Apache)
- Povolený `file_uploads` v `php.ini`

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

---

> 📣 Autor: [0ndra_m_](https://github.com/0ndraM)  

