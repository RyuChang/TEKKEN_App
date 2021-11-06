
--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[del_MoveSubtype_UpdateMoveSubtype_name] @moveSubType_code CHAR(10), 
                                                            @language_code    CHAR(2), 
                                                            @name             VARCHAR(MAX)
AS
     UPDATE [TEKKEN].[dbo].[moveSubType_name]
       SET 
           name = @name
     WHERE moveSubType_code = @moveSubType_code
           AND language_code = @language_code;
