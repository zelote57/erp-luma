<%@ Reference Control="~/inventory/_list.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._VariationsMatrix.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="/RetailPlus/_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="/RetailPlus/_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="/RetailPlus/_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New Product Variation" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="/RetailPlus/_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New Product Variation" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Product Variation" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Product Variation" ImageUrl="/RetailPlus/_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product Variation" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idEdit" ToolTip="Edit Selected Product Variation" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Product Variation" ImageUrl="/RetailPlus/_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product Variation" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="Label1" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idBack" title="Back To Products Inventory List" accessKey="B" tabIndex="5" height="16" width="16" border="0" alt="Back To Products Inventory List" ImageUrl="/RetailPlus/_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" title="Back To Products Inventory List" accessKey="B" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdBack_Click">Back To Products Inv. List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" noWrap align="right"><asp:label id="lblDataCount1" runat="server" CssClass="Normal"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" runat="server" CssClass="class=ms-vb-user"> of 0</asp:label></td>
							</TR>
						</TABLE>
					</TD>
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<COLGROUP>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</COLGROUP>
				<tr>
					<th class="ms-vh2">
						<IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td colSpan="4" height="5"><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="4%">
							<col width="35%" align="left">
							<col width="15%" align="left">
							<col width="15%" align="left">
							<col width="15%" align="left">
							<col width="15%" align="left">
							<col width="1%">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall">&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByVariation" runat="server">Product Variations Description</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="Hyperlink4" runat="server">Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="Hyperlink1" runat="server">Quantity</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="Hyperlink2" runat="server">Min. Threshold</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="Hyperlink3" runat="server">Max. Threshold</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblItemTemplate">
						<colgroup>
							<col width="4%">
							<col width="35%" align="left">
							<col width="15%" align="left">
							<col width="15%" align="left">
							<col width="15%" align="left">
							<col width="15%" align="left">
							<col width="1%">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" NAME="chkList">
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkVariation" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblUnitName" Runat="server"></asp:Label>
							<TD class="ms-vb-user">
								<asp:Label ID="lblQuantity" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblMinThreshold" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblMaxThreshold" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>