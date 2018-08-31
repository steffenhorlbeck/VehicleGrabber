SELECT car_details.id, car_details.modeltypename, car_details.hsn, car_details.tsn
FROM car_detailscar_modeltype
INNER JOIN (
SELECT modeltypename,hsn,tsn
FROM car_details
GROUP BY modeltypename,hsn, tsn
HAVING COUNT(id) > 1) dup ON car_details.modeltypename = dup.modeltypename && car_details.tsn = dup.tsn && car_details.tsn = dup.tsn