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
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `service` (
  `ServiceId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) DEFAULT NULL,
  `Category` varchar(255) DEFAULT NULL,
  `SubCategory` varchar(255) DEFAULT NULL,
  `Description` text,
  `TimeCreateService` datetime NOT NULL,
  `Status` int(11) NOT NULL,
  `SellerId` int(11) NOT NULL,
  PRIMARY KEY (`ServiceId`),
  KEY `IX_Service_SellerId` (`SellerId`),
  CONSTRAINT `FK_Service_Seller_SellerId` FOREIGN KEY (`SellerId`) REFERENCES `seller` (`SellerId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service`
--

LOCK TABLES `service` WRITE;
/*!40000 ALTER TABLE `service` DISABLE KEYS */;
INSERT INTO `service` VALUES (1,'I will develop professional website and tools','Programing & Tech','WordPress','ULTIMATE GIG TO PROVIDE SOLUTIONS FOR ALL WEB DEVELOPMENT\r\n\r\n\"CUSTOMIZATION OF WEBSITE\" \r\nFollowing are the technologies in which we have expertise and experience.\r\n                                                 \r\nHTML5/CSS3\r\nJAVASCRIPT\r\nPHP CODING\r\nMYSQL DATABASE\r\nBOOTSTRAP DESIGNING\r\nJQUERY\r\nCODEIGNITOR\r\n\r\n\"WEB TOOLS AND MANAGEMENT SYSTEMS\"\r\nFollowing are the demos available , for their videos please inbox us.\r\n\r\n1) SCHOOL MANAGEMENT SYSTEM\r\n2) CRM TOOL http\r\n3) POS\r\n4) FOR MORE TOOLS AND SYSTEM PLEASE WATCH VIDEO IN GALLERY\r\n\r\nSERVICE FEATURES\r\n\r\nHITECH SOLUTIONS has following service features, which has not only satisfied clients, but also make them a regular customer and their feed back has never less then 5 STAR REVIEW\r\n\r\nCUSTOMER SATISFACTION 100% ✔\r\nTIMELY DELIVERY ✔\r\nRESPONSIVE ✔\r\nSUPPORT AFTER SALES ✔\r\nAFFORDABLE PRICE ✔\r\nSPECIAL DISCOUNT FOR REGULAR CUSTOMERS ✔','2019-12-30 13:49:55',1,4),(2,'I will create a smart, notable logo for your political campaign','Programing & Tech','WordPress','Who better to hire to work on your campaign assets than a designer living in the heart of that culture: Washington, DC. Every campaign needs a strong brand behind it. You are selling your thoughts, ideas, and values as a person and your logo needs to communicate that. This package offers versatile options to work for anyone\'s campaign and budget. The foundation of the package includes a brilliant, standout, modern logo for you in all of the different formats you would need to apply it to different print and digital elements. You can add on extras such as renderings, stationary, and a social media kit (basic elements and guidelines on how to structure your social media posts and formatted icons for your various social media platforms, up to 3). \r\n\r\nPlatforms I will not help promote:\r\nAnything discriminating or dictating what other people may do with their own freedoms and first-amendment rights. \r\nPro-guns (Anti-gun laws)\r\nAny slander or mud-slinging against other candidates or groups of people.\r\n\r\nCheck out my other political offerings to help complete your campaign needs and make your run for office a beautiful success.','2019-12-30 13:52:37',1,4),(3,'I will design a clean and inspirational logo for your campaign','Programing & Tech','WordPress',NULL,'2019-12-30 13:54:21',6,1),(4,'I will design a clean and inspirational logo for your campaign','Programing & Tech','WordPress','If you need a logo to capture your professionalism and values, you came to the right gig! This gig is perfect for politicians who need a logo for their campaign. \r\n\r\nMy name is Danny Wethern, I\'m the founder and creative Director of Untapped Creative, and I’ve had the opportunity to work with over 80 clients both big and small. With over 7 years of experience, I’ve acquired a wealth of knowledge in the print and digital design space, and I get excited everyday at new opportunities to solve creative problems. I always work with the consumer in mind, and my style can be described as clean and eye catching.\r\n\r\nAll my designs are self-made and uniquely built for you from scratch. I don’t use or design from any templates.\r\n\r\nYou\'ll receive full commercial use and source files for your design, no matter what package you choose. \r\n\r\n\r\nNote:\r\n\r\nIf you need something a little different than any of the packages I have set up, reach out and we can discuss a solution tailored to what you need. I recommend sending me a message before you order, just to make sure you select the right option for what you need.','2019-12-30 13:56:39',1,4);
/*!40000 ALTER TABLE `service` ENABLE KEYS */;
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
