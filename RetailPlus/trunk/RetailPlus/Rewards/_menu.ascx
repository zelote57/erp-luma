<%@ Reference Control="~/_Menu.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Rewards.__Menu" Codebehind="_Menu.ascx.cs" %>
<table height="100%" border="0" cellpadding="0" cellspacing="0" class="ms-navframe">
	<tr valign="top">
		<td id="onetidWatermark" class="ms-navwatermark" dir="ltr">
			<script language="C#"> 
				if (browseris.ie5up && document.all("navWatermark") && document.all("onetidWatermark")) { document.all("navWatermark").fillcolor=document.all("onetidWatermark").currentStyle.color; } </script>
		</td>
		<td width="170" style="PADDING-RIGHT: 2px">
			<div id="mnuInventory" runat="server">
				<table id="SPSWC_NavBar" width="100%" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" class="Ms-pvnav" DisplayType="v1">
					<tr>che
						<td class="ms-navheader">Rewards</td>
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
											<asp:HyperLink id="lnkMembers" runat="server" ToolTip="Display List of Members">
													Members</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="180">
							</div>
						</td>
					</tr>
					<tr>
						<td class="ms-navheader">Rewards Programs</td>
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
											<asp:HyperLink id="lnkRewardPrograms" runat="server" title="Display list of Reward Programs">
													 Reward Programs</asp:HyperLink>
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
				<TBODY>
					<tr>
						<td colspan="2" id="ActionBar1" class="ms-pvtbt">Actions</td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif"></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkCheckRewardPoints" runat="server" ToolTip="Easily check reward points">
								<li>Check Reward Points</li></asp:HyperLink>
						</td>
					</tr>
                    <tr>
						<td colspan="2" id="Td1" class="ms-pvtbt"></td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif"></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkRewardsReddem" runat="server" ToolTip="Reddem reward points" Visible="False">
								<li>Redeem Reward Points</li></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td colspan="2" id="ActionBar2" class="ms-pvtbt"></td>
					</tr>
					<tr>
						<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif"></td>
						<td class="ms-pvtbbutton" style="WIDTH:170px;WORD-WRAP:break-word">
							<asp:HyperLink id="lnkMemberAdd" runat="server" ToolTip="Add New Reward CardNo" Visible="False">
								<li>Add new member</li></asp:HyperLink>
						</td>
					</tr>
				</TBODY>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="">
		</td>
	</tr>
</table>
