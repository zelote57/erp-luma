<%--<%@ Reference Control="~/_Menu.ascx" %>--%>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles.__Menu" Codebehind="_Menu.ascx.cs" %>
<table height="100%" border="0" cellpadding="0" cellspacing="0" class="ms-navframe">
	<tr valign="top">
		<td id="onetidWatermark" class="ms-navwatermark" dir="ltr">
			<script language="C#"> 
				if (browseris.ie5up && document.all("navWatermark") && document.all("onetidWatermark")) { document.all("navWatermark").fillcolor=document.all("onetidWatermark").currentStyle.color; } </script>
		</td>
		<td width="170" style="PADDING-RIGHT: 2px">
			<div id="mnuMasterFiles" runat="server">
				<table id="SPSWC_NavBar" width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnav" DisplayType="v1">
                    <div id="Menu_Product" runat="server">
					<tr>
						<td class="ms-navheader">Product Setup</td>
					</tr>
					<tr>
						<td>
							<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
                                <tr>
									<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									<td class="Ms-pvnavtopc1"></td>
									<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
								</tr>
								<tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkProductsDetailed" runat="server" title="Display List of Products">
												Products Detailed View</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>
								<tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkProducts" runat="server" title="Display List of Products">
												Products Compack View</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>
                                <tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkProductGroup" runat="server" title="Display List of Product Groups" Width="140px">
												Product Groups</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>
								<tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkProductSubGroup" runat="server" title="Display List of Product Sub Group" Width="164px">
												Product Sub Group</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>
                                <tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkPromo" runat="server" title="Display List of Promotinal Plans">
												Products in Promo</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>	
								<tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkVariation" runat="server" title="Display List of Variation" Width="134px">
												Variation</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>
								<tr>
									<td class="Ms-pvnavmidl1"></td>
									<td class="Ms-pvtbbutton">
										<asp:HyperLink id="lnkUnit" runat="server" title="Display List of Units">
												Units</asp:HyperLink>
									</td>
									<td class="Ms-pvnavmidr1"></td>
								</tr>							
							</table>
							<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
						</td>
					</tr>
                    </div>
                    <tr>
						<td class="ms-navheader">Contact Setup</td>
					</tr>
                    <tr>
						<td>
							<div id="Menu_0_1">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
                                    <tr>
										<td class="Ms-pvnavbotl1" background="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavbotc1"></td>
										<td class="Ms-pvnavbotr1" background="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkContactGroup" runat="server" title="Display List of Contact Groups">
													Contact Groups<br /> &nbsp;&nbsp;*Suppliers, Customers, etc</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkContact" runat="server" title="Display List of Contacts">
													Contacts List</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkContactDetailed" runat="server" ToolTip="Display List of Contacts Detailed">
													Customers Detailed Information</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
                                    <tr>
                                        <td class="Ms-pvnavmidl1">
                                        </td>
                                        <td class="Ms-pvtbbutton">
                                            <asp:HyperLink ID="lnkPositions" runat="server" title="Display List of Positions">
													Positions</asp:HyperLink></td>
                                        <td class="Ms-pvnavmidr1">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Ms-pvnavmidl1">
                                        </td>
                                        <td class="Ms-pvtbbutton">
                                            <asp:HyperLink ID="lnkDepartments" runat="server" title="Display List of Departments">
													Departments</asp:HyperLink></td>
                                        <td class="Ms-pvnavmidr1">
                                        </td>
                                    </tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
							</div>
						</td>
					</tr>
                    <tr>
						<td class="ms-navheader">Miscellaneous Setup</td>
					</tr>
                    <tr>
						<td>
							<div id="Div2">
								<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnavtableone1">
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
                                    <tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkDiscount" runat="server" title="Display List of Discounts">
													Discount Types</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkCardType" runat="server" title="Display List of Card Types" Width="134px">
													Credit Cards</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
										<td class="Ms-pvnavtopc1"></td>
										<td class="Ms-pvnavtopr1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkChargeType" runat="server" title="Display List of Charge Types" Width="134px">
													Additional Charge Types</asp:HyperLink>
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
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tbody>
					<tr>
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkSynchronize" runat="server" ToolTip="Synchronize Products of branches.">
								<li>Sync Branch Product</li></asp:HyperLink>
						</td>
					</tr>
                    <tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkAddvariationType" runat="server" title="Add Product Variation">
								<li>Add Product Variation</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkChangeRewardPoints" runat="server" title="Change Products Reward Points">
								<li>Product Reward Points</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkChangeProductPrice" runat="server" title="Change products Price">
								<li>Change Product Price</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkChangeTax" runat="server" title="Change bulk products VAT, EVAT, Local Tax">
								<li>Change VAT, EVAT, LocalTax</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td colspan="2" class="Ms-pvnavtopl1"><img src="/RetailPlus/_layouts/images/trans.gif" alt="" /></td>
					</tr>
				</tbody>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="">
		</td>
	</tr>
</table>
