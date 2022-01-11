using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using TekkenApp.Models;

namespace NewTekkenApp.Utilities
{
    public class NavigationUtil : ComponentBase
    {
        private readonly NavigationManager navigationManager;

        public NavigationUtil(NavigationManager navManager)
        {
            navigationManager = navManager;
        }

        public void MoveTo(AppType appType)
        {
            MoveTo(appType);
        }

        public void MoveTo(AppType appType, ActionType actionType, string paramName, string paramValue)
        {
            Dictionary<string, string> param = new();
            param.Add(paramName, paramValue);
            MoveTo(appType, actionType, 0, param);
        }


        public void MoveTo(AppType appType, ActionType actionType = ActionType.List, int id = 0, Dictionary<string, string> param = default!)
        {
            //string action = (actionType == ActionType.List) ? "" : $"/{GetAction(actionType)}";

            string idUri = (id > 0) ? $"/{id}" : "";
            string url = $"{GetUrl(appType)}{GetActionUrl(actionType)}{idUri}";

            if (param != null)
            {
                navigationManager.NavigateTo(QueryHelpers.AddQueryString(url, param));
            }
            else
            {
                navigationManager.NavigateTo(url);
            }
        }

        private string GetAction(ActionType actionType) =>
            actionType.ToString();

        //private string GetAction(ActionType actionType) =>
        //    actionType switch
        //    {
        //        ActionType.Create => "Create",
        //        ActionType.Detail=> "Detail",
        //        ActionType.Create_name => "Create_name",
        //        _ => ""
        //    };

        public string GetUrl(AppType appType) =>
            appType switch
            {
                AppType.HitTypes => "HitTypes",
                AppType.States => "States",
                AppType.StateGroups => "StateGroups",
                AppType.MoveTexts => "MoveTexts",
                AppType.MoveTypes => "MoveTypes",
                AppType.MoveSubTypes => "MoveSubTypes",
                AppType.MoveDatas => "MoveDatas",
                AppType.Moves => "Moves",
                AppType.MoveCommands => "MoveCommands",
                _ => ""
            };
        private string GetActionUrl(ActionType actionType) =>
            actionType switch
            {
                ActionType.List => "",
                _ => $"/{actionType.ToString()}"
            };


        public Dictionary<string, StringValues> GetQueryStrings()
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);

            return queryStrings;
        }


    }
}

