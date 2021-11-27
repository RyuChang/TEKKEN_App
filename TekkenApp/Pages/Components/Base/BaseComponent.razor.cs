using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using TekkenApp.Data;
using TekkenApp.Models;
using TekkenApp.Pages.Components.Base.Data;
using TekkenApp.Utilities;

namespace TekkenApp.Pages.Components.Base
{
    public partial class BaseComponent<TDataEntity, TNameEntity> : ComponentBase
        where TDataEntity : BaseDataEntity
        where TNameEntity : BaseNameEntity, new()
    {
        [Parameter]
        public AppType App { get; set; }

        [Parameter]
        public baseService<TDataEntity, TNameEntity> baseService { get; set; }

        [Inject]
        protected ILogger<EditComponent<TDataEntity, TNameEntity>> Logger { get; set; }

        [Inject]
        protected NavigationUtil navigationUtil { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        public TDataEntity BaseDataEntity { get; set; }
        public TNameEntity BaseNameEntity { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }
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
            var query = new Dictionary<string, string> { { "Code", Code.ToString() },{ "Language", Language_code } };
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
    }
}
