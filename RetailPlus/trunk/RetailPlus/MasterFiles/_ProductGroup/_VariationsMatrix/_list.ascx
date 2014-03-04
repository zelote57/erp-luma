<%@ Reference Control="~/masterfiles/_productgroup/_list.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductGroup._VariationsMatrix.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../../_Scripts/ConfirmDelete.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Product Variation" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/newuser.gif" alt="Add New Product Variation" border="0" width="16" height="16" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Product Variation" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Add New Product Variation" accessKey="N" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/delitem.gif" alt="Remove Selected Product Variation" border="0" width="16" height="16" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product Variation" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Add New Product Variation" accessKey="N" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/edit.gif" alt="Update Selected Product Variation" border="0" width="16" height="16" OnClick="idEdit_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product Variation" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idBack" title="Back To Product Groups List" accessKey="B" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/back.gif" alt="Back To Product Groups List" border="0" width="16" height="16" OnClick="idBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" title="Back To Product Groups List" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdBack_Click">Back To Group's List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align01" nowrap="nowrap" align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:label id="lblGroupID" runat="server" Visible="False"></asp:label></td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
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
						<img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="30%">
							<col width="19%">
							<col width="10%">
							<col width="10%">
							<col width="10%">
							<col width="10%">
							<col width="10%">
							<col width="1%">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByVariation" runat="server">Product Variations Description</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByUnit" runat="server">Base Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPurchasePrice" runat="server">Purchase Price</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPrice" runat="server">Selling Price</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByVAT" runat="server">VAT</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByEVAT" runat="server">eVAT</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByLocalTax" runat="server">Local Tax</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="30%">
							<col width="19%">
							<col width="10%">
							<col width="10%">
							<col width="10%">
							<col width="10%">
							<col width="10%">
							<col width="1%">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</td>
							<td class="ms-vb2">
							    <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" height="16" width="16" border="0" tooltip="Delete this record" ImageUrl="../../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </td>
							<td class="ms-vb2">
							    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" height="16" width="16" border="0" tooltip="Update this record" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkVariation" Runat="server"></asp:HyperLink>
							</td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkUnitName" Runat="server"></asp:HyperLink>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblPurchasePrice" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblPrice" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblVAT" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblEVAT" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblLocalTax" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
