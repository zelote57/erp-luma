<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts.__List" Codebehind="_List.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Account" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Account" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Account" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Account</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Account" accessKey="N" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Account" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Account" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Account</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Account" accessKey="N" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Account" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Account" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Account</asp:linkbutton></td>
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
					<col width="10">
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
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstAccountSummary" runat="server" CellPadding="0" ShowHeader="false" ShowFooter="False" Width="100%">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="4%">
							<col width="25%">
							<col width="25%">
							<col width="25%">
							<col width="10%">
							<col width="10%">
							<col width="1%" align="center">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAccountSummaryName" runat="server">Account Summary</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAccountCategoryName" runat="server">Account Category</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAccountCode" runat="server">Account</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px" align="right">
								<asp:hyperlink id="SortByDebit" runat="server">Debit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px" align="right">
								<asp:hyperlink id="SortByCredit" runat="server">Credit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="1%">
							<col width="99%">
							<col width="1%" align="center">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
							</td>
							<td class="ms-smallheader">
								<b>
									<asp:Label ID="lblAccountSummaryName" Runat="server"></asp:Label>&nbsp;
									<asp:Label ID="lblAccountSummaryCode" Runat="server"></asp:Label></b>
							</td>
							<td class="ms-vb2">
							</td>
						</tr>
						<tr>
						    <td class="ms-vb-user">
							</td>
						    <td class="ms-vb-user">
						        <asp:datalist id="lstAccountCategory" runat="server" CellPadding="0" ShowHeader="False" ShowFooter="False" Width="100%">
				                    <HeaderTemplate>
				                        <colgroup>
						                    <col width="5%">
						                    <col width="99%">
						                    <col width="1%" align="center">
					                    </colgroup>
						                <tr>
							                <TH class="ms-vh2" style="padding-bottom: 4px">
								                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							                <TH class="ms-vh2" style="padding-bottom: 4px">
								                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							                <TH class="ms-vh2" style="padding-bottom: 4px">
							                </TH>
						                </tr>
				                    </HeaderTemplate>
				                    <ItemTemplate>
					                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblItemTemplate1">
						                    <colgroup>
							                    <col width="5%">
							                    <col width="99%">
							                    <col width="1%" align="center">
						                    </colgroup>
						                    <tr >
							                    <td></td>
							                    <td class="ms-styleheader">
								                    <b>
									                    <asp:Label ID="lblAccountCategoryName" Runat="server"></asp:Label>&nbsp;
									                    <asp:Label ID="lblAccountCategoryCode" Runat="server"></asp:Label></b>
							                    </td>
							                    <td class="ms-vb2">
								                    <A class="DropDown" id="anchorDownAccountCategory" href="" runat="server">
									                    <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							                    </td>
						                    </tr>
						                    <tr>
						                        <td class="ms-vb-user"></td>
						                        <td class="ms-vb-user">
						                            <asp:datalist id="lstChartOfAccounts" runat="server" CellPadding="0" ShowHeader="false" ShowFooter="False" Width="100%">
				                                        <HeaderTemplate>
					                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						                                        <colgroup>
							                                        <col width="4%">
							                                        <col width="55%">
							                                        <col width="20%">
							                                        <col width="20%">
							                                        <col width="1%" align="center">
						                                        </colgroup>
						                                        <tr>
							                                        <TH class="ms-vh2" style="padding-bottom: 4px">
								                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							                                        <TH class="ms-vh2" style="padding-bottom: 4px">
							                                        <TH class="ms-vh2" style="padding-bottom: 4px" align="right">
							                                        <TH class="ms-vh2" style="padding-bottom: 4px" align="right">
							                                        <TH class="ms-vh2" style="padding-bottom: 4px">
							                                        </TH>
						                                        </tr>
					                                        </table>
				                                        </HeaderTemplate>
				                                        <ItemTemplate>
					                                        <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						                                        <colgroup>
							                                        <col width="4%">
							                                        <col width="55%">
							                                        <col width="20%">
							                                        <col width="20%">
							                                        <col width="1%" align="center">
						                                        </colgroup>
						                                        <tr >
							                                        <td class="ms-vb-user">
							                                        </td>
							                                        <td class="ms-vb-user">
							                                            <input type="checkbox" id="chkList" runat="server" name="chkList" />
								                                        <asp:Label ID="lblAccountCode" Runat="server"></asp:Label>&nbsp;
								                                        <asp:Label ID="lblAccountName" Runat="server"></asp:Label>
							                                        </td>
							                                        <td class="ms-vb-user" align="right">
								                                        <asp:Label ID="lblDebit" Runat="server"></asp:Label>
							                                        </td>
							                                        <td class="ms-vb-user" align="right">
								                                        <asp:Label ID="lblCredit" Runat="server"></asp:Label>
							                                        </td>
							                                        <td class="ms-vb2">
								                                        <A class="DropDown" id="anchorDown" href="" runat="server">
									                                        <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							                                        </td>
						                                        </tr>
					                                        </table>
				                                        </ItemTemplate>
			                                        </asp:datalist>
						                        </td>
							                    <td class="ms-vb-user"></td>
						                    </tr>
					                    </table>
				                    </ItemTemplate>
			                    </asp:datalist>
							</td>
							<td class="ms-vb-user">
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
</table>
