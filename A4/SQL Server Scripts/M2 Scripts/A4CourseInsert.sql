-- ================================================
-- Course Insert
-- Ensure table is reseeded prior to running this script
-- Using the following command:
-- DBCC CHECKIDENT(Courses, reseed, 800)
-- Replace DATABASENAME below with your actual 
-- BITCollege_FLContext database name
-- ================================================
USE [DATABASENAME]
GO

INSERT INTO [dbo].[Courses]
           ([ProgramId]
           ,[CourseNumber]
           ,[Title]
           ,[CreditHours]
           ,[TuitionAmount]
           ,[Notes]
           ,[AssignmentWeight]
           ,[MidtermWeight]
           ,[FinalWeight]
           ,[MaximumAttempts]
           ,[Discriminator])
     VALUES
(1,'A-1699','M2-PMM Audit 1',0,192,'Expected Key 801',NULL,NULL,NULL,NULL,'AuditCourse'),
(1,'A-1698','M2-PMM Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(1,'M-16999','M2-PMM Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(1,'M-16998','M2-PMM Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(1,'G-169999','M2-PMM Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(1,'G-169998','M2-PMM Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-169997','M2-PMM Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-169996','M2-PMM Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-169995','M2-PMM Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-169994','M2-PMM Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-169993','M2-PMM Graded 7',5,900,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'A-2699','M2-VT Audit 1',0,192,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(2,'A-2698','M2-VT Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(2,'M-26999','M2-VT Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(2,'M-26998','M2-VT Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(2,'G-269999','M2-VT Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(2,'G-269998','M2-VT Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-269997','M2-VT Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-269996','M2-VT Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-269995','M2-VT Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-269994','M2-VT Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-269993','M2-VT Graded 7',5,900,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'A-3699','M2-PBP Audit 1',0,192,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(3,'A-3698','M2-PBP Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(3,'M-36999','M2-PBP Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(3,'M-36998','M2-PBP Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(3,'G-369999','M2-PBP Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(3,'G-369998','M2-PBP Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-369997','M2-PBP Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-369996','M2-PBP Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-369995','M2-PBP Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-369994','M2-PBP Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-369993','M2-PBP Graded 7',5,900,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'A-4699','M2-CA Audit 1',0,192,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(4,'A-4698','M2-CA Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(4,'M-46999','M2-CA Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(4,'M-46998','M2-CA Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(4,'G-469999','M2-CA Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(4,'G-469998','M2-CA Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-469997','M2-CA Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-469996','M2-CA Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-469995','M2-CA Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-469994','M2-CA Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-469993','M2-CA Graded 7',5,900,'Expected Key 844',.40,.30,.30,NULL,'GradedCourse')


GO