using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;


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


        public JsonResult GetStates(int stateGroup_Code)
        {
            return Json(_stateRepository.GetStateByGroupSelectItems(stateGroup_Code));
        }
    }
}
