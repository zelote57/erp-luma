<%@ Reference Control="~/masterfiles/_product/_list.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._Group._Charges.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../../_Scripts/ConfirmDelete.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Group Charge" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Group Charge" ImageUrl="../../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Group Charge" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Group Charge</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Add New Group Charge" accessKey="N" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Group Charge" ImageUrl="../../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Group Charge" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Group Charge</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Add New Group Charge" accessKey="N" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Group Charge" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Group Charge" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Group Charge</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="Label1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idBack" title="Back To Product Groups List" accessKey="B" tabIndex="5" height="16" width="16" border="0" alt="Back To Product Groups List" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" title="Back To Product Groups List" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdBack_Click">Back To Group's List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align01" nowrap="nowrap" align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="right"><asp:label id="lblDataCount1" runat="server" CssClass="Normal"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" runat="server" CssClass="class=ms-vb-user"> of 0</asp:label></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
			<asp:Label id="lblGroupID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<COLGROUP>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</COLGROUP>
				<tr>
					<th class="ms-vh2">
						<img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="4%">
							<col width="65%" align="left">
							<col width="15%">
							<col width="15%">
							<col width="1%">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByChargeType" runat="server">Group Charge Type</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByChargeAmount" runat="server">Amount</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByInPercent" runat="server">In Percent</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="4%">
							<col width="65%" align="left">
							<col width="15%">
							<col width="15%">
							<col width="1%">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblChargeType" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblChargeAmount" Runat="server" ReadOnly="True"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<input type="checkbox" id="chkInPercent" runat="server" NAME="chkInPercent" disabled>
							</td>
							<td class="ms-vb2">
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
