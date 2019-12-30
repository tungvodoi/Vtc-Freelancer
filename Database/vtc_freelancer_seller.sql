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
-- Table structure for table `seller`
--

DROP TABLE IF EXISTS `seller`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `seller` (
  `SellerId` int(11) NOT NULL AUTO_INCREMENT,
  `SellerPoint` int(11) NOT NULL,
  `Description` text,
  `RegisterDateSeller` datetime NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`SellerId`),
  KEY `IX_Seller_UserId` (`UserId`),
  CONSTRAINT `FK_Seller_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seller`
--

LOCK TABLES `seller` WRITE;
/*!40000 ALTER TABLE `seller` DISABLE KEYS */;
INSERT INTO `seller` VALUES (1,0,'My name is Linh. I\'m Web Developer. I\'m working with visual studio code very good, master C#, ASP .NET core, ReactJS, EF core, PHP.... I can eat everything, i like swimming and play soccer. i hate girlfriend when her say kimochi','2019-12-30 13:47:26',3),(2,0,'My name is Linh. I\'m Web Developer. I\'m working with visual studio code very good, master C#, ASP .NET core, ReactJS, EF core, PHP.... I can eat everything, i like swimming and play soccer. i hate girlfriend when her say kimochi','2019-12-30 13:47:50',3),(3,0,'My name is Linh. I\'m Web Developer. I\'m working with visual studio code very good, master C#, ASP .NET core, ReactJS, EF core, PHP.... I can eat everything, i like swimming and play soccer. i hate girlfriend when her say kimochi','2019-12-30 13:48:07',3),(4,0,'My name is Linh. I\'m Web Developer. I\'m working with visual studio code very good, master C#, ASP .NET core, ReactJS, EF core, PHP.... I can eat everything, i like swimming and play soccer. i hate girlfriend when her say kimochi','2019-12-30 13:55:49',1);
/*!40000 ALTER TABLE `seller` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-30 14:03:02
