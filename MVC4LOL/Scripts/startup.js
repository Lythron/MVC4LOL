var startup = function () {

    var init = function () {
        
        alert('startup.init(null, null)');
        dataservice.getChampions(renderChampions);
    },

    renderChampions = function (data) {

        //for (var key in data.championsWithStrImg)
        //{
        //    var champ = data.championsWithStrImg[key];
        //    renderChampionWithStrImg(champ);
        //}

        for (var key in data.Champions)
        {
            var champ = data.Champions[key];
            var name = champ.Name;
            renderChampion(champ);
        }
    },

    //renderChampionWithStrImg = function (championData) {

    //    var champ = championData.champion;

    //    $("#Champions")
    //    .append("<div>")
    //    .append("<img src=\"data:image/png;base64," + championData.imageBase64 + "\" alt=\"TODO: champ.Name\">") // TODO: alt : Name
    //    .append(champ.Name)
    //    .append("</div>");
    //}

    renderChampion = function (championData) {
        
        //$('<div/>', {
        //    id: someID,
        //    className: 'foobar',
        //    html: content
        //});

        //$("#Champions").append($('<div/>', {

        //                       }));

        $("#Champions")
        .append("<div>")
        .append("<img src=\"data:image/png;base64," + championData.Image + "\" alt=\"TODO: champ.Name\">") // TODO: alt : Name
        .append(championData.Name)
        .append("</div>");

        //$("#Champions").append("<div>")
        //.append("<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==\" alt=\"Red dot\">")
        //.append("</div>");
    }

    return {
        init: init,
        renderChampions: renderChampions,
        renderChampion: renderChampion,
        //renderChamionWithStrImg : renderChampionWithStrImg
    };

}();


