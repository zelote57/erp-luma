<%--<%@ Reference Control="~/_Menu.ascx" %>--%>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security.__Menu" Codebehind="_Menu.ascx.cs" %>
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
						<td class="ms-navheader">Administration Files</td>
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
											<asp:HyperLink id="lnkCompany" runat="server" title="Display Company Information">
													Company Info.</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkTerminal" runat="server" title="Display List of Terminal Clients">
													Terminal Client</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAccessGroup" runat="server" title="Display List of Access Groups">
													Access Groups</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkAccessUser" runat="server" title="Display List of Access Users">
													Access Users</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavmidl1"></td>
										<td class="Ms-pvtbbutton">
											<asp:HyperLink id="lnkReceiptFormat" runat="server" title="Display Receipt Format">
													Receipt Format</asp:HyperLink>
										</td>
										<td class="Ms-pvnavmidr1"></td>
									</tr>
									<tr>
										<td class="Ms-pvnavbotl1" background="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
										<td class="Ms-pvnavbotc1"></td>
										<td class="Ms-pvnavbotr1" background="/RetailPlus/_layouts/images/trans.gif" alt=""></td>
									</tr>
								</table>
								<IMG height="1" alt="" src="/RetailPlus/_layouts/images/trans.gif" width="126">
							</div>
						</td>
					</tr>
				</table>
				<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="126" alt="">
			</div>
			<table id="ActionBar" class="ms-pvtb" width="100%" cellspacing="0" cellpadding="0">
				<tr>
					<td colspan="2" id="ActionBart" class="ms-pvtbt">Actions</td>
				</tr>
				<tr>
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:150px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkAccessGroupAdd" runat="server" ToolTip="Add New Access Group">
							<li>
								New Access Group</li></asp:HyperLink>
					</td>
				</tr>
				<tr>
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:150px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkAccessUserAdd" runat="server" ToolTip="Add New Access User">
							<li>
								New Access User</li></asp:HyperLink>
					</td>
				</tr>
				<tr>
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:150px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkReceiptFormatEdit" runat="server" title="Update Receipt Format">
							<li>
								Edit Receipt Format</li></asp:HyperLink>
					</td>
				</tr>
				<tr>
					<td width="0" class="ms-pvtbicon"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" /></td>
					<td class="ms-pvtbbutton" style="WIDTH:150px;WORD-WRAP:break-word">
						<asp:HyperLink id="lnkRewardPointSystem" runat="server" title="Update Reward Point System">
							<li>Update Rewards Point System</li></asp:HyperLink>
					</td>
				</tr>
			</table>
			<img src="/RetailPlus/_layouts/images/trans.gif" height="1" width="150" alt="">
		</td>
	</tr>
</table>
