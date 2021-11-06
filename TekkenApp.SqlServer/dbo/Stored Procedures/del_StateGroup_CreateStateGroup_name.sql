
--[1] 게시판(DotNetNote)에 글을 작성 : WriteNote
CREATE PROC [dbo].[del_StateGroup_CreateStateGroup_name] @StateGroup_code INT, 
                                                    @language_code   CHAR(2), 
                                                    @name            VARCHAR(MAX)
AS
     INSERT INTO [dbo].[StateGroup_name]
     (StateGroup_code, 
      language_code, 
      name, 
      Checked
     )
     VALUES
     (@StateGroup_code, 
      @language_code, 
      @name, 
      0
     );
