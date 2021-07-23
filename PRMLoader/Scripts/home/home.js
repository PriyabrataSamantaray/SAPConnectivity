var area;
var adRole;
var isPlanning = false;
var isAdmin = false;
var isFinance = false;
var myloc = document.location.pathname == "/" ? "" : document.location.pathname + "/";
var reportsLength;
var planningLength;
var financeLength;
var adminLength;
var mode = "";
var modeMessage = "";
var externalLink = false;
var currentTab = 1;

prm.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("");
    // Now set up the states
    $stateProvider
        .state('planning-new', {
            templateUrl: myloc + "planning/planning/qram",
            controller: 'qramCtrl'
            , params: { 'programs': null, 'projects': null, isCTL: null }
        })
        .state('headcount', {
            templateUrl: myloc + "headCountReport/headCount",
            controller: 'headCountCtrl',
        })
        .state('projectreport', {
            templateUrl: myloc + "projectReport/projectReport/ProjectReport",
            controller: 'projectReportCtrl'
        })
        .state('Analytics', {
            templateUrl: myloc + "projectReport/projectReport/Analytics",
            controller: 'projectReportCtrl'
        })
        //+DRajendran 11/15/2017 - Added a new state for My Team project planning for Supervisor (Manager)
        .state('myTeam', {
            templateUrl: myloc + "planning/planning/myTeam",
            controller: 'myTeamCtrl'
        })
        //-DRajendran 11/15/2017 - Added a new state for My Team project planning for Supervisor (Manager)
        .state('auditlog', {
            templateUrl: myloc + "admin/admin/Audit",
            controller: 'auditLogCtrl'
        })
        .state('home', {
            templateUrl: myloc + "home/home",
            controller: 'homeCtrl'
        })
        .state('FAQ', {
            templateUrl: myloc + "home/home",
            controller: 'homeCtrl',
            params: { 'anchorId': null }
        })
        .state('whoplans', { //Added on 21/08/2017 - Who plans screen.
            templateUrl: myloc + "admin/admin/whoplans",
            controller: 'whoPlansCtrl'
        })
        .state('BPC', { //Added on 2/01/2018 - BPC Export screen.
            templateUrl: myloc + "admin/admin/BPC",
            controller: 'bpcCtrl'
        })
        .state('costCenterSetup', {
            templateUrl: myloc + "admin/admin/CostCenterSetup",
            controller: 'costCenterSetupCtrl'
        })
        .state('versioning', {
            templateUrl: myloc + "admin/admin/Versioning",
            controller: 'versioningCtrl'
        })
        .state('versioningJob', {
            templateUrl: myloc + "admin/admin/VersioningJob",
            controller: 'versioningJobsCtrl'
        })
       .state('manageRoles', {
           templateUrl: myloc + "admin/admin/ManageRoles",
           controller: 'ManageRolesCtrl'
       })
}]);

