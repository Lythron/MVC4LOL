select * from Champion;
select * from ChampionData where ChampionId = 800; 

update ChampionData set BaseAttackSpeed = AttackSpeed;

select baseattackSpeed, AttackSpeed from ChampionData;

select Name, CooldownReduction from item where CooldownReduction > 0;

select * from item;
--delete from item;

select * from itemRecipe

select distinct [availability] from item;

update item set Name = Replace(Name, '&#39;', '''' ) -- now in code;

select i.Name Item, i.Cost , j.Name Component, j.Cost, r.RecipeCost as Cost
from ItemRecipe r
left join item i on r.ItemId = i.Id 
left join item j on r.ComponentId = j.Id

--select REPLACE('Summoner''s Rift', ' Summoner''sRift ', )  from item
update item set [Availability] = Replace([Availability], 'Summoner''s Rift', ' Summoner''sRift ')
update item set [Availability] = Replace([Availability], 'Howling Abyss', ' HowlingAbyss ')
update item set [Availability] = Replace([Availability], 'Twisted Treeline', ' TwistedTreeline ')
update item set [Availability] = Replace([Availability], '  ', ' ')
update item set [Availability] = Replace([Availability], 'Summoner''sRift', 'SummonersRift') --now done in crawler : TEST
update item set Availability = LTRIM(RTRIM(Availability));

select * from PatchVersion;

select * from Tag where ChampionId = 826;

select * from UserProfile;
select * from webpages_Roles;
select * from webpages_Membership;
select * from webpages_OAuthMembership;
select * From webpages_UsersInRoles; insert webpages_UsersInRoles values (2, 1)

Select d.* 
From Champion c
Inner join ChampionData d on c.Id = d.ChampionId 
			and d.PatchVersionId = (Select max(id) from PatchVersion)

