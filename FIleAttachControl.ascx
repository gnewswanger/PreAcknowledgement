<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FileAttachControl.ascx.vb"
    Inherits="PreAcknowledge.FileAttachControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div>
    <div id="Uploader">
        <label>Attach Files: <span class="instructions">Select files to upload.</span></label>
        <telerik:RadAsyncUpload ID="FileUpload1" CssClass="RadUpload" runat="server" MultipleFileSelection="Automatic"
            BorderStyle="None" HideFileInput="True" OnClientFilesUploaded="FileUpload1_FilesUploaded" UploadedFilesRendering="BelowFileInput" OnClientFilesSelected="SetLockpanel">
        </telerik:RadAsyncUpload>
        <div id="DeleteBtnDiv" class="hidden">
            <input type="button" id="BtnDeleteAttached" class="aspButtons" value="Delete Selected" onclick="DeleteAttachedFiles()" />
        </div> 
    </div>
    <div id="UploadedFileTable">
    </div>
    <div>
        <input type="button" id="BtnCLoseAttach" class="aspButtons" onclick="FadePreferDialog()"
            value="Close" />
    </div>
</div>
