//var tileRenderer = function () {

//    var render = function (tileDiv, sceneId) {
//        if (sceneId == null) {
//            sceneId = 0;
//        }
//        alert('tileRenderer.render(null, null)');
//        var size = tileDiv.data().scenes[sceneId].size,
//            template = tileDiv.data().templates[size],
//            formatterFunc = tileDiv.data().formatter;
        
//        tileDiv.html(template);
//        if (formatterFunc != null) {
//            formatterFunc(tileDiv);
//        }
//    };

//    return {
//        render: render
//    };

//} ();
var startup = function () {

    var init = function () {
        
        alert('startup.init(null, null)');
        dataservice.getChampions(renderChampions);
    },

    renderChampions = function (data) {

        for (var key in data.championsWithStrImg)
        {
            var champ = data.championsWithStrImg[key];
            renderChampionWithStrImg(champ);
        }

        //for (var key in data.Champions)
        //{
        //    var champ = data.Champions[key];
        //    var name = champ.Name;
        //    renderChampion(champ);
        //}

        //$('div.tile[id^="Account"]').each(function () {

        //    var tileDiv = $(this);
        //   renderTile(data, tileDiv, 0);
            
        //});
    },

    renderChampionWithStrImg = function (championData) {

        var champ = championData.champion;

        $("#Champions")
        .append("<div>")
        .append("<img src=\"data:image/png;base64," + championData.imageBase64 + "\" alt=\"TODO: champ.Name\">") // TODO: alt : Name
        .append(champ.Name)
        .append("</div>");
    }

    renderChampion = function (championData) {
        
        //$("#Champions").append("<div>").append(championData.Name).append("</div>");

        //$("#Champions")
        //    .append("<div>")
        //    .append("<img src=\"data:image/png;base64,")
        //    .append("iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==")
        //    .append(",\" alt=\"Red dot\">")
        //    .append("</div>");
        

        $("#Champions")
        .append("<div>")
        .append("<img src=\"data:image/png;base64," + "iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==" + "\" alt=\"Red dot\">")
        .append("</div>");

        //$("#Champions").append("<div>")
        //.append("<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==\" alt=\"Red dot\">")
        //.append("</div>");

        
    }

    return {
        init: init,
        renderChampions: renderChampions,
        renderChampion: renderChampion,
        renderChamionWithStrImg : renderChampionWithStrImg

    };

}();


