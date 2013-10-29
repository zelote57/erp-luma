<%--<%@ Reference Control="~/_Menu.ascx" %>--%>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.PurchasesAndPayables.__Menu" Codebehind="_Menu.ascx.cs" %>
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
						<td class="ms-navheader">Purchases &amp; Payables</td>
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
											<asp:HyperLink id="lnkVendors" runat="server" title="Display List of Vendors" Visible="False">
													Vendors</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseOrders" runat="server" title="Display List of Purchase Orders" Visible="False">
													Purchase Orders</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseReturns" runat="server" title="Display List of Purchase Returns">
													Purchase Returns</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseDebitMemo" runat="server" title="Display List of Purchase Debit Memo">
													Purchase Debit Memo</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPurchaseOrder" runat="server" title="Display List of Posted Purchase Orders">
													Posted Purchase Order</asp:HyperLink>
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
											<asp:HyperLink id="lnkPostedPO" runat="server" title="Display Posted Purchase Report">
													Posted Purchases</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPOReturns" runat="server" title="Display Posted Purchase Return Report">
													Posted Returns</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedDebitMemo" runat="server" title="Display Posted Debit Memo Report">
													Posted Debit Memo</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseAnalysis" runat="server" title="Display Purchase Analysis Report">
													Purchase Analysis</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="126">
							</div>
						</td>
					</tr>
				</table>
				<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="180" alt="">
			</div>
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tbody>
					<tr>
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkPOAdd" runat="server" ToolTip="Add New Purchase Order">
								<li>
									New Purchase Order</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td colspan="2" id="ActionBar2" class="ms-pvtbt"></td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkInvoiceAdd" runat="server" ToolTip="Add New Invoice">
								<li>
									New Invoice</li></asp:HyperLink>
						</td>
					</tr>
				</tbody>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="">
		</td>
	</tr>
</table>
