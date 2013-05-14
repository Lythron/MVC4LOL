//var _items;

//var bonusHealth = 0;
//var bonusHealthRegen = 0;
//var bonusMana = 0;
//var bonusManaRegen = 0;
//var bonusDamage = 0;
//var bonusAttackSpeed = 0;
//var bonusArmor = 0;
//var bonusMagicResist = 0;

//var detailsBuilder = function () {
//    init = function (championId) {
//        dataservice.loadBuilder(championId, renderStaff)
//    },

//    renderStaff = function (data) {

//        _items = data.Items;

//        renderChampion(data.Champion);

//        renderItems(data);

//        levelingBuilder.init(data);

//        $('#Items img[id^="itemImage"]').click(function () {

//            var div = $('div[id^="divItem"]:not(:has(img)):first');

//            $("#" + this.id + ":first").clone().appendTo(div);

//            div.click(function () {
//                div.html("");
//                updateStatsFromItems();
//            });

//            updateStatsFromItems(); 

//        });
//    },
    
//    renderChampion = function (championData)
//    {

//        var date = championData.ReleaseDate;
//        var milisecs = parseInt(date.replace("/Date(", "").replace(")/", ""));
//        var d = new Date(milisecs);
//        var month = d.getMonth() + 1;
//        var day = d.getDate();
//        var output = d.getFullYear() + '/' +
//            (('' + month).length < 2 ? '0' : '') + month + '/' +
//            (('' + day).length < 2 ? '0' : '') + day;
//        championData.ReleaseDate = output;

//        $('#ChampionTemplate').tmpl(championData).appendTo('#Champion');
//    },

//    renderItems = function (data) {
//        for (var key in data.Items) {

//            var item = data.Items[key];
//            renderItem(item);
//        }
//    },

//    renderItem = function (item) {
//        $('#ItemTemplate').tmpl(item).appendTo('#Items');
//    },

//    updateStatsFromItems = function () {
//        var carriedItems = $('div[id^="divItem"] img');

//        bonusHealth = 0;
//        bonusHealthRegen = 0;
//        bonusMana = 0;
//        bonusManaRegen = 0;
//        bonusDamage = 0;
//        bonusAttackSpeed = 0;
//        bonusArmor = 0;
//        bonusMagicResist = 0;

//        for (var itemKey in carriedItems)
//        {
//            try {
//                var item = carriedItems[itemKey].id.replace("itemImage", "");

//                var intId = parseInt(item)

//                var g = $.grep(_items, function (i) { return i.Id == intId })[0];

//                bonusHealth += g.Health;
//                bonusHealthRegen += g.HealthRegen;
//                bonusMana += g.Mana;
//                bonusManaRegen += g.ManaRegen;
//                bonusDamage += g.Damage;
//                bonusAttackSpeed += g.AttackSpeed;
//                bonusArmor += g.Armor;
//                bonusMagicResist += g.MagicResist;
//            }
//            catch (excepion)
//            {
//            };
//        }

//        $('#labelHealth').text(bonusHealth);
//        $('#labelHealthRegen').text(bonusHealthRegen);
//        $('#labelMana').text(bonusMana);
//        $('#labelManaRegen').text(bonusManaRegen);
//        $('#labelDamage').text(bonusDamage);
//        $('#labelAttackSpeed').text(bonusAttackSpeed);
//        $('#labelArmor').text(bonusArmor);
//        $('#labelMagicResist').text(bonusMagicResist);

//    }

//    return {
//        init: init,
//        renderStaff: renderStaff,
//        renderChampion: renderChampion,
//        renderItems: renderItems,
//        renderItem: renderItem,
//        upateStatsFromItems: updateStatsFromItems
//    }

//}();