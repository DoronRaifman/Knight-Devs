CREATE DATABASE  IF NOT EXISTS `purchase_list` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `purchase_list`;
-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: localhost    Database: purchase_list
-- ------------------------------------------------------
-- Server version	8.0.18

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
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `items` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `item_name` varchar(45) NOT NULL,
  `papa_id` int(11) NOT NULL DEFAULT '0',
  `order_id` int(11) NOT NULL DEFAULT '5',
  PRIMARY KEY (`id`),
  KEY `Item_name` (`item_name`) /*!80000 INVISIBLE */,
  KEY `papa_id` (`papa_id`)
) ENGINE=InnoDB AUTO_INCREMENT=193 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (157,'חצי חינם',0,5),(158,'בכניסה',157,5),(159,'נייר טואלט',158,5),(160,'אקונומיקה',158,5),(161,'אבקת כביסה',158,5),(162,'שתיה',157,5),(163,'7up',162,5),(164,'מים',162,3),(165,'מקרר',157,3),(166,'חלב',165,5),(167,'גבינה צהובה',165,5),(168,'קוטג',165,5),(169,'בשר',157,5),(170,'כרעיים',169,7),(171,'כנפיים',169,7),(172,'שניצלים',169,5),(173,'טירה בשר',0,5),(174,'עמדה ראשית',173,5),(175,'אנטריקוט',174,5),(176,'צלי כתף',174,5),(177,'מקרר',173,3),(178,'שרימפס',177,5),(179,'בשר טחון קפוא',177,5),(180,'המבורגרים',173,5),(181,'המבורגרים',180,5),(182,'סופר סופר',0,5),(183,'שתיה',182,5),(184,'7up',183,5),(185,'מים',183,3),(186,'ירקות',182,5),(187,'עגבניות',186,5),(188,'מלפפונים',186,3),(189,'מוצרי חלב',182,3),(190,'חלב',189,5),(191,'גבינה צהובה',189,5),(192,'קוטג',189,5);
/*!40000 ALTER TABLE `items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'purchase_list'
--

--
-- Dumping routines for database 'purchase_list'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-08-11 20:57:54
