<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.PurchasesAndPayables._Vendor.__Update" Codebehind="_Update.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" title="Update Vendor" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Vendor" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" title="Update Vendor" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" title="Update Vendor" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Vendor" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" title="Update Vendor" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel UpdatingVendor" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel UpdatingVendor" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel UpdatingVendor" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblVendorID" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			    <tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colspan="3">
						<asp:ValidationSummary id="ValidationSummary2" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Group Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Vendor Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Vendor Name<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboGroup" CssClass="ms-short" runat="server" Width="157px"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" CssClass="ms-error" ForeColor=" " ControlToValidate="cboGroup" Display="Dynamic" ErrorMessage="'Group' must not be left blank."></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtVendorCode" accessKey="C" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="25"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Vendor code.' must not be left blank." Display="Dynamic" ControlToValidate="txtVendorCode" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtVendorName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtVendorName" Display="Dynamic" ErrorMessage="'Vendor name.' must not be left blank." ForeColor=" "></asp:requiredfieldvalidator>
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
                                    <asp:dropdownlist id="cboDepartment" CssClass="ms-short" runat="server">
									</asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Department.' must not be left blank." Display="Dynamic" ControlToValidate="cboDepartment" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboPosition" CssClass="ms-short" runat="server">
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
                                    <label>Mode of terms<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Terms<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Debit<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboModeOfTerms" CssClass="ms-short" runat="server">
										<asp:ListItem Value="0" Selected="True">Daily</asp:ListItem>
										<asp:ListItem Value="1">Monthly</asp:ListItem>
										<asp:ListItem Value="2">Yearly</asp:ListItem>
									</asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator15" runat="server" CssClass="ms-error" ErrorMessage="'Mode of terms.' must not be left blank." Display="Dynamic" ControlToValidate="cboModeOfTerms" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtTerms" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()">0</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator16" runat="server" CssClass="ms-error" ErrorMessage="'Terms.' must not be left blank." Display="Dynamic" ControlToValidate="txtTerms" ForeColor=" "></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator6" runat="server" CssClass="ms-error" ErrorMessage="'Terms' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtTerms" ForeColor=" " Type="Double" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtDebit" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()">0</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" CssClass="ms-error" ForeColor=" " ControlToValidate="txtDebit" Display="Dynamic" ErrorMessage="'Debit.' must not be left blank." DESIGNTIMEDRAGDROP="77"></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator7" runat="server" CssClass="ms-error" ErrorMessage="'Debit' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtDebit" ForeColor=" " Type="Double" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCredit" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()">0</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ErrorMessage="'Credit.' must not be left blank." Display="Dynamic" ControlToValidate="txtCredit" ForeColor=" "></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator8" runat="server" CssClass="ms-error" ForeColor=" " ControlToValidate="txtCredit" Display="Dynamic" ErrorMessage="'Credit' must be in number, max of 3 decimal places." Operator="DataTypeCheck" Type="Double"></asp:RegularExpressionValidator>
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
                                    <label>Credit limit<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBusinessName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTelephoneNo" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsCreditAllowed" runat="server" Text="Check to allow credit." CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCreditLimit" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()">0</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator13" runat="server" CssClass="ms-error" ErrorMessage="'Credit Limit' must not be left blank." Display="Dynamic" ControlToValidate="txtCreditLimit" ForeColor=" "></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" CssClass="ms-error" ForeColor=" " ControlToValidate="txtCreditLimit" Display="Dynamic" ErrorMessage="'Credit Limit' must be in number, max of 3 decimal places." Operator="DataTypeCheck" Type="Double"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Address<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Remarks<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:TextBox id="txtAddress" runat="server" CssClass="ms-long" MaxLength="120" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:TextBox id="txtRemarks" runat="server" CssClass="ms-long" MaxLength="120" BorderStyle="Groove"></asp:TextBox>
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
					<td class="ms-sectionline" colspan="3" height="2"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
