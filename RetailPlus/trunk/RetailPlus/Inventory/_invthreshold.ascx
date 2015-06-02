<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__InvThreshold" Codebehind="_invthreshold.ascx.cs" %>
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
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						
					</td>
                    <td class="ms-toolbar">
					</td>
					<td class="ms-toolbar" id="align01" nowrap="nowrap" align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
                                <td class="ms-toolbar">
									<table cellspacing="0" cellpadding="1" border="0">
										<tr>
											<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrint" title="Print this Inventory for Counting" accessKey="G" tabIndex="5" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../_layouts/images/print.gif" alt="Print this Inventory for Counting" border="0" width="16" height="16" OnClick="imgPrint_Click"></asp:imagebutton></td>
											<td nowrap="nowrap"><asp:linkbutton id="cmdPrint" title="Print this Inventory for Counting" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print Inventory Sheet</asp:linkbutton></td>
										</tr>
									</table>
								</td>
								<td class="ms-toolbar" nowrap="nowrap" align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
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
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; padding-bottom:	10px" colspan="3">
                        <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
					            <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
						            <label>Select Branch to process</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px" colspan="3">
						            <asp:dropdownlist id="cboBranch" CssClass="ms-short" runat="server" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" ></asp:dropdownlist>
					            </td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
					            </td>
					            <td width="99%" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
				            <tr>
					            <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
						            <label>Select Product Group to process</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
						                    <asp:dropdownlist id="cboProductSubGroup" CssClass="ms-long" runat="server" OnSelectedIndexChanged="cboProductSubGroup_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false"></asp:dropdownlist>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgProductSubGroupSearch" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
					            </td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtProductSubGroup" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgProductSubGroupSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand; width: 16px;" accessKey="P" 
                                        ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgProductSubGroupSearch_Click"></asp:imagebutton>
					            </td>
					            <td width="99%" id="Td5" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/empty.gif" /></td>
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
							                    <col width="20%">
                                                <col width="9%">
							                    <col width="6%" align="center">
                                                <col width="6%" align="center">
							                    <col width="6%" align="center">
							                    <col width="6%" align="center">
							                    <col width="6%" align="center">
							                    <col width="6%" align="center">
							                    <col width="6%" align="center">
                                                <col width="6%" align="center">
                                                <col width="6%" align="center">
							                    <col width="15">
						                    </colgroup>
						                    <tr style="padding-bottom: 4px">
							                    <th class="ms-vh2">
								                    <INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" visible="false" style="display:none">&nbsp;&nbsp;</th>
							                    <th class="ms-vh2"><asp:hyperlink id="SortByDescription" runat="server">Barcode</asp:hyperlink></th>
							                    <th class="ms-vh2"><asp:hyperlink id="SortByProductCode" runat="server">Product Code</asp:hyperlink></th>
                                                <th class="ms-vh2"><asp:hyperlink id="SortByVariationDesc" runat="server"></asp:hyperlink></th>
                                                <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByMinThreshold" runat="server">Main Prod. Min-Threshold</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByMaxThreshold" runat="server">Main Prod. Max-Threshold</asp:hyperlink></th>
                                                <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByBranchMinThreshold" runat="server">Min Threshold</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByBranchMaxThreshold" runat="server">Max Threshold</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByRIDBranch" runat="server">RID</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByRIDBranchMinThreshold" runat="server">Suggested Min-Threshold(RID)</asp:hyperlink></th>
						                        <th class="ms-vh2" style="text-align:center"><asp:hyperlink id="SortByRIDBranchMaxThreshold" runat="server">Suggested Max-Threshold(RID)</asp:hyperlink></th>
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
                                                <td class="ms-vb-user">
							                        <asp:TextBox ID="txtMinThreshold" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-disabled" Width="98%">0</asp:TextBox></td>
							                    <td class="ms-vb-user">
								                    <asp:TextBox ID="txtMaxThreshold" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-disabled" Width="98%">0</asp:TextBox></td>
                                                <td class="ms-vb2">
							                        <asp:TextBox ID="txtBranchMinThreshold" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric" Width="98%">0</asp:TextBox></td>								                    
							                    <td class="ms-vb2">
								                    <asp:TextBox ID="txtBranchMaxThreshold" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric" Width="98%">0</asp:TextBox></td>
							                    <td class="ms-vb-user">
							                        <asp:TextBox ID="txtRIDBranch" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric" Width="70%">0</asp:TextBox>
                                                    <asp:ImageButton id="imgSaveThresholds" runat="server" ImageUrl="../_layouts/images/saveitem.gif" ToolTip="Save Thresholds" CommandName="imgSaveThresholds" CausesValidation="false" height="15px"></asp:ImageButton></td>
							                    <td class="ms-vb-user">
							                        <asp:TextBox ID="txtRIDBranchMinThreshold" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-disabled" Width="98%">0</asp:TextBox></td>
							                    <td class="ms-vb-user">
								                    <asp:TextBox ID="txtRIDBranchMaxThreshold" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-disabled" Width="98%">0</asp:TextBox></td>
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
                            <asp:AsyncPostBackTrigger ControlID="cboBranch" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cboProductSubGroup" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
					</td>
				</tr>
			</table>
			
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
				    <td class="ms-toolbar" id="td1" nowrap="nowrap" align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="right"></td>
							</tr>
						</table>
					</td>
                    <td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgSaveThresholds" ToolTip="Save All Thresholds" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" onclick="imgSaveThresholds_Click" ImageUrl="../_layouts/images/saveitem.gif" alt="Save All Thresholds" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap><asp:linkbutton id="cmdSaveThresholds" ToolTip="Save All Thresholds" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveThresholds_Click">Save All Thresholds</asp:linkbutton></td>
							</tr>
						</table>
					</td>	
					<td class="ms-toolbar" id="Td2" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
    