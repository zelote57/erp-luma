<%@ Reference Control="~/_Menu.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger.__Menu" Codebehind="_Menu.ascx.cs" %>
<table height="100%" border="0" cellpadding="0" cellspacing="0" class="ms-navframe">
	<tr valign="top">
		<td id="onetidWatermark" class="ms-navwatermark" dir="ltr">
			<script language="C#"> 
				if (browseris.ie5up && document.all("navWatermark") && document.all("onetidWatermark")) { document.all("navWatermark").fillcolor=document.all("onetidWatermark").currentStyle.color; } </script>
		</td>
		<td width="170" style="PADDING-RIGHT: 2px">
			<div id="mnuInventory" runat="server">
				<table id="SPSWC_NavBar" width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnav" DisplayType="v1">
					<tr>
						<td class="ms-navheader">Banking</td>
					</tr>
					<tr>
						<td>
							<div id="Menu_0_2">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkBankDeposits" runat="server" ToolTip="Bank deposits">
													Bank Deposits</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkWriteCheque" runat="server" ToolTip="Write a check">
													Write Cheques</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkFundTransfers" runat="server" ToolTip="Fund Transfers">
													Fund Transfers</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkReconcileAccounts" runat="server" ToolTip="Reconcile Accounts">
													Reconcile Accounts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">General Ledger</td>
					</tr>
					<tr>
						<td>
							<div id="Menu_0_0">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAccountSummaries" runat="server" title="Display List of Account Summaries">
													Account Summaries</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAccountCategory" runat="server" title="Display List of Account Category">
													Account Category</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkChartOfAccounts" runat="server" title="Display List of Chart of Accounts">
													Chart of Accounts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkGeneralJournals" runat="server" title="Display List of Invoices">
													General Journals</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPaymentJournals" runat="server" title="Display List of Payments">
													Payment Journals</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">Setup</td>
					</tr>
					<tr>
						<td>
							<div id="Menu_0_1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostingDates" runat="server" title="Adjust allowable posting dates">
													Posting Dates</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkBankAccounts" runat="server" title="Display List of Bank Accounts">
													Bank Accounts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkLinkAccountsProduct" runat="server" title="Setup Link Accounts to Products">Product Link Accounts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkLinkAccountsAP" runat="server" title="Setup Link Accounts to Payable">Payable Link Accounts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">Financial Reports</td>
					</tr>
					<tr>
						<td>
							<div id="Div1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkBalanceSheetReport" runat="server" title="Balance Sheet">Balance Sheet</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkChartOfAccountsReport" runat="server" title="Chart of Accounts">Chart of Accounts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkGeneralJournalsReport" runat="server" title="">General Journals</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPaymentJournalReport" runat="server" title="">Payment Journals</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkGeneralLedgerReport" runat="server" title="">General Ledger</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkProfitAndLossReport" runat="server" title="">Profit and Loss</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkTrialBalanceReport" runat="server" title="">Trial Balance</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
							</div>
						</td>
					</tr>
				</table>
				<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="180" alt="">
			</div>
			<!--table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<TBODY>
					<tr>
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif"></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkGeneralJournalAdd" runat="server" ToolTip="Add New General Journal">
								<li>
									New General Journal</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td colspan="2" id="ActionBar2" class="ms-pvtbt"></td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif"></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkPaymentJournalAdd" runat="server" ToolTip="Add New Payment Journal">
								<li>
									New Payment Journal</li></asp:HyperLink>
						</td>
					</tr>
				</TBODY>
			</table!-->
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="">
		</td>
	</tr>
</table>
