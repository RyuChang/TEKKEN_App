using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;

namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoveSubTypeController : BaseCharacterController
    {
        private IMoveSubTypeRepository _moveSubTypeRepository;

        public MoveSubTypeController(
            IStringLocalizer<SharedResource> sharedLocalizer,
            ICharacterRepository characterRepository,
            IMoveSubTypeRepository moveSubTypeRepository,
            ITranslateNameRepository translateNameRepository) : base(sharedLocalizer, translateNameRepository, moveSubTypeRepository, characterRepository)
        {
            _moveSubTypeRepository = moveSubTypeRepository;
            Initialize(TableName.moveSubType);
            _moveSubTypeRepository.SetTable(tableName);
        }
    }
}
