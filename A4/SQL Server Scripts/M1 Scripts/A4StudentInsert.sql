-- ================================================
-- Student Insert
-- Ensure table is reseeded prior to running this script
-- Using the following command:
-- DBCC CHECKIDENT(Students, reseed, 500)
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
		(3,1,88888888,'Jennifer','Johnson','133 Main St.','Winnipeg','MB','R3K 1K3','03-Aug-18',3.25,350,'Expected Key 501'),
		(4,2,88888889,'Michael','Martins','132 Main St.','Winnipeg','MB','R3K 1K2','03-Aug-18',4.32,1000,'Expected Key 502'),
		(1,2,88888886,'Albert','Adams','1 1st Street','Winnipeg','MB','R3K 1K3','03-Aug-18',1.11,0,'Expected Key 503'),
		(1,3,88888885,'Betty','Brown','2 2nd Street','Winnipeg','MB','R3B 1K9','03-Aug-18',1.25,1000,'Expected Key 504'),
		(4,4,88888884,'Carl','Cook','3 3rd Street','Winnipeg','MB','R4R 3T3','03-Aug-18',4.21,500,'Expected Key 505'),
		(2,4,88888883,'Don','Drexler','4 4th Street','Winnipeg','MB','R3B 1K3','03-Aug-18',3.25,75,'Expected Key 506')

GO
