<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory.__InvAdjusment" Codebehind="_invadjustment.ascx.cs" %>
<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../_Scripts/ComputeMargin.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
        <td class="ms-sectionline" colspan="3" height="1">
	        <table class="ms-toolbar" id="threetidGrpsTBC" cellspacing="0" cellpadding="2" border="0" width="100%">
		        <tr>
			        <td class="ms-toolbar">
				        <table cellspacing="0" cellpadding="1" border="0">
					        <tr>
					            <td class="ms-toolbar">
						            <table cellspacing="0" cellpadding="1" border="0">
							            <tr>
								            <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" title="Apply new quantity" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/saveitem.gif" alt="Apply new quantity" border="0" width="16" height="16" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								            </td>
								            <td nowrap="nowrap"><asp:linkbutton id="cmdSave" title="Apply new quantity" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" OnClick="cmdSave_Click">Save and new</asp:linkbutton></td>
							            </tr>
						            </table>
					            </td>
					            <td class="ms-separator">|</td>
						        <td class="ms-toolbar">
						            <table cellspacing="0" cellpadding="1" border="0">
							            <tr>
								            <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" title="Apply new quantity" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/saveitem.gif" alt="Apply new quantity" border="0" width="16" height="16" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								            </td>
								            <td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" title="Apply new quantity" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							            </tr>
						            </table>
					            </td>
					            <td class="ms-separator">|</td>
					            <td class="ms-toolbar">
						            <table cellspacing="0" cellpadding="1" border="0">
							            <tr>
								            <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Inventory Adjustment" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/impitem.gif" alt="Cancel Inventory Adjustment" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								            <td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Inventory Adjustment" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							            </tr>
						            </table>
					            </td>
					            <td class="ms-toolbar">
						            <table cellspacing="0" cellpadding="1" border="0">
							            <tr>
								            <td class="ms-toolbar" nowrap="nowrap">Select branch to process: </td>
								            <td nowrap="nowrap"><asp:DropDownList ID="cboBranch" runat="server" AutoPostBack="True" CssClass="ms-short" CausesValidation="false" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged"> </asp:DropDownList></td>
							            </tr>
						            </table>
					            </td>
					            
					            <td class="ms-toolbar" id="align02" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					            </td>
					        </tr>
				        </table>
			        </td>
			        <td class="ms-toolbar" id="Td1" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
		        </tr>
	        </table>
            <asp:Label ID="lblReferrer" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td class="ms-authoringcontrols" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top" colspan="3">
            <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif"></td>
	                <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="3">
                        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td><label>Select Product Code<font color="red">*</font></label><asp:label id="lblProductID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></td>
                                <td> : </td>
                                <td>
                                    <asp:UpdatePanel ID="updPanelSearchProduct" runat="server" >
                                        <ContentTemplate >
                                            <asp:DropDownList ID="cboProductCode" runat="server" AutoPostBack="True" CssClass="ms-long" OnSelectedIndexChanged="cboProductCode_SelectedIndexChanged"> </asp:DropDownList>
                                            <asp:TextBox ID="txtProductCode" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short"></asp:TextBox>
                                            <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Product code' must not be left blank." Display="Dynamic" ControlToValidate="cboProductCode"></asp:requiredfieldvalidator>
                                            <asp:ImageButton ID="cmdProductCode" runat="server" ImageUrl="../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" CausesValidation="False" Style="cursor: hand" OnClick="cmdProductCode_Click" />
                                            <asp:ImageButton id="imgProductHistory" runat="server" visible="false" ImageUrl="../_layouts/images/prodhist.gif" ToolTip="Show product inventory history report" CausesValidation="false" Style="cursor: hand" OnClick="imgProductHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgProductPriceHistory" runat="server" visible="false" ImageUrl="../_layouts/images/pricehist.gif" ToolTip="Show product price history report" CausesValidation="false" Style="cursor: hand" OnClick="imgProductPriceHistory_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgChangePrice" runat="server" visible="false" ImageUrl="../_layouts/images/chprice.gif" ToolTip="Change price" CausesValidation="false" Style="cursor: hand" OnClick="imgChangePrice_Click" ></asp:ImageButton>
                                            <asp:ImageButton id="imgEditNow" runat="server" visible="false" ImageUrl="../_layouts/images/edit.gif" ToolTip="Edit this product" CausesValidation="false" Style="cursor: hand" OnClick="imgEditNow_Click" ></asp:ImageButton>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" >
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                            <ContentTemplate >
                                                <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
		                                            <colgroup>
			                                            <col width="4%">
			                                            <col width="43%" align="left">
                                                        <col width="10%" align="left">
                                                        <col width="10%" align="left">
                                                        <col width="10%" align="left">
                                                        <col width="10%" align="left">
                                                        <col width="10%" align="left">
			                                            <col width="1%">
		                                            </colgroup>
		                                            <tr>
			                                            <td class="ms-vb-user" align="right">
			                                            </td>
			                                            <td class="ms-vb-user" nowrap>
				                                            Unit
			                                            </td>
			                                            <td class="ms-vb-user">
				                                            Current Quantity
			                                            </td>
			                                            <td class="ms-vb-user">
			                                                Difference
			                                            <td class="ms-vb-user">
			                                                New Quantity
			                                            </td>
			                                            <td class="ms-vb-user">
				                                            Min Threshold
			                                            </td>
			                                            <td class="ms-vb-user">
				                                            Max Threshold
			                                            </td>
			                                            <td class="ms-vb-user">
			                                            </td>
		                                            <tr>
			                                            <td class="ms-vb-user" align="right">
				                                            <input type="checkbox" id="NA" runat="server" NAME="NA" visible="false">
			                                            </td>
			                                            <td class="ms-vb-user" nowrap>
				                                            <asp:Label ID="lblProductDesc" Runat="server"></asp:Label>
                                                            <asp:Label ID="Label2" Runat="server"> (Total) - </asp:Label>
                                                            <asp:Label ID="lblUnitName" Runat="server"></asp:Label>
			                                            </td>
			                                            <td class="ms-vb-user">
				                                            <asp:TextBox accessKey="C" id="txtQuantityBefore" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" Enabled=false Width="100%"></asp:TextBox>
			                                            </td>
			                                            <td class="ms-vb-user">
			                                                <asp:TextBox accessKey="C" id="txtDifference" runat="server" onkeypress="AllNum()" onKeyUp="InvAdjustmentComputeByDiff(this)" CssClass="ms-short" BorderStyle="Groove" BackColor="YellowGreen">0</asp:TextBox>
			                                            <td class="ms-vb-user">
			                                                <asp:TextBox accessKey="C" id="txtQuantityNow" runat="server" onkeypress="AllNum()" onKeyUp="InvAdjustmentComputeByQty(this)" CssClass="ms-short" BorderStyle="Groove">0</asp:TextBox>
			                                            </td>
			                                            <td class="ms-vb-user">
				                                            <asp:TextBox accessKey="C" id="txtMinThreshold" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="6" Width="80%"></asp:TextBox></td>
			                                            <td class="ms-vb-user">
				                                            <asp:TextBox accessKey="C" id="txtMaxThreshold" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="6" Width="80%"></asp:TextBox></td>
			                                            <td class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
					                                            </A>
			                                            </td>
		                                            </tr>
	                                            </table>
                                            </ContentTemplate>
                                            <Triggers> 
                                                <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            </Triggers> 
                                        </asp:UpdatePanel>
                                    </asp:PlaceHolder>
	                            </td>
                            </tr>
                        </table>
                    </td>
	                <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer" colspan="5"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif"></td>
	                <td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="3">
	                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                            <ContentTemplate >
                                <asp:Label ID="lblVariationMatrix" Runat="server" Visible="False">Product Matrix </asp:Label>
	                            <asp:hyperlink id="lnkVariationMatrixAdd" runat="server" ToolTip="Add new variation matrix for this product." Visible="False">+</asp:hyperlink>
	                        </ContentTemplate>
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                            </Triggers> 
                        </asp:UpdatePanel>
	                </td>
	                <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif"></td>
	                <td colspan="3">
	                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                            <ContentTemplate >
	                            <asp:datalist id="lstVariationMatrix" runat="server" Width="100%" cellpadding="0" OnItemDataBound="lstVariationMatrix_ItemDataBound">
		                            <HeaderTemplate>
			                            <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
				                            <colgroup>
					                            <col width="4%">
			                                    <col width="43%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
                                                <col width="10%" align="left">
			                                    <col width="1%">
				                            </colgroup>
				                            <tr>
					                            <th class="ms-vh2" style="padding-bottom: 4px;"></th>
					                            <th class="ms-vh2" style="padding-bottom: 4px;"><asp:hyperlink id="SortByUnit" runat="server">Unit</asp:hyperlink></th>
					                            <th class="ms-vh2" style="padding-bottom: 4px;"><asp:hyperlink id="SortByQuantity" runat="server">Current Quantity</asp:hyperlink></th>
						                        <th class="ms-vh2" style="padding-bottom: 4px;"><asp:hyperlink id="Hyperlink1" runat="server">Difference</asp:hyperlink></th>
					                            <th class="ms-vh2" style="padding-bottom: 4px;"><asp:hyperlink id="SortByPrice" runat="server">New Quantity</asp:hyperlink></th>
					                            <th class="ms-vh2" style="padding-bottom: 4px;"><asp:hyperlink id="SortByEVAT" runat="server">Min Threshold</asp:hyperlink></th>
					                            <th class="ms-vh2" style="padding-bottom: 4px;"><asp:hyperlink id="SortByLocalTax" runat="server">Max Threshold</asp:hyperlink></th>
					                            <th class="ms-vh2" style="padding-bottom: 4px;">
					                            </th>
				                            </tr>
		                            </HeaderTemplate>
		                            <ItemTemplate>
				                            <tr onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
					                            <td class="ms-vb-user" align="right">
						                            <input type="checkbox" id="chkMatrixID" runat="server" NAME="chkMatrixID" visible="false" />
					                            </td>
					                            <td class="ms-vb-user" nowrap>
					                                <asp:Label ID="lblVariationDesc" Runat="server"></asp:Label>
					                                <asp:Label ID="Label2" Runat="server"> - </asp:Label>
						                            <asp:Label ID="lblUnitCode" Runat="server"></asp:Label>
					                            </td>
					                            <td class="ms-vb-user">
						                            <asp:TextBox accessKey="C" id="txtQuantityBefore" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" Enabled=false Width=100%></asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtDifference" runat="server" onkeypress="AllNum()" onKeyUp="InvAdjustmentComputeMatrixByDiff(this)" CssClass="ms-short" BorderStyle="Groove" BackColor="YellowGreen">0</asp:TextBox>
					                            <td class="ms-vb-user">
					                                <asp:TextBox accessKey="C" id="txtQuantityNow" runat="server" onkeypress="AllNum()" onKeyUp="InvAdjustmentComputeMatrixByQty(this)" CssClass="ms-short" BorderStyle="Groove">0</asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
						                            <asp:TextBox accessKey="C" id="txtMinThreshold" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="6" Width=70%></asp:TextBox>
					                            </td>
					                            <td class="ms-vb-user">
						                            <asp:TextBox accessKey="C" id="txtMaxThreshold" runat="server" onkeypress="AllNum()" CssClass="ms-short" BorderStyle="Groove" MaxLength="6" Width=70%></asp:TextBox>
					                            </td>
					                            <td class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
							                            <asp:Image id="divExpCollAsst_img" ImageUrl="../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="False"></asp:Image></A>
					                            </td>
				                            </tr>
		                            </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
	                            </asp:datalist>
	                        </ContentTemplate>
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveTwo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdSaveTwo" EventName="Click" />
                            </Triggers> 
                        </asp:UpdatePanel>
	                </td>
	                <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif" width="10"></td>
                </tr>
                
                <tr>
	                <td class="ms-formspacer" colspan="5"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
		<td id="AddUserTextTDID2"  colspan="3">
			<table class="ms-toolbar" id="onetidGrpsTC" cellspacing="0" cellpadding="2" border="0" width="100%">
				<tr>
					<td class="ms-toolbar" align="left" nowrap="nowrap" width="99%">
						<table cellspacing="0" cellpadding="1" border="0" width="99%">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">Remarks :</td>
								<td nowrap="nowrap" width="99%">
                                    <br />
                                    <asp:textbox id="txtRemarks" accessKey="Q" runat="server" CssClass="ms-long" BorderStyle="Groove" Width="100%" TextMode="MultiLine" Rows="5" BorderColor="Red"></asp:textbox></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator"><asp:label id="Label16" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveTwo" title="Save This Adjustment" accessKey="I" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/edit.gif" alt="Save this adjustment" border="0" width="16" height="16" OnClick="imgSaveTwo_Click" ></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveTwo" title="Save This Adjustment" accessKey="I" tabIndex="6" CssClass="ms-toolbar" runat="server" OnClick="cmdSaveTwo_Click" >Save Adjustment</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align052" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
