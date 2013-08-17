<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security._Terminals.__Update" Codebehind="_Update.ascx.cs" %>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" title="Update Terminal" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Terminal" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" title="Update Terminal" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Updating Terminal" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Terminal" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Updating Terminal" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblBranchID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblTerminalID" runat="server" Visible="False"></asp:Label></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Terminal No<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Terminal Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Terminal Name<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTerminalNo" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="10" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTerminalCode" accessKey="S" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="20" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:textbox id="txtTerminalName" accessKey="S" CssClass="ms-long-disabled" runat="server" BorderStyle="Groove" MaxLength="50" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Status<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Date Created<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtStatus" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="50" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtDateCreated" accessKey="S" runat="server" CssClass="ms-short-disabled" MaxLength="50" BorderStyle="Groove" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
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
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 5px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1: Define Terminal Configuration Options
						</div>
					</td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>VAT<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>eVAT<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Local Tax<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtVAT" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="6" Width="100px">12</asp:textbox>%
									<asp:RequiredFieldValidator id="RequiredFieldValidator9" CssClass="ms-error" runat="server" ControlToValidate="txtVAT" Display="Dynamic" ErrorMessage="'VAT' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'VAT' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtEVAT" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="6" Width="100px">0</asp:textbox>%
									<asp:RequiredFieldValidator id="RequiredFieldValidator10" CssClass="ms-error" runat="server" ControlToValidate="txtEVAT" Display="Dynamic" ErrorMessage="'EVAT' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtEVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'EVAT' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtLocalTax" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="6" Width="100px">0</asp:textbox>%
									<asp:RequiredFieldValidator id="RequiredFieldValidator11" CssClass="ms-error" runat="server" ControlToValidate="txtLocalTax" Display="Dynamic" ErrorMessage="'Local Tax' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLocalTax"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Local Tax' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Form Behaviour<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Discount Code for Senior Citizen<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Machine Serial No<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Accreditation No<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboFormBehaviour" CssClass="ms-short" runat="server">
										<asp:ListItem Value="MODAL">MODAL</asp:ListItem>
										<asp:ListItem Value="NON_MODAL" Selected="True">NON_MODAL</asp:ListItem>
									</asp:dropdownlist>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="cboFormBehaviour" Display="Dynamic" ErrorMessage="'Form Behavior' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboDiscountCode" CssClass="ms-short" runat="server"></asp:dropdownlist>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtMachineSerialNo" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="20" ReadOnly="True"></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtAccreditationNo" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="20" ReadOnly="True"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="8" style="padding-bottom: 2px">
                                    <label>Marquee Message<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=7>
                                    <asp:textbox id="txtMarqueeMessage" accessKey="S" CssClass="ms-long" runat="server" BorderStyle="Groove" MaxLength="255" Width=98% TextMode=MultiLine></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator4" CssClass="ms-error" runat="server" ControlToValidate="txtMarqueeMessage" Display="Dynamic" ErrorMessage="'Marquee Message' must not be left blank." ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkItemVoidConfirmation" runat="server" Text="Check this box if confirmation will be asked when voiding an item."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsVATInclusive" runat="server" Text="Check this box if VAT is inclusive in the item Price."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkEnableEVAT" runat="server" Text="Check this box if you like to enable EVAT."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"><asp:CheckBox ID="chkWillContinueSelectionProduct" runat="server" Text="Check this box if Product Selection Window will re-appear after selecting a product." />
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkShowItemMoreThanZeroQty" runat="server" Text="Check this box if ZERO Quantity Items will not be shown when selecting product."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkShowOnlyPackedTransactions" runat="server" Text="Check this box if only packed transactions will be shown in the resume transaction list."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkShowOneTerminalSuspendedTransactions" runat="server" Text="Check this box if transactions from other terminal will be shown to other terminals."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox ID="chkWillContinueSelectionVariation" runat="server" Text="Check this box if Variation Selection Window will re-appear after selecting a variation." />
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsChargeEditable" runat="server" Text="Check this box if editing of CHARGE in FE will can be edited."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsDiscountEditable" runat="server" Text="Check this box if editing of DISCOUNT in FE will can be edited."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkWithRestaurantFeatures" runat="server" Text="Check this box if WithRestaurantFeatures will be checked."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsTouchScreen" runat="server" Text="Check this box if Monitor Is Touch-Screen."></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkWillPrintGrandTotal" runat="server" Text="Check this box if Grand Total will be printed in FE reports."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkReservedAndCommit" runat="server" Text="Check this box if reserved and commit is enabled."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkShowCustomerSelection" runat="server" Text="Check this box if Customer Selection will be displayed in FE during selection of customer."></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
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
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 5px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 2: Define Cut off Time
						</div>
					</td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Cutt-Off time<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Start Cut-Off time<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>End Cut-Off time<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkCheckCutOffTime" runat="server" Text="[checked=yes] [unchecked=no]" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtStartCutOffTime" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator7" CssClass="ms-error" runat="server" ControlToValidate="txtStartCutOffTime" Display="Dynamic" ErrorMessage="'Start CutOff Time' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtEndCutOffTime" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator8" CssClass="ms-error" runat="server" ControlToValidate="txtEndCutOffTime" Display="Dynamic" ErrorMessage="'End CutOff Time' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
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
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 5px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 3: Printer, Turret and Cash Drawer Information
						</div>
					</td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Printing Preferences<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Receipt Type<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Maximum Receipt Width<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboAutoPrint" CssClass="ms-short" runat="server">
										<asp:ListItem Value="0" Selected="True">Normal</asp:ListItem>
										<asp:ListItem Value="1">Auto</asp:ListItem>
										<asp:ListItem Value="2">AskFirst</asp:ListItem>
                                        <asp:ListItem Value="3">Disable</asp:ListItem>
									</asp:dropdownlist>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboReceiptType" CssClass="ms-short" runat="server"></asp:dropdownlist>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtMaxReceiptWidth" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="2">40</asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" CssClass="ms-error" runat="server" ControlToValidate="txtMaxReceiptWidth" Display="Dynamic" ErrorMessage="'Maximum Receipt Width' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMaxReceiptWidth"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Max Receipt Width' must be in number."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Printer name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Sales invoice printer name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Turret name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Cash drawer name<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtPrinterName" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="20"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" CssClass="ms-error" runat="server" ControlToValidate="txtPrinterName" Display="Dynamic" ErrorMessage="'Printer Name' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtSalesInvoicePrinterName" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="30"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator12" CssClass="ms-error" runat="server" ControlToValidate="txtSalesInvoicePrinterName" Display="Dynamic" ErrorMessage="'Sales Invoice Printer Name' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtTurretName" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="20"></asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator6" CssClass="ms-error" runat="server" ControlToValidate="txtTurretName" Display="Dynamic" ErrorMessage="'Turret Name' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtCashDrawerName" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="520"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" CssClass="ms-error" runat="server" ControlToValidate="txtCashDrawerName" Display="Dynamic" ErrorMessage="'Cash Drawer Name' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsPrinterAutoCutter" runat="server" Text="Check this box if printer is auto-cutter" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkIsPrinterDotmatrix" runat="server" Text="Check this box if printer is dot-matrix" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkCashCountBeforeReport" runat="server" Text="Check this box if cash count before printing" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox id="chkPreviewTerminalReport" runat="server" Text="Check this box if preview of report is enabled" CssClass="ms-short"></asp:CheckBox>
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

