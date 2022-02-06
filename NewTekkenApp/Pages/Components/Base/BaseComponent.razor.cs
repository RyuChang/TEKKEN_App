using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NewTekkenApp.Data;
using NewTekkenApp.Utilities;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Components.Base
{
    public abstract partial class BaseComponent<TDataEntity, TNameEntity> : ComponentBase
        where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public AppType App { get; set; }

        [Parameter]
        public IBaseService<TDataEntity, TNameEntity> baseService { get; set; } = default!;


        //[Inject]
        //protected ILogger<EditComponent<TDataEntity, TNameEntity>> Logger { get; set; }

        [Inject]
        protected NavigationUtil navigationUtil { get; set; } = default!;

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public int? StateGroupCode { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        public TDataEntity baseData { get; set; } = default!;
        public TNameEntity? baseName { get; set; } = default!;

        #region 기본 버튼

        protected void MoveToList()
        {
            navigationUtil.MoveTo(App, ActionType.List);
        }
        #region 데이터 버튼
        protected void MovetoCreate()
        {
            navigationUtil.MoveTo(App, ActionType.Create);
        }
        protected void MovetoCreateWithStateGroup(int stateGroupCode)
        {
            navigationUtil.MoveTo(App, ActionType.Create, stateGroupCode);
        }
        protected void MoveToDetail(int id)
        {
            navigationUtil.MoveTo(App, ActionType.Detail, id);
        }

        protected void MoveToEdit(int id)
        {
            navigationUtil.MoveTo(App, ActionType.Edit, id);
        }

        protected void MoveToDelete(int id)
        {
            navigationUtil.MoveTo(App, ActionType.Delete, id);
        }
        #endregion

        #region 이름 버튼
        protected void MoveToDetailName(int Id)
        {
            navigationUtil.MoveTo(App, ActionType.Detail_name, Id);
        }
        protected void MoveToCreateName(int Code, string Language_code)
        {
            var query = new Dictionary<string, string> { { "Code", Code.ToString() }, { "Language", Language_code } };
            navigationUtil.MoveTo(App, ActionType.Create_name, Code, query);
        }
        protected void MoveToEditName(int Id)
        {
            navigationUtil.MoveTo(App, ActionType.Edit_name, Id);
        }
        protected void MoveToDeleteName(int Id)
        {
            navigationUtil.MoveTo(App, ActionType.Delete_name, Id);
        }
        #endregion

        #endregion
        protected async Task number_Changed(string value)
        {
            int number;
            if (int.TryParse(value, out number))
            {
                baseData.Number = number;
                baseData.Code = await baseService.GetCreateCode(number);
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
