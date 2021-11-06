using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace TEKKEN_WEB.areas.Admin.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected ITranslateNameRepository _translateNameRepository;
        protected IBaseRepository _baseRepository;
        protected TableName tableName;
        protected string TitleDescription;
        protected BaseType baseType;


        public BaseController(IStringLocalizer<SharedResource> sharedLocalizer, ITranslateNameRepository translateNameRepository, IBaseRepository baseRepository)
        {
            _sharedLocalizer = sharedLocalizer;
            _translateNameRepository = translateNameRepository;
            _baseRepository = baseRepository;
        }

        abstract protected void Initialize(TableName tableName);





        /*
        [HttpGet]
        public IActionResult Create(int character_code)
        {
            ViewBag.FormType = FormType.Create;

            BaseModel baseModel = _baseRepository.GetRecentBaseModelByCharacter_code(character_code);
            //ViewBag.character_code = character_code;
            ViewBag.readonly_Id = "readonly=\"readonly\"";
            ViewBag.TitleDescription = "추가 - 다음 필드들을 채워주세요.";
            ViewBag.SaveButtonText = "저장";

            return View(baseModel);
        }*/



        /// <summary>
        /// 기술 하위 분류 수정 폼
        /// <param name="id">id</param>
        /// </summary>
        [HttpGet]
        public virtual IActionResult Update(int id)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.Readonly = "";
            //ViewBag.Readonly= "@readonly=\"readonly\"";
            ViewBag.TitleDescription = "기술 하위 분류 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            BaseModel baseModel = _baseRepository.GetDetailBaseModelById(id);
            //ViewBag.SelectAllCharacters = _characterRepository.GetAllCharactersSelectItems(moveSubType.Character_code);
            ViewBag.baseType = baseType;
            //return View(moveSubType);
            return View(baseModel);
        }

        /// <summary>
        /// 
        /// <param name="code">code</param>
        /// </summary>
        [HttpGet]
        public virtual IActionResult UpdateName(int id)
        {
            ViewBag.TitleDescription = tableName;
            ViewBag.FormType = FormType.Update;
            ViewBag.SaveButtonText = "수정";
            TranslateName translateName = _translateNameRepository.GetTranslateNameById(id, tableName);
            //translateName.TableName = tableName;
            return View(translateName);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateName(TranslateName translateName)
        {
            translateName.TableName = tableName;
            //_translateNameRepository.Update(translateName); // 데이터 저장
            _translateNameRepository.Merge(translateName); // 데이터 저장
            return RedirectToAction("Index", new { movePosition = translateName.Code}); // 저장 후 리스트 페이지로 이동
        }
        

        /// <summary>
        /// 
        /// <param name="code">code</param>
        /// </summary>
        [HttpGet]
        public IActionResult UpdateAllName(int id)
        {

            BaseModel baseModel = _translateNameRepository.GetBaseModelById(id);
            ViewBag.baseModel = baseModel;
            ViewBag.TitleDescription = tableName;
            ViewBag.FormType = FormType.Update;
            ViewBag.SaveButtonText = "수정";
            ViewBag.translateNames = _translateNameRepository.GetAllTranslateNamesByCode(baseModel.Code);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAllName(TranslateName[] translateName)
        {
            for (int i = 0; i < translateName.Length; i++)
            {

                if (translateName[i].Change == true)
                {
                    translateName[i].TableName = tableName;
                    _translateNameRepository.Merge(translateName[i]);
                    //if (translateName[i].Id == 0)
                    //{
                    //    _translateNameRepository.Create(translateName[i]); // 데이터 저장
                    //}
                    //else
                    //{
                    //    _translateNameRepository.Update(translateName[i]); // 데이터 저장
                    //}

                }
            }

            return RedirectToAction("Index", new { movePosition = translateName[0].Code }); // 저장 후 리스트 페이지로 이동
        }


        public JsonResult GetJsonSelectListByStateGroup(int stateGroup_code, int code = 0)
        {

            return Json(_baseRepository.GetSelectListByStateGroup(stateGroup_code, code));
        }
    }
}
