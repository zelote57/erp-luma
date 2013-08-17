<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._GJournals.__Update" Codebehind="_Update.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/PurchasesAndPayables.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible=false></asp:label><asp:label id="lblGJournalID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td style="PADDING-BOTTOM: 10px" vAlign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%" rowSpan="1"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="2">
								    <label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Journal Entry</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="6"><label>
										Particulars:</label>
								</td>
							</tr>
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="4">
									<asp:textbox id="txtRemarks" accessKey="R" runat="server" CssClass="ms-long" BorderStyle="Groove" TextMode="MultiLine" Width="100%" MaxLength="150" Rows="3"></asp:textbox></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="15%"></td>
							</tr>
							<tr>
								<td class="ms-sectionline" colspan="6" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
							</tr>
							<tr>
								<td style="PADDING-BOTTOM: 4px;PADDING-TOP: 4px" vAlign="middle" colSpan="6">
									<div class="ms-sectionheader" style="PADDING-BOTTOM: 4px">
										Add Account Information</div>
								</td>
							</tr>
							<TR>
								<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top" colSpan="6">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
									        <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
										        <tr>
											        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label> Select 
													        Account<font color="red">*</font></label></td>
											        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Enter 
													        Amount<font color="red">*</font></label></td>
										        </tr>
										        <tr>
											        <td class="ms-formspacer" style="HEIGHT: 21px"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
											        <td class="ms-authoringcontrols" style="HEIGHT: 21px"><asp:dropdownlist id="cboAccount" runat="server" CssClass="ms-long"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Account code' must not be left blank." Display="Dynamic" ControlToValidate="cboAccount"></asp:requiredfieldvalidator></td>
											        <td class="ms-formspacer" style="HEIGHT: 21px"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
											        <td class="ms-authoringcontrols" style="HEIGHT: 21px">
												        <asp:textbox onkeypress="AllNum()" id="txtAmount" accessKey="A" runat="server" CssClass="ms-short" BorderStyle="Groove">0.00</asp:textbox>
												        <asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" CssClass="ms-error" ErrorMessage="'Amount' must not be left blank." Display="Dynamic" ControlToValidate="txtAmount"></asp:requiredfieldvalidator></td>
										        </tr>
										        <tr>
											        <td class="ms-formspacer" colSpan="4"></td>
										        </tr>
									        </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
							</TR>
							<tr>
								<td class="ms-sectionline" colSpan="6" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../_layouts/images/empty.gif"></td>
							</tr>
							<tr>
								<td class="ms-sectionline" colSpan="6" height="1">
									<table class="ms-toolbar" id="twotidGrpsTB" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
										<TR>
											<td class="ms-toolbar" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
											</td>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar">
															<table cellSpacing="0" cellPadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" noWrap><asp:imagebutton id="imgClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Clear item and Load Defaults" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgClear_Click"></asp:imagebutton></td>
																	<td noWrap><asp:linkbutton id="cmdClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdClear_Click">Clear Account</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<TD class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></TD>
														<td class="ms-toolbar">
															<table cellSpacing="0" cellPadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveDebit" title="Save As Debit" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save As Debit" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveDebit_Click"></asp:imagebutton>&nbsp;
																	</td>
																	<td noWrap><asp:linkbutton id="cmdSaveDebit" title="Save As Debit" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveDebit_Click">Save As Debit</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
														<td class="ms-toolbar">
															<table cellSpacing="0" cellPadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveCredit" title="Save As Credit" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save As Credit" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveCredit_Click"></asp:imagebutton>&nbsp;
																	</td>
																	<td noWrap><asp:linkbutton id="cmdSaveCredit" title="Save As Credit" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveCredit_Click">Save As Credit</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
														<td class="ms-toolbar">
															<table cellSpacing="0" cellPadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" noWrap><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Account" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Account" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
																	<td noWrap><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Account" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Account</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
														<td class="ms-toolbar">
															<table cellSpacing="0" cellPadding="1" border="0">
																<tr>
																	<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Account And Back To GJournals List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Account And Back To GJournals List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
																	<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Account And Back To GJournals List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To GJournals List</asp:linkbutton></td>
																</tr>
															</table>
														</td>
														<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
														</td>
													</tr>
												</table>
											</td>
										</TR>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td class="ms-sectionline" colSpan="6" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../_layouts/images/empty.gif"></td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</TD>
	</tr>
    <tr>
        <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
        <TD>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstGJournalsDebit" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstGJournalsDebit_ItemDataBound">
		                <HeaderTemplate>
			                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate1">
				                <colgroup>
					                <col width="10">
					                <col width="50%">
					                <col width="25%">
					                <col width="25%">
					                <col width="100">
				                </colgroup>
				                <TR>
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
				                </TR>
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
				                <TR>
					                <TD class="ms-vb-user">
						                <input type="checkbox" id="chkListDebit" runat="server" NAME="chkList">
					                </TD>
					                <TD class="ms-vb-user">
						                <asp:Label ID="lblChartOfAccountCodeDebit" Runat="server"></asp:Label>&nbsp;
						                <asp:Label ID="lblChartOfAccountNameDebit" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb-user">
						                <asp:Label ID="lblDebitAmountDebit" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb-user">
						                <asp:Label ID="lblCreditAmountDebit" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb2">
						                <A class="DropDown" id="anchorDown" href="" runat="server">
							                <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
					                </TD>
				                </TR>
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
        </TD>
        <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
    </tr>
    <tr>
        <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
        <TD>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstGJournalsCredit" runat="server" Width="100%" ShowFooter="False" ShowHeader="False" CellPadding="0" OnItemDataBound="lstGJournalsCredit_ItemDataBound">
		                <HeaderTemplate>
			                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate2">
				                <colgroup>
					                <col width="10">
					                <col width="50%">
					                <col width="25%">
					                <col width="25%">
					                <col width="100">
				                </colgroup>
				                <TR>
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
				                </TR>
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
				                <TR>
					                <TD class="ms-vb-user">
						                <input type="checkbox" id="chkListCredit" runat="server" NAME="chkList">
					                </TD>
					                <TD class="ms-vb-user">
						                <asp:Label ID="lblChartOfAccountCodeCredit" Runat="server"></asp:Label>&nbsp;
						                <asp:Label ID="lblChartOfAccountNameCredit" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb-user">
						                <asp:Label ID="lblDebitAmountCredit" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb-user">
						                <asp:Label ID="lblCreditAmountCredit" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb2">
						                <A class="DropDown" id="A1" href="" runat="server">
							                <asp:Image id="Image1" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
					                </TD>
				                </TR>
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
	    </TD>
        <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
    </tr>
    <tr>
        <td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
    </tr>
    <tr>
        <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
        <TD>
	        <table cellSpacing="0" cellPadding="0" width="100%" border="0">
		        <tr>
			        <td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
		        </tr>
		        <TR>
			        <td vAlign="top" colSpan="3">
			            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
				                <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
					                <tr style="PADDING-BOTTOM: 10px">
						                <TD class="ms-formspacer"></TD>
						                <TD width="70%"></TD>
						                <td style="PADDING-BOTTOM: 2px" align="left"><label><b>Debit Amount:</b></label></td>
						                <td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblTotalDebitAmount" runat="server" CssClass="ms-error"></asp:label></td>
					                </tr>
					                <tr style="PADDING-BOTTOM: 10px">
						                <TD class="ms-formspacer"></TD>
						                <TD width="70%"></TD>
						                <td style="PADDING-BOTTOM: 2px" align="left"><label><b>Credit Amount (-):</b></label></td>
						                <td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblTotalCreditAmount" runat="server" CssClass="ms-error"></asp:label></td>
					                </tr>
					                <tr>
						                <TD class="ms-formspacer"></TD>
						                <TD width="70%"></TD>
						                <td class="ms-sectionline" colspan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
					                </tr>
					                <tr style="PADDING-BOTTOM: 10px">
						                <TD class="ms-formspacer"></TD>
						                <TD width="70%"></TD>
						                <td style="PADDING-BOTTOM: 2px" align="left"><label><b>Total:</b></label></td>
						                <td style="PADDING-BOTTOM: 2px" align="right"><asp:label id="lblTotalAmount" runat="server" CssClass="ms-error"></asp:label></td>
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
		        </TR>
	        </table>
        </TD>
    </tr>
	<tr>
	    <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td class="ms-sectionline" height="1">
			<TABLE class="ms-toolbar" id="threetidGrpsTB" style="MARGIN-LEFT: 3px" cellSpacing="0" cellPadding="2" border="0">
				<TR>
					<td class="ms-toolbar" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
								</TD>
								<td class="ms-toolbar" align="center">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td class="ms-toolbar" noWrap>Posting Date :</td>
											<td noWrap><asp:textbox id="txtPostingDate" accessKey="Q" runat="server" CssClass="ms-short" BorderStyle="Groove" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)"></asp:textbox>
												<asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label></td> 
										</tr>
									</table>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ControlToValidate="txtPostingDate" Display="Dynamic" ErrorMessage="'Posting Date' must not be left blank."></asp:requiredfieldvalidator>
									<asp:CompareValidator id="CompareValidator1" runat="server" CssClass="ms-error" ControlToValidate="txtPostingDate" Display="Dynamic" ErrorMessage="'Posting Date' must be a valid date." Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
								</td>
								<TD class="ms-separator"><asp:label id="Label3" runat="server">|</asp:label></TD>
								<td class="ms-toolbar">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td class="ms-toolbar" noWrap style="height: 21px"><asp:imagebutton id="imgPost" ToolTip="Post Journal" accessKey="P" tabIndex="3" height="16" width="16" border="0" alt="Post Journal" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPost_Click"></asp:imagebutton></td>
											<td noWrap style="height: 21px"><asp:linkbutton id="cmdPost" ToolTip="Post Journal" accessKey="P" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdPost_Click">Post Journal</asp:linkbutton></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
