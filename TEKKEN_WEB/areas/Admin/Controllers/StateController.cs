using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;


namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StateController : BaseStateGroupController
    {
        private IStateRepository _stateRepository;
        private IStateGroupRepository _stateGroupRepository;

        public StateController(IStringLocalizer<SharedResource> sharedLocalizer,
            IStateGroupRepository stateGroupRepository,
            IStateRepository stateRepository,
            ITranslateNameRepository translateNameRepository, 
            IBaseStateGroupRepository baseStateGroupRepository) : base(sharedLocalizer, translateNameRepository, baseStateGroupRepository, stateGroupRepository)
        {
            _stateGroupRepository = stateGroupRepository;
            _stateRepository = stateRepository;
            Initialize(TableName.State);
        }
        /*
        public IActionResult Index(int stateGroup_Code = 0)
        {
            ViewBag.tableName = tableName;
            ViewBag.TitleDescription = "State";
            //ViewBag.stateGroup_Code = stateGroup_Code;
            ViewBag.AllList = _stateRepository.GetAllStates(stateGroup_Code);
            //ViewBag.SelectAllStateGroups = _stateGroupRepository.GetStateGroupsSelectItems(stateGroup_Code);
            //ViewBag.SelectAllStateGroups = _stateGroupRepository.GetStateGroupsSelectItems(stateGroup_Code);
            //StateGroup stateGroup = _stateGroupRepository.GetStateGroup_DetailByStateGroup_code(stateGroup_Code.GetValueOrDefault());

            return View();
        }

        [HttpGet]
        public IActionResult Create(int stateGroup_code)
        {
            ViewBag.FormType = FormType.Create;
            State state = _stateRepository.GetState_LastDetailByStateGroup_code(stateGroup_code);
            //state.StateGroup_code=
            //ViewBag.SelectAllCharacters = _characterRepository.GetAllCharactersSelectItems();

            ViewBag.TitleDescription = "StateGroup 추가 - 다음 필드들을 채워주세요.";
            ViewBag.SaveButtonText = "저장";

            return View(state);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(State state)
        {
            _stateRepository.Create(state); // 데이터 저장
            //// 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
            //TempData["Message"] = "데이터가 저장되었습니다.";
            return RedirectToAction("Index", state); // 저장 후 리스트 페이지로 이동
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
            State state = _stateRepository.GetState_DetailById(id);  //==========

            return View(state);
        }*/
        /*
        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(State state)
        {
            _stateRepository.Update(state); // 데이터 저장
            return RedirectToAction("Index", state); // 저장 후 리스트 페이지로 이동

        }*/

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
        //    TranslateName translateName = _translateNameRepository.GetTranslateNameByCode(code, TableName.State);

        //    return View(translateName);
        //}


        //[HttpPost]
        //public async Task<IActionResult> UpdateName(TranslateName translateName)
        //{
        //    translateName.TableName = TableName.State;
        //    _translateNameRepository.Update(translateName); // 데이터 저장
        //    return RedirectToAction("Index", translateName); // 저장 후 리스트 페이지로 이동
        //}

        
        public JsonResult GetStates(int stateGroup_Code)
        {
            return Json(_stateRepository.GetStateByGroupSelectItems(stateGroup_Code));
        }
        /*
        [HttpGet]
        public async Task<IActionResult> Delete(int code)
        {
            _stateRepository.Delete(code);
            return RedirectToAction("Index"); // 저장 후 리스트 페이지로 이동
        }*/
    }
}
