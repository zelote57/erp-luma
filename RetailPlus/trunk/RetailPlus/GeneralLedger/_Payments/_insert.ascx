<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._Payments.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblPaymentID" runat="server" Visible="False">0</asp:label>
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
										Issue Payment</label></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"><label> Select Bank<FONT color="red">*</FONT></label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="padding-bottom: 2px" width="30%"><asp:dropdownlist id="cboBank" runat="server" CssClass="ms-short" AutoPostBack="True" onselectedindexchanged="cboBank_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Bank code' must not be left blank." Display="Dynamic" ControlToValidate="cboBank"></asp:requiredfieldvalidator></td>
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox id="txtChequeNo" runat="server" CssClass="ms-short"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboBank" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ControlToValidate="txtChequeNo" Display="Dynamic" ErrorMessage="'Cheque No.' must not be left blank."></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"><label>Cheque Date<FONT color="red">*</FONT> 
                                    <asp:Label ID="Label1" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:Label></label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td style="padding-bottom: 2px" width="30%"><asp:TextBox id="txtChequeDate" runat="server" CssClass="ms-short" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" CssClass="ms-error" ControlToValidate="txtChequeDate" Display="Dynamic" ErrorMessage="'Cheque Date' must not be left blank."></asp:requiredfieldvalidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtChequeDate"
                                        CssClass="ms-error" Display="Dynamic" ErrorMessage="'Cheque Date' must be a valid date."
                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
							</tr>
							<tr style="padding-bottom: 20px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Select 
										Payee:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><asp:label id="Label16" runat="server">Payee Name: </asp:label>
								</td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="2"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%">
									<asp:dropdownlist id="cboPayee" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPayee_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboPayee"></asp:requiredfieldvalidator></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%">
									<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
									        <asp:TextBox id="txtPayeeName" runat="server" CssClass="ms-long" Enabled="False"></asp:TextBox>
									    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboPayee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
							    </td>
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
									<asp:textbox id="txtRemarks" accessKey="R" runat="server" CssClass="ms-long" BorderStyle="Groove" TextMode="MultiLine" Width="100%" MaxLength="150" Rows="3"></asp:textbox></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label>
										or&nbsp;Select PO Numbers to Pay: </label>
								</td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="5">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:datalist id="lstPO" runat="server" Width="100%" CellPadding="0" ShowFooter="False">
										        <HeaderTemplate>
											        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
												        <colgroup>
													        <col width="10">
													        <col width="20%">
													        <col width="15%">
													        <col width="14%">
													        <col width="15%">
													        <col width="12%">
													        <col width="12%">
													        <col width="12%">
													        <col width="100">
												        </colgroup>
												        <tr>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
														        <!--INPUT id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall"-->
													        </TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
														        <asp:hyperlink id="SortByPONo" runat="server">PO No</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
														        <asp:hyperlink id="SortByPODate" runat="server">Order Date</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														        <asp:hyperlink id="SortByDeliveryDate" runat="server">Delivery Date</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														        <asp:hyperlink id="SortBySupplierDRNo" runat="server">Supplier DRNo.</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														        <asp:hyperlink id="SortByAmount" runat="server">Amount</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														        <asp:hyperlink id="SortPaidAmount" runat="server">Paid Amount</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
														        <asp:hyperlink id="SortByUnpaidAmount" runat="server">Balance</asp:hyperlink></TH>
													        <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
													        </TH>
												        </tr>
											        </table>
										        </HeaderTemplate>
										        <ItemTemplate>
											        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate">
												        <colgroup>
													        <col width="10">
													        <col width="20%">
													        <col width="15%">
													        <col width="14%">
													        <col width="15%">
													        <col width="12%">
													        <col width="12%">
													        <col width="12%">
													        <col width="100">
												        </colgroup>
												        <tr>
													        <td class="ms-vb-user">
														        <input type="checkbox" id="chkList" runat="server" name="chkList" />
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
													        <td class="ms-vb-user" align="right">
														        <asp:Label ID="lblPaidAmount" Runat="server"></asp:Label>
													        </td>
													        <td class="ms-vb-user" align="right">
														        <asp:Label ID="lblUnpaidAmount" Runat="server"></asp:Label>
													        </td>
													        <td class="ms-vb2">
														        <A class="DropDown" id="anchorDown1" href="" runat="server">
															        <asp:Image id="divExpCollAsst_img1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
													        </td>
												        </tr>
											        </table>
										        </ItemTemplate>
									        </asp:datalist>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboPayee" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
							</tr>
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="5">
									<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblAccountTemplate">
										<colgroup>
											<col width="10">
											<col width="20%">
											<col width="15%">
											<col width="14%">
											<col width="15%">
											<col width="12%">
											<col width="12%">
											<col width="12%">
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
								                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
									                    <asp:Label ID="lblPOTotalAmount" Runat="server" Font-Bold="True"></asp:Label>
									                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cboPayee" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
								            </td>
								            <td class="ms-vb-user" align="right">
								                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
									                    <asp:Label ID="lblPOTotalPaidAmount" Runat="server" Font-Bold="True"></asp:Label>
									                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cboPayee" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
								            </td>
								            <td class="ms-vb-user" align="right">
								                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
									                    <asp:Label ID="lblPOTotalUnpaidAmount" Runat="server" Font-Bold="True"></asp:Label>
									                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cboPayee" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
							<tr>
								<td style="padding-bottom: 4px;PADDING-TOP: 4px" valign="middle" colspan="6">
									<div class="ms-sectionheader" style="padding-bottom: 4px">
										Add Account Information</div>
								</td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top" colspan="6">
									<asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
									        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
										        <tr>
											        <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label> Select 
													        Account<font color="red">*</font></label></td>
											        <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label>Enter 
													        Amount<font color="red">*</font></label></td>
										        </tr>
										        <tr>
											        <td class="ms-formspacer" style="HEIGHT: 21px"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
											        <td class="ms-authoringcontrols" style="HEIGHT: 21px"><asp:dropdownlist id="cboAccount" runat="server" CssClass="ms-long"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboPayee"></asp:requiredfieldvalidator></td>
											        <td class="ms-formspacer" style="HEIGHT: 21px"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
											        <td class="ms-authoringcontrols" style="HEIGHT: 21px">
												        <asp:textbox onkeypress="AllNum()" id="txtAmount" accessKey="A" runat="server" CssClass="ms-short" BorderStyle="Groove">0.00</asp:textbox>
												        <asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" CssClass="ms-error" ErrorMessage="'Amount' must not be left blank." Display="Dynamic" ControlToValidate="txtAmount"></asp:requiredfieldvalidator></td>
										        </tr>
										        <tr>
											        <td class="ms-formspacer" colspan="4"></td>
										        </tr>
									        </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
							</tr>
							<tr>
								<td class="ms-sectionline" colspan="6" height="1"><A name="InputFormSection1"></A><img alt="" src="../../_layouts/images/empty.gif" /></td>
							</tr>
							<tr>
								<td class="ms-sectionline" colspan="6" height="1">
									<table class="ms-toolbar" id="twotidGrpsTB" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
										<tr>
											<td class="ms-toolbar" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
											</td>
											<td class="ms-toolbar">
												<table cellspacing="0" cellpadding="1" border="0">
													<tr>
														<td class="ms-toolbar">
															<table cellspacing="0" cellpadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Clear item and Load Defaults" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgClear_Click"></asp:imagebutton></td>
																	<td nowrap="nowrap"><asp:linkbutton id="cmdClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdClear_Click">Clear Account</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<td class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></td>
														<td class="ms-toolbar">
															<table cellspacing="0" cellpadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveDebit" title="Save As Debit" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save As Debit" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveDebit_Click"></asp:imagebutton>&nbsp;
																	</td>
																	<td nowrap="nowrap"><asp:linkbutton id="cmdSaveDebit" title="Save As Debit" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveDebit_Click">Save As Debit</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<td class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></td>
														<td class="ms-toolbar">
															<table cellspacing="0" cellpadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveCredit" title="Save As Credit" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save As Credit" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveCredit_Click"></asp:imagebutton>&nbsp;
																	</td>
																	<td nowrap="nowrap"><asp:linkbutton id="cmdSaveCredit" title="Save As Credit" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveCredit_Click">Save As Credit</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
														<td class="ms-toolbar">
															<table cellspacing="0" cellpadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Account" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Account" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
																	<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Account" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Account</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<td class="ms-separator"><asp:label id="Label3" runat="server">|</asp:label></td>
														<td class="ms-toolbar">
															<table cellspacing="0" cellpadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Account And Back To Payments List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Account And Back To Payments List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
																	<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Account And Back To Payments List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To Payments List</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
		    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstPaymentsDebit" runat="server" Width="100%" ShowFooter="False" CellPadding="0">
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
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <INPUT id="idSelectAll1" onclick="SelectAll();" type="checkbox" name="selectall">
					                </TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <asp:hyperlink id="SortByDescription" runat="server">Account</asp:hyperlink></TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <asp:hyperlink id="SortByMatrixDescription" runat="server">Debit</asp:hyperlink></TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
						                <asp:hyperlink id="SortByQuantity" runat="server">Credit</asp:hyperlink></TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
					                </TH>
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
						                <input type="checkbox" id="chkListDebit" runat="server" NAME="chkList">
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
						                <A class="DropDown" id="anchorDown" href="" runat="server">
							                <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
					                </td>
				                </tr>
			                </table>
		                </ItemTemplate>
	                </asp:datalist>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgSaveDebit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSaveDebit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgSaveCredit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSaveCredit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
		    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstPaymentsCredit" runat="server" Width="100%" ShowFooter="False" ShowHeader="False" CellPadding="0">
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
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <INPUT id="idSelectAllAccount" onclick="SelectAll();" type="checkbox" name="selectall">
					                </TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <asp:hyperlink id="Hyperlink1" runat="server">Account</asp:hyperlink></TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <asp:hyperlink id="Hyperlink2" runat="server">Debit</asp:hyperlink></TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px" align="right">
						                <asp:hyperlink id="Hyperlink3" runat="server">Credit</asp:hyperlink></TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
					                </TH>
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
						                <input type="checkbox" id="chkListCredit" runat="server" NAME="chkList">
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
						                <A class="DropDown" id="A1" href="" runat="server">
							                <asp:Image id="Image1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
					                </td>
				                </tr>
			                </table>
		                </ItemTemplate>
	                </asp:datalist>
	            </ContentTemplate>
	            <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgSaveDebit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSaveDebit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgSaveCredit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSaveCredit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
		</td>
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
						<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
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
				            </ContentTemplate>
				            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgSaveDebit" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdSaveDebit" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveCredit" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdSaveCredit" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                            </Triggers>
				       </asp:UpdatePanel>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
