<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Product" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Add New Product" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Product" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Product" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="Remove Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Product" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Update Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator3" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idCompose" title="Compose Selected Product" accessKey="C" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Compose Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCompose" title="Compose Selected Product" accessKey="C" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdCompose_Click">Compose Selected Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idFinance" title="Setup Financial Information" accessKey="C" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/tabfinance.gif" alt="Setup Financial Information" border="0" width="16" height="16" OnClick="idFinance_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdFinance" title="Setup Financial Information" accessKey="C" tabIndex="6" CssClass="ms-toolbar" runat="server" OnClick="cmdFinance_Click">Setup Financial Info</asp:linkbutton></td>
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
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th></tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" AlternatingItemStyle-CssClass="ms-alternating">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="1">
							<col width="1">
							<col width="1">
							<col width="1">
							<col width="22%">
							<col width="20%">
							<col width="24%">
							<col width="22%">
							<col width="12%">
							<col width="1">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByBarCode" runat="server">Bar Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByDescription" runat="server">Description</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;
								<asp:hyperlink id="SortByGroupName" runat="server">Product Group</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:hyperlink id="SortByUnit" runat="server">Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<COLGROUP>
							<col width="10">
							<col width="1">
							<col width="1">
							<col width="1">
							<col width="1">
							<col width="22%">
							<col width="20%">
							<col width="26%">
							<col width="22%">
							<col width="10%">
							<col width="1">
						</COLGROUP>
						<tr>
							<td class="ms-vb-user">
								<input id="chkList" type="checkbox" name="chkList" runat="server" /></td>
							<td class="ms-vb2">
								<asp:ImageButton id="imgVariations" runat="server" ImageUrl="../../_layouts/images/tabpub.gif" alt="Setup Product Variation Types" CommandName="imgVariationsClick"></asp:ImageButton></td>
							<td class="ms-vb2">
								<asp:ImageButton id="imgVariationsMatrix" runat="server" ImageUrl="../../_layouts/images/tabpub.gif" alt="Setup Product Variations Matrix" CommandName="imgVariationsMatrixClick"></asp:ImageButton></td>
							<td class="ms-vb2">
								<asp:ImageButton id="imgUnitsMatrix" runat="server" ImageUrl="../../_layouts/images/tabpub.gif" alt="Setup Product Units Matrix" CommandName="imgUnitsMatrixClick"></asp:ImageButton></td>
							<td class="ms-vb2">
								<asp:ImageButton id="imgPackage" runat="server" ImageUrl="../../_layouts/images/tabpub.gif" alt="Setup Product Package Matrix" CommandName="imgPackageMatrixClick"></asp:ImageButton></td>
							<td class="ms-vb-user">
								&nbsp;&nbsp;
								<asp:Label id="lnkProductCode" Runat="server"></asp:Label>
							<td class="ms-vb-user">
								<asp:Label id="lnkBarCode" Runat="server"></asp:Label>
							<td class="ms-vb-user">
								<asp:HyperLink id="lnkDescription" Runat="server"></asp:HyperLink></td>
							<td class="ms-vb2">
								<asp:Label id="lnkGroup" Runat="server"></asp:Label></td>
							<td class="ms-vb2">
								<asp:Label id="lnkUnit" Runat="server"></asp:Label></td>
							<td class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</td>
						</tr>
						<tr>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
							<td colspan="5" height="1">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<asp:panel id="panCard" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
										<table id="tblPanCard" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="ms-formspacer" colspan="1"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
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
													<asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>Local Tax</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label11" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblLocalTax" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label13" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label14" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="Label15" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label1" CssClass="ms-vh2" runat="server" text="<b>Selling Price</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label3" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label ID="lblPrice" CssClass="ms-vb2" Runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label6" CssClass="ms-vh2" runat="server" text="<b>Purchase Price</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblPurchasePrice" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
										</table>
									</asp:panel></DIV>
							</td>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
