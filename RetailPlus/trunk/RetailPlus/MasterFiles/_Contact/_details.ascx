<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Contact.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgBack" accessKey="C" tabIndex="3" height="16" width="16" border="0" ToolTip="Back" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="cmdBack_Click" >Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblContactID" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Group Code</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Contact Code</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Contact Name</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboGroup" CssClass="ms-short-disabled" runat="server" Width="157px" Enabled="False"></asp:dropdownlist>&nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtContactCode" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="25" ReadOnly="True"></asp:textbox>&nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtContactName" accessKey="G" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="75" ReadOnly="True"></asp:textbox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Department<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Position<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboDepartment" CssClass="ms-short" runat="server" Enabled=false>
									</asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Department.' must not be left blank." Display="Dynamic" ControlToValidate="cboDepartment" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboPosition" CssClass="ms-short" runat="server" Enabled=false>
									</asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Position.' must not be left blank." Display="Dynamic" ControlToValidate="cboPosition" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Mode of terms</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Terms</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Debit</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboModeOfTerms" CssClass="ms-short-disabled" runat="server" Enabled="False">
										<asp:ListItem Value="0" Selected="True">Daily</asp:ListItem>
										<asp:ListItem Value="1">Monthly</asp:ListItem>
										<asp:ListItem Value="2">Yearly</asp:ListItem>
									</asp:dropdownlist>&nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtTerms" runat="server" accesskey="G" CssClass="ms-short-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="True">0</asp:TextBox>
                                    &nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtDebit" runat="server" accesskey="G" CssClass="ms-short-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="True">0</asp:TextBox>
                                    &nbsp;
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCredit" runat="server" accesskey="G" CssClass="ms-short-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="True">0</asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Business Name</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Telephone No</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit limit</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBusinessName" accessKey="G" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="75" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTelephoneNo" accessKey="G" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="75" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsCreditAllowed" runat="server" Text="Check to allow credit." Enabled="False" CssClass="ms-short-disabled"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCreditLimit" runat="server" accesskey="G" CssClass="ms-short-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="True">0</asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Address</label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Remarks</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:TextBox id="txtAddress" runat="server" CssClass="ms-long-disabled" MaxLength="120" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:TextBox id="txtRemarks" runat="server" CssClass="ms-long-disabled" MaxLength="120" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
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
