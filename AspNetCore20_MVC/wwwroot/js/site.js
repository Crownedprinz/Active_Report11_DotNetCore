// Write your Javascript code.

var options = {
    element: '#viewer', // id of element that will contain  the viewer
    uiType: 'desktop',
    reportService: { url: 'http://localhost:62533/ActiveReports.ReportService.asmx' },
    //reportService: { url: 'http://localhost:62533/CustomReportService.asmx' }, 
    reportLoaded: function () {
        reportsButtons.prop('disabled', false);
    }
};

var viewer = GrapeCity.ActiveReports.Viewer(options);

$(window).bind('beforeunload', function () {
    viewer.destroy();
});

var uiTypeButtons = $('#btnUIType button');

uiTypeButtons.bind('click', function (ev) {
    ev.stopImmediatePropagation();
    uiTypeButtons.removeClass('active');
    var target = $(ev.target);
    target.addClass('active');
    viewer.option('uiType', target.attr('data-bind'));
});

var reportsButtons = $('#btnReport button');

reportsButtons.bind('click', function (ev) {
    ev.stopImmediatePropagation();
    reportsButtons.removeClass('active');
    var target = $(ev.target);
    target.addClass('active');
    var reportOption = {
        id: target.attr('data-bind')
    };
    reportsButtons.prop('disabled', true);
    viewer.option('report', reportOption);
});