<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.SalesAndReceivables._SalesJournals.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/ComputeSO.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgPrint" title="Print this Sales Order" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Sales Order" ImageUrl="/RetailPlus/_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdPrint" title="Print this Sales Order" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Back To SO List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Back To SO List" ImageUrl="/RetailPlus/_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Back To SO List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To SO List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" colspan="3"><asp:label id="lblSOID" runat="server" Visible="False"></asp:label>
						<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td style="PADDING-BOTTOM: 10px" vAlign="top" colspan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%" rowSpan="4"><IMG alt="" src="/RetailPlus/_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Sales 
										Order</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Sales Order no:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSONo" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer" style="HEIGHT: 52px"></TD>
								<TD class="ms-formspacer" style="HEIGHT: 52px"></TD>
								<td style="PADDING-BOTTOM: 2px; HEIGHT: 52px" vAlign="top" width="30%" colspan="2"><label>Date 
										Prepared: </label>
									<asp:label id="lblSODate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Customer 
										Name:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Terms:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblCustomerID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lblCustomerCode" runat="server">lblCustomerCode</asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblCustomerContact" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblTerms" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblModeOfterms" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Customer 
										Address:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Telephone 
										no:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Required 
										Delivery Date:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblCustomerAddress" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblCustomerTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblRequiredDeliveryDate" runat="server" CssClass="ms-error"></asp:label></LABEL></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Deliver 
										to branch: (Specify complete address)</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="4"><label>Branch 
										Address:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBranchCode" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblBranchID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="3"><asp:label id="lblBranchAddress" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="6"><label>SO 
										Remarks:</label><asp:label id="lblSORemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD><asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="20%">
							<col width="20%">
							<col width="12%">
							<col width="12%">
							<col width="12%">
							<col width="12%">
							<col width="12%">
							<col width="10">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByDescription" runat="server">Description</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByMatrixDescription" runat="server">Matrix Desc.</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByProductUnitCode" runat="server">Unit of Measure</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByUntCost" runat="server">Unit Cost</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByDiscount" runat="server">Discount</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByAmount" runat="server">Total Cost</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
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
							<col width="12%">
							<col width="12%">
							<col width="12%">
							<col width="12%">
							<col width="12%">
							<col width="10">
						</colgroup>
						<TR>
							<TD class="ms-vb-user">
								<input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkDescription" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user">
								<asp:HyperLink ID="lnkMatrixDescription" Runat="server"></asp:HyperLink>
							</TD>
							<TD class="ms-vb-user" align="right">
								<asp:Label ID="lblQuantity" Runat="server"></asp:Label>&nbsp;&nbsp;
							</TD>
							<TD class="ms-vb-user">
								<asp:Label ID="lblProductUnitID" Runat="server" Visible="False"></asp:Label>
								<asp:Label ID="lblProductUnitCode" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user" align="right">
								<asp:Label ID="lblUnitCost" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb-user" align="right">
								<asp:Label ID="lblDiscount" Runat="server"></asp:Label>
								<asp:Label ID="lblPercent" Runat="server" Visible="False">%</asp:Label>
							</TD>
							<TD class="ms-vb-user" align="right">
								<asp:Label ID="lblAmount" Runat="server"></asp:Label>
							</TD>
							<TD class="ms-vb2">
								<A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="/RetailPlus/_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</TD>
						</TR>
						<TR>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></TD>
							<TD colspan="7" height="1">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<asp:panel id="panItem" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
										<table id="tblpanItem" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="ms-formspacer" colspan="1"><IMG alt="" src="/RetailPlus/_layouts/images/trans.gif" width="10"></TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>VAT</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblVAT" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
													<asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>eVAT</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label9" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:Label id="lblEVAT" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label6" CssClass="ms-vh2" runat="server" text="<b>Local Tax</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label11" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblLocalTax" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
													<asp:Label id="Label13" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label14" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:Label id="Label15" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b>Remarks</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label10" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD colspan="4">
													<asp:Label ID="lblRemarks" CssClass="ms-vb2" Runat="server"></asp:Label>
												</TD>
											</TR>
										</TABLE>
									</asp:panel></DIV>
							</TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></TD>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td vAlign="top" colspan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td style="PADDING-BOTTOM: 2px" align="left"><label><b>Subtotal:</b></label></td>
								<td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblSOSubTotal" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td style="PADDING-BOTTOM: 2px" align="left"><label><b>VAT:</b></label></td>
								<td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblSOVAT" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td class="ms-sectionline" colspan="2" height="1"><IMG alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<TD width="70%"></TD>
								<td style="PADDING-BOTTOM: 2px" align="left"><label><b>Total:</b></label></td>
								<td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblSOTotal" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</TD>
	</tr>
</table>
