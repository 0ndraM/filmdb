
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

## 🗂️ Struktura souborů

```
📁 projekt/
├── db.php                # Připojení k databázi
├── login.php             # Přihlášení
├── logout.php            # Odhlášení
├── register.php          # Registrace
├── index.php             # Úvodní stránka / výpis filmů
├── add.php               # Přidání filmu
├── smazat.php            # Smazání filmu (admin nebo owner)
├── edit.php              # Úprava filmu (s kontrolou práv)
├── info.php              # Detail filmu (s kontrolou přístupnosti)
├── admin.php             # Admin sekce pro správu a schvalování
├── settings.php          # Změna jména a hesla
├── plakaty/              # Složka pro nahrané plakáty
├── styles.css            # Stylování
├── export_log.php        # Export logů správy uživatelů
├── export_filmy_log.php  # Export logů úpravy filmů
├── schvalit.php          # Schválení filmu
├── odschválit.php        # Odschvální filmu
├── promenit_roli.php     # Úprava rolí 
├── smazat_uzivatele.php  # Smazání uživatele
├── theme-toggle.js       # Přepínání motivu
├── plakáty               # Složka plakátů filmů
├── filmy_db.sql          # SQL skripty
└── README.md             # Tento soubor
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

