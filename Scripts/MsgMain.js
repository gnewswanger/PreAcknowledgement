function NodeClick(id, attribute) {
    if (id != '') {
        var sender = id._uniqueId;
        //attribute.get_node()._parent._getNodeData().Value
        var argID = "";
        if (typeof attribute.get_node()._itemData == 'undefined') {
            if ((attribute.get_node()._parent._uniqueId == "RadTreeViewMsg") || (attribute.get_node()._parent._uniqueId == "RadTreeViewRsp")) {
                argID = attribute._node._properties._data.value;
            }
            else {
                argID = attribute.get_node()._parent._getNodeData().Value;
            }
        }
        else {
            argID = attribute.get_node()._itemData[0].value;
        }
        DisplayMessagesResponses(argID, sender);
    }
}
function DisplayMessagesResponses(itemId, senderName) {
    var sender = senderName;
    var withdrawnMsg = false;
    var withdrawnRsp = false;
    if (typeof ($("#IncludeWDMsgCheck")) !== 'undefined') withdrawnMsg = $("#IncludeWDMsgCheck").is(":checked");
    if (typeof ($("#IncludeWDRspCheck")) !== 'undefined') withdrawnRsp = $("#IncludeWDRspCheck").is(":checked");

    $("#btnAddEdit").show();
    $.ajax({
        type: "Post",
        data: "{ 'sectionItemIdStr': '" + itemId + "', 'withdrawnMsg': '" + withdrawnMsg + "', 'withdrawnRsp': '" + withdrawnRsp + "' }",
        url: 'WebService/TsMessagesData.asmx/SectionItemMessageResponseByItem',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var msgs = JSON.parse(data.d);
            var messageText = "";
            $("#HFSectionItemId").val(itemId);
            $("#BtnNewMsg").removeClass("hideElement");
            if (msgs.length == 0) {
                messageText = "<li>No Messages Found.</li>";
            }
            else {
                var editRsp = "";
                var editMsg = "";
                var btnRsp = "";
                if (sender == "RadTreeViewMsg") {
                    editMsg = "<a href='#' onclick='ShowEditWindow()'>Edit Status</a>";
                    editRsp = "";
                }
                else {
                    editMsg = "";
                    editRsp = "<a href='#' onclick='ShowEditWindow()'>Edit Status</a>";
                }
                $("#MessageList").html("");
                for (i = 0; i < msgs.length; i++) {
                    var btnAttach = "<input type='button' class='addButton' onclick='AttachFiles(this)' data-msgid='" + msgs[i].MessageID 
                                    + "' data-hasAttch='" + msgs[i].HasAttachments + "' value='Attach Files' />";
                    if (editMsg != '') {
                        editMsg = "<a href='#' onclick='ShowEditWindow(this)' value='" + msgs[i].MessageID + "'>Edit Status</a>";
                    }
                    else {
                        btnRsp = "<input type='button' class='addButton' onclick='ShowAddWindow(this)' data-msgid='" + msgs[i].MessageID + "' value='Add Response' />";
                    }
                    messageText += "<li class='msgText' value='" + msgs[i].MessageID + "'>" + (1 + i) + ".  " + msgs[i].MessageText + "<span class='msgStatus'>"
                            + btnAttach + btnRsp + editMsg + "Status: (" + msgs[i].MessageStatus + ")</span>";
                    if (msgs[i].Responses.length > 0) {
                        messageText += "<ul>";
                        for (j = 0; j < msgs[i].Responses.length; ++j) {
                            if (editRsp != '') {
                                editRsp = "<a href='#' onclick='ShowEditWindow(this)' value='" + msgs[i].Responses[j].ResponseID + "'>Edit Status</a>";
                            }
                            messageText += "<li class='rspText' value='" + msgs[i].MessageID + "'>" + msgs[i].Responses[j].ResponseText + "<span class='msgStatus'>"
                                    + editRsp + "Status: (" + msgs[i].Responses[j].MessageStatus + ")</span></li>";
                        }
                        messageText += "</ul></li>";
                    }
                    else {
                        messageText += "<ul><li class='noRspText'>No response found.</li></ul></li>";
                    }
                }
            }
            $("#MessageList").html(messageText);
        },
        error: function (msg) {
            $("#MessageList").html("");
            if (!$("#BtnNewMsg").hasClass("hideElement"))
                $("#BtnNewMsg").addClass("hideElement");
        },
        complete: function (args, status) {
            //                __doPostBack();
        }
    });
}
function AttachFiles(args) {
    if ((typeof args != 'undefined') && ($("#HfMsgId"))) {
        $("#HfMsgId").val($(args).data("msgid"));
    }
    DisplayAttachedFiles($(args).data("msgid"));
}
function DisplayAttachedFiles(msgid) {
    if (msgid) {
        $.ajax({
            type: "Post",
            data: "{ 'messageIdStr': '" + msgid + "' }",
            url: 'WebService/TsMessagesData.asmx/AttachmentsByMsg',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var tableText = JSON.parse(data.d);
                $("#UploadedFileTable").html(tableText);
            },
            error: function (msg) {
                $("#UploadedFileTable").html("Oops! Get upload files failed.");
            },
            complete: function () {
                $("#FileAttachments").fadeIn('Fast');
                SetLockpanel();
            }
        });
    }
}
function ShowEditWindow(args) {
    $("#MsgEditDlg, #RspnsEditDlg").fadeIn('Fast');
    if ((args != '') && ($("#HfMsgId"))) $("#HfMsgId").val(args.attributes['value'].value);
    SetLockpanel();
};
function ShowAddWindow(args) {
    if ($("#MessageText").length) {
        $("#MessageText").text("Message: " + args.parentNode.parentNode.innerText.substr(0, args.parentNode.parentNode.innerText.indexOf("Status")));
    }
    $("#MsgAddDlg, #RspnsAddDlg").fadeIn('Fast');
    if ((typeof args != 'undefined') && ($("#HfMsgId"))) {
        $("#HfMsgId").val($(args).data("msgid"));
    }
    SetLockpanel();
};
function FadePreferDialog() {
    UnsetLockpanel();
    $("#MsgEditDlg, #MsgAddDlg, #RspnsEditDlg, #RspnsAddDlg, #FileAttachments").fadeOut(function () {
    });
};
function HighlightTvItem() {
    $("#RadTreeViewMsg div.rtSelected span.rtIn").addClass("tvMessageExists");
}
function FileUpload1_FilesUploaded(sender, args) {
    __doPostBack()
}
function ShowDeleteBtn() {
    var chkbxs = $("#UploadedFileTable input:checked")
    if (chkbxs.length > 0) {
        $("#DeleteBtnDiv").removeClass("hidden");
    }
    else {
        if (!$("#DeleteBtnDiv").hasClass("hidden")) {
            $("#DeleteBtnDiv").addClass("hidden");
        }
    }
}
function DeleteAttachedFiles() {
    var chkbxs = $("#UploadedFileTable input:checked")
    if (chkbxs.length > 0) {
        var msgId = $(chkbxs[0]).nextAll("a").data("msgid");
        var idArray = new Array();
        for (i = 0; i < chkbxs.length; i++) {
            var id = $(chkbxs[i]).nextAll("a").data("id");
            var file = $(chkbxs[i]).nextAll("a").attr("href");
            idArray.push({ 'id': id, 'src': file });
        }
        $.ajax({
            type: "Post",
            data: "{ 'AttachIdArry': '" + JSON.stringify(idArray) + "' }",
            url: 'WebService/TsMessagesData.asmx/DeleteAttachmentsByAttchID',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#DeleteBtnDiv").addClass("hidden");
                var retVal = data.d;
                if (retVal === 'True') {
                    DisplayAttachedFiles(msgId);
                }
                else {
                    alert("Oops! Failed to delete some files.");
                }
            },
            error: function () {
                alert("Oops! Failed to delete some files.");
            }
        });
    }
}
function SetLockpanel() {
    var lock = $('div#LockPanel');
    if (lock) {
        lock.removeClass('LockOff')
        lock.addClass('LockOn');
    }
}
function UnsetLockpanel() {
    var lock = $('div#LockPanel');
    if (lock) {
        lock.removeClass('LockOn');
        lock.addClass('LockOff');
    }
}
