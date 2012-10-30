<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security._AccessUser.__Details" Codebehind="_details.ascx.cs" %>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgBack" accessKey="C" tabIndex="3" height="16" width="16" border="0" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="cmdBack_Click">Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblUID" runat="server" Visible="False"></asp:Label></TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
						</TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
					    <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>User Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Full Name<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtUserName" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>&nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:textbox id="txtName" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" ReadOnly="True"></asp:textbox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Enter Password<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Confirm Password<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtPassword" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>&nbsp;
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtConfirm" accessKey="C" runat="server" CssClass="ms-long-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>
                                        User group<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboGroup" runat="server" CssClass="ms-long-disabled" Enabled="False"></asp:dropdownlist>&nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Address 1<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Address 2<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtAddress1" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="2000" Height="40px" TextMode="MultiLine" Rows="5" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtAddress2" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="2000" Height="40px" TextMode="MultiLine" Rows="5" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>City<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>State<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Country<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtCity" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtState" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboCountry" CssClass="ms-short-disabled" runat="server"></asp:dropdownlist>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Office No (Separated by comma)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>direct phone numbers (Separated by comma)<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" style="height: 40px">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3 style="height: 40px">
                                    <asp:textbox id="txtOfficePhone" accessKey="O" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Rows="2" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer" style="height: 40px">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3 style="height: 40px">
                                    <asp:textbox id="txtDirectPhone" accessKey="D" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Rows="2" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Home Telephone No (Separated by comma)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Fax numbers(Separated by comma)<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtHomePhone" accessKey="H" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Rows="2" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtFaxNumber" accessKey="F" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Rows="2" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Mobile Nos (Separated by comma)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Email Address<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtMobile" accessKey="M" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Rows="2" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtEmail" accessKey="E" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Rows="2" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Enter page size or the number of records you want to display in a single page.<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtPageSize" accessKey="E" runat="server" CssClass="ms-short-disabled" MaxLength="5" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                    &nbsp;
									
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
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
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
