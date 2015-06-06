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
					<tr id="divlblPurchasesAndPayables" runat="server">
						<td class="ms-navheader">Purchases &amp; Payables</td>
					</tr>
					<tr id="divtblPurchasesAndPayables" runat="server">
						<td>
							<div id="Menu_0_0">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
									<tr id="divlnkVendors" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkVendors" runat="server" title="Display List of Vendors">
													Vendors</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPurchaseOrders" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseOrders" runat="server" title="Display List of Purchase Orders" Visible="False">
													Purchase Orders</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr id="divlnkPurchaseReturns" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseReturns" runat="server" title="Display List of Purchase Returns">
													Purchase Returns</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPurchaseDebitMemo" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseDebitMemo" runat="server" title="Display List of Purchase Debit Memo">
													Purchase Debit Memo</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPostedPurchaseOrder" runat="server" style="display:none">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPurchaseOrder" runat="server" title="Display List of Posted Purchase Orders" Visible="false">
													Posted Purchase Order</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr id="divlnkPurchaseOrdereSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseOrdereSales" runat="server" title="Display List of Purchase Orders" Visible="False">
													ePurchase Orders</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr id="divlnkPurchaseReturnseSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseReturnseSales" runat="server" title="Display List of Purchase Orders" Visible="False">
													ePurchase Returns</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr id="divlnkPurchaseDebitMemoeSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseDebitMemoeSales" runat="server" title="Display List of Purchase Orders" Visible="False">
													ePurchase Debit Memo</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180" />
							</div>
						</td>
					</tr>
					<tr id="divlblFinancialReports" runat="server">
						<td class="ms-navheader">Financial Reports</td>
					</tr>
					<tr id="divtblFinancialReports" runat="server">
						<td>
							<div id="Menu_0_1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
									<tr id="divlnkPostedPO" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPO" runat="server" title="Display Posted Purchase Report">
													Posted Purchases</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPostedPOReturns" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPOReturns" runat="server" title="Display Posted Purchase Return Report">
													Posted Returns</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPostedDebitMemo" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedDebitMemo" runat="server" title="Display Posted Debit Memo Report">
													Posted Debit Memo</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPurchaseAnalysis" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseAnalysis" runat="server" title="Display Purchase Analysis Report">
													Purchase Analysis</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="126" />
							</div>
						</td>
					</tr>
                    <tr id="divlbleSalesReports" runat="server">
						<td class="ms-navheader">eSales Reports</td>
					</tr>
					<tr id="divtbleSalesReports" runat="server">
						<td>
							<div id="Div1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
									<tr id="divlnkPostedPOeSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPOeSales" runat="server" title="Display Posted Purchase Report">
													Posted Purchases</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPostedPOReturnseSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedPOReturnseSales" runat="server" title="Display Posted Purchase Return Report">
													Posted Returns</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPostedDebitMemoeSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPostedDebitMemoeSales" runat="server" title="Display Posted Debit Memo Report">
													Posted Debit Memo</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkPurchaseAnalysiseSales" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseAnalysiseSales" runat="server" title="Display Purchase Analysis Report">
													Purchase Analysis</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="126" />
							</div>
						</td>
					</tr>
				</table>
				<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="180" alt="" />
			</div>
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tbody>
					<tr id="divlblActionBar" runat="server">
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr id="divlnkPOAdd" runat="server">
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="width:170px;word-wrap:break-word">
							<asp:HyperLink id="lnkPOAdd" runat="server" ToolTip="Add New Purchase Order">
								<li>New Purchase Order</li></asp:HyperLink>
						</td>
					</tr>
					<tr style="display: none">
						<td colspan="2" id="ActionBar2" class="ms-pvtbt"></td>
					</tr>
					<tr style="display: none">
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkInvoiceAdd" runat="server" ToolTip="Add New Invoice" Visible="false">
								<li>New Invoice</li></asp:HyperLink>
						</td>
					</tr>
				</tbody>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="" />
		</td>
	</tr>
</table>
