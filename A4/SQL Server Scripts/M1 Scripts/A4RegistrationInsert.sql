-- ================================================
-- Registration Insert
-- Ensure table is reseeded prior to running this script
-- Using the following command:
-- DBCC CHECKIDENT(Registrations, reseed, 500)
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
 (501,501,2000,.95,'3-Aug-18','Expected Key 501')
,(501,503,2001,.92,'3-Aug-18','Batch Registration')
,(501,505,2002,.87,'3-Aug-18','Batch Registration')
,(501,506,2003,NULL, '1-Jul-18','Batch Registration')
,(501,502,2004,NULL, '1-Jul-18','Batch Registration')
,(501,510,2005,NULL, '1-Jul-18','Batch Registration')
,(502,513,2006,.75,'3-Aug-18','Expected Key 507')
,(502,514,2007,NULL,'3-Aug-18','Batch Registration')
,(502,516,2008,.47,'3-Aug-18','Batch Registration')
,(503,513,2009,.75,'3-Aug-18','Expected Key 510')
,(503,514,2010,NULL,'3-Aug-18','Batch Registration')
,(503,516,2011,NULL,'3-Aug-18','Batch Registration')
,(503,518,2012,NULL, '1-Jul-18','Batch Registration')
,(503,521,2013,NULL, '1-Jul-18','Batch Registration')
,(504,528,2014,NULL,'3-Aug-18','Expected Key 515')
,(504,529,2015,NULL,'3-Aug-18','Batch Registration')
,(505,535,2016,.45,'3-Aug-18','Expected Key 517')
,(505,536,2017,.42,'3-Aug-18','Batch Registration')
,(505,537,2018,.57,'3-Aug-18','Batch Registration')
,(505,540,2019,NULL, '1-Jul-18','Batch Registration')
,(505,542,2020,NULL, '1-Jul-18','Batch Registration')
,(505,541,2021,NULL, '1-Jul-18','Batch Registration')
,(506,536,2022,.65,'3-Aug-18','Expected Key 523')
,(506,538,2023,.82,'3-Aug-18','Batch Registration')
,(506,540,2024,.77,'3-Aug-18','Batch Registration')
,(506,535,2025,.70, '1-Jul-18','Batch Registration')
,(506,537,2026,.80, '1-Jul-18','Batch Registration')
,(506,541,2027,NULL, '1-Jul-18','Expected Key 528')

GO