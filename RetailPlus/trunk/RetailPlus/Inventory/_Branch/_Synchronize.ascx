<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._Branch.__Synchronize" Codebehind="_Synchronize.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap style="height: 21px"><asp:imagebutton id="imgCancel" ToolTip="Cancel uploading stocks" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" BorderWidth="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap style="height: 21px"><asp:linkbutton id="cmdCancel" ToolTip="Cancel uploading stocks" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
		    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<TABLE id="TABLE1" width="100%" border=0 onclick="return TABLE1_onclick()"><TBODY><TR><TD style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" class="ms-descriptiontext" colSpan=3><FONT color=red>*</FONT> Indicates a required field </TD></TR><TR><TD class="ms-sectionline" colSpan=3 height=1><IMG alt="" src="../../_layouts/images/empty.gif" /></TD></TR><TR><TD vAlign=top colSpan=3><DIV style="PADDING-BOTTOM: 8px" class="ms-sectionheader">Step 1:&nbsp;Synchronize Branch inventory count.</DIV><DIV style="PADDING-BOTTOM: 10px" class="ms-descriptiontext">Make sure the branch is connected then select the branch you want&nbsp; to synchronize then click 'Synchronize now'.</DIV></TD></TR><TR><TD style="PADDING-BOTTOM: 20px" vAlign=top></TD><TD style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" class="ms-authoringcontrols ms-formwidth" vAlign=top colSpan=2><TABLE cellSpacing=0 cellPadding=1 border=0><TBODY><TR><TD class="ms-toolbar" noWrap><asp:Label id="Label4" runat="server">Select branch to synchronize :</asp:Label> <asp:DropDownList id="cboBranch" CausesValidation="false" runat="server" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> </TD><TD noWrap><asp:ImageButton accessKey="T" id="imgSynchronize" tabIndex=3 onclick="imgSynchronize_Click" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" Width="16" Height="16" tooltip="Create file for transfer to other branch."></asp:ImageButton> <asp:LinkButton accessKey="T" id="cmdSynchronize" tabIndex=6 onclick="cmdSynchronize_Click" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start inventory count synchronization" Text="Synchronize now"></asp:LinkButton> </TD></TR></TBODY></TABLE></TD></TR><TR><TD style="PADDING-BOTTOM: 20px" vAlign=top></TD><TD style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" class="ms-authoringcontrols ms-formwidth" vAlign=top colSpan=2><asp:Label id="lblError" runat="server"></asp:Label><BR /></TD></TR></TBODY></TABLE>
</ContentTemplate>
</asp:UpdatePanel>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>