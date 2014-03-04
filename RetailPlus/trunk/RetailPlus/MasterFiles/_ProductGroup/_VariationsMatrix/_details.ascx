<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ProductGroup._VariationsMatrix.__Details" Codebehind="_details.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap" style="width: 19px"><asp:ImageButton id="imgBack" accessKey="B" tabIndex="3" height="16" width="16" border="0" ToolTip="Back to previous window" ImageUrl="../../../_layouts/images/back.gif" runat="server" CssClass="ms-toolbar" OnClick="imgBack_Click" /></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" accessKey="B" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="cmdBack_Click">Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblMatrixID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblGroupID" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-sectionline" height="1"><img alt="" src="../../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 10px;" valign="top" colspan="3">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 1:&nbsp;Applied
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
											<asp:TextBox id="txtDescription" CssClass="ms-short-disabled" Runat="server" ReadOnly="true"></asp:TextBox> <label style="font-style: italic; color: red;">(if this is EXPIRATION enter format as YYYY-MM-DD)</label></td>
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
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 2: Price, Tax and Inventory Information
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="90%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Purchase Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Margin<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Selling Price<font color="red">*</font></label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Check if included in subtotal discount.<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtPurchasePrice" accessKey="P" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMargin()" ReadOnly="True"></asp:textbox>
                                    &nbsp;
							    </td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtMargin" accessKey="P" runat="server" CssClass="ms-short-disabled" MaxLength="5" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMargin()" ReadOnly="True">0</asp:textbox>%
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtProductPrice" accessKey="P" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" onKeyUp="UpdateComputeMarginByPrice()" ReadOnly="True"></asp:textbox>
                                    &nbsp;
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:CheckBox id="chkIncludeInSubtotalDiscount" runat="server" Text=" Check if included in subtotal discount." Checked="True" Enabled="False"></asp:CheckBox>
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
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2">
								    <label>Unit Code<font color="red">*</font></label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtVAT" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True">0</asp:textbox>&nbsp;%
                                    &nbsp;
							    </td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
									<asp:textbox id="txtEVAT" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True">0</asp:textbox>&nbsp;%
                                    &nbsp;
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:textbox id="txtLocalTax" accessKey="D" runat="server" CssClass="ms-short-disabled" MaxLength="20" BorderStyle="Groove" onKeyPress="AllNum()" ReadOnly="True">0</asp:textbox>&nbsp;%
                                    &nbsp;
								</td>
								<td class="ms-formspacer"><img alt="" src="../../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols">
								    <asp:dropdownlist id="cboUnit" CssClass="ms-short-disabled" runat="server" Enabled="False"></asp:dropdownlist>&nbsp;
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
		<td colspan="3"><img height="10" alt="" src="../../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
