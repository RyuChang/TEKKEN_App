



--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[MoveCommand_CreateMoveCommand_name] @move_code     INT, 
                                                @name          VARCHAR(MAX), 
                                                @language_code CHAR(2)
AS
     INSERT INTO [dbo].[move_command_NAME]
     (Move_Command_code, 
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
