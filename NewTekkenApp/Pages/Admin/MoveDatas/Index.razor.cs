using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class Index : BasePageComponent
    {
        [Inject] ILanguageService LanguageService { get; set; } = default!;
        public IList<MoveData> moveDataEntities { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            moveDataEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(CharacterCode.Value);
        }

        async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            moveDataEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(CharacterCode.Value);
            StateHasChanged();
        }

        async void GenerateAllData(int? characterCode)
        {
            if (characterCode is not null)
            {
                var moveEntities = await MoveService.GetEntitiesByCharacterCode(characterCode.Value);
                var languageEntities = await LanguageService.GetEntities();

                foreach (Move move in moveEntities)
                {
                    MoveData moveData = await CommonService.GetDataEntityByBaseCodeAsync(move.Code);

                    if (moveData == null)
                    {
                        await CreateMoveDataEntity(move.Code);
                    }
                    else
                    {
                        foreach (var language in languageEntities)
                        {
                            MoveData_name data_name = await CommonService.GetNameEntitiyByBaseCodeAndLanguageCode(move.Code, language.Language_code);
                            if (data_name == null)
                            {
                                bool result = false;
                                result = await CommonService.CreateNameEntityAsync(moveData, language.Language_code);
                            }
                        }
                    }
                }
            }
        }

        private async Task<bool> CreateMoveDataEntity(int moveCode)
        {
            MoveData moveData = new MoveData();
            moveData.Base_code = moveCode;
            moveData.Code = moveCode;
            moveData.Description = "";
            moveData.MoveSubType_code = 0;
            moveData.HitCount = 0;
            moveData.Damage = 0;
            moveData.StartFrame = 0;
            moveData.StartType_code = 0;
            moveData.GuardType_code = 0;
            moveData.HitFrame = 0;
            moveData.HitType_code = 0;
            moveData.CounterFrame = 0;
            moveData.CounterType_code = 0;
            moveData.Version = 0;

            moveData.AfterBreak = "";
            moveData.BreakThrow = "";
            moveData.CounterFrame_Display = "";
            moveData.GuardFrame_Display = "";
            moveData.HitFrame_Display = "";
            moveData.HitLevel = "";
            moveData.Note = "";
            moveData.StartFrame_Display = "";



            await CommonService.CreateEntityAsync(moveData);
            await CommonService.CreateAllNameEntitiesAsync(moveData);
            return true;
        }
    }
}
