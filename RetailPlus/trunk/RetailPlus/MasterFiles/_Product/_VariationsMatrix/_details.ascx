<%@ Reference Control="~/masterfiles/_product/_details.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../../_Scripts/ComputeMargin.js"></script>
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
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgBack" title="Back to previous window" accessKey="B" tabIndex="3" height="16" width="16" border="0" alt="Back to previous window" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" title="Back to previous window" accessKey="B" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblMatrixID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductID" runat="server" Visible="False"></asp:Label>
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
						<A name="InputFormSection1"></A><img alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1:&nbsp;Apply 
							description for each applicable variations
						</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-colspace">&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" style="PADDING-BOTTOM: 20px" colspan="3">
						<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%">
							<HeaderTemplate>
								<table width="90%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
									<colgroup>
										<col width="4%">
										<col width="50%" align="left">
										<col width="46%" align="left">
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
										<COL align="left" width="50%">
										<COL align="left" width="46%">
									</COLGROUP>
									<TR>
										<TD class="ms-vb-user">
											<INPUT id="chkList" type="checkbox" name="chkList" runat="server" visible="false">
										</TD>
										<TD class="ms-vb-user">
											<asp:Label id="lblVariationType" Runat="server"></asp:Label></TD>
										<TD class="ms-vb2">
											<asp:TextBox id="txtDescription" CssClass="ms-short" Runat="server" Enabled="False"></asp:TextBox></TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 2: Product Unit 
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">Choose the product 
							unit code.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label> Select 
										Unit Code<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:dropdownlist id="cboUnit" CssClass="ms-long" runat="server" Width="157px" Enabled="False"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 3: Product Price and 
							Tax Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							product purchase price.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							product selling price.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							product special discount.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							inclusive Value Added Tax (VAT) in percent.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							Expanded Value Added Tax (eVAT) in percent.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							inclusive Local Tax in percent.</div>
					</td>
					<td class="ms-colspace" style="HEIGHT: 67px">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Product 
										Purchase Price<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMargin()" Enabled="False"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Product 
										Margin<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtMargin" accessKey="P" runat="server" CssClass="ms-short" MaxLength="5" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMargin()" Enabled="False">0</asp:textbox>%
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Product 
										Price<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtProductPrice" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMarginByPrice()" Enabled="False"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Include 
										in Subtotal Discount<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:CheckBox id="chkIncludeInSubtotalDiscount" runat="server" Text=" Check if included in subtotal discount." Checked="True" Enabled="False"></asp:CheckBox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Value 
										Added Tax (VAT) in percent<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" Enabled="False">0</asp:textbox>&nbsp;%
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Expanded 
										Value Added Tax (eVAT) in percent<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" Enabled="False">0</asp:textbox>&nbsp;%
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Local 
										Tax in percent<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" Enabled="False">0</asp:textbox>&nbsp;%
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 4: Inventory 
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							Displays the current quantity (readonly since the inventory is up-to-date)</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							minimum threshold quantity.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 25px">
							maximum threshold quantity.</div>
					</td>
					<td class="ms-colspace" style="HEIGHT: 67px">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px; HEIGHT: 67px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Current&nbsp;Quantity 
										in inventory<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtQuantity" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" ReadOnly="True" Enabled="False">0</asp:textbox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Minimum 
										threshold quantity<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtMinThreshold" accessKey="P" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" Enabled="False">0</asp:textbox>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Maximum 
										threshold quantity<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:textbox id="txtMaxThreshold" accessKey="D" runat="server" CssClass="ms-short" MaxLength="20" BorderStyle="Groove" Enabled="False">0</asp:textbox>
								</td>
							</tr>
							<TR>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-authoringcontrols" width="100%"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../../_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
