var dataservice = function () {
    var getChampions = function (callback) {
        
        $.getJSON("/ChampionJson/IndexModel", function (data) {
            callback(data);
        });
    };
    
    return  {
        getChampions: getChampions
    }
}();