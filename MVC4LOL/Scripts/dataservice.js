var dataservice = function () {
    var getChampions = function (callback) {
        
        $.getJSON("/ChampionJson/IndexModel", function (data) {
            callback(data);
        });
    };
    
    var getChampion = function (championId, callback) {
        $.getJSON("/ChampionJson/DetailsJson?championId=" + championId, function (data) {
            callback(data);
        });
    }

    var loadBuilder = function (championId, callback) {
        $.getJSON("/ChampionBuilder/Load?championId=" + championId, function (data) {
            callback(data);
        });
    }

    return  {
        getChampions: getChampions,
        getChampion: getChampion,
        loadBuilder: loadBuilder
    }
}();