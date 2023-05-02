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
  `id` int NOT NULL,
  `name` varchar(256) NOT NULL,
  `photo` varchar(512) DEFAULT NULL,
  `biography` varchar(4096) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `author`
--

LOCK TABLES `author` WRITE;
/*!40000 ALTER TABLE `author` DISABLE KEYS */;
INSERT INTO `author` VALUES (1,'Александр Пушкин',NULL,'Александр Пушкин – великий русский поэт, прозаик, драматург, один из самых авторитетных литературных деятелей первой трети XIX века.'),(2,'Лев Толстой',NULL,'Лев Толстой – глава русской литературы, просветитель, публицист, религиозный мыслитель, член-корреспондент Императорской Академии наук, почетный академик по разряду изящной словесности, обладатель Императорского ордена Святой Анны, медалей «За защиту Севастополя», «В память войны 1853–1856», «В память 50-летия защиты Севастополя».'),(3,'Джоан Роулинг',NULL,'Джоан Роулинг — английская писательница, пишущая под псевдонимом Джоан Кэтлин Роулинг (Joanne Katheline Rowling), автор серии (1997—2007) романов о Гарри Поттере.'),(4,'Рэй Брэдбери',NULL,'РЭЙМОНД ДУГЛАС «РЭЙ» БРЭДБЕРИ — американский писатель-фантаст. Критики относят некоторые его произведения к магическому реализму.');
/*!40000 ALTER TABLE `author` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `author_book`
--

DROP TABLE IF EXISTS `author_book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `author_book` (
  `id` int NOT NULL,
  `author_id` int NOT NULL,
  `book_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_ab_author_idx` (`author_id`),
  KEY `FK_ab_book_idx` (`book_id`),
  CONSTRAINT `FK_ab_author` FOREIGN KEY (`author_id`) REFERENCES `author` (`id`),
  CONSTRAINT `FK_ab_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `author_book`
--

LOCK TABLES `author_book` WRITE;
/*!40000 ALTER TABLE `author_book` DISABLE KEYS */;
INSERT INTO `author_book` VALUES (1,1,1),(2,1,2),(3,1,3);
/*!40000 ALTER TABLE `author_book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `book`
--

DROP TABLE IF EXISTS `book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `book` (
  `id` int NOT NULL,
  `name` varchar(256) NOT NULL,
  `description` varchar(2048) DEFAULT NULL,
  `photo` varchar(512) DEFAULT NULL,
  `avg_rating` double DEFAULT NULL,
  `date_of_creation` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `book`
--

LOCK TABLES `book` WRITE;
/*!40000 ALTER TABLE `book` DISABLE KEYS */;
INSERT INTO `book` VALUES (1,'Евгений Онегин','Роман в стихах «Евгений Онегин» великого русского классика Александра Пушкина – уникален, это настоящая «энциклопедия русской жизни».История о не сбывшейся любви, о вечных ценностях, о том, что в быстром ритме жизни, за соблазнами и быстрыми удовольствиями люди рискуют потерять не только себя, но и то единственно ценное, что есть в нашем мире – настоящую искреннюю любовь. Любовь, которая дается как дар, и не имеет отношения ни к статусу, ни к деньгам, ни даже к личным качествам объекта обожания. И нет заслуги человека в том, что судьба преподносит такой подарок, важно только сможет ли он выйти за рамки своих устоявшихся представлений о том, что правильно, а что нет, и принять это сокровище со всей искренностью..',NULL,4,'1833 год'),(2,'Сказка о рыбаке и рыбке','Самая знаменитая из пушкинских сказок, повествующая о неизбежном наказании за жадность, давно стала фольклором и даже породила поговорку «остаться у разбитого корыта».',NULL,4.9,'1833 год'),(3,'Капитанская дочка','В романе «Капитанская дочка» А.С.Пушкин нарисовал яркую картину стихийного крестьянского восстания под предводительством Емельяна Пугачева.',NULL,4.8,'1836 год');
/*!40000 ALTER TABLE `book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `featured_book`
--

DROP TABLE IF EXISTS `featured_book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `featured_book` (
  `id` int NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `featured_book`
--

LOCK TABLES `featured_book` WRITE;
/*!40000 ALTER TABLE `featured_book` DISABLE KEYS */;
INSERT INTO `featured_book` VALUES (1,1,2,'2023-01-01 00:00:00',3),(2,1,1,'2023-02-01 00:00:00',2),(3,1,3,'2023-03-01 00:00:00',3),(4,2,1,'2023-01-02 00:00:00',4),(5,2,3,'2023-02-02 00:00:00',2);
/*!40000 ALTER TABLE `featured_book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre` (
  `id` int NOT NULL,
  `name` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre`
--

LOCK TABLES `genre` WRITE;
/*!40000 ALTER TABLE `genre` DISABLE KEYS */;
INSERT INTO `genre` VALUES (1,'Боевик'),(2,'Детектив'),(3,'Классика'),(4,'Повесть'),(5,'Роман'),(6,'Сказка'),(7,'Фантастика'),(8,'Фэнтези'),(9,'Приключения'),(10,'Ужасы'),(11,'Психология'),(12,'Наука'),(13,'Юмор'),(14,'Религия');
/*!40000 ALTER TABLE `genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre_book`
--

DROP TABLE IF EXISTS `genre_book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre_book` (
  `id` int NOT NULL,
  `genre_id` int NOT NULL,
  `book_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_gb_book_idx` (`book_id`),
  KEY `FK_gb_genre_idx` (`genre_id`),
  CONSTRAINT `FK_gb_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`),
  CONSTRAINT `FK_gb_genre` FOREIGN KEY (`genre_id`) REFERENCES `genre` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre_book`
--

LOCK TABLES `genre_book` WRITE;
/*!40000 ALTER TABLE `genre_book` DISABLE KEYS */;
INSERT INTO `genre_book` VALUES (1,5,1),(2,6,2),(3,5,3);
/*!40000 ALTER TABLE `genre_book` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mark`
--

DROP TABLE IF EXISTS `mark`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mark` (
  `id` int NOT NULL,
  `name` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
  `id` int NOT NULL,
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
INSERT INTO `note` VALUES (2,2,'2023-01-02 00:05:00','название','че-то не дочитал Евгения Онегина'),(3,2,'2023-02-02 00:11:00','123123','читаю Капитанскую дочку от ПУшкина, интересно'),(4,1,'2023-04-08 21:16:20','fff','dsdsdsdfsdf'),(5,1,'2023-04-08 21:26:54','Без названия',NULL);
/*!40000 ALTER TABLE `note` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `review`
--

DROP TABLE IF EXISTS `review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `review` (
  `id` int NOT NULL,
  `user_id` int NOT NULL,
  `book_id` int NOT NULL,
  `date_of_creation` datetime NOT NULL,
  `rating` int NOT NULL,
  `content` varchar(2048) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_review_user_idx` (`user_id`),
  KEY `FK_review_book_idx` (`book_id`),
  CONSTRAINT `FK_review_book` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`),
  CONSTRAINT `FK_review_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `review`
--

LOCK TABLES `review` WRITE;
/*!40000 ALTER TABLE `review` DISABLE KEYS */;
INSERT INTO `review` VALUES (1,1,1,'2023-01-01 01:00:00',4,'хорошо'),(2,1,1,'2023-02-01 01:00:00',5,'круто'),(3,2,1,'2023-01-02 02:00:00',3,'ну такое');
/*!40000 ALTER TABLE `review` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_user`
--

DROP TABLE IF EXISTS `role_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role_user` (
  `id` int NOT NULL,
  `name` varchar(128) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
  `id` int NOT NULL,
  `login` varchar(256) NOT NULL,
  `password` varchar(256) NOT NULL,
  `name` varchar(256) NOT NULL,
  `date_of_registration` datetime NOT NULL,
  `role_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_user_role_idx` (`role_id`),
  CONSTRAINT `FK_user_role` FOREIGN KEY (`role_id`) REFERENCES `role_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'neverket@gmail.com','123','Arslann','2023-01-01 00:00:00',2),(2,'aminov@lib.lol','12345678','Aminov','2023-01-01 00:00:00',1),(3,'neverket2@gmail.com','1234','Арслан','2023-04-08 18:11:43',1);
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

-- Dump completed on 2023-04-09 15:06:50
