-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Počítač: 127.0.0.1
-- Vytvořeno: Úte 06. kvě 2025, 17:50
-- Verze serveru: 10.4.21-MariaDB
-- Verze PHP: 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáze: `filmy_db`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `filmy`
--

CREATE TABLE `filmy` (
  `id` int(11) NOT NULL,
  `nazev` varchar(255) NOT NULL,
  `rok` int(11) NOT NULL,
  `zanr` varchar(100) DEFAULT NULL,
  `reziser` varchar(255) DEFAULT NULL,
  `hodnoceni` decimal(3,1) DEFAULT NULL,
  `popis` text DEFAULT NULL,
  `schvaleno` tinyint(1) DEFAULT 0
  `vytvoreno` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  `autor` VARCHAR(50) NOT NULL DEFAULT 'neznámý';
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;



--
-- Vypisuji data pro tabulku `filmy`
--

INSERT INTO `filmy` (`id`, `nazev`, `rok`, `zanr`, `reziser`, `hodnoceni`, `popis`, `schvaleno`) VALUES
(1, 'Pán prstenů: Společenstvo Prstenu', 2001, 'Fantasy', 'Peter Jackson', '8.8', 'Pán prstenů: Společenstvo Prstenu je fantasy film z roku 2001 režírovaný Peterem Jacksonem a založený na prvním dílu Pána prstenů anglického spisovatele J. R. R. Tolkiena. Děj se odehrává ve Středozemi a vypráví o Temném pánu Sauronovi, který hledá Jeden prsten (prsten moci). Ten se dostane k mladému hobitu Frodu Pytlíkovi (Elijah Wood). Frodo a osm dalších členů Společenstva Prstenu se vydávají na cestu k Hoře osudu do země Mordor, jediného místa, kde může být prsten zničen.\r\n\r\nFilm měl premiéru v USA 19. prosince 2001 a byl vřele přijatý kritikou i fanoušky – později mnoho z nich tvrdilo, že je to nejvěrnější adaptace původního příběhu z celé Jacksonovy trilogie. Film měl neuvěřitelný komerční úspěch, celosvětově vydělal přes 870 milionů dolarů a stal se tak druhým nejvýdělečnějším filmem roku 2001 (první byl Harry Potter a Kámen mudrců). V té době to byl pátý nejvýdělečnější film v historii a dnes je na 25. pozici v tomto žebříčku. Vyhrál 4 Oscary a pět cen BAFTA. V roce 2007 Společenstvo prstenu získalo 50. místo v žebříčku 100 nejlepších amerických filmů Amerického filmového institutu. Ten jej také zvolil druhým nejlepším fantasy filmem všech dob.\r\n\r\n', 1),
(2, 'Matrix', 1999, 'Sci-Fi', 'Wachowski Brothers', '8.7', 'Matrix (v anglickém originále The Matrix) je kultovní[3] sci-fi film z roku 1999. Je prvním z filmové série Matrix, napsané a režírované sourozenci Wachowskými. V hlavních rolích se představili Keanu Reeves, Laurence Fishburne, Carrie-Anne Mossová a Hugo Weaving. Přímé pokračování, Matrix Reloaded, mělo premiéru v květnu 2003.\r\n\r\nFilm popisuje svět v Matrixu, rozsáhlé počítačové simulaci, na který jsou připojeni lidé, žijící v něm svůj virtuální život. Tito lidé, jejichž mozek je do Matrixu napojen, si neuvědomují, že nežijí skutečný život a že je jim do mozku promítána virtuální realita. Jsou takto nevědomky využíváni stroji s umělou inteligencí, které převzaly dominanci na planetě. Tyto stroje tedy de facto chovají lidstvo pro energii z lidských těl, díky níž fungují.\r\n\r\nVe snímku lze nalézt mnoho odkazů jak na různé filozofické a náboženské myšlenky, tak na hackerskou a kyberpunkovou subkulturu. Film proslul také díky svému vynikajícímu zpracování soubojů v hongkongském stylu a inspirací v japonském anime.\r\n\r\nFilm vznikl za spolupráce studií Warner Bros. a Village Roadshow Pictures. Byl natáčen zejména v australském Sydney.', 1),
(3, 'Inception', 2010, 'Sci-Fi', 'Christopher Nolan', '8.8', 'Počátek (v anglickém originále Inception) je americký film z roku 2010 produkce Warner Brothers s Leonardem DiCapriem v hlavní roli. Autorem, producentem a režisérem filmu je Christopher Nolan. Film získal celkem čtyři Oscary. Hudbu zkomponoval Hans Zimmer, trailery složil Zack Hemsey. Distribuční název byl kvůli pečlivému utajení filmu zvolen bez znalosti děje, vhodnější by byl výraz Vnuknutí.[zdroj⁠?!]\r\n\r\nTvorba začala zhruba před deseti lety, kdy Nolan napsal osmdesátistránkové filmové zpracování o zlodějích snů. Poté, co přednesl myšlenku společnosti Warner Bros. v 2001, cítil, že potřebuje mít víc zkušeností s vytvářením filmů ve velkém měřítku. Proto se rozhodl pracovat na filmech Batman začíná a Temný rytíř. Strávil půl roku dolaďováním scénáře předtím, než ho Warner Bros. koupilo v únoru 2009. Natáčení začalo v Tokiu 19. června 2009 a skončilo na konci listopadu téhož roku.', 1),
(4, 'Skyfall', 2012, 'thriller', 'Sam Mendes', '10.0', 'Skyfall je dvacátá třetí filmová bondovka produkovaná Eon Productions pro společnosti MGM, Columbia Pictures a Sony Pictures Entertainment.[3] Natočil ji režisér Sam Mendes. Na scénáři se podíleli John Logan, Neal Purvis a Robert Wade. Postavu Jamese Bonda ztvárnil potřetí herec Daniel Craig.\r\n\r\nFilm pojednává o Bondově vyšetřování útoku na centrálu MI6, který je součástí spiknutí ze strany bývalého britského agenta a hlavní záporné postavy Raoula Silvy, v podání Javiera Bardema. Jeho cílem je zdiskreditovat a zabít svou bývalou řídící důstojnici M, jakožto odplatu za zradu, které se na něm měla kdysi dopustit. V bondovce se po deseti letech představily dvě vracející se postavy, jež absentovaly v předchozích dvou dílech série: Q, kterého hrál Ben Whishaw a slečna Eve Moneypenny, jíž ztvárnila Naomie Harrisová. Skyfall byl posledním snímkem pro Judi Denchovou, která se v úloze M objevila celkem sedmkrát. Její postavu šéfa MI6 převzal Ralph Fiennes, jakožto Gareth Mallory. Role smyslné Bond girl Sévérine se ujala francouzská herečka Bérénice Marloheová.\r\n\r\nMendes byl k natáčení přizván v roce 2008 po premiéře Quantum of Solace. Práce na filmu však byly pozastaveny pro finanční problémy studia MGM až do prosince 2010; během tohoto období na projektu pracoval v pozici konzultanta. Původní rozpracovaný scénář z dílny Petera Morgana dopsali do finální verze Logan, Purvis a Wade. První klapka padla v listopadu 2011 a natáčení pokračovalo na lokalitách ve Velké Británii, Číně a Turecku.\r\n\r\nSvětová premiéra proběhla za přítomnosti korunního prince Charlese a jeho choti vévodkyně Camilly 23. října 2012 v londýnské Royal Albert Hall. Ve Spojeném království se premiéra uskutečnila 26. října 2012 v kinosálech s formátem velkorozměrného kinematografického systému IMAX,[4][5] ačkoli film nebyl snímán kamerami IMAX.\r\n\r\nDo konce roku 2012 dosáhly celosvětové tržby výše jedné miliardy dolarů,[2] čímž se Skyfall stal čtrnáctým filmem v historii, jenž překročil tuto hranici a také druhým nejvýdělečnějším snímkem roku.[6] Na počátku roku 2013 byl sedmým nejvýnosnějším filmem v historii kinematografie, nejvýdělečnějším snímkem vzešlým z Velké Británie i ze studií Sony Pictures a MGM, a na prvenství dosáhl také v bondovské sérii.\r\n\r\nSkyfall obdržel několik ocenění, včetně cen BAFTA pro nejlepší film a hudbu. Z pěti nominací na Oscara proměnil na 85. ročníku udílení Cen Akademie dvě – za nejlepší píseň „Skyfall“ a střih zvuku. Na jubilejním 70. ročníku udílení Zlatých glóbů obdržel cenu za nejlepší píseň.', 1),
(5, 'The Gentlemen', 2019, 'akční', 'Guy Ritchie', '2.5', 'Gentlemani, v anglickém originále The Gentlemen, je akční komedie z roku 2019 režiséra Guye Ritchieho, který film také produkoval, je autorem scénáře a předlohy spolu s Ivanem Atkinsonem a Marnem Daviesem. Hlavní role ve filmu ztvárnili Matthew McConaughey, Charlie Hunnam, Henry Golding, Michelle Dockeryová, Jeremy Strong, Eddie Marsan, Colin Farrell a Hugh Grant.\r\n\r\nFilm vyvolal povětšinou nadšené reakce u filmových recenzentů, kteří chválili zejména režiséra, herce a scénář, mnoho z nich také kvitovalo, že Ritchie je zpátky ve formě. Nicméně někteří kritici vnímali negativně rasistický podtón a hrubý jazyk. Film byl též komerčně úspěšný, na pokladnách kin vydělal ke květnu 2020 po celém světě celkem 115 milionů amerických dolarů.\r\n\r\nFilm byl poprvé uveden dne 3. prosince 2019 v londýnském kině Curzon Mayfair. Premiéra filmu ve Spojeném království proběhla 1. ledna 2020 a ve Spojených státech amerických 24. ledna 2020. V Česku měl film premiéru 30. ledna 2020.', 1),
(6, 'Velký Gatsby', 2013, 'Romantika', 'Baz Luhrmann', '5.0', 'Velký Gatsby (v anglickém originále The Great Gatsby) je americký dramatický snímek režiséra Baze Luhrmanna, který je též jedním z autorů scénáře. Film je adaptací stejnojmenného románu od Francise Scotta Fitzgeralda. Představitelé hlavních rolí jsou Leonardo DiCaprio, Tobey Maguire, Carey Mulligan, Joel Edgerton, Elizabeth Debicki, Isla Fisher, Jason Clarke a Amitabh Bachchan.\r\n\r\nFilm sleduje životy multimilionáře Jaye Gatsbyho a jeho souseda Nicka, který se s Gatsbym setkává během bouřlivých dvacátých let. Premiéra filmu ve Spojených státech proběhla dne 10. května 2013. Snímek byl vydán ve formátu 3D a získal smíšené recenze od filmových kritiků.', 1);

-- --------------------------------------------------------

--
-- Struktura tabulky `logy`
--

CREATE TABLE `logy` (
  `id` int(11) NOT NULL,
  `autor` varchar(50) NOT NULL,
  `akce` text NOT NULL,
  `cas` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `logy`
--

INSERT INTO `logy` (`id`, `autor`, `akce`, `cas`) VALUES
(1, 'ondra', 'Změnil roli uživateli \'admin\' z \'admin\' na \'user\'', '2025-05-06 16:28:03'),
(2, 'ondra', 'Změnil roli uživateli \'admin\' z \'user\' na \'admin\'', '2025-05-06 16:28:05');

-- --------------------------------------------------------

--
-- Struktura tabulky `uzivatele`
--

CREATE TABLE `uzivatele` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `role` enum('owner','admin','user') DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vypisuji data pro tabulku `uzivatele`
--

INSERT INTO `uzivatele` (`id`, `username`, `password`, `role`) VALUES
(1, 'admin', '$2y$10$M5TZX0TDr2beVv3p52wp8uQv21MOqWSWNW8pc9ujWCflKklqRW9ge', 'admin'),
(2, 'user', '$2y$10$KXrMDM4zIBPnwok.cIgbQeNH6vNRPzKf30SBHX5TAPZ2q4rS0Htl2', 'user'),
(3, 'ondra', '$2y$10$q1iNvXKznYRWL8UN0mzkH.YAN1Q7S75fGSW4F/2BHyOSFEC76iZlG', 'owner');

--
-- Indexy pro exportované tabulky
--

CREATE TABLE filmy_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    film_id INT NOT NULL,
    nazev VARCHAR(255),
    rok INT,
    zanr VARCHAR(100),
    reziser VARCHAR(255),
    hodnoceni DECIMAL(3,1),
    popis TEXT,
    autor VARCHAR(50),
    zmeneno TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (film_id) REFERENCES filmy(id)
);

--
-- Indexy pro tabulku `filmy`
--
ALTER TABLE `filmy`
  ADD PRIMARY KEY (`id`);

--
-- Indexy pro tabulku `logy`
--
ALTER TABLE `logy`
  ADD PRIMARY KEY (`id`);

--
-- Indexy pro tabulku `uzivatele`
--
ALTER TABLE `uzivatele`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `filmy`
--
ALTER TABLE `filmy`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pro tabulku `logy`
--
ALTER TABLE `logy`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pro tabulku `uzivatele`
--
ALTER TABLE `uzivatele`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
