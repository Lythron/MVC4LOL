var startup = function () {

    var init = function () {
        dataservice.getChampions(renderStaff);
    },

    renderStaff = function (data) {
        renderPatches(data);
        renderTags(data);
        renderChampions(data);
        $('input[name="Patch"]').click(function () {
            //renderChampions(data); 
            updateChampionsByPatch();
        });
        $('input[type="checkbox"]').click(function() {
            //renderChampions(data); 
            updateChampionsByTag(this);
        });
        updateChampionsByPatch();
    },

    renderPatches = function (data) {
        for (var key in data.Patches) {
            $('#PatchTemplate').tmpl(data.Patches[key]).appendTo('#Patches');
        }
    },

    renderTags = function (data) {
        
        var tags = [];
        $.each(data.Tags, function (ind, val) {
            if ($.inArray(val.Name, tags) == -1) {
                tags.push(val.Name);
            }
        });

        for (var key in tags) {
            var tagLiteral = { Name: tags[key] };
            $('#ChampionTagsTemplate').tmpl(tagLiteral).appendTo('#Tags');
        }
    },

    renderChampions = function (data) {

        $('#Champions').html("");

        for (var key in data.Champions) {

            var champ = data.Champions[key];
            renderChampion(champ);

            for (var key in data.Tags)
            {
                var tag = data.Tags[key];
                if (tag.ChampionId == champ.ChampionId)
                {
                    //$('div[id=ChampionDiv' + champ.ChampionId + ']').addClass("Tag" + tag.Id);
                    $('div[id=ChampionDiv' + champ.ChampionId + ']').addClass("Tag" + tag.Name.replace(" ", "")); // faster, but possible errors
                }
            }

            // Working exapmle of filtering champions
            //var selectedPatchId = $('input[name="Patch"]:checked').val();
            //var checkedTags = $('input[type="Checkbox"]:checked');

            
            //if (champ.PatchVersionId == selectedPatchId) {

            //    if (checkedTags.length == 0) {
            //        renderChampion(champ);
            //    }
            //    else {
            //        for (var key in data.Tags) {
            //            var entry = data.Tags[key];
            //            if (entry.ChampionId == champ.ChampionId) {
            //                for (var key in checkedTags) {
            //                    if (entry.Name == checkedTags[key].title) {
            //                        renderChampion(champ);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
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

    },

    updateChampionsByPatch = function () {
        var selectedPatchId = $('input[name="Patch"]:checked').val();

        var divs = $("div[id^=ChampionDiv]").not(".Patch" + selectedPatchId);

        $(".Patch" + selectedPatchId).show();
        //$("div[id^=ChampionDiv][class!=Patch" + selectedPatchId + "]").hide();
        $("div[id^=ChampionDiv]").not(".Patch" + selectedPatchId).hide();

    }

    updateChampionsByTag = function (tag) {
        var allUnchecked = $('input[type="Checkbox"]:checked').length == 0; // TODO;

        $(".Tag" + tag.title.replace(" ", "")).toggle();

    }

    return {
        init: init,
        renderChampions: renderChampions,
        renderChampion: renderChampion,
        renderStaff: renderStaff,
        renderPatches: renderPatches,
        renderTags: renderTags,
        updateChampionsByPatch: updateChampionsByPatch, 
        updateChampionsByTag: updateChampionsByTag

    };

}();


