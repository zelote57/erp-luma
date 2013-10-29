<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._Payments.__Details" Codebehind="_Details.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/PurchasesAndPayables.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrint" title="Print this Purchase Order" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Purchase Order" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdPrint" title="Print this Purchase Order" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="Label5" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Account And Back To Payments List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Account And Back To Payments List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Account And Back To Payments List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To Payments List</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="Td1" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblPaymentID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 10px" valign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="30%" rowspan="5"><img alt="" src="../../_layouts/images/company_logo.gif" /></td>
								<td class="ms-formspacer"></td>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowspan="6"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">
										Payment</label></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"><label>Bank<FONT color="red">*</FONT></label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="padding-bottom: 2px" width="30%">
                                    &nbsp;<asp:HyperLink ID="lblBank" runat="server" CssClass="ms-error"></asp:HyperLink>
                                    </td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2">Cheque No<FONT color="red">*</FONT></td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="padding-bottom: 10px" width="30%">
								    <asp:Label ID="lblChequeNo" runat="server" CssClass="ms-error"></asp:Label>&nbsp;
								
								</td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"><label>Cheque Date<FONT color="red">*</FONT> 
                                    <asp:Label ID="Label4" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:Label></label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="padding-bottom: 2px" width="30%">
								    <asp:Label ID="lblChequeDate" runat="server" CssClass="ms-error"></asp:Label></td>
							</tr>
							<tr style="padding-bottom: 20px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Payee Information:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2">
								    <asp:label id="Label16" runat="server">Payee Name: </asp:label>
								</td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="2"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%">
								    <asp:HyperLink ID="lblPayeeCode" runat="server" CssClass="ms-error"></asp:HyperLink>
								</td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%">
									<asp:Label ID="lblPayeeName" runat="server" CssClass="ms-error"></asp:Label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label>
										Particulars:</label>
								</td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="3">
								    <asp:Label ID="lblRemarks" runat="server" CssClass="ms-error"></asp:Label>
								</td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label><b>
										Paid Purchase Order(s): </b></label>
								</td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="5">
									<asp:datalist id="lstPO" runat="server" Width="100%" cellpadding="0" ShowFooter="False">
										<HeaderTemplate>
											<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
												<colgroup>
													<col width="10">
													<col width="20%">
													<col width="21%">
													<col width="20%">
													<col width="21%">
													<col width="18%">
													<col width="100">
												</colgroup>
												<tr>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
													</th>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
														<asp:hyperlink id="SortByPONo" runat="server">PO No</asp:hyperlink></th>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
														<asp:hyperlink id="SortByPODate" runat="server">Order Date</asp:hyperlink></th>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														<asp:hyperlink id="SortByDeliveryDate" runat="server">Delivery Date</asp:hyperlink></th>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														<asp:hyperlink id="SortBySupplierDRNo" runat="server">Supplier DRNo.</asp:hyperlink></th>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														<asp:hyperlink id="SortByAmount" runat="server">Amount</asp:hyperlink></th>
													<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
													</th>
												</tr>
											</table>
										</HeaderTemplate>
										<ItemTemplate>
											<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate">
												<colgroup>
													<col width="10">
													<col width="20%">
													<col width="21%">
													<col width="20%">
													<col width="21%">
													<col width="18%">
													<col width="100">
												</colgroup>
												<tr>
													<td class="ms-vb-user">
														<input type="checkbox" id="chkList" runat="server" NAME="chkList" Visible="false">
													</td>
													<td class="ms-vb-user">
														<asp:HyperLink ID="lnkPONo" Runat="server"></asp:HyperLink>&nbsp;
													</td>
													<td class="ms-vb-user">
														<asp:Label ID="lblPODate" Runat="server"></asp:Label>
													</td>
													<td class="ms-vb-user">
														<asp:Label ID="lblDeliveryDate" Runat="server"></asp:Label>
													</td>
													<td class="ms-vb-user">
														<asp:Label ID="lblSupplierDRNo" Runat="server"></asp:Label>
													</td>
													<td class="ms-vb-user" align="right">
														<asp:Label ID="lblAmount" Runat="server"></asp:Label>
													</td>
													<td class="ms-vb2">
														<a class="DropDown" id="anchorDown1" href="" runat="server">
															<asp:Image id="divExpCollAsst_img1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></a>
													</td>
												</tr>
											</table>
										</ItemTemplate>
									</asp:datalist></td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="5">
									<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate">
										<colgroup>
											<col width="10">
											<col width="20%">
											<col width="21%">
											<col width="20%">
											<col width="21%">
											<col width="18%">
											<col width="100">
										</colgroup>
										<tr>
										    <td colspan=5></td>
										    <td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
										</tr>
										<tr>
											<td class="ms-vb-user">
											</td>
											<td class="ms-vb-user">
											</td>
											<td class="ms-vb-user">
											</td>
											<td class="ms-vb-user">
											</td>
											<td class="ms-vb-user">
											</td>
											<td class="ms-vb-user" align="right">
												<asp:Label ID="lblPOTotalAmount" Runat="server" Font-Bold="True"></asp:Label>
											</td>
											<td class="ms-vb2">
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="ms-sectionline" colspan="6" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td><asp:datalist id="lstPaymentsDebit" runat="server" Width="100%" ShowFooter="False" cellpadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate1">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<tr>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByDescription" runat="server">Account</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="SortByMatrixDescription" runat="server">Debit</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="SortByQuantity" runat="server">Credit</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</th>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate1">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblChartOfAccountCodeDebit" Runat="server"></asp:Label>&nbsp;
								<asp:Label ID="lblChartOfAccountNameDebit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblDebitAmountDebit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblCreditAmountDebit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
								<a class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></a>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td><asp:datalist id="lstPaymentsCredit" runat="server" Width="100%" ShowFooter="False" ShowHeader="False" cellpadding="0">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate2">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<tr>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="Hyperlink1" runat="server">Account</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
								<asp:hyperlink id="Hyperlink2" runat="server">Debit</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
								<asp:hyperlink id="Hyperlink3" runat="server">Credit</asp:hyperlink></th>
							<th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
							</th>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate2">
						<colgroup>
							<col width="10">
							<col width="50%">
							<col width="25%">
							<col width="25%">
							<col width="100">
						</colgroup>
						<tr>
							<td class="ms-vb-user">
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblChartOfAccountCodeCredit" Runat="server"></asp:Label>&nbsp;
								<asp:Label ID="lblChartOfAccountNameCredit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblDebitAmountCredit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb-user">
								<asp:Label ID="lblCreditAmountCredit" Runat="server"></asp:Label>
							</td>
							<td class="ms-vb2">
								<a class="DropDown" id="A1" href="" runat="server">
									<asp:Image id="Image1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></a>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td valign="top" colspan="3">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td style="padding-bottom: 2px" align="left"><label><b>Debit Amount:</b></label></td>
								<td style="padding-bottom: 2px" align="right"><asp:label id="lblTotalDebitAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td style="padding-bottom: 2px" align="left"><label><b>Credit Amount (-):</b></label></td>
								<td style="padding-bottom: 2px" align="right"><asp:label id="lblTotalCreditAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="70%"></td>
								<td style="padding-bottom: 2px" align="left"><label><b>Total:</b></label></td>
								<td style="padding-bottom: 2px" align="right"><asp:label id="lblTotalAmount" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
