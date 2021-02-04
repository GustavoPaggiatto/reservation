USE [Reservation]
go

/****** Object:  UserDefinedFunction [dbo].[split]    Script Date: 27/01/2021 17:33:45 ******/
CREATE FUNCTION [dbo].[split] ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN
	 DECLARE @name NVARCHAR(255)
	 DECLARE @pos INT

	 WHILE CHARINDEX(',', @stringToSplit) > 0
	 BEGIN
		  SELECT @pos  = CHARINDEX(',', @stringToSplit)  
		  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

		  INSERT INTO @returnList 
		  SELECT @name

		  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
	 END

	 INSERT INTO @returnList
	 SELECT @stringToSplit

	 RETURN
END
GO

/****** Object:  StoredProcedure [dbo].[sp_get_contacts_by_reserveIds]    Script Date: 27/01/2021 17:33:45 ******/
create proc [dbo].[sp_get_contacts_by_reserveIds]
(
	@contactIds varchar(max)
) as
begin
	select
		convert(int,s.Name) [contactId]
	into
		#temp
	from
		dbo.split(@contactIds) s	

	select
		c.*
	from
		Contact c
			join #temp t on t.contactId=c.id
end