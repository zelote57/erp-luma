<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__ChangePrice" Codebehind="_changeprice.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../_Scripts/ComputeMargin.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/blank.gif" /></td>
	</tr>
	<tr>
        <td class="ms-sectionline" colspan="3" height="1"><asp:Label ID="lblReferrer" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td class="ms-authoringcontrols" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top" colspan="3">
            <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" /></td>
	                <td class="ms-authoringcontrols" style="padding-bottom: 2px; PADDING-TOP: 10px" colspan="3">
                        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td nowrap="nowrap"><label>Select Product Code<font color="red">*</font></label><asp:label id="lblProductID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></td>
                                <td> : </td>
                                <td nowrap="nowrap">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                                        <ContentTemplate >
                                            <asp:DropDownList ID="cboProductCode" runat="server" AutoPostBack="True" CssClass="ms-long" OnSelectedIndexChanged="cboProductCode_SelectedIndexChanged"> </asp:DropDownList>
                                            <asp:TextBox ID="txtProductCode" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short"></asp:TextBox>
                                            <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator>
                                            <asp:ImageButton ID="cmdProductCode" runat="server" ImageUrl="../../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" CausesValidation="False" Style="cursor: hand" OnClick="cmdProductCode_Click" />
                                            <asp:ImageButton id="imgProductHistory" runat="server" Visible=false ImageUrl="../../_layouts/images/prodhist.gif" ToolTip="Show product inventory history report" CausesValidation="false" Style="cursor: hand" OnClick="imgProductHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgProductPriceHistory" runat="server" Visible=false ImageUrl="../../_layouts/images/pricehist.gif" ToolTip="Show product price history report" CausesValidation="false" Style="cursor: hand" OnClick="imgProductPriceHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgInventoryAdjustment" runat="server" Visible=false ImageUrl="../../_layouts/images/invadj.gif" ToolTip="Adjust inventory count" CausesValidation="false" Style="cursor: hand" OnClick="imgInventoryAdjustment_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgEditNow" runat="server" Visible=false ImageUrl="../../_layouts/images/edit.gif" ToolTip="Edit this product" CausesValidation="false" Style="cursor: hand" OnClick="imgEditNow_Click" ></asp:ImageButton>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgSaveCopyToAllMatrix" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveCopyToAllMatrix" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
	                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer" colspan="5"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" /></td>
	                <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="3">
	                    
	                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                            <ContentTemplate >
                                <asp:Label ID="lblProductPackage" Runat="server" Visible="False">Product Packages </asp:Label>
	                            <asp:hyperlink id="lnkProductPackageAdd" runat="server" ToolTip="Add new Package for this product." Visible="False">+</asp:hyperlink>
	                        </ContentTemplate>
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveCopyToAllMatrix" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdSaveCopyToAllMatrix" EventName="Click" />
                            </Triggers> 
                        </asp:UpdatePanel>
	                </td>
	                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" /></td>
	                <td colspan="3">
	                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                            <ContentTemplate >
	                            <asp:datalist id="lstProductPackages" runat="server" Width="100%" ShowFooter="False" cellpadding="0" OnItemDataBound="lstProductPackages_ItemDataBound" OnItemCommand="lstProductPackages_ItemCommand">
		                            <HeaderTemplate>
			                            <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
				                            <colgroup>
					                            <col width="10">
					                            <col width="10">
					                            <col width="10">
					                            <col width="10">
					                            <col width="12%" align="left">
                                                <col width="9%" align="left">
                                                <col width="8%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="9%" align="left">
                                                <col width="8%" align="left">
                                                <col width="8%" align="left">
                                                <col width="8%" align="left">
					                            <col width="1%">
				                            </colgroup>
				                            <tr>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                        </TH>
						                        <TH class="ms-vh2" style="padding-bottom: 4px">
						                        </TH>
						                        <TH class="ms-vh2" style="padding-bottom: 4px">
						                        </TH>
						                        <TH class="ms-vh2" style="padding-bottom: 4px">
						                        </TH>
                                                <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortByProductDesc" runat="server">Description</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortByUnit" runat="server">Unit</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortByQuantity" runat="server">Quantity</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortByPurchasePrice" runat="server">Purchase Price</asp:hyperlink></TH>
						                        <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="Hyperlink1" runat="server">Retail %Margin</asp:hyperlink> /
						                            <asp:hyperlink id="Hyperlink2" runat="server">Selling Price</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="Hyperlink3" runat="server">Wholesale %Margin</asp:hyperlink> /
						                            <asp:hyperlink id="SortByPrice" runat="server">Selling Price</asp:hyperlink></TH>
						                        <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="Hyperlink5" runat="server">%Comm</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortBarCode1" runat="server">BarCode1</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortBarCode2" runat="server">BarCode2</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
						                            <asp:hyperlink id="SortByBarCode3" runat="server">BarCode3</asp:hyperlink></TH>
					                            <TH class="ms-vh2" style="padding-bottom: 4px">
					                            </TH>
				                            </tr>
			                            </table>
		                            </HeaderTemplate>
		                            <ItemTemplate>
			                            <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
				                            <colgroup>
					                            <col width="10">
					                            <col width="10">
					                            <col width="10">
					                            <col width="10">
					                            <col width="12%" align="left">
                                                <col width="9%" align="left">
                                                <col width="8%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="9%" align="left">
                                                <col width="8%" align="left">
                                                <col width="8%" align="left">
                                                <col width="8%" align="left">
					                            <col width="1%">
				                            </colgroup>
				                            <tr>
					                            <td class="ms-vb-user" align="right">
						                            <input type="checkbox" id="chkProductPackageID" runat="server" NAME="chkProductPackageID" visible=false>
						                            &nbsp;<asp:ImageButton id="cmdDelProductPackage" runat="server" ImageUrl="../../_layouts/images/delitem.gif" height="16" width="16" ToolTip="Delete this Package" CommandName="cmdDelProductPackage" CausesValidation="false"></asp:ImageButton>
					                            </td>
					                            <td class="ms-vb-user" align="right">
					                                <asp:ImageButton id="cmdPrintShelvesBarCode1" runat="server" ImageUrl="../../_layouts/images/print.gif" height="16" width="16" ToolTip="" CommandName="cmdPrintShelvesBarCode1" CausesValidation="false"></asp:ImageButton></td>
					                            <td class="ms-vb-user" align="right">
					                                <asp:ImageButton id="cmdPrintShelvesBarCode2" runat="server" ImageUrl="../../_layouts/images/print.gif" height="16" width="16" ToolTip="" CommandName="cmdPrintShelvesBarCode2" CausesValidation="false"></asp:ImageButton></td>
					                            <td class="ms-vb-user" align="right">
					                                <asp:ImageButton id="cmdPrintShelvesBarCode3" runat="server" ImageUrl="../../_layouts/images/print.gif" height="16" width="16" ToolTip="" CommandName="cmdPrintShelvesBarCode3" CausesValidation="false"></asp:ImageButton></td>
					                            <td class="ms-vb-user" nowrap="nowrap">
						                            <asp:Label ID="lblProductDesc" Runat="server"></asp:Label>
					                            </td>
                                                <td class="ms-vb-user" nowrap="nowrap">
					                                <asp:Label ID="lblProductPackageID" Runat="server" Visible=false></asp:Label>
						                            <asp:Label ID="lblUnitName" Runat="server"></asp:Label>
					                            </td>
                                                <td>
						                            <asp:TextBox accessKey="C" id="txtQuantity" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" Enabled="false" Width="95%"></asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtPurchasePrice" runat="server" onkeypress="AllNum()" onKeyUp="ChangePriceComputeMarginPP(this)" CssClass="ms-short" BorderStyle="Groove" Width="95%">0</asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtMargin" runat="server" onkeypress="AllNum()" onKeyUp="ChangePriceComputeMarginPP(this)" CssClass="ms-short" BorderStyle="Groove" BackColor="YellowGreen" Width="45%">0</asp:TextBox>
					                                <asp:TextBox accessKey="C" id="txtSellingPrice" runat="server" onkeypress="AllNum()" onKeyUp="ChangePriceComputeMarginByPricePP(this)" CssClass="ms-short" BorderStyle="Groove" Width="45%">0</asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtWSPriceMarkUp" runat="server" onkeypress="AllNum()" onKeyUp="ChangePriceComputeMarginPP(this)" CssClass="ms-short" BorderStyle="Groove" BackColor="YellowGreen" Width="45%">0</asp:TextBox>
					                                <asp:TextBox accessKey="C" id="txtWSPrice" runat="server" onkeypress="AllNum()" onKeyUp="ChangePriceComputeMarginByPricePP(this)" CssClass="ms-short" BorderStyle="Groove" Width="45%">0</asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtCommision" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" BackColor="Yellow" Width=95%>0</asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtBarCode1" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="15" Width=95%></asp:TextBox>
						                            <asp:Label id="lblVAT" runat="server" Visible="false"></asp:Label>
					                            </td>
					                            <td class="ms-vb-user">
						                            <asp:TextBox accessKey="C" id="txtBarCode2" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="15" Width=95%></asp:TextBox>
						                            <asp:Label id="lblEVAT" runat="server" Visible="false"></asp:Label>
					                            </td>
					                            <td class="ms-vb-user">
						                            <asp:TextBox accessKey="C" id="txtBarCode3" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="15" Width=95%></asp:TextBox>
						                            <asp:Label id="lblLocalTax" runat="server" Visible="false"></asp:Label>
					                            </td>
					                            <td class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
							                            <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="False"></asp:Image></A>
					                            </td>
				                            </tr>
			                            </table>
		                            </ItemTemplate>
	                            </asp:datalist>
	                            <asp:Label id="lblPurchasePriceHistory" runat="server" Visible=false CssClass="ms-error"></asp:Label>
	                        </ContentTemplate>
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveCopyToAllMatrix" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdSaveCopyToAllMatrix" EventName="Click" />
                            </Triggers> 
                        </asp:UpdatePanel>
	                </td>
	                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer" colspan="5" style="height: 21px"></td>
                </tr>
            </table>
	            
        </td>
    </tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/blank.gif" /></td>
	</tr>
	
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td class="ms-sectionline" colspan="3" height="1">
	        <table class="ms-toolbar" id="TABLE2" cellspacing="0" cellpadding="2" border="0" width="100%">
				<tr>
				    
		            <td class="ms-toolbar">
		                <table cellspacing="0" cellpadding="1" border="0">
			                <tr>
				                <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveCopyToAllMatrix" tooltip="Save product package prices" accessKey="N" tabIndex="1" height="16" width="16" border="0" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveCopyToAllMatrix_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveCopyToAllMatrix" tooltip="Save product package pricess" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" OnClick="cmdSaveCopyToAllMatrix_Click">Save product package prices</asp:linkbutton></td>
			                </tr>
		                </table>
	                </td>
                    <td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Applying Local Tax" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Applying Local Tax" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Applying Local Tax" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="TD3" nowrap="nowrap" align="right" width="99%"></td>
					<td class="ms-toolbar" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
