using Admin.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace TEKKEN_WEB.areas.Admin.Controllers
{
    public class BaseDefaultController : BaseController
    {
        protected new IBaseDefaultRepository _baseDefaultRepository;
         public BaseDefaultController(IStringLocalizer<SharedResource> sharedLocalizer, 
             ITranslateNameRepository translateNameRepository,
             IBaseDefaultRepository baseDefaultRepository) : base(sharedLocalizer, translateNameRepository, baseDefaultRepository)
        {
            _baseDefaultRepository = baseDefaultRepository;
        }

        protected override void Initialize(TableName tableName)
        {
            this.tableName = tableName;
            _translateNameRepository.SetTable(tableName);
            _baseRepository.SetTable(tableName);
            _baseDefaultRepository.SetTable(tableName);
             baseType = BaseType.Default;
            TitleDescription = tableName.ToString();
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.tableName = tableName;
            ViewBag.TitleDescription = TitleDescription;
            ViewBag.AllList = _baseRepository.GetAllList(baseType,0);
            ViewBag.baseType = BaseType.Default.ToString();
            //ViewBag.AllList = _moveTypeRepository.GetAllMoveTypes();
            return View();
        }



        [HttpGet]
        public new IActionResult Create(int character_code)
        {
            ViewBag.FormType = FormType.Create;

            BaseModel baseModel = _baseDefaultRepository.GetRecentBaseModel(character_code);
            //ViewBag.character_code = character_code;
            ViewBag.readonly_Id = "readonly=\"readonly\"";
            ViewBag.TitleDescription = "추가 - 다음 필드들을 채워주세요.";
            ViewBag.SaveButtonText = "저장";
            return View(baseModel);
        }        /// <summary>
                 /// 데이터 저장, 수정, 답변 공통 메서드
                 /// </summary>
                 /// 

        [HttpPost]
        public async Task<IActionResult> Create(BaseModel model)
        {
            _baseDefaultRepository.Create(model); // 데이터 저장
            //// 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
            //TempData["Message"] = "데이터가 저장되었습니다.";
            return RedirectToAction("Index"); // 저장 후 리스트 페이지로 이동
        }

        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(BaseModel baseModel)
        {
            _baseDefaultRepository.Update(baseModel);
            return RedirectToAction("Index", baseModel); // 저장 후 리스트 페이지로 이동
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _baseRepository.Delete(id);
            return RedirectToAction("Index"); // 저장 후 리스트 페이지로 이동
        }


    }
}
