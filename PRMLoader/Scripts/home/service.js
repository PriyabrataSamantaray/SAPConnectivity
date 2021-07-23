prm.service("homeService",['$http',  function ($http) {


    this.getData = function () {

        var location = document.location.pathname == "/" ? "" : document.location.pathname;

        return $http.get(location + "/api/HomeData/");
    };




}]);