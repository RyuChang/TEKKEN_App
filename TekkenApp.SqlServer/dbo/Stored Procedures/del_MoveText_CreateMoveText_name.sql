

--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[del_MoveText_CreateMoveText_name] @moveText_code INT, 
                                                @language_code CHAR(2), 
                                                @name          VARCHAR(MAX)
AS
     INSERT INTO [dbo].[moveText_name]
     (moveText_code, 
      language_code, 
      name
     )
     VALUES
     (@moveText_code, 
      @language_code, 
      @name
     );
