<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.PurchasesAndPayables._PO.__eList" Codebehind="_elist.ascx.cs" %>
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
			<table class="ms-toolbar" style="margin-left: 0px" cellspacing="0" cellpadding="2" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel eSales List and Back To PO List" accessKey="C" tabIndex="3" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Adding New Item And Back To PO List" border="0" width="16" height="16" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel eSales List And Back To PO List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To PO List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align01" nowrap="nowrap" align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="right">
								    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label>
							                <asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
									            <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
								            </asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label>
								        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Order Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtOrderStartDate"></asp:CompareValidator><asp:Label
                ID="lblStatus" runat="server" CssClass="ms-error" Visible="False"></asp:Label></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Order End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtOrderEndDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:CompareValidator id="CompareValidator3" CssClass="ms-error" runat="server" ErrorMessage="'Posting Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPostingStartDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:CompareValidator id="CompareValidator4" CssClass="ms-error" runat="server" ErrorMessage="'Posting End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtPostingEndDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>
                            Order Start &nbsp;Date</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtOrderStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
						<asp:TextBox id="txtOrderStartTime" accessKey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">00:00</asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>
                            Order End Date</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtOrderEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
						<asp:TextBox id="txtOrderEndTime" accessKey="M" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">23:59</asp:TextBox>
					</td>
					<td width="99%" id="align05" nowrap="nowrap" align="left">&nbsp;
						<asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label2" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>
                            Posting Start &nbsp;Date</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtPostingStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
						<asp:TextBox id="txtPostingStartTime" accessKey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">00:00</asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>
                            Posting End Date</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtPostingEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
						<asp:TextBox id="txtPostingEndTime" accessKey="M" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">23:59</asp:TextBox>
					</td>
					<td width="99%" id="Td1" nowrap="nowrap" align="left">&nbsp;
						<asp:Label id="Label4" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label5" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>PO No /Vendor /Remarks</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap" colspan=4>
						<asp:TextBox id="txtSearch" CssClass="ms-long" Width="100%" runat="server" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td nowrap="nowrap">
						<asp:DropDownList id="cboStatus" CssClass="ms-short" Width="100%" runat="server"></asp:DropDownList>
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
		<td style="PADDING-TOP:	10px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstItem" runat="server" Width="100%" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand" AlternatingItemStyle-CssClass="ms-alternating">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
							        <col width="10">
							        <col width="5%">
							        <col width="16%">
							        <col width="14%">
							        <col width="21%">
							        <col width="14%">
							        <col width="15%">
							        <col width="15%" align="right">
							        <col width="1">
						        </colgroup>
						        <tr>
							        <th class="ms-vh2" style="padding-bottom: 4px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" style="visibility:hidden" /></th>
								    <th class="ms-vh2" style="padding-bottom: 4px;" align="center">
                                        <asp:hyperlink id="SortByIncludeIneSales" runat="server">In eSales</asp:hyperlink>
                                        <br /><asp:CheckBox ID="chkIncludeIneSalesAll" runat="server" OnCheckedChanged="chkIncludeIneSalesAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" />
                                    </th>
							        <th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByPONo" runat="server">PO No.</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByPODate" runat="server">Order Date</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortBySupplierCode" runat="server">Vendor/Supplier</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByReqDeliveryDate" runat="server">Req Delivery Date</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByBranchCode" runat="server">Deliver to Branch</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px; text-align:right"><asp:hyperlink id="SortByPOSubTotal" runat="server">Amount</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"></th>
						        </tr>
				        </HeaderTemplate>
				        <ItemTemplate>
						        <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
							        <td class="ms-vb-user">
								        <input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
							        </td>
						            <td class="ms-vb2">
							            &nbsp;&nbsp;<asp:CheckBox ID="chkIncludeIneSales" runat="server" OnCheckedChanged="chkIncludeIneSales_CheckedChanged" AutoPostBack="true" CausesValidation="false" />
						            </td>
							        <td class="ms-vb-user">
								        <asp:HyperLink id="lnkPONo" runat="server"></asp:HyperLink>
							        </td>
							        <td class="ms-vb-user">
								        <asp:Label ID="lblPODate" Runat="server"></asp:Label>
							        </td>
							        <td class="ms-vb-user">
								        <asp:HyperLink ID="lblSupplierCode" Runat="server" Target="_blank"></asp:HyperLink>
								        <asp:Label ID="lblSupplierID" Runat="server" Visible="False"></asp:Label>
							        </td>
							        <td class="ms-vb-user">
								        <asp:Label ID="lblReqDeliveryDate" Runat="server"></asp:Label>
							        </td>
							        <td class="ms-vb-user">
								        <asp:Label ID="lblBranchCode" Runat="server"></asp:Label>
								        <asp:Label ID="lblBranchID" Runat="server" Visible="False"></asp:Label>
							        </td>
							        <td class="ms-vb-user" style="text-align:right">
								        <asp:Label ID="lblPOSubTotal" Runat="server"></asp:Label>
							        </td>
							        
							        <td class="ms-vb2">
								        <A class="DropDown" id="anchorDown" href="" runat="server">
									        <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="true"></asp:Image></A>
							        </td>
						        </tr>
						        <tr>
					                <td class="ms-vh2" height="1" colspan=4><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
					                <td colspan="5" height="1">
						                <div class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
							                <asp:panel id="panItem" runat="server" Width="100%" Height="100%" >
								                <table id="tblpanItem" cellspacing="0" cellpadding="0" width="100%" border="0">
									                <tr>
										                <td class="ms-formspacer" colspan="1"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
									                </tr>
									                <tr>
										                <td width="10%">
											                <asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>Remarks</b>"></asp:Label>
										                </td>
										                <td width="1%">
											                <asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
										                </td>
										                <td width="80%">
								                            <asp:Label ID="lblPORemarks" CssClass="ms-vh2" Runat="server"></asp:Label>
										                </td>
									                </tr>
								                </table>
							                </asp:panel></DIV>
					                </td>
					                <td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
				                </tr>
				        </ItemTemplate>
                        <FooterTemplate>
					        </table>
                        </FooterTemplate>
			        </asp:datalist>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboCurrentPage" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
			</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3" style="padding-bottom: 10px"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
