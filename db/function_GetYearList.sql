CREATE FUNCTION `GetYearList` (id INTEGER)
RETURNS VARCHAR(255)
BEGIN
    DECLARE lowest_year INTEGER;
    DECLARE highest_year INTEGER;   
    DECLARE lowest_date DATETIME;
    DECLARE highest_date DATETIME;
    DECLARE retval VARCHAR(255);
    DECLARE curyear INTEGER;
    
	SELECT MIN(model_start) INTO lowest_date FROM h26346_cardata.car_details WHERE model_id = id LIMIT 1; 
    
    SELECT MIN(model_end) INTO highest_date FROM h26346_cardata.car_details WHERE model_id = id LIMIT 1; 
   
    SET lowest_year = YEAR(lowest_date);
    SET highest_year = YEAR(highest_date);
    IF(highest_year = 0) THEN SET highest_year=YEAR(NOW()); END IF;
    
    SET curyear = lowest_year;
    REPEAT		
		SET retval = CONCAT(retval,curyear,',');
		SET curyear = curyear + 1;
		UNTIL curyear >= highest_year
	END REPEAT;
    
	RETURN retval;
END