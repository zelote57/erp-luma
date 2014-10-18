<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits._Customers.__Update" Codebehind="_Update.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" title="Update Customer" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Customer" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" title="Update Customer" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel UpdatingContact" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel UpdatingContact" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel UpdatingContact" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
                    <td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">&nbsp;Select billing date of report to print :</td>
								<td nowrap="nowrap"><asp:dropdownlist id="cboBillingDate" CssClass="ms-short" runat="server"></asp:dropdownlist>&nbsp;</td>
							</tr>
						</table>
					</td>
                    <td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrintBilling" title="Print current selected billing report" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/print.gif" alt="Print selected billing report" border="0" width="16" height="16" OnClick="imgPrintBilling_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdPrintBilling" title="Print current selected billing report" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdPrintBilling_Click">Print Billing</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblContactID" runat="server" Visible="False"></asp:Label>
            <asp:dropdownlist id="cboModeOfTerms" CssClass="ms-short" runat="server" Visible="false">
				<asp:ListItem Value="0" Selected="True">Daily</asp:ListItem>
				<asp:ListItem Value="1">Monthly</asp:ListItem>
				<asp:ListItem Value="2">Yearly</asp:ListItem>
			</asp:dropdownlist>
            <asp:TextBox id="txtTerms" runat="server" accesskey="G" CssClass="ms-short-numeric-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="true" Visible="false">0</asp:TextBox>
            <asp:TextBox id="txtDebit" runat="server" accesskey="G" CssClass="ms-short-numeric-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="true" Visible="false">0</asp:TextBox>
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
                                    <label>Group Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Contact Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Contact Name<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboGroup" CssClass="ms-short" runat="server" Width="157px"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" CssClass="ms-error" ForeColor=" " ControlToValidate="cboGroup" Display="Dynamic" ErrorMessage="'Group' must not be left blank."></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtContactCode" accessKey="C" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="25"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Contact code.' must not be left blank." Display="Dynamic" ControlToValidate="txtContactCode" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:textbox id="txtContactName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtContactName" Display="Dynamic" ErrorMessage="'Contact name.' must not be left blank." ForeColor=" "></asp:requiredfieldvalidator>
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
                                    <label>Business Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Telephone No<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboDepartment" CssClass="ms-short" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Department.' must not be left blank." Display="Dynamic" ControlToValidate="cboDepartment" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboPosition" CssClass="ms-short" runat="server">
									</asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Position.' must not be left blank." Display="Dynamic" ControlToValidate="cboPosition" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBusinessName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTelephoneNo" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
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
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:TextBox id="txtAddress" runat="server" CssClass="ms-long" MaxLength="120" BorderStyle="Groove" Width="80%"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:TextBox id="txtRemarks" runat="server" CssClass="ms-long" MaxLength="120" BorderStyle="Groove" Width="80%"></asp:TextBox>
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
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit Card No<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit Card Award Date<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Expiry Date<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit Card Status / Credit Status<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditCardNo" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="25" ReadOnly="true"></asp:textbox>
                                    <asp:dropdownlist id="cboCreditCardType" CssClass="ms-short-disabled" runat="server" Width="157px" Enabled="false"></asp:dropdownlist>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCreditAwardDate" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="25" ReadOnly="true"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtExpiryDate" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="25" ReadOnly="true"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboCreditCardStatus" CssClass="ms-short-disabled" runat="server" Width="157px" Enabled="false"></asp:dropdownlist>
			                        (<asp:Label id="lblCreditCardActive" runat="server"></asp:Label>)
                                    <asp:CheckBox id="chkIsCreditAllowed" runat="server" Text="IsCredit Allowed" CssClass="ms-short" Enabled="false" Visible="false"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Credit Limit<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Current Credit<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Payments<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Current Balance<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCreditLimit" runat="server" accesskey="G" CssClass="ms-short-numeric-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="true">0</asp:TextBox>
									<asp:RequiredFieldValidator ID="Requiredfieldvalidator7" runat="server" ControlToValidate="txtCreditLimit"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Credit Limit' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCreditLimit"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Credit Limit' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCredit" runat="server" accesskey="G" CssClass="ms-short-numeric-disabled" MaxLength="11" BorderStyle="Groove" onkeypress="AllNum()" ReadOnly="true">0</asp:TextBox>
									<asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ControlToValidate="txtCredit"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Credit' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCredit"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Credit' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtPaidAmount" accessKey="C" CssClass="ms-short-numeric-disabled" runat="server" BorderStyle="Groove" MaxLength="25" ReadOnly="true"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCurrentBalance" accessKey="C" CssClass="ms-short-numeric-disabled" runat="server" BorderStyle="Groove" MaxLength="25" ReadOnly="true"></asp:textbox>
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
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
                <tr>
					<td class="ms-descriptiontext" style="padding-bottom: 2px; PADDING-TOP: 8px" colspan="3"><b>Purchases & Charges</b>&nbsp;[<asp:Label id="lblLastBillingDate" runat="server"></asp:Label>]</td>
				</tr>
                <tr>
                    <td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <asp:datalist id="lstPurchases" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstPurchases_ItemDataBound" AlternatingItemStyle-CssClass="ms-alternating">
				            <HeaderTemplate>
					            <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						            <colgroup>
                                        <col width="10">
                                        <col width="12%">
							            <col width="13%">
							            <col width="45%">
                                        <col width="10%">
                                        <col width="10%">
                                        <col width="10%">
							            <col width="1%">
						            </colgroup>
						            <tr>
							            <TH class="ms-vh2" style="padding-bottom: 4px">
								            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							            <TH class="ms-vh2" style="padding-bottom: 4px">
								            <asp:hyperlink id="SortByTransactionNo" runat="server">Trans. No</asp:hyperlink></TH>
							            <TH class="ms-vh2" style="padding-bottom: 4px">
								            <asp:hyperlink id="SortByTransactionDate" runat="server">Trans. Date</asp:hyperlink></TH>
							            <TH class="ms-vh2" style="padding-bottom: 4px">
								            <asp:hyperlink id="SortByCreditReason" runat="server">Description</asp:hyperlink></TH>
                                        <TH class="ms-vh2" style="padding-bottom: 4px" align="right">
								            <asp:hyperlink id="SortByCredit" runat="server">Credit</asp:hyperlink></TH>
                                        <TH class="ms-vh2" style="padding-bottom: 4px" align="right">
								            <asp:hyperlink id="SortByCreditPaid" runat="server">Credit Paid</asp:hyperlink></TH>
                                        <TH class="ms-vh2" style="padding-bottom: 4px" align="right">
								            <asp:hyperlink id="SortByBalance" runat="server">Balance</asp:hyperlink></TH>
                                        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						            </tr>
					            </table>
				            </HeaderTemplate>
				            <ItemTemplate>
					            <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						            <colgroup>
							            <col width="10">
                                        <col width="12%">
							            <col width="13%">
							            <col width="45%">
                                        <col width="10%">
                                        <col width="10%">
                                        <col width="10%">
							            <col width="1%">
						            </colgroup>
						            <tr>
							            <td class="ms-vb-user">
								            <input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
                                            <img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" />
							            </td>
							            <td class="ms-vb-user">
								            <asp:HyperLink ID="lnkTransactionNo" Runat="server" Target="_blank"></asp:HyperLink>
							            </td>
							            <td class="ms-vb-user">
								            <asp:Label ID="lblTransactionDate" Runat="server"></asp:Label>
							            </td>
                                        <td class="ms-vb-user">
								            <asp:Label ID="lblCreditReason" Runat="server"></asp:Label>
							            </td>
							            <td class="ms-vb-user" align="right">
								            <asp:Label ID="lblCredit" Runat="server"></asp:Label>
							            </td>
                                        <td class="ms-vb-user" align="right">
								            <asp:Label ID="lblCreditPaid" Runat="server"></asp:Label>
							            </td>
                                        <td class="ms-vb-user" align="right">
								            <asp:Label ID="lblBalance" Runat="server"></asp:Label>
							            </td>
							            <td class="ms-vb2">
								            <A class="DropDown" id="anchorDown" href="" runat="server">
									            <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							            </td>
						            </tr>
					            </table>
				            </ItemTemplate>
			            </asp:datalist>
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
