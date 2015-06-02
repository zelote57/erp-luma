<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._WBranchTransfer.__Post" Codebehind="_Post.ascx.cs" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlInsert" Src="~/MasterFiles/_Product/_insert.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<script language="JavaScript" src="../../_Scripts/PurchasesAndPayables.js"></script>
<script language="JavaScript" src="../../_Scripts/ComputeMarginWBranchTransfer.js"></script>
<script language="JavaScript" src="../../_Scripts/dd_tooltip.js"></script>
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
				        Indicates a required field<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblWBranchTransferID" runat="server" Visible="False"></asp:label>
			        </td>
		        </tr>
		        <tr>
			        <td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
		        </tr>
		        <tr>
			        <td class="ms-authoringcontrols" style="padding-bottom: 5px" valign="top" colspan="3">
				        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr style="padding-bottom: 10px">
								<td class="ms-formspacer"></td>
								<td width="30%" rowspan="4"><img alt="" src="../../_layouts/images/company_logo.gif" /></td>
								<td class="ms-formspacer"></td>
								<td style="HEIGHT: 70px; border-color:White" align="center" width="40%" rowspan="3"><label class="ms-sectionheader" style="FONT-WEIGHT: bold; FONT-SIZE: 40px">Branch Transfer</label></td>
								<td style="padding-bottom: 2px" width="30%" colspan="2"><label>Branch Transfer no:</label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
								<td style="padding-bottom: 2px" width="30%"><asp:HyperLink id="lnkWBranchTransferNo" runat="server" CssClass="ms-error"></asp:HyperLink></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer" style="HEIGHT: 52px"></td>
								<td class="ms-formspacer" style="HEIGHT: 52px"></td>
								<td style="padding-bottom: 2px; HEIGHT: 52px" valign="top" width="30%" colspan="2"><label>Date Prepared: </label>
									<asp:label id="lblWBranchTransferDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Deliver From Branch:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%" colspan="2"><label>To Branch:</label></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="2"><label>Required Delivery Date:</label></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblBranchCodeFrom" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="40%"><asp:label id="lblBranchCodeTo" runat="server" CssClass="ms-error"></asp:label></td>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%"><asp:label id="lblRequiredDeliveryDate" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
							<tr>
								<td class="ms-formspacer" colspan="6"></td>
							</tr>
							<tr style="padding-bottom: 5px">
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" width="30%" colspan="6"><label>WBranchTransfer 
										Remarks:</label><asp:label id="lblWBranchTransferRemarks" runat="server" CssClass="ms-error"></asp:label></td>
							</tr>
						</table>
			        </td>
		        </tr>
		        <tr>
			        <td class="ms-sectionline" colspan="3" height="1">
				        <table class="ms-toolbar" id="threetidGrpsTBC" cellspacing="0" cellpadding="2" border="0" width="100%">
					        <tr>
						        <td class="ms-separator" width="99%"></td>
						        <td class="ms-toolbar">
							        <table cellspacing="0" cellpadding="1" border="0">
								        <tr>
									        <td class="ms-toolbar">
										        <table cellspacing="0" cellpadding="1" border="0">
											        <tr>
												        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="cmdUpdateHeader" title="Update Branch Transfer Header" accessKey="E" tabIndex="5" runat="server" CssClass="ms-toolbar" ImageUrl="../../_layouts/images/edit.gif" alt="Update Branch Transfer Header" border="0" width="16" height="16" OnClick="cmdUpdateHeader_Click"></asp:imagebutton></td>
												        <td nowrap="nowrap"><asp:linkbutton id="imgUpdateHeader" title="Update Branch Transfer Header" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="imgUpdateHeader_Click">Update Branch Transfer Header</asp:linkbutton></td>
											        </tr>
										        </table>
									        </td>
									        <td class="ms-separator"><asp:label id="Label18" runat="server">|</asp:label></td>
									        <td class="ms-toolbar">
                                                <asp:UpdatePanel ID="updPrint" runat="server">
                                                    <ContentTemplate>
										                <table cellspacing="0" cellpadding="1" border="0">
											                <tr>
												                <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrint" title="Print this Branch Transfer" accessKey="G" tabIndex="5" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../../_layouts/images/print.gif" alt="Print this Branch Transfer" border="0" width="16" height="16" OnClick="imgPrint_Click"></asp:imagebutton></td>
												                <td nowrap="nowrap"><asp:linkbutton id="cmdPrint" title="Print this Branch Transfer" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdPrint_Click">Print</asp:linkbutton></td>
											                </tr>
										                </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
									        </td>
									        <td class="ms-separator"><asp:label id="Label21" runat="server">|</asp:label></td>
									        <td class="ms-toolbar">
										        <table cellspacing="0" cellpadding="1" border="0">
											        <tr>
												        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Item And Back To WBranchTransfer List" accessKey="C" tabIndex="3" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Adding New Item And Back To WBranchTransfer List" border="0" width="16" height="16" OnClick="imgCancel_Click"></asp:imagebutton></td>
												        <td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Item And Back To WBranchTransfer List" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Back To WBranchTransfer List</asp:linkbutton></td>
											        </tr>
										        </table>
									        </td>
									        <td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
									        </td>
								        </tr>
							        </table>
						        </td>
						        <td class="ms-toolbar" nowrap="nowrap" align="right" width="1%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
						        </td>
					        </tr>
				        </table>
			        </td>
		        </tr>
		        <tr>
			        <td style="padding-bottom: 4px; PADDING-TOP: 4px" valign="top" colspan="3">
				        <div class="ms-sectionheader" style="padding-bottom: 8px; PADDING-TOP: 8px">Step 1: 
					        Add Item Information</div>
			        </td>
		        </tr>
		        <tr>
			        <td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: 2px solid; PADDING-LEFT: 8px; padding-bottom: 10px" valign="top" colspan="3">
				        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
					                <tr>
						                <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label>Select 
								                Product Code<font color="red">*
									                <asp:label id="lblWBranchTransferItemID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></font></label></td>
						                <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label>Select 
								                Variation<font color="red">*</font></label></td>
						                <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label>Select 
								                unit<font color="red">*</font></label></td>
					                </tr>
					                <tr>
						                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
						                <td class="ms-authoringcontrols">
						                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
						                            <asp:dropdownlist id="cboProductCode" runat="server" CssClass="ms-long" width="70%" AutoPostBack="True" onselectedindexchanged="cboProductCode_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator>
						                            <asp:ImageButton id="imgProductHistory" runat="server" Visible="false" ImageUrl="../../_layouts/images/prodhist.gif" ToolTip="Show product inventory history report" CausesValidation=false Style="cursor: hand" OnClick="imgProductHistory_Click" ></asp:ImageButton>
                                                    <asp:ImageButton id="imgProductPriceHistory" runat="server" Visible="false" ImageUrl="../../_layouts/images/pricehist.gif" ToolTip="Show product price history report" CausesValidation=false Style="cursor: hand" OnClick="imgProductPriceHistory_Click" ></asp:ImageButton>
                                                    <asp:ImageButton id="imgChangePrice" runat="server" Visible="false" ImageUrl="../../_layouts/images/chprice.gif" ToolTip="Change price" CausesValidation=false Style="cursor: hand" OnClick="imgChangePrice_Click" ></asp:ImageButton>
                                                    <asp:ImageButton id="imgEditNow" runat="server" Visible="false" ImageUrl="../../_layouts/images/edit.gif" ToolTip="Edit this product" CausesValidation=false Style="cursor: hand" OnClick="imgEditNow_Click" ></asp:ImageButton>
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
						                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
						                <td class="ms-authoringcontrols">
						                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
						                            <asp:dropdownlist id="cboVariation" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboVariation_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Variation' must not be left blank." Display="Dynamic" ControlToValidate="cboVariation"></asp:requiredfieldvalidator>
						                        </ContentTemplate>
				                                <Triggers> 
				                                    <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="cmdEdit" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="imgEdit" EventName="Click" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                                </Triggers> 
                                            </asp:UpdatePanel>
						                </td>
						                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
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
                                            </Triggers>
                                            </asp:UpdatePanel>
						                </td>
					                </tr>
					                <tr>
						                <td class="ms-formspacer"></td>
						                <td class="ms-authoringcontrols" valign="top">
						                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
						                            <asp:textbox id="txtProductCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>						                            
						                            <asp:imagebutton id="cmdProductCode" ToolTip="Execute search" style="CURSOR: hand" accessKey="P" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False" OnClick="cmdProductCode_Click"></asp:imagebutton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:HyperLink id="lnkProductDetails" runat="server" Visible="false" ToolTip="View product details" Target="_blank">[view details]</asp:HyperLink>
                                                    <asp:Label id="lblPurchasePriceHistory" runat="server" Visible="false" CssClass="ms-error"></asp:Label>
                                                </ContentTemplate>
				                                <Triggers> 
                                                    <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                                </Triggers> 
                                            </asp:UpdatePanel>
						                </td>
						                <td class="ms-formspacer"></td>
						                <td class="ms-authoringcontrols" valign="top">
						                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                    <asp:textbox id="txtVariation" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" ondblclick="ontime(this)" ToolTip="Double Click to Select Date From Calendar"></asp:textbox>
						                            <asp:imagebutton id="cmdVariationSearch" runat="server" ImageUrl="../../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" style="CURSOR: hand" CssClass="ms-toolbar" BorderStyle="Groove" CausesValidation="False" OnClick="cmdVariationSearch_Click" Visible="false"></asp:imagebutton>
						                        </ContentTemplate>
				                                <Triggers> 
                                                    <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                                </Triggers> 
                                            </asp:UpdatePanel>
						                </td>
						                <td class="ms-formspacer" colspan="2"></td>
					                </tr>
                                    <tr>
				                        <td class="ms-formspacer" colspan="6"></td>
			                        </tr>
                                    <tr>
				                        <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="6"><label>Quantity</label></td>
			                        </tr>
                                    <tr>
				                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
				                        <td class="ms-authoringcontrols" colspan="5">
				                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <asp:textbox onkeypress="AllNum()" id="txtQuantity" onkeyup="ComputeAmountPost()" accessKey="Q" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Width="120px">1</asp:textbox>
                                                <asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Quantity' must not be left blank." Display="Dynamic" ControlToValidate="txtQuantity"></asp:requiredfieldvalidator>
				                            </ContentTemplate>
				                                <Triggers> 
				                                    <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
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
				                        <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="6"><label>Remarks</label></td>
			                        </tr>
			                        <tr>
				                        <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
				                        <td class="ms-authoringcontrols" colspan="5">
				                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
				                                <asp:textbox id="txtRemarks" accessKey="R" runat="server" CssClass="ms-long" BorderStyle="Groove" Rows="2" MaxLength="150" Width="100%" TextMode="MultiLine"></asp:textbox>
                                                <asp:textbox onkeypress="AllNum()" id="txtPrice" onkeyup="ChangePriceComputeMarginMPWBranchTransfer(this)" accessKey="P" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Width="80%" Visible="false">0</asp:textbox>
                                                <asp:textbox onkeypress="AllNum()" id="txtAmount" onkeyup="ComputeAmountPost()" accessKey="A" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" ReadOnly="True" Width="80%" Visible="false">0</asp:textbox>
                                                <asp:textbox onkeypress="AllNum()" id="txtDiscount" onkeyup="ComputeAmountPost()" accessKey="D" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Width="50%" Visible="false">0</asp:textbox>
							                    <INPUT id="chkInPercent" type="checkbox" onchange="ComputeAmountPost()" CHECKED name="chkInPercent" runat="server" Visible="false" />
							                    <INPUT id="chkIsTaxable" type="checkbox" CHECKED name="chkIsTaxable" runat="server" onchange="ComputeAmountPost()" Visible="false" />
							                    <asp:textbox onkeypress="AllNum()" id="txtSellingQuantity" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Enabled=false Visible="false">1</asp:textbox>
				                                <asp:textbox onkeypress="AllNum()" id="txtMargin" onkeyup="ChangePriceComputeMarginPPWBranchTransfer(this)" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" BackColor="YellowGreen" Width="80%" Visible="false">0</asp:textbox>
				                                <asp:textbox onkeypress="AllNum()" id="txtSellingPrice" onkeyup="ChangePriceComputeMarginByPriceMPWBranchTransfer(this)" runat="server" width="31%" CssClass="ms-short-numeric" BorderStyle="Groove" BackColor="YellowGreen" Visible="false">0</asp:textbox>
				                                <button name="cmdRevertSellingPrice" id="cmdRevertSellingPrice" causesvalidation="false" runat="server" onclick="RevertSellingPrice()" Visible="false"> <- </button>
				                                <asp:textbox onkeypress="AllNum()" id="txtOldSellingPrice" runat="server" width="31%" CssClass="ms-short-numeric" BorderStyle="Groove" BackColor="YellowGreen" ReadOnly="true" Text="0.00" Visible="false"></asp:textbox>
				                                <asp:TextBox accessKey="C" id="txtVAT" runat="server" onkeypress="AllNum()" CssClass="ms-short-numeric" BorderStyle="Groove" MaxLength="6" Width="80%" BackColor="YellowGreen" Visible="false"></asp:TextBox>
				                                <asp:TextBox accessKey="C" id="txtEVAT" runat="server" onkeypress="AllNum()" CssClass="ms-short-numeric" BorderStyle="Groove" MaxLength="6" Width="80%" BackColor="YellowGreen" Visible="false"></asp:TextBox>
                                                <asp:TextBox accessKey="C" id="txtLocalTax" runat="server" onkeypress="AllNum()" CssClass="ms-short-numeric" BorderStyle="Groove" MaxLength="6" Width="80%" BackColor="YellowGreen" Visible="false"></asp:TextBox>
				                            </ContentTemplate>
				                                <Triggers> 
				                                    <asp:AsyncPostBackTrigger ControlID="cmdClear" EventName="Click" />
				                                    <asp:AsyncPostBackTrigger ControlID="imgClear" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="lstItem" EventName="ItemCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                                </Triggers> 
                                            </asp:UpdatePanel>  
				                        </td>
			                        </tr>
			                            
					                <tr>
						                <td class="ms-formspacer" colspan="6"></td>
					                </tr>
				                </table>
			        </td>
		        </tr>
		        <tr>
			        <td class="ms-sectionline" colspan="3" height="1">
				        <table class="ms-toolbar" id="twotidGrpsTB" cellspacing="0" cellpadding="2" border="0" width="100%">
					        <tr>
						        <td class="ms-toolbar" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
						        </td>
						        <td class="ms-toolbar">
							        <table cellspacing="0" cellpadding="1" border="0">
								        <tr>
									        <td class="ms-toolbar">
										        <table cellspacing="0" cellpadding="1" border="0">
											        <tr>
												        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgClear" title="Clear and Load Defaults" accessKey="C" tabIndex="3" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../../_layouts/images/delitem.gif" alt="Clear and Load Defaults" border="0" width="16" height="16" OnClick="imgClear_Click"></asp:imagebutton></td>
												        <td nowrap="nowrap"><asp:linkbutton id="cmdClear" title="Clear and Load Defaults" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdClear_Click">Clear & Load Defaults</asp:linkbutton></td>
											        </tr>
										        </table>
									        </td>
									        <td class="ms-separator"><asp:label id="Label2" runat="server">|</asp:label></td>
									        <td class="ms-toolbar">
										        <table cellspacing="0" cellpadding="1" border="0">
											        <tr>
												        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" title="Save Item" accessKey="A" tabIndex="1" runat="server" CssClass="ms-toolbar" ImageUrl="../../_layouts/images/newuser.gif" alt="Save Item" border="0" width="16" height="16" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
												        </td>
												        <td nowrap="nowrap"><asp:linkbutton id="cmdSave" title="Save Item" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save Item</asp:linkbutton></td>
											        </tr>
										        </table>
									        </td>
									        <td class="ms-separator"><asp:label id="lblSeparator01" runat="server">|</asp:label></td>
									        <td class="ms-toolbar">
										        <table cellspacing="0" cellpadding="1" border="0">
											        <tr>
												        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Item" accessKey="X" tabIndex="3" runat="server" CssClass="ms-toolbar" CausesValidation="False" ImageUrl="../../_layouts/images/delitem.gif" alt="Remove Selected Item" border="0" width="16" height="16" OnClick="imgDelete_Click"></asp:imagebutton></td>
												        <td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Item" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdDelete_Click">Remove Selected Item</asp:linkbutton></td>
											        </tr>
										        </table>
									        </td>
									        <td class="ms-toolbar" id="align03" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:datalist id="lstItem" runat="server" Width="100%" cellpadding="0" OnItemCommand="lstItem_ItemCommand" OnItemDataBound="lstItem_ItemDataBound" AlternatingItemStyle-CssClass="ms-alternating">
		                <HeaderTemplate>
			                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
				                <colgroup>
					                <col width="10">
					                <col width="10">
					                <col width="10">
					                <col width="40%">
					                <col width="12%">
					                <col width="12%">
					                <col width="36%">
					                <col width="10">
				                </colgroup>
				                <tr>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" /></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"><asp:hyperlink id="SortByDescription" runat="server">Description</asp:hyperlink></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px; text-align:right"><asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"><asp:hyperlink id="SortByProductUnitCode" runat="server">Unit of Measure</asp:hyperlink></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px; text-align:left"><asp:hyperlink id="SortByRemarks" runat="server">Remarks</asp:hyperlink></th>
					                <th class="ms-vh2" style="padding-bottom: 4px; padding-top: 4px"></th>
				                </tr>
		                </HeaderTemplate>
		                <ItemTemplate>
				                <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
					                <td class="ms-vb-user">
						                <input type="checkbox" id="chkList" runat="server" name="chkList" />
					                </td>
					                <td class="ms-vb-user">
						                <asp:ImageButton id="imgItemUpdate" runat="server" ImageUrl="../../_layouts/images/edit.gif" ToolTip="Update Item" CommandName="imgItemUpdateClick" CausesValidation=false></asp:ImageButton>
					                </td>
					                <td class="ms-vb-user">
					                </td>
					                <td class="ms-vb-user">
					                    <asp:HyperLink ID="lnkBarcode" Runat="server" Target="_blank"></asp:HyperLink>
						                <asp:HyperLink ID="lnkDescription" Runat="server" Target="_blank"></asp:HyperLink>
						                <asp:HyperLink ID="lnkMatrixDescription" Runat="server" Target="_blank"></asp:HyperLink>
					                </td>
					                <td class="ms-vb-user" style="text-align:right">
						                <asp:Label ID="lblQuantity" Runat="server"></asp:Label>&nbsp;&nbsp;
					                </td>
					                <td class="ms-vb-user">
						                <asp:Label ID="lblProductUnitID" Runat="server" Visible="False"></asp:Label>
						                <asp:Label ID="lblProductUnitCode" Runat="server"></asp:Label>
					                </td>
					                <td class="ms-vb-user" style="text-align:left">
                                        <asp:Label ID="lblRemarks" Runat="server"></asp:Label>
						                <%--<asp:Label ID="lblUnitCost" Runat="server" Visible="false"></asp:Label>
						                <asp:Label ID="lblDiscountApplied" Runat="server" Visible="False"></asp:Label>
						                <asp:Label ID="lblPercent" Runat="server" Visible="False">%</asp:Label>
                                        <asp:Label ID="lblAmount" Runat="server" Visible="False"></asp:Label>--%>
					                </td>
					                <td class="ms-vb2">
						                <a class="DropDown" id="anchorDown" href="" runat="server">
							                <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></a>
					                </td>
				                </tr>
				                <tr>
					                <td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
					                <td colspan="6" height="1">
						                <div class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
							                <asp:panel id="panItem" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
								                <%--<table id="tblpanItem" cellspacing="0" cellpadding="0" width="100%" border="0">
									                <tr>
										                <td class="ms-formspacer" colspan="1"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
									                </tr>
									                <tr>
										                <td width="19%">
											                <asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>VAT</b>"></asp:Label>
										                </td>
										                <td width="1%">
											                <asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
										                </td>
										                <td width="20%">
											                <asp:Label id="lblVAT" CssClass="ms-vb2" runat="server"></asp:Label>
										                </td>
										                <td width="19%">
											                <asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>eVAT</b>"></asp:Label>
										                </td>
										                <td width="1%">
											                <asp:Label id="Label9" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
										                </td>
										                <td width="40%">
											                <asp:Label id="lblEVAT" CssClass="ms-vb2" runat="server"></asp:Label>
										                </td>
									                </tr>
									                <tr>
										                <td width="19%">
											                <asp:Label id="Label6" CssClass="ms-vh2" runat="server" text="<b>Local Tax</b>"></asp:Label>
										                </td>
										                <td width="1%">
											                <asp:Label id="Label11" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
										                </td>
										                <td width="20%">
											                <asp:Label id="lblLocalTax" CssClass="ms-vb2" runat="server"></asp:Label>
										                </td>
										                <td width="19%">
											                <asp:Label id="Label13" CssClass="ms-vh2" runat="server" text="<b>VAT Inclusive</b>"></asp:Label>
										                </td>
										                <td width="1%">
											                <asp:Label id="Label14" CssClass="ms-vh2" runat="server" text="<b></b>"></asp:Label>
										                </td>
										                <td width="40%">
											                <asp:Label id="lblisVATInclusive" CssClass="ms-vb2" runat="server"></asp:Label>
										                </td>
									                </tr>
									                <tr>
										                <td width="19%">
											                <asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b>Remarks</b>"></asp:Label>
										                </td>
										                <td width="1%">
											                <asp:Label id="Label10" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
										                </td>
										                <td colspan="4">
											                
										                </td>
									                </tr>
								                </table>--%>
							                </asp:panel></div>
					                </td>
					                <td class="ms-vh2" height="1"><img height="5" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				                </tr>
		                </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
	                </asp:datalist>
	             </ContentTemplate>
	             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdGRN" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="imgGRN" EventName="Click" />
                </Triggers> 
            </asp:UpdatePanel>
	    </td>
        <td><a name="itemsection"></a><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
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
					    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
		                        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
			                        <tr style="padding-bottom: auto">
				                        <td class="ms-formspacer"></td>
				                        <td></td>
				                        <td align="left"><label>   &nbsp; &nbsp; Applicable Discount:</label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtWBranchTransferDiscountApplied" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text=0 Width="82px" AutoPostBack="True" OnTextChanged="txtWBranchTransferDiscountApplied_TextChanged"></asp:textbox><asp:dropdownlist id="cboWBranchTransferDiscountType" runat="server" CssClass="ms-short" AutoPostBack="True" OnSelectedIndexChanged="cboWBranchTransferDiscountType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">NA</asp:ListItem>
                                            <asp:ListItem Value="1">amt</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">%</asp:ListItem>
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ControlToValidate="txtWBranchTransferDiscountApplied" Display="Dynamic" ErrorMessage="'Discount' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td width="50%"></td>
				                        <td align="left"><label><b>Subtotal Discount:</b></label></td>
				                        <td align="right"><asp:label id="lblWBranchTransferDiscount" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td></td>
				                        <td align="left"><label><b>VATable Amount:</b></label></td>
				                        <td align="right"><asp:label id="lblWBranchTransferVatableAmount" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td width="50%"></td>
				                        <td align="left"><label><b>Subtotal:</b></label></td>
				                        <td align="right"><asp:label id="lblWBranchTransferSubTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td></td>
				                        <td align="left"><label><b>VAT:</b></label></td>
				                        <td align="right"><asp:label id="lblWBranchTransferVAT" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td width="50%"></td>
				                        <td align="left"><label><b>Freight:</b></label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtWBranchTransferFreight" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text="0.00" AutoPostBack="True" OnTextChanged="txtWBranchTransferFreight_TextChanged"></asp:textbox>
				                                                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" CssClass="ms-error" ControlToValidate="txtWBranchTransferFreight" Display="Dynamic" ErrorMessage="'Freight' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td></td>
				                        <td align="left"><label><b>WBranchTransfer Deposit:</b></label></td>
				                        <td align="right"><asp:textbox onkeypress="AllNum()" id="txtWBranchTransferDeposit" accessKey="" runat="server" CssClass="ms-short-numeric" BorderStyle="Groove" Text="0.00" AutoPostBack="True" OnTextChanged="txtWBranchTransferDeposit_TextChanged"></asp:textbox>
				                                                                       <asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" CssClass="ms-error" ControlToValidate="txtWBranchTransferDeposit" Display="Dynamic" ErrorMessage="'WBranchTransfer Deposit' must not be left blank."></asp:requiredfieldvalidator></td>
			                        </tr>
			                        <tr>
				                        <td class="ms-formspacer"></td>
				                        <td></td>
				                        <td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
			                        </tr>
			                        <tr style="padding-bottom: 5px">
				                        <td class="ms-formspacer"></td>
				                        <td width="50%"></td>
				                        <td align="left"><label><b>Total:</b></label></td>
				                        <td align="right"><asp:label id="lblWBranchTransferTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                        </tr>
		                        </table>
		                    </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSave" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdDelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgDelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdGRN" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgGRN" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
	                </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
        <td>
	        <table class="ms-toolbar" id="onetidGrpsTC" cellspacing="0" cellpadding="2" border="0" width="100%">
		        <tr>
			        <td class="ms-toolbar" id="align01" nowrap="nowrap" align="right" width="99%">
			        </td>
			        <td class="ms-toolbar" align="center">
                        <asp:UpdatePanel ID="updIssueGRN1" runat="server">
                            <ContentTemplate>
				                <table cellspacing="0" cellpadding="1" border="0">
					                <tr>
						                <td class="ms-toolbar" nowrap="nowrap">Delivery Date :</td>
						                <td nowrap="nowrap"><asp:textbox id="txtDeliveryDate" accessKey="Q" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox>
							                <asp:label id="Label17" runat="server" CssClass="ms-error"> 'yyyy-mm-dd' format</asp:label></td>
					                </tr>
				                </table>
				                <asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ControlToValidate="txtDeliveryDate" Display="Dynamic" ErrorMessage="'Delivery Date' must not be left blank."></asp:requiredfieldvalidator>
				                <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="ms-error" ControlToValidate="txtDeliveryDate" Display="Dynamic" ErrorMessage="'Delivery Date' must be a valid date." Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
			        </td>
			        <td class="ms-separator"><asp:label id="lblSeparator1" runat="server">|</asp:label></td>
			        <td class="ms-toolbar" align="center">
                        <asp:UpdatePanel ID="updIssueGRN2" runat="server">
                            <ContentTemplate>
				                <table cellspacing="0" cellpadding="1" border="0">
					                <tr>
						                <td class="ms-toolbar" nowrap="nowrap">Received by:</td>
						                <td nowrap="nowrap"><asp:textbox id="txtReceivedBy" accessKey="Q" runat="server" CssClass="ms-short" BorderStyle="Groove">NA</asp:textbox></td>
					                </tr>
				                </table>
				                <asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" CssClass="ms-error" ControlToValidate="txtReceivedBy" Display="Dynamic" ErrorMessage="'Delivery No' must not be left blank."></asp:requiredfieldvalidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:label id="lblStatus" runat="server" Visible="false"></asp:label>
			        </td>
			        <td class="ms-separator"><asp:label id="lblSeparator2" runat="server">|</asp:label></td>
			        <td class="ms-toolbar">
                        <asp:UpdatePanel ID="updIssueGRN" runat="server">
                            <ContentTemplate>
				                <table cellspacing="0" cellpadding="1" border="0">
					                <tr>
						                <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgGRN" title="Post &amp; Issue GRN" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/issuegrn.gif" alt="Issue Goods Receive Note" border="0" width="16" height="16" OnClick="imgGRN_Click"></asp:imagebutton></td>
						                <td nowrap="nowrap"><asp:linkbutton id="cmdGRN" title="Issue GRN" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdGRN_Click">Issue GRN</asp:linkbutton></td>
					                </tr>
				                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
			        </td>
                    <td class="ms-separator"><asp:label id="lblSeparator3" runat="server" Visible="false">|</asp:label></td>
			        <td class="ms-toolbar">
                        <asp:UpdatePanel ID="updSubmitForApproval" runat="server">
                            <ContentTemplate>
                                <table cellspacing="0" cellpadding="1" border="0">
					                <tr>
						                <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSubmitForApproval" title="Submit For Approval" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/issuegrn.gif" alt="Submit For Approval" border="0" width="16" height="16" OnClick="imgSubmitForApproval_Click"></asp:imagebutton></td>
						                <td nowrap="nowrap"><asp:linkbutton id="cmdSubmitForApproval" title="Submit For Approval" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdSubmitForApproval_Click">Submit For Approval</asp:linkbutton></td>
					                </tr>
				                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
			        </td>
			        <td class="ms-toolbar" id="align052" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
			        </td>
		        </tr>
	        </table>
        </td>
        <td><a name="postsection"></a><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
    </tr>
</table>
   