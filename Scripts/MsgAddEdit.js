function ShowEditWindow() {
    $("#MsgEditDlg, #RspnsEditDlg").fadeIn('Fast');
    var lock = document.getElementById('LockPanel');
    if (lock) {
        lock.className = 'LockOn';
    }
};
function ShowAddWindow() {
    $("#MsgAddDlg, #RspnsAddDlg").fadeIn('Fast');
    var lock = document.getElementById('LockPanel');
    if (lock) {
        lock.className = 'LockOn';
    }
};
function FadePreferDialog() {
    $("#MsgEditDlg, #MsgAddDlg, #RspnsEditDlg, #RspnsAddDlg").fadeOut(function () {
        $("#MsgEditDlg, #MsgAddDlg").css('display', 'none');
    });
};
