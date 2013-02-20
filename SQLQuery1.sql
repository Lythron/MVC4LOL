select * from Champion;
select * from ChampionData;
select * from PatchVersion;

select * from Tag;


Select d.* 
From Champion c
Inner join ChampionData d on c.Id = d.ChampionId 
			and d.PatchVersionId = (Select max(id) from PatchVersion)

