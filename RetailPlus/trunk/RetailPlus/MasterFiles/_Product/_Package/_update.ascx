<%@ Reference Control="~/masterfiles/_product/_update.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._ProductPackage.__Update" Codebehind="_Update.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../../_Scripts/ComputeMargin.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" title="Update Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSave" title="Update Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" title="Update Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" title="Update Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Updating Product Variation" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Product Variation" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Updating Product Variation" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblPackageID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
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
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../../_layouts/images/empty.gif"></td>
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
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtQuantity" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator11" CssClass="ms-error" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="'Initial Quantity' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator6" CssClass="ms-error" runat="server" ErrorMessage="'Initial Quantity' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtQuantity" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
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
                                    <label>
                                        Margin<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>
                                        Selling Price<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMargin()">0.00</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtPurchasePrice" Display="Dynamic" ErrorMessage="'Product Purchase Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator4" CssClass="ms-error" runat="server" ErrorMessage="'Product Purchase Price' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtPurchasePrice" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtMargin" accessKey="P" runat="server" CssClass="ms-short" MaxLength="5" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMargin()">0</asp:textbox>%
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtProductPrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMarginByPrice()">0.00</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator7" CssClass="ms-error" runat="server" ControlToValidate="txtProductPrice" Display="Dynamic" ErrorMessage="'Product Selling Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Product Selling Price' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtProductPrice" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
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
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ErrorMessage="'Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator3" CssClass="ms-error" runat="server" ErrorMessage="'VAT' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Expanded Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator5" CssClass="ms-error" runat="server" ErrorMessage="'eVAT' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                                <td class="ms-formspacer">
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator13" runat="server" CssClass="ms-error" ErrorMessage="'Local Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Local Tax' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
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
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBarCode1" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ></asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'BarCode1' must not be left blank." Display="Dynamic" ControlToValidate="txtBarCode1" ForeColor=" "></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer">
                                    <img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBarCode2" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ></asp:textbox>
                                </td>
                                <td class="ms-formspacer">
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBarCode3" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ></asp:textbox>
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
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
