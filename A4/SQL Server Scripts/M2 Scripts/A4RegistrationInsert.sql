-- ================================================
-- Registration Insert
-- Ensure table is reseeded prior to running this script
-- Using the following command:
-- DBCC CHECKIDENT(Registrations, reseed, 800)
-- Replace DATABASENAME below with your actual 
-- BITCollege_FLContext database name
-- ================================================
USE [DATABASENAME]
GO


INSERT INTO [dbo].[Registrations]
           ([StudentId]
           ,[CourseId]
           ,[RegistrationNumber]
		   ,[Grade]
           ,[RegistrationDate]
		   ,[Notes])
     VALUES
 (801,801,3000,.95,'3-Aug-18','Expected Key 801')
,(801,803,3001,.92,'3-Aug-18','Batch Registration')
,(801,805,3002,.87,'3-Aug-18','Batch Registration')
,(801,806,3003,NULL, '1-Jul-18','Batch Registration')
,(801,802,3004,NULL, '1-Jul-18','Batch Registration')
,(801,810,3005,NULL, '1-Jul-18','Batch Registration')
,(802,803,3006,.60,'3-Aug-18','Expected Key 807')
,(802,805,3007,.50,'3-Aug-18','Batch Registration')
,(802,806,3008,NULL,'3-Aug-18','Batch Registration')
,(802,807,3009,.40,'3-Aug-18','Expected Key 810')
,(803,815,3010,NULL,'3-Aug-18','Batch Registration')
,(803,816,3011,NULL,'3-Aug-18','Batch Registration')
,(803,818,3012,NULL, '1-Jul-18','Batch Registration')
,(803,821,3013,NULL, '1-Jul-18','Batch Registration')
,(803,828,3014,NULL,'3-Aug-18','Expected Key 815')
,(804,829,3015,NULL,'3-Aug-18','Batch Registration')
,(805,835,3016,.52,'3-Aug-18','Expected Key 817')
,(805,836,3017,.48,'3-Aug-18','Batch Registration')
,(805,837,3018,.44,'3-Aug-18','Batch Registration')
,(805,840,3019,NULL, '1-Jul-18','Batch Registration')
,(805,842,3020,NULL, '1-Jul-18','Batch Registration')
,(805,841,3021,NULL, '1-Jul-18','Batch Registration')
,(806,836,3022,.85,'3-Aug-18','Expected Key 823')
,(806,838,3023,.82,'3-Aug-18','Batch Registration')
,(806,840,3024,.87,'3-Aug-18','Batch Registration')
,(806,835,3025,.90, '1-Jul-18','Batch Registration')
,(806,837,3026,.90, '1-Jul-18','Batch Registration')
,(806,841,3027,NULL, '1-Jul-18','Expected Key 828')

GO