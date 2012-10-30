<%@ Reference Control="~/masterfiles/_product/_insert.ascx" %>
<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._Group._Charges.__Insert" Codebehind="_Insert.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" ToolTip="Add New Group Charge" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../../_layouts/images/saveitem.gif" alt="Add New Group Charge" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" ToolTip="Add New Group Charge" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" ToolTip="Add New Group Charge" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../../_layouts/images/saveitem.gif" alt="Add New Group Charge" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Group Charge" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Group Charge" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../../../_layouts/images/impitem.gif" alt="Cancel Adding New Group Charge" border="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Group Charge" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label><asp:label id="lblProductGroupID" runat="server" Visible="False"></asp:label></TD>
		<td><IMG height="1" alt="" src="../../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px" vAlign="top">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1:&nbsp;General 
							Information</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Select&nbsp; charge 
							type.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Change the default 
							charge amount.</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Check if the charge is 
							in percent or uncheck if the charge is in amount.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
						<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label for="txtAccountNo">Charge 
										Type<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:dropdownlist id="cboChargeType" CssClass="ms-long" runat="server" Width="157px" AutoPostBack="True" onselectedindexchanged="cboChargeType_SelectedIndexChanged"></asp:dropdownlist><asp:imagebutton id="imgAdd" ToolTip="Add New Charge" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../../_layouts/images/newuser.gif" alt="Add New Charge" border="0" width="16" height="16" CausesValidation="False"></asp:imagebutton><asp:requiredfieldvalidator id="Requiredfieldvalidator1" CssClass="ms-error" runat="server" ControlToValidate="cboChargeType" Display="Dynamic" ErrorMessage="'Charge type' must not be left blank."></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" colSpan="2"><label>Charge 
										Amount<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:textbox id="txtChargeAmount" accessKey="P" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="20">0</asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator3" CssClass="ms-error" runat="server" ControlToValidate="txtChargeAmount" Display="Dynamic" ErrorMessage="'Charge amount' must not be left blank."></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-formspacer"><IMG alt="" src="../../../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:checkbox id="chkInPercent" accessKey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" Text="In Percent"></asp:checkbox></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			    <tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
						<asp:ValidationSummary id="ValidationSummary2" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Variation type<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Card type Name<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:TextBox id="txtCardTypeCode" runat="server" accesskey="C" CssClass="ms-short" MaxLength="30" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Card Type Code' must not be left blank." Display="Dynamic" ControlToValidate="txtCardTypeCode"></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols"  colspan=3>
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
					<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
