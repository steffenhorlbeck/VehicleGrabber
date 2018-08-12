/* Database export results for db h26346_cardata */

/* Preserve session variables */
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS;
SET FOREIGN_KEY_CHECKS=0;

/* Export data */

/* Table structure for car_details */
CREATE TABLE `car_details` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `maker_id` int(11) DEFAULT NULL,
  `model_id` int(11) DEFAULT NULL,
  `modeltype_id` int(11) DEFAULT NULL,
  `maker` varchar(150) DEFAULT NULL,
  `model` varchar(150) DEFAULT NULL,
  `type` varchar(150) DEFAULT NULL,
  `series` varchar(100) DEFAULT NULL,
  `modeltypename` varchar(100) DEFAULT NULL,
  `internal_class_name` varchar(45) DEFAULT NULL,
  `model_start` datetime DEFAULT NULL,
  `model_end` datetime DEFAULT NULL,
  `series_start` datetime DEFAULT NULL,
  `series_end` datetime DEFAULT NULL,
  `hsn` varchar(5) DEFAULT NULL,
  `tsn` varchar(5) DEFAULT NULL,
  `tsn2` varchar(20) DEFAULT NULL,
  `car_tax` varchar(45) DEFAULT NULL,
  `co2_class` varchar(45) DEFAULT NULL,
  `base_price` varchar(45) DEFAULT NULL,
  `engine_type` varchar(45) DEFAULT NULL,
  `fuel` varchar(45) DEFAULT NULL,
  `fuel2` varchar(45) DEFAULT NULL,
  `emission_control` varchar(45) DEFAULT NULL,
  `engine_design` varchar(45) DEFAULT NULL,
  `cylinder` int(11) DEFAULT NULL,
  `fuel_type` varchar(45) DEFAULT NULL,
  `charge` varchar(45) DEFAULT NULL,
  `valves` int(11) DEFAULT NULL,
  `cubic` varchar(45) DEFAULT NULL,
  `power_kw` int(11) DEFAULT NULL,
  `power_ps` int(11) DEFAULT NULL,
  `max_power` varchar(45) DEFAULT NULL,
  `turning_moment` varchar(45) DEFAULT NULL,
  `max_turning_moment` varchar(45) DEFAULT NULL,
  `type_of_drive` varchar(45) DEFAULT NULL,
  `gearing` varchar(45) DEFAULT NULL,
  `gears` int(11) DEFAULT NULL,
  `start_stop_automatic` varchar(45) DEFAULT NULL,
  `emission_class` varchar(45) DEFAULT NULL,
  `length` varchar(45) DEFAULT NULL,
  `width` varchar(45) DEFAULT NULL,
  `height` varchar(45) DEFAULT NULL,
  `chassis` varchar(45) DEFAULT NULL,
  `doors` int(11) DEFAULT NULL,
  `car_class` varchar(45) DEFAULT NULL,
  `seats` int(11) DEFAULT NULL,
  `speed_up` varchar(45) DEFAULT NULL,
  `max_speed` varchar(45) DEFAULT NULL,
  `tank` varchar(45) DEFAULT NULL,
  `tank2` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_idx` (`maker_id`),
  KEY `model_id_idx` (`model_id`),
  KEY `modeltype_id_idx` (`modeltype_id`),
  CONSTRAINT `maker_id` FOREIGN KEY (`maker_id`) REFERENCES `car_maker` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `model_id` FOREIGN KEY (`model_id`) REFERENCES `car_model` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `modeltype_id` FOREIGN KEY (`modeltype_id`) REFERENCES `car_modeltype` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* Table structure for car_maker */
CREATE TABLE `car_maker` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `url` varchar(100) DEFAULT NULL,
  `logo` mediumblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* Table structure for car_model */
CREATE TABLE `car_model` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(150) NOT NULL,
  `maker` varchar(100) DEFAULT NULL,
  `maker_id` int(11) DEFAULT NULL,
  `image` mediumblob,
  `model_url` varchar(150) DEFAULT NULL,
  `img_url` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* Table structure for car_modeltype */
CREATE TABLE `car_modeltype` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `maker_id` int(11) DEFAULT NULL,
  `model_id` int(11) DEFAULT NULL,
  `modeltype_id` int(11) DEFAULT NULL,
  `name` varchar(150) DEFAULT NULL,
  `cubic` varchar(45) DEFAULT NULL,
  `fuel` varchar(45) DEFAULT NULL,
  `power` varchar(45) DEFAULT NULL,
  `tank` varchar(45) DEFAULT NULL,
  `from_year` varchar(45) DEFAULT NULL,
  `to_year` varchar(45) DEFAULT NULL,
  `chassis` varchar(45) DEFAULT NULL,
  `doors` int(11) DEFAULT NULL,
  `type_url` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* Restore session variables to original values */
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