prm.run(['$rootScope', '$window', 'projectReportFactory', 'planFuncs', 'costCenterSetupFuncs', 'whoPlansFuncs', 'versioningFuncs', 'myteamFuncs', '$customExceptionHandle', function ($rootScope, $window, projectReportFactory, planFuncs, costCenterSetupFuncs, whoPlansFuncs, versioningFuncs, myteamFuncs, $customExceptionHandle) {
    $rootScope.$on(
        '$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {
            try {
                //+For Google Analytics.
                if (angular.isDefined(toState.url)) {
                    $rootScope.currentURL = toState.url;
                }
                else if (angular.isDefined(toState.templateUrl)) {
                    $rootScope.currentURL = toState.templateUrl;
                }
                else {
                    $rootScope.currentURL = '/home';
                }

                dataLayer.push({
                    'event': 'virtualPageView',
                    'page': {
                        'virtualtitle': toState.name,
                        'virtualurl': $rootScope.currentURL
                    }
                    //,'userId': typeof (window.badge) == 'undefined' ? '' : window.badge
                });
                //-For Google Analytics.
                if (planFuncs.Navigate == false) {
                    planFuncs.filteredData = planFuncs.filteredData.invoke();
                    planFuncs.filteredMatData = planFuncs.filteredMatData.invoke();
                    var changedData = planFuncs.getChangedRows();
                    if (changedData.resources.length > 0 || changedData.materials.length > 0) {
                        var retVal = confirm("Are you sure you want to leave this page?\n All changes will be lost.");
                        if (retVal == false) {
                            event.preventDefault();
                            planFuncs.Navigate = false;
                            return;
                        }
                    }
                    planFuncs.Navigate = true;
                }
                if (costCenterSetupFuncs.Navigate == false) {
                    var retVal = confirm("Are you sure you want to leave this page?\n All changes will be lost.");
                    if (retVal == false) {
                        event.preventDefault();
                        costCenterSetupFuncs.Navigate = false;
                        return;
                    }
                    else {
                        costCenterSetupFuncs.Navigate = true;
                    }
                }
                if (whoPlansFuncs.Navigate == false) {
                    var retVal = confirm("Are you sure you want to leave this page?\n All changes will be lost.");
                    if (retVal == false) {
                        event.preventDefault();
                        whoPlansFuncs.Navigate = false;
                        return;
                    }
                    else {
                        whoPlansFuncs.Navigate = true;
                    }
                }
                if (versioningFuncs.Navigate == false) {
                    var retVal = confirm("Are you sure you want to leave this page?\n All changes will be lost.");
                    if (retVal == false) {
                        event.preventDefault();
                        versioningFuncs.Navigate = false;
                        return;
                    }
                    else {
                        versioningFuncs.Navigate = true;
                    }
                }
                if (myteamFuncs.Navigate == false) {
                    var retVal = confirm("Are you sure you want to leave this page?\n All changes will be lost.");
                    if (retVal == false) {
                        event.preventDefault();
                        myteamFuncs.Navigate = false;
                        return;
                    }
                    else {
                        myteamFuncs.Navigate = true;
                    }
                }
                projectReportFactory.clearData();
                planFuncs.clearData();
                whoPlansFuncs.clearData();
                costCenterSetupFuncs.clearData();
                versioningFuncs.clearData();
                myteamFuncs.clearData();
            } catch (e) {
                $customExceptionHandle.errorLogClient(e);
            }
        }
    );

    document.addEventListener("keyup", function (e) {
        if (e.keyCode === 27)
            $rootScope.$broadcast("escapePressed", e.target);
    });

    document.addEventListener("click", function (e) {
        $rootScope.$broadcast("documentClicked", e.target);
    });

}]);


