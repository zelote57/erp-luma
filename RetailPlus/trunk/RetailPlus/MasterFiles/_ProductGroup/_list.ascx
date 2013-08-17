<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductGroup.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New Product Group" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Group" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New Product Group" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Product Group</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Product Group" accessKey="N" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Product Group" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product Group" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Product Group</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idEdit" ToolTip="Edit Selected Product Group" accessKey="N" tabIndex="5" height="16" width="16" border="0" alt="Edit Selected Product Group" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="idEdit_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product Group" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Product Group</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="lblSeparator3" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap style="WIDTH: 19px"><asp:imagebutton id="idCharges" title="Update Additional Charges" accessKey="N" tabIndex="5" height="16" width="16" border="0" alt="Update Additional Charges" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="idCharges_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCharges" title="Update Additional Charges" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdCharges_Click">Update Additional Charges</asp:linkbutton></td>
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
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colSpan="2"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
				</tr>
				<tr>
					<td colSpan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="2%">
							<col width="2%">
							<col width="2%">
							<col width="33%">
							<col width="33%">
							<col width="10%">
							<col width="15%">
							<col width="1%">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByProductGroupCode" runat="server">Product Group Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByProductGroupName" runat="server">Product Group Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByBaseUnitName" runat="server">Base Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPrice" runat="server">Selling Price</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="2%">
							<col width="2%">
							<col width="2%">
							<col width="33%">
							<col width="35%">
							<col width="10%">
							<col width="15%">
							<col width="1%">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</TD>
							<TD class="ms-vb2">
							    <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" height="16" width="16" border="0" tooltip="Delete group" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </TD>
							<TD class="ms-vb2">
							    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" height="16" width="16" border="0" tooltip="Update group" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </TD>
							<TD class="ms-vb2">
					            <asp:ImageButton id="imgVariations" runat="server" ImageUrl="../../_layouts/images/variations.gif" ToolTip="Show Variation Types" CommandName="imgVariationsClick" CausesValidation=false></asp:ImageButton></TD>
					        <TD class="ms-vb2">
					            <asp:ImageButton id="imgVariationsMatrix" runat="server" ImageUrl="../../_layouts/images/varmatrix.gif" ToolTip="Show Variations Matrix" CommandName="imgVariationsMatrixClick" CausesValidation=false></asp:ImageButton></TD>
					        <TD class="ms-vb2">
					            <asp:ImageButton id="imgUnitsMatrix" runat="server" ImageUrl="../../_layouts/images/unitmatrix.gif" ToolTip="Show Units Matrix" CommandName="imgUnitsMatrixClick" CausesValidation=false></asp:ImageButton></TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkProductGroupCode" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkProductGroupName" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkBaseUnitName" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblPrice" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="true"></asp:Image></A>
							</TD>
						</TR>
						<TR>
						    <TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
						    <TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
							<TD colSpan="4" height="1">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<asp:panel id="panCard" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
										<TABLE id="tblPanCard" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="ms-formspacer" colSpan="1"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label2" CssClass="ms-vh2" runat="server" text="<b>VAT</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblVAT" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
													<asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>eVAT</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label9" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:Label id="lblEVAT" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>Local Tax</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label11" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblLocalTax" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
													<asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b>Purchase Price</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label10" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:Label id="lblPurchasePrice" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
										</TABLE>
									</asp:panel></DIV>
							</TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
