<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.SalesAndReceivables._Returns.__Post" Codebehind="_Post.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/SalesAndReceivables.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblCreditMemoID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<TR>
					<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 5px" vAlign="top" colspan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%" rowSpan="4"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Sales 
										Return</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Sales Return no:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"></TD>
								<TD class="ms-formspacer"></TD>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td style="PADDING-BOTTOM: 2px" width="30%"><asp:HyperLink id="lnkReturnNo" runat="server" CssClass="ms-error"></asp:Hyperlink></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer" style="HEIGHT: 52px"></TD>
								<TD class="ms-formspacer" style="HEIGHT: 52px"></TD>
								<td style="PADDING-BOTTOM: 2px; HEIGHT: 52px" vAlign="top" width="30%" colspan="2"><label>Date 
										Prepared: </label>
									<asp:label id="lblReturnDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Customer 
										Name:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="2"><label>Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="2"><label>Terms:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblCustomerID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lnkCustomerCode" runat="server"></asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%">
								    <asp:HyperLink id="lnkCustomerContact" runat="server" CssClass="ms-error"></asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
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
										Return Date:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblCustomerAddress" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblCustomerTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblRequiredReturnDate" runat="server" CssClass="ms-error"></asp:label></td>
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
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBranchCode" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblBranchID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colspan="3"><asp:label id="lblBranchAddress" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colspan="6"><label>Remarks:</label><asp:label id="lblReturnRemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1">
						<table class="ms-toolbar" id="twotidGrpsTB1" cellSpacing="0" cellPadding="2" border="0" width="100%">
							<TR>
								<TD class="ms-separator" width="99%"></TD>
								<td class="ms-toolbar">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="cmdUpdateHeader" ToolTip="Update Sales Return Header" accessKey="E" tabIndex="5" runat="server" CssClass="ms-toolbar" ImageUrl="../../_layouts/images/edit.gif" alt="Update Sales Return Header" border="0" width="16" height="16" OnClick="cmdUpdateHeader_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="imgUpdateHeader" ToolTip="Update Sales Return Header" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="imgUpdateHeader_Click">Update Sales Return Header</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label3" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap style="width: 19px"><asp:imagebutton id="imgPrint" ToolTip="Print this Sales Return" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Sales Return" ImageUrl="../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgPrint_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdPrint" ToolTip="Print this Sales Return" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label18" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" ToolTip="Cancel Adding New Item And Back To Return List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Item And Back To Return List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdCancel" ToolTip="Cancel Adding New Item And Back To Return List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To Return List</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<td class="ms-toolbar" id="align03" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
											</td>
										</tr>
									</table>
								</td>
								<td class="ms-toolbar" noWrap align="right" width="1%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 4px; PADDING-TOP: 4px" vAlign="top" colspan="3">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px; PADDING-TOP: 8px">Step 1: 
							Add Item Information</div>
					</td>
				</tr>
				<TR>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top" colspan="3">
		                <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
			                <tr>
				                <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Select 
						                Product Code<font color="red">*
							                <asp:label id="lblCreditMemoItemID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></font></label></td>
				                <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Select 
						                Variation<font color="red">*</font></label></td>
				                <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Select 
						                unit<font color="red">*</font></label></td>
			                </tr>
			                <tr>
				                <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                <td class="ms-authoringcontrols">
				                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
				                            <asp:dropdownlist id="cboProductCode" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboProductCode_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator>
				                        </ContentTemplate>
		                                <Triggers> 
		                                    <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
		                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
				                </td>
				                <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                <td class="ms-authoringcontrols">
				                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
			                                <asp:dropdownlist id="cboVariation" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboVariation_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Variation' must not be left blank." Display="Dynamic" ControlToValidate="cboVariation"></asp:requiredfieldvalidator>
			                            </ContentTemplate>
	                                    <Triggers> 
	                                        <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
	                                        <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
				                </td>
				                <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                <td class="ms-authoringcontrols">
				                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
				                            <asp:dropdownlist id="cboProductUnit" runat="server" CssClass="ms-short"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Unit' must not be left blank." Display="Dynamic" ControlToValidate="cboProductUnit"></asp:requiredfieldvalidator>
				                        </ContentTemplate>
		                                <Triggers> 
		                                    <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
		                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
				                </td>
			                </tr>
			                <TR>
				                <TD class="ms-formspacer"></TD>
				                <TD class="ms-authoringcontrols">
				                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
			                                <asp:textbox id="txtProductCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox>
			                                <asp:imagebutton id="cmdProductCode" ToolTip="Execute search" style="CURSOR: hand" accessKey="P" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False" OnClick="cmdProductCode_Click"></asp:imagebutton>
			                                <asp:ImageButton id="imgProductHistory" runat="server" Visible=false ImageUrl="../../_layouts/images/prodhist.gif" ToolTip="Show product inventory history report" CausesValidation=false Style="cursor: hand" OnClick="imgProductHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgProductPriceHistory" runat="server" Visible=false ImageUrl="../../_layouts/images/pricehist.gif" ToolTip="Show product price history report" CausesValidation=false Style="cursor: hand" OnClick="imgProductPriceHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgChangePrice" runat="server" Visible=false ImageUrl="../../_layouts/images/chprice.gif" ToolTip="Change price" CausesValidation=false Style="cursor: hand" OnClick="imgChangePrice_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgEditNow" runat="server" Visible=false ImageUrl="../../_layouts/images/edit.gif" ToolTip="Edit this product" CausesValidation=false Style="cursor: hand" OnClick="imgEditNow_Click" ></asp:ImageButton>
                                            <asp:HyperLink id="lnkAddProduct" runat="server" Visible=false ToolTip="Add new product">&nbsp;+&nbsp;</asp:HyperLink>
                                        </ContentTemplate>
	                                    <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
						        </TD>
				                <TD class="ms-formspacer"></TD>
				                <TD class="ms-authoringcontrols">
				                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:textbox id="txtVariation" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox>
				                            <asp:imagebutton id="cmdVariationSearch" runat="server" ImageUrl="../../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" style="CURSOR: hand" CssClass="ms-toolbar" BorderStyle="Groove" CausesValidation="False" OnClick="cmdVariationSearch_Click" Visible="false"></asp:imagebutton>
				                            <asp:imagebutton id="imgVariationQuickAdd" runat="server" ImageUrl="../../_layouts/images/quickadd.gif" ToolTip="Quickly add the new Product Variation (Make sure you selected a product to add this.)" style="CURSOR: hand" CssClass="ms-toolbar" BorderStyle="Groove" CausesValidation=false OnClick="imgVariationQuickAdd_Click" Visible="false"></asp:imagebutton>
				                            <asp:HyperLink id="lnkVariationAdd" runat="server" Visible=false ToolTip="Add new variation">&nbsp;+&nbsp;</asp:HyperLink>
				                        </ContentTemplate>
		                                <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
				                </TD>
				                <td class="ms-formspacer" colspan="2"></td>
			                </TR>
			                <tr>
				                <td class="ms-formspacer" colspan="6"></td>
			                </tr>
			                <tr>
				                <td class="ms-authoringcontrols" width="100%" colspan="6">
				                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
					                        <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
						                        <tr>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Enter 
									                        Quantity<font color="red">*</font></label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Enter 
									                        Unit Cost<font color="red">*</font></label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Enter 
									                        Discount<font color="red">*<INPUT id="chkInPercent" type="checkbox" onchange="ComputeAmountPost()" CHECKED name="chkInPercent" runat="server"></font></label><label style="FONT-SIZE: 10px">(check 
									                        if in percent)</label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="2"><label>Amount</label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" align="center"><label>Check 
									                        if taxable</label></td>
						                        </tr>
						                        <tr>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols"><asp:textbox onkeypress="AllNum()" id="txtQuantity" onkeyup="ComputeAmountPost()" accessKey="Q" runat="server" CssClass="ms-short" BorderStyle="Groove">1</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Quantity' must not be left blank." Display="Dynamic" ControlToValidate="txtQuantity"></asp:requiredfieldvalidator></td>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols"><asp:textbox onkeypress="AllNum()" id="txtPrice" onkeyup="ComputeAmountPost()" accessKey="P" runat="server" CssClass="ms-short" BorderStyle="Groove">0</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator8" runat="server" CssClass="ms-error" ErrorMessage="'Price' must not be left blank." Display="Dynamic" ControlToValidate="txtPrice"></asp:requiredfieldvalidator></td>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols"><asp:textbox onkeypress="AllNum()" id="txtDiscount" onkeyup="ComputeAmountPost()" accessKey="D" runat="server" CssClass="ms-short" BorderStyle="Groove">0</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Discount' must not be left blank." Display="Dynamic" ControlToValidate="txtDiscount"></asp:requiredfieldvalidator></td>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols"><asp:textbox onkeypress="AllNum()" id="txtAmount" onkeyup="ComputeAmountPost()" accessKey="A" runat="server" CssClass="ms-short" BorderStyle="Groove" ReadOnly="True">0</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" CssClass="ms-error" ErrorMessage="'Amount' must not be left blank." Display="Dynamic" ControlToValidate="txtAmount"></asp:requiredfieldvalidator></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" align="center"><INPUT id="chkIsTaxable" type="checkbox" CHECKED name="chkIsTaxable" runat="server" onchange="ComputeAmountPost()"></td>
						                        </tr>
					                        </table>
					                    </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>   
				                </td>
			                </tr>
			                <tr>
				                <td class="ms-formspacer" colspan="6"></td>
			                </tr>
			                <tr>
				                <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colspan="6"><label>Remarks</label></td>
			                </tr>
			                <tr>
				                <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                <td class="ms-authoringcontrols" colspan="5"><asp:textbox id="txtRemarks" accessKey="R" runat="server" CssClass="ms-long" BorderStyle="Groove" Rows="3" MaxLength="150" Width="100%" TextMode="MultiLine"></asp:textbox></td>
			                </tr>
			                <tr>
				                <td class="ms-formspacer" colspan="6"></td>
			                </tr>
		                </table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1">
						<table class="ms-toolbar" id="twotidGrpsTB3" cellSpacing="0" cellPadding="2" border="0" width="100%">
							<TR>
								<td class="ms-toolbar" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-toolbar">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgClear" ToolTip="Clear item and Load Defaults" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Clear item and Load Defaults" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgClear_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdClear" ToolTip="Clear item and Load Defaults" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdClear_Click">Clear Item & Load Defaults</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" ToolTip="Save Item" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save Item" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
														</td>
														<td noWrap><asp:linkbutton id="cmdSave" ToolTip="Save Item" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save Item</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap style="height: 21px"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Item" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Item" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgDelete_Click"></asp:imagebutton></td>
														<td noWrap style="height: 21px"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Item" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdDelete_Click">Remove Selected Item</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label1" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgEdit" ToolTip="Update Item" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Item" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgEdit_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdEdit" ToolTip="Update Item" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdEdit_Click">Update Item</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<td class="ms-toolbar" id="align04" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
											</td>
										</tr>
									</table>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
		    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" AlternatingItemStyle-CssClass="ms-alternating">
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
						                <input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
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
						                <input type="checkbox" id="chkList" runat="server" name="chkList" />
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
						                <asp:Label ID="lblDiscountApplied" Runat="server"></asp:Label>
						                <asp:Label ID="lblPercent" Runat="server" Visible="False">%</asp:Label>
					                </TD>
					                <TD class="ms-vb-user" align="right">
						                <asp:Label ID="lblAmount" Runat="server"></asp:Label>
					                </TD>
					                <TD class="ms-vb2">
						                <A class="DropDown" id="anchorDown" href="" runat="server">
							                <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
					                </TD>
				                </TR>
				                <TR>
					                <TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
					                <TD colspan="7" height="1">
						                <DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
							                <asp:panel id="panItem" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
								                <table id="tblpanItem" cellSpacing="0" cellPadding="0" width="100%" border="0">
									                <TR>
										                <TD class="ms-formspacer" colspan="1"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
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
											                <asp:Label id="Label13" CssClass="ms-vh2" runat="server" text="<b>VAT Inclusive</b>"></asp:Label>
										                </TD>
										                <TD width="1%">
											                <asp:Label id="Label14" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
										                </TD>
										                <TD width="40%">
											                <asp:Label id="lblisVATInclusive" CssClass="ms-vb2" runat="server"></asp:Label>
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
					                <TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
				                </TR>
			                </table>
		                </ItemTemplate>
	                </asp:datalist>
	             </ContentTemplate>
	             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdPost" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgPost" EventName="Click" />
                </Triggers> 
            </asp:UpdatePanel>
		</TD>
		<td><a name="itemsection"></a><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<TR>
					<td vAlign="top" colspan="3">
						<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
		                        <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
			                        <tr style="PADDING-BOTTOM: auto">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount:</label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtSODiscountApplied" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text=0 Width="82px" AutoPostBack="True" OnTextChanged="txtSODiscountApplied_TextChanged"></asp:textbox><asp:dropdownlist id="cboSODiscountType" runat="server" CssClass="ms-short" AutoPostBack="True" OnSelectedIndexChanged="cboSODiscountType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">NA</asp:ListItem>
                                            <asp:ListItem Value="1">amt</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ControlToValidate="txtSODiscountApplied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Subtotal Discount:</b></label></td>
				                        <td align="right"><asp:label id="lblSODiscount" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label><b>VATable Amount:</b></label></td>
				                        <td align="right"><asp:label id="lblSOVatableAmount" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Subtotal:</b></label></td>
				                        <td align="right"><asp:label id="lblSOSubTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label><b>VAT:</b></label></td>
				                        <td align="right"><asp:label id="lblSOVAT" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Freight:</b></label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtSOFreight" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" AutoPostBack="True" OnTextChanged="txtSOFreight_TextChanged"></asp:textbox>
				                                                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" CssClass="ms-error" ControlToValidate="txtSOFreight" Display="Dynamic" ErrorMessage="'Freight' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label><b>Deposit:</b></label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtSODeposit" accessKey="" runat="server" CssClass="ms-short" BorderStyle="Groove" Text="0.00" AutoPostBack="True" OnTextChanged="txtSODeposit_TextChanged"></asp:textbox>
				                                                                       <asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ControlToValidate="txtSODeposit" Display="Dynamic" ErrorMessage="'Deposit' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr>
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Total:</b></label></td>
				                        <td align="right"><asp:label id="lblSOTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
		                        </table>
		                    </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSave" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdPost" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgPost" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
					</td>
				</TR>
			</table>
		</TD>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD id="AddUserTextTDID2">
			<table class="ms-toolbar" id="onetidGrpsTC" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
					</TD>
					<td class="ms-toolbar" align="center">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap>Posting Date :</td>
								<td noWrap><asp:textbox id="txtPostDate" accessKey="Q" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox>
									<asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label></td>
							</tr>
						</table>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ControlToValidate="txtPostDate" Display="Dynamic" ErrorMessage="'Return Date' must not be left blank."></asp:requiredfieldvalidator>
						<asp:CompareValidator id="CompareValidator1" runat="server" CssClass="ms-error" ControlToValidate="txtPostDate" Display="Dynamic" ErrorMessage="'Return Date' must be a valid date." Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
					</td>
					<TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
					<td class="ms-toolbar" align="center">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap>Customer Doc No:</td>
								<td noWrap><asp:textbox id="txtCustomerDocNo" accessKey="Q" runat="server" CssClass="ms-short" BorderStyle="Groove">NA</asp:textbox></td>
							</tr>
						</table>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" CssClass="ms-error" ControlToValidate="txtCustomerDocNo" Display="Dynamic" ErrorMessage="'Return No' must not be left blank."></asp:requiredfieldvalidator>
					</td>
					<TD class="ms-separator"><asp:label id="Label16" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap style="height: 21px"><asp:imagebutton id="imgPost" title="Post" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/post.gif" alt="Post" border="0" width="16" height="16" OnClick="imgPost_Click"></asp:imagebutton></td>
								<td noWrap style="height: 21px"><asp:linkbutton id="cmdPost" title="Post" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdPost_Click">Post</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align052" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><a name="postsection"></a><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
</table>

