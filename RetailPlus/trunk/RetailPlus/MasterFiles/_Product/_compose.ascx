<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__Compose" Codebehind="_Compose.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/SalesAndReceivables.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblProductID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td style="PADDING-BOTTOM: 10px" vAlign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="left" width="40%" colspan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">
										<asp:label id="lblProductCode" runat="server"></asp:label></label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>
										Barcode:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="2"><label>Product 
										Description:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Current 
										Quantity:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBarcode" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblProductDesc" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%">
									<asp:label id="lblQuantity" runat="server" CssClass="ms-error"></asp:label>&nbsp;
									<asp:label id="lblUnitCode" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Supplier 
										Code:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="2"><label>Supplier 
										Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>
										Telephone no.:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSupplierID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lblSupplierCode" runat="server"></asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%">
									<asp:label id="lblSupplierContact" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%">
									<asp:label id="lblSupplierTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Product 
										Group (Department):</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="4"><label>Product 
										Sub-Group:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblProductGroup" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="3"><asp:label id="lblProductSubGroup" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 4px" vAlign="top" colSpan="3">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1: Add Item 
							Composition</div>
					</td>
				</tr>
				<TR>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Select 
										Product Code<font color="red">*
											<asp:label id="lblCompositionID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Select 
										Variation<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Enter 
										Quantity<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label> unit<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols"><asp:dropdownlist id="cboProductCode" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboProductCode_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols"><asp:dropdownlist id="cboVariation" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboVariation_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Variation' must not be left blank." Display="Dynamic" ControlToValidate="cboVariation"></asp:requiredfieldvalidator></td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="80"><asp:textbox onkeypress="AllNum()" id="txtQuantity" onkeyup="ComputeAmountAddItem()" accessKey="Q" runat="server" CssClass="ms-short" BorderStyle="Groove">1</asp:textbox>
                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Quantity' must not be left blank." Display="Dynamic" ControlToValidate="txtQuantity"></asp:requiredfieldvalidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtQuantity"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Quantity' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
								<td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols"><asp:dropdownlist id="cboProductUnit" runat="server" CssClass="ms-short"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Unit' must not be left blank." Display="Dynamic" ControlToValidate="cboProductUnit"></asp:requiredfieldvalidator></td>
							</tr>
							<TR>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-authoringcontrols"><asp:textbox id="txtProductCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox><asp:imagebutton id="cmdProductCode" title="Execute search" style="CURSOR: hand" accessKey="P" border="0" alt="Execute search" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False"></asp:imagebutton></TD>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-authoringcontrols"><asp:textbox id="txtVariation" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox><asp:imagebutton id="cmdVariationSearch" title="Execute search" style="CURSOR: hand" accessKey="V" border="0" alt="Execute search" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False"></asp:imagebutton></TD>
								<td class="ms-formspacer" colSpan="2"></td>
								<td class="ms-formspacer" colSpan="2"></td>
							</TR>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1">
						<table class="ms-toolbar" id="twotidGrpsTB" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
							<TR>
								<td class="ms-toolbar" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-toolbar">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Clear item and Load Defaults" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdClear_Click">Clear Item & Load Defaults</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" title="Save Item" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save Item" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
														</td>
														<td noWrap><asp:linkbutton id="cmdSave" title="Save Item" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save Item</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Item" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Item" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Item" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Item</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgEdit" title="Update Item" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Item" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdEdit" title="Update Item" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Update Item</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label16" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Item And Back To Product List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Item And Back To Product List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Item And Back To Product List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To Product List</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
											</td>
										</tr>
									</table>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD><asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="32%">
							<col width="32%">
							<col width="22%">
							<col width="24%">
							<col width="10">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByDescription" runat="server">Description</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByMatrixDescription" runat="server">Matrix Desc.</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByProductUnitCode" runat="server">Unit of Measure</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="10">
							<col width="32%">
							<col width="32%">
							<col width="22%">
							<col width="24%">
							<col width="10">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" />
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkDescription" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkMatrixDescription" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user" align="right">
								<asp:Label ID="lblItemQuantity" Runat="server"></asp:Label>&nbsp;&nbsp;
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblProductUnitID" Runat="server" Visible="False"></asp:Label>
								<asp:Label ID="lblProductUnitCode" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
