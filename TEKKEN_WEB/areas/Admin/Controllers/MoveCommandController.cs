using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.COMMON.Command;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoveCommandController : BaseCharacterController
    {
        private IMoveCommandRepository _moveCommandRepository;
        private IStateRepository _stateRepository;
        private IStateGroupRepository _stateGroupRepository;

        public MoveCommandController(
            IStringLocalizer<SharedResource> sharedLocalizer,
            ICharacterRepository characterRepository,
            IMoveCommandRepository moveCommandRepository,
            IStateRepository stateRepository,
            IStateGroupRepository stateGroupRepository,
            ITranslateNameRepository translateNameRepository) : base(sharedLocalizer, translateNameRepository, moveCommandRepository, characterRepository)
        {
            _moveCommandRepository = moveCommandRepository;
            _stateRepository = stateRepository;
            _stateGroupRepository = stateGroupRepository;

            Initialize(TableName.move);
            TitleDescription = TableName.move_Command.ToString();
            _translateNameRepository.SetTable(TableName.move, TableName.move_Command);
            _moveCommandRepository.SetTable(tableName);
        }

        [HttpGet]
        public override IActionResult Index(int character_code = 1, int movePosition = -1)
        {
            ViewBag.tableName = tableName;
            ViewBag.TitleDescription = TitleDescription;
            ViewBag.AllList = _moveCommandRepository.GetAllList(baseType, character_code);
            ViewBag.baseType = baseType;
            ViewBag.character_code = character_code;
            ViewBag.movePosition = movePosition;
            ViewBag.SelectAllCharacters = _characterRepository.GetAllCharactersSelectItems(character_code);
            ViewBag.subTableName = TableName.move_Command;
            //ViewBag.AllList = _moveTypeRepository.GetAllMoveTypes();
            //ViewBag.SelectAllStateGroups = _stateGroupRepository.GetStateGroupsSelectItems(0);
            //ViewBag.SelectAllStateGroups = _stateGroupRepository.GetStateGroupsSelectItems();


            return View();
        }

        [HttpGet]
        public IActionResult UpdateMoveCommand(int id, int character_code, int stateGroup_code)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.readonly_Id = "readonly=\"readonly\"";
            ViewBag.TitleDescription = "기술명 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            ViewBag.moveCommand = _moveCommandRepository.GetMoveCommandById(id);
            //ViewBag.tableName = TableName.move;
            //ViewBag.subTableName = TableName.move_Command;
            ViewBag.SelectStates = _stateRepository.GetSelectList();
            ViewBag.SelectStateGroups = _stateGroupRepository.GetSelectList(stateGroup_code);

            return View();
        }

        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateMoveCommand(MoveCommand[] moveCommand)
        {
            _moveCommandRepository.Merge(moveCommand[0]); // 데이터 저장
            TranslateName[] translateName = new TranslateName[moveCommand.Length];
            for (int i = 0; i < moveCommand.Length; i++)
            {
                translateName[i] = new TranslateName(
                    TableName.move_Command,
                    moveCommand[i].Id,
                    moveCommand[0].Code,
                    moveCommand[i].Name,
                    moveCommand[i].Language_Code,
                    moveCommand[i].Change
                    );
            }

            UpdateAllName(translateName);
            //UpdateAllName()
            return RedirectToAction("Index", new { character_code = moveCommand[0].Character_code, movePosition = moveCommand[0].Code }); // 저장 후 리스트 페이지로 이동
        }

        /// <summary>
        /// 기술 하위 분류 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateMoveCommandName(MoveCommand[] moveCommand)
        {
            _moveCommandRepository.UpdateMoveCommandName(moveCommand); // 데이터 저장
            return RedirectToAction("Index", moveCommand); // 저장 후 리스트 페이지로 이동

        }

        /// <summary>
        /// 기술 하위 분류 수정 폼
        /// <param name="code">code</param>
        /// </summary>
        [HttpGet]
        public override IActionResult UpdateName(int id)
        {
            ViewBag.TitleDescription = tableName;
            ViewBag.FormType = FormType.Update;
            ViewBag.SaveButtonText = "수정";

            TranslateName translateName = _translateNameRepository.GetTranslateNameById(id, TableName.move);

            return View(translateName);
        }

        [HttpPost]
        public String TransCommand(string command, string language_code)
        {
            return new TekkenCommand(command, language_code).GetResultcommand();
        }

        [HttpGet]
        public JsonResult GetKeyMap()
        {
            return Json(new TekkenCommand().GetKeyMaps());
        }

    }
}
