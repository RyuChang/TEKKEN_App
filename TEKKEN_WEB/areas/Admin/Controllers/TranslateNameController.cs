using Admin.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class TranslateNameController : Controller
    {
        private readonly IStringLocalizer<CommonController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private ITranslateNameRepository _translateNameRepository;

        public TranslateNameController(IStringLocalizer<CommonController> localizer,
                       IStringLocalizer<SharedResource> sharedLocalizer, ITranslateNameRepository translateNameRepository) 
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _translateNameRepository = translateNameRepository;
        }



        /// <summary>
        /// 기술 하위 분류 수정 폼
        /// <param name="code">code</param>
        /// </summary>
        [HttpGet]
        public IActionResult CreateName(TableName tableName, string Language_Code, int code, string description)
        {
            TranslateName translateName = new TranslateName();
            translateName.TableName = tableName;
            translateName.Language_Code = Language_Code;
            translateName.Code = code;
            translateName.Description = description;
            ViewBag.FormType = FormType.Create;
            ViewBag.TitleDescription = "기술  수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            return View(translateName);
        }

        [HttpPost]
        public async Task<IActionResult> CreateName(TranslateName translateName)
        {
            _translateNameRepository.Create(translateName);
            return RedirectToAction("Index",""); // 저장 후 리스트 페이지로 이동
        }

    }
}
