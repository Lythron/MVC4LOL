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

            item.Description = String.Empty;

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
            if (infoDiv == null) return;

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

                using (MVC4LOLDb db = new MVC4LOLDb())
                {
                    if (db.Items.Select(o => o.Name).Contains(item.Name))
                    {
                        return;   
                    }
                }
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

            //try
            //{
            //    HtmlNodeCollection descNodes = infoDiv.SelectNodes("descendant::tr/th[contains(.,'Stats')]/following-sibling::td | descendant::tr/th[a[@href='/wiki/Item#Secondary_effects']]/following-sibling::td");
            //    if (descNodes == null) return;
            //    String descStr = String.Empty;
            //    foreach (HtmlNode node in descNodes)
            //    {
            //        descStr += node.InnerText.Trim() + System.Environment.NewLine;
            //    }

            //    Regex regHealthRegen = new Regex("([\\d]+)[\\s]*health regeneration");
            //    if (regHealthRegen.IsMatch(descStr))
            //    {
            //        item.HealthRegen = Decimal.Parse(regHealthRegen.Match(descStr).Groups[1].Value);
            //        descStr = descStr.Replace(regHealthRegen.Match(descStr).Groups[0].Value, String.Empty);
            //    }

            //    Regex regHealth = new Regex("([\\d]+)[\\s]*health");
            //    if (regHealth.IsMatch(descStr))
            //    {
            //        item.Health = Decimal.Parse(regHealth.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regDamage = new Regex("([\\d]+)[\\s]*attack damage");
            //    if (regDamage.IsMatch(descStr))
            //    {
            //        item.Damage = Decimal.Parse(regDamage.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regAttackSpeed = new Regex("([\\d]+)%?[\\s]*attack speed");
            //    if (regAttackSpeed.IsMatch(descStr))
            //    {
            //        item.AttackSpeed = Decimal.Parse(regAttackSpeed.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regManaRegen = new Regex("([\\d]+)%?[\\s]*mana regen");
            //    if (regManaRegen.IsMatch(descStr))
            //    {
            //        item.ManaRegen = Decimal.Parse(regManaRegen.Match(descStr).Groups[1].Value);
            //        descStr = descStr.Replace(regManaRegen.Match(descStr).Groups[0].Value, String.Empty);
            //    }

            //    Regex regMana = new Regex("([\\d]+)%?[\\s]*mana");
            //    if (regMana.IsMatch(descStr))
            //    {
            //        item.Mana = Decimal.Parse(regMana.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regArmor = new Regex("([\\d]+)%?[\\s]*armor");
            //    if (regArmor.IsMatch(descStr))
            //    {
            //        item.Armor = Decimal.Parse(regArmor.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regMagicResist = new Regex("([\\d]+)%?[\\s]*magic resist");
            //    if (regMagicResist.IsMatch(descStr))
            //    {
            //        item.MagicResist = Decimal.Parse(regMagicResist.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regAbilityPower = new Regex("([\\d]+)%?[\\s]*ability power");
            //    if (regAbilityPower.IsMatch(descStr))
            //    {
            //        item.AbilityPower = Decimal.Parse(regAbilityPower.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regCritChance = new Regex("([\\d]+)%?[\\s]*critical strike chance");
            //    if (regCritChance.IsMatch(descStr))
            //    {
            //        item.CriticalChance = Decimal.Parse(regCritChance.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regLifeSteal = new Regex("([\\d]+)%?[\\s]*life steal");
            //    if (regLifeSteal.IsMatch(descStr))
            //    {
            //        item.LifeSteal = Decimal.Parse(regLifeSteal.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regSpellVamp = new Regex("([\\d]+)%?[\\s]*spell vamp");
            //    if (regLifeSteal.IsMatch(descStr))
            //    {
            //        item.SpellVamp = Decimal.Parse(regSpellVamp.Match(descStr).Groups[1].Value);
            //    }

            //    Regex regCooldownReduction = new Regex("([\\d]+)%?[\\s]*cooldown reduction");
            //    if (regLifeSteal.IsMatch(descStr))
            //    {
            //        item.CooldownReduction = Decimal.Parse(regCooldownReduction.Match(descStr).Groups[1].Value);
            //    }

            //}
            //catch
            //{ 
            //}

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("health regeneration"))
                {
                    Regex regHealthRegen = new Regex("([\\d]+)[\\s]*health regeneration");
                    String h = regHealthRegen.Match(stats).Groups[1].Value;

                    stats = stats.Replace(regHealthRegen.Match(stats).Groups[0].Value, String.Empty);
                    item.HealthRegen = Decimal.Parse(h);
                }

                if (stats.Contains("health"))
                {
                    Regex regHealth = new Regex("([\\d]+)[\\s]*health");
                    String h = regHealth.Match(stats).Groups[1].Value;
                    item.Health = Decimal.Parse(h);
                }
                item.Description = stats;
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
                    item.AttackSpeed = Decimal.Parse(h)/100; // ?
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
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("armor"))
                {
                    Regex regArmor = new Regex("([\\d]+)%?[\\s]*armor");
                    String h = regArmor.Match(stats).Groups[1].Value;
                    item.Armor = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("magic resist"))
                {
                    Regex regMagicResist = new Regex("([\\d]+)%?[\\s]*magic resist");
                    String h = regMagicResist.Match(stats).Groups[1].Value;
                    item.MagicResist = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("ability power"))
                {
                    Regex regAbilityPower = new Regex("([\\d]+)%?[\\s]*ability power");
                    String h = regAbilityPower.Match(stats).Groups[1].Value;
                    item.AbilityPower = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("critical strike chance"))
                {
                    Regex regCritChance = new Regex("([\\d]+)%?[\\s]*critical strike chance");
                    String h = regCritChance.Match(stats).Groups[1].Value;
                    item.CriticalChance = Decimal.Parse(h);
                }
            }
            catch
            {
            }
            
            // only IE and its in passives not stats.
            //try
            //{
            //    String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
            //    if (stats.Contains("critical strike damage"))
            //    {
            //        Regex regCritDamage = new Regex("([\\d]+)%?[\\s]*critical strike damage");
            //        String h = regCritDamage.Match(stats).Groups[1].Value;
            //        item.CriticalDamage = Decimal.Parse(h);
            //    }
            //}
            //catch
            //{
            //}

            try
            {
                HtmlNodeCollection descNodes = infoDiv.SelectNodes("descendant::tr/th[a[@href='/wiki/Item#Secondary_effects']]/following-sibling::td");

                String desc = String.Empty;

                foreach (var node in descNodes)
                {
                    desc += node.InnerText.Trim();
                }

                item.Description += desc;

                Regex regLifeSteal = new Regex("([\\d]+)%?[\\s]*life steal");
                String h = regLifeSteal.Match(desc).Groups[1].Value;
                item.LifeSteal = Decimal.Parse(h);
            
            }
            catch
            {
            }

            try
            {
                HtmlNodeCollection descNodes = infoDiv.SelectNodes("descendant::tr/th[a[@href='/wiki/Item#Secondary_effects']]/following-sibling::td");

                String desc = String.Empty;

                foreach (var node in descNodes)
                {
                    desc += node.InnerText.Trim();
                }

                Regex regSpellVamp = new Regex("([\\d]+)%?[\\s]*spell vamp");
                String hs = regSpellVamp.Match(desc).Groups[1].Value;
                item.SpellVamp = Decimal.Parse(hs);
            }
            catch
            {
            }

            try
            {
                HtmlNodeCollection descNodes = infoDiv.SelectNodes("descendant::tr/th[a[@href='/wiki/Item#Secondary_effects']]/following-sibling::td");

                String desc = String.Empty;

                foreach (var node in descNodes)
                {
                    desc += node.InnerText.Trim();
                }

                Regex regCooldownReduction = new Regex("([\\d]+)%?[\\s]*cooldown reduction");
                String hc = regCooldownReduction.Match(desc).Groups[1].Value;
                item.CooldownReduction = Decimal.Parse(hc);

            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("life steal"))
                {
                    Regex regLifeSteal = new Regex("([\\d]+)%?[\\s]*life steal");
                    String h = regLifeSteal.Match(stats).Groups[1].Value;
                    item.LifeSteal = Decimal.Parse(h);
                }
            }
            catch
            {
            }

            try
            {
                String stats = infoDiv.SelectSingleNode("descendant::tr/th[contains(.,'Stats')]/following-sibling::td").InnerText.Trim();
                if (stats.Contains("spell vamp"))
                {
                    Regex regSpellVamp = new Regex("([\\d]+)%?[\\s]*spell vamp");
                    String h = regSpellVamp.Match(stats).Groups[1].Value;
                    item.SpellVamp = Decimal.Parse(h);
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
