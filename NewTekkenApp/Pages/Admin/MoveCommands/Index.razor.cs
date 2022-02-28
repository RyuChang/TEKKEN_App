using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveCommands
{
    public partial class Index : BasePageComponent
    {
        [Inject] ILanguageService LanguageService { get; set; } = default!;

        public IEnumerable<MoveCommand> MoveCommandEntities { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //baseEntities = await baseService.GetEntities();

            MoveCommandEntities = await CommonService.GetEntitiesWithMove();
        }

        async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            MoveCommandEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(CharacterCode);
            StateHasChanged();
        }


        async void GenerateAllname(int? characterCode)
        {
            if (characterCode is not null)
            {
                var moveEntities = await MoveService.GetEntitiesByCharacterCode(characterCode.Value);
                var languageEntities = await LanguageService.GetEntities();

                foreach (Move move in moveEntities)
                {
                    MoveCommand moveCommand = await CommonService.GetDataEntityByBaseCodeAsync(move.Code);

                    if (moveCommand == null)
                    {
                        await CreateMoveCommandEntity(move.Code);
                    }
                    else
                    {
                        foreach (var language in languageEntities)
                        {
                            MoveCommand_name command_name = await CommonService.GetNameEntitiyByBaseCodeAndLanguageCode(move.Code, language.Language_code);
                            if (command_name == null)
                            {
                                bool result = false;
                                result = await CommonService.CreateNameEntityAsync(moveCommand, language.Language_code);
                            }
                        }
                    }
                }
            }
        }

        private async Task<bool> CreateMoveCommandEntity(int moveCode)
        {
            MoveCommand moveCommand = new MoveCommand();
            moveCommand.Base_Code = moveCode;
            moveCommand.Code = moveCode;
            moveCommand.Command = "";
            moveCommand.Description = "";

            await CommonService.CreateEntityAsync(moveCommand);
            await CommonService.CreateAllNameEntitiesAsync(moveCommand);
            return true;
        }
    }
}