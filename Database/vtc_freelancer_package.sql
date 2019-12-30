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
-- Table structure for table `package`
--

DROP TABLE IF EXISTS `package`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `package` (
  `PackageId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Title` text,
  `Description` text,
  `Price` double NOT NULL,
  `NumberRevision` int(11) NOT NULL,
  `DeliveryTime` int(11) NOT NULL,
  `ServiceId` int(11) NOT NULL,
  PRIMARY KEY (`PackageId`),
  KEY `IX_Package_ServiceId` (`ServiceId`),
  CONSTRAINT `FK_Package_Service_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ServiceId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `package`
--

LOCK TABLES `package` WRITE;
/*!40000 ALTER TABLE `package` DISABLE KEYS */;
INSERT INTO `package` VALUES (1,'Basic','WEB FIXER\r\n','WRITE OR FIX HTML CSS JAVASCRIPT PHP CODE, ONE OR MORE PAGE. WEB PLUGINS FIXING.',5,0,1,1),(2,'Standard','WEB TOOLS','DEVELOPMENT AND DESIGNING TOOLS LIKE POS CRM AND MANAGEMENT SYSTEMS',35,1,2,1),(3,'Premium','WEB DEVELOPMENT\r\n','WEB DESIGN AND DEVELOPMENT IN ANY TECHNOLOGY',105,2,3,1),(4,'Basic','The Freshman\r\n','Great option for the first-time candidate; focuses on the basic elements to get you started.',35,3,2,2),(5,'Standard','The Junior\r\n','Ideal option for the young candidate; includes all of the fundamental elements to get on the trail.',100,3,4,2),(6,'Premium','The Senior\r\n','All of the needed materials for an experienced politician\'s campaign.',335,5,6,2),(7,'Basic','Logo Design\r\n','I will design a logo for your political campaign',65,1,1,4),(8,'Standard','Logo + Style Guide\r\n','I will design a logo for your political campaign, + a style guide so you can stay consistent',85,3,3,4),(9,'Premium','Premium Logo Package\r\n','I will design a main logo, + secondary logo layouts, & a style guide',100,5,7,4);
/*!40000 ALTER TABLE `package` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-30 14:03:01
