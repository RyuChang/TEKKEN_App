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
    public class BaseStateGroupController : BaseController
    {
        protected IBaseStateGroupRepository _baseStateGroupRepository;
        protected IStateGroupRepository _StateGroupRepository;
        public BaseStateGroupController(
            IStringLocalizer<SharedResource> sharedLocalizer,
            ITranslateNameRepository translateNameRepository,
            IBaseStateGroupRepository baseStateGroupRepository,
            IStateGroupRepository stateGroupRepository) : base(sharedLocalizer, translateNameRepository, baseStateGroupRepository)
        {
            _baseStateGroupRepository = baseStateGroupRepository;
            _StateGroupRepository = stateGroupRepository;
        }

        protected override void Initialize(TableName tableName)
        {
            this.tableName = tableName;
            _translateNameRepository.SetTable(tableName);
            _baseRepository.SetTable(tableName);
            _baseStateGroupRepository.SetTable(tableName);
            baseType = BaseType.StateGroup_Code;
            TitleDescription = tableName.ToString();
        }

        [HttpGet]
        public IActionResult Index(int stateGroup_Code = 80000001)
        {
            ViewBag.tableName = tableName;
            ViewBag.TitleDescription = TitleDescription;
            ViewBag.AllList = _baseRepository.GetAllList(baseType, stateGroup_Code);
            ViewBag.baseType = baseType;
            ViewBag.stateGroup_Code = stateGroup_Code;
            ViewBag.SelectAllStateGroups = _StateGroupRepository.GetStateGroupsSelectItems(stateGroup_Code);

            //ViewBag.AllList = _moveTypeRepository.GetAllMoveTypes();
            return View();
        }

        [HttpGet]
        public new IActionResult Create(int stateGroup_code)
        {
            ViewBag.FormType = FormType.Create;
            ViewBag.baseType = baseType;

            BaseModel baseModel = _baseStateGroupRepository.GetRecentBaseModel(stateGroup_code);
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
            _baseStateGroupRepository.Create(model); // 데이터 저장
            //// 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
            //TempData["Message"] = "데이터가 저장되었습니다.";
            return RedirectToAction("Index", model.StateGroup_code); // 저장 후 리스트 페이지로 이동
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(BaseModel baseModel)
        {
            _baseStateGroupRepository.Update(baseModel);
            return RedirectToAction("Index", baseModel); // 저장 후 리스트 페이지로 이동
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int stateGroup_code)
        {
            _baseRepository.Delete(id);
            return RedirectToAction("Index", new { stateGroup_code = stateGroup_code }); // 저장 후 리스트 페이지로 이동
        }
    }
}
