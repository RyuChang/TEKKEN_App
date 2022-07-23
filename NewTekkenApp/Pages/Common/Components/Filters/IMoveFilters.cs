
namespace NewTekkenApp.Pages.Common.Components.Filters
{
    public interface IMoveFilters
    {


        /// <summary>
        /// 다중요청 출돌 처리 방지 상태값
        /// </summary>
        bool Loading { get; set; }
        /// <summary>
        /// 페이징 상태 처리 객체
        /// </summary>
        IPageHelper PageHelper { get; set; }

        /// <summary>
        /// 다중요청 출돌 처리 방지 상태값
        /// </summary>
        string? FilterText { get; set; }

        /// <summary>
        /// 필터링 커맨드
        /// </summary>
        string? FilterCommand { get; set; }

        /// <summary>
        /// True 내림차순 False 오름차순
        /// </summary>
        bool SortAscending { get; set; }

        /// <summary>
        /// 정렬 컬럼
        /// </summary>
        MoveFilterColumns SortColumn { get; set; }

        /// <summary>
        /// 필터링 컬럼
        /// </summary>
        MoveFilterColumns FilterColumn { get; set; }
    }
}