prm.controller('homeCtrl', ['$rootScope', '$scope', '$window', 'planFuncs', '$stateParams', '$anchorScroll', '$location', '$customExceptionHandle', function (rootScope, scope, window, planFuncs, $stateParams, $anchorScroll, $location, $customExceptionHandle) {
    scope.$on('$viewContentLoaded', function () {
        if (typeof ($stateParams.anchorId) != 'undefined') {
            $location.hash($stateParams.anchorId);
            $anchorScroll();
        }
    });
    if (window.mode != "" || window.modeMessage != "") //change happened from admin screen
    {
        var idx = 0;
        angular.forEach(scope.$parent.data.states, function (value, key) {
            if (value.area == window.area) {
                scope.$parent.data.states[idx].reason = window.modeMessage;
                scope.$parent.data.states[idx].readOnlyMode = false;
                scope.$parent.data.states[idx].shutDownMode = false;

                if (window.mode == "readOnly")
                    scope.$parent.data.states[idx].readOnlyMode = true;
                if (window.mode == "shutDown")
                    scope.$parent.data.states[idx].shutDownMode = true;
            }
            idx += 1;
        });
        window.mode = "";
        window.modeMessage = "";
    }
    rootScope.isDisplayHeader = true; //Show and Hide the header/nav bar from myTeamPage
    scope.data = scope.$parent.data;
    scope.areas = scope.$parent.areas;
    scope.selectedArea = window.area;
    scope.errorMessage;
    scope.shutdown;
    scope.readOnly;
    scope.selectAreaVisisble = scope.areas.length > 1 ? "featureVisible" : "featureInVisible";
    scope.$parent.$watch('data.homeProjectsData', function (oldValue, newValue) {
        if (newValue != oldValue && scope.$parent != null) {
            scope.changeArea();
        }
    });
    scope.changeArea = function () {
        try {
            var newValue = scope.selectedArea;
            if (newValue == undefined)
                return;
            scope.$parent.areaChanged(newValue);
            angular.forEach(scope.data.states, function (value, key) {
                if (value.area == newValue) {
                    scope.errorMessage = value.reason;
                    scope.shutdown = value.shutDownMode;
                    scope.readOnly = value.readOnlyMode;
                }
            });
            scope.$parent.shutDown(scope.shutdown);
            scope.calVisible = scope.errorMessage == "" && newValue == "Engineering" ? "featureVisible" : "featureInVisible";
            scope.pseHomeVis = scope.errorMessage == "" && newValue == "PSE" ? true : false;
            scope.errorMsgVisisble = scope.errorMessage == "" ? "featureInVisible" : "featureVisible";
            scope.area = newValue;
            scope.homeProjectsData = scope.$parent.data.homeProjectsData;
            //to display GFP projects for the user
            var loggedinuser = window.userName;
            loggedinuser = loggedinuser.replace("AMAT", "").toUpperCase();
            var isCTL = false;
            angular.forEach(scope.homeProjectsData, function (value, key) {  //Check if the user is a CTL 
                var user = value.projectOwner;
                var CTL = value.CTL;
                if (loggedinuser.toUpperCase().indexOf(CTL.toUpperCase()) != -1) {
                    isCTL = true;
                }
            });
            tmpArray = [];
            if (isCTL) {
                angular.forEach(scope.homeProjectsData, function (value, key) {
                    var user = value.projectOwner;
                    var CTL = value.CTL;
                    if ((loggedinuser.toUpperCase().indexOf(CTL.toUpperCase()) != -1) || (loggedinuser.toUpperCase().indexOf(user.toUpperCase()) != -1)) {
                        var pos = tmpArray.map(function (e) { return e.projects; }).indexOf(value.projects); //search the activeFilter object array
                        if (pos == -1)
                            tmpArray.push({
                                Id: value.Id, projects: value.projects, programs: value.programs, userName: value.CTL,
                                route: 'planning-new', fullName: 'Planning', duration: value.duration, roleID: value.roleID, plannedStatus: value.plannedStatus, isCTL: (loggedinuser.toUpperCase().indexOf(CTL.toUpperCase()) != -1)
                            });
                    }
                });
                scope.homeProjectsData = angular.copy(tmpArray);
            }
            else {
                angular.forEach(scope.homeProjectsData, function (value, key) {
                    var user = value.projectOwner;
                    var CTL = value.CTL;
                    if (loggedinuser.toUpperCase().indexOf(user.toUpperCase()) != -1) {
                        var pos = tmpArray.map(function (e) { return e.projects; }).indexOf(value.projects); //search the activeFilter object array
                        if (pos == -1)
                            tmpArray.push({
                                Id: value.Id, projects: value.projects, programs: value.programs, userName: value.projectOwner,
                                route: 'planning-new', fullName: 'Planning', duration: value.duration, roleID: value.roleID, plannedStatus: value.plannedStatus, isCTL: (loggedinuser.toUpperCase().indexOf(CTL.toUpperCase()) != -1)
                            });
                    }
                });
                scope.homeProjectsData = angular.copy(tmpArray);
            }
        } catch (e) {
            $customExceptionHandle.errorLogClient(e);
        }
    };
    scope.changeArea();
}]);

