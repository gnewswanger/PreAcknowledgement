function DisplayEmailNotice(cntId, jobno) {
    $("#EmailNotice").fadeIn('Fast');
    var lock = document.getElementById('LockPanel');
    if (lock) {
        lock.className = 'LockOn';
    }
    GetContactInfo(cntId, jobno);
};
function FadeModalDialog() {
    $("#EmailNotice").fadeOut(function () {
    });
};
function GetContactInfo(cntId, jobno) {
    var id = cntId;
    var JobNumber = jobno;
    $.ajax({
        type: "Post",
        data: "{ 'contactIdStr': '" + id + "' }",
        url: 'WebService/TsMessagesData.asmx/PrimeContactInfo',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var infoArry = JSON.parse(data.d);
            var contactText = "<div><label class='labelDesc'>Primary Contact: </label><label>" + infoArry.FullName + "</label></div>";
            contactText += "<div><label class='labelDesc'>Dealer: </label><label>" + infoArry.DealerName + "</label></div>";
            contactText += "<div><label class='labelDesc'>Email Address: </label><label id='EmailAddrLabel' runat='server'>" + infoArry.EmailAddress + "</label></div>";
            $("#ContactInfo").html(contactText);
            var text = infoArry.FirstName + ", \r\n \r\n";
            text += "This is a friendly reminder that there are some unresolved messages for your recently submitted order '" + JobNumber + "'. \r\n \r\n";
            text += "Your messages can be accessed from the DealeNet homepage by clicking on the MessageCenter menu.\r\n \r\nThank you for your assistance. \r\n \r\n"

            $("#TextArea1").html(text);
        },
        error: function (msg) {
            $("#ContactInfo").html("");
        }
    });

};