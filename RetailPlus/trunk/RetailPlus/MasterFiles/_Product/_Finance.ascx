<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__Finance" Codebehind="_Finance.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/SalesAndReceivables.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblProductID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 10px" vAlign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<td class="ms-formspacer"></td>
								<td width="30%"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<td class="ms-formspacer"></td>
								<td style="HEIGHT: 70px" borderColor="white" align="left" width="40%" colspan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">
										<asp:label id="lblProductCode" runat="server"></asp:label></label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>
										Barcode:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Product 
										Description:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Current 
										Quantity:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBarcode" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblProductDesc" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%">
									<asp:label id="lblQuantity" runat="server" CssClass="ms-error"></asp:label>&nbsp;
									<asp:label id="lblUnitCode" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Supplier 
										Code:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Supplier 
										Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>
										Telephone no.:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSupplierID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lblSupplierCode" runat="server"></asp:HyperLink></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%">
									<asp:label id="lblSupplierContact" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%">
									<asp:label id="lblSupplierTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Product 
										Group (Department):</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="4"><label>Product 
										Sub-Group:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblProductGroup" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="3"><asp:label id="lblProductSubGroup" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1: Financial
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
                            Choose the account type to be use when buying this item.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
                            Choose account type to be use when selling this item.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">
                            Choose account type to be use for the inventory of this item.</div>
					</td>
					<td class="ms-colspace" style="HEIGHT: 67px">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>
                                    Select account type when buying or purchasing<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountPurchase" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="ms-error" ErrorMessage="'Account Type' must not be left blank." Display="Dynamic" ControlToValidate="cboChartOfAccountPurchase" ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>
                                    Select account type when selling this item<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountSold" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountSold" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>
                                    Select account type to be use for inventory of this item<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountInventory" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountInventory" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
								<td class="ms-authoringcontrols" width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 2: Link accounts for TAX Codes</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
                            Choose the TAX ACCOUNT TYPE to be use when buying this item.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
                            Choose the TAX ACCOUNT TYPE to be use when selling this item.</div>
					</td>
					<td class="ms-colspace" style="HEIGHT: 67px">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>
                                    Select TAX ACCOUNT TYPE when buying or purchasing<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountIDTaxPurchase" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" CssClass="ms-error" ErrorMessage="'Account Type' must not be left blank." Display="Dynamic" ControlToValidate="cboChartOfAccountPurchase" ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>
                                    Select TAX ACCOUNT TYPE when selling this item<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
                                    &nbsp;<asp:DropDownList ID="cboChartOfAccountIDTaxSold" runat="server" CssClass="ms-long">
                                    </asp:DropDownList>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ControlToValidate="cboChartOfAccountSold" Display="Dynamic" ErrorMessage="'Account Type' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
								<td class="ms-authoringcontrols" width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1">
						<table class="ms-toolbar" id="twotidGrpsTB" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
							<tr>
								<td class="ms-toolbar" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-toolbar">
									<table cellspacing="0" cellpadding="1" border="0">
										<tr>
										    <td class="ms-toolbar">
						                        <table cellspacing="0" cellpadding="1" border="0">
							                        <tr>
								                        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" title="Update Product" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Update Product" border="0" width="16" height="16" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								                        </td>
								                        <td nowrap="nowrap"><asp:linkbutton id="cmdSave" title="Update Product" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSave_Click">Save</asp:linkbutton></td>
							                        </tr>
						                        </table>
					                        </td>
					                        <td class="ms-separator">|</td>
					                        <td class="ms-toolbar">
						                        <table cellspacing="0" cellpadding="1" border="0">
							                        <tr>
								                        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" title="Update Product" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Update Product" border="0" width="16" height="16" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								                        </td>
								                        <td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" title="Update Product" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							                        </tr>
						                        </table>
					                        </td>
					                        <td class="ms-separator">|</td>
					                        <td class="ms-toolbar">
						                        <table cellspacing="0" cellpadding="1" border="0">
							                        <tr>
								                        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Updating Product" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Updating Product" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								                        <td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Updating Product" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							                        </tr>
						                        </table>
					                        </td>
											<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
