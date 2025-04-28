using System.Globalization;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Flat_Rock_Technology___Test_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string html = File.ReadAllText("input.html"); //load html file
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            List<Product> products = new List<Product>();

            foreach (HtmlNode itemDiv in document.DocumentNode.SelectNodes("//div[@class = \"item\"]")) //find all product elements
            {
                string ratingString = itemDiv.GetAttributeValue("rating", "0"); // get and normalize rating
                if (!double.TryParse(ratingString, out double rating))
                {
                    rating = 0;
                }

                if (rating > 5)
                {
                    rating /= 2.0;
                }

                // get product name from image alt and price from span
                var imgNode = itemDiv.SelectSingleNode(".//figure/a/img"); 
                var priceNode = itemDiv.SelectSingleNode(".//p[@class=\"price\"]//span[@itemprop=\"price\"]//span[@style=\"display: none\"]");

                if (imgNode != null && priceNode != null)
                {
                    string productName = HttpUtility.HtmlDecode(imgNode.GetAttributeValue("alt", "").Trim());
                    string priceText = priceNode.InnerText.Replace("$", "").Trim();

                    string formattedRating = rating % 1 == 0 ? ((int)rating).ToString() : rating.ToString("0.0", CultureInfo.InvariantCulture); // format rating properly

                    products.Add(new Product
                    {
                        ProductName = productName,
                        Price = priceText,
                        Rating = formattedRating
                    });
                }

            }

            //output json
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
