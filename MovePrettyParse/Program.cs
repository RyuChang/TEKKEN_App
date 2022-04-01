using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace MovePrettyParse
{
    internal class Program
    {

        static void Main(string[] args)
        {
            String fileName = "AKUMA_kr.TXT";
            string htmlFile = ReadVideoList(fileName);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlFile);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//table");

            HtmlNodeCollection childNodes = htmlBody.ChildNodes;


            //    < div class="move-card-header__index">1</div>
            //<div class="">진순옥살</div>
            //<div class="move-card-header__hit-count">
            IEnumerable<HtmlNode> indexNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card-header__index"));
            IEnumerable<HtmlNode> nameNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card-header__name"));


            IEnumerable<HtmlNode> mainNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card"));

            foreach (var mainNode in mainNodes)
            {
                foreach (var childNode in mainNode.ChildNodes)
                {
                    if (childNode.FirstChild.FirstChild.InnerText == "★")
                    {
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
                            }
                            else if (mainInfo.HasClass("move-title"))
                            {
                                Console.WriteLine("title:" + mainInfo.FirstChild.InnerText);
                                Console.WriteLine("Hit:" + mainInfo.LastChild.InnerText);

                            }
                            else if (mainInfo.HasClass("move-string"))
                            {
                                foreach (var node in mainInfo.ChildNodes)
                                {

                                    Console.Write(node.InnerText);
                                }
                                Console.WriteLine();
                            }
                            else if (mainInfo.HasClass("move-hit-dmg"))
                            {
                                var firstNode = mainInfo.FirstChild;
                                if (firstNode.HasChildNodes)
                                {
                                    Console.Write("Hit LV : ");
                                    foreach (var node_hitLevel in firstNode.ChildNodes)
                                    {
                                        Console.Write(" " + node_hitLevel.InnerText);
                                    }
                                    Console.WriteLine();
                                }
                                //Console.Write("Hit LV:"+node.InnerText);


                                var lastNode = mainInfo.LastChild;  //move-dmg
                                Console.WriteLine("mv Frame: " + lastNode.FirstChild.InnerText);
                                Console.WriteLine("mv hitdmg : " + lastNode.LastChild.LastChild.InnerText);

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
                        Console.WriteLine("start Frame:" + frame_start);
                        Console.WriteLine("start_seg Frame:" + frame_start_seg);
                        Console.WriteLine("block Frame:" + frame_block);
                        Console.WriteLine("hit Frame:" + frame_hit);

                    }
                    Console.WriteLine();
                }


                /*
                 <div class="move-card-hit-info__damage">
            <span class="move-card-hit-info__sum">
              20
            </span>

                  <span class="move-card-hit-info__expression">
                    (10+10)
                  </span>

          </div>

                 */

                //if (node.NodeType == HtmlNodeType.Element)
                //{
                //}

                /*Console.WriteLine("containerNodes : " + containerNodes.Count());
                Console.WriteLine("index Count : "+ indexNodes.Count());
                Console.WriteLine("name Count : "+ nameNodes.Count());
        */
            }
        }


        private static string ReadVideoList(string path)
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
