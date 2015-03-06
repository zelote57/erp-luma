<%@ Reference Control="~/masterfiles/_product/_insert.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._ProductPackage.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../../_Scripts/ComputeMargin.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" ToolTip="Add New Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSave" ToolTip="Add New Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" ToolTip="Add New Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Product Variation" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Product Variation" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Product Variation" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label>
            <asp:Label id="lblProductSubGroupID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 4px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Quantity<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Unit Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtQuantity" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator11" CssClass="ms-error" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="'Initial Quantity' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtQuantity"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Quantity' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:dropdownlist id="cboUnit" CssClass="ms-long" runat="server"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator1" CssClass="ms-error" runat="server" ControlToValidate="cboUnit" Display="Dynamic" ErrorMessage="'Product Unit' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>
                                        Purchase Price<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Margin<font color="red">*</font></label>/
                                    <label>Selling Price<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Margin<font color="red">*</font></label>/
                                    <label>WS Price<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" style="width:10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMargin()">0.00</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtPurchasePrice" Display="Dynamic" ErrorMessage="'Product Purchase Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPurchasePrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Purchase Price' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" style="width:10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtMargin" accessKey="P" runat="server" CssClass="ms-short" 
                                                MaxLength="7" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMargin()" style="width:55px">0</asp:textbox>% /
                                    <asp:textbox id="txtProductPrice" accessKey="P" runat="server" 
                                                CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMarginByPrice()" style="width:75px">0.00</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator7" CssClass="ms-error" runat="server" ControlToValidate="txtProductPrice" Display="Dynamic" ErrorMessage="'Product Selling Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtProductPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Price' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" style="width:10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtWSPriceMarkUp" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric"
                                                MaxLength="7" onkeypress="AllNum()" onkeyup="UpdateComputeMargin()" style="width:55px">0</asp:TextBox>% /
                                    <asp:TextBox ID="txtWSPrice" runat="server" AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" onkeyup="UpdateComputeMarginByPrice()" style="width:75px">0.00</asp:TextBox>
									<asp:RequiredFieldValidator ID="Requiredfieldvalidator16" runat="server" ControlToValidate="txtWSPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Wholesale Price' must not be left blank."
                                                ForeColor=" "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtWSPrice"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Wholesale Price' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Price: Level1 / Level2</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Price: Level3 / Level4 / Level5<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" style="width:10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" style="width:10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtPrice1" runat="server" AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" style="width:75px">0.00</asp:TextBox>
                                    <asp:TextBox ID="txtPrice2" runat="server" AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" style="width:75px">0.00</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator18" runat="server" ControlToValidate="txtPrice1"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level1 Price' must not be left blank."
                                        ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtPrice2"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level1 Price' must be in number, max of 3 decimal places."
                                        ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator17" runat="server" ControlToValidate="txtPrice1"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level2 Price' must not be left blank."
                                        ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtPrice2"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level2 Price' must be in number, max of 3 decimal places."
                                        ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtPrice3" runat="server" AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" style="width:75px">0.00</asp:TextBox>
                                    <asp:TextBox ID="txtPrice4" runat="server" AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" style="width:75px">0.00</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator19" runat="server" ControlToValidate="txtPrice3"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level3 Price' must not be left blank."
                                        ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtPrice4"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level3 Price' must be in number, max of 3 decimal places."
                                        ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator20" runat="server" ControlToValidate="txtPrice3"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level4 Price' must not be left blank."
                                        ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtPrice4"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level4 Price' must be in number, max of 3 decimal places."
                                        ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtPrice5" runat="server" AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric" MaxLength="20" onkeypress="AllNum()" 
                                        style="width:75px" Height="22px">0.00</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator21" runat="server" ControlToValidate="txtPrice5"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level5 Price' must not be left blank."
                                        ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtPrice5"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Level5 Price' must be in number, max of 3 decimal places."
                                        ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
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
                                    <img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ErrorMessage="'Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'VAT' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Expanded Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEVAT"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'EVAT' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator13" runat="server" CssClass="ms-error" ErrorMessage="'Local Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLocalTax"
                                                CssClass="ms-error" Display="Dynamic" ErrorMessage="'Local Tax' must be in number, max of 3 decimal places."
                                                ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>BarCode1<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>BarCode2<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>BarCode3<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBarCode1" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ></asp:textbox>
                                    &nbsp&nbsp<asp:imagebutton id="imgCreateBarCode1" ToolTip="Generate EAN13 in-house barcode" accessKey="S" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/createbarcode.gif" alt="Generate EAN13 in-house barcode" border="0" width="16" height="16" onclick="imgCreateBarCode1_Click" CausesValidation="False"></asp:imagebutton>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'BarCode1' must not be left blank." Display="Dynamic" ControlToValidate="txtBarCode1" ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBarCode2" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ></asp:textbox>
                                    &nbsp<asp:imagebutton id="imgCreateBarCode2" ToolTip="Generate EAN13 in-house barcode" accessKey="S" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/createbarcode.gif" alt="Generate EAN13 in-house barcode" border="0" width="16" height="16" onclick="imgCreateBarCode2_Click" CausesValidation="False"></asp:imagebutton>
                                </td>
                                <td class="ms-formspacer">
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBarCode3" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ></asp:textbox>
                                    &nbsp<asp:imagebutton id="imgCreateBarCode3" ToolTip="Generate EAN13 in-house barcode" accessKey="S" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/createbarcode.gif" alt="Generate EAN13 in-house barcode" border="0" width="16" height="16" onclick="imgCreateBarCode3_Click" CausesValidation="False"></asp:imagebutton>
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
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
