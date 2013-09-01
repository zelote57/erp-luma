<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__CloseInventory" Codebehind="_closeinventory.ascx.cs" %>
<script language="JavaScript" src="../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../_Scripts/Inventory.js"></script>
<script language="JavaScript" src="../_Scripts/calendar.js"></script>

<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						
					</td>
                    <td class="ms-toolbar">
					</td>
					<td class="ms-toolbar" id="align01" nowrap align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
                                <td class="ms-toolbar">
									<table cellspacing="0" cellpadding="1" border="0">
										<tr>
											<td class="ms-toolbar" nowrap><asp:imagebutton id="imgPrint" title="Print this Inventory for Counting" accessKey="G" tabIndex="5" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../_layouts/images/print.gif" alt="Print this Inventory for Counting" border="0" width="16" height="16" OnClick="imgPrint_Click"></asp:imagebutton></td>
											<td nowrap><asp:linkbutton id="cmdPrint" title="Print this Inventory for Counting" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print Inventory Sheet</asp:linkbutton></td>
										</tr>
									</table>
								</td>
								<td class="ms-toolbar" nowrap align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	10px" colspan="3">
                        <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
					            <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
						            <label>Select Branch to process</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
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
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
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
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="PADDING-TOP:8px; HEIGHT: 15px" colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <table cellspacing="0" cellpadding="1" border="0">
							                    <tr>
								                    <td class="ms-toolbar" nowrap><asp:imagebutton id="imgLockUnlockProduct" ToolTip="Lock / Unlock products for selling" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" onclick="imgLockUnlockProduct_Click" ImageUrl="../_layouts/images/saveitem.gif" alt="Lock products for Selling" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								                    </td>
								                    <td nowrap>
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
			                    <asp:datalist id="lstItem" runat="server" Width="100%" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
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
						                    <tr style="padding-bottom: 4px">
							                    <th class="ms-vh2">
								                    <INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" visible="false" style="display:none">&nbsp;&nbsp;</th>
							                    <th class="ms-vh2"><asp:hyperlink id="SortByDescription" runat="server">Barcode</asp:hyperlink></th>
							                    <th class="ms-vh2"><asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></th>
                                                <th class="ms-vh2"><asp:hyperlink id="SortByVariationDesc" runat="server"></asp:hyperlink></th>
                                                <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByUnit" runat="server">Actual Quantity</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByGroupName" runat="server">POS Quantity</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByShort" runat="server">Short</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByOver" runat="server">Over</asp:hyperlink></th>
						                        <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByPurchasePrice" runat="server">Purchase Price</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByAmountShort" runat="server">Amount Short</asp:hyperlink></th>
						                        <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByAmountOver" runat="server">Amount Over</asp:hyperlink></th>
							                    <th class="ms-vh2"></th>
						                    </tr>
				                    </HeaderTemplate>
				                    <ItemTemplate>
						                    <tr style="padding-bottom: 4px" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
							                    <td class="ms-vb-user" align="right">
								                    <input id="chkList" type="checkbox" name="chkList" runat="server" visible="false" />
                                                    <input id="chkMatrixID" type="checkbox" runat="server" name="chkMatrixID" visible="false" />
								                    <asp:Label id="lblItemNo" Runat="server"></asp:Label>&nbsp;</td>
							                    <td class="ms-vb-user">
							                        <asp:HyperLink id="lnkBarcode" Runat="server" Target="_blank"></asp:HyperLink></td>
							                    <td class="ms-vb-user">
								                    &nbsp;&nbsp;								
								                    <asp:Label id="lnkProductCode" Runat="server"></asp:Label>
							                    </td>
                                                <td class="ms-vb-user">
								                    &nbsp;&nbsp;								
								                    <asp:Label id="lnkVariationDesc" Runat="server"></asp:Label>
							                    </td>
                                                <td class="ms-vb2">
							                        <asp:TextBox ID="txtActualQuantity" runat="server" AccessKey="C" onkeyup="ComputeQuantity(this)" BorderStyle="Groove" CssClass="ms-short" Width="70%">0</asp:TextBox>
								                    <asp:ImageButton id="imgSaveActualQuantity" runat="server" ImageUrl="../_layouts/images/saveitem.gif" ToolTip="Save Actual Quantity" CommandName="imgSaveActualQuantity" CausesValidation="false" height="15px"></asp:ImageButton></td>
							                    <td class="ms-vb2">
								                    <asp:TextBox ID="txtPOSQuantity" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></td>
							                    <td class="ms-vb-user">
							                        <asp:TextBox ID="txtShort" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></td>
							                    <td class="ms-vb-user">
							                        <asp:TextBox ID="txtOver" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></td>
							                    <td class="ms-vb-user">
								                    <asp:TextBox ID="txtPurchasePrice" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></td>
							                    <td class="ms-vb-user">
								                    <asp:TextBox ID="txtAmountShort" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></td>
						                        <td class="ms-vb-user">
								                    <asp:TextBox ID="txtAmountOver" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled" Width="98%"></asp:TextBox></td>
							                    <td class="ms-vb-user">
								                    <asp:ImageButton id="imgProductTag" runat="server" ImageUrl="../_layouts/images/prodtagact.gif" ToolTip="Tag as inactive" CommandName="imgProductTag" CausesValidation="false" height="15px"></asp:ImageButton></td>
						                    </tr>
				                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
			                    </asp:datalist>
			                </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="imgLockUnlockProduct" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLockUnlockProduct" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgSaveActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdSaveActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgZeroOutActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdZeroOutActualQuantity" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgCopyPOSToActual" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdCopyPOSToActual" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgCloseInventory" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdCloseInventory" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
					</td>
				</tr>
			</table>
			
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
				    <td class="ms-toolbar" id="td1" nowrap align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap align="right"></td>
							</tr>
						</table>
					</td>
                    <td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgSaveActualQuantity" ToolTip="Save All Actual Quantity" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" onclick="imgSaveActualQuantity_Click" ImageUrl="../_layouts/images/saveitem.gif" alt="Save All Actual Quantity" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap><asp:linkbutton id="cmdSaveActualQuantity" ToolTip="Save All Actual Quantity" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveActualQuantity_Click">Save All Actual Quantity</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgZeroOutActualQuantity" ToolTip="Zero Out Actual Quantity" accessKey="Z" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/newuser.gif" alt="Zero Out Actual Quantity" border="0" width="16" height="16" onclick="imgZeroOutActualQuantity_Click"></asp:imagebutton>&nbsp;</td>
								<td nowrap><asp:linkbutton id="cmdZeroOutActualQuantity" ToolTip="Zero Out Actual Quantity" accessKey="Z" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdZeroOutActualQuantity_Click">Zero Out Actual Quantity</asp:linkbutton></td>
							</tr>
						</table>
					</td>
                    <td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgCopyPOSToActual" ToolTip="Copy current POS Quantity to Actual Quantity" accessKey="Z" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/newuser.gif" alt="Copty POS Quantity to Actual Quantity" border="0" width="16" height="16" onclick="imgCopyPOSToActual_Click"></asp:imagebutton>&nbsp;</td>
								<td nowrap><asp:linkbutton id="cmdCopyPOSToActual" ToolTip="Copy current POS Quantity to Actual Quantity" accessKey="Z" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdCopyPOSToActual_Click">Copy POS Quantity To Actual</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
                        <asp:UpdatePanel ID="updCloseInventory" runat="server">
                            <ContentTemplate>
						        <table cellspacing="0" cellpadding="1" border="0">
							        <tr>
								        <td class="ms-toolbar" nowrap>
								            <asp:textbox id="txtClosingDate" accessKey="Q" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" CssClass="ms-short" BorderStyle="Groove" Width="80"></asp:textbox>
							                <asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label>
								            <asp:imagebutton id="imgCloseInventory" ToolTip="Close Inventory" accessKey="X" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/delitem.gif" alt="Remove Selected Product" border="0" width="16" height="16" onclick="imgCloseInventory_Click"></asp:imagebutton></td>
								        <td nowrap><asp:linkbutton id="cmdCloseInventory" ToolTip="Close Inventory" accessKey="X" tabIndex="4" CssClass="ms-toolbar" runat="server" onclick="cmdCloseInventory_Click">Close Inventory</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</td>					
					<td class="ms-toolbar" id="Td2" nowrap align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
    