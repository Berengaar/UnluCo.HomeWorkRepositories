USE [DbEducationalInstitution]
GO

/****** Object:  View [dbo].[GetStudentsByEducation]    Script Date: 29.01.2022 21:48:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetStudentsByEducation] AS
SELECT UserId, EducationId
FROM tbl_EducationUsers
GO

