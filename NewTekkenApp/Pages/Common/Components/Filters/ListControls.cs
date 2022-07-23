namespace NewTekkenApp.Pages.Common.Components.Filters
{
    public class ListControls : IMoveFilters
    {
        public ListControls(IPageHelper pageHelper)
        {
            PageHelper = pageHelper;
        }

        /// <summary>
        /// 다중요청 출돌 처리 방지 상태값
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// 페이징 상태 처리 객체
        /// </summary>
        public IPageHelper PageHelper { get; set; }

        /// <summary>
        /// 필터링 텍스트
        /// </summary>
        public string? FilterText { get; set; }

        /// <summary>
        /// 필터링 커맨드
        /// </summary>
        public string? FilterCommand { get; set; }

        /// <summary>
        /// True 내림차순 False 오름차순
        /// </summary>
        public bool SortAscending { get; set; } = true;

        /// <summary>
        /// 정렬 컬럼
        /// </summary>
        public MoveFilterColumns SortColumn { get; set; }
            = MoveFilterColumns.Number;

        /// <summary>
        /// 필터링 컬럼
        /// </summary>
        public MoveFilterColumns FilterColumn { get; set; } = MoveFilterColumns.Title;
    }
}

