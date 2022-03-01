using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using NewTekkenApp.Utilities;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Common.Components.Base
{
    public abstract partial class BaseComponent<TDataEntity, TNameEntity> : ComponentBase
        where TDataEntity : BaseDataEntity, new()
        where TNameEntity : BaseNameEntity, new()
    {
        [Parameter] public AppType App { get; set; }
        [Parameter] public IBaseNameService<TDataEntity, TNameEntity> baseService { get; set; } = default!;
        [Parameter] public int Id { get; set; }
        [Parameter] public int? CharacterCode { get; set; }
        [Parameter] public int? StateGroupCode { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] protected NavigationUtil navigationUtil { get; set; } = default!;

        //[Inject]
        //protected ILogger<EditComponent<TDataEntity, TNameEntity>> Logger { get; set; }

        Dictionary<string, string> param = new Dictionary<string, string>();

        public TDataEntity baseData { get; set; } = default!;
        public TNameEntity? baseName { get; set; } = default!;

        #region 기본 버튼

        private void SetCommonParam()
        {
            if (StateGroupCode is not null) { param["StateGroupCode"] = StateGroupCode.ToString(); }
            if (CharacterCode is not null) { param["CharacterCode"] = CharacterCode.ToString(); }
        }
            protected void MoveToList()
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.List, 0, param);
            }

            #region 데이터 버튼
            protected void MoveToCreate()
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Create, 0, param);
            }
            protected void MoveToCreateWithStateGroup(int stateGroupCode)
            {
                if (stateGroupCode > 0)
                {
                    SetCommonParam();
                    navigationUtil.MoveTo(App, UserType.Admin, ActionType.Create, 0, param);
                }
            }
            protected void MoveToDetail(int id)
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Detail, id, param);
            }

            protected void MoveToEdit(int id)
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Edit, id, param);
            }

            protected void MoveToDelete(int id)
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Delete, id, param);
            }
            #endregion

            #region 이름 버튼
            protected void MoveToDetailName(int Id)
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Detail_name, Id, param);
            }
            protected void MoveToCreateName(int Code, string Language_code)
            {
                var query = new Dictionary<string, string> { { "Code", Code.ToString() }, { "Language", Language_code } };
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Create_name, Code, query);
            }
            protected void MoveToEditName(int Id)
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Edit_name, Id, param);
            }
            protected void MoveToDeleteName(int Id)
            {
                SetCommonParam();
                navigationUtil.MoveTo(App, UserType.Admin, ActionType.Delete_name, Id, param);
            }
            #endregion

            #endregion
            protected async Task number_Changed(string value)
            {
                int number;
                if (int.TryParse(value, out number))
                {
                    baseData.Number = number;
                    baseData.Code = await baseService.GetCreateCode(number, CharacterCode, StateGroupCode);
                }
            }

            public string GetAppTitle()
            {
                return App.ToString();
            }

            public void SetAppType(AppType appType)
            {
                this.App = appType;
            }
        }
    }
