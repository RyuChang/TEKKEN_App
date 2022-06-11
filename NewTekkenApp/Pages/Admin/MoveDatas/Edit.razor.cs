using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveDatas
{
    public partial class Edit : BasePageComponent
    {
        [Parameter] public string NextNumber { get; set; }
        public Move moveEntity { get; set; } = default!;

        public List<SelectListItem> MoveTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> MoveSubTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> StartTypeSelectListItems { get; set; } = default!;
        public List<SelectListItem> HitTypeSelectListItems { get; set; } = default!;

        public List<SelectListItem> GuardTypeSelectListItems { get; set; } = default!;

        public List<SelectListItem> CounterTypeSelectListItems { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var queryStrings = navigationUtil.GetQueryStrings();
            if (queryStrings.TryGetValue("NextNumber", out var _nextNumber))
            {
                NextNumber = _nextNumber;
            }
            if (NextNumber is null)
            {
                moveEntity = await MoveService.GetMoveWithMoveDataByIdAsync(Id);
            }
            else
            {
                moveEntity = await MoveService.GetMoveWithMoveDataByCharacterCodeAndNumberAsync(CharacterCode.Value, int.Parse(NextNumber));
            }

            MoveTypeSelectListItems = await moveTypeService.GetSelectItems(true);

            MoveSubTypeSelectListItems = await moveSubTypeService.GetSelectItems(true);

            StartTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            HitTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            GuardTypeSelectListItems = await hitTypeService.GetSelectItems(true);

            CounterTypeSelectListItems = await hitTypeService.GetSelectItems(true);
        }

        protected async Task SaveEdit()
        {


            if (!await JSRuntime.InvokeAsync<bool>("confirm", "저장 하겠습니까"))
            {
                return;
            }
            await MoveService.UpdateDataAsync(moveEntity);
            MoveToDetail(moveEntity.Id);
        }

        private void HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");
        }
        protected async Task LoadMigrationData()
        {
            Character character = await CharacterService.GetCharacterByCharacterCode(CharacterCode.Value);

            Character_name character_name_en = character.NameSet.Where(n => n.Language_code == "en").FirstOrDefault();

            MigrationDataVM? migrationDataVM = await CommonService.GetMigrationData(character_name_en.Name, moveEntity.Description);
            if (migrationDataVM.TITLE_EN == null)
            {
                return;
            }

            moveEntity.MoveData.MoveType_code = 60000001;


            moveEntity.MoveData.HitCount = StringToNumber(migrationDataVM.hit);
            moveEntity.MoveData.HitLevel = TransHitLevel(migrationDataVM.HitLv);


            moveEntity.MoveData.StartFrame = StringToNumber(migrationDataVM.StartFrame);
            moveEntity.MoveData.StartFrame_Display = migrationDataVM.StartFrame;
            moveEntity.MoveData.StartType_code = 110000001;

            moveEntity.MoveData.GuardFrame = StringToNumber(migrationDataVM.BlockFrame);
            moveEntity.MoveData.GuardFrame_Display = migrationDataVM.BlockFrame;
            moveEntity.MoveData.GuardType_code = 110000001;

            if (migrationDataVM.Dmg != "-")
            {
                moveEntity.MoveData.Damage = int.Parse(migrationDataVM.Dmg);
            }
            else
            {
                moveEntity.MoveData.Damage = 0;
            }
            moveEntity.MoveData.Damage_Display = migrationDataVM.DisplayDmg;
            //moveEntity.MoveData.Damage
            moveEntity.MoveData.HitFrame = GetHitFrame(migrationDataVM.HitFrame);
            moveEntity.MoveData.HitType_code = GetHitTypeCode(migrationDataVM.HitFrame);
            moveEntity.MoveData.HitFrame_Display = migrationDataVM.HitFrame;

            moveEntity.MoveData.CounterFrame = GetHitFrame(migrationDataVM.CounterFrame);
            moveEntity.MoveData.CounterType_code = GetHitTypeCode(migrationDataVM.CounterFrame);
            moveEntity.MoveData.CounterFrame_Display = migrationDataVM.CounterFrame;


            Console.WriteLine(migrationDataVM.number);
        }


        private int StringToNumber(string data)
        {
            string number = string.Empty;
            char type = ' ';

            if (data == "-")
            {
                return 0;
            }
            //data

            for (int i = 0; i <= data.Length; i++)
            {
                if (data[i] == '-' || data[i] == '+' || data[i] == 'D')
                {
                    type = data[i];
                    break;
                }
                if (char.IsDigit(data[i]))
                {
                    type = '+';
                    break;
                }
            }

            if (type == 'D')
            {
                return 0;
            }
            else
            {
                number = new string(data.SkipWhile(c => !char.IsDigit(c))
                         .TakeWhile(c => char.IsDigit(c) || c == '-')
                         .ToArray());
                number = type + number;

            }


            return Convert.ToInt32(number);
        }
        private int GetHitTypeCode(string data)
        {
            char type = data[0];

            if (type == 'D')
            {
                return 110000002;
            }
            else if (type == 'A')
            {
                return 110000003;
            }
            else if (type == 'S')
            {
                return 110000006;
            }

            return 110000001;
        }


        private string TransHitLevel(string data)
        {
            data = data.Replace("UNBLOCK", "UB/");
            data = data.Replace("SPECIAL", "SP/");
            data = data.Replace("HIGH", "H/");
            data = data.Replace("MID", "M/");
            data = data.Replace("LOW", "L/");
            data = data.Replace("PARRY", "P/");
            data = data.Replace("-", "/");
            data = data.Replace(" ", ""); ;

            return data.Substring(0, data.Length - 1);
        }

        private int GetHitFrame(string data)
        {
            int result;
            char type = data[0];

            if (type == 'A' || type == 'D' || type == 'S')
            {
                return 0;
            }
            result = StringToNumber(data);
            return result;
        }

    }
}
