UPDATE district
SET secondary_sales_person_ids = ARRAY[1,11]
WHERE id = 1;

SELECT * FROM sales_person;
SELECT * FROM district;

SELECT ARRAY(SELECT id FROM sales_person)::int[] as ty <@ ANY('{1,2,3,5,6,7}'::int[])

SELECT 252 = ANY('{100, 12, 30, 150, 25}'::int[]) As array_contains;

SELECT id FROM sales_person = ARRAY[1,2,3,5,6,7]

SELECT id
FROM sales_person
WHERE id <> ALL(ARRAY[1,2,3,5,6,7]);

SELECT id
FROM sales_person
WHERE id <> ALL(ARRAY[1,2,3,5,6,7])

SELECT EXISTS (SELECT ARRAY[1]::int[] <@ ARRAY(SELECT id FROM sales_person)::int[])
SELECT ARRAY[1.1,2.1,3.1]::int[] = ARRAY[1,2,3]