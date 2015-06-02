<%--<%@ Reference Control="~/_Menu.ascx" %>--%>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__Menu" Codebehind="_Menu.ascx.cs" %>
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
						<td class="ms-navheader">Inventory</td>
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
											<asp:HyperLink id="lnkBranch" runat="server" ToolTip="Display List of Branches">
													Branches</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton" nowrap="nowrap">
											<asp:HyperLink id="lnkBranchTransfer" runat="server" ToolTip="Display List of Branch Transfers">
													Branch Inventory Transfer</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton" nowrap="nowrap">
											<asp:HyperLink id="lnkWarehouseTransfer" runat="server" ToolTip="Display List of Warehouse-Branch Transfers">
													Warehouse->Branch Transfer</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkInventoryList" runat="server" ToolTip="Display List of Inventory">
													Inventory List</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkStockType" runat="server" ToolTip="Display List of Stocked Type">
													Stock Type</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkStock" runat="server" ToolTip="Display List of Stocked Transaction">
													Stock Transactions</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkTransferIn" runat="server" ToolTip="Display List of Transfer In">
													Transfer In</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkTransferOut" runat="server" ToolTip="Display List of Transfer Out">
													Transfer Out</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkInvAdjustment" runat="server" ToolTip="Display List of Inventory Adjustment">
													Inv Adjustment</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkInvThreshold" runat="server" ToolTip="Set inventory threshold">
													Set Inventory Threshold</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
				</table>
			</div>
			<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tbody>
					<tr>
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkInventoryAnalyst" runat="server" ToolTip="Inventory Analyst">
								<li>Inventory Analyst</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkCloseInventory" runat="server" ToolTip="Close Inventory">
								<li>Close Inventory By Group</li></asp:HyperLink>
						</td>
					</tr>
                    <tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkCloseInventoryProduct" runat="server" ToolTip="Close Inventory">
								<li>Close Inventory By Product</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word" nowrap="nowrap">
							<asp:HyperLink id="lnkCloseInventoryDetailed" runat="server" ToolTip="Close Inventory using detailed variation">
								<li>
									Close Inventory By Supplier</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkUpload" runat="server" ToolTip="Upload Stocks to Inventory">
								<li>
									Upload Stock Inventory</li></asp:HyperLink>
						</td>
					</tr>
					<tr><td colspan="2" class="Ms-pvtbt"></td></tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkSynchronize" runat="server" ToolTip="Synchronize Inventory Count">
								<li>Synchronize Inv Count</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkExport" runat="server" ToolTip="Export Branch Inventory Count">
								<li>Export Inventory Count</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkImport" runat="server" ToolTip="Import Branch Inventory Count">
								<li>Import Inventory Count</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td colspan="2" class="Ms-pvtbt"></td></tr>
					<tr>
						<td colspan="2" id="ActionBar2" class="ms-pvtbt">Reports</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkInventory" runat="server" ToolTip="Display Inventory List Report">
								<li>
									Inventory</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkBranchInventory" runat="server" ToolTip="Display Inventory List Report per Branch">
								<li>
									Inventory per Branch</li></asp:HyperLink>
						</td>
					</tr>
					<%--<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkExpiredInventory" runat="server" ToolTip="Display Expired Inventory List Report">
								<li>
									Expired Inventory</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkReorder" ToolTip="Display Items For Re-Order" runat="server"><li>
													Items For Re-Order</asp:HyperLink></td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkOverStock" ToolTip="Display Over Stock Items List Report" runat="server"><li>
													Over Stock Items</asp:HyperLink></td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkTotalStock" runat="server" ToolTip="Display Inventory List of Stock">
								<li>Total Stock</li></asp:HyperLink>
						</td>
					</tr>--%>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkCLosingInventoryReport" runat="server" ToolTip="Display Closing Inventory Reports">
								<li>Closing Inventory Report</li></asp:HyperLink>
						</td>
					</tr>
				</tbody>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="">
		</td>
	</tr>
</table>