prm.controller('mainCtrl', ['$rootScope', '$scope', 'homeService', '$window', '$state', '$animate', '$stateParams', 'planFuncs', 'costCenterSetupFuncs', 'whoPlansFuncs', 'versioningFuncs', 'myteamFuncs', '$anchorScroll', '$location', '$customExceptionHandle', 'fEmployeeDetails', 'fCommonPlanData', 'fCCSetupData', 'fPlanDropData', function (rootScope, scope, homeService, window, state, $animate, stateParams, planFuncs, costCenterSetupFuncs, whoPlansFuncs, versioningFuncs, myteamFuncs, $anchorScroll, $location, $customExceptionHandle, fEmployeeDetails, fCommonPlanData, fCCSetupData, fPlanDropData) {
    scope.data;
    scope.title;
    scope.serverTitle;
    scope.version;
    scope.sapVersion;
    scope.displayRole;
    scope.pageVisible = "notVisible";
    scope.adminVisible = false;
    scope.buAccessNotAdmin = 0;
    scope.titleBarVisible = true; //"menu";
    scope.financeVisible = false;
    scope.fiscalWeek;
    scope.fiscalWeekColor = "site-normal";
    scope.watchDog;
    scope.PSEWatchDog = [];
    scope.features;
    scope.loc = "historical";
    scope.reports = [];
    scope.plans = [];
    scope.finances = [];
    scope.admins = [];
    scope.buplanning = [];
    scope.gfpReports = [];
    scope.tabTitle = "MYTEAM";
    fEmployeeDetails.RefreshEmployeeData = true;
    fCommonPlanData.RefreshData = true;
    fCCSetupData.RefreshCCSetupData = true;
    fPlanDropData.RefreshPlanDropData = true;
    scope.routeChosen = function (obj) {
        try {
            var feature;
            if (obj.plan != null) {
                feature = obj.plan;
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
            }
            else if (obj.report != null) {
                feature = obj.report;
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
            }
            else if (obj.finance != null) {
                feature = obj.finance;
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
                if (planFuncs.redirection == false) {
                    planFuncs.Navigate = false;
                }
                if (costCenterSetupFuncs.redirection == false) {
                    costCenterSetupFuncs.Navigate = false;
                }
                if (whoPlansFuncs.redirection == false) {
                    whoPlansFuncs.Navigate = false;
                }
                if (versioningFuncs.redirection == false) {
                    versioningFuncs.Navigate = false;
                }
                if (myteamFuncs.redirection == false) {
                    myteamFuncs.Navigate = false;
                }
            }
            else if (obj.admin != null) {
                feature = obj.admin;
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
                if (planFuncs.redirection == false) {
                    planFuncs.Navigate = false;
                }
                if (costCenterSetupFuncs.redirection == false) {
                    costCenterSetupFuncs.Navigate = false;
                }
                if (whoPlansFuncs.redirection == false) {
                    whoPlansFuncs.Navigate = false;
                }
                if (versioningFuncs.redirection == false) {
                    versioningFuncs.Navigate = false;
                }
                if (myteamFuncs.redirection == false) {
                    myteamFuncs.Navigate = false;
                }
            }
            else if (obj == "home") {
                if (planFuncs.redirection == false) {
                    planFuncs.Navigate = false;
                }
                if (costCenterSetupFuncs.redirection == false) {
                    costCenterSetupFuncs.Navigate = false;
                }
                if (whoPlansFuncs.redirection == false) {
                    whoPlansFuncs.Navigate = false;
                }
                if (versioningFuncs.redirection == false) {
                    versioningFuncs.Navigate = false;
                }
                if (myteamFuncs.redirection == false) {
                    myteamFuncs.Navigate = false;
                }
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
                scope.title = "Project Resource Management" + scope.serverTitle;
                scope.title = window.area == "GFP" ? "" : scope.title;
                //window.location.reload();
                state.go("home");
                window.showApp();
                scope.loadHomeData(false);
                return;
            }
            else if (obj == "projectreport") {
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
                window.showApp();
                return;
            }
            else if (obj.fullName == "Planning") {
                stateParams.projects = obj.projects.trim();
                stateParams.programs = obj.programs.trim();
                stateParams.isCTL = obj.isCTL;
                window.showApp();
                return;
            }
            else if (obj.gfpReport.route != null) {
                stateParams.projects = "";
                stateParams.programs = "";
                stateParams.isCTL = false;
                if (planFuncs.redirection == false) {
                    planFuncs.Navigate = false;
                }
                if (costCenterSetupFuncs.redirection == false) {
                    costCenterSetupFuncs.Navigate = false;
                }
                if (whoPlansFuncs.redirection == false) {
                    whoPlansFuncs.Navigate = false;
                }
                if (versioningFuncs.redirection == false) {
                    versioningFuncs.Navigate = false;
                }
                if (myteamFuncs.redirection == false) {
                    myteamFuncs.Navigate = false;
                }
            }
            if (feature.route != "tableau") {
                window.showApp();
            }
            else {
                tableau.location.replace(feature.url);
            }
            if (feature.route == "qram")  //check into routeparams
            {
                window.isPlanning = false;
            }
            else {
                window.isPlanning = true;
            }
            if (feature.route == "projectreport")  //check into routeparams
            {
                pdfWindow = window.open("projectReport/projectReport", "pdfWindow", "resizable = yes");
            }
            scope.title = feature.fullName + scope.serverTitle;
        } catch (e) {
            $customExceptionHandle.errorLogClient(e);
        }
    };

    scope.docClick = function (route) {
        try {
            var popupUrl = "";
            var title = "";
            if (area == "Engineering" && route == "faqs.htm") {
                popupUrl = "http:\\\\prmfaq";
                title = "FAQ"
                pdfWindow = window.open(popupUrl, "pdfWindow", "resizable = yes");
            }
            else if (area == "GFP" && route == "projectreport") {
                if (planFuncs.redirection == false) {
                    planFuncs.Navigate = false;
                }
                if (costCenterSetupFuncs.redirection == false) {
                    costCenterSetupFuncs.Navigate = false;
                }
                if (whoPlansFuncs.redirection == false) {
                    whoPlansFuncs.Navigate = false;
                }
                state.go('projectreport');
                return;
            }
                //+DRajendran 11/15/2017 - Navigate to the My Team project planning page for Supervisor (Manager)
            else if (area == "GFP" && route == "MyTeam") {
                state.go("myTeam");
                return;
            }
                //-DRajendran 11/15/2017 - Navigate to the My Team project planning page for Supervisor (Manager)
            else if (area == "GFP" && route == "survey_GFP.htm") {
                popupUrl = "https://survey.amat.com/survey/s?s=38404";
                title = "Survey";
                pdfWindow = window.open(popupUrl, "pdfWindow", "resizable = yes");
            }
            else if (area == "GFP" && route == "faq_GFP.htm") {
                if (state.current.name != "FAQ") {
                    if (planFuncs.redirection == false) {
                        planFuncs.Navigate = false;
                    }
                    if (costCenterSetupFuncs.redirection == false) {
                        costCenterSetupFuncs.Navigate = false;
                    }
                    if (whoPlansFuncs.redirection == false) {
                        whoPlansFuncs.Navigate = false;
                    }
                    state.go("FAQ", { 'anchorId': 'divFAQ' });
                }
                else {
                    $location.hash('divFAQ');
                    $anchorScroll();
                }
                return;
            }
            else if (area == 'GFP' && route == 'RSTTeam_GFP.htm') {
                popupUrl = "https://spprdappweb.amat.com/RoleAssignmentTool/Pages/Demos.aspx";
                title = "RST Team & Role";
                pdfWindow = window.open(popupUrl, "pdfWindow", "resizable = yes");
            }
            else {
                popupUrl = myloc + "\\docs\\" + route;
                title = route;
                pdfWindow = window.open(popupUrl, "pdfWindow", "resizable = yes");
            }
            //+For Google Analytics.
            dataLayer.push({
                'event': 'virtualPageView',
                'page': {
                    'virtualtitle': title,
                    'virtualurl': popupUrl
                }
                //,'userId': typeof (window.badge) == 'undefined' ? '' : window.badge
            });
            //-For Google Analytics.
        } catch (e) {
            $customExceptionHandle.errorLogClient(e);
        }
    }
    scope.shutDown = function (val) {
        if (val && window.isAdmin == false)
            scope.titleBarVisible = false; //"nomenu";
        else
            scope.titleBarVisible = true; //"menu";
        scope.pageVisible = "visible"; //set here so when bottom is finished both will display
    };
    scope.areaChanged = function (newValue) {
        try {
            var keepGoing = true;
            angular.forEach(scope.data.adSets, function (value, key) {
                if (keepGoing) {
                    if (value.adArea == newValue) {
                        keepGoing = false;
                        scope.displayRole = value.adArea + "\\" + value.adRole;
                        window.area = value.adArea;
                        window.adRole = value.adRole;
                        scope.title = value.adArea == "GFP" ? 'Team Planning' : "Project Resource Management" + scope.serverTitle;
                        rootScope.buAccessNotAdmin = scope.buAccessNotAdmin = (value.adArea == "GFP" && value.adRole != "Admin" && scope.data.buAccessLevel.length > 0) ? 1 : 0;
                        rootScope.buAccessLevel = scope.data.buAccessLevel;
                        rootScope.isReadOnlyUser = scope.isReadOnlyUser;
                        scope.area = value.adArea;
                        if (value.adRole == "Admin") {
                            window.isAdmin = true;
                            window.isFinance = true;
                            scope.adminVisible = true; //"displayHeader";
                            scope.financeVisible = false; //"displayHeader";
                            scope.buPlanVisible = true;
                        }
                        else if (value.adRole == "Finance") {
                            window.isAdmin = false;
                            window.isFinance = true;
                            scope.adminVisible = false; //"noDisplayHeader";
                            scope.financeVisible = false; //"displayHeader";
                            scope.buPlanVisible = false;
                        }
                        else {
                            window.isAdmin = false;
                            window.isFinance = false;
                            scope.adminVisible = false; //"noDisplayHeader";
                            scope.financeVisible = false; //"noDisplayHeader";
                            scope.buPlanVisible = false;
                        }
                        if (scope.buAccessNotAdmin == 1 || scope.isReadOnlyUser) {
                            scope.adminVisible = false;
                            scope.buPlanVisible = true;
                        }
                    }
                }
            });
            scope.reports = [];//clear array object
            scope.plans = [];
            scope.finances = [];
            scope.admins = [];
            scope.buplanning = [];
            scope.docs = [];
            scope.gfpReports = [];//clear array object
            angular.forEach(scope.data.states, function (value, key) {
                if (value.area == newValue)
                    scope.readOnlyMode = value.readOnlyMode;
            });
            angular.forEach(scope.data.features, function (value, key) {
                if (value.area == newValue && value.category == "report" && value.feature != "Head Count") {
                    scope.reports.push({
                        feature: value.feature, route: value.route, fullName: value.featureFullName, url: value.url
                    });
                }
                if (value.area == newValue && value.category == "planning") {
                    if ((window.adRole == "ReadOnly" || (scope.readOnlyMode == true && window.adRole != "Admin")) && value.feature == "Projects")//for PSE or Varian a read only user can't create projects
                        var xx = 1;//do nothing jscript doesnt have continue
                    else
                        scope.plans.push({
                            feature: value.feature, route: value.route, fullName: value.featureFullName
                        });
                }
                if (value.area == newValue && value.category == "finance") {
                    scope.finances.push({
                        feature: value.feature, route: value.route, fullName: value.featureFullName
                    });
                }

                if (value.area == newValue && scope.buAccessNotAdmin == 0 && (value.category == "admin")) {
                    scope.admins.push({
                        feature: value.feature, route: value.route, fullName: value.featureFullName
                    });
                }

                if (value.area == newValue && value.category == "planning") {

                    scope.buplanning.push({
                        feature: value.feature, route: value.route, fullName: value.featureFullName
                    });
                }

                if (value.area == newValue && value.category == "docs") {
                    scope.docs.push({
                        feature: value.feature, route: value.route, fullName: value.featureFullName
                    });
                }
                if (value.area == newValue && value.category == "report") {
                    scope.gfpReports.push({
                        feature: value.feature, route: value.route, fullName: value.featureFullName
                    });
                }
            });
            //used for sizing the drop downs;
            window.reportsLength = scope.reports.length;
            window.planningLength = scope.plans.length;
            window.financeLength = scope.finances.length;
            window.adminLength = scope.admins.length;
            window.buplanningLength = scope.buplanning.length;
            window.gfpReportsLength = scope.gfpReports.length;
            angular.forEach(scope.watchDog, function (value, key) {
                if (value.functionalArea == newValue && value.display) {
                    scope.fiscalWeek = value.fiscalWeek;
                    if (window.isAdmin == true)
                        scope.fiscalWeekColor = value.overallStatus == true ? "site-normal" : "site-error";
                    else
                        scope.fiscalWeekColor = "site-normal";
                }
            });
            scope.features = scope.data.features[newValue];
        } catch (e) {
            $customExceptionHandle.errorLogClient(e);
        }
    };
    //slide start
    scope.leftVisible = true;
    function callslide() {

        var result = document.getElementById("slidenav");
        var wrappedResult = angular.element(result);
        if (wrappedResult.hasClass('visible')) {
            //scope.slidestyle = "slide-nav Slidehide";
            $animate.removeClass(wrappedResult, 'visible');
        } else {

            //scope.slidestyle = "slide-nav Slideshow";
            $animate.addClass(wrappedResult, 'visible');
        }
    }
    scope.showLeft = function () {

        scope.leftVisible = scope.leftVisible == true ? false : true;
        var myElement = angular.element(document.querySelector('#slidenav'));
        myElement[0].removeClass("ng-hide");

        callslide();
    };
    //slide end
    scope.showHomePageLoading = false;
    scope.loadHomeData = function (loadHomePage) {
        //if (scope.data == null) {
        scope.showHomePageLoading = true;
        var promiseGet = homeService.getData();
        promiseGet.then(function (result) {
            scope.showHomePageLoading = false;
            scope.data = result.data;
            window.userName = scope.data.user;
            window.badge = scope.data.badge;
            window.firstName = scope.data.firstName;
            window.lastName = scope.data.lastName;
            scope.serverTitle;
            if (scope.data.server == "Production")
                scope.serverTitle = "";
            else
                scope.serverTitle = " (" + scope.data.server + ")";
            scope.title = "Project Resource Management" + scope.serverTitle;
            scope.tabTitle = "MYTEAM" + scope.serverTitle;
            scope.fullName = result.data.fullName == null ? window.userName : result.data.fullName;

            scope.version = "Version: " + scope.data.version;
            scope.sapVersion = scope.data.sapVersion;
            scope.areas = scope.data.allAreas;
            //+DRajendran 11/15/2017 - Validating the user access and redirecting the home page or no access page.
            scope.hasAccessToApplication = scope.data.hasAccessToApplication;
            scope.isSupervisor = scope.data.isSupervisor;
            scope.isReadOnlyUser = scope.data.isReadOnlyUser;
            //-DRajendran 11/15/2017 - Validating the user access and redirecting the home page or no access page.
            if (!scope.hasAccessToApplication)//user doesn't have access to the application
            {
                var url = window.myloc + "noAccess.html";
                document.location.replace(url);
                return;
            }
            if (scope.areas.length > 1) {
                if (scope.areas.indexOf("Engineering") == 1) //I like to default to Engineering if it has it
                    scope.selectedArea = "Engineering";
                else
                    scope.selectedArea = scope.areas[0];
            }
            else
                scope.selectedArea = scope.areas[0];
            scope.watchDog = scope.data.watchDog;
            //define watchdog for PSE only
            angular.forEach(scope.watchDog, function (value, key) {
                if (value.functionalArea == "PSE") {
                    scope.PSEWatchDog.push(value);
                }
            });
            scope.areaChanged(scope.selectedArea);
            angular.forEach(scope.data.states, function (value, key) {// call shutdown and read only on page load
                if (value.area == scope.selectedArea) {
                    scope.shutDown(value.shutDownMode);
                }
            });
            if (loadHomePage) {
                state.go("home"); //load bottom half state
            }
        },
        function (result) {
            //alert(result);
            $customExceptionHandle.errorLogServer(result);
        });
        //}
    }
    scope.loadHomeData(true);
}]);

