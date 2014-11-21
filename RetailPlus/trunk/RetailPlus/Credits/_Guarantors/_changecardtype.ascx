<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits._Guarantors.__ChangeCardType" Codebehind="_changecardtype.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        <td class="ms-sectionline" colspan="3" height="1">
            <asp:Label ID="lblReferrer" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td class="ms-authoringcontrols" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top" colspan="3">
            <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" /></td>
	                <td class="ms-authoringcontrols" style="padding-bottom: 2px; PADDING-TOP: 10px" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                            <ContentTemplate >
                                <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr >
                                        <td nowrap="nowrap"><label>Select Guarantor to change Credit Card Type</label><asp:label id="lblGuarantorID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></td>
                                        <td> : </td>
                                        <td nowrap="nowrap">
                                            <asp:DropDownList ID="cboContact" runat="server" AutoPostBack="True" CssClass="ms-long" OnSelectedIndexChanged="cboContact_SelectedIndexChanged"> </asp:DropDownList>
                                            <asp:TextBox ID="txtSearch" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short"></asp:TextBox>
                                            <asp:ImageButton ID="cmdContactSearch" runat="server" ImageUrl="../../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" CausesValidation="False" Style="cursor: hand" OnClick="cmdContactSearch_Click" />
                                        </td>
                                    </tr>
                                    <div id="divGuarantorInfo" runat="server" >
                                    <tr>
	                                    <td class="ms-formspacer" colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap"><label>Change the current issued credit card from</label></td>
                                        <td> : </td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox ID="txtCreditCardTypeCode" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-disabled"></asp:TextBox>
                                            &nbsp;to a new credit card type of&nbsp;
                                            <asp:DropDownList ID="cboCardType" runat="server" CssClass="ms-short"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" colspan="3"><img alt="" src="../../_layouts/images/blank.gif" height="30" /></td>
                                    </tr>
                                    <tr>
	                                    <td colspan="3">
                                            <asp:datalist id="lstItemCustomer" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItemCustomer_ItemDataBound" OnItemCommand="lstItemCustomer_ItemCommand">
				                                <HeaderTemplate>
					                                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						                                <colgroup>
							                                <col width="10">
                                                            <col width="10">
							                                <col width="28%">
							                                <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
							                                <col width="1%">
						                                </colgroup>
						                                <tr>
							                                <th class="ms-vh2" style="padding-bottom: 4px">
								                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
							                                <th class="ms-vh2" style="padding-bottom: 4px"></th>
							                                <th class="ms-vh2" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByContactName" runat="server">Customer Name</asp:hyperlink></th>
							                                <th class="ms-vh2" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByCreditType" runat="server">Credit Type</asp:hyperlink></th>
                                                            <th class="ms-vh2" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByCreditCardNo" runat="server">Card No</asp:hyperlink></th>
                                                            <th class="ms-vh2" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByCreditCardStatus" runat="server">Card Status</asp:hyperlink></th>
                                                            <th class="ms-vh2" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByTotalPurchases" runat="server">Credit Status</asp:hyperlink></th>
                                                            <th class="ms-vh2" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByExpiryDate" runat="server">Expiry Date</asp:hyperlink></th>
                                                            <th class="ms-vh2-r" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByCreditLimit" runat="server">Credit Limit</asp:hyperlink></th>
                                                            <th class="ms-vh2-r" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByCredit" runat="server">Credit</asp:hyperlink></th>
                                                            <th class="ms-vh2-r" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByAvailableCredit" runat="server">Available Credit</asp:hyperlink></th>
                                                            <th class="ms-vh2-r" style="padding-bottom: 4px">
								                                <asp:hyperlink id="SortByLastBillingDate" runat="server">Last Billing Date</asp:hyperlink></th>
                                                            <th class="ms-vh2" style="padding-bottom: 4px"></th>
						                                </tr>
					                                </table>
				                                </HeaderTemplate>
				                                <ItemTemplate>
					                                <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						                                <colgroup>
							                                <col width="10">
							                                <col width="10">
							                                <col width="28%">
							                                <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
                                                            <col width="8%">
							                                <col width="1%">
						                                </colgroup>
						                                <tr>
							                                <td class="ms-vb-user">
								                                <input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
							                                </td>
							                                <td class="ms-vb2">
							                                    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this Contact" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>&nbsp;
						                                    </td>
                                                            <td class="ms-vb-user">
								                                <asp:HyperLink ID="lnkContactName" Runat="server" Target="_blank"></asp:HyperLink>
							                                </td>
                                                            <td class="ms-vb-user">
								                                <asp:Label ID="lblCreditType" Runat="server"></asp:Label>
							                                </td>
							                                <td class="ms-vb-user">
								                                <asp:Label ID="lblCreditCardNo" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user">
								                                <asp:Label ID="lblCreditCardStatus" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user">
								                                <asp:Label ID="lblTotalPurchases" Runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCreditActive" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user">
								                                <asp:Label ID="lblExpiryDate" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user-right">
								                                <asp:Label ID="lblCreditLimit" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user-right">
								                                <asp:Label ID="lblCredit" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user-right">
								                                <asp:Label ID="lblAvailableCredit" Runat="server"></asp:Label>
							                                </td>
                                                            <td class="ms-vb-user-right" >
								                                <asp:Label ID="lblLastBillingDate" Runat="server"></asp:Label>
							                                </td>
							                                <td class="ms-vb2">
								                                <A class="DropDown" id="anchorDown" href="" runat="server">
									                                <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							                                </td>
						                                </tr>
					                                </table>
				                                </ItemTemplate>
			                                </asp:datalist>
                                        </td>
                                    </tr>
                                    <tr>
	                                    <td class="ms-formspacer" colspan="3"></td>
                                    </tr>
                                    </div>
                                </table>
                            </ContentTemplate >
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="cboContact" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cmdContactSearch" EventName="Click" />    
                                <asp:AsyncPostBackTrigger ControlID="imgSaveCardType" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdSaveCardType" EventName="Click" />
                            </Triggers> 
                        </asp:UpdatePanel>
                    </td>
	                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                </tr>
                <tr>
	                <td class="ms-formspacer" colspan="5"></td>
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
                        <asp:UpdatePanel ID="updSave" runat="server">
                            <ContentTemplate>
		                        <table cellspacing="0" cellpadding="1" border="0">
			                        <tr>
				                        <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveCardType" tooltip="Save the selected card type to the selected guarantor and creditor's" accessKey="S" tabIndex="1" height="16" width="16" border="0" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveCardType_Click"></asp:imagebutton>&nbsp;
								        </td>
								        <td nowrap="nowrap"><asp:linkbutton id="cmdSaveCardType" tooltip="Save the selected card type to the selected guarantor and creditor's" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" OnClick="cmdSaveCardType_Click">Save New Card Type</asp:linkbutton></td>
			                        </tr>
		                        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
	                </td>
	                <td class="ms-separator">|</td>
	                <td class="ms-toolbar">
			            <table cellspacing="0" cellpadding="1" border="0">
				            <tr>
					            <td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Applying New Card Type" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Applying New Card Type" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
					            <td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Applying New Card Type" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
				            </tr>
			            </table>
		            </td>
		            <td class="ms-toolbar" id="TD3" nowrap="nowrap" align="right" width="99%"></td>
				    <td class="ms-toolbar" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
			</table>
		</td>
	</tr>
</table>


