<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../_Scripts/ComputeMargin.js"></script>
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
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:ImageButton id="imgProductHistory" runat="server" ImageUrl="../../_layouts/images/prodhist.gif" ToolTip="Show product inventory history report" CausesValidation=false OnClick="imgProductHistory_Click"></asp:ImageButton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdProductHistory" accessKey="I" tabIndex="2" CssClass="ms-toolbar" runat="server" CausesValidation=false OnClick="cmdProductHistory_Click">Show inventory history</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:ImageButton id="imgProductPriceHistory" runat="server" ImageUrl="../../_layouts/images/pricehist.gif" ToolTip="Show product price history report" CausesValidation=false OnClick="imgProductPriceHistory_Click"></asp:ImageButton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdProductPriceHistory" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" CausesValidation=false OnClick="cmdProductPriceHistory_Click">Show price history</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:ImageButton id="imgChangePrice" runat="server" ImageUrl="../../_layouts/images/chprice.gif" ToolTip="Change price" CausesValidation=false OnClick="imgChangePrice_Click"></asp:ImageButton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdChangePrice" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" CausesValidation=false OnClick="cmdChangePrice_Click">Change price</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator3" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:ImageButton id="imgEditNow" runat="server" ImageUrl="../../_layouts/images/edit.gif" ToolTip="Edit this product" CausesValidation=false OnClick="imgEditNow_Click"></asp:ImageButton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEditNow" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" CausesValidation=false OnClick="cmdEditNow_Click">Edit this product</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:ImageButton id="imgInvAdjustment" runat="server" ImageUrl="../../_layouts/images/invadj.gif" ToolTip="Adjust inventory count" CausesValidation=false OnClick="imgInvAdjustment_Click"></asp:ImageButton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdInvAdjustment" accessKey="N" tabIndex="2" CssClass="ms-toolbar" runat="server" CausesValidation=false OnClick="cmdInvAdjustment_Click">Inventory Adjustment</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="lblSeparator5" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgBack" title="Back to previous window" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" title="Back to previous window" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdBack_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
			    <tr>
					<td class="ms-descriptiontext" style="padding-bottom: 4px; PADDING-TOP: 8px" colspan="3">
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
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
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtBarcode" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short-disabled" MaxLength="25"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBarcode" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Barcode' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtBarcode2" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short-disabled" MaxLength="25"></asp:TextBox>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtBarcode3" runat="server" AccessKey="B" BorderStyle="Groove" CssClass="ms-short-disabled" MaxLength="25"></asp:TextBox>
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
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:TextBox ID="txtProductCode" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-disabled" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductCode" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Code' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan="6">
                                            <asp:TextBox ID="txtProductDesc" runat="server" AccessKey="S" BorderStyle="Groove" CssClass="ms-long-disabled" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProductDesc" CssClass="ms-error" Display="Dynamic" ErrorMessage="'Product Description' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Supplier<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Product Group<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Product SubGroup<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:DropDownList ID="cboSupplier" runat="server" Enabled=false CssClass="ms-short-disabled">
                                    </asp:DropDownList>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:DropDownList ID="cboProductGroup" runat="server" Enabled=false AutoPostBack="True" CssClass="ms-short-disabled"
                                        OnSelectedIndexChanged="cboProductGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:DropDownList ID="cboProductSubGroup" runat="server" Enabled=false AutoPostBack="True" CssClass="ms-short-disabled"
                                        OnSelectedIndexChanged="cboProductSubGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox ID="chkWillPrintProductComposition" runat="server" Enabled=false Checked="True" Text="print composition" />
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
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtPurchasePrice" runat="server" ReadOnly=true AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric-disabled" MaxLength="20" onkeypress="AllNum()" onkeyup="InsertComputeMargin()">0.00</asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtMargin" runat="server" ReadOnly=true AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric-disabled"
                                        MaxLength="5" onkeypress="AllNum()" onkeyup="InsertComputeMargin()" Width="55px">0</asp:TextBox>% /
                                    <asp:TextBox ID="txtProductPrice" runat="server" ReadOnly=true AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric-disabled" MaxLength="20" onkeypress="AllNum()" onkeyup="InsertComputeMarginByPrice()" Width="75px">0.00</asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtWSPriceMarkUp" runat="server" AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric-disabled"
                                                MaxLength="5" onkeypress="AllNum()" onkeyup="InsertComputeMargin()" Width="55px">0</asp:TextBox>% /
                                            <asp:TextBox ID="txtWSPrice" runat="server" AccessKey="P" BorderStyle="Groove"
                                                CssClass="ms-short-numeric-disabled" MaxLength="20" onkeypress="AllNum()" onkeyup="InsertComputeMarginByPrice()" Width="75px">0.00</asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtPercentageCommision" runat="server" ReadOnly=true AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric-disabled"
                                        MaxLength="5" onkeypress="AllNum()" Width="55px">0</asp:TextBox>% /
                                    <asp:CheckBox ID="chkIsItemSold" runat="server" Enabled=false Checked="True" Text="sell this item" />
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
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtVAT" runat="server" ReadOnly=true AccessKey="D" BorderStyle="Groove" CssClass="ms-short-numeric-disabled"
                                        MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>%
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtEVAT" runat="server" ReadOnly=true AccessKey="D" BorderStyle="Groove" CssClass="ms-short-numeric-disabled"
                                        MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>%
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtLocalTax" runat="server" ReadOnly=true AccessKey="D" BorderStyle="Groove" CssClass="ms-short-numeric-disabled" Width=110
                                        MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>%
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox ID="chkIncludeInSubtotalDiscount" runat="server" Enabled=false Checked="True" Text="include in subtotal discount." />
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>
                                        Base Unit<font color="red">*</font></label></td>
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
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:DropDownList ID="cboProductUnit" runat="server" Enabled=false CssClass="ms-short-disabled">
                                    </asp:DropDownList>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtQuantity" runat="server" ReadOnly=true AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric-disabled"
                                        MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtMinThreshold" runat="server" ReadOnly=true AccessKey="P" BorderStyle="Groove"
                                        CssClass="ms-short-numeric-disabled" MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtMaxThreshold" runat="server" ReadOnly=true AccessKey="D" BorderStyle="Groove"
                                        CssClass="ms-short-numeric-disabled" MaxLength="20" onkeypress="AllNum()">0</asp:TextBox>
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
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox ID="chkVariations" runat="server" Enabled=false Text="Check this box if you like to inherit the variations from selected subgroup." />
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:CheckBox ID="chkVariationsMatrix" runat="server" Enabled=false Text="Check this box if you like to inherit the variations and price matrix from selected subgroup." />
                                </td>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:CheckBox ID="chkUnitMatrix" runat="server" Enabled=false Text="Check this box if you like to inherit the unit matrix from selected subgroup." />
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
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
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
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox ID="txtRID" runat="server"  ReadOnly=true AccessKey="P" BorderStyle="Groove" CssClass="ms-short-numeric-disabled" MaxLength="30">0</asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
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
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
