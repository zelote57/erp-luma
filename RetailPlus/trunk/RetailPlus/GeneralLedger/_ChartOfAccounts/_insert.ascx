<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../_Scripts/GeneralLedger.js"></script>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" ToolTip="Add New Account" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Account" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" ToolTip="Add New Account" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" ToolTip="Add New Account" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Account" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Account" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Account" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Account" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Account" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px">
						<font color="red">*</font> Indicates a required field
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1">
						<A name="InputFormSection1"></A><img alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td style="PADDING-BOTTOM: 20px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1: Account Category 
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Choose account 
							category.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2">
									<label>Select Account Category<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:dropdownlist id="cboAccountCategory" CssClass="ms-short" runat="server" AutoPostBack="false"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Account Category' must not be left blank." Display="Dynamic" ControlToValidate="cboAccountCategory" ForeColor=" "></asp:RequiredFieldValidator></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1">
						<A name="InputFormSection1"></A><img alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td valign="top" style="PADDING-BOTTOM: 20px">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 2:&nbsp;Account 
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
							Enter the account code.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
							Enter the account name.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 1px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	20px">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellSpacing="0" cellPadding="0" border="0" width="100%">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2">
									<label>Account Code<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:TextBox id="txtAccountCode" runat="server" accesskey="C" CssClass="ms-short" MaxLength="30" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Account Code' must not be left blank." Display="Dynamic" ControlToValidate="txtAccountCode"></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2">
									<label>Account Name<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:TextBox id="txtAccountName" runat="server" accesskey="G" CssClass="ms-long" MaxLength="50" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Account Name' must not be left blank." Display="Dynamic" ControlToValidate="txtAccountName"></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1">
						<A name="InputFormSection1"></A><img alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td valign="top" style="PADDING-BOTTOM: 20px">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 3:&nbsp;Figures Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
							Enter opening balance.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
							Debit.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
							Credit.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 1px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	20px">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellSpacing="0" cellPadding="0" border="0" width="100%">
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2">
									<label> &nbsp;+ Debit<font color="red"><strong>:</strong></font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer" style="height: 24px"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%" style="height: 24px">
									<asp:TextBox id="txtDebit" runat="server" accesskey="C" CssClass="ms-short" MaxLength="30" BorderStyle="Groove">0.00</asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2">
									<label> &nbsp;- Credit<font color="red"><strong>:</strong></font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:TextBox id="txtCredit" runat="server" accesskey="C" CssClass="ms-short" MaxLength="30" BorderStyle="Groove">0.00</asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
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
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
