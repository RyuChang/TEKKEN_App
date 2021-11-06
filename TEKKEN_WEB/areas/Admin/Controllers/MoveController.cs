using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;


namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoveController : BaseCharacterController
    {
        private IMoveRepository _moveRepository;

        public MoveController(
            IStringLocalizer<SharedResource> sharedLocalizer,
                                ICharacterRepository characterRepository,
                                IMoveRepository moveRepository,
                                ITranslateNameRepository translateNameRepository) : base(sharedLocalizer, translateNameRepository, moveRepository, characterRepository)
        {
            _moveRepository = moveRepository;
            Initialize(TableName.move);

        }

        /// <summary>
        /// 기술목록 페이지
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectMove(string stateType, int character_code)
        {
            ViewBag.stateType = stateType;
            ViewBag.SelectMoves = _moveRepository.GetSelectListByCharacter(character_code);
            return PartialView("_SelectMoveModalPartial");
        }

    }
}
