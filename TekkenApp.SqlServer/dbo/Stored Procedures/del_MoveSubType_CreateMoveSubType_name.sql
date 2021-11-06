
--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[del_MoveSubType_CreateMoveSubType_name] @moveSubType_code CHAR(10), 
                                                            @language_code    CHAR(2), 
                                                            @name             VARCHAR(MAX)
AS
     INSERT INTO [dbo].[moveSubType_name]
     (moveSubType_code, 
      language_code, 
      name
     )
     VALUES
     (@moveSubType_code, 
      @language_code, 
      @name
     );