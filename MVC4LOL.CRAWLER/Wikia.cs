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
using System.Web;
//using MVC4LOL.Filters;

namespace MVC4LOL.CRAWLER
{
    public class Wikia
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
            doc.LoadHtml(html);

            HtmlNodeCollection champsLi = doc.DocumentNode.SelectNodes("//ol[@class='champion_roster']/li");
            foreach (HtmlNode champ in champsLi)
            {
                HtmlNode aNode = champ.SelectSingleNode("span/a");
                String urlChamp = aNode.GetAttributeValue("href", null);
                HarvestChamp(urlChamp);
            }
        }


        private static Decimal Parse(string input)
        {
            Decimal output = 0;
            try
            {
                output = Decimal.Parse(input.Replace('.', ','));
            }
            catch
            {
            }
            return output;
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
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
            doc.LoadHtml(html);

            ChampionData champion = new ChampionData();

            champion.PatchVersionId = 2; // HARDCODE;


            HtmlNode upperTable = doc.DocumentNode.SelectSingleNode("//table[@id='champion_info-upper']");

            try
            {
                String imgSrc = upperTable.SelectSingleNode("descendant::a[@class='image']/img").GetAttributeValue("src", String.Empty);
                HttpWebRequest reqImg = HttpWebRequest.CreateHttp(imgSrc);
                HttpWebResponse resImg = (HttpWebResponse)reqImg.GetResponse();
                champion.Image = ReadToEnd(resImg.GetResponseStream());
            }
            catch
            {
            }

            try
            {
                champion.Name = upperTable.SelectSingleNode("tr/td/span").InnerText;
                champion.Title = upperTable.SelectSingleNode("tr/td/span/following-sibling::span").InnerText;

                using (MVC4LOLDb us = new MVC4LOLDb())
                {
                    Champion champ = us.Champions.FirstOrDefault(o => o.Name == champion.Name);
                    if (champ != null)
                    {
                        champion.ChampionId = champ.Id;
                    }
                    else
                    { 
                        Champion ch = new Champion();
                        ch.Name = champion.Name;
                        us.Champions.Add(ch);
                        us.SaveChanges();
                        champion.ChampionId = ch.Id;
                    }
                    
                }

            }
            catch
            {
            }

            try
            {
                champion.ReleaseDate = DateTime.Parse(upperTable.SelectSingleNode("descendant::td[contains(.,'Release date')]/following-sibling::td/a").InnerText);
            }
            catch
            {
                champion.ReleaseDate = DateTime.Parse("2010-03-01");
            }

            try
            {
                champion.IP_Cost = Int32.Parse(upperTable.SelectSingleNode("descendant::a[contains(@href,'IP_Champion')]").InnerText);
            }
            catch
            {
            }

            try
            {
                champion.RP_Cost = Int32.Parse(upperTable.SelectSingleNode("descendant::a[contains(@href,'RP_Champion')]").InnerText);
            }
            catch
            {
            }

            //champion.Skills = new List<Skill>();

            HtmlNode tableStatistic = doc.DocumentNode.SelectSingleNode("//table[tr/th/span[span = 'Statistics']]");

            try
            {
                String healthString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Health']]/following-sibling::td").InnerText.Trim();
                Match healthMatch = Regex.Match(healthString, @"([\d]*) ?\( ?\+([\d]*) ?\)");
                champion.Health = Parse(healthMatch.Groups[1].Value.Replace('.', ','));
                champion.HealthPerLvl = Parse(healthMatch.Groups[2].Value.Replace('.', ','));
            }
            catch
            {
            }

            try
            {
                String healthRegenString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Health regen.']]/following-sibling::td").InnerText.Trim();
                Match healthRegenMatch = Regex.Match(healthRegenString, @"([\d\.]*) ?\( ?\+([\d\.]*) ?\)");
                champion.HealthRegen = Parse(healthRegenMatch.Groups[1].Value);
                champion.HealthRegenPerLvl = Parse(healthRegenMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String attackDamageString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Attack damage']]/following-sibling::td").InnerText.Trim();
                Match attackDamageMatch = Regex.Match(attackDamageString, @"([\d\.]*) ?\( ?\+([\d\.]*) ?\)");
                champion.Damage = Parse(attackDamageMatch.Groups[1].Value);
                champion.DamagePerLvl = Parse(attackDamageMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String attackSpeedString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Attack speed']]/following-sibling::td").InnerText.Trim();
                Match attackSpeedMatch = Regex.Match(attackSpeedString, @"([\d\.]*) ?\( ?\+([\d\.]*)% ?\)");
                champion.AtkSpeed = Parse(attackSpeedMatch.Groups[1].Value);
                champion.AtkSpeedPerLvl = Parse(attackSpeedMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String manaString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Mana']]/following-sibling::td").InnerText.Trim();
                Match manaMatch = Regex.Match(manaString, @"([\d\.]*) ?\( ?\+([\d\.]*) ?\)");
                champion.Mana = Parse(manaMatch.Groups[1].Value);
                champion.ManaPerLvl = Parse(manaMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String manaRegenString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Mana regen.']]/following-sibling::td").InnerText.Trim();
                Match manaRegenMatch = Regex.Match(manaRegenString, @"([\d\.]*) ?\( ?\+([\d\.]*) ?\)");
                champion.ManaRegen = Parse(manaRegenMatch.Groups[1].Value);
                champion.ManaRegenPerLvl = Parse(manaRegenMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String armorString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Armor']]/following-sibling::td").InnerText.Trim();
                Match armorMatch = Regex.Match(armorString, @"([\d\.]*) ?\( ?\+([\d\.]*) ?\)");
                champion.Armor = Parse(armorMatch.Groups[1].Value);
                champion.ArmorPerLvl = Parse(armorMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String magicResistString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Magic res.']]/following-sibling::td").InnerText.Trim();
                Match magicResistMatch = Regex.Match(magicResistString, @"([\d\.]*) ?\( ?\+([\d\.]*) ?\)");
                champion.MagicResist = Parse(magicResistMatch.Groups[1].Value);
                champion.MagicResistPerLvl = Parse(magicResistMatch.Groups[2].Value);
            }
            catch
            {
            }

            try
            {
                String moveSpeedString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Mov. speed']]/following-sibling::td").InnerText.Trim();
                Match moveSpeedMatch = Regex.Match(moveSpeedString, @"([\d\.]*)");
                champion.MoveSpeed = Parse(moveSpeedMatch.Groups[1].Value);
            }
            catch
            {
            }

            try
            {
                String rangeString = tableStatistic.SelectSingleNode("//tr/td[span[a = 'Range']]/following-sibling::td").InnerText.Trim();
                Match rangeMatch = Regex.Match(rangeString, @"([\d\.]*)");
                champion.Range = Parse(rangeMatch.Groups[1].Value);
            }
            catch
            {
            }

            using (MVC4LOLDb cx = new MVC4LOLDb())
            {
                cx.ChampionData.Add(champion);
                cx.SaveChanges();
            }

        }

    }
}
