USE [DbEducationalInstitution]
GO

/****** Object:  Trigger [dbo].[processController]    Script Date: 29.01.2022 20:42:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create trigger [dbo].[processController] on [dbo].[tbl_RollCalls]
after insert
as
begin
	--inserted
	declare @studentId int
	declare @duration int
	declare @rollCallControl bit
	select @studentId=StudentId,@duration=Duration, @rollCallControl=RollCallControl from inserted

	Update tbl_Process set Process=Process+@duration/100*@rollCallControl where StudentId=@studentId
end
GO

ALTER TABLE [dbo].[tbl_RollCalls] ENABLE TRIGGER [processController]
GO