function getArea() {
    return area;
}

function getadRole() {
    return adRole;
}

function getIsAdmin() {
    return isAdmin;
}

function getIsFinance() {
    return isFinance;
}

function checkforPlan() {
    return isPlanning; //isPlanning or Qram
}

function goPlanMode(val) {
    if (val) {
        tabsTop.style.display = "visible";
        window.onbeforeunload = function (event) {
            var message = 'Are you sure you want to leave this page?\n All changes will be lost.';
            if (typeof event == 'undefined') {
                event = window.event;
            }
            if (event) {
                event.returnValue = message;
            }
            return message;
        }
    }
    else {
        window.onbeforeunload = null;
        tabsTop.style.display = "block";
        body.style.height = (body.clientHeight - 50) + "px";
    }
}

function displayProjectLink(jobno) {
    e = document.getElementById("body");
    scope = angular.element(e).scope();
    scope.$apply(function () {
        scope.displayProjectLink(jobno);
    });
}

function showApp() {
    document.getElementById("body").className = "appDiv";
}

function showApp1() {
    document.getElementById("body").className = "appDiv1";
}

function slideWatchDog() {
    if (area == "PSE" && adRole == "Admin") {
        var height = "250px";
        if (watchDogMenu.className == "watchSlider" || watchDogMenu.classList.contains("close")) {
            watchDogMenu.style.height = height;
            watchDogMenu.style.maxHeight = height;
            watchDogMenu.className = "watchSlider open";
        }
        else {
            watchDogMenu.style.height = 0;
            watchDogMenu.style.maxHeight = 0;
            watchDogMenu.className = "watchSlider close";
        }
    }
}
