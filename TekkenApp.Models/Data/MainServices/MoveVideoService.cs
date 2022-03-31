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
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class MoveVideoService : BaseNameService<MoveVideo, MoveVideo_name>, IMoveVideoService
    {

        UserCredential credential;
        YouTubeService youtubeService;

        public MoveVideoService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.MoveVideo, tekkenDbContext.MoveVideo_name)
        {
            MainTable = TableName.MoveVideo.ToString();
            NameTable = TableName.MoveVideo_name.ToString();
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
            var channelsListResponse = channelsListRequest.Execute();

            //var uploadListId = "PLs0IT9AM5mDvuR9ROGH605fC1yBWaDik6";      //Upload
            //var uploadListId = "PLs0IT9AM5mDtiuL7EM5mTKYpOSULORc1x";    //카즈야
            var uploadListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;//UUh-_wQe1LT2JmAowcHd1Liw

            string nextPageToken = string.Empty;


            List<Video> videoList = new List<Video>();
            while (nextPageToken != null)
            {
                var uploadListItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                uploadListItemsListRequest.PlaylistId = uploadListId;
                uploadListItemsListRequest.MaxResults = 50;
                uploadListItemsListRequest.PageToken = nextPageToken;
                var uploadListItemsListResponse = uploadListItemsListRequest.Execute();

                foreach (var item in uploadListItemsListResponse.Items)
                {
                    var videoId = item.Snippet.ResourceId.VideoId;
                    Video video = await GetVideoById(videoId);
                    videoList.Add(video);
                }
                nextPageToken = uploadListItemsListResponse.NextPageToken;
            }



            foreach (Video video in videoList)
            {
                //var moveVideoEntity = await _tekkenDBContext.MoveVideo.Where(v => v.FileName.Replace("_", " ").Replace(".", " ").Replace("(", "").Replace(")", "") == video.Snippet.Title).FirstOrDefaultAsync();
                var moveVideoEntity = await _tekkenDBContext.MoveVideo.Where(v => v.FileName == video.FileDetails.FileName.Replace(".mp4", "")).FirstOrDefaultAsync();

                if (moveVideoEntity != null && string.IsNullOrEmpty(video.Snippet.Description))
                {
                    video.Snippet.Title = moveVideoEntity.YoutubeTitle;
                    video.Snippet.Description = moveVideoEntity.YoutubeDescription;
                    video.Snippet.Tags = moveVideoEntity.YoutubeTag.Split(',');
                    video.Snippet.CategoryId = "10";
                    video.Status.PrivacyStatus = "public";
                    //status.uploadStatus
                    //video.Snippet.ChannelTitle = "Kazuya";
                    //video.Snippet.s.publicStatsViewable=video.Status.PrivacyStatus = "unlisted"; // or "private" or "public"
                    var my_update_request = youtubeService.Videos.Update(video, "snippet, status, fileDetails");
                    var result = my_update_request.Execute();

                    int updateResult = await UpdateYoutubeUrl(moveVideoEntity, result.Id);
                    //result.Id
                    //video.FileDetails.FileName
                }
            }
            /*
            var uploadListItemsListRequest1 = youtubeService.PlaylistItems.List("snippet");
            uploadListItemsListRequest1.PlaylistId = uploadListId;
            uploadListItemsListRequest1.MaxResults = 10;
            uploadListItemsListRequest1.PageToken = nextPageToken;
            var uploadListItemsListResponse1 = uploadListItemsListRequest1.Execute();



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

            //TekkenVideo t = tekkenVideoList.Where(Video => Video.Title_en == videoList[3].Snippet.Title);


            //youtubeService.Videos.List  = youtube.videos().list(videoId, "snippet");


            //var updateVideoRequest = youtubeService.Videos.Update("snippet", video);
            // Request is executed and updated video is returned
            //Video videoResponse = updateVideosRequest.execute();



            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            async Task SetCredential()
            {
                using (var stream = new FileStream("Properties/client_secrets.json", FileMode.Open, FileAccess.Read))
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
            }

            async Task<Video> GetVideoById(string videoId)
            {
                var my_video_request = youtubeService.Videos.List("snippet, status, fileDetails");
                my_video_request.Id = videoId; // the Youtube video id of the video you want to update
                my_video_request.MaxResults = 1;
                var my_video_response = await my_video_request.ExecuteAsync();
                return my_video_response.Items[0];
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


