<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Rewards.__RedeemRewards" Codebehind="_redeemrewards.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../_Scripts/ComputeMargin.js"></script>
<script language="JavaScript" src="../_Scripts/Rewards.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/blank.gif"></td>
	</tr>
	<tr>
        <td class="ms-sectionline" colSpan="3" height="1">
	        <%--<table class="ms-toolbar" id="threetidGrpsTBC" cellSpacing="0" cellPadding="2" border="0" width="100%">
		        <TR>
			        <td class="ms-toolbar">
				        <table cellSpacing="0" cellPadding="1" border="0">
					        <tr>
					            
					            <td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					            </td>
					        </tr>
				        </table>
			        </td>
			        <td class="ms-toolbar" id="Td1" noWrap align="right" width="99%"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					</td>
		        </TR>
	        </TABLE>--%>
            <asp:Label ID="lblReferrer" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <TR>
        <td class="ms-authoringcontrols" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top" colSpan="3">
            <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="ms-formspacer"><IMG alt="" src="../_layouts/images/trans.gif"></td>
	                <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px; PADDING-TOP: 10px" colspan=3>
                        <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
                            <tr >
                                <td nowrap><label>Select Member</label><asp:label id="lblProductGroupID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></td>
                                <td> : </td>
                                <td nowrap>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                                        <ContentTemplate >
                                            <asp:DropDownList ID="cboCustomer" runat="server" AutoPostBack="True" CssClass="ms-long" OnSelectedIndexChanged="cboCustomer_SelectedIndexChanged"> </asp:DropDownList>
                                            <asp:TextBox ID="txtCustomer" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short"></asp:TextBox>
                                            <asp:ImageButton ID="cmdCustomer" runat="server" ImageUrl="../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" CausesValidation="False" Style="cursor: hand" OnClick="cmdCustomer_Click" />
                                            <label class="ms-error">Enter Customer or Reward Card No to search</label>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboCustomer" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCustomer" EventName="Click" />    
                                            <asp:AsyncPostBackTrigger ControlID="imgRedeemRewards" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdRedeemRewards" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap><label>Reward Card No</label></td>
                                <td> : </td>
                                <td nowrap>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                        <ContentTemplate >
                                            <asp:TextBox ID="txtRewardCardNo" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-long-disabled"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboCustomer" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCustomer" EventName="Click" />    
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgRedeemRewards" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdRedeemRewards" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
	                            <td class="ms-formspacer" colSpan="3"></td>
                            </tr>
                            <tr>
                                <td nowrap><label>Select Specific Product to redeem</label><asp:label id="lblProductID" runat="server" CssClass="ms-error" Visible="False">0</asp:label></td>
                                <td> : </td>
                                <td nowrap>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                                        <ContentTemplate >
                                            <asp:DropDownList ID="cboProductCode" runat="server" AutoPostBack="True" CssClass="ms-long" OnSelectedIndexChanged="cboProductCode_SelectedIndexChanged"> </asp:DropDownList>
                                            <asp:TextBox ID="txtProductCode" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short"></asp:TextBox>
                                            <asp:ImageButton ID="cmdProductCode" runat="server" ImageUrl="../_layouts/images/SPSSearch2.gif" ToolTip="Execute search" CausesValidation="False" Style="cursor: hand" OnClick="cmdProductCode_Click" />
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboCustomer" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCustomer" EventName="Click" />    
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgRedeemRewards" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdRedeemRewards" EventName="Click" />
                                        </Triggers> 
                                        
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr >
                                <td nowrap style="PADDING-TOP: 8px"><label>Current Rewards Points (Redeemable)</label></td>
                                <td style="PADDING-TOP: 8px"> : </td>
                                <td nowrap style="PADDING-TOP: 8px">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                        <ContentTemplate >
                                            <asp:TextBox ID="txtCurrentRewardPoints" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-disabled">0</asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboCustomer" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCustomer" EventName="Click" />    
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgRedeemRewards" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdRedeemRewards" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr >
                                <td nowrap style="PADDING-TOP: 8px"><label>Enter/Adjust Reward Points To Redeem</label></td>
                                <td style="PADDING-TOP: 8px"> : </td>
                                <td nowrap style="PADDING-TOP: 8px">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                                        <ContentTemplate >
                                            <asp:TextBox ID="txtRedeemRewardPoints" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-required" onkeyup="ComputeRedeemRewards()" onkeypress="AllNum()">0</asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboCustomer" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCustomer" EventName="Click" />    
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgRedeemRewards" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdRedeemRewards" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr >
                                <td nowrap style="PADDING-TOP: 8px"><label>New Reward Points</label></td>
                                <td style="PADDING-TOP: 8px"> : </td>
                                <td nowrap style="PADDING-TOP: 8px">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                                        <ContentTemplate >
                                            <asp:TextBox ID="txtNewRewardPoints" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-short-numeric-disabled">0</asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers> 
                                            <asp:AsyncPostBackTrigger ControlID="cboCustomer" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdCustomer" EventName="Click" />    
                                            <asp:AsyncPostBackTrigger ControlID="cboProductCode" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdProductCode" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgRedeemRewards" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="cmdRedeemRewards" EventName="Click" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr >
                                <td nowrap style="PADDING-TOP: 8px"><label>Enter remarks</label></td>
                                <td style="PADDING-TOP: 8px"> : </td>
                                <td nowrap style="PADDING-TOP: 8px">
                                    <asp:TextBox ID="txtRemarks" runat="server" AccessKey="C" BorderStyle="Groove" CssClass="ms-long-required" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
	                <td class="ms-formspacer"><IMG alt="" src="../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer" colSpan="5"></td>
                </tr>
            </table>
	            
        </td>
    </TR>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/blank.gif"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td class="ms-sectionline" colSpan="3" height="1">
	        <TABLE class="ms-toolbar" id="TABLE2" cellSpacing="0" cellPadding="2" border="0" width="100%">
				<TR>
		            <td class="ms-toolbar">
		                <table cellSpacing="0" cellPadding="1" border="0">
			                <tr>
				                <td class="ms-toolbar" noWrap><asp:imagebutton id="imgRedeemRewards" tooltip="Deduct the Reward Points to the selected member" accessKey="S" tabIndex="1" height="16" width="16" border="0" ImageUrl="../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgRedeemRewards_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdRedeemRewards" tooltip="Deduct the Reward Points to the selected member" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" OnClick="cmdRedeemRewards_Click">Redeem Reward Points</asp:linkbutton></td>
			                </tr>
			                
		                </table>
	                </td>
	                <TD class="ms-separator">|</TD>
	                <td class="ms-toolbar">
			            <table cellSpacing="0" cellPadding="1" border="0">
				            <tr>
					            <td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Redeemning Points" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/impitem.gif" alt="Cancel Applying Taxes" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
					            <td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Redeemning Points" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
				            </tr>
			            </table>
		            </td>
		            <TD class="ms-toolbar" id="TD3" noWrap align="right" width="99%"></TD>
				    <td class="ms-toolbar" id="Td4" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1"></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>


