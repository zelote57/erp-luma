<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._Stock.__Additem" Codebehind="_AddItem.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgPrint" title="Print this Stock Transaction" accessKey="G" tabIndex="5" height="16" width="16" border="0" alt="Print this Stock Transaction" CausesValidation="false" ImageUrl="/RetailPlus/_layouts/images/print.gif" runat="server" CssClass="ms-toolbar" OnClick="imgPrint_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdPrint" title="Print this Stock Transaction" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdPrint_Click" CausesValidation="false">Print</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="Label12" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Stock Type" accessKey="C" tabIndex="3" CausesValidation="False" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Adding New Stock Type" border="0" width="16" height="16" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Stock Type" accessKey="C" tabIndex="4" CausesValidation="False" CssClass="ms-toolbar" runat="server" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblStockID" runat="server" Visible="False"></asp:label></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="3" style="padding-bottom: 2px">
                                    <label>Branch To Process&nbsp;<b>:</b>&nbsp;</label><asp:dropdownlist id="cboBranch" runat="server" CssClass="ms-short-disabled" Enabled="false"></asp:dropdownlist></td>
                                <td class="ms-authoringcontrols" colspan="3" style="padding-bottom: 2px">
                                    <label>Transaction Number&nbsp;<b>:</b>&nbsp;</label><asp:Label id="lblTransactionNo" CssClass="ms-error" runat="server"></asp:Label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Stock Date&nbsp;<b>:</b></label>&nbsp;<asp:Label id="lblStockDate" CssClass="ms-error" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Supplier<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Stock Type<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Description<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Direction<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtSupplier" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                    <asp:Label id="lblSupplierID" CssClass="ms-error" runat="server" Visible=false></asp:Label>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtStockTypeCode" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                     <asp:TextBox id="txtStockDescription" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                     <asp:TextBox id="txtStockDirection" accessKey="C" CssClass="ms-short-disabled" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="8" style="padding-bottom: 2px">
                                    <label>Stock Transaction Remarks<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=7>
                                    <asp:TextBox id="txtStockRemarks" runat="server" accesskey="R" CssClass="ms-long-disabled" MaxLength="150" BorderStyle="Groove" Width="100%" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 10px;" valign="top" colspan="3">
						<div class="ms-sectionheader">Enter item Information to add.</div>
						<div class="ms-descriptiontext"><font color="red">*</font>Indicates a required field</div>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%" valign="top" >
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Product Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Select Variation<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Select Unit<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Enter Quantity<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Purchase Price<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Purchase Amount<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
				                            <asp:dropdownlist id="cboProductCode" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboProductCode_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator>
				                        </ContentTemplate>
		                                <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:dropdownlist id="cboVariation" CssClass="ms-short" runat="server" CausesValidation="false" AutoPostBack="True" onselectedindexchanged="cboVariation_SelectedIndexChanged"></asp:dropdownlist>    
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator5" CssClass="ms-error" runat="server" ControlToValidate="cboVariation" Display="Dynamic" ErrorMessage="'Variation' must not be left blank."></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
				                            <asp:dropdownlist id="cboProductUnit" runat="server" CssClass="ms-short" AutoPostBack="True" CausesValidation=false OnSelectedIndexChanged="cboProductUnit_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Unit' must not be left blank." Display="Dynamic" ControlToValidate="cboProductUnit"></asp:requiredfieldvalidator>
				                        </ContentTemplate>
		                                <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdVariationSearch" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" valign="top">
                                    <asp:textbox id="txtQuantity" accessKey="B" CssClass="ms-short-numeric" runat="server" BorderStyle="Groove" onKeyPress="AllNum()" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged">1</asp:textbox>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator4" CssClass="ms-error" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="'Quantity' must not be left blank."></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" valign="top">
                                    <asp:textbox id="txtPurchasePrice" accessKey="P" CssClass="ms-short-numeric" 
                                        runat="server" BorderStyle="Groove" onKeyPress="AllNum()"  AutoPostBack="true" OnTextChanged="txtPurchasePrice_TextChanged">1</asp:textbox>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator3" CssClass="ms-error" runat="server" ControlToValidate="txtPurchasePrice" Display="Dynamic" ErrorMessage="'Purchase Price' must not be left blank."></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" valign="top">
                                    <asp:textbox id="txtPurchaseAmount" accessKey="P" CssClass="ms-short-numeric-disabled" 
                                        runat="server" BorderStyle="Groove" enabled="false">1</asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" valign="top">
                                    <asp:textbox id="txtProductCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove"></asp:textbox><asp:imagebutton id="cmdProductCode" title="Execute search" style="CURSOR: hand" accessKey="P" border="0" alt="Execute search" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False" OnClick="cmdProductCode_Click"></asp:imagebutton>
						            <asp:imagebutton id="imgAddProduct" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/add.gif" ToolTip="Add New Product" border="0" width="16" height="16" CausesValidation=false OnClick="imgAddProduct_Click"></asp:imagebutton>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" valign="top">
                                    <asp:textbox id="txtVariation" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" Width="112px"></asp:textbox><asp:imagebutton id="cmdVariationSearch" title="Execute search" style="CURSOR: hand" accessKey="V" border="0" alt="Execute search" ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" CausesValidation="False" OnClick="cmdVariationSearch_Click"></asp:imagebutton>
						            <asp:imagebutton id="imgVariationAdd" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/add.gif" ToolTip="Add New Product Variation (Make sure you selected the product to add this.)" border="0" width="16" height="16" CausesValidation=false OnClick="imgVariationAdd_Click"></asp:imagebutton>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Remarks<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=11>
                                    <asp:textbox id="txtRemarks" accessKey="S" runat="server" CssClass="ms-long" MaxLength="50" BorderStyle="Groove" TextMode="MultiLine" Width="100%" Rows="2"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><A name="InputFormSection1"></A><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1">
						<table class="ms-toolbar" id="twotidGrpsTB" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
								</td>
								<td class="ms-toolbar">
									<table cellspacing="0" cellpadding="1" border="0">
										<tr>
											<td class="ms-toolbar" nowrap="nowrap">
											    <asp:imagebutton id="imgSave" title="Add Item" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Add Item" border="0" width="16" height="16" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
											</td>
											<td nowrap="nowrap">
											    <asp:linkbutton id="cmdSave" title="Add Item" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSave_Click">AddItem</asp:linkbutton>
											</td>
											<td class="ms-toolbar" nowrap="nowrap">
											    <asp:imagebutton id="cmdCloseTransaction" title="Close this transaction and back to Stock List" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Close Transaction and Back to Stock List" border="0" width="16" height="16" OnClick="cmdCloseTransaction_Click"></asp:imagebutton>&nbsp;
											</td>
											<td nowrap="nowrap">
											    <asp:linkbutton id="lnkCloseTransaction" title="Close this transaction and back to Stock List" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" OnClick="lnkCloseTransaction_Click">Close Transaction and Back to Stock List</asp:linkbutton>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px">&nbsp;
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B> </B></label>
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
			        <asp:datalist id="lstItem" runat="server" CellPadding="0" Width="100%" OnItemDataBound="lstItem_ItemDataBound">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
							        <col width="10">
							        <col width="30%">
							        <col width="15%">
							        <col width="10%">
                                    <col width="10%">
                                    <col width="10%">
							        <col width="24%">
							        <col width="1%">
						        </colgroup>
						        <tr>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByProduct" runat="server">Product</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByUnit" runat="server">Unit</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px; text-align:right" ><asp:hyperlink id="SortByQty" runat="server">Quantity</asp:hyperlink></TH>
                                    <TH class="ms-vh2" style="padding-bottom: 4px; text-align:right"><asp:hyperlink id="SortByPurchasePrice" runat="server">Purchase Price</asp:hyperlink></TH>
                                    <TH class="ms-vh2" style="padding-bottom: 4px; text-align:right"><asp:hyperlink id="SortByAmount" runat="server">Amount</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"><asp:hyperlink id="SortByRemarks" runat="server">Remarks</asp:hyperlink></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						        </tr>
				        </HeaderTemplate>
				        <ItemTemplate>
						        <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
							        <td class="ms-vb-user">
								        <input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
							        </td>
							        <td class="ms-vb-user">
								        <asp:HyperLink ID="lnkProduct" Runat="server" Target="_blank"></asp:HyperLink>
                                        <asp:HyperLink ID="lnkVariation" Runat="server" Target="_blank"></asp:HyperLink>
							        </td>
							        <td class="ms-vb-user">
								        <asp:Label ID="lblProductUnit" Runat="server"></asp:Label>
								        <asp:Label ID="lblStockType" Runat="server" Visible="False"></asp:Label>
							        </td>
							        <td class="ms-vb-user" style="text-align:right;">
								        <asp:Label ID="lblQuantity" Runat="server"></asp:Label>
							        </td>
                                    <td class="ms-vb-user" style="text-align:right;">
								        <asp:Label ID="lblPurchasePrice" Runat="server"></asp:Label>
							        </td>
                                    <td class="ms-vb-user" style="text-align:right;">
								        <asp:Label ID="lblAmount" Runat="server"></asp:Label>
							        </td>
							        <td class="ms-vb-user">
								        <asp:Label ID="lblRemarks" Runat="server"></asp:Label>
							        </td>
							        <td class="ms-vb2">
								        <A class="DropDown" id="anchorDown" href="" runat="server">
									        <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							        </td>
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
		                <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
				                <td class="ms-formspacer"></td>
				                <td></td>
				                <td class="ms-sectionline" colspan="2" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
			                </tr>
			                <tr style="padding-bottom: 5px" height="30">
				                <td class="ms-formspacer"></td>
				                <td width="50%"></td>
				                <td align="left"><label><b>Total:</b></label></td>
				                <td align="right"><asp:label id="lblStockTotal" runat="server" CssClass="ms-error">0.00</asp:label></td>
			                </tr>
		                </table>
	                </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>
