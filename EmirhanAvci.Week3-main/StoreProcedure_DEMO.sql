create procedure AddStudentToEducationIfStudentDontHaveAnExistEducation(
	@userId int,
	@educationId int,
	@process float
)
as
begin
	if(@process=0) begin
	Insert into tbl_EducationUsers values (@userId,@educationId);
end
