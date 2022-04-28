using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveVideoService : BaseNameService<MoveVideo, MoveVideo_name>, IMoveVideoService
    {
        [Inject] ICharacterService CharacterService { get; set; } = default!;

        UserCredential credential;
        YouTubeService youtubeService;

        public MoveVideoService(TekkenDbContext tekkenDbContext, ICharacterService _characterService) : base(tekkenDbContext, tekkenDbContext.MoveVideo, tekkenDbContext.MoveVideo_name)
        {
            MainTable = TableName.MoveVideo.ToString();
            NameTable = TableName.MoveVideo_name.ToString();
            CharacterService = _characterService;
        }

        public async Task<List<MoveVideo>> GetEntitiesWithMove()
        {
            return await _dataDbSet.Include("Move").Include("NameSet").ToListAsync();
            //return  _dataDbSet.ToList();
        }

        public async Task<MoveVideo> GetEntityWithMovesByIdAsync(int id)
        {
            return await _dataDbSet.Include("Move").Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MoveVideo>> GetEntitiesWithMoveByCharacterCode(int characterCode)
        {
            return await _dataDbSet.Where(m => m.Move.Character_code == characterCode).OrderBy(m => m.Move.Number).Include(d => d.Move).Include(d => d.NameSet).ToListAsync();
        }



        public async Task UpdateYoutubeVideoInfos(int characterCode)
        {
            await SetCredential();
            SetYoutubeService();

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;
            //channelsListRequest.ManagedByMe= true;
            var channelsListResponse = channelsListRequest.Execute();

            //var uploadListId = "PLs0IT9AM5mDvuR9ROGH605fC1yBWaDik6";      //Upload
            //var uploadListId = "PLs0IT9AM5mDtiuL7EM5mTKYpOSULORc1x";    //카즈야

            // SIARIAPAPA "UCdJqpY1ix4PnCWhnJ8RayWQ"
            // SIARIAPAPA1 "UCDR9pqs0PMhe7q_A6OiBIPg"
            var uploadListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;//UUh-_wQe1LT2JmAowcHd1Liw
            //var uploadListId = "UCDR9pqs0PMhe7q_A6OiBIPg";
            string nextPageToken = string.Empty;


            List<Video> videoList = new List<Video>();
            int count = 5;
            while (nextPageToken != null /*&& count-- > 0*/)
            {
                var uploadListItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                uploadListItemsListRequest.PlaylistId = uploadListId;
                uploadListItemsListRequest.MaxResults = 50;
                uploadListItemsListRequest.PageToken = nextPageToken;
                var uploadListItemsListResponse = uploadListItemsListRequest.Execute();
                Console.WriteLine("Count:" + count);
                
                foreach (var item in uploadListItemsListResponse.Items)
                {
                    //string characterName = await CharacterService.GetCharacterByCharacterCode(characterCode).Result.NameSet.ToList()[0].Name;

                    var character = await CharacterService.GetCharacterByCharacterCode(characterCode);
                    string characterName = character.NameSet.ToList()[0].Name;
                    Console.WriteLine("Title:" + item.Snippet.Title);
                    
                    if (IsNotUpdated(characterName, item.Snippet.Title))
                    {
                        await UpdateVideo(characterCode, characterName, item.Snippet.ResourceId.VideoId);
                    }

                }
                nextPageToken = uploadListItemsListResponse.NextPageToken;
            }


            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            async Task SetCredential()
            {
                using (var stream = new FileStream("Properties/client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { YouTubeService.Scope.Youtubepartner, YouTubeService.Scope.YoutubeReadonly, YouTubeService.Scope.YoutubeUpload },
                        "user",
                        CancellationToken.None
                    );
                }
            }

            void SetYoutubeService()
            {
                youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
                });
                string ApplicationName = Assembly.GetExecutingAssembly().GetName().Name;
            }

        }

        private async Task<Video> GetVideoById(string videoId)
        {
            var my_video_request = youtubeService.Videos.List("snippet, status, fileDetails");
            my_video_request.Id = videoId; // the Youtube video id of the video you want to update
            my_video_request.MaxResults = 1;
            var my_video_response = await my_video_request.ExecuteAsync();
            return my_video_response.Items[0];
        }

        private bool IsNotUpdated(string characterName, string title)
        {

            if (title.Contains(characterName)|| title.Contains(characterName.Replace("-", " ")))
            {
                return true;
            }
            return false;
        }


        private async Task UpdateVideo(int characterCode, string characterName, string videoId)
        {
            Video video = await GetVideoById(videoId);


            //var moveVideoEntity = await _tekkenDBContext.MoveVideo.Where(v => v.FileName.Replace("_", " ").Replace(".", " ").Replace("(", "").Replace(")", "") == video.Snippet.Title).FirstOrDefaultAsync();
            var moveVideoEntity = await _tekkenDBContext.MoveVideo.Where(d => d.Move.Character_code == characterCode).Where(v => video.Snippet.Title == v.FileName || v.FileName == video.FileDetails.FileName.ToLower().Replace(".mp4", "")).FirstOrDefaultAsync();
            //var moveVideoEntity = await _tekkenDBContext.MoveVideo.Where(d => d.Move.Character_code == characterCode).Where(v =>  v.FileName == video.FileDetails.FileName.ToLower().Replace(".mp4", "")).FirstOrDefaultAsync();

            if (moveVideoEntity != null && string.IsNullOrEmpty(video.Snippet.Description))
            {
                video.Snippet.Title = moveVideoEntity.YoutubeTitle;
                video.Snippet.Description = moveVideoEntity.YoutubeDescription;
                video.Snippet.Tags = moveVideoEntity.YoutubeTag.Split(',');
                video.Snippet.CategoryId = "20";
                //video.Snippet.DefaultLanguage = "en";
                video.Snippet.DefaultAudioLanguage = "en-US";
                video.Status.PrivacyStatus = "public";
                //status.uploadStatus
                //video.Snippet.ChannelTitle = "Kazuya";
                //video.Status.PublicStatsViewable = false;
                var my_update_request = youtubeService.Videos.Update(video, "snippet, status, fileDetails");
                Video result = null;
                try
                {
                    result = my_update_request.Execute();
                    int updateResult = await UpdateYoutubeUrl(moveVideoEntity, result.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }


                //result.Id
                //video.FileDetails.FileName
            }

        }

        private async Task<int> UpdateYoutubeUrl(MoveVideo moveVideoEntity, string id)
        {
            moveVideoEntity.YoutubeId = id;
            _tekkenDBContext.Entry(moveVideoEntity).State = EntityState.Modified;
            var result = await _tekkenDBContext.SaveChangesAsync();
            return result;
        }

        string TranseFileName(string fileName)
        {
            string newFileName = fileName.Replace("_", " ").Replace(".", " ").Replace("(", "").Replace(")", "");
            return newFileName;
        }

    }

}


/*


            string AKUMA_playlist = "PLs0IT9AM5mDsdhS17PYnb8a1oP9HL-sdx";
            var AKUMAListItemsListRequest = youtubeService.PlaylistItems.List("snippet");
            AKUMAListItemsListRequest.PlaylistId = AKUMA_playlist;
            AKUMAListItemsListRequest.MaxResults = 10;
            AKUMAListItemsListRequest.PageToken = nextPageToken;
            var AKUMAListItemsListResponse = AKUMAListItemsListRequest.Execute();


            foreach (var item in uploadListItemsListResponse1.Items)
            {
                AKUMAListItemsListResponse.Items.Add(item);
                item.Snippet.PlaylistId = AKUMA_playlist;
                //item.Snippet.PlaylistId=
                //var my_update_request = youtubeService.PlaylistItems.Update(item, "snippet, status");
                //my_update_request.Execute();
                var my_update_request = youtubeService.PlaylistItems.Update(item, "snippet").ExecuteAsync();
            }
            */