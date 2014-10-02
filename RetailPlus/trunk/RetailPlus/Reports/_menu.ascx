<%--<%@ Reference Control="~/_Menu.ascx" %>--%>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__Menu" Codebehind="_Menu.ascx.cs" %>
<table height="100%" border="0" cellpadding="0" cellspacing="0" class="ms-navframe">
	<tr valign="top">
		<td id="onetidWatermark" class="ms-navwatermark" dir="ltr">
			<script language="C#"> 
				if (browseris.ie5up && document.all("navWatermark") && document.all("onetidWatermark")) { document.all("navWatermark").fillcolor=document.all("onetidWatermark").currentStyle.color; } </script>
		</td>
		<td width="150" style="PADDING-RIGHT: 2px">
			<div id="mnuMasterFiles" runat="server">
				<table id="SPSWC_NavBar" width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnav" DisplayType="v1">
					<tr>
						<td class="ms-navheader">Common Reports</td>
					</tr>
					<tr>
						<td>
							<div id="Menu_0_0">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="../_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="../_layouts/images/trans.gif" alt="" /></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkProducts" runat="server" title="Display Product List Report">
													Products</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkProductHistory" runat="server" title="Display Product History Report">
													Product History</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1" style="HEIGHT: 18px"></td>
										<td class="Ms-pvtbbutton" style="HEIGHT: 18px">
											<asp:HyperLink id="lnkInventory" runat="server" title="Display Inventory List Report">
													Inventory</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1" style="HEIGHT: 18px"></td>
									</tr>
									<%--<tr>
										<td class="Ms-pvnavmidl1" style="HEIGHT: 18px"></td>
										<td class="Ms-pvtbbutton" style="HEIGHT: 18px">
											<asp:HyperLink id="lnkExpiredInventory" runat="server" title="Display Expired Inventory List Report">
													Expired Inventory</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1" style="HEIGHT: 18px"></td>
									</tr>--%>
									<%--<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkProductPriceHistory" runat="server" title="Display Product Price History Report">
													Product Price History</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>--%>
									<%--<tr>
										<td class="Ms-pvnavmidl1" style="HEIGHT: 18px"></td>
										<td class="Ms-pvtbbutton" style="HEIGHT: 18px">
											<asp:HyperLink id="lnkReorder" title="Display Items For Re-Order" runat="server">
													Items For Re-Order</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1" style="HEIGHT: 18px"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkOverStock" title="Display Over Stock Items List Report" runat="server">
													Over Stock Items</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>--%>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="HyperLink3" title="Display Reports of Items returned" runat="server" Visible="False">
													Returned Items</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkReturnedItemsReport" title="Display Reports of items VOID" runat="server" Visible="False">
													Void Items</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<%--<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkMostSalableItems" runat="server" title="Display Most Salable Items Report">
													Most Salable Items</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkLeastSalableItems" title="Display Least Salable Items Report" runat="server">
													Least Salable Items</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>--%>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkStockTransaction" title="Display Stock Transaction Item Report" runat="server" Visible="False">
													Stock Transaction</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavbotl1" background="../_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavbotc1"></td>
										<td class="Ms-pvnavbotr1" background="../_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkContacts" runat="server" title="Display Contact List Report">
													Contacts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkLoginLogoutReport" runat="server" title="Display Login-Logout Report" Visible="False">
													Login-Logout Report</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">Retail (POS) Reports</td>
					</tr>
					<tr> 
					    <td nowrap>
							<div id="Div1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="../_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="../_layouts/images/trans.gif" alt=""></td>
									</tr>
									
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkTransaction" title="Display Sales Transaction Report" runat="server">
													per Transaction</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton" >
											<asp:HyperLink id="lnkDatedReport" runat="server" title="Display Reports with selected dates" Visible="False">
													Sales Reports</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkManagementReport" runat="server" title="Display Management Reports" Visible="False">
													Management Reports</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAnalyticsReport" runat="server" title="Display Analytical Reports" Visible="False">
													Analytics Reports</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton" >
											<asp:HyperLink id="lnkeSalesReport" runat="server" title="Display eSales Reports for Gov" Visible="False">
													eSales Reports</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<%--<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkSalesPerDay" runat="server" title="Display Sales per Day Report" Visible="False">
													Sales per Day</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkSalesPerHour" runat="server" title="Display Sales per Hour Report" Visible="False">
													Sales per Hour</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>--%>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkTerminalReports" runat="server" title="Display Current Terminal Report" Visible="False">
													Terminal Report</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkDiscountReports" title="Display Reports with discounts" runat="server" Visible="False">
													Discounts Reports</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkElectroniJournal" title="Display Electronic Journal" runat="server" Visible="False">
													Electronic Journal</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="../_layouts/images/trans.gif" width="126">
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">Financial Reports</td>
					</tr>
					<tr>
						<td nowrap>
							<div id="Menu_0_1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="../_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="../_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAgentsSales" title="Extract Agents Sales Report" runat="server">
													Agents Sales</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAgentsCommision" title="Extract Agents Commision Report" runat="server">
													Agents Commision</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCustomerCredit" title="Display Customer Credit Report" runat="server">
													Customer Credit</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<%--<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCustomersWithCreditReport" title="Display Customer Credit Report" runat="server">
													Customers W/ Credit</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>--%>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkPurchaseAnalysis" runat="server" title="Display Purchase Analysis Report">
													Purchase Analysis</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="../_layouts/images/trans.gif" width="126">
							</div>
						</td>
					</tr>
				</table>
				<img src="../_layouts/images/trans.gif" height="1" width="126" alt="">
			</div>
		</td>
	</tr>
</table>
