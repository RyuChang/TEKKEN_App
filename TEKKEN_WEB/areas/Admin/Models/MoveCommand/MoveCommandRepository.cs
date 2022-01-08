using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public class MoveCommandRepository : BaseCharacterRepository, IMoveCommandRepository
    {
        private ITranslateNameRepository _translateNameRepository;

        public MoveCommandRepository(IConfiguration config, ILogger<MoveCommandRepository> logger,
            IHttpContextAccessor httpContextAccessor, ITranslateNameRepository translateNameRepository) : base(config, logger, httpContextAccessor)
        {
            _logger = logger;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _translateNameRepository = translateNameRepository;
        }

        public List<MoveCommand> GetAllList(BaseType baseType, int character_code) => con.Query<MoveCommand>("[MoveCommand_GetAllList]", new { character_code = character_code }, commandType: CommandType.StoredProcedure).ToList();

        public List<MoveCommand> GetMoveCommandById(int id) => con.Query<MoveCommand>("[MoveCommand_GetMoveCommandById]", new { id = id }, commandType: CommandType.StoredProcedure).ToList();



        public void Merge(MoveCommand moveCommand)
        {
            _logger.LogInformation("데이터 입력");
            try
            {
                SaveOrUpdate(moveCommand, FormType.Create);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 입력 에러: " + ex);
            }
        }

        /// <summary>
        /// 데이터 저장, 수정, 답변 공통 메서드
        /// </summary>
        public int SaveOrUpdate(MoveCommand moveCommand, FormType formType)
        {
            int result = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@Id", value: moveCommand.Id, dbType: DbType.Int32);
            parameters.Add("@code", value: moveCommand.Code, dbType: DbType.Int32);
            parameters.Add("@Command", value: moveCommand.Command, dbType: DbType.String);


            result = con.Execute("[moveCommand_Merge]", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }




        public void UpdateMoveCommandName(MoveCommand[] moveCommand)
        {
            _logger.LogInformation("데이터 수정");
            try
            {
                foreach (MoveCommand mv in moveCommand)
                {
                    if (mv.Change == true)
                    {
                        TranslateName translateName = new TranslateName();
                        translateName.Id = mv.Id;
                        translateName.TableName = TableName.move_Command;
                        translateName.Name = mv.Name;
                        _translateNameRepository.Update(translateName); // 데이터 저장
                        //new TekkenCommand(move.Command, "en").CreateForeignCommand(move.Code);
                        //new TekkenCommand(move.Command, "ko").CreateForeignCommand(move.Code);

                    }
                }


            }
            catch (System.Exception ex)
            {
                _logger.LogError("데이터 수정 에러: " + ex);
            }
            //return RedirectToAction("Index", translateName); // 저장 후 리스트 페이지로 이동
        }
        /*
      public List<SelectListItem> GetMovesByCharacterSelectItems(int character_code)
      {
          List<SelectListItem> list = new List<SelectListItem>();
          foreach (Move move in GetAllMoves(character_code))
          {
              list.Add(new SelectListItem { Text = move.Name, Value = move.Code.ToString() });
          }
          return list;
      }*/

    }
}
