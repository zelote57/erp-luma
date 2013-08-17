<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__Update" Codebehind="_Update.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../_Scripts/ComputeMargin.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" title="Update Product" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Update Product" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" title="Update Product" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" title="Update Product" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Update Product" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" title="Update Product" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Updating Product" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Updating Product" border="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Updating Product" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
					    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
			                <ContentTemplate >
                                <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Barcode (Primary)<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Barcode (Secondary)</label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                        </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Barcode (Tertiary)</label></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtBarcode" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short" MaxLength="25"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBarcode" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Barcode' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtBarcode2" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short" MaxLength="25"></asp:TextBox>
                                        </td>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                        </td>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtBarcode3" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Product Code<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Description<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtProductCode" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductCode" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Code' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan="6">
                                            <asp:TextBox ID="txtProductDesc" runat="server" AccessKey="S" BorderStyle="Groove" CssClass="ms-long" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProductDesc" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Description' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Select Supplier<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Select Group<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Select SubGroup<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:DropDownList ID="cboSupplier" runat="server" CssClass="ms-short">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator15" runat="server" ControlToValidate="cboSupplier"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Supplier' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:DropDownList ID="cboProductGroup" runat="server" AutoPostBack="True" CssClass="ms-short"
                                                OnSelectedIndexChanged="cboProductGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cboProductGroup"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Group' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:DropDownList ID="cboProductSubGroup" runat="server" AutoPostBack="True" CssClass="ms-short"
                                                OnSelectedIndexChanged="cboProductSubGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" runat="server" ControlToValidate="cboProductSubGroup"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product sub group' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:CheckBox ID="chkWillPrintProductComposition" runat="server" Checked="True" Text="print composition" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Purchase Price<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Retail Margin<font color="red">*</font></label> / 
                                            <label>Selling Price <font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Wholesale Margin<font color="red">*</font></label>/
                                            <label>Selling Price <font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Commision<font color="red">*</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtPurchasePrice" runat="server" AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" onkeyup="UpdateComputeMargin()">0.00</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ControlToValidate="txtPurchasePrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Purchase Price' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPurchasePrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Purchase Price' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtMargin" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric"
                                                MaxLength="5" onkeypress="AllNum()" onkeyup="UpdateComputeMargin()" Width="55px">0</asp:TextBox>% / 
                                            <asp:TextBox ID="txtProductPrice" runat="server" AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" onkeyup="UpdateComputeMarginByPrice()" Width="75px">0.00</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator7" runat="server" ControlToValidate="txtProductPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Selling Price' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtProductPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Selling Price' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtWSPriceMarkUp" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric"
                                                MaxLength="5" onkeypress="AllNum()" onkeyup="UpdateComputeMargin()" Width="55px">0</asp:TextBox>% /
                                            <asp:TextBox ID="txtWSPrice" runat="server" AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" onkeyup="UpdateComputeMarginByPrice()" Width="75px">0.00</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator16" runat="server" ControlToValidate="txtWSPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Wholesale Price' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtWSPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Wholesale Price' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator> 
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtPercentageCommision" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric"
                                                MaxLength="5" onkeypress="AllNum()" Width="55px">0</asp:TextBox>% /
                                            <asp:CheckBox ID="chkIsItemSold" runat="server" Checked="True" Text="sell this item" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>VAT (in percent)<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>eVAT (in percent)<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Local Tax (in percent)<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtVAT" runat="server" AccessKey="D" BorderStyle="Groove" CssClass="ms-short-numeric"
                                                MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>%
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator12" runat="server" ControlToValidate="txtVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Value Added Tax' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'VAT' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtEVAT" runat="server" AccessKey="D" BorderStyle="Groove" CssClass="ms-short-numeric"
                                                MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>%
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator14" runat="server" ControlToValidate="txtEVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Expanded Value Added Tax' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtEVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'EVAT' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtLocalTax" runat="server" AccessKey="D" BorderStyle="Groove" CssClass="ms-short-numeric" Width=110
                                                MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>%
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator13" runat="server" ControlToValidate="txtLocalTax"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Local Tax' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLocalTax"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Local Tax' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:CheckBox ID="chkIncludeInSubtotalDiscount" runat="server" Checked="True" Text="include in subtotal discount." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>
                                                Select Base Unit<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>
                                                Current Quantity<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>
                                                Minimum Threshold<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>
                                                Maximum Threshold<font color="red">*</font></label></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:DropDownList ID="cboProductUnit" runat="server" CssClass="ms-short">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboProductUnit"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Base Unit' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtQuantity" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-disabled"
                                                MaxLength="20" onkeypress="AllNum()" ReadOnly="True">0</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator11" runat="server" ControlToValidate="txtQuantity"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Initial Quantity' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtQuantity"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Current Quantity' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtMinThreshold" runat="server" AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" runat="server" ControlToValidate="txtMinThreshold"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Min threshold' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtMinThreshold"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Minimum Threshold' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtMaxThreshold" runat="server" AccessKey="D" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator10" runat="server" ControlToValidate="txtMaxThreshold"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Max threshold' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMaxThreshold"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Maximum Threshold Quantity' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:CheckBox ID="chkVariations" runat="server" Text="Check this box if you like to inherit the variations from selected subgroup." />
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan=3>
                                            <asp:CheckBox ID="chkVariationsMatrix" runat="server" Text="Check this box if you like to inherit the variations and price matrix from selected subgroup." />
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:CheckBox ID="chkUnitMatrix" runat="server" Text="Check this box if you like to inherit the unit matrix from selected subgroup." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
			        </asp:UpdatePanel>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Required Inventory Days (RID)<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label><font color="red"></font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label><font color="red"></font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtRID" runat="server"  AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
