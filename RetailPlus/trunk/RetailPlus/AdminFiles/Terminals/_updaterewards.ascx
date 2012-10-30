<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security._Terminals.__UpdateRewards" Codebehind="_updaterewards.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" title="Update Reward Point System" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Reward Point System" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" title="Update Reward Point System" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Updating Reward Point System" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Reward Point System" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Updating Terminal" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 5px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1: Define the parameters for earning Reward Points
						</div>
					</td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Enable Reward Points<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Round Down Reward Points<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" nowrap>
                                    <asp:CheckBox id="chkEnableRewardPoints" runat="server" Text="Check this box if reward point system will be enabled" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" nowrap>
                                    <asp:CheckBox id="chkRoundDownRewardPoints" runat="server" Text="Check this box if reward point system will ignore decimal points" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"></td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Reward Points Minimum Amount to join<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Credit x Points Every<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Reward Points to credit<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtRewardPointsMinimum" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="12">1</asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtRewardPointsEvery" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="12">1</asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtRewardPoints" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="12">0</asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 5px;" vAlign="top" colspan=3>
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 2: Define the parameters when redeeming or using as payment.
						</div>
					</td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Enable Reward Points as Payment<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    </td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" nowrap>
                                    <asp:CheckBox id="chkEnableRewardPointsAsPayment" runat="server" Text="Check this box if reward points can be use as payment" CssClass="ms-short"></asp:CheckBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" nowrap>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"></td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Reward Points Maximum % for Payment<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>x Points to Debit For Payment<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                    <label>Cash Value of x Points<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px" nowrap>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtRewardPointsMaxPercentageForPayment" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="6" Width="75">100</asp:textbox>%
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtRewardPointsPaymentValue" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="12">1</asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox onkeypress="AllNum()" id="txtRewardPointsPaymentCashEquivalent" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="12">0.00</asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>

