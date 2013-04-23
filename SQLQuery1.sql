select * from Champion;
select * from ChampionData;
select * from PatchVersion;

select * from Tag where ChampionId = 826;

select * from UserProfile;
select * from webpages_Roles; insert webpages_Roles values ('Admin')
select * from webpages_Membership;
select * from webpages_OAuthMembership;
select * From webpages_UsersInRoles; insert webpages_UsersInRoles values (2, 1)


Select d.* 
From Champion c
Inner join ChampionData d on c.Id = d.ChampionId 
			and d.PatchVersionId = (Select max(id) from PatchVersion)

