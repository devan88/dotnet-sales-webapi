-- This script was generated by the ERD tool in pgAdmin 4.
-- Please log an issue at https://github.com/pgadmin-org/pgadmin4/issues/new/choose if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE IF NOT EXISTS public.district
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    name text NOT NULL UNIQUE,
    primary_sales_person_id  integer NOT NULL CHECK ((primary_sales_person_id <> ALL(secondary_sales_person_ids)) IS TRUE),
    secondary_sales_person_ids integer[] NOT NULL CHECK ((primary_sales_person_id <> ALL(secondary_sales_person_ids)) IS TRUE),
    stores text[],
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.sales_person
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    name text NOT NULL UNIQUE,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.district
    ADD FOREIGN KEY (primary_sales_person_id)
    REFERENCES public.sales_person (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;

END;