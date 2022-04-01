using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace MoveParser
{
    internal class Program
    {

        static void Main(string[] args)
        {
            String fileName = "AKUMA_kr.TXT";
            string htmlFile = ReadVideoList(fileName);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlFile);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//table/tbody");

            HtmlNodeCollection childNodes = htmlBody.ChildNodes;


            //    < div class="move-card-header__index">1</div>
            //<div class="">진순옥살</div>
            //<div class="move-card-header__hit-count">
            IEnumerable<HtmlNode> indexNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card-header__index"));
            IEnumerable<HtmlNode> nameNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card-header__name"));


            IEnumerable<HtmlNode> mainNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("move-card-container"));

            foreach (var mainNode in mainNodes)
            {
                foreach (var childNode in mainNode.ChildNodes)
                {

                    //=========================================== left-card-pannel  ===========================================
                    if (childNode.HasClass("left-card-pannel"))
                    {
                        foreach (var mainInfo in childNode.ChildNodes)
                        {
                            //////////////////////////////////  move-card-header  /////////////////////////////////
                            if (mainInfo.HasClass("move-card-header"))
                            {
                                foreach (var node in mainInfo.ChildNodes)
                                {
                                    if (node.HasClass("move-card-header__index")) Console.Write(node.InnerText);
                                    if (node.HasClass("move-card-header__name")) Console.WriteLine("   " + node.InnerText);
                                    if (node.HasClass("move-card-header__hit-count")) Console.WriteLine(node.InnerText.Trim());
                                }
                            }
                            //////////////////////////////////  move-card-content  /////////////////////////////////

                            if (mainInfo.HasClass("move-card-content"))
                            {
                                foreach (var node in mainInfo.ChildNodes)
                                {
                                    if (node.HasClass("move-card-hit-info"))
                                    {
                                        foreach (var hitNode in node.ChildNodes)
                                        {
                                            if (hitNode.HasClass("move-card-hit-info__level"))
                                            {
                                                Console.WriteLine("Level:" + hitNode.InnerText.Trim());
                                            }
                                            if (hitNode.HasClass("move-card-hit-info__damage"))
                                            {
                                                Console.WriteLine("DMG:" + hitNode.InnerText.Trim());
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
                                Console.WriteLine("frame:===============");
                                int index = 0;
                                //4 8 12 16 값 start block hit counter
                                foreach (var frameNode in mainInfo.ChildNodes)
                                {
                                    index++;
                                    Console.WriteLine(index + ": " + frameNode.InnerText.Trim());
                                }
                            }
                        }

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
