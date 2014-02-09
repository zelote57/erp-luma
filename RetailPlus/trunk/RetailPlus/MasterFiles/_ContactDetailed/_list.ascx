<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ContactDetailed.__List" Codebehind="_List.ascx.cs" %>
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
		<td></td>
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Birth Date Month</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:DropDownList id="cboBirthMonth" CssClass="ms-short" Width="100%" runat="server"></asp:DropDownList>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Anniversary Month</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:DropDownList id="cboAnniversaryMonth" CssClass="ms-short" Width="100%" runat="server"></asp:DropDownList>
					</td>
					<td width="99%" id="Td2" nowrap="nowrap" align="left">&nbsp;
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Birth Date From</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtBirthStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Birth Date To</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtBirthEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
					</td>
					<td width="99%" id="align05" nowrap="nowrap" align="left">&nbsp;
						<asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label2" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Anniv Start Date</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtAnnivStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Anniv End Date</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtAnnivEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
					</td>
					<td width="99%" id="Td1" nowrap="nowrap" align="left">&nbsp;
						<asp:Label id="Label4" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label5" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Membership No/Name/Reward Card No</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap" colspan=4>
						<asp:TextBox id="txtSearch" CssClass="ms-long" Width="100%" runat="server" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td nowrap="nowrap">
						<asp:DropDownList id="cboGroup" CssClass="ms-short" Width="100%" runat="server"></asp:DropDownList>
					</td>
					<td width="99%" nowrap="nowrap" align="left">&nbsp;
					    <asp:ImageButton accessKey="s" style="CURSOR: hand" id="cmdSearch" 
                            ImageUrl="../../_layouts/images/icongo01.gif" border="0" 
                            ToolTip="Execute search" runat="server" causesvalidation="false" 
                            onclick="cmdSearch_Click"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
	    <td></td>
		<td class="ms-sectionline" height="2" ><img alt="" src="../../_layouts/images/empty.gif" /></td>
		<td></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<colgroup>
					<col width="1"/>
					<col width="25%"/>
					<col width="25%"/>
					<col width="50%"/>
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><b>Select All</b></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="true" Width="100%" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand" AlternatingItemStyle-CssClass="ms-alternating">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="10%">
							<col width="28%">
                            <col width="10%">
                            <col width="13%">
                            <col width="8%">
                            <col width="10%">
                            <col width="10%">
                            <col width="10%">
							<col width="1%">
						</colgroup>
                        <tr>
							<th class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
						    <th class="ms-vh2" style="padding-bottom: 4px"></th>
							<th class="ms-vh2" style="padding-bottom: 4px"></th>
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByContactCode" runat="server">Membership No</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByContactName" runat="server">Member Name</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByContactGroupName" runat="server">Group</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByRewardCardNo" runat="server">Reward CardNo</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByRewardExpiryDate" runat="server">Reward Expiry Date</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByBirthDate" runat="server">Member Birth Date</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortBySpouseName" runat="server">Spouse</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortBySpouseBirthDate" runat="server">Spouse BirthDate</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAnniversaryDate" runat="server">Anniversary</asp:hyperlink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px"></th>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
						<tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
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
                                <asp:HyperLink ID="lnkBusinessName" Runat="server" Target="_blank"></asp:HyperLink>
							</td>
                            <td class="ms-vb-user">
								<asp:HyperLink ID="lnkContactGroupName" Runat="server" Target="_blank"></asp:HyperLink>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblRewardCardNo" Runat="server"></asp:Label>
                                <asp:Label ID="lblRewardCardStatus" Runat="server"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblRewardExpiryDate" Runat="server"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblBirthDate" Runat="server"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblSpouseName" Runat="server"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblSpouseBirthDate" Runat="server"></asp:Label>
							</td>
                            <td class="ms-vb-user">
								<asp:Label ID="lblAnniversaryDate" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</td>
						</tr>
				</ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
			</asp:datalist>
            </td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
