using Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace TEKKEN_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class TekkenVersionController : Controller
    {
        private readonly IStringLocalizer<CommonController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private ITekkenVersionRepository _repository;

        public TekkenVersionController(IStringLocalizer<CommonController> localizer,
                       IStringLocalizer<SharedResource> sharedLocalizer, ITekkenVersionRepository repository)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var list = _repository.GetAllVersions();
            return View(list);

        }


        /// <summary>
        /// 버전의 상세 보기 페이지
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns> 
        public async Task<IActionResult> Details(float version)
        {
            TekkenVersion tekkenVersion = _repository.GetVersionDetail(version);
            return View(tekkenVersion);
        }

        /// <summary>
        /// 버전 수정 폼
        /// </summary>
        [HttpGet]
        public IActionResult Update(float version)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.TitleDescription = "버전 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            TekkenVersion tekkenVersion = _repository.GetVersionDetail(version);
            return View(tekkenVersion);
        }

        /// <summary>
        /// 버전 수정 저장
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(float version, TekkenVersion model)
        {
            ViewBag.FormType = FormType.Update;
            ViewBag.TitleDescription = "버전 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            TekkenVersion tekkenVersion = _repository.GetVersionDetail(version);

            tekkenVersion.Season = model.Season;
            tekkenVersion.UpdateDate = model.UpdateDate;

            int result = _repository.UpdateVersion(model, version); // 데이터베이스에 수정 적용

            if (result > 0)
            {
                TempData["Message"] = "수정되었습니다.";
                return RedirectToAction("Details", new { Version = version });
            }
            else
            {
                //ViewBag.ErrorMessage =
                //    "업데이트가 되지 않았습니다. 암호를 확인하세요.";
                //return View(note);
                return View(model);
            }
        }


        /// <summary>
        /// 버전 삭제 폼
        /// </summary>
        [HttpGet]
        public IActionResult Remove(float version)
        {
            ViewBag.Version = version;
            return View();
        }

        /// <summary>
        /// 버전 삭제 처리
        /// </summary>
        [HttpPost]
        public IActionResult Remove(float version, int tmp)
        {
            TekkenVersion tekkenVersion = _repository.GetVersionDetail(version);

            if (_repository.Remove(version) > 0)
            {
                TempData["Message"] = "데이터가 삭제되었습니다.";

                //// 학습 목적으로 삭제 후의 이동 페이지를 2군데 중 하나로 분기
                //if (DateTime.Now.Second % 2 == 0)
                //{
                //    //[a] 삭제 후 특정 뷰 페이지로 이동
                //    return RedirectToAction("DeleteCompleted");
                //}
                //else
                {
                    //[b] 삭제 후 Index 페이지로 이동
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Message = "삭제되지 않았습니다. 비밀번호를 확인하세요.";
            }

            ViewBag.Version = version;
            return View(tekkenVersion);
        }

        /// <summary>
        /// 게시판 삭제 완료 후 추가적인 처리할 때 페이지
        /// </summary>
        public IActionResult DeleteCompleted()
        {
            return View();
        }


        public IActionResult Version()
        {
            return View(_repository.GetVersion());
        }
    }
}
