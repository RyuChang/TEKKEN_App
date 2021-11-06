

--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[del_State_CreateState_name]		@State_code INT, 
                                                 @language_code   CHAR(2), 
                                                 @name            VARCHAR(MAX)
AS
     INSERT INTO [dbo].[State_name]
     (State_code, 
      language_code, 
      name, 
      Checked
     )
     VALUES
     (@State_code, 
      @language_code, 
      @name, 
      0
     );
