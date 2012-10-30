<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.__Processing" Codebehind="_Processing.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript" language="javascript"> 
    var ModalProgress ='<%= ModalProgress.ClientID %>';         
</script> 
<script type="text/javascript" src="/RetailPlus/_Scripts/DocumentScripts.js"></script>		
<div>
	<asp:Panel ID="panelUpdateProgress" runat="server" CssClass="ms-updateProgress">
		<asp:UpdateProgress ID="UpdateProg1" DisplayAfter="10" runat="server">
			<ProgressTemplate>
				 <div style="position: absolute; top: 30%; left:40%; text-align: center;">
					<img src="/RetailPlus/_layouts/images/processing.gif" style="vertical-align: middle" alt="Processing" />
					<b>Processing, please wait...</b>
				</div>
			</ProgressTemplate>
		</asp:UpdateProgress>
	</asp:Panel>
	<asp:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
		BackgroundCssClass="ms-modalBackGround" PopupControlID="panelUpdateProgress" DropShadow="True" />
</div>