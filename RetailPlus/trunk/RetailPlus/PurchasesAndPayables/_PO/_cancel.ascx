<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.PurchasesAndPayables._PO.__Cancel" Codebehind="_Cancel.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
                        <asp:UpdatePanel ID="updPrint" runat="server">
                            <ContentTemplate>
						        <table cellspacing="0" cellpadding="1" border="0">
							        <tr>
								        <td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrint" title="Print this Purchase Order" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Purchase Order" ImageUrl="../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgPrint_Click"></asp:imagebutton></td>
								        <td noWrap><asp:linkbutton id="cmdPrint" title="Print this Purchase Order" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</td>
					<td class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Back To PO List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Back To PO List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Back To PO List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To PO List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" colSpan="3"><asp:label id="lblPOID" runat="server" Visible="False"></asp:label>
						<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 10px" valign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<td class="ms-formspacer"></td>
								<td width="30%" rowSpan="4"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<td class="ms-formspacer"></td>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Purchase 
										Order</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Purchase Order no:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblPONo" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer" style="HEIGHT: 52px"></td>
								<td class="ms-formspacer" style="HEIGHT: 52px"></td>
								<td style="PADDING-BOTTOM: 2px; HEIGHT: 52px" valign="top" width="30%" colSpan="2"><label>Date 
										Prepared: </label>
									<asp:label id="lblPODate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Supplier 
										Name:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="2"><label>Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Terms:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSupplierID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lblSupplierCode" runat="server" Target="_blank">lblSupplierCode</asp:HyperLink></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblSupplierContact" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblTerms" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblModeOfterms" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Supplier 
										Address:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="2"><label>Telephone 
										no:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Required 
										Delivery Date:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSupplierAddress" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblSupplierTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblRequiredDeliveryDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Deliver 
										to branch: (Specify complete address)</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="4"><label>Branch 
										Address:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBranchCode" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblBranchID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="3"><asp:label id="lblBranchAddress" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="6"><label>PO 
										Remarks:</label><asp:label id="lblPORemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
            <asp:DataList ID="lstItem" runat="server" CellPadding="0" Width="100%" OnItemDataBound="lstItem_ItemDataBound">
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
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"><asp:HyperLink ID="SortByDescription" runat="server">Description</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"><asp:HyperLink ID="SortByMatrixDescription" runat="server">Matrix Desc.</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px; text-align:right"><asp:HyperLink ID="SortByQuantity" runat="server">Quantity</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"><asp:HyperLink ID="SortByProductUnitCode" runat="server">Unit of Measure</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px; text-align:right"><asp:HyperLink ID="SortByUntCost" runat="server">Unit Cost</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px; text-align:right"><asp:HyperLink ID="SortByDiscount" runat="server">Discount</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px; text-align:right"><asp:HyperLink ID="SortByAmount" runat="server">Total Cost</asp:HyperLink></th>
                            <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                        <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
                            <td class="ms-vb-user">
                                <input id="chkList" runat="server" name="chkList" type="checkbox" visible="false" />
                            </td>
                            <td class="ms-vb-user">
                                <asp:HyperLink ID="lnkDescription" runat="server" Target="_blank"></asp:HyperLink>
                            </td>
                            <td class="ms-vb-user">
                                <asp:HyperLink ID="lnkMatrixDescription" runat="server" Target="_blank"></asp:HyperLink>
                            </td>
                            <td class="ms-vb-user" style="text-align: right">
                                <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td class="ms-vb-user">
                                <asp:Label ID="lblProductUnitID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblProductUnitCode" runat="server"></asp:Label>
                            </td>
                            <td class="ms-vb-user"  style="text-align: right">
                                <asp:Label ID="lblUnitCost" runat="server"></asp:Label>
                            </td>
                            <td class="ms-vb-user" style="text-align: right">
                                <asp:Label ID="lblDiscountApplied" runat="server"></asp:Label>
                                <asp:Label ID="lblPercent" runat="server" Visible="False">%</asp:Label>
                            </td>
                            <td class="ms-vb-user" style="text-align: right">
                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            </td>
                            <td class="ms-vb2">
                                <a id="anchorDown" runat="server" class="DropDown" href="">
                                    <asp:Image ID="divExpCollAsst_img" runat="server" alt="Show" ImageUrl="../../_layouts/images/DLMAX.gif" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="ms-vh2" height="1">
                                <img alt="" height="5" src="../../_layouts/images/blank.gif" width="1" /></td>
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
                                <img alt="" height="5" src="../../_layouts/images/blank.gif" width="1" /></td>
                        </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:DataList></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td valign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 15px">
		                        <td class="ms-formspacer"></td>
		                        <td width="50%"></td>
		                        <td align="left"><label>   &nbsp; &nbsp; <b>Total Before Discount:</b></label></td>
		                        <td align="right"><asp:label id="lblTotalDiscount1" runat="server" CssClass="ms-error">0.00</asp:label></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: auto">
		                        <td class="ms-formspacer"></td>
		                        <td></td>
		                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount (1):</label></td>
		                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODiscountApplied" accessKey="" runat="server" CssClass="ms-short-numeric-disabled" BorderStyle="Groove" Text=0 Width="82px"></asp:textbox><asp:dropdownlist id="cboPODiscountType" runat="server" CssClass="ms-short-disabled">
                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                    <asp:ListItem Value="1">amt</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                </asp:dropdownlist>
                                <asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ControlToValidate="txtPODiscountApplied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: 15px">
		                        <td class="ms-formspacer"></td>
		                        <td></td>
		                        <td align="left"><label>   &nbsp; &nbsp; Subtotal Discount (1):</label></td>
		                        <td align="right"><asp:label id="lblPODiscount" runat="server" CssClass="ms-error">0.00</asp:label></td>
	                        </tr>
	                        <tr>
		                        <td></td>
		                        <td></td>
		                        <td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: 15px">
		                        <td class="ms-formspacer"></td>
		                        <td width="50%"></td>
		                        <td align="left"><label>   &nbsp; &nbsp; <b>Total Before Discount:</b></label></td>
		                        <td align="right"><asp:label id="lblTotalDiscount2" runat="server" CssClass="ms-error">0.00</asp:label></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: auto">
		                        <td class="ms-formspacer"></td>
		                        <td></td>
		                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount (2):</label></td>
		                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODiscount2Applied" accessKey="" runat="server" CssClass="ms-short-numeric-disabled" BorderStyle="Groove" Text=0 Width="82px"></asp:textbox><asp:dropdownlist id="cboPODiscount2Type" runat="server" CssClass="ms-short-disabled">
                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                    <asp:ListItem Value="1">amt</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                </asp:dropdownlist>
                                <asp:requiredfieldvalidator id="Requiredfieldvalidator16" runat="server" CssClass="ms-error" ControlToValidate="txtPODiscount2Applied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: 15px">
		                        <td class="ms-formspacer"></td>
		                        <td width="50%"></td>
		                        <td align="left"><label>   &nbsp; &nbsp; Subtotal Discount (2):</b></label></td>
		                        <td align="right"><asp:label id="lblPODiscount2" runat="server" CssClass="ms-error">0.00</asp:label></td>
	                        </tr>
	                        <tr>
		                        <td></td>
		                        <td></td>
		                        <td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: 15px">
		                        <td class="ms-formspacer"></td>
		                        <td width="50%"></td>
		                        <td align="left"><label>   &nbsp; &nbsp; <b>Total Before Discount:</b></label></td>
		                        <td align="right"><asp:label id="lblTotalDiscount3" runat="server" CssClass="ms-error">0.00</asp:label></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: auto">
		                        <td class="ms-formspacer"></td>
		                        <td></td>
		                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount (3):</label></td>
		                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODiscount3Applied" accessKey="" runat="server" CssClass="ms-short-numeric-disabled" BorderStyle="Groove" Text=0 Width="82px"></asp:textbox><asp:dropdownlist id="cboPODiscount3Type" runat="server" CssClass="ms-short-disabled">
                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                    <asp:ListItem Value="1">amt</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                </asp:dropdownlist>
                                <asp:requiredfieldvalidator id="Requiredfieldvalidator17" runat="server" CssClass="ms-error" ControlToValidate="txtPODiscount3Applied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
	                        </tr>
	                        <tr style="PADDING-BOTTOM: 25px">
		                        <td class="ms-formspacer"></td>
		                        <td width="50%"></td>
		                        <td align="left"><label>   &nbsp; &nbsp; Subtotal Discount (3):</label></td>
		                        <td align="right"><asp:label id="lblPODiscount3" runat="server" CssClass="ms-error">0.00</asp:label></td>
	                        </tr>
	                        <tr>
		                        <td class="ms-formspacer"></td>
		                        <td></td>
		                        <td class="ms-sectionline" colSpan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
	                        </tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label><b>VATable Amount:</b></label><asp:CheckBox ID="chkIsVatInclusive" runat="server" Checked="true" Text="(Is-Vat-Inclusive)" Enabled="false"/></td>
								<td align="right"><asp:label id="lblPOVatableAmount" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Subtotal:</b></label></td>
								<td align="right"><asp:label id="lblPOSubTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label><b>VAT:</b></label></td>
								<td align="right"><asp:label id="lblPOVAT" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Freight:</b></label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtPOFreight" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" Enabled="False"></asp:textbox>
								                                            </td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label><b>PO Deposit:</b></label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODeposit" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" Enabled="False"></asp:textbox>
								                                               </td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
								<td></td>
								<td class="ms-sectionline" colSpan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Total:</b></label></td>
								<td align="right"><asp:label id="lblPOTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td id="AddUserTextTDID2">
			<table class="ms-toolbar" id="onetidGrpsTC" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
			<table class="ms-toolbar" id="onetidGrpsTC" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar" align="left" noWrap width="99%">
						<table cellspacing="0" cellpadding="1" border="0" width="99%">
							<tr>
								<td class="ms-toolbar" noWrap>Remarks :</td>
								<td noWrap width="99%">
                                    <asp:Label ID="Label1" runat="server" CssClass="ms-error">Enter remarks to of cancellation. Remarks should not be blank.</asp:Label><br />
                                    <asp:textbox id="txtRemarks" accessKey="Q" runat="server" CssClass="ms-long" BorderStyle="Groove" Width="100%" TextMode="MultiLine" Rows="5" BorderColor="Red"></asp:textbox></td>
							</tr>
						</table>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ControlToValidate="txtRemarks" Display="Dynamic" ErrorMessage="'Remarks' must not be left blank."></asp:requiredfieldvalidator>
					</td>
					<td class="ms-separator"><asp:label id="Label16" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
                        <asp:UpdatePanel ID="updCancel" runat="server">
                            <ContentTemplate>
						        <table cellspacing="0" cellpadding="1" border="0">
							        <tr>
								        <td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancelPO" title="Cancel This Purchase Order" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/cancel.gif" alt="Cancel this purchase order" border="0" width="16" height="16" OnClick="imgCancelPO_Click"></asp:imagebutton></td>
								        <td noWrap><asp:linkbutton id="cmdCancelPO" title="Cancel this purchase order" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdCancelPO_Click">Cancel PO</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</td>
					<td class="ms-toolbar" id="align052" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><a name="cancelsection"></a><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
</table>
