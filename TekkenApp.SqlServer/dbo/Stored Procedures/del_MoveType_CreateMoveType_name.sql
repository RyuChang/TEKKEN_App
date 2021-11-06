

--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[del_MoveType_CreateMoveType_name] @moveType_code	CHAR(10), 
                                                       @language_code   CHAR(2), 
                                                       @name            VARCHAR(MAX)
AS
     INSERT INTO [dbo].[moveType_name]
     (moveType_code, 
      language_code, 
      name,
	  Checked
     )
     VALUES
     (@moveType_code, 
      @language_code, 
      @name,
	  0
     );
