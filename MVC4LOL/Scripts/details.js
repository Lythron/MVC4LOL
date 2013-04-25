var details = function () {
    init = function (championId) {
        dataservice.getChampion(championId, renderStaff)
    },

    renderStaff = function (data) {

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
    }

    return {
        init: init,
        renderStaff: renderStaff
    }


}();