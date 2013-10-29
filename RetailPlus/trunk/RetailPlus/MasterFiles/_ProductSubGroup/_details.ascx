<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductSubGroup.__Details" Codebehind="_details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgBack" accessKey="C" tabIndex="3" height="16" width="16" border="0" ToolTip="Back" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="cmdBack_Click" >Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductSubGroupID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
		    <table cellspacing="0" cellpadding="0" width="100%" border="0">
			    <tr>
					<td class="ms-descriptiontext" style="padding-bottom: 4px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3">
						<asp:ValidationSummary id="ValidationSummary2" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
					    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
			                <ContentTemplate >
                                <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Select group<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px"></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan=5>
                                            <asp:dropdownlist id="cboGroup" CssClass="ms-long-disabled" runat="server" Enabled="False"></asp:dropdownlist>
									        <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Group' must not be left blank." Display="Dynamic" ControlToValidate="cboGroup" ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Product subgroup code<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Product subgroup name<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Base unit<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtProductSubGroupCode" accessKey="G" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" MaxLength="20" ReadOnly="True"></asp:textbox>
                                            <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product sub group code' must not be left blank." Display="Dynamic" ControlToValidate="txtProductSubGroupCode" ForeColor=" "></asp:requiredfieldvalidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" >
                                            <asp:TextBox id="txtProductSubGroupName" runat="server" accesskey="N" CssClass="ms-short-disabled" MaxLength="50" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
									        <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Product sub group name' must not be left blank." Display="Dynamic" ControlToValidate="txtProductSubGroupName" ForeColor=" "></asp:requiredfieldvalidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:dropdownlist id="cboProductSubGroupUnit" runat="server" CssClass="ms-short-disabled" Width="80px" Enabled="False"></asp:dropdownlist>
                                            <asp:imagebutton id="imgAdd" ToolTip="Add New Unit" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Unit" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgAdd_Click"></asp:imagebutton>
									        <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" CssClass="ms-error" ControlToValidate="cboProductSubGroupUnit" Display="Dynamic" ErrorMessage="'Product SubGroup Base Unit' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Purchase price<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Selling price<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True"></asp:textbox>
									        <asp:RequiredFieldValidator id="Requiredfieldvalidator3" CssClass="ms-error" runat="server" ControlToValidate="txtPurchasePrice" Display="Dynamic" ErrorMessage="'Product Purchase Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									        <asp:RegularExpressionValidator id="RegularExpressionValidator4" CssClass="ms-error" runat="server" ErrorMessage="'Product Purchase Price' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtPurchasePrice" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtProductPrice" accessKey="P" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True"></asp:textbox>
									        <asp:RequiredFieldValidator id="Requiredfieldvalidator7" CssClass="ms-error" runat="server" ControlToValidate="txtProductPrice" Display="Dynamic" ErrorMessage="'Product Selling Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									        <asp:RegularExpressionValidator id="RegularExpressionValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Product Selling Price' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtProductPrice" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan="3">
                                            <asp:CheckBox id="chkIncludeInSubtotalDiscount" runat="server" Text=" Check if included in subtotal discount." Checked="True" CssClass="ms-short-disabled" Enabled="False"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20"></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Value Added Tax (VAT)<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Expanded Value Added Tax (eVAT)<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Local Tax<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True">0</asp:textbox>&nbsp;%
									        <asp:RequiredFieldValidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ErrorMessage="'Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" "></asp:RequiredFieldValidator>
									        <asp:RegularExpressionValidator id="RegularExpressionValidator3" CssClass="ms-error" runat="server" ErrorMessage="'VAT' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True">0</asp:textbox>&nbsp;%
									        <asp:RequiredFieldValidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ErrorMessage="'Expanded Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" "></asp:RequiredFieldValidator>
									        <asp:RegularExpressionValidator id="RegularExpressionValidator5" CssClass="ms-error" runat="server" ErrorMessage="'eVAT' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True">0</asp:textbox>&nbsp;%
									        <asp:RequiredFieldValidator id="Requiredfieldvalidator13" runat="server" CssClass="ms-error" ErrorMessage="'Local Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" "></asp:RequiredFieldValidator>
									        <asp:RegularExpressionValidator id="RegularExpressionValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Local Tax' must be in number, max of 3 decimal places." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" " Type="Currency" Operator="DataTypeCheck"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20"></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:CheckBox id="chkVariations" runat="server" Text="Check this box if you like to inherit the variations from selected group." CssClass="ms-short-disabled" Enabled="False"></asp:CheckBox>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:CheckBox id="chkVariationsMatrix" runat="server" Text="Check this box if you like to inherit the variations and price matrix from selected group." CssClass="ms-short-disabled" Enabled="False"></asp:CheckBox>
                                        </td>
                                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan="3">
                                            <asp:CheckBox id="chkUnitMatrix" runat="server" Text="Check this box if you like to inherit the unit matrix from selected group." CssClass="ms-short-disabled" Enabled="False"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
			                <Triggers> 
                            </Triggers>
			            </asp:UpdatePanel>
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
