using System.Reflection;
using System.Text;
using FileUploader;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.Samples
{
    /// <summary>
    /// YouTube Data API v3 sample: upload a video.
    /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
    /// See https://code.google.com/p/google-api-dotnet-client/wiki/GettingStarted
    /// </summary>
    internal class UploadVideo
    {
        static int Number = 1;
        static string Character_en = "AKUMA";
        static string Character_kr = "고우키";
        static List<TekkenVideo> tekkenVideoList = new List<TekkenVideo>();

        [STAThread]
        static void Main(string[] args)
        {


            Console.WriteLine("Number: " + Number.ToString().PadLeft(2, '0'));

            ReadVideoList();

            try
            {
                new UploadVideo().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void ReadVideoList()
        {
            string strFile = $"D:\\TEKKEN_PROJECT\\DATA\\INFO\\{Number.ToString().PadLeft(2, '0')}.{Character_en}\\{Character_en}_UPLOAD.csv";

            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
                {
                    string strLineValue = null;
                    string[] keys = null;
                    string[] values = null;
                    int lineCount = 0;

                    while ((strLineValue = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(strLineValue)) return;

                        if (lineCount++ > 0)
                        {
                            keys = strLineValue.Split(',');

                            string title_en = keys[3];
                            string title_kr = keys[4];
                            string fileName = keys[5];
                            TekkenVideo tekkenVideo = new(Number, Character_en, Character_kr, title_en, title_kr, fileName);
                            tekkenVideoList.Add(tekkenVideo);
                            Console.WriteLine(lineCount.ToString() + " " + title_en + " " + title_kr + " " + tekkenVideo.Description);
                            continue;
                        }
                    }
                }
            }
        }

        private async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtubepartner, YouTubeService.Scope.YoutubeReadonly, YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;
            var channelsListResponse = channelsListRequest.Execute();

            //var uploadListId = "PLs0IT9AM5mDvuR9ROGH605fC1yBWaDik6";
            ///foreach (var channel in channelsListResponse.Items[0]){}
            var uploadListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;//UUh-_wQe1LT2JmAowcHd1Liw



            List<string> videoIdList = new List<string>();
            List<Video> videoList = new List<Video>();
            string nextPageToken = string.Empty;
            while (nextPageToken != null)
            {
                var uploadListItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                uploadListItemsListRequest.PlaylistId = uploadListId;
                uploadListItemsListRequest.MaxResults = 13;
                uploadListItemsListRequest.PageToken = nextPageToken;


                var uploadListItemsListResponse = uploadListItemsListRequest.Execute();


                foreach (var item in uploadListItemsListResponse.Items)
                {
                    var uploadedVideo = item;
                    videoIdList.Add(uploadedVideo.Snippet.ResourceId.VideoId);
                }
                nextPageToken = uploadListItemsListResponse.NextPageToken;
            }


            foreach (var videoId in videoIdList)
            {
                var my_video_request = youtubeService.Videos.List("snippet, status");
                my_video_request.Id = videoId; // the Youtube video id of the video you want to update
                my_video_request.MaxResults = 1;
                var my_video_response = await my_video_request.ExecuteAsync();
                videoList.Add(my_video_response.Items[0]);
            }



            foreach (Video video in videoList)
            {

                var tVideo =
                        from tekkenVideo in tekkenVideoList
                        where tekkenVideo.FileName.Replace("_", " ").Replace("(", "").Replace(")", "").Replace(".mp4", "") == video.Snippet.Title
                        select tekkenVideo;

                int count = tVideo.Count();
                if (count > 0)
                {
                    TekkenVideo tekkenVideo = tVideo?.FirstOrDefault();

                    video.Snippet.Title = tekkenVideo.Title_en;
                    video.Snippet.Description = tekkenVideo.Description;


                    video.Snippet.CategoryId = "20";
                    //video.Snippet.s.publicStatsViewable=
                    //                    video.Status.PrivacyStatus = "unlisted"; // or "private" or "public"


                    /*if (tags == null)
                    {
                        tags = new ArrayList<String>(1);
                        snippet.setTags(tags);
                    }
                    tags.add(tag);*/

                    var my_update_request = youtubeService.Videos.Update(video, "snippet, status");
                    var result = my_update_request.Execute();


                }

            }

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











            //TekkenVideo t = tekkenVideoList.Where(Video => Video.Title_en == videoList[3].Snippet.Title);
            //tekkenVideoList.






            /*  // then we change it's attributes
              string title = "New title";
              string description = "New description";
              List<String> keywords = new List<String>();

              video.Snippet.Title = title;
              video.Snippet.Description = description;
              video.Snippet.Tags = new System.Collections.Generic.List<String>();*/

            // and tell the changes we want to youtube
            //var my_update_request = youtubeService.Videos.Update(video, "snippet, status");
            //my_update_request.Execute();


            //youtubeService.Videos.List  = youtube.videos().list(videoId, "snippet");


            //var updateVideoRequest = youtubeService.Videos.Update("snippet", video);
            // Request is executed and updated video is returned
            //Video videoResponse = updateVideosRequest.execute();


            //u14Qxhoofvg
            //playlistItemsListRequest.PageToken = nextPageToken;
            //playlistItemsListRequest.RestPath=
            // Call the search.list method to retrieve results matching the specified query term.


            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();




            //Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            //Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            //Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
        }

        private async Task UploadVideos(YouTubeService youtubeService, TekkenVideo tekkenVideo)
        {
            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = tekkenVideo.Title_en;
            video.Snippet.Description = tekkenVideo.Description;
            video.Snippet.Tags = new string[] { "TEKKEN", "TEKKEN7", "TEKKEN 7", "MOVEMENT", Character_en, Character_kr };
            video.Snippet.ChannelId = "UCdJqpY1ix4PnCWhnJ8RayWQ"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Snippet.CategoryId = "20";
            //video.Snippet.ChannelTitle

            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "public"; // or "private" or "public"

            //await Upload(youtubeService, video, tekkenVideo.FilePath);
        }

/*        private async Task Upload(YouTubeService youtubeService, Video video, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();
            }
        }

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }*/
    }
}
