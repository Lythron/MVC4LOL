var startup = function () {

    var init = function () {
        dataservice.getChampions(renderStaff);
    },

    renderStaff = function (data) {
        renderPatches(data);
        renderTags(data);
        renderChampions(data);
    },

    renderPatches = function (data) {
        for (var key in data.Patches) {
            $('#PatchTemplate').tmpl(data.Patches[key]).appendTo('#Patches');

            $('input[name="Patch"]').checked = function () {
                // call render champions here, first decouple rendering patches from champions
            }
        }
    },

    renderTags = function (data) {
        for (var key in data.Tags) {
            var tagLiteral = { Name: data.Tags[key] };

            $('#ChampionTagsTemplate').tmpl(tagLiteral).appendTo('#Tags');
        }
    },

    renderChampions = function (data) {

        for (var key in data.Champions) {
            // on first render check last patch
            // filter here only patch selected and tags selected

            // TODO : get data connecting champs with name of tag ( only ids right now )

            var selectedPatchId = $('input[name="Patch"]:checked').val();

            var champ = data.Champions[key];
            if (champ.PatchVersionId == selectedPatchId) {
                renderChampion(champ);
            }
        }
    },

    renderChampion = function (championData) {

        // Working example of formating html in code
        //var image = $('<img/>', {
        //                            src: "data:image/png;base64," + championData.Image ,
        //                            alt: championData.Name,
        //                            title: championData.Name,
        //                            height: 70
        //                          });
        
        //var a = $('<a/>', {
        //    href: "/Champion/Details2?ChampionId=" + championData.ChampionId,
        //    html: image
        //});

        //$("#Champions").append($('<div/>', {
        //    id: championData.Id,
        //    height: 100,
        //    html: a
        //}));
        
        //$('#ChampionDetailsOverviewTemplate').tmpl(championData).appendTo('#' + championData.Id);

        $('#ChampionDetailsOverviewTemplate').tmpl(championData).appendTo('#Champions');
    }

    return {
        init: init,
        renderChampions: renderChampions,
        renderChampion: renderChampion,
        renderStaff: renderStaff,
        renderPatches: renderPatches,
        renderTags: renderTags
    };

}();


