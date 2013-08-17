<%@ Control Language="c#" Inherits="AceSoft.RetailPlus._Company.__Details" Codebehind="_Details.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Company Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>CompanyName<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Date Created<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Tax Idenification No<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCompanyCode" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCompanyName" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="150" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtDateCreated" accessKey="Z" runat="server" BorderStyle="Groove" MaxLength="100" CssClass="ms-short-disabled" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTIN" accessKey="E" runat="server" CssClass="ms-short-disabled" MaxLength="100" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
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
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtAddress1" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="2000" Height="40px" TextMode="MultiLine" Rows="5" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtAddress2" accessKey="S" runat="server" CssClass="ms-long-disabled" MaxLength="2000" Height="40px" TextMode="MultiLine" Rows="5" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>City<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>State<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Zip Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Country<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCity" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="100" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtState" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="100" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtZip" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="15" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCountry" accessKey="Z" runat="server" BorderStyle="Groove" MaxLength="100" CssClass="ms-short-disabled" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Office Phone<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Direct Phone<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Fax No<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Mobile No<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtOfficePhone" accessKey="O" runat="server" CssClass="ms-short-disabled" MaxLength="150" TextMode="MultiLine" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtDirectPhone" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="150" TextMode="MultiLine" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtFaxNumber" accessKey="F" runat="server" CssClass="ms-short-disabled" MaxLength="150" TextMode="MultiLine" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtMobile" accessKey="M" runat="server" CssClass="ms-short-disabled" MaxLength="150" TextMode="MultiLine" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Email Address<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Web Site<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtEmail" accessKey="E" runat="server" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtWebSite" accessKey="W" runat="server" BorderStyle="Groove" MaxLength="100" CssClass="ms-long-disabled" ReadOnly="True"></asp:textbox>
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
					<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
