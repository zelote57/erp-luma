<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.SalesAndReceivables._SalesJournals.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="/RetailPlus/_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="/RetailPlus/_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="/RetailPlus/_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgGRN" title="Post &amp; Issue GRN" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="/RetailPlus/_layouts/images/edit.gif" alt="Issue Goods Receive Note" border="0" width="16" height="16"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdGRN" title="Issue GRN" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdGRN_Click">Issue GRN</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" noWrap align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
							</TR>
						</TABLE>
					</TD>
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
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
						<IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></th></tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px">
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="16%">
							<col width="14%">
							<col width="18%">
							<col width="14%">
							<col width="18%">
							<col width="10%" align="right">
							<col width="10%">
							<col width="1">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortBySONo" runat="server">SO No.</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortBySODate" runat="server">Order Date</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByCustomerCode" runat="server">Vendor/Customer</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByReqDeliveryDate" runat="server">Req Delivery Date</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByBranchCode" runat="server">Deliver to Branch</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortBySOSubTotal" runat="server">Amount</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortBySORemarks" runat="server">Remarks</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblItemTemplate">
						<colgroup>
							<col width="10">
							<col width="16%">
							<col width="14%">
							<col width="18%">
							<col width="14%">
							<col width="18%">
							<col width="5%" align="right">
							<col width="15%">
							<col width="1">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" NAME="chkList">
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink id="lnkSONo" runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblSODate" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lblCustomerCode" Runat="server"></asp:HyperLink>
								<asp:Label ID="lblCustomerID" Runat="server" Visible="False"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblReqDeliveryDate" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblBranchCode" Runat="server"></asp:Label>
								<asp:Label ID="lblBranchID" Runat="server" Visible="False"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblSOSubTotal" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblSORemarks" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="/RetailPlus/_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3" style="PADDING-BOTTOM: 10px"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
