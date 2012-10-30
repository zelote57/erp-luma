<%@ Reference Control="~/masterfiles/_productgroup/_insert.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductGroup._UnitsMatrix.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/DocumentScripts.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" ToolTip="Add New Product Group Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Group Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" ToolTip="Add New Product Group Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" ToolTip="Add New Product Group Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Group Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Product Group Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Product Group Variation" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Product Group Variation" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Product Group Variation" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblGroupID" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px">
						<font color="red">*</font> Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Base unit value (Quantity)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Base unit</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Bottom unit value (Quantity)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Bottom unit<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtBaseUnitValue" CssClass="ms-short" runat="server" onkeypress="AllNum()">1</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Base Unit Value' must not be left blank." Display="Dynamic" ControlToValidate="txtBaseUnitValue" ForeColor=" "></asp:requiredfieldvalidator>
									<asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ControlToValidate="txtBaseUnitValue" Display="Dynamic" ErrorMessage="'Base Unit Value' must be in number." ForeColor=" " Type="Double" Operator="DataTypeCheck"></asp:CompareValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtBaseUnit" CssClass="ms-short" runat="server" Width="157" Enabled="False"></asp:TextBox>
									<asp:Label id="lblBaseUnitID" runat="server" Visible="False"></asp:Label>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtBottomUnitValue" CssClass="ms-short" runat="server" onkeypress="AllNum()">1</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Bottom Unit Value' must not be left blank." Display="Dynamic" ControlToValidate="txtBottomUnitValue" ForeColor=" "></asp:requiredfieldvalidator>
									<asp:CompareValidator id="CompareValidator3" CssClass="ms-error" runat="server" ControlToValidate="txtBottomUnitValue" Display="Dynamic" ErrorMessage="'Bottom Unit Value' must be in number." ForeColor=" " Type="Double" Operator="DataTypeCheck"></asp:CompareValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:DropDownList id="cboBottomUnit" CssClass="ms-short" runat="server" Width="157"></asp:DropDownList>
                                    <asp:ImageButton ID="imgAdd" runat="server" AccessKey="N" alt="Add New Unit" border="0"
                                        CausesValidation="False" CssClass="ms-toolbar" Height="16" ImageUrl="/RetailPlus/_layouts/images/newuser.gif"
                                        OnClick="imgAdd_Click" TabIndex="1" ToolTip="Add New Unit" Width="16" />
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Bottom Unit' must not be left blank." Display="Dynamic" ControlToValidate="cboBottomUnit" ForeColor=" "></asp:requiredfieldvalidator>
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
					<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
