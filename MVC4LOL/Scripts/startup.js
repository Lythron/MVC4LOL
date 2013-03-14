var startup = function () {

    var init = function () {
        dataservice.getChampions(renderStaff);
    },

    renderStaff = function (data) {
        renderPatches(data);
        renderTags(data);
        renderChampions(data);
        $('input[name="Patch"]').click(function () {
            renderChampions(data); 
        });
        $('input[type="checkbox"]').click(function() {
            renderChampions(data); // TEST 
        });
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

            var selectedPatchId = $('input[name="Patch"]:checked').val();
            var checkedTags = $('input[type="Checkbox"]:checked');

            var champ = data.Champions[key];
            if (champ.PatchVersionId == selectedPatchId) {

                // Check here if champ has any of selected tags
                //if ($.inArray(
                //if (data.Tags.Where(o => o.ChampionId == champ.ChampionId && checkedTags.contains(o.Name)).Count > 0)
                //{
                //      render();
                //}

                if (checkedTags.length == 0) {
                    renderChampion(champ);
                }
                else {
                    for (var key in data.Tags) {
                        var entry = data.Tags[key];
                        if (entry.ChampionId == champ.ChampionId) {
                            for (var key in checkedTags) {
                                if (entry.Name == checkedTags[key].title) {
                                    renderChampion(champ);
                                }
                            }
                        }
                    }
                }

                //renderChampion(champ);
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


