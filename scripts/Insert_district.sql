INSERT INTO public.district(
	name, primary_sales_person_id, secondary_sales_person_ids, stores)
	VALUES 
	('Hovedstaden', 1, ARRAY[2,3,4,5,6], ARRAY['Store_A','Store_B','Store_C']),
	('Sjælland', 2, ARRAY[1,3,4,5,6], ARRAY['Store_D','Store_E','Store_F']),
	('Syddanmark', 3, ARRAY[1,2,4,5,6], ARRAY['Store_G','Store_H','Store_I']),
	('Midtjylland', 4, ARRAY[1,2,3,5,6], ARRAY['Store_J','Store_K','Store_L']),
	('Nordjylland', 5, ARRAY[1,2,3,4,6], ARRAY['Store_M','Store_N','Store_O']);