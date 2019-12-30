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
-- Table structure for table `imageservice`
--

DROP TABLE IF EXISTS `imageservice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `imageservice` (
  `ImageServiceID` int(11) NOT NULL AUTO_INCREMENT,
  `Image` text,
  `ServiceId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ImageServiceID`),
  KEY `IX_ImageService_ServiceId` (`ServiceId`),
  CONSTRAINT `FK_ImageService_Service_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ServiceId`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imageservice`
--

LOCK TABLES `imageservice` WRITE;
/*!40000 ALTER TABLE `imageservice` DISABLE KEYS */;
INSERT INTO `imageservice` VALUES (1,'Images/Gigs/Admin/ddcf19f2-c751-4f7d-8e4f-a1a26e3843d8.jfif',1),(2,'Images/Gigs/Admin/cc3819d7-d408-44ff-b995-36974deb89f2.webp',1),(3,'Images/Gigs/Admin/c432594a-dff3-414b-9458-5ae7b1c46f7c.png',1),(4,'Images/Gigs/Admin/02689c04-ab59-4dd9-98fc-d4e7c9c10cfb.webp',2),(5,'Images/Gigs/Admin/47c29115-a886-4321-a53f-61d7403ee47a.jpg',2),(6,'Images/Gigs/Admin/6d4c11b4-8cdb-40c9-a5b0-0524f368bfa3.webp',2),(7,'Images/Gigs/user1/dae9c712-3151-496d-b7ae-a30a729a16ac.webp',4),(8,'Images/Gigs/user1/eaae6e53-1008-47ea-83f8-0b12c14e6199.webp',4);
/*!40000 ALTER TABLE `imageservice` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-30 14:02:59
