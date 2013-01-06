<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New Product" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/newuser.gif" alt="Add New Product" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New Product" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Product" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/delitem.gif" alt="Remove Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idEdit" ToolTip="Edit Selected Product" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/edit.gif" alt="Update Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" noWrap align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
							</TR>
						</TABLE>
					</TD>
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<%--<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../_layouts/images/blank.gif" width="1"></th></tr>
				<tr>
					<td colSpan="4" height="5"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>--%>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" AlternatingItemStyle-CssClass="ms-alternating" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="2%">
							<col width="15%">
							<col width="19%">
							<col width="17%">
							<col width="14%">
							<col width="14%">
							<col width="15%">
							<col width="1%">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall">&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByBarcode" runat="server">Barcode</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;
								<asp:hyperlink id="SortByGroupName" runat="server">Group / Sub-group</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByUnit" runat="server">Total Quantity</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="Hyperlink1" runat="server">Min. Threshold</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="Hyperlink2" runat="server">Max. Threshold</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<COLGROUP>
							<col width="2%">
							<col width="2%">
							<col width="15%">
							<col width="19%">
							<col width="18%">
							<col width="15%">
							<col width="14%">
							<col width="15%">
							<col width="1%">
						</COLGROUP>
						<TR>
							<TD class="ms-vb-user">
								<INPUT id="chkList" type="checkbox" name="chkList" runat="server"></TD>
							<TD class="ms-vb2">
								<asp:ImageButton id="imgVariationsMatrix" runat="server" ImageUrl="../_layouts/images/tabpub.gif" alt="Show Product Variations Matrix" CommandName="imgVariationsMatrixClick"></asp:ImageButton></TD>
							<TD class="ms-vb-user">
								&nbsp;&nbsp;
								<asp:HyperLink id="lnkBarcode" Runat="server"></asp:HyperLink></TD>
							<TD class="ms-vb-user">
								<asp:HyperLink id="lnkProductCode" Runat="server"></asp:HyperLink>
							<TD class="ms-vb2">
								<asp:Label id="lblGroup" Runat="server"></asp:Label></TD>
							<TD class="ms-vb2">
								<asp:Label id="lblQuantity" Runat="server"></asp:Label></TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblMinThreshold" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblMaxThreshold" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
							</TD>
						</TR>
						<tr class="ms-vb2">
						    <td colspan=2></td>
						    <td colspan="6">
						        <asp:datalist id="lstBranchInventory" runat="server" Width="80%" ShowFooter="False" CellPadding="0" CssClass="ms-styleheader" OnItemDataBound="lstBranchInventory_ItemDataBound" Visible=false>
				                    <HeaderTemplate>
					                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate" class="">
						                    <colgroup>
							                    <col width="2%">
							                    <col width="70%">
							                    <col width="28%">
						                    </colgroup>
						                    <TR>
							                    <TH class="ms-vh2" style="padding-top: 1px"></TH>
							                    <TH class="ms-vh2" style="padding-top: 1px">
								                    <asp:hyperlink id="SortByBranchCode" runat="server">Branch</asp:hyperlink></TH>
							                    <TH class="ms-vh2" style="padding-top: 1px">
								                    <asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></TH>
						                    </TR>
					                    </table>
				                    </HeaderTemplate>
				                    <ItemTemplate>
					                    <TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						                    <COLGROUP>
							                    <col width="2%">
							                    <col width="70%">
							                    <col width="28%">
						                    </COLGROUP>
						                    <TR>
							                    <TD class="ms-vb-user">
								                    <INPUT id="chkList" type="checkbox" name="chkList" runat="server" visible=false></TD>
							                    <TD class="ms-vb-user">
								                    &nbsp;&nbsp;
								                    <asp:HyperLink id="lnkBranchCode" Runat="server"></asp:HyperLink></TD>
							                    <TD class="ms-vb2">
								                    <asp:Label id="lblQuantity" Runat="server"></asp:Label></TD>
						                    </TR>
					                    </TABLE>
				                    </ItemTemplate>
			                    </asp:datalist>
						    </td>
						</tr>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</TD>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
