


--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[Move_CreateMove_name_DEL] @move_code     INT, 
                                         @language_code CHAR(2), 
                                         @name          VARCHAR(MAX)
AS
     INSERT INTO [dbo].[move_name]
     (move_code, 
      language_code, 
      name, 
      Checked
     )
     VALUES
     (@move_code, 
      @language_code, 
      @name, 
      0
     );
