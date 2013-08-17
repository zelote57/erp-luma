<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Promo.__List" Codebehind="_List.ascx.cs" %>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New Promo" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Add New Promo" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New Promo" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Promo" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="Remove Selected Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Promo" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idEdit" ToolTip="Edit Selected Promo" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Update Selected Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Promo" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator3" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idStuff" title="Stuff items in Promo" accessKey="S" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Stuff items in Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdStuff" title="Stuff items in Promo" accessKey="S" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdStuff_Click">Stuff Items</asp:linkbutton></td>
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
					<td colSpan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="4%">
							<col width="15%">
							<col width="20%">
							<col width="25%">
							<col width="15%">
							<col width="15%">
							<col width="5%">
							<col width="1%">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPromoCode" runat="server">Promo Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPromoName" runat="server">Promo Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPromoType" runat="server">Promo Type</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByStartDate" runat="server">Start Date</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByEndDate" runat="server">End Date</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByStatus" runat="server">Status</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="4%">
							<col width="15%">
							<col width="20%">
							<col width="25%">
							<col width="15%">
							<col width="15%">
							<col width="5%">
							<col width="1%">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblPromoCode" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblPromoName" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblPromoType" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblStartDate" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblEndDate" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblStatus" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</TD>
						</TR>
						<TR>
							<TD class="ms-vb-user">
							</TD>
							<TD class="ms-vb-user" colspan="6">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<table width="100%" cellpadding="0" cellspacing="0" border="0">
										<TR>
											<TD class="ms-vb-user">
												<asp:datalist id="lstStuff" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
													<HeaderTemplate>
														<table width="100%" cellpadding="0" cellspacing="0" border="0">
															<colgroup>
																<col width="10">
																<col width="15%">
																<col width="10%">
																<col width="15%">
																<col width="15%">
																<col width="15%">
																<col width="10%" align="right">
																<col width="10%" align="right">
																<col width="10%" align="right">
																<col width="1">
															</colgroup>
															<TR>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																</TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink11" runat="server">Contact</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink4" runat="server">Group</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink5" runat="server">Sub Group</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink6" runat="server">Product</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink7" runat="server">Variation</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:hyperlink id="Hyperlink8" runat="server">Quantity</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink9" runat="server">Promo Value</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:hyperlink id="Hyperlink10" runat="server">In Percent</asp:hyperlink></TH>
																<TH class="ms-vh2" style="padding-bottom: 4px">
																</TH>
															</TR>
														</table>
													</HeaderTemplate>
													<ItemTemplate>
														<table width="100%" cellpadding="0" cellspacing="0" border="0">
															<colgroup>
																<col width="10">
																<col width="15%">
																<col width="10%">
																<col width="15%">
																<col width="15%">
																<col width="15%">
																<col width="10%" align="right">
																<col width="10%" align="right">
																<col width="10%" align="right">
																<col width="1">
															</colgroup>
															<TR>
																<TD class="ms-vb-user">
																	<input type="checkbox" id="chkListStuff" runat="server" NAME="chkListStuff" visible="false">
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblContactName" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblProductGroup" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblProductSubGroup" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblProduct" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblVariation" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblQuantity" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:Label ID="lblPromoValue" Runat="server"></asp:Label>
																</TD>
																<TD class="ms-vb-user">
																	<asp:CheckBox id="chkInPercent" Runat="server" Enabled="False"></asp:CheckBox>
																</TD>
																<TD class="ms-vb2">
																	<A class="DropDown" id="A1" href="" runat="server">
																		<asp:Image id="Image1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
																</TD>
															</TR>
														</table>
													</ItemTemplate>
												</asp:datalist>
											</TD>
										</TR>
									</table>
								</DIV>
							</TD>
							<TD class="ms-vb2">
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD id="AddUserTextTDID2">
			<table class="ms-toolbar" id="twotidGrpsTB" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgActivate" title="Activate Selected Promo" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Activate Selected Promo" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdActivate" title="Activate Selected Promo" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdActivate_Click">Activate Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDeactivate" title="DeActivate Selected Promo" accessKey="N" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="DeActivate Selected Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDeactivate" title="DeActivate Selected Promo" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDeactivate_Click">DeActivate Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" id="align03" noWrap align="right" width="99%">
					</TD>
					<td class="ms-toolbar" id="align04" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
</table>
