<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._Stock.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrint" title="Print this Stock Transaction" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Stock Transaction" ImageUrl="../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdPrint" title="Print this Stock Transaction" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Back to stock transactions" accessKey="C" tabIndex="3" CausesValidation="False" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Back to stock transactions" border="0" width="16" height="16" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Back to stock transactions" accessKey="C" tabIndex="4" CausesValidation="False" CssClass="ms-toolbar" runat="server" onclick="cmdCancel_Click">Back to stock transactions</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblStockID" runat="server" Visible="False"></asp:label></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">General Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">
							Transaction No.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Stock Date.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Stock Type and Stock 
							Direction.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 10px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Transaction 
										No.:
										<asp:label id="lblTransactionNo" CssClass="ms-error" runat="server"></asp:label></label>&nbsp;
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><LABEL>Stock&nbsp;Date:</LABEL>
									<asp:label id="lblStockDate" CssClass="ms-error" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><LABEL>Supplier:</LABEL>
									<asp:label id="lblSupplier" CssClass="ms-error" runat="server"></asp:label>
									<asp:label id="lblSupplierID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><LABEL>Stock&nbsp;Type:</LABEL>
									<asp:label id="lblStockTypeCode" CssClass="ms-error" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><LABEL>Direction:</LABEL>
									<asp:label id="lblStockDirection" CssClass="ms-error" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px">&nbsp;
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B> </B></label>
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colSpan="2"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
				</tr>
				<tr>
					<td colSpan="4" height="5"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="20%">
							<col width="20%">
							<col width="15%">
							<col width="10%">
							<col width="34%">
							<col width="1%">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByProduct" runat="server">Product</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByVariation" runat="server">Variation</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByUnit" runat="server">Unit</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByQty" runat="server">Quantity</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByRemarks" runat="server">Remarks</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						<colgroup>
							<col width="10">
							<col width="20%">
							<col width="20%">
							<col width="15%">
							<col width="10%">
							<col width="34%">
							<col width="1%">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" NAME="chkList" visible="false">
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkProduct" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkVariation" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblProductUnit" Runat="server"></asp:Label>
								<asp:Label ID="lblStockType" Runat="server" Visible="False"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblQuantity" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblRemarks" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							</TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
