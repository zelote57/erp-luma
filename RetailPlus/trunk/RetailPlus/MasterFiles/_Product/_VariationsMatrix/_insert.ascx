<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix.__Insert" Codebehind="_Insert.ascx.cs" %>
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
                        <asp:UpdatePanel ID="updSave" runat="server">
                            <ContentTemplate>
						        <table cellspacing="0" cellpadding="1" border="0">
							        <tr>
								        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" ToolTip="Add New Product Variation Matrix" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								        </td>
								        <td nowrap="nowrap"><asp:linkbutton id="cmdSave" ToolTip="Add New Product Variation Matrix" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							        </tr>
						        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" ToolTip="Add New Product Variation Matrix" accessKey="B" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Product Variation Matrix" accessKey="B" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Product Variation Matrix" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Product Variation" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Product Variation Matrix" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" src="../../../_layouts/images/blank.gif" width="10"></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px">
						<font color="red">*</font> Indicates a required field
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1">
						<A name="InputFormSection1"></A><img alt="" src="../../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 10px;" valign="top" colspan="3">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 1:&nbsp;Apply 
							description for each applicable variations
						</div>
					</td>
				</tr>
				<tr>
					<td valign="top" style="padding-bottom: 20px" colspan="3" align="center">
						<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="90%" OnItemDataBound="lstItem_ItemDataBound">
							<HeaderTemplate>
								<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
									<colgroup>
										<col width="4%">
										<col width="30%" align="left">
										<col width="66%" align="left">
									</colgroup>
									<tr>
										<TH class="ms-vh2" style="padding-bottom: 4px">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
										<TH class="ms-vh2" style="padding-bottom: 4px">
											<asp:hyperlink id="SortByVariation" runat="server">Variation Type</asp:hyperlink></TH>
										<TH class="ms-vh2" style="padding-bottom: 4px">
											Description
										</TH>
									</tr>
								</table>
							</HeaderTemplate>
							<ItemTemplate>
								<table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
									<COLGROUP>
										<COL width="4%">
										<COL align="left" width="30%">
										<COL align="left" width="66%">
									</COLGROUP>
									<tr>
										<td class="ms-vb-user">
											<INPUT id="chkList" type="checkbox" name="chkList" runat="server" visible="false">
										</td>
										<td class="ms-vb-user">
											<asp:HyperLink id="lnkVariationType" Runat="server"></asp:HyperLink></td>
										<td class="ms-vb2">
											<asp:TextBox id="txtDescription" CssClass="ms-short" Runat="server"></asp:TextBox> <label style="font-style: italic; color: red;">(if this is EXPIRATION enter format as YYYY-MM-DD)</label>
											<asp:RequiredFieldValidator id="RequiredFieldValidator5" CssClass="ms-error" runat="server" ErrorMessage="'Description' must not be left blank." Display="Dynamic" ControlToValidate="txtDescription" ForeColor=" "></asp:RequiredFieldValidator></td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 5px;" valign="top" colspan="3">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 2: Enter Price, Tax and Inventory Information
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="90%" border="0">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Barcode (Primary)</td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Barcode (Secondary)</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Barcode (Tertiary)</label></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtBarcode" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short" MaxLength="25"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtBarcode2" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short" MaxLength="25"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtBarcode3" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short" MaxLength="25"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Purchase Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Retail Margin<font color="red">*</font></label> /
								    <label>Selling Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Wholesale Margin<font color="red">*</font></label> /
								    <label>Selling Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Check if included in subtotal discount.<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMargin()"></asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtPurchasePrice" Display="Dynamic" ErrorMessage="'Product Purchase Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator7" CssClass="ms-error" runat="server" ErrorMessage="'Product Purchase Price' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtPurchasePrice" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
							    </td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtMargin" accessKey="P" runat="server" CssClass="ms-short" MaxLength="5" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMargin()" Width="55px">0</asp:textbox>% /
									<asp:textbox id="txtProductPrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMarginByPrice()" Width="75px"></asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator7" CssClass="ms-error" runat="server" ControlToValidate="txtProductPrice" Display="Dynamic" ErrorMessage="'Product Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator6" CssClass="ms-error" runat="server" ErrorMessage="'Product Price' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtProductPrice" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:TextBox ID="txtWSPriceMarkUp" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short" MaxLength="5" onkeypress="AllNum()" onkeyup="InsertComputeMargin()" Width="55px">0</asp:TextBox>% /
                                    <asp:TextBox ID="txtWSPrice" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short" MaxLength="20" onkeypress="AllNum()" onkeyup="InsertComputeMarginByPrice()" Width="75px">0.00</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator16" runat="server" ControlToValidate="txtWSPrice" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Wholesale Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtWSPrice" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Wholesale Price' must be in number, max of 3 decimal places." ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator> 
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:CheckBox id="chkIncludeInSubtotalDiscount" runat="server" Text=" Check if included in subtotal discount." Checked="True"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer" height=20></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>VAT (in percent)<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>eVAT (in percent)<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Local Tax (in percent)<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ErrorMessage="'Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Value Added Tax' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtVAT" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
							    </td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Expanded Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator8" CssClass="ms-error" runat="server" ErrorMessage="'Expanded Value Added Tax' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtEVAT" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator13" runat="server" CssClass="ms-error" ErrorMessage="'Local Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Local Tax' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtLocalTax" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer" height=20></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Select Unit Code<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Initial Quantity<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Minimum Threshold<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Maximum Threshold<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:dropdownlist id="cboUnit" CssClass="ms-short-disabled" runat="server"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator1" CssClass="ms-error" runat="server" ControlToValidate="cboUnit" Display="Dynamic" ErrorMessage="'Product Unit' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
							    </td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtQuantity" accessKey="P" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator11" CssClass="ms-error" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="'Initial Quantity' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator3" CssClass="ms-error" runat="server" ErrorMessage="'Initial Quantity' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtQuantity" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtMinThreshold" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator9" CssClass="ms-error" runat="server" ControlToValidate="txtMinThreshold" Display="Dynamic" ErrorMessage="'Min threshold' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator4" CssClass="ms-error" runat="server" ErrorMessage="'Min threshold' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtMinThreshold" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtMaxThreshold" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator10" CssClass="ms-error" runat="server" ControlToValidate="txtMaxThreshold" Display="Dynamic" ErrorMessage="'Max threshold' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator5" CssClass="ms-error" runat="server" ErrorMessage="'Max threshold' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtMaxThreshold" ValidationExpression="^\s*-?([\d\,]+(\.\d{1,3})?|\.\d{1,3})\s*$" ></asp:RegularExpressionValidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
		            <td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../../_layouts/images/empty.gif" /></td>
	            </tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
