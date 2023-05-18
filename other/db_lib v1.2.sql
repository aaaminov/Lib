CREATE DATABASE  IF NOT EXISTS `db_lib` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_lib`;
-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: db_lib
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `author`
--

DROP TABLE IF EXISTS `author`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `author` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL,
  `photo` varchar(512) DEFAULT NULL,
  `biography` varchar(4096) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `author`
--

LOCK TABLES `author` WRITE;
/*!40000 ALTER TABLE `author` DISABLE KEYS */;
INSERT INTO `author` VALUES (1,'Александр Пушкин','https://upload.wikimedia.org/wikipedia/commons/thumb/5/56/Kiprensky_Pushkin.jpg/512px-Kiprensky_Pushkin.jpg','Пушкин Александр Сергеевич - русский поэт, драматург и прозаик, заложивший основы русского реалистического направления, литературный критик и теоретик литературы, историк, публицист, журналист. Один из самых авторитетных литературных деятелей первой трети XIX века.'),(2,'Михаил Лермонтов','https://upload.wikimedia.org/wikipedia/commons/thumb/2/24/Mikhail_lermontov.jpg/512px-Mikhail_lermontov.jpg','Лермонтов Михаил Юрьевич - русский поэт, прозаик, драматург, художник. Поручик лейб-гвардии Гусарского полка. Творчество Лермонтова, в котором сочетаются гражданские, философские и личные мотивы, отвечавшие насущным потребностям духовной жизни русского общества, ознаменовало собой новый расцвет русской литературы и оказало большое влияние на виднейших русских писателей и поэтов XIX и XX веков. Произведения Лермонтова получили большой отклик в живописи, театре, кинематографе. Его стихи стали подлинным кладезем для оперного, симфонического и романсового творчества. Многие из них стали народными песнями[10].'),(3,'Николай Гоголь','https://upload.wikimedia.org/wikipedia/commons/b/b2/N.Gogol_by_F.Moller_%281840%2C_Tretyakov_gallery%29.jpg','Никола́й Васи́льевич Го́голь (при рождении Яно́вский, с 1821 года — Го́голь-Яно́вский; 20 марта [1 апреля] 1809, Сорочинцы, Миргородский уезд, Полтавская губерния — 21 февраля [4 марта] 1852, Москва) — русский прозаик, драматург, критик, публицист, признанный одним из классиков русской литературы[7][8]. Происходил из старинного малороссийского дворянского рода Гоголей-Яновских'),(4,'Лев Толстой','https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Tolstoy_Leo_port.jpg/512px-Tolstoy_Leo_port.jpg','Толстой Лев Николаевич - один из наиболее известных русских писателей и мыслителей, один из величайших в мире писателей-романистов.'),(5,'Джоан Роулинг','https://upload.wikimedia.org/wikipedia/commons/thumb/5/5d/J._K._Rowling_2010.jpg/274px-J._K._Rowling_2010.jpg','известная под псевдонимами Дж. К. Роулинг (J. K. Rowling)[6], Джоан Кэ́тлин Роулинг (англ. Joanne Kathleen Rowling) и Ро́берт Гелбре́йт (Robert Galbraith), — британская писательница, сценаристка и кинопродюсер, наиболее известная как автор серии романов о Гарри Поттере. Книги о Гарри Поттере получили несколько наград и были проданы в количестве более 500 миллионов экземпляров[7]. Они стали самой продаваемой серией книг в истории[8] и основой для серии фильмов, ставшей третьей по кассовому сбору серией фильмов в истории[9]. Джоан Роулинг сама утверждала сценарии фильмов[10], а также вошла в состав продюсеров последних двух частей[11].'),(6,'Рэй Брэдбери','https://upload.wikimedia.org/wikipedia/commons/a/aa/Ray_Bradbury_%281975%29.jpg','американский писатель, известный по антиутопии «451 градус по Фаренгейту», циклу рассказов «Марсианские хроники» и частично автобиографической повести «Вино из одуванчиков»[9][10].');
/*!40000 ALTER TABLE `author` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `author_book`
--

DROP TABLE IF EXISTS `author_book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `author_book` (
  `id` int NOT NULL AUTO_INCREMENT,
  `author_id` int NOT NULL,
  `book_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_ab_author_idx` (`author_id`),
  KEY `FK_ab_book_idx` (`book_id`),
  CONSTRAINT `FK_ab_author` FOREIGN KEY (`author_id`) REFERENCES `author` (`id`),
  CONSTRAINT `FK_ab_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `author_book`
--

LOCK TABLES `author_book` WRITE;
/*!40000 ALTER TABLE `author_book` DISABLE KEYS */;
INSERT INTO `author_book` VALUES (1,1,1),(2,1,2),(3,1,3),(4,1,4),(5,1,5),(6,2,6),(7,2,7),(8,4,8),(9,4,9),(10,3,10),(11,3,11),(12,3,12),(13,6,13),(14,5,14);
/*!40000 ALTER TABLE `author_book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `book`
--

DROP TABLE IF EXISTS `book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `book` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL,
  `description` varchar(2048) DEFAULT NULL,
  `photo` varchar(512) DEFAULT NULL,
  `avg_rating` double DEFAULT NULL,
  `date_of_creation` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `book`
--

LOCK TABLES `book` WRITE;
/*!40000 ALTER TABLE `book` DISABLE KEYS */;
INSERT INTO `book` VALUES (1,'Евгений Онегин','роман в стихах русского писателя и поэта Александра Сергеевича Пушкина, начат 9 мая 1823 года и закончен 5 октября 1831 года, одно из самых значительных произведений русской словесности. Повествование ведётся от имени безымянного автора, который, впрочем, в первых же строфах называет Онегина «добрый мой приятель». По известному определению В. Г. Белинского, Пушкин назвал «Евгения Онегина» романом в стихах, поскольку в нём изображена «жизнь во всей её прозаической действительности».','https://upload.wikimedia.org/wikipedia/commons/e/ed/Eugene_Onegin_book_edition.jpg',4.75,'1833 год'),(2,'Капитанская дочка','исторический роман (или повесть) Александра Пушкина, действие которого происходит во время восстания Емельяна Пугачёва. Впервые опубликован без указания имени автора в 4-й книжке журнала «Современник», поступившей в продажу в последней декаде 1836 года','https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Captain%27s_Daughter_1837.jpg/256px-Captain%27s_Daughter_1837.jpg',NULL,'1836 год'),(3,'Руслан и Людмила','первая законченная поэма Александра Сергеевича Пушкина; волшебная сказка, вдохновлённая древнерусскими былинами.','https://upload.wikimedia.org/wikipedia/commons/b/b7/%D0%91%D0%BE%D0%B9_%D0%A7%D0%B5%D1%80%D0%BD%D0%BE%D0%BC%D0%BE%D1%80%D0%B0_%D1%81_%D0%A0%D1%83%D1%81%D0%BB%D0%B0%D0%BD%D0%BE%D0%BC.jpg',NULL,'1820 год'),(4,'Дубровский','наиболее известный разбойничий роман на русском языке, необработанное для печати (и неоконченное) произведение А. С. Пушкина. Повествует о любви Владимира Дубровского к Марии Троекуровой — потомков двух враждующих помещичьих семейств.','https://upload.wikimedia.org/wikipedia/commons/a/a6/Pushkin_Dubrovsky_1919.jpg',NULL,'1841 год'),(5,'Сказка о рыбаке и рыбке','сказка А. С. Пушкина. Написана 2 (14) октября 1833 года. Впервые напечатана в 1835 году в журнале «Библиотека для чтения»[1]. В рукописи есть пометка: «18 песнь сербская». Эта пометка означает, что Пушкин собирался включить её в состав «Песен западных славян». С этим циклом сближает сказку и стихотворный размер.','https://upload.wikimedia.org/wikipedia/commons/3/3b/%D0%A1%D0%BA%D0%B0%D0%B7%D0%BA%D0%B0_%D0%BE_%D1%80%D1%8B%D0%B1%D0%B0%D0%BA%D0%B5_%D0%B8_%D1%80%D1%8B%D0%B1%D0%BA%D0%B5_1913.jpg',NULL,'1835 год'),(6,'Бородино','баллада поэта Михаила Юрьевича Лермонтова. Было написано в 1837 году. Опубликовано в журнале «Современник» в том же 1837 году. Посвящено Бородинскому сражению 7 сентября 1812 года, в котором русская армия сражалась против французского наполеоновского войска.','https://upload.wikimedia.org/wikipedia/commons/3/3d/Lermontov-Borodino-Sovremennik1837.jpg',NULL,'1837 год'),(7,'Герой нашего времени',' первый в русской прозе социально-психологический роман, написанный Михаилом Юрьевичем Лермонтовым в 1838—1840 годах. Классика русской литературы.','https://upload.wikimedia.org/wikipedia/commons/4/41/Geroy_nashego_vremeni.png',NULL,'1840 год'),(8,'Анна Каренина','роман Льва Толстого о трагической любви замужней дамы Анны Карениной и блестящего офицера Алексея Вронского на фоне счастливой семейной жизни дворян Константина Лёвина[К 1] и Кити Щербацкой. Масштабная картина нравов и быта дворянской среды Петербурга и Москвы второй половины XIX века, сочетающая философские размышления авторского alter ego Лёвина с передовыми в русской литературе психологическими зарисовками, а также сценами из жизни крестьян.','https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/AnnaKareninaTitle.jpg/512px-AnnaKareninaTitle.jpg',NULL,'1875—1877 года'),(9,'Война и мир','роман-эпопея Льва Николаевича Толстого, описывающий русское общество в эпоху войн против Наполеона в 1805—1812 годах. Эпилог романа доводит повествование до 1820 года.','https://upload.wikimedia.org/wikipedia/commons/2/2a/T25-011.jpg',NULL,'1865—1869 года'),(10,'Шинель','одна из петербургских повестей Николая Гоголя. Увидела свет в 3-м томе собрания сочинений Гоголя, отпечатанного на исходе 1842 года и поступившего в продажу в последней декаде января 1843 года','https://upload.wikimedia.org/wikipedia/commons/b/bf/Gogol_Palto.jpg',NULL,'1843  год'),(11,'Мёртвые души','произведение Николая Васильевича Гоголя, жанр которого сам автор обозначил как поэма. Писать книгу Гоголь начал в 1835 году как трёхтомник. Первый том был издан в 1842 году. Практически готовый второй том был утерян, но сохранилось несколько глав в черновиках. Третий том не был начат, о нём остались только отдельные сведения, которые были опубликованы вскоре после смерти автора в 1852 году.','https://upload.wikimedia.org/wikipedia/commons/b/bc/Dead_Souls_%28novel%29_Nikolai_Gogol_1842_title_page.jpg',NULL,'1842 год'),(12,'Тарас Бульба','События произведения происходят в среде запорожских казаков в первой половине XVII века[1]. В основу повести Н. В. Гоголя легла история казацкого восстания 1637—1638 годов, подавленного гетманом Николаем Потоцким. Одним из прообразов Тараса Бульбы является предполагаемый предок известного путешественника Н. Н. Миклухо-Маклая, родившийся в Стародубе в начале XVII века куренной атаман Войска Запорожского Охрим Макуха, сподвижник Богдана Хмельницкого, который имел троих сыновей: Назара, Хому (Фому) и Омелька (Емельяна). Назар предал своих товарищей казаков и перешёл на сторону поляков из-за любви к польской паночке, его брат Хома (прототип гоголевского Остапа) погиб, пытаясь доставить Назара отцу, a Емельян стал предком Николая Миклухо-Маклая и его дяди Григория Ильича Миклухи, учившегося вместе с Николаем Гоголем и рассказавшего ему семейное предание. Прообразом является также один из предводителей «колиивщины» Иван Гонта (ум. 1768), которому ошибочно приписывалось убийство двоих сыновей от жены-польки, хотя его жена — русская, а история — вымышленная.','https://upload.wikimedia.org/wikipedia/commons/0/0c/%D0%A2%D0%B0%D1%80%D0%B0%D1%81_%D0%91%D1%83%D0%BB%D1%8C%D0%B1%D0%B0.jpg',NULL,'1835  год'),(13,'451 градус по Фаренгейту','научно-фантастический роман-антиутопия Рэя Брэдбери, изданный в 1953 году. Роман описывает американское общество близкого будущего, в котором книги находятся под запретом; «пожарные»[1], к числу которых принадлежит и главный герой Гай Монтэг, сжигают любые найденные книги. В ходе романа Монтэг разочаровывается в идеалах общества, частью которого он является, становится изгоем и присоединяется к небольшой подпольной группе маргиналов, сторонники которой заучивают тексты книг, чтобы спасти их для потомков. Название книги объясняется в эпиграфе: «451 градус по Фаренгейту — температура, при которой воспламеняется и горит бумага»[2]. В книге содержится немало цитат из произведений англоязычных авторов прошлого (таких, как Уильям Шекспир, Джонатан Свифт и другие), а также несколько цитат из Библии.','https://upload.wikimedia.org/wikipedia/ru/d/d3/451_%D0%B3%D1%80%D0%B0%D0%B4%D1%83%D1%81_%D0%BF%D0%BE_%D0%A4%D0%B0%D1%80%D0%B5%D0%BD%D0%B3%D0%B5%D0%B9%D1%82%D1%83.jpg',NULL,'1953 год'),(14,'Гарри Поттер и философский камень','первый роман в серии книг про юного волшебника Гарри Поттера, написанный Дж. К. Роулинг. В нём рассказывается, как Гарри узнает, что он волшебник, встречает близких друзей и немало врагов в Школе Чародейства и Волшебства «Хогвартс», а также с помощью своих друзей пресекает попытку возвращения злого волшебника Волан-де-Морта, который убил родителей Гарри (самому Гарри в тот момент был год от роду).','https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg',NULL,'26 июня 1997 года');
/*!40000 ALTER TABLE `book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `featured_book`
--

DROP TABLE IF EXISTS `featured_book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `featured_book` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `book_id` int NOT NULL,
  `date_of_add` datetime NOT NULL,
  `mark_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_fb_mark_idx` (`mark_id`),
  KEY `FK_fb_user_idx` (`user_id`),
  KEY `FK_fb_book_idx` (`book_id`),
  CONSTRAINT `FK_fb_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`),
  CONSTRAINT `FK_fb_mark` FOREIGN KEY (`mark_id`) REFERENCES `mark` (`id`),
  CONSTRAINT `FK_fb_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `featured_book`
--

LOCK TABLES `featured_book` WRITE;
/*!40000 ALTER TABLE `featured_book` DISABLE KEYS */;
INSERT INTO `featured_book` VALUES (1,1,1,'2023-05-01 10:10:10',3),(2,1,13,'2023-05-02 10:10:10',2),(3,1,14,'2023-05-03 10:10:10',1),(4,2,1,'2023-05-02 10:10:10',3),(5,2,14,'2023-05-03 10:10:10',2);
/*!40000 ALTER TABLE `featured_book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre`
--

LOCK TABLES `genre` WRITE;
/*!40000 ALTER TABLE `genre` DISABLE KEYS */;
INSERT INTO `genre` VALUES (1,'Детектив'),(2,'Повесть'),(3,'Роман'),(4,'Сказка'),(5,'Фэнтези'),(6,'Исторический'),(7,'Поэма'),(8,'Баллада'),(9,'Эпопея'),(10,'Драма'),(11,'Научная фантастика'),(12,'Антиутопия');
/*!40000 ALTER TABLE `genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre_book`
--

DROP TABLE IF EXISTS `genre_book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre_book` (
  `id` int NOT NULL AUTO_INCREMENT,
  `genre_id` int NOT NULL,
  `book_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_gb_book_idx` (`book_id`),
  KEY `FK_gb_genre_idx` (`genre_id`),
  CONSTRAINT `FK_gb_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`),
  CONSTRAINT `FK_gb_genre` FOREIGN KEY (`genre_id`) REFERENCES `genre` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre_book`
--

LOCK TABLES `genre_book` WRITE;
/*!40000 ALTER TABLE `genre_book` DISABLE KEYS */;
INSERT INTO `genre_book` VALUES (1,3,1),(2,3,2),(3,6,2),(4,7,3),(5,3,4),(6,4,5),(7,8,6),(8,3,7),(9,3,8),(10,3,9),(11,9,9),(12,2,10),(13,10,10),(14,3,11),(15,7,11),(16,2,12),(17,11,13),(18,12,13),(19,3,13),(20,3,14),(21,5,14);
/*!40000 ALTER TABLE `genre_book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `like`
--

DROP TABLE IF EXISTS `like`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `like` (
  `id` int NOT NULL AUTO_INCREMENT,
  `review_id` int NOT NULL,
  `user_id` int NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_like_review_idx` (`review_id`),
  KEY `FK_like_user_idx` (`user_id`),
  CONSTRAINT `FK_like_review` FOREIGN KEY (`review_id`) REFERENCES `review` (`id`),
  CONSTRAINT `FK_like_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `like`
--

LOCK TABLES `like` WRITE;
/*!40000 ALTER TABLE `like` DISABLE KEYS */;
INSERT INTO `like` VALUES (1,1,1,'2023-05-03 18:38:05'),(2,2,1,'2023-05-03 15:04:33'),(3,1,2,'2023-05-04 01:31:44'),(4,2,2,'2023-05-04 01:33:24'),(5,1,3,'2023-05-04 01:42:40');
/*!40000 ALTER TABLE `like` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mark`
--

DROP TABLE IF EXISTS `mark`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mark` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mark`
--

LOCK TABLES `mark` WRITE;
/*!40000 ALTER TABLE `mark` DISABLE KEYS */;
INSERT INTO `mark` VALUES (1,'Прочитать позже'),(2,'Читаю'),(3,'Прочитано'),(4,'Заброшено');
/*!40000 ALTER TABLE `mark` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `note`
--

DROP TABLE IF EXISTS `note`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `note` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `date_of_creation` datetime NOT NULL,
  `name` varchar(256) DEFAULT NULL,
  `content` varchar(4096) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_note_user_idx` (`user_id`),
  CONSTRAINT `FK_note_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `note`
--

LOCK TABLES `note` WRITE;
/*!40000 ALTER TABLE `note` DISABLE KEYS */;
/*!40000 ALTER TABLE `note` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `review`
--

DROP TABLE IF EXISTS `review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `review` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `book_id` int NOT NULL,
  `date_of_creation` datetime NOT NULL,
  `rating` int NOT NULL,
  `content` varchar(4096) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_review_user_idx` (`user_id`),
  KEY `FK_review_book_idx` (`book_id`),
  CONSTRAINT `FK_review_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`),
  CONSTRAINT `FK_review_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `review`
--

LOCK TABLES `review` WRITE;
/*!40000 ALTER TABLE `review` DISABLE KEYS */;
INSERT INTO `review` VALUES (1,3,1,'2023-03-11 22:11:00',5,'Онегин, добрый мой приятель... Писать рецензию на Евгения Онегина, после сотен тысяч страниц пушкиноведов, то еще занятие, но - сама, как говориться, напросилась. '),(2,4,1,'2023-02-08 12:22:00',4,'Эта классика обошла меня стороной в школе, а на сочинении, скорее всего же оно было, не могло не быть, спасло то, что в музыкальной школе мы проходили оперу, поэтому сам сюжет я знала) Ну да это не важно, важно то, что дошла я до чтения только сейчас, когда есть жизненный опыт и были влюблённости, безответные и не очень))) И это облегчает чтение, хоть оно и в стихах, которые всё же мне читать сложно, хотя, по идее, должно быть наоборот и читаться ещё быстрее. Но даже в стихотворной форме меня напрягали излишние подробности и уточнения, кто из героев кого читал, когда и сколько) Да, понятно, что это тоже их характеризует, но всё же).  '),(3,5,1,'2023-04-30 21:43:00',5,'Прошла любовь - завяли помидоры (а она не прошла, но всё равно завяли). Впервые (если не забыла) читаю роман в стихотворном формате. В начале нужно было привыкнуть, да и потом, приходилось иногда перечитывать строфы, чтобы понять общий смысл. Так же, периодически заглядывала в толковый словарь для перевода старорусских слов. Книга (обложка другая) уже была у меня предустановлена, так ещё, там есть комментарии в сносках, что очень удобно - понять весь смысл. Повествование идёт от безымянного друга главного героя Евгения Онегина. Не от лица самого Евгения.•ЕВГЕНИЙ ОНЕГИН. Родился на брегах Невы. Знает в совершенстве французский, немного по латыни. Вроде хороший человек, но расстраивал всю книгу. •ВЛАДИМИР ЛЕНСКИЙ. Его друг. Красавец, чёрные кудри до плеч. Из Германии. Поэт. Поклонник Канта. Самый добрый персонаж книги, жаль его. Нравился, как человек. •ТАТЬЯНА ЛАРИНА. Тёмноволосая, бледная. Тихая, дикая, молчаливая, боязливая, задумчивая. Любит страшные рассказы. Любит читать. Наивная. Чем-то напоминает меня саму, нравилась абсолютно, молодец, в конце вызвала восхищение, на такую можно ровняться. Есть сестра Ольга. •ОЛЬГА ЛАРИНА. Младшая сестра Татьяны. Скромная, послушная, простодушная. Голубые глаза. Вертихвостка, ветреная. Раздражала меня как личность последними качествами. Отец: бригадир ДМИТРИЙ Ларин. Мать: •ПРАСКОВЬЯ. Бьёт своих слуг. Татьяна любила сидеть у окна, наблюдая восходы и закаты, ну не прекрасно ли это? Это значит, она ценит мгновенья красоты природы и жизни. И ещё замечу, что она верна принципам и людям. Многие строфы понравились мне. Да и вообще, язык написания очень красив и своеобразен. Стиль пересказа, описания с философским подтекстом нравились своей меланхолией. Люблю грустные цитаты. Книга мне понравилась. Читается сверхбыстро. Вызывала тихие эмоции: грусти, влюблённости, ностальгии и жалости. Мне кажется, она может нас научить ценить то, что у нас \"под носом\" и моменты жизни, как Татьяна. Советую, хорошая классика на все времена.'),(4,7,1,'2023-03-17 09:51:00',5,'Подвиг русского поэта, роман в стихах: о жизни, о России, о любви и о себе. Евгений Онегин – юноша 18 лет из Санкт-Петербурга, “забав и роскоши дитя”, “свободный, в цвете лучших лет”, которому уже “наскучил света шум”. Получив наследство от покойного дяди, он решил переселиться в деревню в свои новые владения и был рад, что “свой прежний путь переменил на что-нибудь”. Однако очень скоро Евгений увидел, что “и в деревне скука та же”. Здесь Онегин познакомился с пылким молодым поэтом Владимиром Ленским. И хотя они достаточно сблизились, но были “волна и камень, стихи и проза, лед и пламень”. Онегин противопоставлен Ленскому своим образом мыслей: он пресыщен радостями жизни и страстями, ничто его уже не будоражит, Владимир же был неопытный мечтатель, его юный ум еще пленяли “мира блеск и шум”.'),(5,8,1,'2023-03-23 23:40:00',5,'На данный роман написано много разборов, много рецензий. И о его значении в мире литературы, и о проблемах, раскрытых тут, и о методах написания, и о персонажах… Я же опишу свой личный опыт взаимодействия с данным романом. Главное, что я хочу тут сказать - «Евгений Онегин» - это разговор, личный разговор, с А.С. Пушкиным об обществе, о времени, об архетипах, и о чувствах. Читая «Евгения Онегина», будто бы читаешь описание реальности того времени, ну а проблемы этого времени и его архетипы - актуальных и сейчас, и, по всей видимости, во все времена. Татьяна - любимая героиня автора, и, лично для меня, самый настоящий человек с большой буквы. Хоть я и не разделяю ее привязанностей и вкусов (это дело субъективное), все же я восхищаюсь ее моральными ценностями, решительностью, смелостью, ее…'),(6,10,1,'2022-12-01 18:28:00',5,'Очередное знакомство с Онегиным Очень приятным оказалось очередное знакомство с Пушкиным и главной звездой - Огегиным. Многое со времен школы забыто было, на что-то и внимания тогда не обратил, а сегодня я грущу. А всё потому, что в те времена, что сегодня люди делают одинаковые глупости. Не ценим любовь и дружбу, бываем слишком заносчивыми или невыносимыми даже для самих себя, а ещё неверно расставляем приоритеты в нашей жизни. Мелочность и суета героев, их достоинства и недостатки поразили меня вновь. Я уже забыл, что Татьяна по-русски плохо изъяснялась, а Онегин был мажором. Да и другие мелочи или авторские ремарки добавляли к повествованию изысканности, как хороший соус может оживить полумертвое блюдо.'),(7,11,1,'2022-12-22 16:35:00',4,'Зная, как Александр Сергеевич относился к своим друзьям, так и хочется предположить, что Евгений Онегин и правда его приятель. Тем более, имя Евгений даётся им и главному герою поэмы Медный всадник . Значит, приятеля, возможно, тоже звали Евгений. Оказалось, что 2 года назад до этой мысли дошел Валерий Бурт в своей статье Евгений Баратынский: друг Пушкина и \"глубоко растерзанное сердце\". Как уже ясно из названия статьи, подходящий друг Пушкина с именем Евгений найден. Это Евгений Баратынский .'),(8,12,1,'2022-12-30 14:09:00',5,'Нет, такие книги нужно читать и перечитывать, растаскивать на цитаты, запоминать наиболее важные строчки. Помнится, в школе девочки учили письмо Татьяне Онегину, так я до сих пор его помню, хоть только часть, но все же. Пушкин работал над романом 8 лет и назвал его своим подвигом. Главный герой - Евгений Онегин, молодой дворянин, которому пришлось переехать из Петербурга в деревню, чтобы проститься с дядей. Он любитель балов, женщин, долго поспать и покутить и, конечно же, в деревне ему стало скучно, да еще и соседка, Татьяна Ларина, влюбилась в него. Он не готов быть возлюбленным, мужем, отцом и отказывает ей. Жизнь потом повернулась к нему той же точкой, что и он в свое время к ней. Теперь он оказался ненужным. Мне хотелось бы финал поточнее, люблю, когда все понятно, а тут начинаешь задумываться: \"а что же было дальше с Онегиным\". Но тут автор оставляет нам право самим решить, что героя ждет дальше. Классика....');
/*!40000 ALTER TABLE `review` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_user`
--

DROP TABLE IF EXISTS `role_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role_user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_user`
--

LOCK TABLES `role_user` WRITE;
/*!40000 ALTER TABLE `role_user` DISABLE KEYS */;
INSERT INTO `role_user` VALUES (1,'reader'),(2,'admin');
/*!40000 ALTER TABLE `role_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(256) NOT NULL,
  `password` varchar(256) NOT NULL,
  `name` varchar(256) NOT NULL,
  `date_of_registration` datetime NOT NULL,
  `role_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_user_role_idx` (`role_id`),
  CONSTRAINT `FK_user_role` FOREIGN KEY (`role_id`) REFERENCES `role_user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'neverket@gmail.com','123456','Arslan Aminov','2023-01-01 00:00:00',2),(2,'neverket2@gmail.com','qwerty','Aminovv','2023-01-02 00:00:00',1),(3,'0@gmail.com','12345678','Jane Martinez','2023-01-03 00:00:00',1),(4,'1@gmail.com','123467890','Eugene Gross','2023-01-03 00:00:00',1),(5,'2@gmail.com','123123123','Leon Hernandez','2023-01-03 00:00:00',1),(6,'3@gmail.com','12341234','Linda Delgado','2023-01-03 00:00:00',1),(7,'4@gmail.com','qwerty123','David Myers','2023-01-03 00:00:00',1),(8,'5@gmail.com','qwerty123456','Virgil Lopez','2023-01-04 00:00:00',1),(9,'6@gmail.com','qweqwe123','Mark Diaz','2023-01-04 00:00:00',1),(10,'7@gmail.com','qwerty123','Brian Hernandez','2023-01-04 00:00:00',1),(11,'8@gmail.com','123456','Charles Adams','2023-01-05 00:00:00',1),(12,'9@gmail.com','qwertyasdfgh','Lois Johnson','2023-01-05 00:00:00',1),(13,'10@gmail.com','zxc123456','James Baker','2023-01-05 00:00:00',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-18 23:25:52
