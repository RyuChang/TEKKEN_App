using System;
using System.Collections.Generic;
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

        public void MoveTo(AppType appType, ActionType actionType = ActionType.List, int id = 0, Dictionary<string, string> param = null)
        {
            //string action = (actionType == ActionType.List) ? "" : $"/{GetAction(actionType)}";

            string idUri = (id > 0) ? $"/{id}" : "";
            string url = $"{GetUrl(appType)}{GetActionUrl(actionType)}{idUri}";

            if (param == null)
            {
                navigationManager.NavigateTo(url);
            }
            else
            {
                navigationManager.NavigateTo(QueryHelpers.AddQueryString(url, param));
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

        private string GetUrl(AppType appType) =>
            appType switch
            {
                AppType.HitTypes => "HitTypes",
                AppType.StateGroups => "StateGroups",
                AppType.MoveText => "MoveText",
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

