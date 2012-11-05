<%@ Reference Control="~/masterfiles/_productsubgroup/_insert.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductSubGroup._VariationsMatrix.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/DocumentScripts.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" ToolTip="Add New Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" ToolTip="Add New Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" ToolTip="Add New Product Variation" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Product Variation" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Product Variation" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Product Variation" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Product Variation" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Product Variation" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblSubGroupID" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
		    <table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px">
						<font color="red">*</font> Indicates a required field
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1">
						<A name="InputFormSection1"></A><img src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 10px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1:&nbsp;Apply 
							description for each applicable variations
						</div>
					</td>
				</tr>
				<tr>
					<td valign="top" style="PADDING-BOTTOM: 20px" colspan="3" align="center">
						<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="90%" OnItemDataBound="lstItem_ItemDataBound">
							<HeaderTemplate>
								<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
									<colgroup>
										<col width="4%">
										<col width="30%" align="left">
										<col width="66%" align="left">
									</colgroup>
									<TR>
										<TH class="ms-vh2" style="padding-bottom: 4px">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TH>
										<TH class="ms-vh2" style="padding-bottom: 4px">
											<asp:hyperlink id="SortByVariation" runat="server">Variation Type</asp:hyperlink></TH>
										<TH class="ms-vh2" style="padding-bottom: 4px">
											Description
										</TH>
									</TR>
								</table>
							</HeaderTemplate>
							<ItemTemplate>
								<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<COLGROUP>
										<COL width="4%">
										<COL align="left" width="30%">
										<COL align="left" width="66%">
									</COLGROUP>
									<TR>
										<TD class="ms-vb-user">
											<INPUT id="chkList" type="checkbox" name="chkList" runat="server" visible="false">
										</TD>
										<TD class="ms-vb-user">
											<asp:HyperLink id="lnkVariationType" Runat="server"></asp:HyperLink></TD>
										<TD class="ms-vb2">
											<asp:TextBox id="txtDescription" CssClass="ms-short" Runat="server"></asp:TextBox> <label style="font-style: italic; color: red;">(if this is EXPIRATION enter format as YYYY-MM-DD)</label>
											<asp:RequiredFieldValidator id="RequiredFieldValidator5" CssClass="ms-error" runat="server" ErrorMessage="'Description' must not be left blank." Display="Dynamic" ControlToValidate="txtDescription" ForeColor=" "></asp:RequiredFieldValidator></TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 5px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 2: Enter Price, Tax and Inventory Information
						</div>
					</td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>Purchase Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>Margin<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>Selling Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>Check if included in subtotal discount.<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMargin()"></asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtPurchasePrice" Display="Dynamic" ErrorMessage="'Product Purchase Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:CompareValidator id="Comparevalidator7" CssClass="ms-error" runat="server" ErrorMessage="'Product Purchase Price' must be in number." Display="Dynamic" ControlToValidate="txtPurchasePrice" Type="Currency" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator>
							    </td>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtMargin" accessKey="P" runat="server" CssClass="ms-short" MaxLength="5" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMargin()">0</asp:textbox>%
								</td>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtProductPrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="InsertComputeMarginByPrice()"></asp:textbox>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator7" CssClass="ms-error" runat="server" ControlToValidate="txtProductPrice" Display="Dynamic" ErrorMessage="'Product Price' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
									<asp:CompareValidator id="CompareValidator6" CssClass="ms-error" runat="server" ErrorMessage="'Product Price' must be in number." Display="Dynamic" ControlToValidate="txtProductPrice" Type="Currency" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator>
								</td>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
									<asp:CheckBox id="chkIncludeInSubtotalDiscount" runat="server" Text=" Check if included in subtotal discount." Checked="True"></asp:CheckBox>
								</td>
							</tr>
							<TR>
								<TD class="ms-formspacer" height=20></TD>
							</TR>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>VAT (in percent)<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>eVAT (in percent)<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>Local Tax (in percent)<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">
								    <label>Unit Code<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ErrorMessage="'Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Value Added Tax' must be in number." Display="Dynamic" ControlToValidate="txtVAT" Type="Currency" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator>
							    </td>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Expanded Value Added Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtEVAT" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:CompareValidator id="Comparevalidator8" CssClass="ms-error" runat="server" ErrorMessage="'Expanded Value Added Tax' must be in number." Display="Dynamic" ControlToValidate="txtEVAT" Type="Currency" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator>
								</td>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()">0</asp:textbox>&nbsp;%
									<asp:RequiredFieldValidator id="Requiredfieldvalidator13" runat="server" CssClass="ms-error" ErrorMessage="'Local Tax' must not be left blank." Display="Dynamic" ControlToValidate="txtLocalTax" ForeColor=" "></asp:RequiredFieldValidator>
									<asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ErrorMessage="'Local Tax' must be in number." Display="Dynamic" ControlToValidate="txtLocalTax" Type="Currency" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator>
								</td>
								<td class="ms-formspacer"><IMG src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols">
								    <asp:dropdownlist id="cboUnit" CssClass="ms-short" runat="server"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator1" CssClass="ms-error" runat="server" ControlToValidate="cboUnit" Display="Dynamic" ErrorMessage="'Product Unit' must not be left blank." ForeColor=" "></asp:RequiredFieldValidator>
								</td>
							</tr>
							<TR>
								<TD class="ms-formspacer"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
		            <td colspan="3" class="ms-sectionline" height="2"><img src="../../../_layouts/images/empty.gif"></td>
	            </tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>