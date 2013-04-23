var startup = function () {

    var init = function () {
        dataservice.getChampions(renderStaff);
    },

    renderStaff = function (data) {
        renderPatches(data);
        renderTags(data);
        renderChampions(data);
        $('input[name="Patch"]').click(function () {
            //updateChampionsByTags();
            updateChampionsByTagsSelect();
        });
        //$('input[type="checkbox"]').click(function() {
        //    updateChampionsByTags();
        //});
        $('select').click(function () {
            updateChampionsByTagsSelect();
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

            var checkboxes = $('input[type=checkbox]');
            checkboxes.button();
            $("#Tags").buttonset();
        });

        updateChampionsByTagsSelect();

        updateChampionsByTags();
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
            //$('#ChampionTagsTemplate').tmpl(tagLiteral).appendTo('#Tags');

            $('#ChampionTagsSelectTemplate').tmpl(tagLiteral).appendTo('#TagsSelect');

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

    updateChampionsByTagsSelect = function () {

        var selectedPatchId = $('input[name="Patch"]:checked').val();

        var tagsOut = $('select option[value = "out"]:selected');

        var tagsAnd = $("select option[value='and']:selected");

        var tagsOr = $("select option[value='or']:selected");

        var tagsWhatever = $("select option[value='whatever']:selected");

        var tagsAll = $("select option:selected");

        if (tagsWhatever.length == tagsAll.length) {
            $("div[id^=ChampionDiv]").hide();
            $("tr[id^=ChampionDetailsTr]").hide();
            $(".Patch" + selectedPatchId).show();
        }
        else {
            var sb = "";
            var sbOut = ""; 
            
            for (var i = 0; i < tagsAnd.length; i++) {
                var title = tagsAnd[i].title.toLowerCase().replace(/\s/g, '');
                sb += ".Tag" + title;
            }
            for (var i = 0; i < tagsOr.length; i++) {
                var title = tagsOr[i].title.toLowerCase().replace(/\s/g, '');
                if (sb != "") {
                    sb += ", .Tag" + title;
                }
                else {
                    sb += " .Tag" + title;
                }
            }
            for (var i = 0; i < tagsOut.length; i++) {
                var title = tagsOut[i].title.toLowerCase().replace(/\s/g, '');
                sbOut += ".Tag" + title;
            }
            $("div[id^=ChampionDiv]").hide();
            $("tr[id^=ChampionDetailsTr]").hide();
            //$(".Patch" + selectedPatchId + sb).not(sbOut).show();
            if (sb != "") {
                $(".Patch" + selectedPatchId).filter(sb).not(sbOut).show();
            }
            else {
                $(".Patch" + selectedPatchId).not(sbOut).show();
            }
        }
    },

    updateChampionsByTags = function () {
        var selectedPatchId = $('input[name="Patch"]:checked').val();
        var checkedTags = $('input[type="Checkbox"]:checked');

        if (checkedTags.length == 0) {
            $("div[id^=ChampionDiv]").hide();
            $("tr[id^=ChampionDetailsTr]").hide();
            $(".Patch" + selectedPatchId).show();
        }
        else {
            var sb = "";
            for (var i = 0; i < checkedTags.length; i++) {
                var t = checkedTags[i].title.toLowerCase().replace(/\s/g, '');
                sb += ".Tag" + t;
            }
            $("div[id^=ChampionDiv]").hide();
            $("tr[id^=ChampionDetailsTr]").hide();
            $(".Patch" + selectedPatchId + sb).show();
        };
    }

    return {
        init: init,
        renderChampions: renderChampions,
        renderChampion: renderChampion,
        renderStaff: renderStaff,
        renderPatches: renderPatches,
        renderTags: renderTags,
        updateChampionsByTags: updateChampionsByTags,
        updateChampionsByTagsSelect: updateChampionsByTagsSelect
    };
}();


