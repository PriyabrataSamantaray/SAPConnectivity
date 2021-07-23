var sigfp = angular.module('sigfp', ['ngSanitize']);

sigfp.controller('mainCtrl', ['$rootScope', '$scope', '$sce', 'homeService', function (rootScope, scope, $sce, homeService) {
    scope.tabTitle = "PRM";
    scope.accessibleProjects = [];
    var myloc = document.location.pathname == "/" ? "" : document.location.pathname + "/";

    scope.initApp = function () {
        var promiseGet = homeService.getData();
        promiseGet.then(function (result) {
            scope.data = result.data;
            if (!scope.data.hasAccessToApplication)//user doesn't have access to the application
            {
                var url = myloc + "noAccess.htm";
                document.location.replace(url);
                return;
            }

            if (scope.data.accessibleProjects.length > 0) {
                if (scope.data.accessibleProjects.length == 1) {
                    scope.selectedProjectUrl = scope.data.accessibleProjects[0].URL;
                    //scope.tabTitle = scope.data.accessibleProjects[0].ProjectName;
                    scope.showPRMLoaderStartPage = false;
                }
                else {
                    scope.accessibleProjects = scope.data.accessibleProjects;
                    scope.selectedProjectUrl = '';
                    scope.showPRMLoaderStartPage = true;
                    if (scope.accessibleProjects.length == 2) {
                        if (scope.accessibleProjects[0].URL == scope.accessibleProjects[1].URL) {
                            scope.selectedProjectUrl = scope.accessibleProjects[0].URL;
                            scope.showPRMLoaderStartPage = false;
                        }
                    }

                }
            }
            else {
                scope.selectedProjectUrl = '';
                scope.showPRMLoaderStartPage = true;
            }
        }, function (error) {
            alert(error);
        });
    }
    scope.initApp();

    scope.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    }

    scope.routeChosen = function (proj) {
        scope.showPRMLoaderStartPage = false;
        scope.selectedProjectUrl = proj.URL;
        //scope.tabTitle = proj.ProjectName;
    }
}]);

sigfp.filter('trustHtml', function ($sce) {
    return function (val) {
        return $sce.trustAsHtml(val);
    };
});

sigfp.service("homeService", ['$http', function ($http) {
    this.getData = function () {
        var location = document.location.pathname == "/" ? "" : document.location.pathname;
        return $http.get(location + "/api/HomeData/");
    };
}]);