-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: vtc_freelancer
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `category` (
  `CategoryId` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(255) DEFAULT NULL,
  `ParenId` int(11) NOT NULL,
  PRIMARY KEY (`CategoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Programing & Tech',0),(2,'WordPress',1),(3,'Game Development',1),(4,'Web Programming',1),(5,'Mobile Apps',1),(6,'Databases',1),(7,'Data Analysis & Reports',1),(8,'IoT',1),(9,'Graphics & Design',0),(10,'Logo Design',9),(11,'Web & Mobile Design',9),(12,'Game Design',9),(13,'Graphics for Streamers',9),(14,'Twitch Store',9),(15,'Banner Ads',9),(16,'Poster Design',9),(17,'Music & Audio',0),(18,'Voice Over',17),(19,'Mixing & Mastering',17),(20,'Producers & Composers',17),(21,'Sound Effects',17),(22,'Vocal Tuning',17),(23,'Podcast Editing',17),(24,'Audiobook Production',17),(25,'Video & Animation',0),(26,'Whiteboard & Animated Explainers',25),(27,'Video Editing',25),(28,'Short Video Ads',25),(29,'Logo Animation',25),(30,'Visual Effects',25),(31,'3D Product Animation',25),(32,'Digital Marketing',0),(33,'Social Media Marketing',32),(34,'SEO',32),(35,'Public Relations',32),(36,'Content Marketing',32),(37,'Video Marketing',32),(38,'Web Analytics',32),(39,'Mobile Marketing & Advertising',32),(40,'Business',0),(41,'Virtual Assistant',40),(42,'Data Entry',40),(43,'Market Research',40),(44,'Product Research',40),(45,'Business Plans',40),(46,'Branding Services',40),(47,'Legal Consulting',40),(48,'Financial Consulting',40),(49,'Writing & Translation',0),(50,'Articles & Blog Posts',49),(51,'Proofreading & Editing',49),(52,'Technical Writing',49),(53,'Translation',49),(54,'White Papers',49),(55,'UX Writing',49),(56,'Podcast Writing',49),(57,'Lifestyle',0),(58,'Online Lessons',57),(59,'Arts & Crafts',57),(60,'Relationship Advice',57),(61,'Health, Nutrition & Fitness',57),(62,'Gaming',57),(63,'Greeting Cards & Videos',57),(64,'Celebrity Impersonators',57),(65,'Your Message On...',57);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-30 14:03:00
