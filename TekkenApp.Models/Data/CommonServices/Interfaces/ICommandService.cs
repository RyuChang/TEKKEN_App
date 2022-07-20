using System.Threading.Tasks;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public interface ICommandService : IBaseNameService<Command, Command_name>
    {
        public string RawCommand { get; set; }

        /// <summary>
        /// RawCommand 구분자 제거
        /// </summary>
        Task SetCommand();
        
        /// <summary>
        /// State를 입력받은 형식에 맞게 변환하여 리턴
        /// </summary>
        /// <param name="stateGroupType"></param>
        /// <param name="stateCode"></param>
        /// <param name="dataCode"></param>
        /// <returns>State추가 된 RawCommand</returns>
        string AddState(string stateGroupType, int stateCode, int dataCode = 0);

        /// <summary>
        /// 언어에 맞게 변환된 TAG 커맨드 리턴
        /// </summary>
        /// <param name="rawCommand"></param>
        /// <param name="language_code"></param>
        /// <returns>이미지로 변환된 TAG 커맨드</returns>
        Task<string> TransCommand(string rawCommand, string language_code);

        string GetStateGroupType(int stateGroupCode);
    }
}