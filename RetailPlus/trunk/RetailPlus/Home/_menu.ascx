
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Home.__Menu" Codebehind="_Menu.ascx.cs" %>
<table height="100%" border="0" cellpadding="0" cellspacing="0" class="ms-navframe">
	<tr valign="top">
		<td id="onetidWatermark" class="ms-navwatermark" dir="ltr">
			<script language="C#"> 
				if (browseris.ie5up && document.all("navWatermark") && document.all("onetidWatermark")) { document.all("navWatermark").fillcolor=document.all("onetidWatermark").currentStyle.color; } </script>
		</td>
		<td width="150" style="PADDING-RIGHT: 2px">
			<div id="mnuMasterFiles" runat="server">
				<table id="SPSWC_NavBar" width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnav" DisplayType="v1">
					<tr id="divlblMyFavorites" runat="server">
						<td class="ms-navheader">My Favorites</td>
					</tr>
					<tr id="divtblMyFavorites" runat="server">
						<td>
							<div id="Menu_0_0" runat="server" visible="true">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
									<tr id="divlnkProducts" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkProducts" runat="server" title="Display List of Products">
													Products List</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkContact" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkContact" runat="server" title="Display List of Contacts">
													Contacts</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr id="divlnkRewards" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkRewards" runat="server" title="Display Customers with Rewards">
													Rewards</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkInventoryList" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkInventoryList" title="Display List of Inventory" runat="server">
													Inventory List</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr id="divlnkStock" runat="server">
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkStock" title="Display List of Stocked Transaction" runat="server" Width="140px">
													Stock Transactions</asp:HyperLink></td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
				</table>
			</div>
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tr id="divlblActionBar" runat="server">
					<td colspan="2" id="ActionBart" class="ms-pvtbt">Actions</td>
				</tr>
				<tr id="divlnkUpload" runat="server">
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkUpload" runat="server" title="Upload Stocks to Inventory">
							<li>Upload Stock Inventory</li></asp:HyperLink>
					</td>
				</tr>
                <tr id="divlnkGLAUploadSales" runat="server">
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkGLAUploadSales" runat="server" title="Upload Sales transactions from InfoGenisis">
							<li>Upload Mandarin Sales</li></asp:HyperLink>
					</td>
				</tr>
				<tr  id="divlnkSynchronize" runat="server">
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkSynchronize" runat="server" ToolTip="Synchronize Products of branches.">
							<li>Sync Branch Product</li></asp:HyperLink>
					</td>
				</tr>
				<tr id="divlnkProductAdd" runat="server">
					<td class="ms-pvtbicon"></td>
					<td class="ms-pvtbbutton" style="WIDTH: 170px; WORD-WRAP: break-word">
						<asp:HyperLink id="lnkProductAdd" ToolTip="Add New Product" runat="server">
							<li>
								New Product</li></asp:HyperLink></td>
				</tr>
				<tr id="divlnkAccessUserAdd" runat="server">
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkAccessUserAdd" ToolTip="Add New Access User" runat="server">
							<li>New Access User</li></asp:HyperLink>
					</td>
				</tr>
				<tr id="divlnkReceiptFormatEdit" runat="server">
					<td class="ms-pvtbicon"></td>
					<td class="ms-pvtbbutton" style="WIDTH: 170px; WORD-WRAP: break-word">
						<asp:HyperLink id="lnkReceiptFormatEdit" title="Update Receipt Format" runat="server">
							<li>
								Edit Receipt Format</li></asp:HyperLink></td>
				</tr>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="" />
		</td>
	</tr>
</table>
