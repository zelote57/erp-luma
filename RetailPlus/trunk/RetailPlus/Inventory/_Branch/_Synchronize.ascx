<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._Branch.__Synchronize" Codebehind="_Synchronize.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" style="height: 21px"><asp:imagebutton id="imgCancel" ToolTip="Cancel uploading stocks" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" BorderWidth="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td nowrap="nowrap" style="height: 21px"><asp:linkbutton id="cmdCancel" ToolTip="Cancel uploading stocks" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
		    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<table id="TABLE1" width="100%" border="0" onclick="return TABLE1_onclick()"><tbody><tr><td style="padding-bottom: 10px; PADDING-TOP: 8px" class="ms-descriptiontext" colspan="3"><FONT color=red>*</FONT> Indicates a required field </td></tr><tr><td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td></tr><tr><td valign=top colspan="3"><DIV style="padding-bottom: 8px" class="ms-sectionheader">Step 1:&nbsp;Synchronize Branch inventory count.</DIV><DIV style="padding-bottom: 10px" class="ms-descriptiontext">Make sure the branch is connected then select the branch you want&nbsp; to synchronize then click 'Synchronize now'.</DIV></td></tr><tr><td style="padding-bottom: 20px" valign=top></td><td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; padding-bottom: 20px" class="ms-authoringcontrols ms-formwidth" valign=top colspan=2><table cellspacing="0" cellpadding="1" border="0"><tbody><tr><td class="ms-toolbar" nowrap="nowrap"><asp:Label id="Label4" runat="server">Select branch to synchronize :</asp:Label> <asp:DropDownList id="cboBranch" CausesValidation="false" runat="server" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> </td><td nowrap="nowrap"><asp:ImageButton accessKey="T" id="imgSynchronize" tabIndex="3" onclick="imgSynchronize_Click" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" Width="16" Height="16" tooltip="Create file for transfer to other branch."></asp:ImageButton> <asp:LinkButton accessKey="T" id="cmdSynchronize" tabIndex=6 onclick="cmdSynchronize_Click" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start inventory count synchronization" Text="Synchronize now"></asp:LinkButton> </td></tr></tbody></table></td></tr><tr><td style="padding-bottom: 20px" valign=top></td><td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; padding-bottom: 20px" class="ms-authoringcontrols ms-formwidth" valign=top colspan=2><asp:Label id="lblError" runat="server"></asp:Label><BR /></td></tr></tbody></table>
</ContentTemplate>
</asp:UpdatePanel>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>