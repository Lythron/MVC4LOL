var startup = function () {

    var init = function () {
        dataservice.getChampions(renderStaff);
    },

    renderStaff = function (data) {
        renderPatches(data);
        renderTags(data);
        renderChampions(data);
        $('input[name="Patch"]').click(function () {
            updateChampionsByPatch();
        });
        $('input[type="checkbox"]').click(function() {
            updateChampionsByTag(this);
        });
        $('#aViewMode').click(function () {
            $('#Champions').toggle();
            $('#ChampionsDetails').toggle();
        });

        $(function () {
            $('div[id=Champions]').sortable();
            //$('div[id=Champions]').draggable();
            $('div[id=Champions]').disableSelection();
            //$('table[id=ChampionDetailsTable]').sortable();
            //$('table[id=ChampionDetailsTable]').draggable();
            //$('table[id=ChampionDetailsTable]').disableSelection();

            $('tr[id^=ChampionDetailsTr]').sortable();

            //$("#check").button();
            //$("#Tags").buttonset();

        });

        $(function () {
            
        });

        updateChampionsByPatch();
        $('#ChampionsDetails').hide();
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
                    $('div[id=ChampionDiv' + champ.ChampionId + ']').addClass("Tag" + tag.Name.toLowerCase().replace(/\s/g, '')); // faster, but possible errors
                    $('tr[id=ChampionDetailsTr' + champ.ChampionId + ']').addClass("Tag" + tag.Name.toLowerCase().replace(/\s/g, ''));
                }
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

        var date = championData.ReleaseDate;
        var milisecs = parseInt(date.replace("/Date(", "").replace(")/", ""));
        var d = new Date(milisecs);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = d.getFullYear() + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day;
        championData.ReleaseDate = output;

        $('#ChampionOverviewTemplate').tmpl(championData).appendTo('#Champions');
        $('#ChampionDetailsOverviewTemplate').tmpl(championData).appendTo('#ChampionDetailsTable');
    },

    updateChampionsByPatch = function () {
        var selectedPatchId = $('input[name="Patch"]:checked').val();

        $(".Patch" + selectedPatchId).show();
        $("div[id^=ChampionDiv]").not(".Patch" + selectedPatchId).hide();
        $("tr[id^=ChampionDetailsTr]").not(".Patch" + selectedPatchId).hide();
    }

    updateChampionsByTag = function (tag) {
        var selectedPatchId = $('input[name="Patch"]:checked').val();
        var checkedTags = $('input[type="Checkbox"]:checked');

        if (checkedTags.length == 0) {
            updateChampionsByPatch();
        }
        else {
            var sb = "";
            for ( var i = 0; i < checkedTags.length; i++) {
                var t = checkedTags[i].title.toLowerCase().replace(/\s/g, '');
                sb += ".Tag" + t;
            }
            $("div[id^=ChampionDiv]").hide();
            $("tr[id^=ChampionDetailsTr]").hide();
            $(".Patch" + selectedPatchId + sb).show();
        };
        // Tags should be union ,intersection and exclude so this approach will need to change.
        // as first approach Im going with intersection
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


