namespace TekkenApp.Data
{
    public interface ICommanderMapperService
    {
        /// <summary>
        /// 키보드 입력값 매핑
        /// </summary>
        /// <param name="formedKey">입력받은 키</param>
        /// <returns>매핑된 키 값</returns>
        string MapKey(string formedKey);

        /// <summary>
        /// state 입력값 매핑
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="languageCode"></param>
        /// <returns>매핑된 state값</returns>
        string MapState(string stateCode, string languageCode);
    }
}
