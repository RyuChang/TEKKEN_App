using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB.areas.Admin.Controllers;
using TEKKEN_WEB.Enums;


namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HitTypeController : BaseDefaultController
    {
        private IHitTypeRepository _hitTypeRepository;

        public HitTypeController(IStringLocalizer<SharedResource> sharedLocalizer,
                                IHitTypeRepository hitTypeRepository,
                                ITranslateNameRepository translateNameRepository,
                                IBaseDefaultRepository baseDefaultRepository) : base(sharedLocalizer, translateNameRepository, baseDefaultRepository)
        {
            _hitTypeRepository = hitTypeRepository;
            _hitTypeRepository.SetTable(tableName);
            Initialize(TableName.HitType);
        }
    }
}
