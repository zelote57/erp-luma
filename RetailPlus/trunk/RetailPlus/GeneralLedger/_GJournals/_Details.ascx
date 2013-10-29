<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._GJournals.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/PurchasesAndPayables.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrint" title="Print this Journal" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Journal" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdPrint" title="Print this Journal" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="Label5" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Account And Back To GJournals List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Account And Back To GJournals List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Account And Back To GJournals List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To GJournals List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="Td1" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblGJournalID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 10px" valign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="30%" rowspan="1"><img alt="" src="../../_layouts/images/company_logo.gif" /></td>
								<td class="ms-formspacer"></td>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowspan="2"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">
                                    Journal Details</label></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label>
										Particulars:</label>
								</td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="3">
								    <asp:Label ID="lblRemarks" runat="server" CssClass="ms-error"></asp:Label>
								</td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label><b>
										Paid Journal(s): </b></label>
								</td>
							</tr>
							<tr>
								<td class="ms-sectionline" colspan="6" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td><asp:datalist id="lstGJournalsDebit" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate1">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<tr>
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
						</tr>
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
						<tr>
							<td class="ms-vb-user">
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblChartOfAccountCodeDebit" Runat="server"></asp:Label>&nbsp;
								<asp:Label ID="lblChartOfAccountNameDebit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblDebitAmountDebit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblCreditAmountDebit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td><asp:datalist id="lstGJournalsCredit" runat="server" Width="100%" ShowFooter="False" ShowHeader="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate2">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<tr>
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
						</tr>
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
						<tr>
							<td class="ms-vb-user">
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblChartOfAccountCodeCredit" Runat="server"></asp:Label>&nbsp;
								<asp:Label ID="lblChartOfAccountNameCredit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblDebitAmountCredit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblCreditAmountCredit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
								<A class="DropDown" id="A1" href="" runat="server">
									<asp:Image id="Image1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td valign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td style="padding-bottom: 2px" align="left"><label><b>Debit Amount:</b></label></td>
								<td style="padding-bottom: 2px" align="right"><asp:label id="lblTotalDebitAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td style="padding-bottom: 2px" align="left"><label><b>Credit Amount (-):</b></label></td>
								<td style="padding-bottom: 2px" align="right"><asp:label id="lblTotalCreditAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td style="padding-bottom: 2px" align="left"><label><b>Total:</b></label></td>
								<td style="padding-bottom: 2px" align="right"><asp:label id="lblTotalAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
