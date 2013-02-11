using HtmlAgilityPack;
using MVC4LOL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


using System.Web;
using MVC4LOL.Filters;
using MVC4LOL.Models;


namespace MVC4LOL.CRAWLER
{
    class Wikia
    {
        private const String siteUrl = "http://leagueoflegends.wikia.com";

        public Wikia()
        { 
        }

        public static void HarvestData()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(siteUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream());
            String html = stream.ReadToEnd();
           
            HtmlDocument doc = new HtmlDocument();
            doc.Load(html);

            HtmlNodeCollection champsLi = doc.DocumentNode.SelectNodes("//ol[@class='champion_roster']/li");
            foreach (HtmlNode champ in champsLi)
            {
                HtmlNode aNode = champ.SelectSingleNode("span/a");
                String urlChamp = aNode.GetAttributeValue("href", null);
                HarvestChamp(urlChamp);
            }
        }

        private static void HarvestChamp(string urlChamp)
        {
            String url = siteUrl + urlChamp;
            HttpWebRequest request = HttpWebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String html = reader.ReadToEnd();


            HtmlDocument doc = new HtmlDocument();

            String health = "0";
            String healthPerLvl = "0";
            String healthRegen = "0";
            String healthRegenPerLvl = "0";

            HtmlNode upperTable = doc.DocumentNode.SelectSingleNode("//table[@id='champion_info_upper']");

            String name = upperTable.SelectSingleNode("tr/td/span").InnerText;
            // TODO UpperTable

            HtmlNode tableStatistic = doc.DocumentNode.SelectSingleNode("//table[tr/th/span[span = 'Statistics']");

            try
            {
                String healthString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Health']]/following-sibling::td").InnerText.Trim();
                Match healthMatch = Regex.Match(healthString, "([d]*?)(+([d]*?))");
                health = healthMatch.Groups[0].Value;
                healthPerLvl = healthMatch.Groups[1].Value;
            }
            catch
            {
            }

            try
            {
                String healthString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Health regen.']]/following-sibling::td").InnerText.Trim();
                Match healthMatch = Regex.Match(healthString, "([d]*?)(+([d]*?))");
                healthRegen = healthMatch.Groups[0].Value;
                healthRegenPerLvl = healthMatch.Groups[1].Value;
            }
            catch
            {
            }

            // TODO Statistics table
            
            Champion champ = new Champion()
            {
                Name = name,
                Health = Int32.Parse(health),
                HealthPerLvl = Int32.Parse(healthPerLvl),
                HealthRegen = Int32.Parse(healthRegen),
                HealthRegenPerLvl = Int32.Parse(healthRegenPerLvl)
            };

            using (UsersContext cx = new UsersContext())
            {
                cx.Champions.Add(champ);
            }

            

        }






    }
}
