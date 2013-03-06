//var dataservice = function () {
//    var getChampions = function () { 
//        alert('dataservice.getChampions'); 
//    };
    
//    return {
//        getChampions: getChampions
        
//    }
//}();

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

//var dataservice = function () {

//    var init = function () {

//        alert('dataservice.init()');

//    };

//    return {
//        init: init
//    };

//}();
