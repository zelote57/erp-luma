<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._TransferOut.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New TransferOut Order" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Add New TransferOut Order" border="0" width="16" height="16" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New TransferOut Order" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add TransferOut Order</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" title="Cancel Selected TransferOut Order" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="Cancel Selected TransferOut Order" border="0" width="16" height="16" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" title="Cancel Selected TransferOut Order" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Cancel Selected TransferOut Order</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgEdit" title="Update TransferOut order" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Update TransferOut Order" border="0" width="16" height="16" OnClick="imgEdit_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" title="Update TransferOut Order" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Update TransferOut Order</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgGRN" title="Post &amp; Issue GRN" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Issue Goods Receive Note" border="0" width="16" height="16" OnClick="imgGRN_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdGRN" title="Issue GRN" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdGRN_Click">Issue GRN</asp:linkbutton></td>
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
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Order Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtOrderStartDate"></asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Order End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtOrderEndDate"></asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:CompareValidator id="CompareValidator3" CssClass="ms-error" runat="server" ErrorMessage="'Posting Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPostingStartDate"></asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:CompareValidator id="CompareValidator4" CssClass="ms-error" runat="server" ErrorMessage="'Posting End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPostingEndDate"></asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellSpacing="0" cellPadding="0" border="0" width="100%">
				<tr>
					<td style="PADDING-BOTTOM:2px" nowrap>
						<label>
                            Order Start &nbsp;Date</label>&nbsp;
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;&nbsp;</TD>
					<td nowrap>
						<asp:TextBox id="txtOrderStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
						<asp:TextBox id="txtOrderStartTime" accessKey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">00:00</asp:TextBox>
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</TD>
					<td style="PADDING-BOTTOM:2px" nowrap>
						<label>
                            Order End Date</label>
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;&nbsp;</TD>
					<td nowrap>
						<asp:TextBox id="txtOrderEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
						<asp:TextBox id="txtOrderEndTime" accessKey="M" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">23:59</asp:TextBox>
					</td>
					<td width="99%" id="align05" noWrap align="left">&nbsp;
						<asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label2" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM:2px" nowrap>
						<label>
                            Posting Start &nbsp;Date</label>&nbsp;
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;&nbsp;</TD>
					<td nowrap>
						<asp:TextBox id="txtPostingStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
						<asp:TextBox id="txtPostingStartTime" accessKey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">00:00</asp:TextBox>
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</TD>
					<td style="PADDING-BOTTOM:2px" nowrap>
						<label>
                            Posting End Date</label>
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;&nbsp;</TD>
					<td nowrap>
						<asp:TextBox id="txtPostingEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
						<asp:TextBox id="txtPostingEndTime" accessKey="M" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">23:59</asp:TextBox>
					</td>
					<td width="99%" id="Td1" noWrap align="left">&nbsp;
						<asp:Label id="Label4" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label5" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<tr>
	    <td></td>
		<td class="ms-sectionline" height="2" ><img alt="" src="../../_layouts/images/empty.gif"></td>
		<td></td>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD style="PADDING-TOP:	10px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
							        <col width="10">
							        <col width="10">
							        <col width="10">
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
								        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
								    <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
								    <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByTransferOutNo" runat="server">TransferOut No.</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByTransferOutDate" runat="server">Order Date</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortBySupplierCode" runat="server">Vendor/Supplier</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByReqDeliveryDate" runat="server">Req Delivery Date</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByBranchCode" runat="server">Deliver to Branch</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByTransferOutSubTotal" runat="server">Amount</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByTransferOutRemarks" runat="server">Remarks</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
							        </TH>
						        </TR>
					        </table>
				        </HeaderTemplate>
				        <ItemTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblItemTemplate">
						        <colgroup>
							        <col width="10">
							        <col width="10">
							        <col width="10">
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
							        <TD class="ms-vb2">
							            <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" accessKey="D" tabIndex="1" height="16" width="16" border="0" tooltip="Cancel this transfer order" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </TD>
							        <TD class="ms-vb2">
							            <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this transfer order" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </TD>
						            <TD class="ms-vb2">
							            <asp:imagebutton id="imgItemPost" CommandName="imgItemPost" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Issue GRN for this transfer order" ImageUrl="../../_layouts/images/issuegrn.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </TD>
							        <TD class="ms-vb-user">
								        <asp:HyperLink id="lnkTransferOutNo" runat="server"></asp:HyperLink>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblTransferOutDate" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:HyperLink ID="lblSupplierCode" Runat="server"></asp:HyperLink>
								        <asp:Label ID="lblSupplierID" Runat="server" Visible="False"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblReqDeliveryDate" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblBranchCode" Runat="server"></asp:Label>
								        <asp:Label ID="lblBranchID" Runat="server" Visible="False"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblTransferOutSubTotal" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblTransferOutRemarks" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb2">
								        <A class="DropDown" id="anchorDown" href="" runat="server">
									        <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							        </TD>
						        </TR>
					        </table>
				        </ItemTemplate>
			        </asp:datalist>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboCurrentPage" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
			</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3" style="PADDING-BOTTOM: 10px"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
