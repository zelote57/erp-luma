<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._TransferIn.__Cancel" Codebehind="_Cancel.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<asp:UpdatePanel ID="updPrint" runat="server">
                            <ContentTemplate>
						        <table cellspacing="0" cellpadding="1" border="0">
							        <tr>
								        <td class="ms-toolbar" nowrap><asp:imagebutton id="imgPrint" title="Print this TransferIn Order" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this TransferIn Order" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click"></asp:imagebutton></td>
								        <td nowrap><asp:linkbutton id="cmdPrint" title="Print this TransferIn Order" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</td>
					<td class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgCancel" title="Back To TransferIn List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Back To TransferIn List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap><asp:linkbutton id="cmdCancel" title="Back To TransferIn List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To TransferIn List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
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
					<td class="ms-descriptiontext" colspan="3"><asp:label id="lblTransferInID" runat="server" Visible="False"></asp:label>
						<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 10px" valign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="30%" rowspan="4"><img alt="" src="../../_layouts/images/company_logo.gif" /></td>
								<td class="ms-formspacer"></td>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowspan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">TransferIn 
										Order</label></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"><label>TransferIn Order no:</label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="padding-bottom: 2px" width="30%"><asp:label id="lblTransferInNo" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer" style="HEIGHT: 52px"></td>
								<td class="ms-formspacer" style="HEIGHT: 52px"></td>
								<td style="padding-bottom: 2px; HEIGHT: 52px" valign="top" width="30%" colspan="2"><label>Date 
										Prepared: </label>
									<asp:label id="lblTransferInDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="padding-bottom: 20px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Supplier 
										Name:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="2"><label>Contact:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Terms:</label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblSupplierID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lblSupplierCode" runat="server" Target="_blank">lblSupplierCode</asp:HyperLink></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%"><asp:label id="lblSupplierContact" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblTerms" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblModeOfterms" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Supplier 
										Address:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="2"><label>Telephone 
										no:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Required 
										Delivery Date:</label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblSupplierAddress" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%"><asp:label id="lblSupplierTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblRequiredDeliveryDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2">
                                    <label>Transfer IN to branch: (Specify complete address)</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="4"><label>Branch 
										Address:</label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblBranchCode" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblBranchID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="3"><asp:label id="lblBranchAddress" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label>TransferIn 
										Remarks:</label><asp:label id="lblTransferInRemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
            <asp:DataList ID="lstItem" runat="server" cellpadding="0" Width="100%" OnItemDataBound="lstItem_ItemDataBound">
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
                            <th class="ms-vh2" style="padding-bottom:4px; padding-top:4px;"></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px;"><asp:hyperlink id="SortByDescription" runat="server">Description</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px;"><asp:hyperlink id="SortByMatrixDescription" runat="server">Matrix Desc.</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px; text-align:right"><asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px;"><asp:hyperlink id="SortByProductUnitCode" runat="server">Unit of Measure</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px; text-align:right"><asp:hyperlink id="SortByUntCost" runat="server">Unit Cost</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px; text-align:right"><asp:hyperlink id="SortByDiscount" runat="server">Discount</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px; text-align:right"><asp:hyperlink id="SortByAmount" runat="server">Total Cost</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom:4px; padding-top:4px;"></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                        <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
                            <td class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
							</td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkDescription" Runat="server" Target="_blank"></asp:HyperLink>
							</td>
							<td class="ms-vb-user">
								<asp:HyperLink ID="lnkMatrixDescription" Runat="server" Target="_blank"></asp:HyperLink>
							</td>
							<td class="ms-vb-user" style="text-align:right">
								<asp:Label ID="lblQuantity" Runat="server"></asp:Label>&nbsp;&nbsp;
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblProductUnitID" Runat="server" Visible="False"></asp:Label>
								<asp:Label ID="lblProductUnitCode" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user" style="text-align:right">
								<asp:Label ID="lblUnitCost" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user" style="text-align:right">
								<asp:Label ID="lblDiscountApplied" Runat="server"></asp:Label>
								<asp:Label ID="lblPercent" Runat="server" Visible="False">%</asp:Label>
							</td>
							<td class="ms-vb-user" style="text-align:right">
								<asp:Label ID="lblAmount" Runat="server"></asp:Label>
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
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td valign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: auto">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label>   &nbsp; &nbsp; Applicable Discount:</label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtTransferInDiscountApplied" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text=0 Width="82px" Enabled="False"></asp:textbox><asp:dropdownlist id="cboTransferInDiscountType" runat="server" CssClass="ms-short" Enabled="False">
                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                    <asp:ListItem Value="1">amt</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                </asp:dropdownlist>
                                </td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Subtotal Discount:</b></label></td>
								<td align="right"><asp:label id="lblTransferInDiscount" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label><b>VATable Amount:</b></label></td>
								<td align="right"><asp:label id="lblTransferInVatableAmount" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Subtotal:</b></label></td>
								<td align="right"><asp:label id="lblTransferInSubTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label><b>VAT:</b></label></td>
								<td align="right"><asp:label id="lblTransferInVAT" runat="server" CssClass="ms-error">0.00</asp:label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Freight:</b></label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtTransferInFreight" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" Enabled="False"></asp:textbox>
								                                            </td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td></td>
								<td align="left"><label><b>TransferIn Deposit:</b></label></td>
								<td align="right"><asp:textbox onkeypress="AllNum()" id="txtTransferInDeposit" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" Enabled="False"></asp:textbox>
								                                               </td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
								<td></td>
								<td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td width="50%"></td>
								<td align="left"><label><b>Total:</b></label></td>
								<td align="right"><asp:label id="lblTransferInTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
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
			<table class="ms-toolbar" id="onetidGrpsTC" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar" align="left" nowrap="nowrap" width="99%">
						<table cellspacing="0" cellpadding="1" border="0" width="99%">
							<tr>
								<td class="ms-toolbar" nowrap>Remarks :</td>
								<td nowrap="nowrap" width="99%">
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
								        <td class="ms-toolbar" nowrap><asp:imagebutton id="imgCancelTransferIn" title="Cancel This TransferIn Order" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Cancel this purchase order" border="0" width="16" height="16" OnClick="imgCancelTransferIn_Click"></asp:imagebutton></td>
								        <td nowrap><asp:linkbutton id="cmdCancelTransferIn" title="Cancel this purchase order" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdCancelTransferIn_Click">Cancel TransferIn</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</td>
					<td class="ms-toolbar" id="align052" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><a name="cancelsection"></a><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
</table>
