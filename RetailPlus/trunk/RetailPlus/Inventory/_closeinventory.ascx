<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__CloseInventory" Codebehind="_closeinventory.ascx.cs" %>
<script language="JavaScript" src="../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../_Scripts/Inventory.js"></script>
<script language="JavaScript" src="../_Scripts/calendar.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
				    <td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap>
                                	Select Branch to process: 						
								</td>
								<td noWrap>
								    <asp:dropdownlist id="cboBranch" CssClass="ms-short" runat="server" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" ></asp:dropdownlist>
								</td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:FileUpload ID="txtPath" runat="server" cssclass="ms-short" tabindex="7"></asp:FileUpload>
								    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="imgUpload" ToolTip="Upload Actual Quantity From File" accessKey="U" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/newuser.gif" alt="Upload Actual Quantity From File" border="0" width="16" height="16" onclick="imgUpload_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdUpload" ToolTip="Upload Actual Quantity From File" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdUpload_Click">Upload Actual Quantity From File</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveActualQuantity" ToolTip="Save All Actual Quantity" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" onclick="imgSaveActualQuantity_Click" ImageUrl="../_layouts/images/saveitem.gif" alt="Save All Actual Quantity" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveActualQuantity" ToolTip="Save All Actual Quantity" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveActualQuantity_Click">Save All Actual Quantity</asp:linkbutton></td>
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
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
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
						<IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../_layouts/images/blank.gif" width="1"></th>
				</tr>
			    <tr style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 10px; PADDING-TOP: 0px">
					<td class="ms-vb2" colSpan="4">
					    <asp:RadioButton ID="rdoShowAll" GroupName="FilterProductList" runat="server" Text="Show both active and inactive products " OnCheckedChanged="rdoShowAll_CheckedChanged" AutoPostBack="True" />
                        <asp:RadioButton ID="rdoShowActiveOnly" GroupName="FilterProductList" runat="server" Text="Show active products only " AutoPostBack="True" OnCheckedChanged="rdoShowActiveOnly_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="rdoShowInactiveOnly" GroupName="FilterProductList" runat="server" Text="Show inactive products only " AutoPostBack="True" OnCheckedChanged="rdoShowInactiveOnly_CheckedChanged"/>
					</td>
				</tr>
				<tr style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 10px; PADDING-TOP: 0px">
					<td class="ms-vb2"  colSpan="2">
					    No. of items to show (0=All Items)&nbsp;:<asp:TextBox id="txtLimit" onkeypress="AllNum()" runat="server" CssClass="ms-short" Width=50 MaxLength="5" BorderStyle="Groove">100</asp:TextBox>
					</td>
					<td class="ms-vb2" colSpan="2">
					    Search Product: <asp:TextBox id="txtSearch" runat="server" CssClass="ms-short" MaxLength="30" BorderStyle="Groove"></asp:TextBox>
					    <asp:ImageButton id="cmdSearch" title="Execute search" style="CURSOR: hand" accessKey="s" BorderStyle="Groove" runat="server" ImageUrl="../_layouts/images/icongo01.gif" border="0" alt="Execute search" causesvalidation=false Height="14px" OnClick="cmdSearch_Click"></asp:ImageButton>
					</td>
				</tr>
				<tr style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 10px; PADDING-TOP: 0px">
					<td colSpan="4" height="5"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
			        <asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" AlternatingItemStyle-CssClass="ms-alternating"
			            OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
							        <col width="2%">
							        <col width="18%">
							        <col width="28%">
							        <col width="7%" align=center>
							        <col width="10%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="15">
						        </colgroup>
						        <TR>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" visible="false">&nbsp;&nbsp;</TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByDescription" runat="server">Barcode</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        &nbsp;&nbsp;
								        <asp:hyperlink id="SortByGroupName" runat="server">POS Quantity</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByUnit" runat="server">Actual Quantity</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByShort" runat="server">Short</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByOver" runat="server">Over</asp:hyperlink></TH>
						            <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="Hyperlink1" runat="server">Purchase Price</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByAmountShort" runat="server">Amount Short</asp:hyperlink></TH>
						            <TH class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByAmountOver" runat="server">Amount Over</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						        </TR>
					        </table>
				        </HeaderTemplate>
				        <ItemTemplate>
					        <TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						        <COLGROUP>
							        <col width="2%">
							        <col width="18%">
							        <col width="28%">
							        <col width="7%" align=center>
							        <col width="10%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="7%" align=center>
							        <col width="15">
						        </COLGROUP>
						        <TR>
							        <TD class="ms-vb-user" align=right>
								        <INPUT id="chkList" type="checkbox" name="chkList" runat="server" visible=false>
								        <asp:Label id="lblItemNo" Runat="server"></asp:Label></TD>
							        <TD class="ms-vb-user">
							            <asp:HyperLink id="lnkBarcode" Runat="server"></asp:HyperLink></TD>
							        <TD class="ms-vb-user">
								        &nbsp;&nbsp;								
								        <asp:Label id="lnkProductCode" Runat="server"></asp:Label>
							        </TD>
							        <TD class="ms-vb2">
								        <asp:TextBox ID="txtPOSQuantity" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
							        <TD class="ms-vb2">
							            <asp:TextBox ID="txtActualQuantity" runat="server" AccessKey="C" onkeyup="ComputeQuantity(this)" BorderStyle="Groove" CssClass="ms-short" Width="70%">0</asp:TextBox>
								        <asp:ImageButton id="imgSaveActualQuantity" runat="server" ImageUrl="../_layouts/images/saveitem.gif" ToolTip="Save Actual Quantity" CommandName="imgSaveActualQuantity" CausesValidation=false height="15px"></asp:ImageButton></TD>
							        <TD class="ms-vb-user">
							            <asp:TextBox ID="txtShort" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
							        <TD class="ms-vb-user">
							            <asp:TextBox ID="txtOver" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
							        <TD class="ms-vb-user">
								        <asp:TextBox ID="txtPurchasePrice" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
							        <TD class="ms-vb-user">
								        <asp:TextBox ID="txtAmountShort" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
						            <TD class="ms-vb-user">
								        <asp:TextBox ID="txtAmountOver" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
							        <TD class="ms-vb-user">
								        <asp:ImageButton id="imgProductTag" runat="server" ImageUrl="../_layouts/images/prodtagact.gif" ToolTip="Tag as inactive" CommandName="imgProductTag" CausesValidation=false height="15px"></asp:ImageButton></TD>
						        </TR>
					        </TABLE>
				        </ItemTemplate>
			        </asp:datalist>
			    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdoShowAll" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rdoShowActiveOnly" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rdoShowInactiveOnly" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgZeroOutActualQuantity" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdZeroOutActualQuantity" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgCloseInventory" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdCloseInventory" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
		</TD>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
				    <TD class="ms-toolbar" id="TD1" noWrap align="right" width="99%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" noWrap align="right"></td>
							</TR>
						</TABLE>
					</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgZeroOutActualQuantity" ToolTip="Zero Out Actual Quantity" accessKey="Z" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/newuser.gif" alt="Zero Out Actual Quantity" border="0" width="16" height="16" onclick="imgZeroOutActualQuantity_Click"></asp:imagebutton>&nbsp;</td>
								<td noWrap><asp:linkbutton id="cmdZeroOutActualQuantity" ToolTip="Zero Out Actual Quantity" accessKey="Z" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdZeroOutActualQuantity_Click">Zero Out Actual Quantity</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap>
								    <asp:textbox id="txtClosingDate" accessKey="Q" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" CssClass="ms-short" BorderStyle="Groove" Width="80"></asp:textbox>
							        <asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label>
								    <asp:imagebutton id="imgCloseInventory" ToolTip="Close Inventory" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/delitem.gif" alt="Remove Selected Product" border="0" width="16" height="16" onclick="imgCloseInventory_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCloseInventory" ToolTip="Close Inventory" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdCloseInventory_Click">Close Inventory</asp:linkbutton></td>
							</tr>
						</table>
					</td>					
					<td class="ms-toolbar" id="Td2" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
