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
    public class MoveDataController : BaseCharacterController
    {
        private IMoveDataRepository _moveDataRepository;
        private IHitTypeRepository _hitTypeRepository;
        private IMoveTypeRepository _moveTypeRepository;
        private IMoveSubTypeRepository _moveSubTypeRepository;

        public MoveDataController(
            IStringLocalizer<SharedResource> sharedLocalizer,
            ICharacterRepository characterRepository,
            IMoveDataRepository moveDataRepository,
            IMoveTypeRepository moveTypeRepository,
            IMoveSubTypeRepository moveSubTypeRepository,
        ITranslateNameRepository translateNameRepository,
            IHitTypeRepository hitTypeRepository) : base(sharedLocalizer, translateNameRepository, moveDataRepository, characterRepository)
        {
            _moveDataRepository = moveDataRepository;
            _hitTypeRepository = hitTypeRepository;
            _moveTypeRepository = moveTypeRepository;
            _moveSubTypeRepository = moveSubTypeRepository;
            Initialize(TableName.move);
            TitleDescription = TableName.move_Data.ToString();
            _translateNameRepository.SetTable(TableName.move, TableName.move_Data);
            _moveDataRepository.SetTable(tableName);
        }

        [HttpGet]
        public override IActionResult Index(int character_code = 1, int movePosition = -1)
        {
            ViewBag.tableName = tableName;
            ViewBag.TitleDescription = TitleDescription;
            ViewBag.AllList = _moveDataRepository.GetAllList(baseType, character_code);
            ViewBag.baseType = baseType;
            ViewBag.character_code = character_code;
            ViewBag.movePosition = movePosition;
            ViewBag.SelectAllCharacters = _characterRepository.GetAllCharactersSelectItems(character_code);
            ViewBag.subTableName = TableName.move_Data;

            return View();
        }

        [HttpGet]
        public IActionResult UpdateMoveData(int id, int character_code)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.readonly_Id = "readonly=\"readonly\"";
            ViewBag.TitleDescription = "기술명 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "저장";
            //BaseModel baseModel = _moveDataRepository.GetDetailBaseModelById (id);
            MoveData moveData = _moveDataRepository.GetMoveDataById(id);
            ViewBag.tableName = TableName.move;
            ViewBag.subTableName = TableName.move_Data;
            //_hitTypeRepository.SetTable(TableName.HitType);
            ViewBag.SelectHitTypes = _hitTypeRepository.GetSelectList();
            ViewBag.SelectMoveSubTypes = _moveSubTypeRepository.GetSelectListByCharacter(character_code);
            ViewBag.SelectMoveTypes = _moveTypeRepository.GetSelectList(character_code);
            return View(moveData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMoveData(MoveData moveData)
        {
            _moveDataRepository.Merge(moveData);

            return RedirectToAction("Index", new { character_code = moveData.Character_code, movePosition = moveData.Code}); // 저장 후 리스트 페이지로 이동
        }
    }
}
