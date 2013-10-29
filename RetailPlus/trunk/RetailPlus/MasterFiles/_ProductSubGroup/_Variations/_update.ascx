<%@ Reference Control="~/masterfiles/_productsubgroup/_update.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductSubGroup._Variations.__Update" Codebehind="_Update.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" ToolTip="Update Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Product Variation" ImageUrl="/RetailPlus/_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSave" ToolTip="Update Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" ToolTip="Update Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Product Variation" ImageUrl="/RetailPlus/_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" ToolTip="Update Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" ToolTip="Cancel Updating Product Variation" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Product Variation" ImageUrl="/RetailPlus/_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" ToolTip="Cancel Updating Product Variation" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductSubGroupVariationID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductSubGroupID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
			    <tr>
					<td class="ms-descriptiontext" style="padding-bottom: 4px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3">
						<asp:ValidationSummary id="ValidationSummary2" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Variation type<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:dropdownlist id="cboVariationType" CssClass="ms-long" runat="server"></asp:dropdownlist>
									<asp:imagebutton id="imgAdd" ToolTip="Add New Variation" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/newuser.gif" alt="Add New Variation" border="0" width="16" height="16" CausesValidation="False" OnClick="imgAdd_Click"></asp:imagebutton>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Variation type' must not be left blank." Display="Dynamic" ControlToValidate="cboVariationType" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"  colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
