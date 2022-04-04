using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;

namespace MovePrettyParseCore
{
    class Program
    {
        //static TekkenDbContext tekkenDbContext;

        public static void Main(string[] args)
        {
            //"FAHKUMRAM", "ANNA", "ARMOR KING", "GANRYU", "GEESE","JULIA","LEI","LEROY", , "ZAFINA" "NOCTIS","NEGAN", "MARDUK",
            string[] characterList = { "AKUMA", "ALISA", "ASUKA", "BOB", "BRYAN", "CLAUDIO", "DEVIL JIN", "DRAGUNOV", "EDDY", "ELIZA", "FENG",  "GIGAS", "HEIHACHI", "HWOARANG", "JACK-7", "JIN", "JOSIE",  "KATARINA", "KAZUMI", "KAZUYA", "KING", "KUMA", "LARS", "LAW", "LEE",  "LEO", "LILI", "LUCKY CHLOE",  "MASTER RAVEN", "MIGUEL", "NINA",  "PANDA", "PAUL", "SHAHEEN", "STEVE", "XIAOYU", "Yoshimitsu"};
            //string[] characterList = { "AKUMA", "ALISA", "ANNA", "ARMOR KING", "ASUKA", "BOB", "BRYAN", "CLAUDIO", "DEVIL JIN", "DRAGUNOV", "EDDY", "ELIZA", "FAHKUMRAM", "FENG", "GANRYU", "GEESE", "GIGAS", "HEIHACHI", "HWOARANG", "JACK-7", "JIN", "JOSIE", "JULIA", "KATARINA", "KAZUMI", "KAZUYA", "KING", "KUMA", "LARS", "LAW", "LEE", "LEI", "LEO", "LEROY", "LILI", "LUCKY CHLOE", "MARDUK", "MASTER RAVEN", "MIGUEL", "NEGAN", "NINA", "NOCTIS", "PANDA", "PAUL", "SHAHEEN", "STEVE", "XIAOYU", "Yoshimitsu", "ZAFINA" };

            // DbcontextOptionBuilder 를 사용하여 인-메모리 데이터베이스 정보를 DbContext에 전달
            //Server = (localdb)\\mssqllocaldb; Database = Tekken; Trusted_Connection = True; MultipleActiveResultSets = true
            //    builder.Services.AddDbContext<TekkenDbContext>(options =>
            //options.UseSqlServer(
            //builder.Configuration.GetConnectionString("tekkenConnection"), b => b.MigrationsAssembly("TekkenApp")).EnableSensitiveDataLogging(), ServiceLifetime.Transient);
            //    builder.Services.AddControllers().AddJsonOptions(x =>
            //    {
            int count = 0;
            foreach (var charater in characterList)
            {
                //if (count++ < 1)
                {
                    InsertData(charater, "en");
                    InsertData(charater, "kr");
                }
            }


        }

        private static void InsertData(string charater, string language_code)
        {
            String fileName = $"MovePrettyData\\{charater}_{language_code}.TXT";
            string htmlFile = ReadVideoList(fileName);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlFile);
            Update(htmlDoc, charater, language_code).Wait();
        }

