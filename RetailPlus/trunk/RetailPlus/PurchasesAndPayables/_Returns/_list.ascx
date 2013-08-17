<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.PurchasesAndPayables._Returns.__List" Codebehind="_List.ascx.cs" %>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New Purchase Return" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/newuser.gif" alt="Add New Purchase Return" border="0" width="16" height="16" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New Purchase Return" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Purchase Return</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" title="Cancel Selected Purchase Return" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/delitem.gif" alt="Cancel Selected Purchase Return" border="0" width="16" height="16" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" title="Cancel Selected Purchase Return" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Cancel Selected Purchase Return</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgEdit" title="Update Purchase Return" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Update Purchase Return" border="0" width="16" height="16" OnClick="imgEdit_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" title="Update Purchase Return" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" OnClick="cmdEdit_Click">Update Purchase Return</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPost" title="Post this return order" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/post.gif" alt="Post this return order" border="0" width="16" height="16" OnClick="imgPost_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdPost" title="Post this return order" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdPost_Click">Post</asp:linkbutton></td>
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
    <TR>
		<TD></TD>
		<TD><asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Order Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtOrderStartDate"></asp:CompareValidator><asp:Label
                ID="lblStatus" runat="server" CssClass="ms-error" Visible="False"></asp:Label></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Order End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtOrderEndDate"></asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:CompareValidator id="CompareValidator3" CssClass="ms-error" runat="server" ErrorMessage="'Posting Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPostingStartDate"></asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:CompareValidator id="CompareValidator4" CssClass="ms-error" runat="server" ErrorMessage="'Posting End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPostingEndDate"></asp:CompareValidator></TD>
	</TR>
    <TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
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
				<tr>
					<td style="PADDING-BOTTOM:2px" nowrap>
						<label>PO Return No /Vendor /Remarks</label>&nbsp;
					</td>
					<TD class="ms-separator">&nbsp;&nbsp;&nbsp;</TD>
					<td nowrap colspan=4>
						<asp:TextBox id="txtSearch" CssClass="ms-long" Width="100%" runat="server" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td nowrap >
						<asp:DropDownList id="cboStatus" CssClass="ms-short" Width="100%" runat="server"></asp:DropDownList>
					</td>
					<td width="99%" noWrap align="left">&nbsp;
					    <asp:ImageButton accessKey="s" style="CURSOR: hand" id="cmdSearch" ImageUrl="../../_layouts/images/icongo01.gif" border="0" ToolTip="Execute search" runat="server" causesvalidation="false" onclick="cmdSearch_Click"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<tr>
	    <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td class="ms-sectionline" height="2" style="MARGIN-BOTTOM: 5px"><img alt="" src="../../_layouts/images/empty.gif"></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
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
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th></tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px">
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colSpan="2"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
				</tr>
				<tr>
					<td colSpan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand" AlternatingItemStyle-CssClass="ms-alternating">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
							        <col width="10">
							        <col width="10">
							        <col width="10">
							        <col width="10">
							        <col width="20%">
							        <col width="14%">
							        <col width="16%">
							        <col width="14%">
							        <col width="16%">
							        <col width="10%" align="right">
							        <col width="10%">
							        <col width="1">
						        </colgroup>
						        <TR>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" /></TH>
								    <TH class="ms-vh2" style="padding-bottom: 4px">
								        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
								    <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
								    <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByReturnNo" runat="server">Return No.</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByReturnDate" runat="server">Return Date</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortBySupplierCode" runat="server">Vendor/Supplier</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByReqReturnDate" runat="server">Req Return Date</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByBranchCode" runat="server">Return From Branch</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px" align="right">
								        <asp:hyperlink id="SortBySubTotal" runat="server">Amount</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByRemarks" runat="server">Remarks</asp:hyperlink></TH>
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
							        <col width="10">
							        <col width="20%">
							        <col width="14%">
							        <col width="16%">
							        <col width="14%">
							        <col width="16%">
							        <col width="5%" align="right">
							        <col width="15%">
							        <col width="1">
						        </colgroup>
						        <TR>
							        <TD class="ms-vb-user">
								        <input type="checkbox" id="chkList" runat="server" name="chkList" />
							        </TD>
							        <TD class="ms-vb2">
							            <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" accessKey="D" tabIndex="1" height="16" width="16" border="0" tooltip="Cancel this order" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </TD>
							        <TD class="ms-vb2">
							            <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this order" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </TD>
						            <TD class="ms-vb2">
							            <asp:imagebutton id="imgItemPost" CommandName="imgItemPost" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Post for this order" ImageUrl="../../_layouts/images/post.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </TD>
							        <TD class="ms-vb-user">
								        <asp:HyperLink id="lnkReturnNo" runat="server"></asp:HyperLink>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblReturnDate" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:HyperLink ID="lblSupplierCode" Runat="server"></asp:HyperLink>
								        <asp:Label ID="lblSupplierID" Runat="server" Visible="False"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblReqreturnDate" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblBranchCode" Runat="server"></asp:Label>
								        <asp:Label ID="lblBranchID" Runat="server" Visible="False"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblSubTotal" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb-user">
								        <asp:Label ID="lblRemarks" Runat="server"></asp:Label>
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
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colSpan="3" style="PADDING-BOTTOM: 10px"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
