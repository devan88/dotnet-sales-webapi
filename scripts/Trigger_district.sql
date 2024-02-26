CREATE OR REPLACE FUNCTION check_secondary_sales_persons()
  RETURNS TRIGGER 
  LANGUAGE PLPGSQL
  AS
$$
BEGIN
	IF 
		NOT NEW.secondary_sales_person_ids <@ ARRAY(SELECT id FROM sales_person)::int[]
	THEN
		RAISE EXCEPTION 'secondary sales person does not exists in table';
	END IF;
	RETURN NEW;
END;
$$;

CREATE OR REPLACE TRIGGER secondary_sales_persons_changes
  BEFORE UPDATE
  ON district
  FOR EACH ROW
  EXECUTE PROCEDURE check_secondary_sales_persons();