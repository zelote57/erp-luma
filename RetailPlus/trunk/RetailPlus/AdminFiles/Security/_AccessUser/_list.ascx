<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security._AccessUser.__List" Codebehind="_List.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Access User" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Access User" ImageUrl="../../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Access User" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Access User</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Access User" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Access User" ImageUrl="../../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Access User" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Access User</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" style="width: 19px"><asp:imagebutton id="imgEdit" title="Update Selected Access User" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Access User" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="imgEdit_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" title="Update Selected Access User" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Access User</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator3" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAccessRightsUpdate" title="Update Access Rights" accessKey="U" tabIndex="5" height="16" width="16" border="0" alt="Update Access Rights" ImageUrl="../../../_layouts/images/tabsec.gif" runat="server" CssClass="ms-toolbar" OnClick="imgAccessRightsUpdate_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAccessRightsUpdate" title="Update Access Rights" accessKey="U" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdAccessRightsUpdate_Click">Update Access Rights</asp:linkbutton></td>
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
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col>
							<col>
							<col>
							<col>
							<col>
							<col>
							<col width="15%">
							<col width="15%">
							<col width="15%">
							<col width="20%">
							<col width="20%">
							<col width="14%">
							<col width="1%">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" value="on"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByUserName" runat="server">User Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByPassword" runat="server">Password</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByName" runat="server">Full Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAddress1" runat="server">Address1</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByEmailAddress" runat="server">Email Address</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByGroupName" runat="server">Group Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<COLGROUP>
							<col>
							<col>
							<col>
							<col>
							<col>
                            <col>
							<col>
							<col width="15%">
							<col width="15%">
							<col width="15%">
							<col width="20%">
							<col width="20%">
							<col width="14%">
							<COL align="center" width="1%">
						</COLGROUP>
						<tr>
							<td class="ms-vb-user">
								<input id="chkList" type="checkbox" name="chkList" runat="server" />
							</td>
							<td class="ms-vb2">
							    <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" accessKey="D" tabIndex="1" height="16" width="16" border="0" tooltip="Delete this user" ImageUrl="../../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </td>
							<td class="ms-vb2">
							    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this user" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </td>
						    <td class="ms-vb2">
							    <asp:imagebutton id="imgItemAccessRights" CommandName="imgItemAccessRights" accessKey="A" tabIndex="1" height="16" width="16" border="0" tooltip="Update access right of this group" ImageUrl="../../../_layouts/images/tabsec.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
						    </td>
                            <td class="ms-vb2">
							    <asp:imagebutton id="imgResetPassword" CommandName="imgResetPassword" accessKey="R" tabIndex="1" height="16" width="16" border="0" tooltip="Reset password" ImageUrl="../../../_layouts/images/resetp.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
						    </td>
						    <td class="ms-vb2">
							    <asp:imagebutton id="imgReloadAccessRights" CommandName="imgReloadAccessRights" accessKey="R" tabIndex="1" height="16" width="16" border="0" tooltip="Reload access rights from group" ImageUrl="../../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
						    </td>
						    <td class="ms-vb2">
							    <asp:imagebutton id="imgPrintBarCodeAccess" CommandName="imgPrintBarCodeAccess" accessKey="R" tabIndex="1" height="16" width="16" border="0" tooltip="Print barcode access rights" ImageUrl="../../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
						    </td>
							<td class="ms-vb-user">
								&nbsp;<asp:Label id="lblUserName" Runat="server"></asp:Label>
							<td class="ms-vb-user">
								<asp:Label id="lblPassword" Runat="server"></asp:Label>
								<asp:Label id="lblPasswordReadable" Runat="server" Visible="false"></asp:Label>
							<td class="ms-vb-user">
								<asp:HyperLink id="lnkName" Runat="server"></asp:HyperLink></td>
							<td class="ms-vb2">
								<asp:HyperLink id="lnkAddress1" Runat="server"></asp:HyperLink></td>
							<td class="ms-vb2">
								<asp:HyperLink id="lnkEmailAddress" Runat="server"></asp:HyperLink></td>
							<td class="ms-vb2">
								<asp:HyperLink id="lnkGroupName" Runat="server"></asp:HyperLink></td>
							<td class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</td>
						</tr>
						<tr>
							<td class="ms-vh2" height="1"><img height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
							<td colspan="11" height="1">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<asp:panel id="panCard" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
										<table id="tblPanCard" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="ms-formspacer" colspan="1"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label1" CssClass="ms-vh2" runat="server" text="<b>Address2</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblAddress2" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>City</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label9" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblCity" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label3" CssClass="ms-vh2" runat="server" text="<b>State</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label11" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblState" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b>Country Name</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label12" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblCountryName" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label2" CssClass="ms-vh2" runat="server" text="<b>Office Phone</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label13" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblOfficePhone" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label6" CssClass="ms-vh2" runat="server" text="<b>Direct Phone</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label14" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblDirectPhone" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>Home Phone</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label15" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="20%">
													<asp:Label id="lblHomePhone" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
												<td width="19%">
													<asp:Label id="Label10" CssClass="ms-vh2" runat="server" text="<b>Fax Phone</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label16" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblFaxPhone" CssClass="ms-vb2" runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td width="19%">
													<asp:Label id="Label17" CssClass="ms-vh2" runat="server" text="<b>Mobile Phone</b>"></asp:Label>
												</td>
												<td width="1%">
													<asp:Label id="Label18" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</td>
												<td width="40%">
													<asp:Label id="lblMobilePhone" CssClass="ms-vb2" runat="server"></asp:Label>
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
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
