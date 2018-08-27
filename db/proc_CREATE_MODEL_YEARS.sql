CREATE DEFINER=`h26346_cardata`@`%` PROCEDURE `CREATE_MODEL_YEARS`()
BEGIN
  DECLARE m_id,
          id int;
  DECLARE syear,
          eyear,
          curyear int;

  SET m_id = 0;

  WHILE m_id < 100000 DO
    SET m_id = m_id + 1;

    SELECT
      YEAR(MIN(model_start)) INTO syear
    FROM h26346_cardata.car_details
    WHERE model_id = m_id LIMIT 1;
    
    SELECT
      YEAR(MAX(model_end)) INTO eyear
    FROM h26346_cardata.car_details
    WHERE model_id = m_id LIMIT 1;

    IF (eyear = 0) THEN
      SET eyear = YEAR(NOW());
    END IF;

    SET curyear = syear;

    WHILE curyear <= eyear DO
      SELECT id INTO id FROM h26346_cardata.car_model_years
      WHERE model_id = m_id
      AND model_year = curyear LIMIT 1;

      IF ISNULL(id) THEN
        INSERT INTO h26346_cardata.car_model_years (model_id, model_year)
          VALUES (m_id, curyear);
      END IF;
      SET curyear = curyear + 1;
    END WHILE;

  END WHILE;
END