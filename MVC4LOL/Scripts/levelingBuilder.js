var _data;
var levelingBuilder = function () {
    var init = function (data) {
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
    }

    adjustLvl = function (level) {

        var champ = _data.Champion;

        $('#labelHealth').text(champ.Health + champ.HealthPerLvl * level);
        $('#labelHealthRegen').text(champ.HealthRegen + champ.HealthRegenPerLvl * level);
        $('#labelMana').text(champ.Mana + champ.ManaPerLvl * level);
        $('#labelManaRegen').text(champ.ManaRegen + champ.ManaRegenPerLvl * level);
        $('#labelDamage').text(champ.Damage + champ.DamagePerLvl * level);
        $('#labelAttackSpeed').text(champ.AtkSpeed * (1 + champ.AtkSpeedPerLvl/100 * level));
        $('#labelArmor').text(champ.Armor + champ.ArmorPerLvl * level);
        $('#labelMagicResist').text(champ.MagicResist + champ.MagicResistPerLvl * level);

    }
    return {
        init : init,
        increaseLvl : increaseLvl,
        decreaseLvl: decreaseLvl,
        adjustLvl : adjustLvl
    };

}();