<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._GJournals.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/PurchasesAndPayables.js"></script>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrint" title="Print this Journal" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Journal" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdPrint" title="Print this Journal" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label5" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Account And Back To GJournals List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Account And Back To GJournals List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Account And Back To GJournals List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To GJournals List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="Td1" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblGJournalID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td style="PADDING-BOTTOM: 10px" vAlign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%" rowSpan="1"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="2"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">
                                    Journal Details</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="6"><label>
										Particulars:</label>
								</td>
							</tr>
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="3">
								    <asp:Label ID="lblRemarks" runat="server" CssClass="ms-error"></asp:Label>
								</td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="6"><label><b>
										Paid Journal(s): </b></label>
								</td>
							</tr>
							<tr>
								<td class="ms-sectionline" colSpan="6" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</TD>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD><asp:datalist id="lstGJournalsDebit" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate1">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByDescription" runat="server">Account</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByMatrixDescription" runat="server">Debit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByQuantity" runat="server">Credit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate1">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblChartOfAccountCodeDebit" Runat="server"></asp:Label>&nbsp;
								<asp:Label ID="lblChartOfAccountNameDebit" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblDebitAmountDebit" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblCreditAmountDebit" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD><asp:datalist id="lstGJournalsCredit" runat="server" Width="100%" ShowFooter="False" ShowHeader="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate2">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="Hyperlink1" runat="server">Account</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="Hyperlink2" runat="server">Debit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="Hyperlink3" runat="server">Credit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate2">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblChartOfAccountCodeCredit" Runat="server"></asp:Label>&nbsp;
								<asp:Label ID="lblChartOfAccountNameCredit" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblDebitAmountCredit" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblCreditAmountCredit" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="A1" href="" runat="server">
									<asp:Image id="Image1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td vAlign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td style="PADDING-BOTTOM: 2px" align="left"><label><b>Debit Amount:</b></label></td>
								<td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblTotalDebitAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td style="PADDING-BOTTOM: 2px" align="left"><label><b>Credit Amount (-):</b></label></td>
								<td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblTotalCreditAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td class="ms-sectionline" colspan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td style="PADDING-BOTTOM: 2px" align="left"><label><b>Total:</b></label></td>
								<td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblTotalAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</TD>
	</tr>
</table>