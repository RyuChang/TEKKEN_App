using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface IHitTypeService : IBaseService<HitType, HitType_name>
    {
        Task<HitType> UpdateHitTypeAsync(HitType hitType);
    }
}