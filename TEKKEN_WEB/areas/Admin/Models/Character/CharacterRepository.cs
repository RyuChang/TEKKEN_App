using Admin.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using TEKKEN_WEB.Enums;
using TEKKEN_WEB.Models;

namespace Admin.Models
{
    public class CharacterRepository : BaseCharacterRepository, ICharacterRepository
    {
        private IConfiguration _config;
        private SqlConnection con;
        private ILogger<CharacterRepository> _logger;
        public string language_code = string.Empty;

        public CharacterRepository(IConfiguration config, ILogger<CharacterRepository> logger, IHttpContextAccessor httpContextAccessor) : base(config, logger, httpContextAccessor)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("TekkenConnection").Value);
            _logger = logger;
            language_code = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName;
        }

        public List<Character> GetAllCharacters() => con.Query<Character>("[Character_GetAllCharacters]", new { language_code = language_code }, commandType: CommandType.StoredProcedure).ToList();

        public int Remove(int id) => con.Execute("[Character_RemoveCharacter]", new { Id = id }, commandType: CommandType.StoredProcedure);

        public int SaveOrUpdate(TEKKEN_WEB.Models.Character model, FormType formType)
        {
            throw new NotImplementedException();
        }

        public int UpdateCharacter(TEKKEN_WEB.Models.Character model, int character_code)
        {
            //throw new NotImplementedException();
            return 0;
        }

        public Character GetCharacterDetail(int character_code)
            => con.Query<Character>("[GetCharacter_DetailByCharacterCode]",
                new DynamicParameters(new { character_code = character_code, language_code = language_code }),
                commandType: CommandType.StoredProcedure).SingleOrDefault();

        List<SelectListItem> ICharacterRepository.GetAllCharactersSelectItems(int character_code)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Character characterin in GetAllCharacters())
            {
                bool selectedItem = false;
                if (characterin.Code == character_code)
                {
                    selectedItem = true;
                }

                list.Add(new SelectListItem { Text = characterin.Name, Value = characterin.Code.ToString(), Selected = selectedItem });
            }
            return list;
        }
    }
}
