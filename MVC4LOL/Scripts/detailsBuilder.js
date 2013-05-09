var detailsBuilder = function () {
    init = function (championId) {
        dataservice.loadBuilder(championId, renderStaff)
    },

    renderStaff = function (data) {

        renderChampion(data.Champion);

        renderItems(data);

        levelingBuilder.init(data);

        $('#Items img[id^="itemImage"]').click(function () {

            var div = $('div[id^="divItem"]:not(:has(img)):first');

            //var clonedObject = $.extend({}, this).clone();

            //clonedObject.appendTo(div);

            $("#" + this.id + ":first").clone().appendTo(div);

            //this.clone().appendTo(div); // WHY NOT WORKING ??

            //div.html(this);

            div.click(function () {
                div.html("");
            });

        });
    },
    
    renderChampion = function (championData)
    {

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
    }

    return {
        init: init,
        renderStaff: renderStaff,
        renderChampion: renderChampion,
        renderItems: renderItems,
        renderItem: renderItem,
    }

}();