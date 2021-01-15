-- ================================================
-- Course Insert
-- Ensure table is reseeded prior to running this script
-- Using the following command:
-- DBCC CHECKIDENT(Courses, reseed, 500)
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
(1,'A-1999','PMM Audit 1',0,192,'Expected Key 501',NULL,NULL,NULL,NULL,'AuditCourse'),
(1,'A-1998','PMM Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(1,'M-19999','PMM Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(1,'M-19998','PMM Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(1,'G-199999','PMM Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(1,'G-199998','PMM Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-199997','PMM Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-199996','PMM Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-199995','PMM Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-199994','PMM Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(1,'G-199993','PMM Graded 7',5,900,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'A-2999','VT Audit 1',0,192,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(2,'A-2998','VT Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(2,'M-29999','VT Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(2,'M-29998','VT Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(2,'G-299999','VT Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(2,'G-299998','VT Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-299997','VT Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-299996','VT Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-299995','VT Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-299994','VT Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(2,'G-299993','VT Graded 7',5,900,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'A-3999','PBP Audit 1',0,192,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(3,'A-3998','PBP Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(3,'M-39999','PBP Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(3,'M-39998','PBP Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(3,'G-399999','PBP Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(3,'G-399998','PBP Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-399997','PBP Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-399996','PBP Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-399995','PBP Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-399994','PBP Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(3,'G-399993','PBP Graded 7',5,900,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'A-4999','CA Audit 1',0,192,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(4,'A-4998','CA Audit 2',0,1322,'BATCH',NULL,NULL,NULL,NULL,'AuditCourse'),
(4,'M-49999','CA Mastery 1',3,633,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(4,'M-49998','CA Mastery 2',3,1506,'BATCH',NULL,NULL,NULL,1,'MasteryCourse'),
(4,'G-499999','CA Graded 1',5,1000,'BATCH',.50,.25,.25,NULL,'GradedCourse'),
(4,'G-499998','CA Graded 2',5,925,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-499997','CA Graded 3',5,950,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-499996','CA Graded 4',5,258,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-499995','CA Graded 5',5,625,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-499994','CA Graded 6',5,1125,'BATCH',.40,.30,.30,NULL,'GradedCourse'),
(4,'G-499993','CA Graded 7',5,900,'Expected Key 544',.40,.30,.30,NULL,'GradedCourse')


GO