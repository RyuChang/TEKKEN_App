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
    public class MoveTypeController : BaseDefaultController
    {
        private IMoveTypeRepository _moveTypeRepository;

        public MoveTypeController(IStringLocalizer<SharedResource> sharedLocalizer,
            IMoveTypeRepository moveTypeRepository,
            ITranslateNameRepository translateNameRepository,
            IBaseDefaultRepository baseDefaultRepository) : base(sharedLocalizer, translateNameRepository, baseDefaultRepository)
        {
            _moveTypeRepository = moveTypeRepository;
            _translateNameRepository.SetTable(tableName);
            Initialize(TableName.moveType);
            _moveTypeRepository.SetTable(tableName);
        }

        

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.FormType = FormType.Create;
        //    BaseModel baseModel = _moveTypeRepository.GetRecentBaseModel();

        //    ViewBag.TitleDescription = "MoveType 추가 - 다음 필드들을 채워주세요.";
        //    ViewBag.SaveButtonText = "저장";

        //    return View(baseModel);
        //}
        /*
        [HttpPost]
        public async Task<IActionResult> Create(MoveType model)
        {
            _moveTypeRepository.Create(model); // 데이터 저장
            //// 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
            //TempData["Message"] = "데이터가 저장되었습니다.";
            return RedirectToAction("Index", model); // 저장 후 리스트 페이지로 이동
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
            MoveType moveType = _moveTypeRepository.GetMoveType_DetailById(id);


            return View(moveType);
        }

        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(MoveType moveType)
        {
            _moveTypeRepository.Update(moveType); // 데이터 저장
            return RedirectToAction("Index", moveType); // 저장 후 리스트 페이지로 이동

        }
        */
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
        //    TranslateName translateName = _translateNameRepository.GetTranslateNameByCode(code, TableName.moveType);

        //    return View(translateName);
        //}


        //[HttpPost]
        //public async Task<IActionResult> UpdateName(TranslateName translateName)
        //{
        //    translateName.TableName = TableName.moveType;
        //    _translateNameRepository.Update(translateName); // 데이터 저장
        //    return RedirectToAction("Index", translateName); // 저장 후 리스트 페이지로 이동
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(int code)
        //{
        //    _moveTypeRepository.Delete(code);
        //    return RedirectToAction("Index"); // 저장 후 리스트 페이지로 이동
        //}
    }
}
