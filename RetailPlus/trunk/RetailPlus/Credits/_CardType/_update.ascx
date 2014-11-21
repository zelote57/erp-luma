<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits._CardType.__Update" Codebehind="_Update.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" title="Update Card Type" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Card Type" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSave" title="Update Card Type" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" title="Update Card Type" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Card Type" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" title="Update Card Type" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Updating Card Type" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Card Type" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Updating Card Type" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblCardTypeID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblCreatedOn" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
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
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Card type code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Card type Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>With Guarantor<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCardTypeCode" runat="server" accesskey="C" CssClass="ms-short" MaxLength="30" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Card Type Code' must not be left blank." Display="Dynamic" ControlToValidate="txtCardTypeCode"></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"  colspan="3">
                                    <asp:TextBox id="txtCardTypeName" runat="server" accesskey="G" CssClass="ms-long" MaxLength="30" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Card Type Name' must not be left blank." Display="Dynamic" ControlToValidate="txtCardTypeName"></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkWithGuarantor" runat="server" Text=" Check if requires Guarantor." Checked="False"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Finance Charge - 30th(%)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Late Penalty Charge - 30th(%)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Minimum Amount Due - 30th<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Minimum Percentage Due - 30th(%)<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditFinanceCharge" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator3" CssClass="ms-error" runat="server" ControlToValidate="txtCreditFinanceCharge" Display="Dynamic" ErrorMessage="'Finance Charge' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCreditFinanceCharge"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Finance Charge' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditLatePenaltyCharge" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator7" CssClass="ms-error" runat="server" ControlToValidate="txtCreditLatePenaltyCharge" Display="Dynamic" ErrorMessage="'Late Penalty Charge' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtCreditLatePenaltyCharge"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Late Penalty Charge' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-authoringcontrols">
                                    <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditMinimumAmountDue" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator4" CssClass="ms-error" runat="server" ControlToValidate="txtCreditMinimumAmountDue" Display="Dynamic" ErrorMessage="'Minimum AmountDue' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCreditMinimumAmountDue"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Minimum AmountDue' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditMinimumPercentageDue" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator5" CssClass="ms-error" runat="server" ControlToValidate="txtCreditMinimumPercentageDue" Display="Dynamic" ErrorMessage="'Minimum Percentage Due' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCreditMinimumPercentageDue"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Minimum Percentage Due' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Finance Charge - 15th(%)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Late Penalty Charge- 15th(%)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Minimum Amount Due - 15th<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Minimum Percentage Due - 15th(%)<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditFinanceCharge15th" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator6" CssClass="ms-error" runat="server" ControlToValidate="txtCreditFinanceCharge15th" Display="Dynamic" ErrorMessage="'Finance Charge' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCreditFinanceCharge15th"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Finance Charge' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditLatePenaltyCharge15th" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator9" CssClass="ms-error" runat="server" ControlToValidate="txtCreditLatePenaltyCharge15th" Display="Dynamic" ErrorMessage="'Late Penalty Charge' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCreditLatePenaltyCharge15th"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Late Penalty Charge' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-authoringcontrols">
                                    <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditMinimumAmountDue15th" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator10" CssClass="ms-error" runat="server" ControlToValidate="txtCreditMinimumAmountDue15th" Display="Dynamic" ErrorMessage="'Minimum AmountDue' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtCreditMinimumAmountDue15th"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Minimum AmountDue' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditMinimumPercentageDue15th" accessKey="P" runat="server" CssClass="ms-short-numeric" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0.00</asp:textbox>%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator11" CssClass="ms-error" runat="server" ControlToValidate="txtCreditMinimumPercentageDue15th" Display="Dynamic" ErrorMessage="'Minimum Percentage Due' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtCreditMinimumPercentageDue15th"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Minimum Percentage Due' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
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
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
