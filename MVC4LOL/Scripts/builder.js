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
var bonusAbilityPower = 0;
var itemsLifeSteal = 0;
var itemsSpellVamp = 0;
var itemsCooldownReduction = 0;

var builder = function () {
    init = function (championId) {
        dataservice.loadBuilder(championId, renderStaff)
    },

    renderStaff = function (data) {

        _items = data.Items;

        renderChampion(data.Champion);

        renderItems(data);

        levelingInit(data);

        var mapSelected = $("#selectMap option:selected").text().replace(" ", "").replace("'", "");
        var itemClass = "." + mapSelected;
        $("#Items img[id^='itemImage']").not('[class*="Common"]').hide();
        $("#Items img[id^='itemImage']").filter(itemClass).show();

        $("#selectMap").change(function () {
            var mapSelected = $("#selectMap option:selected").text().replace(" ","").replace("'",""); 
            var itemClass = "." + mapSelected;
            $("#Items img[id^='itemImage']").not('[class*="Common"]').hide();
            $("#Items img[id^='itemImage']").filter(itemClass).show();
        });

        // TODO, Combine with map classes, all item tags include, refactor
        $("#InputItemDamage").change(function () {
            var selectedItemTags = $("#InputItemDamage:checked");
            if (selectedItemTags.length > 0) {
                var tagValue = ".Tag" + selectedItemTags[0].value;
                $("#Items img[id^='itemImage']").hide();
                $("#Items img[id^='itemImage']").filter(tagValue).show();
            }
            else {
                $("#Items img[id^='itemImage']").show();
            }
        });

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

        for (var prop in item) {

            if ($.inArray(prop, ["Damage", "Health"]) > -1 && item[prop] > 0) 
            {
                var tagClass = "Tag" + prop;
                $('img[id=itemImage' + item.Id + ']').addClass(tagClass);
            }
        }

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
        bonusAbilityPower = 0;
        itemsLifeSteal = 0;
        itemsSpellVamp = 0;
        itemsCooldownReduction = 0;

        for (var itemKey in carriedItems) {
            try {
                var itemId = carriedItems[itemKey].id.replace("itemImage", "");

                var intId = parseInt(itemId)

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
                bonusAbilityPower += g.AbilityPower;
                itemsLifeSteal += g.LifeSteal;
                itemsSpellVamp += g.SpellVamp;
                itemsCooldownReduction += g.CooldownReduction;
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
        var computedAttackSpeed = champ.AttackSpeed * (1 + champ.AttackSpeedPerLvl / 100 * level + bonusAttackSpeed / 100)
        if (computedAttackSpeed > 2.5) {
            computedAttackSpeed = 2.5;
        };
        $('#labelAttackSpeed').text(computedAttackSpeed);
        $('#labelArmor').text(champ.Armor + champ.ArmorPerLvl * level + bonusArmor);
        $('#labelMagicResist').text(champ.MagicResist + champ.MagicResistPerLvl * level + bonusMagicResist);
        $('#labelLifeSteal').text(itemsLifeSteal);
        $('#labelSpellVamp').text(itemsSpellVamp);
        $('#labelCriticalChance').text(criticalChance);
        $('#labelAbilityPower').text(bonusAbilityPower);
        var computedCooldownReduction = itemsCooldownReduction;
        if (computedCooldownReduction > 40)
        {
            computedCooldownReduction = 40;
        }
        $('#labelCooldownReduction').text(computedCooldownReduction);

        renderDamage();
        renderHealth();

    },

    computeDamage = function (armor) {
        var damage = parseFloat($("#labelDamage").text());
        //var attackSpeed = parseFloat($("#labelAttackSpeed").text());
        var attackCritChance = parseFloat($("#labelCriticalChance").text());
        var attackCritDamage = parseFloat($("#labelCriticalDamage").text());
        var percentageArmorPen = parseFloat($("#labelPercentageArmorPen").text());
        var flatArmorPen = parseFloat($("#labelFlatArmorPen").text());

        var effectiveDamage = damage * (1 + (attackCritChance / 100 * (attackCritDamage - 1)));
        var effectiveArmor = armor * (1 - percentageArmorPen) - flatArmorPen;

        var damageDone = (effectiveDamage * 100) / (100 + effectiveArmor);

        return damageDone;
    },

    computeDPS = function (armor) {
        var attackSpeed = parseFloat($("#labelAttackSpeed").text());

        return computeDamage(armor) * attackSpeed;
    },

    renderDamage = function () {
        var lifeSteal = parseFloat($('#labelLifeSteal').text()) / 100;
        var noArmor = parseFloat($("#labelNoArmorArmor").text());
        $('#labelNoArmorDamage').text(computeDamage(noArmor));
        $('#labelNoArmorDPS').text(computeDPS(noArmor));
        $('#labelNoArmorLifeSteal').text(computeDPS(noArmor) * lifeSteal);
        var lightArmor = parseFloat($("#labelLightArmorArmor").text());
        $('#labelLightArmorDamage').text(computeDamage(lightArmor));
        $('#labelLightArmorDPS').text(computeDPS(lightArmor));
        $('#labelLightArmorLifeSteal').text(computeDPS(lightArmor) * lifeSteal);
        var mediumArmor = parseFloat($("#labelMediumArmorArmor").text());
        $('#labelMediumArmorDamage').text(computeDamage(mediumArmor));
        $('#labelMediumArmorDPS').text(computeDPS(mediumArmor));
        $('#labelMediumArmorLifeSteal').text(computeDPS(mediumArmor) * lifeSteal);
        var heavyArmor = parseFloat($("#labelHeavyArmorArmor").text());
        $('#labelHeavyArmorDamage').text(computeDamage(heavyArmor));
        $('#labelHeavyArmorDPS').text(computeDPS(heavyArmor));
        $('#labelHeavyArmorLifeSteal').text(computeDPS(heavyArmor) * lifeSteal);
    },

    computeHealth = function (percentagePen, flatPen, resist, health) {
        var effectiveResist = (resist * (1 - percentagePen / 100) - flatPen);
        if (effectiveResist < 0)
        {
            effectiveResist = 0;
        }
        var effectiveHealth = health * (1 + effectiveResist / 100);
        return effectiveHealth;
    };

    renderHealth = function () {
        var health = parseFloat($("#labelHealth").text());
        var armor = parseFloat($("#labelArmor").text());
        var magicResist = parseFloat($("#labelMagicResist").text());

        var noPercentageArmorPen = parseFloat($("#labelNoPercentageArmorPen").text());
        var noFlatArmorPen = parseFloat($("#labelNoFlatArmorPen").text());
        $('#labelEffectiveHealthNoArmorPen').text(computeHealth(noPercentageArmorPen, noFlatArmorPen, armor, health));
        var lightPercentageArmorPen = parseFloat($("#labelLightPercentageArmorPen").text());
        var lightFlatArmorPen = parseFloat($("#labelLightFlatArmorPen").text());
        $('#labelEffectiveHealthLightArmorPen').text(computeHealth(lightPercentageArmorPen, lightFlatArmorPen, armor, health));
        var mediumPercentageArmorPen = parseFloat($("#labelMediumPercentageArmorPen").text());
        var mediumFlatArmorPen = parseFloat($("#labelMediumFlatArmorPen").text());
        $('#labelEffectiveHealthMediumArmorPen').text(computeHealth(mediumPercentageArmorPen, mediumFlatArmorPen, armor, health));
        var heavyPercentageArmorPen = parseFloat($("#labelHeavyPercentageArmorPen").text());
        var heavyFlatArmorPen = parseFloat($("#labelHeavyFlatArmorPen").text());
        $('#labelEffectiveHealthHeavyArmorPen').text(computeHealth(heavyPercentageArmorPen, heavyFlatArmorPen, armor, health));

        var noPercentageMagicResistPen = parseFloat($("#labelNoPercentageMagicResistPen").text());
        var noFlatMagicResistPen = parseFloat($("#labelNoFlatMagicResistPen").text());
        $('#labelEffectiveHealthNoMagicResistPen').text(computeHealth(noPercentageMagicResistPen, noFlatMagicResistPen, magicResist, health));
        var lightPercentageMagicResistPen = parseFloat($("#labelLightPercentageMagicResistPen").text());
        var lightFlatMagicResistPen = parseFloat($("#labelLightFlatMagicResistPen").text());
        $('#labelEffectiveHealthLightMagicResistPen').text(computeHealth(lightPercentageMagicResistPen, lightFlatMagicResistPen, magicResist, health));
        var mediumPercentageMagicResistPen = parseFloat($("#labelMediumPercentageMagicResistPen").text());
        var mediumFlatMagicResistPen = parseFloat($("#labelMediumFlatMagicResistPen").text());
        $('#labelEffectiveHealthMediumMagicResistPen').text(computeHealth(mediumPercentageMagicResistPen, mediumFlatMagicResistPen, magicResist, health));
        var heavyPercentageMagicResistPen = parseFloat($("#labelHeavyPercentageMagicResistPen").text());
        var heavyFlatMagicResistPen = parseFloat($("#labelHeavyFlatMagicResistPen").text());
        $('#labelEffectiveHealthHeavyMagicResistPen').text(computeHealth(heavyPercentageMagicResistPen, heavyFlatMagicResistPen, magicResist, health));
            
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
        computeDPS: computeDPS,
        renderDamage: renderDamage,

        computeHealth: computeHealth,
        renderHealth: renderHealth,
    }
}();
