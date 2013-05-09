using HtmlAgilityPack;
using MVC4LOL.Model;
using MVC4LOL.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVC4LOL.CRAWLER
{
    public class ItemsWikia
    {
        private const String siteUrl = "http://leagueoflegends.wikia.com";

        public ItemsWikia()
        { 
        }

        public static void HarvestData()
        {
            HtmlDocument doc = new HtmlDocument();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/wiki/Item");
            //request.Timeout = 10000;
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    String html = streamReader.ReadToEnd();
                    doc.LoadHtml(html);
                }
            }
            
            HtmlNodeCollection itemsAList = doc.DocumentNode.SelectNodes("//a[img[contains(@alt, 'item')]]");
            foreach (var a in itemsAList)
            {
                var url = a.GetAttributeValue("href", String.Empty);
                HarvestItem(url);
            }
        }

        private static void HarvestItem(string url)
        {

            HtmlDocument doc = new HtmlDocument();
            Item item = new Item();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(siteUrl + url);
            request.Timeout = 10000;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    String html = stream.ReadToEnd();
                    doc.LoadHtml(html);
                }
            }
            
            HtmlNode infoDiv = doc.DocumentNode.SelectSingleNode("//div[@class='infobox']");

            try 
            {
                var img = infoDiv.SelectSingleNode("div/a/img");
                String imgSrc = img.GetAttributeValue("src", String.Empty);
                HttpWebRequest reqImg = HttpWebRequest.CreateHttp(imgSrc);
                HttpWebResponse resImg = (HttpWebResponse)reqImg.GetResponse();
                item.Image = Helpers.ReadToEnd(resImg.GetResponseStream());
            }
            catch
            {
            }

            try
            {
                item.Name = infoDiv.SelectSingleNode("descendant::div/b").InnerText.Trim();
            }
            catch
            { 
            }

            try
            {
                item.Availability = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Availability')]/following-sibling::td").InnerText.Trim();
            }
            catch
            { 
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("health regeneration"))
                {
                    Regex regHealthRegen = new Regex("([\\d]+)[\\s]*health regeneration");
                    String h = regHealthRegen.Match(stats).Groups[1].Value;

                    stats = stats.Replace(regHealthRegen.Match(stats).Groups[0].Value, String.Empty);
                    item.Health = Decimal.Parse(h);
                }

                if (stats.Contains("health"))
                {
                    Regex regHealth = new Regex("([\\d]+)[\\s]*health");
                    String h = regHealth.Match(stats).Groups[1].Value;
                    item.Health = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("attack damage"))
                {
                    Regex regDamage = new Regex("([\\d]+)[\\s]*attack damage");
                    String h = regDamage.Match(stats).Groups[1].Value;
                    item.Damage = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("attack speed"))
                {
                    Regex regAttackSpeed = new Regex("([\\d]+)%?[\\s]*attack speed");
                    String h = regAttackSpeed.Match(stats).Groups[1].Value;
                    item.AttackSpeed = Decimal.Parse(h)/100;
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("mana regen"))
                {
                    Regex regManaRegen = new Regex("([\\d]+)%?[\\s]*mana regen");
                    String h = regManaRegen.Match(stats).Groups[1].Value;
                    stats = stats.Replace(regManaRegen.Match(stats).Groups[0].Value, String.Empty);
                    item.ManaRegen = Decimal.Parse(h);
                }
                if (stats.Contains("mana"))
                {
                    Regex regMana = new Regex("([\\d]+)%?[\\s]*mana");
                    String h = regMana.Match(stats).Groups[1].Value;
                    item.Mana = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String costStr = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Item cost')]/following-sibling::td").InnerText.Trim();
                item.Cost = Int32.Parse(costStr.Substring(0, costStr.IndexOf("g")));
            }
            catch
            {
            }

            using (MVC4LOLDb db = new MVC4LOLDb())
            {
                db.Items.Add(item);
                db.SaveChanges();
            }
        }

    }
}
