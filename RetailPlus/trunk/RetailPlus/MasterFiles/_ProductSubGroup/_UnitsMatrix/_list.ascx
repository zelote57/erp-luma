<%@ Reference Control="~/masterfiles/_productsubgroup/_list.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductSubGroup._UnitsMatrix.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../../_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgAdd" ToolTip="Add New Product Group Unit" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/newuser.gif" alt="Add New Product Group Unit" border="0" width="16" height="16" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdAdd" ToolTip="Add New Product Group Unit" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdAdd_Click">Add Unit</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Add New Product Group Unit" accessKey="N" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/delitem.gif" border="0" width="16" height="16" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Product Group Unit" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdDelete_Click">Remove Selected Unit</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idEdit" ToolTip="Add New Product Group Unit" accessKey="N" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/edit.gif" border="0" width="16" height="16" OnClick="idEdit_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Product Group Unit" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Unit</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">
						<asp:Label id="Label1" runat="server">|</asp:Label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="idBack" ToolTip="Back To Product Groups List" accessKey="B" tabIndex="5" height="16" width="16" border="0" alt="Back To Product Groups List" ImageUrl="../../../_layouts/images/back.gif" runat="server" CssClass="ms-toolbar" OnClick="idBack_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" ToolTip="Back To Product Groups List" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdBack_Click">Back To SubGroup's List</asp:linkbutton></td>
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
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblSubGroupID" runat="server" Visible="False"></asp:label></TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<COLGROUP>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</COLGROUP>
				<tr>
					<th class="ms-vh2">
						<IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"><INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall">
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colSpan="2"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
				</tr>
				<tr>
					<td colSpan="4" height="5"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="45%" align=right>
							<col width="10%" align=center>
							<col width="45%">
							<col width="10">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px" align=right>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:hyperlink id="Hyperlink2" runat="server">Base Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							    <asp:hyperlink id="Hyperlink1" runat="server">Bottom Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<COLGROUP>
							<col width="10">
							<col width="10">
							<col width="10">
							<col width="45%" align=right>
							<col width="10%" align=center>
							<col width="45%">
							<col width="10">
						</COLGROUP>
						<TR>
							<TD class="ms-vb-user">
								<INPUT id="chkList" type="checkbox" name="chkList" runat="server">
							</TD>
							<TD class="ms-vb2">
							    <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" height="16" width="16" border="0" tooltip="Delete" ImageUrl="../../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </TD>
							<TD class="ms-vb2">
							    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" height="16" width="16" border="0" tooltip="Update" ImageUrl="../../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </TD>
						    <TD class="ms-vb-user" align=right>
						        <asp:Label id="lblBaseUnitID" Runat="server" Visible=false></asp:Label>
								<asp:TextBox id="txtBaseUnitValue" Width="60" Runat="server" ReadOnly="True" CssClass="ms-short-disabled"></asp:TextBox>&nbsp;
								<asp:TextBox id="txtBaseUnit" Width="150" Runat="server" ReadOnly="True" CssClass="ms-short-disabled"></asp:TextBox></TD>
							<TD class="ms-vb-user" align=center>
								<asp:Label id="lblEquals" Runat="server">=</asp:Label></TD>
							<TD class="ms-vb-user">
							    <asp:Label id="lblBottomUnitID" Runat="server" Visible=false></asp:Label>
							    <asp:TextBox id="txtBottomUnitValue" Width="60" Runat="server" ReadOnly="True" CssClass="ms-short-disabled"></asp:TextBox>&nbsp;
								<asp:TextBox id="txtBottomUnit" Width="150" Runat="server" ReadOnly="True" CssClass="ms-short-disabled"></asp:TextBox></TD>
							<TD class="ms-vb2"></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
