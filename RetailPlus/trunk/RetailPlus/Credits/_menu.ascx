<%--<%@ Reference Control="~/_Menu.ascx" %>--%>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits.__Menu" Codebehind="_Menu.ascx.cs" %>
<table height="100%" border="0" cellpadding="0" cellspacing="0" class="ms-navframe">
	<tr valign="top">
		<td id="onetidWatermark" class="ms-navwatermark" dir="ltr">
			<script language="C#"> 
				if (browseris.ie5up && document.all("navWatermark") && document.all("onetidWatermark")) { document.all("navWatermark").fillcolor=document.all("onetidWatermark").currentStyle.color; } </script>
		</td>
		<td width="170" style="PADDING-RIGHT: 2px">
			<div id="mnuInventory" runat="server">
				<table id="SPSWC_NavBar" width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnav" displaytype="v1">
					<tr>
						<td class="ms-navheader">Credits</td>
					</tr>
					<tr>
						<td>
							<div id="Menu_0_0">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCardTypes" runat="server" ToolTip="Display List of Credit Card Types">
													Internal Credit Cards</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsWithoutGuarantors" runat="server" ToolTip="Display List of Customers">
													Customer's w/out Guarantor</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsWithGuarantors" runat="server" ToolTip="Display List of Guarantors">
													Customer's w/ Guarantor</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180" />
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">Reports: Creditor's w/out Guarantor</td>
					</tr>
					<tr>
						<td>
							<div id="Menu_0_1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsWithoutGurantorPurchases" runat="server" title="Print creditor's purchases report">Purchases</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsWithoutGuarantorPayments" runat="server" title="Print creditor's purchases report">Payments</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsLedgerSummary" runat="server" title="Print Guarantor's Ledger Summary report">
													    Creditor's Ledger Summary</asp:HyperLink>
										</td>
                                        <td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="126" />
							</div>
						</td>
					</tr>
                    <tr>
						<td class="ms-navheader">Reports: Creditor's w/ Guarantor</td>
					</tr>
					<tr>
						<td>
							<div id="Div1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsWithGurantorPurchases" runat="server" title="Print creditor's purchases report">Purchases</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCreditorsWithGuarantorPayments" runat="server" title="Print creditor's purchases report">Payments</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkGuarantorsLedger" runat="server" title="Print Guarantor's Ledger Summary report">
													    Guarantor's Ledger Summary</asp:HyperLink>
										</td>
                                        <td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="126" alt="" />
							</div>
						</td>
					</tr>
				</table>
				<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="180" alt="" />
			</div>
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tbody>
				    <tr>
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkChangeCreditType" runat="server" ToolTip="Change credit type of individual members">
								<li>Change I-Credit Type</li></asp:HyperLink>
						</td>
					</tr>
                    <tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkChangeCreditTypeWG" runat="server" title="Change credit type of guarantor's">
								<li>Change G-Credit Type</li></asp:HyperLink>
						</td>
					</tr>
                    <tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkChangeGuarantor" runat="server" title="Change guarantor of member">
								<li>Change Guarantor</li></asp:HyperLink>
						</td>
					</tr>
				</tbody>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="" />
		</td>
	</tr>
</table>
