using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;


namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StateGroupController : BaseDefaultController
    {
        private IStateGroupRepository _stateGroupRepository;

        public StateGroupController(IStringLocalizer<SharedResource> sharedLocalizer,
            IStateGroupRepository stateGroupRepository,
            ITranslateNameRepository translateNameRepository,
            IBaseDefaultRepository baseDefaultRepository) : base(sharedLocalizer, translateNameRepository, baseDefaultRepository)
        {
            _stateGroupRepository = stateGroupRepository;
            _translateNameRepository.SetTable(tableName);
            Initialize(TableName.StateGroup);
            _stateGroupRepository.SetTable(tableName);
            
        }

        //public IActionResult Index()
        //{
        //    ViewBag.tableName = tableName;
        //    ViewBag.TitleDescription = "StateGroup";
        //    ViewBag.AllList = _stateGroupRepository.GetStateGroups();
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewBag.FormType = FormType.Create;
        //    BaseModel baseModel = _stateGroupRepository.GetRecentBaseModel();
        //    //ViewBag.SelectAllCharacters = _characterRepository.GetAllCharactersSelectItems();

        //    ViewBag.TitleDescription = "StateGroup 추가 - 다음 필드들을 채워주세요.";
        //    ViewBag.SaveButtonText = "저장";

        //    return View(baseModel);
        //}

        /*
        [HttpPost]
        public async Task<IActionResult> Create(StateGroup stateGroup)
        {
            _stateGroupRepository.Create(stateGroup); // 데이터 저장
            //// 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
            //TempData["Message"] = "데이터가 저장되었습니다.";
            return RedirectToAction("Index", stateGroup); // 저장 후 리스트 페이지로 이동
        }

        /// <summary>
        /// 기술 하위 분류 수정 폼
        /// <param name="id">id</param>
        /// </summary>
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.TitleDescription = "기술 하위 분류 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";
            StateGroup stateGroup = _stateGroupRepository.GetStateGroup_DetailById(id);
            
            return View(stateGroup);
        }

        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(StateGroup stateGroup)
        {
            _stateGroupRepository.Update(stateGroup); // 데이터 저장
            return RedirectToAction("Index", stateGroup); // 저장 후 리스트 페이지로 이동

        }

        ///// <summary>
        ///// 기술 하위 분류 수정 폼
        ///// <param name="code">code</param>
        ///// </summary>
        //[HttpGet]
        //public IActionResult UpdateName(int code)
        //{
        //    ViewBag.FormType = FormType.Update;
        //    ViewBag.TitleDescription = "기술 하위 분류명 수정 - 아래 항목을 수정하세요.";
        //    ViewBag.SaveButtonText = "수정";
        //    TranslateName translateName = _translateNameRepository.GetTranslateNameByCode(code, TableName.StateGroup);

        //    return View(translateName);
        //}


        //[HttpPost]
        //public async Task<IActionResult> UpdateName(TranslateName translateName)
        //{
        //    translateName.TableName = TableName.StateGroup;
        //    _translateNameRepository.Update(translateName); // 데이터 저장
        //    return RedirectToAction("Index", translateName); // 저장 후 리스트 페이지로 이동
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int code)
        {
            _stateGroupRepository.Delete(code);
            return RedirectToAction("Index"); // 저장 후 리스트 페이지로 이동
        }
        */
    }
}
