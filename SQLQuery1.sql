select * from Champion;
select * from ChampionData where ChampionId = 800; 

update ChampionData set BaseAttackSpeed = AttackSpeed;

select baseattackSpeed, AttackSpeed from ChampionData;

select Name, CooldownReduction from item where CooldownReduction > 0;
select * from item where CriticalChance > 0;
select * From skill;

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

