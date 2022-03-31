using Microsoft.AspNetCore.Components;
using TekkenApp.Data;
using TekkenApp.Models;

namespace NewTekkenApp.Pages.Admin.MoveVideos
{
    public partial class Index : BasePageComponent
    {
        [Inject] ILanguageService LanguageService { get; set; } = default!;
        [Inject] ICharacterService CharacterService { get; set; } = default!;

        public IEnumerable<MoveVideo> MoveVideoEntities { get; set; } = default!;
        async void OnCharacterChanged(int characterCode)
        {
            CharacterCode = characterCode;
            //childList?.GetEntitiesByCharacterCode(CharacterCode.Value);
            MoveVideoEntities = await CommonService.GetEntitiesWithMoveByCharacterCode(CharacterCode.Value);

            StateHasChanged();
        }

        async void GenerateAllVideos(int? characterCode)
        {
            if (characterCode is not null)
            {
                var moveEntities = await MoveService.GetEntitiesByCharacterCode(characterCode.Value);
                var languageEntities = await LanguageService.GetEntities();

                foreach (Move move in moveEntities)
                {
                    MoveVideo moveVideo = await CommonService.GetDataEntityByBaseCodeAsync(move.Code);

                    if (moveVideo == null)
                    {
                        await CreateMoveVideoEntity(move);
                    }
                    else
                    {
                        //foreach (var language in languageEntities)
                        //{
                        //    MoveCommand_name command_name = await CommonService.GetNameEntitiyByBaseCodeAndLanguageCode(move.Code, language.Language_code);
                        //    if (command_name == null)
                        //    {
                        //        bool result = false;
                        //        result = await CommonService.CreateNameEntityAsync(moveCommand, language.Language_code);
                        //    }
                        //}
                    }
                }
            }
        }

        async void UpdateVideoInfos(int? characterCode)
        {
            if (characterCode is not null)
            {
                CommonService.UpdateYoutubeVideoInfos(characterCode.Value);

            }
        }
        private async Task<bool> CreateMoveVideoEntity(Move move)
        {
            MoveVideo moveVideo = new();
            moveVideo.Base_code = move.Code;
            moveVideo.Code = move.Code;
            moveVideo.FileName = $"{move.Description}";
            moveVideo.Description = $"{move.Description} Movement Frame Data";
            //번호 영문 한글  영문 제목   한글 제목    Youtube Url
            //번호 영문  한글 파일명

            //파일명 desc

            await CommonService.CreateEntityAsync(moveVideo);
            await CreateAllNameEntitiesAsync(moveVideo, move);
            await UpdateMoveVideoEntityDetail(move.Number, moveVideo);
            return true;
        }

        public async Task<bool> CreateAllNameEntitiesAsync(MoveVideo moveVideo, Move move)
        {
            bool result = false;
            var languageEntities = await LanguageService.GetEntities();

            foreach (Language language in languageEntities)
            {
                if (language.Language_code == "ko")
                {
                    MoveVideo koreanVideo = new();
                    koreanVideo.Code = moveVideo.Code;
                    koreanVideo.Description = $"{move.NameSet.Where(n => n.Language_code == "ko").First().Name} 기술 프레임 데이터";
                    result = await CommonService.CreateNameEntityAsync(koreanVideo, language.Language_code);
                    continue;
                }
                result = await CommonService.CreateNameEntityAsync(moveVideo, language.Language_code);
            }

            return result;
        }

        private async Task<bool> UpdateMoveVideoEntityDetail(int number, MoveVideo moveVideo)
        {
            MoveVideo_name moveVideo_Name_en = moveVideo.NameSet.Where(n => n.Language_code == "en").FirstOrDefault() as MoveVideo_name;
            MoveVideo_name moveVideo_Name_ko = moveVideo.NameSet.Where(n => n.Language_code == "ko").FirstOrDefault() as MoveVideo_name;

            Character character = await CharacterService.GetCharacterByCharacterCode(CharacterCode.Value);

            Character_name character_name_en = character.NameSet.Where(n => n.Language_code == "en").FirstOrDefault();
            Character_name character_name_ko = character.NameSet.Where(n => n.Language_code == "ko").FirstOrDefault();
            moveVideo.YoutubeTitle = $"{number.ToString().PadLeft(3, '0')}. {moveVideo_Name_en.Name}";
            moveVideo.YoutubeDescription = moveVideo_Name_en.Name + "\r\n" + moveVideo_Name_ko.Name;
            moveVideo.YoutubeTag = $"TEKKEN, TEKKEN7, MOVEMENT, FRAME, FRAMEDATA, 프레임, 데이타, {character_name_en.Name}, {character_name_en.fullName}, {character_name_ko.Name}, {character_name_ko.fullName}";


            //    카즈야 기술 암운 프레임 데이터Kazuya_Movement_Ultimate_Punch_1080p
            await CommonService.UpdateDataAsync(moveVideo);
            return true;
        }

    }

}