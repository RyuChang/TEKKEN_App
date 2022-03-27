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
        static List<TekkenVideo> videoList = new List<TekkenVideo>();

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
                            videoList.Add(tekkenVideo);
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
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }


            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });


            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "Google"; // Replace with your search term.
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }

            Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
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

        private async Task Upload(YouTubeService youtubeService, Video video, string filePath)
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
        }
    }
}
