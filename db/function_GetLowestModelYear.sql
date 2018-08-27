CREATE FUNCTION `GetLowestModelYear` (id INTEGER)
RETURNS INTEGER
BEGIN
    DECLARE lowest_year INTEGER;
    DECLARE lowest_date DATETIME;
    
	SELECT MIN(model_start) AS lowest_date INTO @lowest_date FROM h26346_cardata.car_details WHERE model_id = id LIMIT 1; 
   
    SET lowest_year = YEAR(lowest_date);
    
	RETURN lowest_year;
END