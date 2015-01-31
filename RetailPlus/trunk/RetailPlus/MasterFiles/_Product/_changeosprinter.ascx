<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__ChangeOSPrinter" Codebehind="_changeosprinter.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/Inventory.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>

<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
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
											<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrint" title="Print this Inventory for Counting" accessKey="G" tabIndex="5" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../../_layouts/images/print.gif" alt="Print this Inventory for Counting" border="0" width="16" height="16" OnClick="imgPrint_Click" Visible="false"></asp:imagebutton></td>
											<td nowrap="nowrap"><asp:linkbutton id="cmdPrint" title="Print this Inventory for Counting" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click" Visible="false">Print Inventory Sheet</asp:linkbutton></td>
										</tr>
									</table>
								</td>
								<td class="ms-toolbar" nowrap="nowrap" align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
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
					            <td width="99%" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
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
						                    <asp:dropdownlist id="cboProductGroup" CssClass="ms-long" runat="server" OnSelectedIndexChanged="cboProductGroup_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false"></asp:dropdownlist>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgProductGroupSearch" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
					            </td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtProductGroup" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgProductGroupSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand; width: 16px;" accessKey="P" 
                                        ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgProductGroupSearch_Click"></asp:imagebutton>
					            </td>
					            <td width="99%" id="Td5" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
			                    <asp:datalist id="lstItem" runat="server" Width="100%" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				                    <HeaderTemplate>
					                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						                    <colgroup>
							                    <col width="10">
							                    <col width="15%">
							                    <col width="23%">
                                                <col width="12%">
							                    <col width="10%" align="center">
                                                <col width="10%" align="center">
							                    <col width="10%" align="center">
							                    <col width="10%" align="center">
							                    <col width="10%" align="center">
							                    <col width="15">
						                    </colgroup>
						                    <tr style="padding-bottom: 4px">
							                    <th class="ms-vh2"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" visible="false" style="display:none" />&nbsp;&nbsp;</th>
							                    <th class="ms-vh2"><asp:hyperlink id="SortByProductSubGroupCode" runat="server">Sub Group</asp:hyperlink></th>
							                    <th class="ms-vh2"><asp:hyperlink id="SortByProductCode" runat="server">Description</asp:hyperlink></th>
							                    <th class="ms-vh2" style="text-align:center">
                                                    <asp:hyperlink id="SortByOrderSlipPrinter1" runat="server">OSPrinter1</asp:hyperlink>
                                                    <br /><asp:CheckBox ID="chkOrderSlipPrinter1All" runat="server" OnCheckedChanged="chkOrderSlipPrinterAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" />
                                                </th>
							                    <th class="ms-vh2" style="text-align:center">
                                                    <asp:hyperlink id="SortByOrderSlipPrinter2" runat="server">OSPrinter2</asp:hyperlink>
                                                    <br /><asp:CheckBox ID="chkOrderSlipPrinter2All" runat="server" OnCheckedChanged="chkOrderSlipPrinterAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" />
                                                </th>
							                    <th class="ms-vh2" style="text-align:center">
                                                    <asp:hyperlink id="SortByOrderSlipPrinter3" runat="server">OSPrinter3</asp:hyperlink>
                                                    <br /><asp:CheckBox ID="chkOrderSlipPrinter3All" runat="server" OnCheckedChanged="chkOrderSlipPrinterAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" />
                                                </th>
						                        <th class="ms-vh2" style="text-align:center">
                                                    <asp:hyperlink id="SortByOrderSlipPrinter4" runat="server">OSPrinter4</asp:hyperlink>
                                                    <br /><asp:CheckBox ID="chkOrderSlipPrinter4All" runat="server" OnCheckedChanged="chkOrderSlipPrinterAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" />
                                                </th>
							                    <th class="ms-vh2" style="text-align:center">
                                                    <asp:hyperlink id="SortByOrderSlipPrinter5" runat="server">OSPrinter5</asp:hyperlink>
                                                    <br /><asp:CheckBox ID="chkOrderSlipPrinter5All" runat="server" OnCheckedChanged="chkOrderSlipPrinterAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" />
                                                </th>
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
							                        <asp:HyperLink id="lnkProductSubGroupCode" Runat="server" Target="_blank"></asp:HyperLink></td>
							                    <td class="ms-vb-user">
								                    &nbsp;&nbsp;<asp:HyperLink id="lnkBarcode" Runat="server" Target="_blank"></asp:HyperLink>
                                                    &nbsp;&nbsp;<asp:HyperLink id="lnkProductCode" Runat="server" Target="_blank"></asp:HyperLink>
							                    </td>
							                    <td class="ms-vb-user" align="center">
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chkOrderSlipPrinter1" runat="server" OnCheckedChanged="chkOrderSlipPrinter_CheckedChanged" AutoPostBack="true" CausesValidation="false" /></td>
							                    <td class="ms-vb-user" align="center">
							                        &nbsp;&nbsp;<asp:CheckBox ID="chkOrderSlipPrinter2" runat="server" OnCheckedChanged="chkOrderSlipPrinter_CheckedChanged" AutoPostBack="true" CausesValidation="false" /></td>
							                    <td class="ms-vb-user" align="center">
							                        &nbsp;&nbsp;<asp:CheckBox ID="chkOrderSlipPrinter3" runat="server" OnCheckedChanged="chkOrderSlipPrinter_CheckedChanged" AutoPostBack="true" CausesValidation="false" /></td>
							                    <td class="ms-vb-user" align="center">
								                    &nbsp;&nbsp;<asp:CheckBox ID="chkOrderSlipPrinter4" runat="server" OnCheckedChanged="chkOrderSlipPrinter_CheckedChanged" AutoPostBack="true" CausesValidation="false" /></td>
							                    <td class="ms-vb-user" align="center">
								                    &nbsp;&nbsp;<asp:CheckBox ID="chkOrderSlipPrinter5" runat="server" OnCheckedChanged="chkOrderSlipPrinter_CheckedChanged" AutoPostBack="true" CausesValidation="false" /></td>
							                    <td class="ms-vb-user"></td>
						                    </tr>
				                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
			                    </asp:datalist>
			                </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
					</td>
				</tr>
			</table>
			
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
    