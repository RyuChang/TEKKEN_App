using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CharacterController : Controller
    {
        private readonly IStringLocalizer<CommonController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private ICharacterRepository _repository;

        public CharacterController(IStringLocalizer<CommonController> localizer,
                       IStringLocalizer<SharedResource> sharedLocalizer, ICharacterRepository repository)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var list = _repository.GetAllCharacters();
            return View(list);
        }

        /// <summary>
        /// 캐릭터 상세 보기 페이지
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>  
        public async Task<IActionResult> Details(int character_code)
        {
            Character character = _repository.GetCharacterDetail(character_code);
            return View(character);
        }

        /// <summary>
        /// 캐릭터 수정 폼
        /// </summary>
        [HttpGet]
        public IActionResult Update(int character_code)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.TitleDescription = "캐릭터 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            Character character = _repository.GetCharacterDetail(character_code);
            return View(character);
        }

        /// <summary>
        /// 캐릭터 수정 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(int character_code, Character model)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.TitleDescription = "캐릭터 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            Character character = _repository.GetCharacterDetail(character_code);
            character.Code = model.Code;
            character.Season = model.Season;
            character.Name = model.Name;
            character.FullName = model.FullName;

            int result = _repository.UpdateCharacter(model, character_code); // 데이터베이스에 수정 적용

            if (result > 0)
            {
                TempData["Message"] = "수정되었습니다.";
                return RedirectToAction("Details", new { character_code = character_code });
            }
            else
            {
                //ViewBag.ErrorMessage =
                //    "업데이트가 되지 않았습니다. 암호를 확인하세요.";
                //return View(note);
                return View(model);
            }
        }

    }
}
