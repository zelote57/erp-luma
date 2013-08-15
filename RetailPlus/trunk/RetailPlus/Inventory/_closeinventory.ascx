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
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						
					</td>
                    <td class="ms-toolbar">
					</td>
					<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
                                <td class="ms-toolbar">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrint" title="Print this Inventory for Counting" accessKey="G" tabIndex="5" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../_layouts/images/print.gif" alt="Print this Inventory for Counting" border="0" width="16" height="16" OnClick="imgPrint_Click"></asp:imagebutton></td>
											<td noWrap><asp:linkbutton id="cmdPrint" title="Print this Inventory for Counting" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print Inventory Sheet</asp:linkbutton></td>
										</tr>
									</table>
								</td>
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
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	10px" colspan="3">
                        <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
					            <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
						            <label>Select Branch to process</label>
					            </td>
					            <TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
					            <td style="HEIGHT: 15px" colspan="3">
						            <asp:dropdownlist id="cboBranch" CssClass="ms-short" runat="server" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" ></asp:dropdownlist>
					            </td>
                                <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
					            </td>
					            <td width="99%" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					            </td>
				            </tr>
				            <tr>
					            <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
						            <label>Select Product Group to process</label>
					            </td>
					            <TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
					            <td style="HEIGHT: 15px" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
						                    <asp:dropdownlist id="cboProductGroup" CssClass="ms-long" runat="server" OnSelectedIndexChanged="cboProductGroup_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false"></asp:dropdownlist>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgProductGroupSearch" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
					            </td>
                                <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtProductGroup" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgProductGroupSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand; width: 16px;" accessKey="P" 
                                        ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgProductGroupSearch_Click"></asp:imagebutton>
					            </td>
					            <td width="99%" id="Td5" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					            </td>
				            </tr>
                            <tr>
					            <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
					            </td>
					            <TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
					            <td style="PADDING-TOP:8px; HEIGHT: 15px" colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <table cellSpacing="0" cellPadding="1" border="0">
							                    <tr>
								                    <td class="ms-toolbar" noWrap><asp:imagebutton id="imgLockUnlockProduct" ToolTip="Lock / Unlock products for selling" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" onclick="imgLockUnlockProduct_Click" ImageUrl="../_layouts/images/saveitem.gif" alt="Lock products for Selling" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								                    </td>
								                    <td noWrap>
                                                        <asp:linkbutton id="cmdLockUnlockProduct" ToolTip="Lock / Unlock products for selling" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdLockUnlockProduct_Click">Lock products for Selling</asp:linkbutton>
                                                    </td>
							                    </tr>
						                    </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgLockUnlockProduct" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdLockUnlockProduct" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgCloseInventory" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCloseInventory" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
					            <td width="99%" id="Td3" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					            </td>
				            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
			                    <asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0"
			                        OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				                    <HeaderTemplate>
					                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						                    <colgroup>
							                    <col width="2%">
							                    <col width="15%">
							                    <col width="23%">
                                                <col width="12%">
							                    <col width="8%" align="center">
                                                <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="15">
						                    </colgroup>
						                    <TR>
							                    <TH class="ms-vh2" style="padding-bottom: 4px">
								                    <INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" visible="false" style="display:none">&nbsp;&nbsp;</TH>
							                    <TH class="ms-vh2" style="padding-bottom: 4px">
								                    <asp:hyperlink id="SortByDescription" runat="server">Barcode</asp:hyperlink></TH>
							                    <TH class="ms-vh2" style="padding-bottom: 4px">
								                    <asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></TH>
                                                <TH class="ms-vh2" style="padding-bottom: 4px">
								                    <asp:hyperlink id="SortByVariationDesc" runat="server"></asp:hyperlink></TH>
                                                <TH class="ms-vh2" style="padding-bottom: 4px">
								                    <asp:hyperlink id="SortByUnit" runat="server">Actual Quantity</asp:hyperlink></TH>
							                    <TH class="ms-vh2" style="padding-bottom: 4px">
								                    <asp:hyperlink id="SortByGroupName" runat="server">POS Quantity</asp:hyperlink></TH>
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
						                    <colgroup>
							                    <col width="2%">
							                    <col width="15%">
							                    <col width="23%">
                                                <col width="12%">
                                                <col width="8%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="7%" align="center">
							                    <col width="15">
						                    </colgroup>
						                    <TR>
							                    <TD class="ms-vb-user" align="right">
								                    <input id="chkList" type="checkbox" name="chkList" runat="server" visible="false" />
                                                    <input id="chkMatrixID" type="checkbox" runat="server" name="chkMatrixID" visible="false" />
								                    <asp:Label id="lblItemNo" Runat="server"></asp:Label>&nbsp;</TD>
							                    <TD class="ms-vb-user">
							                        <asp:HyperLink id="lnkBarcode" Runat="server"></asp:HyperLink></TD>
							                    <TD class="ms-vb-user">
								                    &nbsp;&nbsp;								
								                    <asp:Label id="lnkProductCode" Runat="server"></asp:Label>
							                    </TD>
                                                <TD class="ms-vb-user">
								                    &nbsp;&nbsp;								
								                    <asp:Label id="lnkVariationDesc" Runat="server"></asp:Label>
							                    </TD>
                                                <TD class="ms-vb2">
							                        <asp:TextBox ID="txtActualQuantity" runat="server" AccessKey="C" onkeyup="ComputeQuantity(this)" BorderStyle="Groove" CssClass="ms-short" Width="70%">0</asp:TextBox>
								                    <asp:ImageButton id="imgSaveActualQuantity" runat="server" ImageUrl="../_layouts/images/saveitem.gif" ToolTip="Save Actual Quantity" CommandName="imgSaveActualQuantity" CausesValidation=false height="15px" Visible="false"></asp:ImageButton></TD>
							                    <TD class="ms-vb2">
								                    <asp:TextBox ID="txtPOSQuantity" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></TD>
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
                            <asp:AsyncPostBackTrigger ControlID="imgLockUnlockProduct" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLockUnlockProduct" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgSaveActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdSaveActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgZeroOutActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdZeroOutActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgCloseInventory" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdCloseInventory" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
					</td>
				</tr>
			</table>
			
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveActualQuantity" ToolTip="Save All Actual Quantity" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" onclick="imgSaveActualQuantity_Click" ImageUrl="../_layouts/images/saveitem.gif" alt="Save All Actual Quantity" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveActualQuantity" ToolTip="Save All Actual Quantity" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveActualQuantity_Click">Save All Actual Quantity</asp:linkbutton></td>
							</tr>
						</table>
					</td>
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
                        <asp:UpdatePanel ID="updCloseInventory" runat="server">
                            <ContentTemplate>
						        <table cellSpacing="0" cellPadding="1" border="0">
							        <tr>
								        <td class="ms-toolbar" noWrap>
								            <asp:textbox id="txtClosingDate" accessKey="Q" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" CssClass="ms-short" BorderStyle="Groove" Width="80"></asp:textbox>
							                <asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label>
								            <asp:imagebutton id="imgCloseInventory" ToolTip="Close Inventory" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/delitem.gif" alt="Remove Selected Product" border="0" width="16" height="16" onclick="imgCloseInventory_Click"></asp:imagebutton></td>
								        <td noWrap><asp:linkbutton id="cmdCloseInventory" ToolTip="Close Inventory" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdCloseInventory_Click">Close Inventory</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
    