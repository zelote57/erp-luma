<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../_Scripts/ConfirmDelete.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Product" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/newuser.gif" alt="Add New Product" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Product" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Product" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/delitem.gif" alt="Remove Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Product" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/edit.gif" alt="Update Selected Product" border="0" width="16" height="16"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Product</asp:linkbutton></td>
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
						</TABLE>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</TABLE>
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
    <tr>
		<td></td>
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
				<asp:PlaceHolder id="holderExpiry" runat="server" Visible="false">
                <tr>
                    <td style="HEIGHT:15px" nowrap="nowrap">
						<label>Filter by Expiration Date</label>
					</td>
					<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					<td style="HEIGHT: 15px" colspan="7">
						<asp:TextBox id="txtExpiryDate" ondblclick="ontime(this)" accessKey="E" CssClass="ms-short" runat="server" ToolTip="Double click to select date from Calendar" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
                        <asp:Label id="Label1" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
					<td width="99%" id="Td1" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
                <tr>
                    <td style="HEIGHT:15px" colspan="8">
					</td>
					<td width="99%" id="Td5" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
                </asp:PlaceHolder>
                <tr>
					<td style="HEIGHT:15px" nowrap="nowrap">
						<label>Filter by Branch</label>
					</td>
					<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					<td style="HEIGHT: 15px">
						<asp:dropdownlist id="cboBranch" CssClass="ms-medium" runat="server"></asp:dropdownlist>
					</td>
                    <td style="HEIGHT:15px" nowrap="nowrap">
                    </td>
                    <td style="HEIGHT:15px;  width:25px; text-align:center;" nowrap="nowrap">
                    </td>
                    <td style="HEIGHT:15px" nowrap="nowrap">
						<label>Filter by Supplier</label>
					</td>
					<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					<td style="HEIGHT: 15px">
						<asp:dropdownlist id="cboContact" CssClass="ms-medium" runat="server"></asp:dropdownlist>
					</td>
                    <td style="HEIGHT:15px" nowrap="nowrap">
                        <asp:textbox id="txtContactCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                        <asp:imagebutton id="imgContactCodeSearch" ToolTip="Execute search" 
                            style="CURSOR: hand; width: 16px;" accessKey="P" 
                            ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                            CausesValidation="False" onclick="imgContactCodeSearch_Click"></asp:imagebutton>
					</td>
					<td width="99%" id="Td2" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
                <tr>
					<td style="HEIGHT:15px" nowrap="nowrap">
                        <label>Filter by Group</label>
					</td>
					<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					<td style="HEIGHT: 15px">
                        <asp:dropdownlist id="cboProductGroup" CssClass="ms-medium" runat="server" AutoPostBack="True" onselectedindexchanged="cboProductGroup_SelectedIndexChanged"></asp:dropdownlist>
					</td>
                    <td style="HEIGHT:15px" nowrap="nowrap">
                        <asp:textbox id="txtProductGroupCode" accessKey="G" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                        <asp:imagebutton id="imgProductGroupCodeSearch" ToolTip="Execute search" 
                            style="CURSOR: hand" accessKey="P" 
                            ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                            CausesValidation="False" onclick="imgProductGroupCodeSearch_Click"></asp:imagebutton>
                    </td>
                    <td style="HEIGHT:15px; width:25px; text-align:center;" nowrap="nowrap">
                    </td>
                    <td style="HEIGHT:15px" nowrap="nowrap">
						<label>Filter by Sub Group</label>
					</td>
					<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					<td style="HEIGHT: 15px">
						<asp:DropDownList id="cboSubGroup" runat="server" CssClass="ms-medium"></asp:DropDownList>
					</td>
                    <td style="HEIGHT:15px" nowrap="nowrap">
                        <asp:textbox id="txtSubGroupCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                        <asp:imagebutton id="imgSubGroupCodeSearch" ToolTip="Execute search" 
                            style="CURSOR: hand" accessKey="P" 
                            ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                            CausesValidation="False" onclick="imgSubGroupCodeSearch_Click"></asp:imagebutton>
                    </td>
					<td width="99%" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
                <tr>
					<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
						<label>Product Code like</label>&nbsp;
					</td>
					<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					<td colspan="7">
						<asp:TextBox id="txtProductCode" runat="server" TextMode="MultiLine" Rows="1" Width="95%"></asp:TextBox>
                        <asp:ImageButton accessKey="s" style="CURSOR: hand" id="cmdSearch" ImageUrl="../_layouts/images/icongo01.gif" border="0" ToolTip="Execute search" runat="server" causesvalidation="false" onclick="cmdSearch_Click"></asp:ImageButton>
						<asp:Label id="Label4" CssClass="ms-error" runat="server" Visible="false">Enter 'Product Code' separated by semi-colon(;) to filter more than one product code.</asp:Label>
					</td>
					<td width="99%" id="align03" nowrap="nowrap" align="right" style="HEIGHT: 15px"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
	    <td></td>
		<td class="ms-sectionline" height="2" ><img alt="" src="../_layouts/images/empty.gif" /></td>
		<td></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<td>
			<asp:datalist id="lstItem" runat="server" Width="100%" CellPadding="0" AlternatingItemStyle-CssClass="ms-alternating" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="12%">
							<col width="26%">
							<col width="15%">
							<col width="15%">
							<col width="14%">
							<col width="15%">
							<col width="1%">
						</colgroup>
						<tr>
							<th class="ms-vh2" style="padding-bottom: 4px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" /></th>
							<th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByBarcode" runat="server">Barcode</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByGroupName" runat="server">Group / Sub-group</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; text-align:right"><asp:hyperlink id="SortByUnit" runat="server">Total Quantity</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; text-align:right"><asp:hyperlink id="SortByMinThreshold" runat="server">Min. Threshold</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; text-align:right"><asp:hyperlink id="SortByMaxThreshold" runat="server">Max. Threshold</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px"></th>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
						<tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
							<td class="ms-vb-user"><input id="chkList" type="checkbox" name="chkList" runat="server" /></td>
							<td class="ms-vb-user"><asp:HyperLink id="lnkBarcode" Runat="server" Target="_blank"></asp:HyperLink></td>
							<td class="ms-vb-user"><asp:HyperLink id="lnkProductCode" Runat="server" Target="_blank"></asp:HyperLink>
							<td class="ms-vb2"><asp:Label id="lblGroup" Runat="server"></asp:Label></td>
							<td class="ms-vb2" style="text-align:right"><asp:Label id="lblQuantity" Runat="server"></asp:Label></td>
							<td class="ms-vb-user" style="text-align:right"><asp:Label ID="lblMinThreshold" Runat="server"></asp:Label></td>
							<td class="ms-vb-user" style="text-align:right"><asp:Label ID="lblMaxThreshold" Runat="server"></asp:Label></td>
							<td class="ms-vb2"></td>
						</tr>
						<tr class="ms-vb2">
						    <td colspan="2"></td>
						    <td colspan="4">
						        <asp:datalist id="lstBranchInventory" runat="server" Width="80%" CellPadding="0" CssClass="ms-styleheader" OnItemDataBound="lstBranchInventory_ItemDataBound" Visible=false>
				                    <HeaderTemplate>
					                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate" class="">
						                    <colgroup>
							                    <col width="2%">
							                    <col width="70%">
							                    <col width="28%">
						                    </colgroup>
						                    <tr>
							                    <th class="ms-vh2" style="padding-top: 1px"></th>
							                    <th class="ms-vh2" style="padding-top: 1px"><asp:hyperlink id="SortByBranchCode" runat="server">Branch</asp:hyperlink></th>
							                    <th class="ms-vh2" style="padding-top: 1px; text-align:right"><asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></th>
						                    </tr>
				                    </HeaderTemplate>
				                    <ItemTemplate>
						                    <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
							                    <td class="ms-vb-user"><input id="chkList" type="checkbox" name="chkList" runat="server" visible="false" /></td>
							                    <td class="ms-vb-user"><asp:HyperLink id="lnkBranchCode" Runat="server"></asp:HyperLink></td>
							                    <td class="ms-vb2" style="text-align:right"><asp:Label id="lblQuantity" Runat="server"></asp:Label></td>
						                    </tr>
				                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
			                    </asp:datalist>
						    </td>
                            <td colspan="3"></td>
						</tr>
				</ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
			</asp:datalist>
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
