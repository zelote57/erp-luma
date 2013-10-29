<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix.__List" Codebehind="_List.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Product Variation" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Product Variation" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Product Variation" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Product Variation" ImageUrl="../../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product Variation" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Product Variation" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Product Variation" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="idEdit_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product Variation" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Variation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="Label1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idBack" title="Back To Products List" accessKey="B" tabIndex="5" height="16" width="16" border="0" alt="Back To Products List" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="idBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" title="Back To Products List" accessKey="B" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdBack_Click">Back To Products List</asp:linkbutton></td>
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
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label>
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
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="4%">
							<col width="1">
							<col width="35%" align="left">
							<col width="20%" align="left">
							<col width="20%" align="left">
							<col width="20%" align="left">
							<col width="1%">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByVariation" runat="server">Product Variations Description</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByUnit" runat="server">Base Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPurchasePrice" runat="server">Purchase Price</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPrice" runat="server">Selling Price</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="4%">
							<col width="1">
							<col width="35%" align="left">
							<col width="20%" align="left">
							<col width="20%" align="left">
							<col width="20%" align="left">
							<col width="1%">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</td>
							<td class="ms-vb2">
								<asp:ImageButton id="imgPackage" runat="server" ImageUrl="../../../_layouts/images/tabpub.gif" alt="Show Product Package Matrix" CommandName="imgPackageMatrixClick"></asp:ImageButton></td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkVariation" Runat="server"></asp:HyperLink>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblUnitName" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblPurchasePrice" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblPrice" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</td>
						</tr>
						<tr>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
							<td colspan="4" height="1">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<asp:panel id="panCard" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
										<table id="tblPanCard" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="ms-formspacer" colspan="1"><img alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label2" CssClass="ms-vh2" runat="server" text="<b>VAT</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblVAT" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>eVAT</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label9" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblEVAT" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label3" CssClass="ms-vh2" runat="server" text="<b>Local Tax</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblLocalTax" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label10" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="Label11" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
										</table>
									</asp:panel></DIV>
							</td>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
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
