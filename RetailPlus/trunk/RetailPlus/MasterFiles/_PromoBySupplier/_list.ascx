<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._PromoBySupplier.__List" Codebehind="_List.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Promo" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Add New Promo" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Promo" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Promo" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="Remove Selected Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Promo" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Promo" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Update Selected Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Promo" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator3" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idStuff" title="Stuff items in Promo" accessKey="S" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Stuff items in Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdStuff" title="Stuff items in Promo" accessKey="S" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdStuff_Click">Stuff Items</asp:linkbutton></td>
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
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="4%">
							<col width="15%">
							<col width="45%">
							<col width="15%">
							<col width="15%">
							<col width="5%">
							<col width="1%">
						</colgroup>
						<tr>
							<th class="ms-vh2" style="padding-bottom: 4px">
								<input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />&nbsp;&nbsp;&nbsp;&nbsp;</th >
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPromoBySupplierCode" runat="server">Promo Code</asp:hyperlink></th >
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPromoBySupplierName" runat="server">Promo Name</asp:hyperlink></th >
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByStartDate" runat="server">Start Date</asp:hyperlink></th >
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByEndDate" runat="server">End Date</asp:hyperlink></th >
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByStatus" runat="server">Status</asp:hyperlink></th >
							<th class="ms-vh2" style="padding-bottom: 4px">
							</th >
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="4%">
							<col width="15%">
							<col width="45%">
							<col width="15%">
							<col width="15%">
							<col width="5%">
							<col width="1%">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblPromoBySupplierCode" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblPromoBySupplierName" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblStartDate" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblEndDate" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblStatus" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</td>
						</tr>
						<tr>
							<td class="ms-vb-user">
							</td>
							<td class="ms-vb-user" colspan="5">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<table width="100%" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td class="ms-vb-user">
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
																<col width="20%">
																<col width="1">
															</colgroup>
															<tr>
																<th class="ms-vh2" style="padding-bottom: 4px">
																</th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink11" runat="server">Contact</asp:hyperlink></th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink4" runat="server">Group</asp:hyperlink></th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink5" runat="server">Sub Group</asp:hyperlink></th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink6" runat="server">Product</asp:hyperlink></th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink7" runat="server">Variation</asp:hyperlink></th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink9" runat="server">PromoBySupplier Value</asp:hyperlink></th >
                                                                <th class="ms-vh2" style="padding-bottom: 4px">
																	<asp:hyperlink id="Hyperlink1" runat="server">Remarks To Print</asp:hyperlink></th >
																<th class="ms-vh2" style="padding-bottom: 4px">
																</th >
															</tr>
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
																<col width="20%">
																<col width="1">
															</colgroup>
															<tr>
																<td class="ms-vb-user">
																	<input type="checkbox" id="chkListStuff" runat="server" name="chkListStuff" visible="false" />
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblContactName" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblProductGroup" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblProductSubGroup" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblProduct" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblVariation" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblQuantity" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblPromoBySupplierValue" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb-user">
																	<asp:Label ID="lblCouponRemarks" Runat="server"></asp:Label>
																</td>
																<td class="ms-vb2">
																	<a class="dropdown" id="a1" href="" runat="server">
																		<asp:image id="image1" imageurl="../../_layouts/images/dlmax.gif" runat="server" alt="show" visible="false"></asp:image></a>
																</td>
															</tr>
														</table>
													</ItemTemplate>
												</asp:datalist>
											</td>
										</tr>
									</table>
								</DIV>
							</td>
							<td class="ms-vb2">
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td id="AddUserTextTDID2">
			<table class="ms-toolbar" id="twotidGrpsTB" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgActivate" title="Activate Selected Promo" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Activate Selected Promo" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdActivate" title="Activate Selected Promo" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdActivate_Click">Activate Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDeactivate" title="DeActivate Selected Promo" accessKey="N" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="DeActivate Selected Promo" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDeactivate" title="DeActivate Selected Promo" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDeactivate_Click">DeActivate Selected Promo</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align03" nowrap="nowrap" align="right" width="99%">
					</td>
					<td class="ms-toolbar" id="align04" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
</table>
