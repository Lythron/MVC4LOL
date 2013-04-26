﻿var details = function () {
    init = function (championId) {
        dataservice.getChampion(championId, renderStaff)
    },

    renderStaff = function (data) {

        renderPatches(data);

        renderChampions(data);

        leveling.init(data);

        var date = data.Champions[0].ReleaseDate;
        var milisecs = parseInt(date.replace("/Date(", "").replace(")/", ""));
        var d = new Date(milisecs);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = d.getFullYear() + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day;
        data.Champions[0].ReleaseDate = output;

        $('#DetailsTemplate').tmpl(data).appendTo('#divDetails');
    },
    renderPatches = function (data) {
        for (var key in data.Patches) {
            $('#PatchTemplate').tmpl(data.Patches[key]).appendTo('#Patches');
        }
    },
    renderChampions = function (data) {

        $('#Champions').html("");

        for (var key in data.Champions) {

            var champ = data.Champions[key];
            renderChampion(champ);
        }
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

        $('#ChampionTemplate').tmpl(championData).appendTo('#Champions');
    }

    return {
        init: init,
        renderStaff: renderStaff,
        renderPatches: renderPatches,
        renderChampions: renderChampions,
        renderChampion: renderChampion
    }

}();