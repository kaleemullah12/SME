$(document).ready(function () {
    $('#HeadLoginView_HeadLoginStatus').button({
        icons: {
            primary: "ui-icon-person"
        },
        text: false
    });
    $('#HeadChangePassword').button({
        icons: {
            primary: "ui-icon-key"
        },
        text: false
    });
    $('.textBoxDate').datepicker({ dateFormat: 'dd/mm/yy' });
    $('.textboxTime').timepicker();
    $(".jqueryTab").tabs();
    var msg = $('#hidMsg').val();
    if (msg) {
        $('#divMsg').append(msg);
        $('#divMsg').dialog({
            autoOpen: true,
            modal: true,
            close: function (event, ui) {
                msg.valueOf('');
            }
        });
    }
});
function goBack() {
    window.history.back()
}
function goForward() {
    window.history.forward()
}
function OpenReport(name) {
    var URL = 'ReportViewer.aspx?name=' + name;
    window.open(URL, 'Report', 'width=1020,height=600,left=50,top=50,location=0,menubar=0,resizable=0,toolbar=0');
}