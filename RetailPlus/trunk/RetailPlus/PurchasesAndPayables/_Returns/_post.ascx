<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.PurchasesAndPayables._Returns.__Post" Codebehind="_Post.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/PurchasesAndPayables.js"></script>
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
						Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblDebitMemoID" runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<TR>
					<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 5px" vAlign="top" colSpan="3">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr style="PADDING-BOTTOM: 10px">
								<TD class="ms-formspacer"></TD>
								<td width="30%" rowSpan="4"><IMG alt="" src="../../_layouts/images/company_logo.gif"></td>
								<TD class="ms-formspacer"></TD>
								<td style="HEIGHT: 70px" borderColor="white" align="center" width="40%" rowSpan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Purchase 
										Return</label></td>
								<td style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Purchase Return no:</label></td>
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
								<td style="PADDING-BOTTOM: 2px; HEIGHT: 52px" vAlign="top" width="30%" colSpan="2"><label>Date 
										Prepared: </label>
									<asp:label id="lblReturnDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 20px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Supplier 
										Name:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="2"><label>Contact:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Terms:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSupplierID" runat="server" CssClass="ms-error" Visible="False"></asp:label>
									<asp:HyperLink id="lnkSupplierCode" runat="server"></asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%">
								    <asp:HyperLink id="lnkSupplierContact" runat="server" CssClass="ms-error"></asp:HyperLink></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblTerms" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblModeOfterms" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Supplier 
										Address:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="2"><label>Telephone 
										no:</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Required 
										Return Date:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblSupplierAddress" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%"><asp:label id="lblSupplierTelephoneNo" runat="server" CssClass="ms-error"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblRequiredReturnDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="2"><label>Deliver 
										to branch: (Specify complete address)</label></td>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="4"><label>Branch 
										Address:</label></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%"><asp:label id="lblBranchCode" runat="server" CssClass="ms-error"></asp:label><asp:label id="lblBranchID" runat="server" CssClass="ms-error" Visible="False"></asp:label></td>
								<TD class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="40%" colSpan="3"><asp:label id="lblBranchAddress" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colSpan="6"></td>
							</tr>
							<tr style="PADDING-BOTTOM: 5px">
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" width="30%" colSpan="6"><label>Remarks:</label><asp:label id="lblReturnRemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1">
						<table class="ms-toolbar" id="twotidGrpsTB1" cellSpacing="0" cellPadding="2" border="0" width="100%">
							<TR>
								<TD class="ms-separator" width="99%"></TD>
								<td class="ms-toolbar">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="cmdUpdateHeader" title="Update Purchase Return Header" accessKey="E" tabIndex="5" runat="server" CssClass="ms-toolbar" ImageUrl="../../_layouts/images/edit.gif" alt="Update Purchase Return Header" border="0" width="16" height="16" OnClick="cmdUpdateHeader_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="imgUpdateHeader" title="Update Purchase Return Header" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="imgUpdateHeader_Click">Update Purchase Return Header</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label3" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap style="width: 19px"><asp:imagebutton id="imgPrint" title="Print this Purchase Return" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Purchase Return" ImageUrl="../../_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgPrint_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdPrint" title="Print this Purchase Return" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label18" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Item And Back To Return List" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Item And Back To Return List" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Item And Back To Return List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To Return List</asp:linkbutton></td>
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
					<td style="PADDING-BOTTOM: 4px; PADDING-TOP: 4px" vAlign="top" colSpan="3">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px; PADDING-TOP: 8px">Step 1: 
							Add Item Information</div>
					</td>
				</tr>
				<TR>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top" colSpan="3">
		                <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
					        <tr>
						        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Select 
								        Product Code<font color="red">*
									        <asp:label id="lblPODebitMemoItemID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></font></label></td>
						        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Select 
								        Variation<font color="red">*</font></label></td>
						        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Select 
								        unit<font color="red">*</font></label></td>
					        </tr>
					        <tr>
						        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
						        <td class="ms-authoringcontrols">
						            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
						                    <asp:dropdownlist id="cboProductCode" runat="server" CssClass="ms-long" width="70%" AutoPostBack="True" onselectedindexchanged="cboProductCode_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator>
						                    <asp:ImageButton id="imgProductHistory" runat="server" Visible=false ImageUrl="../../_layouts/images/prodhist.gif" ToolTip="Show product inventory history report" CausesValidation=false Style="cursor: hand" OnClick="imgProductHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgProductPriceHistory" runat="server" Visible=false ImageUrl="../../_layouts/images/pricehist.gif" ToolTip="Show product price history report" CausesValidation=false Style="cursor: hand" OnClick="imgProductPriceHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgChangePrice" runat="server" Visible=false ImageUrl="../../_layouts/images/chprice.gif" ToolTip="Change price" CausesValidation=false Style="cursor: hand" OnClick="imgChangePrice_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgEditNow" runat="server" Visible=false ImageUrl="../../_layouts/images/edit.gif" ToolTip="Edit this product" CausesValidation=false Style="cursor: hand" OnClick="imgEditNow_Click" ></asp:ImageButton>
						                </ContentTemplate>
				                        <Triggers> 
				                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
				                            <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                            <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <%--<%--<asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgItemUpdate" EventName="Click" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
						        </td>
						        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
						        <td class="ms-authoringcontrols">
						            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
						                    <asp:dropdownlist id="cboVariation" runat="server" CssClass="ms-long" Width="70%" AutoPostBack="True" onselectedindexchanged="cboVariation_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Variation' must not be left blank." Display="Dynamic" ControlToValidate="cboVariation"></asp:requiredfieldvalidator>
						                </ContentTemplate>
				                        <Triggers> 
				                            <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                            <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgVariationQuickAdd" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
						        </td>
						        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
						        <td class="ms-authoringcontrols">
						            <asp:UpdatePanel ID="UpdatePanel6" runat="server"><ContentTemplate>
                                        <asp:dropdownlist id="cboProductUnit" runat="server" CssClass="ms-short" AutoPostBack="True" CausesValidation=false OnSelectedIndexChanged="cboProductUnit_SelectedIndexChanged">
                                        </asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ControlToValidate="cboProductUnit" Display="Dynamic" ErrorMessage="'Unit' must not be left blank."></asp:requiredfieldvalidator> 
                                    </ContentTemplate>
                                    <Triggers>
                                            
                                        <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
	                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                        <%--<asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="imgVariationQuickAdd" EventName="Click" />
                                    </Triggers>
                                    </asp:UpdatePanel>
						        </td>
					        </tr>
					        <TR>
						        <TD class="ms-formspacer"></TD>
						        <TD class="ms-authoringcontrols" vAlign="top">
						            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
						                    <asp:textbox id="txtProductCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
						                    <asp:imagebutton id="cmdProductCode" ToolTip="Execute search" style="CURSOR: hand" accessKey="P" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False" OnClick="cmdProductCode_Click"></asp:imagebutton>
                                            <asp:ImageButton ID="imgProductQuickAdd" runat="server" BorderStyle="Groove" CausesValidation="false"
                                                CssClass="ms-toolbar" ImageUrl="../../_layouts/images/quickadd.gif" OnClick="imgProductQuickAdd_Click"
                                                Style="cursor: hand" ToolTip="Quickly add a new Product"/>
                                            <asp:HyperLink id="lnkProductDetails" runat="server" Visible=false ToolTip="View product details">[view details]</asp:HyperLink>
                                            <asp:HyperLink id="lnkAddProduct" runat="server" ToolTip="Add new product">[add new]</asp:HyperLink>
                                            <asp:Label id="lblPurchasePriceHistory" runat="server" Visible=false CssClass="ms-error"></asp:Label>
                                        </ContentTemplate>
				                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
						        </TD>
						        <TD class="ms-formspacer"></TD>
						        <TD class="ms-authoringcontrols" vAlign="top">
						            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:textbox id="txtVariation" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" ondblclick="ontime(this)" ToolTip="Double Click to Select Date From Calendar"></asp:textbox>
						                    <asp:imagebutton id="cmdVariationSearch" runat="server" ImageUrl="../../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" style="CURSOR: hand" CssClass="ms-toolbar" BorderStyle="Groove" CausesValidation="False" OnClick="cmdVariationSearch_Click" Visible="false"></asp:imagebutton>
						                    <asp:imagebutton id="imgVariationQuickAdd" runat="server" ImageUrl="../../_layouts/images/quickadd.gif" ToolTip="Quickly add the new Product Variation (Make sure you selected a product to add this.)" style="CURSOR: hand" CssClass="ms-toolbar" BorderStyle="Groove" CausesValidation=false OnClick="imgVariationQuickAdd_Click" Visible="false"></asp:imagebutton>
						                    <asp:HyperLink id="lnkVariationAdd" runat="server" Visible=false ToolTip="Add new variation">[add new]</asp:HyperLink>
						                </ContentTemplate>
				                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
						        </TD>
						        <TD class="ms-formspacer"></TD>
						        <TD class="ms-authoringcontrols" vAlign="top">
						            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
						                    <asp:HyperLink id="lnkProductUnitMatrix" runat="server" Visible=false ToolTip="View Product Unit Matrix">[View Unit Matrix]</asp:HyperLink>
						                </ContentTemplate>
				                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
						        </TD>
					        </TR>
			                <tr>
				                <td class="ms-formspacer" colSpan="6"></td>
			                </tr>
			                <tr style="PADDING-TOP: 20px">
				                <td class="ms-authoringcontrols" width="100%" colSpan="6">
				                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
					                        <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
						                        <tr valign="top">
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Enter Quantity / RID<font color="red">*</font></label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Enter Unit Cost<font color="red">*</font></label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Enter Discount<font color="red">*</td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Amount</label></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"></td>
						                        </tr>
						                        <tr valign="top">
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols">
							                            <asp:textbox onkeypress="AllNum()" id="txtQuantity" onkeyup="ComputeAmountPost()" accessKey="Q" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" >1</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Quantity' must not be left blank." Display="Dynamic" ControlToValidate="txtQuantity"></asp:requiredfieldvalidator>
							                        </td>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols"><asp:textbox onkeypress="AllNum()" id="txtPrice" onkeyup="ChangePriceComputeMarginMPPO(this)" accessKey="P" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Width="80%">0</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator8" runat="server" CssClass="ms-error" ErrorMessage="'Price' must not be left blank." Display="Dynamic" ControlToValidate="txtPrice"></asp:requiredfieldvalidator></td>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols">
							                            <asp:textbox onkeypress="AllNum()" id="txtDiscount" onkeyup="ComputeAmountPost()" accessKey="D" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Width="50%">0</asp:textbox>
							                            <INPUT id="chkInPercent" type="checkbox" onchange="ComputeAmountPost()" CHECKED name="chkInPercent" runat="server" /><label style="FONT-SIZE: 10px">(in percent)</label>
							                            <asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Discount' must not be left blank." Display="Dynamic" ControlToValidate="txtDiscount"></asp:requiredfieldvalidator>
							                        </td>
							                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
							                        <td class="ms-authoringcontrols"><asp:textbox onkeypress="AllNum()" id="txtAmount" accessKey="A" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" ReadOnly="True" Width="80%">0</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" CssClass="ms-error" ErrorMessage="'Amount' must not be left blank." Display="Dynamic" ControlToValidate="txtAmount"></asp:requiredfieldvalidator></td>
							                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" align="center" colspan=2>
							                            <INPUT id="chkIsTaxable" type="checkbox" CHECKED name="chkIsTaxable" runat="server" onchange="ComputeAmountPost()" />
							                            <label>Check if taxable</label>
							                        </td>
						                        </tr>
						                        <tr>
						                            <td colspan=10 style="PADDING-BOTTOM: 5px; PADDING-TOP: 5px" ><label class="ms-error">Change the figures below to update the selling information.</label></td>
						                        </tr>
						                        <tr valign="top">
				                                    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">Margin in Percent (%) <font color="red">*</font></td>
				                                    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">Selling Price / Prev. Selling Price<font color="red">*</font></td>
				                                    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">VAT<font color="red">*</font></td>
				                                    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">EVAT<font color="red">*</font></td>
				                                    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2">Local Tax<font color="red">*</font></td>
			                                    </tr>
			                                    <tr valign="top">
				                                    <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                                    <td class="ms-authoringcontrols">
				                                        <asp:textbox onkeypress="AllNum()" id="txtSellingQuantity" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Enabled=false Visible=false>1</asp:textbox>
				                                        <asp:textbox onkeypress="AllNum()" id="txtMargin" onkeyup="ChangePriceComputeMarginPPPO(this)" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" BackColor="YellowGreen" Width="80%">0</asp:textbox>
				                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator14" runat="server" CssClass="ms-error" ErrorMessage="'Price' must not be left blank." Display="Dynamic" ControlToValidate="txtMargin"></asp:requiredfieldvalidator>
				                                    </td>
				                                    <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                                    <td class="ms-authoringcontrols">
				                                        <asp:textbox onkeypress="AllNum()" id="txtSellingPrice" onkeyup="ChangePriceComputeMarginByPriceMPPO(this)" runat="server" width="31%" CssClass="ms-short-numeric" BorderStyle="Groove" BackColor="YellowGreen">0</asp:textbox>
				                                        <button name="cmdRevertSellingPrice" id="cmdRevertSellingPrice" causesvalidation="false" runat="server" onclick="RevertSellingPrice()"> <- </button>
				                                        <asp:textbox onkeypress="AllNum()" id="txtOldSellingPrice" runat="server" width="31%" CssClass="ms-short-numeric" BorderStyle="Groove" BackColor="YellowGreen" ReadOnly="true" Text="0.00"></asp:textbox>
				                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator15" runat="server" CssClass="ms-error" ErrorMessage="'Discount' must not be left blank." Display="Dynamic" ControlToValidate="txtDiscount"></asp:requiredfieldvalidator>
				                                    </td>
				                                    <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                                    <td class="ms-authoringcontrols">
				                                        <asp:TextBox accessKey="C" id="txtVAT" runat="server" onkeypress="AllNum()" CssClass="ms-short-numeric" BorderStyle="Groove" MaxLength="6" Width="80%" BackColor="YellowGreen"></asp:TextBox>%&nbsp;&nbsp;&nbsp;
				                                                        
				                                    </td>
				                                    <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                                    <td class="ms-authoringcontrols">
				                                        <asp:TextBox accessKey="C" id="txtEVAT" runat="server" onkeypress="AllNum()" CssClass="ms-short-numeric" BorderStyle="Groove" MaxLength="6" Width="80%" BackColor="YellowGreen"></asp:TextBox>%
				                                    </td>
				                                    <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                                    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px">
				                                        <asp:TextBox accessKey="C" id="txtLocalTax" runat="server" onkeypress="AllNum()" CssClass="ms-short-numeric" BorderStyle="Groove" MaxLength="6" Width="80%" BackColor="YellowGreen"></asp:TextBox>%
				                                    </td>
			                                    </tr>
						                        <tr>
						                            <td colspan=10>
						                                <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
						                                                    
						                                </table>
						                            </td>
						                        </tr>
					                        </table>
					                    </ContentTemplate>
				                        <Triggers> 
				                            <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                            <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgVariationQuickAdd" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>       
				                </td>
			                </tr>
			                <tr>
				                <td class="ms-formspacer" colSpan="6"></td>
			                </tr>
			                        <tr>
				                        <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="6"><label>Remarks</label></td>
			                        </tr>
			                        <tr>
				                        <td class="ms-formspacer"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></td>
				                        <td class="ms-authoringcontrols" colSpan="5">
				                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
				                                <asp:textbox id="txtRemarks" accessKey="R" runat="server" CssClass="ms-long" BorderStyle="Groove" Rows="2" MaxLength="150" Width="100%" TextMode="MultiLine"></asp:textbox>
				                            </ContentTemplate>
				                                <Triggers> 
				                                    <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="imgVariationQuickAdd" EventName="Click" />
                                                </Triggers> 
                                            </asp:UpdatePanel>  
				                        </td>
			                        </tr>
			                            
					        <tr>
						        <td class="ms-formspacer" colSpan="6"></td>
					        </tr>
				        </table>
					</td>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1">
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
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Clear item and Load Defaults" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgClear_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdClear" title="Clear item and Load Defaults" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdClear_Click">Clear Item & Load Defaults</asp:linkbutton></td>
													</tr>
												</table>
											</td>
											<TD class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></TD>
											<td class="ms-toolbar">
												<table cellSpacing="0" cellPadding="1" border="0">
													<tr>
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" title="Save Item" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save Item" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
														</td>
														<td noWrap><asp:linkbutton id="cmdSave" title="Save Item" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save Item</asp:linkbutton></td>
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
														<td class="ms-toolbar" noWrap><asp:imagebutton id="imgEdit" title="Update Item" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Item" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgEdit_Click"></asp:imagebutton></td>
														<td noWrap><asp:linkbutton id="cmdEdit" title="Update Item" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdEdit_Click">Update Item</asp:linkbutton></td>
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
                    <asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemCommand="lstItem_ItemCommand" OnItemDataBound="lstItem_ItemDataBound" AlternatingItemStyle-CssClass="ms-alternating">
		                <HeaderTemplate>
			                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
				                <colgroup>
					                <col width="10">
					                <col width="10">
					                <col width="10">
					                <col width="40%">
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
						                &nbsp;&nbsp;&nbsp;&nbsp;
					                </TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                &nbsp;&nbsp;&nbsp;&nbsp;
					                </TH>
					                <TH class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px">
						                <asp:hyperlink id="SortByDescription" runat="server">Description</asp:hyperlink></TH>
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
					                <col width="10">
					                <col width="10">
					                <col width="40%">
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
						                <asp:ImageButton id="imgItemUpdate" runat="server" ImageUrl="../../_layouts/images/edit.gif" ToolTip="Update Item" CommandName="imgItemUpdateClick" CausesValidation=false></asp:ImageButton>						                
					                </TD>
					                <TD class="ms-vb-user">
					                    <asp:Label id="lblPODebitItemReceivedStatus" CssClass="ms-vh2" runat="server" text="0" Visible=false></asp:Label>
						                <asp:ImageButton id="imgItemReceive" runat="server" ImageUrl="../../_layouts/images/post.gif" ToolTip="Update Item" CommandName="imgItemReceive" CausesValidation=false></asp:ImageButton>
					                </TD>
					                <TD class="ms-vb-user">
					                    <asp:HyperLink ID="lnkBarcode" Runat="server"></asp:HyperLink>
						                <asp:HyperLink ID="lnkDescription" Runat="server"></asp:HyperLink>
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
					                <TD colSpan="8" height="1">
						                <DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
							                <asp:panel id="panItem" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
								                <TABLE id="tblpanItem" cellSpacing="0" cellPadding="0" width="100%" border="0">
									                <TR>
										                <TD class="ms-formspacer" colSpan="1"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
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
						<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
		                        <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
		                            <tr style="PADDING-BOTTOM: 15px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; <b>Total Before Discount:</b></label></td>
				                        <td align="right"><asp:label id="lblTotalDiscount1" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: auto">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount (1):</label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODebitMemoDiscountApplied" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text=0 Width="82px" AutoPostBack="True" OnTextChanged="txtPODebitMemoDiscountApplied_TextChanged"></asp:textbox><asp:dropdownlist id="cboPODebitMemoDiscountType" runat="server" CssClass="ms-short" AutoPostBack="True" OnSelectedIndexChanged="cboPODebitMemoDiscountType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">NA</asp:ListItem>
                                            <asp:ListItem Value="1">amt</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ControlToValidate="txtPODebitMemoDiscountApplied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 15px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Subtotal Discount (1):</label></td>
				                        <td align="right"><asp:label id="lblPODebitMemoDiscount" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr>
				                        <TD></TD>
				                        <TD></TD>
				                        <td class="ms-sectionline" colspan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 15px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; <b>Total Before Discount:</b></label></td>
				                        <td align="right"><asp:label id="lblTotalDiscount2" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: auto">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount (2):</label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODebitMemoDiscount2Applied" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text=0 Width="82px" AutoPostBack="True" OnTextChanged="txtPODebitMemoDiscountApplied_TextChanged"></asp:textbox><asp:dropdownlist id="cboPODebitMemoDiscount2Type" runat="server" CssClass="ms-short" AutoPostBack="True" OnSelectedIndexChanged="cboPODebitMemoDiscountType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">NA</asp:ListItem>
                                            <asp:ListItem Value="1">amt</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator16" runat="server" CssClass="ms-error" ControlToValidate="txtPODebitMemoDiscount2Applied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 15px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Subtotal Discount (2):</b></label></td>
				                        <td align="right"><asp:label id="lblPODebitMemoDiscount2" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr>
				                        <TD></TD>
				                        <TD></TD>
				                        <td class="ms-sectionline" colspan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 15px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; <b>Total Before Discount:</b></label></td>
				                        <td align="right"><asp:label id="lblTotalDiscount3" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: auto">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount (3):</label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODebitMemoDiscount3Applied" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text=0 Width="82px" AutoPostBack="True" OnTextChanged="txtPODebitMemoDiscountApplied_TextChanged"></asp:textbox><asp:dropdownlist id="cboPODebitMemoDiscount3Type" runat="server" CssClass="ms-short" AutoPostBack="True" OnSelectedIndexChanged="cboPODebitMemoDiscountType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">NA</asp:ListItem>
                                            <asp:ListItem Value="1">amt</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator17" runat="server" CssClass="ms-error" ControlToValidate="txtPODebitMemoDiscount3Applied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 25px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label>   &nbsp; &nbsp; Subtotal Discount (3):</label></td>
				                        <td align="right"><asp:label id="lblPODebitMemoDiscount3" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr>
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td class="ms-sectionline" colSpan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
			                        </tr>
			                        <tr style="PADDING-TOP: 5px;PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label><b>VATable Amount:</b></label><asp:CheckBox ID="chkIsVatInclusive" runat="server" Checked="true" Text="(Is-Vat-Inclusive)" OnCheckedChanged="chkIsVatInclusive_CheckedChanged" AutoPostBack="true"/></td>
				                        <td align="right"><asp:label id="lblPODebitMemoVatableAmount" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Subtotal:</b></label></td>
				                        <td align="right"><asp:label id="lblPODebitMemoSubTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label><b>VAT:</b></label></td>
				                        <td align="right"><asp:label id="lblPODebitMemoVAT" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Freight:</b></label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODebitMemoFreight" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text="0.00" AutoPostBack="True" OnTextChanged="txtPODebitMemoFreight_TextChanged"></asp:textbox>
				                                                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" CssClass="ms-error" ControlToValidate="txtPODebitMemoFreight" Display="Dynamic" ErrorMessage="'Freight' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td align="left"><label><b>PODebitMemo Deposit:</b></label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtPODebitMemoDeposit" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text="0.00" AutoPostBack="True" OnTextChanged="txtPODebitMemoDeposit_TextChanged"></asp:textbox>
				                                                                       <asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ControlToValidate="txtPODebitMemoDeposit" Display="Dynamic" ErrorMessage="'PODebitMemo Deposit' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr>
				                        <TD class="ms-formspacer"></TD>
				                        <TD></TD>
				                        <td class="ms-sectionline" colSpan="2" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
			                        </tr>
			                        <tr style="PADDING-BOTTOM: 5px">
				                        <TD class="ms-formspacer"></TD>
				                        <TD width="50%"></TD>
				                        <td align="left"><label><b>Total:</b></label></td>
				                        <td align="right"><asp:label id="lblPODebitMemoTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
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
        <TD>
	        <table class="ms-toolbar" id="onetidGrpsTC" cellSpacing="0" cellPadding="2" border="0" width="100%">
		        <TR>
			        <TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
			        </TD>
			        <td class="ms-toolbar" align="center">
				        <table cellSpacing="0" cellPadding="1" border="0">
					        <tr>
						        <td class="ms-toolbar" noWrap>Delivery Date :</td>
						        <td noWrap><asp:textbox id="txtPostDate" accessKey="Q" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox>
							        <asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label></td>
					        </tr>
				        </table>
				        <asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ControlToValidate="txtPostDate" Display="Dynamic" ErrorMessage="'Delivery Date' must not be left blank."></asp:requiredfieldvalidator>
				        <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="ms-error" ControlToValidate="txtPostDate" Display="Dynamic" ErrorMessage="'Delivery Date' must be a valid date." Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
			        </td>
			        <TD class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></TD>
			        <td class="ms-toolbar" align="center">
				        <table cellSpacing="0" cellPadding="1" border="0">
					        <tr>
						        <td class="ms-toolbar" noWrap>Supplier Delivery No:</td>
						        <td noWrap><asp:textbox id="txtSupplierDocNo" accessKey="Q" runat="server" CssClass="ms-short" BorderStyle="Groove">NA</asp:textbox></td>
					        </tr>
				        </table>
				        <asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" CssClass="ms-error" ControlToValidate="txtSupplierDocNo" Display="Dynamic" ErrorMessage="'Delivery No' must not be left blank."></asp:requiredfieldvalidator>
			        </td>
			        <TD class="ms-separator"><asp:label id="Label16" runat="server">|</asp:label></TD>
			        <td class="ms-toolbar">
				        <table cellSpacing="0" cellPadding="1" border="0">
					        <tr>
						        <td class="ms-toolbar" noWrap><asp:imagebutton id="imgPost" title="Post &amp; Issue GRN" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/issuegrn.gif" alt="Issue Goods Receive Note" border="0" width="16" height="16" OnClick="imgPost_Click"></asp:imagebutton></td>
						        <td noWrap><asp:linkbutton id="cmdPost" title="Issue GRN" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdPost_Click">Issue GRN</asp:linkbutton></td>
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
