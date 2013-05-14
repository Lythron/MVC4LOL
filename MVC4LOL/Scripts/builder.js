var _items;
var _data;

var bonusHealth = 0;
var bonusHealthRegen = 0;
var bonusMana = 0;
var bonusManaRegen = 0;
var bonusDamage = 0;
var bonusAttackSpeed = 0;
var bonusArmor = 0;
var bonusMagicResist = 0;
var criticalChance = 0;

var builder = function () {
    init = function (championId) {
        dataservice.loadBuilder(championId, renderStaff)
    },

    renderStaff = function (data) {

        _items = data.Items;

        renderChampion(data.Champion);

        renderItems(data);

        levelingInit(data);

        $('#Items img[id^="itemImage"]').click(function () {

            var div = $('div[id^="divItem"]:not(:has(img)):first');

            $("#" + this.id + ":first").clone().appendTo(div);

            div.click(function () {
                div.html("");
                updateStatsFromItems();
            });

            updateStatsFromItems();

        });
    },

    renderChampion = function (championData) {

        var date = championData.ReleaseDate;
        var milisecs = parseInt(date.replace("/Date(", "").replace(")/", ""));
        var d = new Date(milisecs);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = d.getFullYear() + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day;
        championData.ReleaseDate = output;

        $('#ChampionTemplate').tmpl(championData).appendTo('#Champion');
    },

    renderItems = function (data) {
        for (var key in data.Items) {

            var item = data.Items[key];
            renderItem(item);
        }
    },

    renderItem = function (item) {
        $('#ItemTemplate').tmpl(item).appendTo('#Items');
    },

    updateStatsFromItems = function () {
        var carriedItems = $('div[id^="divItem"] img');

        bonusHealth = 0;
        bonusHealthRegen = 0;
        bonusMana = 0;
        bonusManaRegen = 0;
        bonusDamage = 0;
        bonusAttackSpeed = 0;
        bonusArmor = 0;
        bonusMagicResist = 0;
        criticalChance = 0;

        for (var itemKey in carriedItems) {
            try {
                var item = carriedItems[itemKey].id.replace("itemImage", "");

                var intId = parseInt(item)

                var g = $.grep(_items, function (i) { return i.Id == intId })[0];

                bonusHealth += g.Health;
                bonusHealthRegen += g.HealthRegen;
                bonusMana += g.Mana;
                bonusManaRegen += g.ManaRegen;
                bonusDamage += g.Damage;
                bonusAttackSpeed += g.AttackSpeed;
                bonusArmor += g.Armor;
                bonusMagicResist += g.MagicResist;
                criticalChance += g.CriticalChance;
            }
            catch (excepion) {
            };
        }

        if (criticalChance > 100) {
            criticalChance = 100;
        }


        var lvl = parseInt($("#labelLvl").text());
        adjustLvl(lvl);

    },

    levelingInit = function (data) { 
        _data = data;
        $('input[name="buttonLvlUp"]').click(function () {
            increaseLvl();
        });
        $('input[name="buttonLvlDown"]').click(function () {
            decreaseLvl();
        });

        adjustLvl(1);
    },

    increaseLvl = function () {
        var lvl = parseInt($("#labelLvl").text());
        if (lvl < 18) {
            $("#labelLvl").text(lvl + 1);
            adjustLvl(lvl + 1);
        }
    },

    decreaseLvl = function () {
        var lvl = parseInt($("#labelLvl").text());
        if (lvl > 1) {
            $("#labelLvl").text(lvl - 1);
            adjustLvl(lvl - 1);
        }
    },

    adjustLvl = function (level) {

        var champ = _data.Champion;

        $('#labelHealth').text(champ.Health + champ.HealthPerLvl * level + bonusHealth);
        $('#labelHealthRegen').text(champ.HealthRegen + champ.HealthRegenPerLvl * level + bonusHealthRegen);
        $('#labelMana').text(champ.Mana + champ.ManaPerLvl * level + bonusMana);
        $('#labelManaRegen').text(champ.ManaRegen + champ.ManaRegenPerLvl * level + bonusManaRegen);
        $('#labelDamage').text(champ.Damage + champ.DamagePerLvl * level + bonusDamage);
        var computedAttackSpeed = champ.AttackSpeed * (1 + champ.AttackSpeedPerLvl / 100 * level + bonusAttackSpeed)
        if (computedAttackSpeed > 2.5) {
            computedAttackSpeed = 2.5;
        };
        $('#labelAttackSpeed').text(computedAttackSpeed);
        $('#labelArmor').text(champ.Armor + champ.ArmorPerLvl * level + bonusArmor);
        $('#labelMagicResist').text(champ.MagicResist + champ.MagicResistPerLvl * level + bonusMagicResist);
        $('#labelCriticalChance').text(criticalChance);

        renderDamage();

    },

    computeDamage = function (armor) {
        var damage = parseFloat($("#labelDamage").text());
        var attackSpeed = parseFloat($("#labelAttackSpeed").text());
        var attackCritChance = parseFloat($("#labelCriticalChance").text());
        var attackCritDamage = parseFloat($("#labelCriticalDamage").text());
        var attackSpeed = parseFloat($("#labelAttackSpeed").text());
        var percentageArmorPen = parseFloat($("#labelPercentageArmorPen").text());
        var flatArmorPen = parseFloat($("#labelFlatArmorPen").text());

        var effectiveDamage = damage * attackSpeed * ( 1 +  (attackCritChance/100 * (attackCritDamage - 1)));
        var effectiveArmor = armor * (1 - percentageArmorPen) - flatArmorPen;

        var damageDone = (effectiveDamage * 100) / (100 + effectiveArmor);

        return damageDone;
    },

    renderDamage = function () {
        var noArmor = parseFloat($("#labelNoArmorArmor").text());
        $('#labelNoArmorDamage').text(computeDamage(noArmor));
        var lightArmor = parseFloat($("#labelLightArmorArmor").text());
        $('#labelLightArmorDamage').text(computeDamage(lightArmor));
        var mediumArmor = parseFloat($("#labelMediumArmorArmor").text());
        $('#labelMediumArmorDamage').text(computeDamage(mediumArmor));
        var heavyArmor = parseFloat($("#labelHeavyArmorArmor").text());
        $('#labelHeavyArmorDamage').text(computeDamage(heavyArmor));
    }

    return {
        init: init,
        renderStaff: renderStaff,
        renderChampion: renderChampion,
        renderItems: renderItems,
        renderItem: renderItem,
        upateStatsFromItems: updateStatsFromItems,

        levelingInit: levelingInit,
        increaseLvl: increaseLvl,
        decreaseLvl: decreaseLvl,
        adjustLvl: adjustLvl,

        computeDamage: computeDamage,
        renderDamage: renderDamage
    }
}();
