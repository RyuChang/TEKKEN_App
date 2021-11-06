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
    public class BaseCharacterController : BaseController
    {
        protected IBaseCharacterRepository _baseCharacterRepository;
        protected ICharacterRepository _characterRepository;
        public BaseCharacterController(
            IStringLocalizer<SharedResource> sharedLocalizer,
            ITranslateNameRepository translateNameRepository,
            IBaseCharacterRepository baseCharacterRepository,
            ICharacterRepository characterRepository) : base(sharedLocalizer, translateNameRepository, baseCharacterRepository)
        {
            _baseCharacterRepository = baseCharacterRepository;
            _characterRepository = characterRepository;
        }

        protected override void Initialize(TableName tableName)
        {
            this.tableName = tableName;
            _translateNameRepository.SetTable(tableName);
            _baseRepository.SetTable(tableName);
            _baseCharacterRepository.SetTable(tableName);
            baseType = BaseType.Character_Code;
            TitleDescription = tableName.ToString();
        }

        [HttpGet]
        public virtual IActionResult Index(int character_code = 18, int movePosition = -1)
        {
            ViewBag.tableName = tableName;
            ViewBag.TitleDescription = TitleDescription;
            ViewBag.AllList = _baseRepository.GetAllList(baseType, character_code);
            ViewBag.baseType = baseType;
            ViewBag.character_code = character_code;
            ViewBag.movePosition = movePosition;
            ViewBag.SelectAllCharacters = _characterRepository.GetAllCharactersSelectItems(character_code);

            //ViewBag.AllList = _moveTypeRepository.GetAllMoveTypes();
            return View();
        }


        [HttpGet]
        public virtual IActionResult Create(int character_code)
        {
            ViewBag.FormType = FormType.Create;

            BaseModel baseModel = _baseCharacterRepository.GetRecentBaseModel(character_code);
            //ViewBag.character_code = character_code;
            ViewBag.readonly_Id = "readonly=\"readonly\"";
            ViewBag.TitleDescription = "추가 - 다음 필드들을 채워주세요.";
            ViewBag.SaveButtonText = "저장";
            ViewBag.baseType = baseType;

            return View(baseModel);
        }        /// <summary>
                 /// 데이터 저장, 수정, 답변 공통 메서드
                 /// </summary>
                 /// 

        [HttpPost]
        public virtual async Task<IActionResult> Create(BaseModel baseModel)
        {
            _baseCharacterRepository.Create(baseModel); // 데이터 저장
                                                        //// 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
                                                        //TempData["Message"] = "데이터가 저장되었습니다.";
            return RedirectToAction("Index", new { character_code = baseModel.Character_code }); // 저장 후 리스트 페이지로 이동
        }

        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>=
        [HttpPost]
        public virtual async Task<IActionResult> Update(BaseModel baseModel)
        {
            _baseCharacterRepository.Update(baseModel);
            return RedirectToAction("Index", new
            {
                character_code = baseModel.Character_code,
                movePosition = baseModel.Code

            }); // 저장 후 리스트 페이지로 이동
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int character_code)
        {
            _baseRepository.Delete(id);
            return RedirectToAction("Index", new { character_code = character_code }); // 저장 후 리스트 페이지로 이동
        }
    }
}
