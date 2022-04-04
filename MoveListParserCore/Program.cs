using System.Xml;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MoveListParser
{
    class Program
    {
        //static TekkenDbContext tekkenDbContext;

        public static void Main(string[] args)
        {
            //"FAHKUMRAM", "ANNA", "ARMOR KING", "GANRYU", "GEESE","JULIA","LEI","LEROY", , "ZAFINA" "NOCTIS","NEGAN", "MARDUK",
            string[] characterList = { "EDDY"};
            /////"PANDA",
            //string[] characterList = { "AKUMA", "ALISA", "ANNA", "ARMOR KING", "ASUKA", "BOB", "BRYAN", "CLAUDIO", "DEVIL JIN", "DRAGUNOV", "EDDY", "ELIZA", "FAHKUMRAM", "FENG", "GANRYU", "GEESE", "GIGAS", "HEIHACHI", "HWOARANG", "JACK-7", "JIN", "JOSIE", "JULIA", "KATARINA", "KAZUMI", "KAZUYA", "KING", "KUMA", "LARS", "LAW", "LEE", "LEI", "LEO", "LEROY", "LILI", "LUCKY CHLOE", "MARDUK", "MASTER RAVEN", "MIGUEL", "NEGAN", "NINA", "NOCTIS",  "PAUL", "SHAHEEN", "STEVE", "XIAOYU", "Yoshimitsu", "ZAFINA" };

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
            String fileName = $"Data\\{charater}_{language_code}.TXT";
            string htmlFile = ReadVideoList(fileName);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlFile);
            Update(htmlDoc, charater, language_code).Wait();
        }

        private static async Task Update(HtmlDocument htmlDoc, string charater, string language_code)
        {
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//table/tbody");

            IEnumerable<HtmlNode> mainNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card-container"));

            int count = 0;
            foreach (var mainNode in mainNodes)//move-card
            {
                //   if (count++ > 2) return;
                bool success = true;
                TekkenApp.Models.TekkenMoveList tekkenMoveList = new();
                tekkenMoveList.Language_code = language_code;
                tekkenMoveList.Character_Name = charater;
                //move-info move-extra
                foreach (var childNode in mainNode.ChildNodes)
                {
                    //if (childNode.FirstChild.FirstChild.InnerText == "★")
                    //{
                    //    success = false;
                    //    continue;
                    //}
                    //=========================================== move-info  ===========================================
                    if (childNode.HasClass("left-card-pannel"))
                    {
                        foreach (var mainInfo in childNode.ChildNodes)
                        {
                            //////////////////////////////////  move-card-header  /////////////////////////////////
                            if (mainInfo.HasClass("move-card-header"))
                            {
                                foreach (var node in mainInfo.ChildNodes)
                                {
                                    if (node.HasClass("move-card-header__index")) tekkenMoveList.Number = int.Parse(node.InnerText);
                                    if (node.HasClass("move-card-header__name")) tekkenMoveList.Title = node.InnerText.Trim();
                                    if (node.HasClass("move-card-header__hit-count")) tekkenMoveList.Hit = node.InnerText.Trim();
                                }
                            }
                            //////////////////////////////////  move-card-content  /////////////////////////////////
                            else if (mainInfo.HasClass("move-card-content"))
                            {
                                foreach (var node in mainInfo.ChildNodes)
                                {
                                    if (node.HasClass("move-card-command"))
                                    {
                                        tekkenMoveList.Command = node.InnerHtml.ToString().Trim();
                                        Console.WriteLine("Command:" + tekkenMoveList.Command);
                                    }
                                    else if (node.HasClass("move-card-hit-info"))
                                    {
                                        foreach (var hitNode in node.ChildNodes)
                                        {
                                            if (hitNode.HasClass("move-card-hit-info__level"))
                                            {
                                                string hitLv = hitNode.InnerText.Trim();
                                                tekkenMoveList.HitLv = hitLv; ;
                                                Console.WriteLine("Hit Level:" + tekkenMoveList.HitLv);
                                            }
                                            else if (hitNode.HasClass("move-card-hit-info__damage"))
                                            {
                                                foreach (var node_dmg in hitNode.ChildNodes)
                                                {
                                                    if (node_dmg.HasClass("move-card-hit-info__sum"))
                                                    {
                                                        string Dmg = node_dmg.InnerText.Trim();
                                                        tekkenMoveList.Dmg = Dmg; ;
                                                        Console.WriteLine("DMG:" + tekkenMoveList.Dmg);
                                                    }
                                                    else if (node_dmg.HasClass("move-card-hit-info__expression"))
                                                    {
                                                        string displayDmg = node_dmg.InnerText.Trim();
                                                        tekkenMoveList.DisplayDmg = displayDmg;
                                                        Console.WriteLine("DMG:" + tekkenMoveList.Dmg);
                                                    }
                                                }

                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }

                    //=========================================== right-card-pannel  ===========================================
                    if (childNode.HasClass("right-card-pannel"))
                    {
                        foreach (var mainInfo in childNode.ChildNodes)
                        {
                            if (mainInfo.HasClass("move-frame"))
                            {
                                int index = 0;
                                //4 8 12 16 값 start block hit counter
                                foreach (var frameNode in mainInfo.ChildNodes)
                                {
                                    index++;
                                    if (index == 4)
                                    {
                                        tekkenMoveList.StartFrame = frameNode.InnerText.Trim();
                                    }
                                    else if (index == 8)
                                    {
                                        tekkenMoveList.BlockFrame = frameNode.InnerText.Trim();
                                    }
                                    else if (index == 12)
                                    {
                                        tekkenMoveList.HitFrame = frameNode.InnerText.Trim();
                                    }
                                    else if (index == 16)
                                    {
                                        tekkenMoveList.CounterFrame = frameNode.InnerText.Trim();
                                    }
                                }
                                Console.WriteLine(index + ": ");
                                Console.WriteLine("frame:===============" + tekkenMoveList.StartFrame);
                                Console.WriteLine("frame:===============" + tekkenMoveList.BlockFrame);
                                Console.WriteLine("frame:===============" + tekkenMoveList.HitFrame);
                                Console.WriteLine("frame:===============" + tekkenMoveList.CounterFrame);

                            }
                        }

                    }
                }

                if (success)
                {
                    await UpdateAsync(tekkenMoveList);
                }

            }
        }

        public static async Task UpdateAsync(TekkenMoveList tekkenMoveList)
        {
            var options = new DbContextOptionsBuilder<TekkenDbContext>().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Tekken; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("TekkenApp")).EnableSensitiveDataLogging(true).Options;
            var tekkenDbContext = new TekkenDbContext(options);

            tekkenDbContext.Entry(tekkenMoveList).State = EntityState.Added;
            await tekkenDbContext.TekkenMoveList.AddAsync(tekkenMoveList);
            await tekkenDbContext.SaveChangesAsync();
            //return tekkenPretty;
        }


        private static string ReadVideoList(string path)
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                return sr.ReadToEnd().Replace("\r\n", "");
            }
        }
    }
}
