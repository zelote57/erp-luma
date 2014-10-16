<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits._Guarantors.__List" Codebehind="_list.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Customer" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Customer" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Customer" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Customer</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Customer" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Customer" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Customer" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Customer</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Customer" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Customer" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="idEdit_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Customer" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Customer</asp:linkbutton></td>
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
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
				<tr>
					<td style="padding-bottom:2px" nowrap>
						&nbsp;<label>Credit Card Status</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap>
						<asp:dropdownlist id="cboCreditCardStatus" CssClass="ms-short" runat="server"></asp:dropdownlist>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap>
						<label>Credit Type</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap>
						<asp:dropdownlist id="cboCreditType" CssClass="ms-short" runat="server"></asp:dropdownlist>
					</td>
					<td width="99%" id="align05" nowrap="nowrap" align="left">&nbsp;
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap>
						&nbsp;<label>Credit Card Expiry Date From</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap>
						<asp:TextBox id="txtExpiryDateFrom" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap>
						<label>Credit Card Expiry Date To</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap>
						<asp:TextBox id="txtExpiryDateTo" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
					</td>
					<td width="99%" id="Td1" nowrap="nowrap" align="left">&nbsp;
						<asp:Label id="Label4" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label5" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap>
						&nbsp;<label>Credit Card No or Customer Code or Customer Name</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap colspan=5>
						<asp:TextBox id="txtSearch" CssClass="ms-long" Width="100%" runat="server" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td width="99%" nowrap="nowrap" align="left">&nbsp;
					    <asp:ImageButton accessKey="s" style="CURSOR: hand" id="cmdSearch" ImageUrl="../../_layouts/images/icongo01.gif" border="0" ToolTip="Execute search" runat="server" causesvalidation="false" onclick="cmdSearch_Click"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
	    <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td class="ms-sectionline" height="2" style="MARGIN-BOTTOM: 5px"><img alt="" src="../../_layouts/images/empty.gif" /></td>
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
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand" AlternatingItemStyle-CssClass="ms-alternating">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="10%">
							<col width="18%">
							<col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
							<col width="1%">
						</colgroup>
						<tr>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
						    <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByContactCode" runat="server">Guarantor Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByContactName" runat="server">Guarantor Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByCreditType" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByCreditCardNo" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByCreditCardStatus" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByExpiryDate" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByCreditLimit" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByCredit" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAvailableCredit" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByTotalPurchases" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByLastBillingDate" runat="server"></asp:hyperlink></TH>
                            <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="10%">
							<col width="18%">
							<col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
                            <col width="8%">
							<col width="1%">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</td>
							<td class="ms-vb2">
							    <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" accessKey="D" tabIndex="1" height="16" width="16" border="0" tooltip="Delete this Contact" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </td>
							<td class="ms-vb2">
							    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this Contact" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkContactCode" Runat="server" Target="_blank"></asp:HyperLink>
							</td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkContactName" Runat="server" Target="_blank"></asp:HyperLink>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblCreditType" Runat="server" Visible="false"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblCreditCardNo" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblCreditCardStatus" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblExpiryDate" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblCreditLimit" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">

								<asp:Label ID="lblCredit" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblAvailableCredit" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblTotalPurchases" Runat="server" Visible="false"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblLastBillingDate" Runat="server" Visible="false"></asp:Label>
							</td>
							<td class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</td>
						</tr>
                        <tr>
                            <td colspan="4"></td>
                            <td colspan="11">
                                <asp:datalist id="lstItemCustomer" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItemCustomer_ItemDataBound" OnItemCommand="lstItemCustomer_ItemCommand" AlternatingItemStyle-CssClass="ms-alternating">
				                <HeaderTemplate>
					                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						                <colgroup>
							                <col width="10">
							                <col width="10">
							                <col width="10">
							                <col width="28%">
							                <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
							                <col width="1%">
						                </colgroup>
						                <tr>
							                <TH class="ms-vh2" style="padding-bottom: 4px">
								                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
						                    <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							                <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							                <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByContactName" runat="server">Customer Name</asp:hyperlink></TH>
							                <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByCreditType" runat="server">Credit Type</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByCreditCardNo" runat="server">Card No</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByCreditCardStatus" runat="server">Status</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByExpiryDate" runat="server">Expiry Date</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByCreditLimit" runat="server">Credit Limit</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByCredit" runat="server">Credit</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByAvailableCredit" runat="server">Available Credit</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByTotalPurchases" runat="server">Total Purchases</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                <asp:hyperlink id="SortByLastBillingDate" runat="server">Last Billing Date</asp:hyperlink></TH>
                                            <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						                </tr>
					                </table>
				                </HeaderTemplate>
				                <ItemTemplate>
					                <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						                <colgroup>
							                <col width="10">
							                <col width="10">
							                <col width="10">
							                <col width="28%">
							                <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
                                            <col width="8%">
							                <col width="1%">
						                </colgroup>
						                <tr>
							                <td class="ms-vb-user">
								                <input type="checkbox" id="chkList" runat="server" name="chkList" />
							                </td>
							                <td class="ms-vb2">
							                    <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" accessKey="D" tabIndex="1" height="16" width="16" border="0" tooltip="Delete this Contact" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						                    </td>
							                <td class="ms-vb2">
							                    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this Contact" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						                    </td>
							                <td class="ms-vb-user">
								                <asp:HyperLink ID="lnkContactName" Runat="server" Target="_blank"></asp:HyperLink>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblCreditType" Runat="server"></asp:Label>
							                </td>
							                <td class="ms-vb-user">
								                <asp:Label ID="lblCreditCardNo" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblCreditCardStatus" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblExpiryDate" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblCreditLimit" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">

								                <asp:Label ID="lblCredit" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblAvailableCredit" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblTotalPurchases" Runat="server"></asp:Label>
							                </td>
                                            <td class="ms-vb-user">
								                <asp:Label ID="lblLastBillingDate" Runat="server"></asp:Label>
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
