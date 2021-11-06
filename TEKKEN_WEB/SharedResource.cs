using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEKKEN_WEB
{
    public class SharedResource
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public int CurrentLanguage { get; set; }

        SharedResource(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
        }
        public String GetLanguage() {
            return _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
        }
    }
}
