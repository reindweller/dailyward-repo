using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace DailyWard
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in ReadPage())
            {
                Console.WriteLine(item.Title);
                Console.WriteLine(item.ThumbnailSrc);
                Console.WriteLine("");
                Console.WriteLine("");
            }
            Console.WriteLine("press any key to exit");
            Console.ReadLine();
        }

        public static List<SsModel> ReadPage()
        {
            var models = new List<SsModel>();
            //get the page
            var web = new HtmlWeb();
            var document = web.Load("https://www.reddit.com/");
            var page = document.DocumentNode;

            //loop through all div tags with item css class
            foreach (var item in page.QuerySelectorAll(".thing"))
            {
                var title = item.QuerySelector("a.title").InnerText;
                var thumbnail = item.QuerySelector(".thumbnail").ChildNodes["img"];
                var srcAttr = string.Empty;
                var src = string.Empty;


                if (thumbnail != null)
                {
                    var thumbnailAttribute = thumbnail.Attributes["src"];
                    src = srcAttr == null ? string.Empty : thumbnailAttribute.Value;
                }
                

                models.Add(new SsModel
                {
                    Title = title,
                    ThumbnailSrc = src,
                });

            }

            
            return models;
        }

        
    }

    public class SsModel
    {
        public string Title { get; set; }
        public string ThumbnailSrc { get; set; }
    }
}
