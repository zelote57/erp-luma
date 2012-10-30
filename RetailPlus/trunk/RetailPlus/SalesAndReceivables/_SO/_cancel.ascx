<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.SalesAndReceivables._SO.__Cancel" Codebehind="_Cancel.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrint" ToolTip="Print this Sales Order" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Sales Order" ImageUrl="../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdPrint" ToolTip="Print this Sales Order" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click" CausesValidation="False">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrintQuotation" ToolTip="Print this Quotation Order" accessKey="G" tabIndex="5" height="16" width="16" border="0" ImageUrl="../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrintQuotation_Click" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdPrintQuotation" ToolTip="Print this Quotation Order" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" OnClick="cmdPrintQuotation_Click" CausesValidation="False">Print Quotation</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Back To SO List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Back To SO List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Back To SO List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To SO List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" colspan="3"><asp:label id="lblSOID" runat="server" Visible="False"></asp:label>
						<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td style="PADDING-BOTTOM: 10px" vAlign="top" colspan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%" rowSpan="4"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Sales 
										Order</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Sales Order no:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSONo" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer" style="HEIGHT: 52px"></TD>
								<TD class="ms-formspacer" style="HEIGHT: 52px"></TD>
								<td style="PADDING-BOTTOM: 2px; HEIGHT: 52px" vAlign="top" width="30%" colspan="2"><label>Date 
										Prepared: </label>
									<asp:label id="lblSODate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Customer 
										Name:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Terms:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblCustomerID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lblCustomerCode" runat="server">lblCustomerCode</asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblCustomerContact" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblTerms" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblModeOfterms" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Customer 
										Address:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Telephone 
										no:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Required 
										Delivery Date:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblCustomerAddress" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblCustomerTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblRequiredDeliveryDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Deliver 
										to branch: (Specify complete address)</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="4"><label>Branch 
										Address:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBranchCode" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblBranchID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="3"><asp:label id="lblBranchAddress" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="6"><label>SO 
										Remarks:</label><asp:label id="lblSORemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
            <asp:DataList ID="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound">
                <HeaderTemplate>
                    <table id="tblHeaderTemplate" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <colgroup>
                            <col width="10">
                            <col width="20%">
                            <col width="20%">
                            <col width="12%">
                            <col width="12%">
                            <col width="12%">
                            <col width="12%">
                            <col width="12%">
                            <col width="10">
                        </colgroup>
                        <tr>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">                                
                            </th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByDescription" runat="server">Description</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByMatrixDescription" runat="server">Matrix Desc.</asp:HyperLink></th>
                            <th align="right" class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByQuantity" runat="server">Quantity</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByProductUnitCode" runat="server">Unit of Measure</asp:HyperLink></th>
                            <th align="right" class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByUntCost" runat="server">Unit Cost</asp:HyperLink></th>
                            <th align="right" class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByDiscount" runat="server">Discount</asp:HyperLink></th>
                            <th align="right" class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                                <asp:HyperLink ID="SortByAmount" runat="server">Total Cost</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
                            </th>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table id="tblItemTemplate" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <colgroup>
                            <col width="10">
                            <col width="20%">
                            <col width="20%">
                            <col width="12%">
                            <col width="12%">
                            <col width="12%">
                            <col width="12%">
                            <col width="12%">
                            <col width="10">
                        </colgroup>
                        <tr>
                            <td class="ms-vb-user">
                                <input id="chkList" runat="server" name="chkList" type="checkbox" visible=false>
                            </td>
                            <td class="ms-vb-user">
                                <asp:HyperLink ID="lnkDescription" runat="server"></asp:HyperLink>
                            </td>
                            <td class="ms-vb-user">
                                <asp:HyperLink ID="lnkMatrixDescription" runat="server"></asp:HyperLink>
                            </td>
                            <td align="right" class="ms-vb-user">
                                <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td class="ms-vb-user">
                                <asp:Label ID="lblProductUnitID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblProductUnitCode" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="ms-vb-user">
                                <asp:Label ID="lblUnitCost" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="ms-vb-user">
                                <asp:Label ID="lblDiscountApplied" runat="server"></asp:Label>
                                <asp:Label ID="lblPercent" runat="server" Visible="False">%</asp:Label>
                            </td>
                            <td align="right" class="ms-vb-user">
                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            </td>
                            <td class="ms-vb2">
                                <a id="anchorDown" runat="server" class="DropDown" href="">
                                    <asp:Image ID="divExpCollAsst_img" runat="server" alt="Show" ImageUrl="../../_layouts/images/DLMAX.gif" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="ms-vh2" height="1">
                                <img alt="" height="5" src="../../_layouts/images/blank.gif" width="1"></td>
                            <td colspan="7" height="1">
                                <div id="divExpCollAsst" runat="server" border="0" class="ACECollapsed">
                                    <asp:Panel ID="panItem" runat="server" CssClass="ms-authoringcontrols" Height="100%"
                                        Width="100%">
                                        <table id="tblpanItem" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="ms-formspacer" colspan="1">
                                                    <img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
                                            </tr>
                                            <tr>
                                                <td width="19%">
                                                    <asp:Label ID="Label4" runat="server" CssClass="ms-vh2" Text="<b>VAT</b>"></asp:Label>
                                                </td>
                                                <td width="1%">
                                                    <asp:Label ID="Label7" runat="server" CssClass="ms-vh2" Text="<b>:</b>"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:Label ID="lblVAT" runat="server" CssClass="ms-vb2"></asp:Label>
                                                </td>
                                                <td width="19%">
                                                    <asp:Label ID="Label5" runat="server" CssClass="ms-vh2" Text="<b>eVAT</b>"></asp:Label>
                                                </td>
                                                <td width="1%">
                                                    <asp:Label ID="Label9" runat="server" CssClass="ms-vh2" Text="<b>:</b>"></asp:Label>
                                                </td>
                                                <td width="40%">
                                                    <asp:Label ID="lblEVAT" runat="server" CssClass="ms-vb2"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="19%">
                                                    <asp:Label ID="Label6" runat="server" CssClass="ms-vh2" Text="<b>Local Tax</b>"></asp:Label>
                                                </td>
                                                <td width="1%">
                                                    <asp:Label ID="Label11" runat="server" CssClass="ms-vh2" Text="<b>:</b>"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:Label ID="lblLocalTax" runat="server" CssClass="ms-vb2"></asp:Label>
                                                </td>
                                                <td width="19%">
                                                    <asp:Label ID="Label13" runat="server" CssClass="ms-vh2" Text="<b>VAT Inclusive</b>"></asp:Label>
                                                </td>
                                                <td width="1%">
                                                    <asp:Label ID="Label14" runat="server" CssClass="ms-vh2" Text="<b></b>"></asp:Label>
                                                </td>
                                                <td width="40%">
                                                    <asp:Label ID="lblisVATInclusive" runat="server" CssClass="ms-vb2"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="19%">
                                                    <asp:Label ID="Label8" runat="server" CssClass="ms-vh2" Text="<b>Remarks</b>"></asp:Label>
                                                </td>
                                                <td width="1%">
                                                    <asp:Label ID="Label10" runat="server" CssClass="ms-vh2" Text="<b>:</b>"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <asp:Label ID="lblRemarks" runat="server" CssClass="ms-vb2"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </td>
                            <td class="ms-vh2" height="1">
                                <img alt="" height="5" src="../../_layouts/images/blank.gif" width="1"></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td vAlign="top" colspan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: auto">
								<TD class="ms-formspacer"></TD>
								<TD></TD>
								<td align="left"><label>   &nbsp; &nbsp; Applicable Discount:</label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtSODiscountApplied" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text=0 Width="82px" Enabled="False"></asp:textbox><asp:dropdownlist id="cboSODiscountType" runat="server" CssClass="ms-short" Enabled="False">
                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                    <asp:ListItem Value="1">amt</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                </asp:dropdownlist>
                                </td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD width="50%"></TD>
								<td align="left"><label><b>Subtotal Discount:</b></label></td>
								<td align="right"><asp:label id="lblSODiscount" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD></TD>
								<td align="left"><label><b>VATable Amount:</b></label></td>
								<td align="right"><asp:label id="lblSOVatableAmount" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD width="50%"></TD>
								<td align="left"><label><b>Subtotal:</b></label></td>
								<td align="right"><asp:label id="lblSOSubTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD></TD>
								<td align="left"><label><b>VAT:</b></label></td>
								<td align="right"><asp:label id="lblSOVAT" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD width="50%"></TD>
								<td align="left"><label><b>Freight:</b></label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtSOFreight" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" Enabled="False"></asp:textbox>
								                                            </td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD></TD>
								<td align="left"><label><b>SO Deposit:</b></label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtSODeposit" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" Enabled="False"></asp:textbox>
								                                               </td>
							</tr>
							<tr>
								<TD class="ms-formspacer"></TD>
								<TD></TD>
								<td class="ms-sectionline" colspan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD width="50%"></TD>
								<td align="left"><label><b>Total:</b></label></td>
								<td align="right"><asp:label id="lblSOTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</TD>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD id="AddUserTextTDID2">
			<table class="ms-toolbar" id="onetidGrpsTC" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar" align="left" noWrap width="99%">
						<table cellSpacing="0" cellPadding="1" border="0" width="99%">
							<tr>
								<td class="ms-toolbar" noWrap>Remarks :</td>
								<td noWrap width="99%">
                                    <asp:Label ID="Label1" runat="server" CssClass="ms-error">Enter remarks to of cancellation. Remarks should not be blank.</asp:Label><br />
                                    <asp:textbox id="txtRemarks" accessKey="Q" runat="server" CssClass="ms-long" BorderStyle="Groove" Width="100%" TextMode="MultiLine" Rows="5" BorderColor="Red"></asp:textbox></td>
							</tr>
						</table>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ControlToValidate="txtRemarks" Display="Dynamic" ErrorMessage="'Remarks' must not be left blank."></asp:requiredfieldvalidator>
					</td>
					<TD class="ms-separator"><asp:label id="Label16" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancelSO" title="Cancel This Sales Order" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/cancel.gif" alt="Cancel this purchase order" border="0" width="16" height="16" OnClick="imgCancelSO_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancelSO" title="Cancel this purchase order" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdCancelSO_Click">Cancel SO</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align052" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><a name="cancelsection"></a><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
</table>
