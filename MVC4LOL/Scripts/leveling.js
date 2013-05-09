var _data;
var leveling = function () {
    var init = function (data) {
        _data = data;
        //$('input[name="buttonLvlUp"]').unbind('click');
        $('input[name="buttonLvlUp"]').click(function () {
            increaseLvl();
        });
        //$('input[name="buttonLvlDown"]').unbind('click');
        $('input[name="buttonLvlDown"]').click(function () {
            decreaseLvl();
        });

    },

    increaseLvl = function () {
        var lvl = parseInt($("#labelLvl").text());
        if (lvl < 18) {
            $("#labelLvl").text(lvl + 1);
            adjustLvl(lvl + 1, "Green");
        }
    },

    decreaseLvl = function () {
        var lvl = parseInt($("#labelLvl").text());
        if (lvl > 1) {
            $("#labelLvl").text(lvl - 1);
            adjustLvl(lvl - 1, "Red");
        }
    }

    adjustLvl = function (level, color) {

        for (var key in _data.Champions)
        {
            var champ = _data.Champions[key];
            var patchId = champ.PatchVersionId;

            var c = $("div[class='Patch1'] #labelHealth");
            var cc = $("div[class='Patch" + patchId + "'] #labelHealth");
            $("div[class='Patch" + patchId + "'] #labelHealth").text(champ.Health + champ.HealthPerLvl * level);
            $("div[class='Patch" + patchId + "'] #labelHealthRegen").text(champ.HealthRegen + champ.HealthRegenPerLvl * level);
            $("div[class='Patch" + patchId + "'] #labelMana").text(champ.Mana + champ.ManaPerLvl * level);
            $("div[class='Patch" + patchId + "'] #labelManaRegen").text(champ.ManaRegen + champ.ManaRegenPerLvl * level);
            $("div[class='Patch" + patchId + "'] #labelDamage").text(champ.Damage + champ.DamagePerLvl * level);
            $("div[class='Patch" + patchId + "'] #labelAtkSpeed").text(champ.AtkSpeed * (1 + champ.DamagePerLvl * level));
            $("div[class='Patch" + patchId + "'] #labelArmor").text(champ.Armor + champ.ArmorPerLvl * level);
            $("div[class='Patch" + patchId + "'] #labelMagicResist").text(champ.MagicResist + champ.MagicResistPerLvl * level);

        }

        // TODO : Generic animation for all fields

        //$("#labelHealth").text(_data.Champions[0].Health + _data.Champions[0].HealthPerLvl * level);
        //$("#labelHealth").animate({ backgroundColor: color, fontSize: "23px" }, 'slow').animate({ backgroundColor: "Transparent", fontSize: "15px" }, 'slow');
        //$("#labelHealthRegen").text(_data.Champions[0].HealthRegen + _data.Champions[0].HealthRegenPerLvl * level);
        //$("#labelHealthRegen").animate({ backgroundColor: color }, 'slow').animate({ backgroundColor: "Transparent" }, 'slow');
        //$("#labelMana").text(_data.Champions[0].Mana + _data.Champions[0].ManaPerLvl * level);
        //$("#labelMana").animate({ backgroundColor: color }, 'slow').animate({ backgroundColor: "Transparent" }, 'slow');
        //$("#labelManaRegen").text(_data.Champions[0].ManaRegen + _data.Champions[0].ManaRegenPerLvl * level);
        //$("#labelManaRegen").animate({ backgroundColor: color }, 'slow').animate({ backgroundColor: "Transparent" }, 'slow');
    }
    return {
        init : init,
        increaseLvl : increaseLvl,
        decreaseLvl: decreaseLvl,
        adjustLvl : adjustLvl
    };

}();