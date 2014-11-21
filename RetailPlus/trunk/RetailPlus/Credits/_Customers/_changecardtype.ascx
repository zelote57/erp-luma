<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits._Customers.__ChangeCardType" Codebehind="_changecardtype.ascx.cs" %>
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
                                        <td nowrap="nowrap"><label>Select Member to change Credit Card Type</label><asp:label id="lblContactID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></td>
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


