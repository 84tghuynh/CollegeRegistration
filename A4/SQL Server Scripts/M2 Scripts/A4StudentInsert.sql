-- ================================================
-- Student Insert
-- Ensure table is reseeded prior to running this script
-- Using the following command:
-- DBCC CHECKIDENT(Students, reseed, 800)
-- Replace DATABASENAME below with your actual 
-- BITCollege_FLContext database name
-- ================================================
USE [DATABASENAME]
GO

INSERT INTO [dbo].[Students]
           ([GPAStateId]
           ,[ProgramId]
           ,[StudentNumber]
           ,[FirstName]
           ,[LastName]
           ,[Address]
           ,[City]
           ,[Province]
           ,[PostalCode]
           ,[DateCreated]
           ,[GradePointAverage]
           ,[OutstandingFees]
           ,[Notes])
     VALUES
		(3,1,11111111,'Eric','Ellis','5 5th St.','Winnipeg','MB','R3K 1K3','03-Aug-18',3.00,350,'Expected Key 801'),
		(4,1,11111112,'Francis','Falk','6 6th St.','Winnipeg','MB','R3K 1K2','03-Aug-18',4.00,1000,'Expected Key 802'),
		(1,2,11111113,'George','Garez','7 7th St.','Winnipeg','MB','R3K 1K3','03-Aug-18',2.00,0,'Expected Key 803'),
		(1,3,11111114,'Henry','Harrison','8 8th St.','Winnipeg','MB','R3B 1K9','03-Aug-18',3.00,1000,'Expected Key 804'),
		(4,4,11111115,'Isaac','Ireges','9 9th St.','Winnipeg','MB','R4R 3T3','03-Aug-18',4.00,500,'Expected Key 805'),
		(2,4,11111116,'Jackie','Jones','10 10th St.','Winnipeg','MB','R3B 1K3','03-Aug-18',3.00,75,'Expected Key 806'),
		(2,4,11111117,'Karen','Kross','11 11th St.','Winnipeg','MB','R3K 1K3','03-Aug-18',3.00,750,'Expected Key 807'),
		(2,4,11111118,'Lindsay','Lange','12 12th St.','Winnipeg','MB','R3K 1K6','03-Aug-18',2.00,5000,'Expected Key 808'),
		(2,4,11111119,'Mark','Morris','13 13th St.','Winnipeg','MB','R3B 1K7','03-Aug-18',1.00,500,'Expected Key 809'),
		(2,6,11111120,'Nancy','Nederson','14 14th St.','Winnipeg','MB','R3B 1K4','03-Aug-18',2.00,700,'Expected Key 810'),
		(2,5,11111121,'Owen','Olafson','15 15th St.','Winnipeg','MB','R3K 1K7','03-Aug-18',4.00,150,'Expected Key 811'),
		(2,6,11111122,'Peter','Parker','16 16th St.','Winnipeg','MB','R3K 1K3','03-Aug-18',1.00,2500,'Expected Key 812')

GO
