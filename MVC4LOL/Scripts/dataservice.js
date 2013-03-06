var dataservice = function () {
    var getChampions = function (callback) {
        alert('dataservice.getChampions');
        $.getJSON("/ChampionJson/IndexModel", function (data) {
            callback(data);
        });
    };
    
    return  {
        getChampions: getChampions
    }
}();