<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._Setup.__LinkAccountsAP" Codebehind="_LinkAccountsAP.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" ToolTip="Update Accounts Payable Link" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Vendor" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" ToolTip="Update Accounts Payable Link" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" ToolTip="Cancel Updating Accounts Payable Link" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel UpdatingContact" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" ToolTip="Cancel Updating Accounts Payable Link" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px">
						<font color="red">*</font> Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1: Financial
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 30px">
                            Choose the account type for tracking payables.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 30px">
                            Choose cheque account for paying bills.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 30px">
                            Choose Expense or Cost of sales account for frieght.<br/>(This is only applicable if
                            you pay freight on purchases).</div>
                       <div class="ms-descriptiontext" style="PADDING-BOTTOM: 30px">
                            Choose Asset account for vendor deposits.<br/>(This is only applicable if
                            you track deposits paid to vendor).</div>
                       <div class="ms-descriptiontext" style="PADDING-BOTTOM: 30px">
                            Choose Expense (or Contra) account for discounts.<br/>(This is only applicable if
                            you take discounts for early payment).</div>
                        <div class="ms-descriptiontext" style="PADDING-BOTTOM: 30px">
                            Choose Expense account for late payments.<br/>(This is only applicable if
                            you pay charge for late payments).</div>
					</td>
					<td class="ms-colspace" style="HEIGHT: 67px">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">
                                    Select account type for tracking payables<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" style="height: 22px"><IMG alt="" src="../..//_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 22px;PADDING-BOTTOM: 25px">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountAPTracking" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" CssClass="ms-error" ErrorMessage="'Account Type' must not be left blank." Display="Dynamic" ControlToValidate="cboChartOfAccountAPTracking" ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">
                                    Select cheque account for paying bills<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../..//_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 22px;PADDING-BOTTOM: 25px">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountAPBills" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountAPBills" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<TR>
								<td class="ms-formspacer"></td>
							</TR>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">
                                    Select Expense or Cost of sales account for freight.<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../..//_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 22px;PADDING-BOTTOM: 25px">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountAPFreight" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountAPFreight" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator></td>
							</tr>
							<TR>
								<td class="ms-formspacer"></td>
							</TR>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">
                                    Select Asset account for vendor deposits<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../..//_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 22px;PADDING-BOTTOM: 25px">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountAPVDeposit" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountAPVDeposit" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator></td>
							</tr>
							<TR>
								<td class="ms-formspacer"></td>
							</TR>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">
                                    Select Expense (or Contra) account for discounts<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../..//_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 22px;PADDING-BOTTOM: 25px">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountAPContra" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountAPContra" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator></td>
							</tr>
							<TR>
								<td class="ms-formspacer"></td>
							</TR>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">
                                    Select Expense account for late payments<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer" style="height: 22px"><IMG alt="" src="../..//_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 22px;PADDING-BOTTOM: 25px">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountAPLatePayment" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountAPLatePayment" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator></td>
							</tr>
							<TR>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-authoringcontrols" width="100%"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
