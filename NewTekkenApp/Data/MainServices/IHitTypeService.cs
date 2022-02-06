using TekkenApp.Models;

namespace NewTekkenApp.Data
{
    public interface IHitTypeService : IBaseService<HitType, HitType_name>
    {
        Task<HitType> UpdateHitTypeAsync(HitType hitType);
    }
}