        private static async Task Update(HtmlDocument htmlDoc, string charater, string language_code)
        {
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//table");

            //HtmlNodeCollection childNodes = htmlBody.ChildNodes;
            IEnumerable<HtmlNode> mainNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card"));

            int count = 0;
            foreach (var mainNode in mainNodes)//move-card
            {
                //   if (count++ > 2) return;
                bool success = true;
                TekkenApp.Models.TekkenPretty tekkenPretty = new();
                tekkenPretty.Language_code = language_code;
                tekkenPretty.Character_Name= charater;
                //move-info move-extra
                foreach (var childNode in mainNode.ChildNodes)
                {
                    if (childNode.FirstChild.FirstChild.InnerText == "★")
                    {
                        success = false;
                        continue;
                    }
                    //=========================================== move-info  ===========================================
                    if (childNode.HasClass("move-info"))
                    {
                        foreach (var mainInfo in childNode.ChildNodes)
                        {


                            //////////////////////////////////  move-card-header  /////////////////////////////////
                            if (mainInfo.HasClass("move-number"))
                            {
                                Console.WriteLine("number:" + mainInfo.InnerText);
                                tekkenPretty.Number = int.Parse(mainInfo.InnerText);
                            }
                            else if (mainInfo.HasClass("move-title"))
                            {
                                string title = mainInfo.FirstChild.InnerText;
                                string hit = mainInfo.LastChild.InnerText;
                                Console.WriteLine("title:" + title);
                                Console.WriteLine("Hit:" + hit);
                                tekkenPretty.Title = title;
                                tekkenPretty.Hit = hit;

                            }
                            else if (mainInfo.HasClass("move-string"))
                            {
                                string command = mainInfo.InnerHtml;
                                //foreach (var node in mainInfo.ChildNodes)
                                //{
                                //    command += node.InnerText;
                                //    Console.Write(node.InnerText);
                                //}
                                Console.WriteLine(command);
                                tekkenPretty.Command = command;
                            }
                            else if (mainInfo.HasClass("move-hit-dmg"))
                            {
                                var firstNode = mainInfo.FirstChild;
                                if (firstNode.HasChildNodes)
                                {
                                    string hitLv = string.Empty;
                                    Console.Write("Hit LV : ");
                                    foreach (var node_hitLevel in firstNode.ChildNodes)
                                    {
                                        hitLv += node_hitLevel.InnerText;
                                    }
                                    Console.WriteLine(hitLv);
                                    tekkenPretty.HitLv = hitLv;
                                }

                                var lastNode = mainInfo.LastChild;  //move-dmg
                                string dmg = lastNode.FirstChild.InnerText;
                                string DisplayDmg = lastNode.LastChild.LastChild.InnerText;

                                Console.WriteLine("mv dmg: " + tekkenPretty.Dmg);
                                Console.WriteLine("mv hitdmg : " + tekkenPretty.DisplayDmg);

                                tekkenPretty.Dmg = dmg;
                                tekkenPretty.DisplayDmg = DisplayDmg;
                            }

                        }
                    }

                    //////////////////////////////////  move-card-content  /////////////////////////////////

                    if (childNode.HasClass("move-extra"))
                    {
                        var mvSection_node = childNode.FirstChild; //mv-section



                        var moveFramesNode = mvSection_node.LastChild;

                        string frame_start = string.Empty;
                        string frame_start_seg = string.Empty;
                        string frame_block = string.Empty;
                        string frame_hit = string.Empty;

                        foreach (var frameNode in moveFramesNode.FirstChild.ChildNodes)
                        {

                            if (frameNode.HasClass("move-startf"))
                            {
                                frame_start = frameNode.LastChild.InnerText.Trim();

                            }
                            else if (frameNode.HasClass("move-startf-seg"))
                            {
                                frame_start_seg = frameNode.LastChild.InnerText.Trim();
                            }
                            else if (frameNode.HasClass("move-blockf"))
                            {

                                frame_block = frameNode.LastChild.InnerText.Trim();
                            }
                            else if (frameNode.HasClass("move-hitf"))
                            {
                                frame_hit = frameNode.LastChild.InnerText.Trim();
                            }
                        }
                        tekkenPretty.StartFrame = frame_start;
                        tekkenPretty.StratSegFrame = frame_start_seg;
                        tekkenPretty.BlockFrame = frame_block;
                        tekkenPretty.HitFrame = frame_hit;

                        Console.WriteLine("start Frame:" + frame_start);
                        Console.WriteLine("start_seg Frame:" + frame_start_seg);
                        Console.WriteLine("block Frame:" + frame_block);
                        Console.WriteLine("hit Frame:" + frame_hit);

                    }
                }
                if (success)
                {
                    await UpdateAsync(tekkenPretty);
                }

            }
        }

        public static async Task UpdateAsync(TekkenPretty tekkenPretty)
        {
            var options = new DbContextOptionsBuilder<TekkenDbContext>().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Tekken; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("TekkenApp")).EnableSensitiveDataLogging(true).Options;
            var tekkenDbContext = new TekkenDbContext(options);

            //tekkenDbContext.Entry(tekkenPretty).State = EntityState.Added;
            await tekkenDbContext.TekkenPretty.AddAsync(tekkenPretty);
            await tekkenDbContext.SaveChangesAsync();
            //return tekkenPretty;
        }


        private static string ReadVideoList(string path)
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                return sr.ReadToEnd().Replace("\r\n","");
            }
        }
    }
}
