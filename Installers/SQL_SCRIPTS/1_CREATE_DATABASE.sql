--BD creation
IF NOT EXISTS(SELECT name FROM SYS.DATABASES WHERE name = 'GBMChallenge')
BEGIN
    CREATE DATABASE GBMChallenge
END
--Check BD
SELECT * FROM SYS.DATABASES WHERE name = 'GBMChallenge'
