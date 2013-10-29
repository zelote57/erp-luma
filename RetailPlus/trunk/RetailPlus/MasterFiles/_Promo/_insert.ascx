<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Promo.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" ToolTip="Add New Promo" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Promo" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSave" ToolTip="Add New Promo" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" ToolTip="Add New Promo" accessKey="B" tabIndex="1" height="16" width="16" border="0" alt="Add New Promo" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Promo" accessKey="B" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveAdd" title="Save New Promo and Add Items" accessKey="A" tabIndex="1" height="16" width="16" border="0" alt="Save New Promo and Add Items" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveAdd" title="Save New Promo and Add Items" accessKey="A" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveAdd_Click">Save and Add Items</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Promo" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Promo" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Promo" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px">
						<font color="red">*</font> Indicates a required field
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1"></td>
				</tr>
				<tr>
					<td valign="top" style="padding-bottom: 20px">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 1:&nbsp;General 
							Information</div>
						<div class="ms-descriptiontext" style="padding-bottom:	10px">
							Enter promo code.</div>
						<div class="ms-descriptiontext" style="padding-bottom:	10px">
							Enter promo name.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 1px solid; PADDING-LEFT:	8px; padding-bottom:	20px">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom:2px" colspan="2">
									<label>Promo Code<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:TextBox id="txtPromoCode" runat="server" accesskey="G" CssClass="ms-short" MaxLength="60" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Promo code' must not be left blank." Display="Dynamic" ControlToValidate="txtPromoCode" ForeColor=" "></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom:2px" colspan="2">
									<label>Promo&nbsp;Name<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:TextBox id="txtPromoName" runat="server" accesskey="G" CssClass="ms-short" MaxLength="75" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" CssClass="ms-error" ErrorMessage="'Promo name.' must not be left blank." Display="Dynamic" ControlToValidate="txtPromoName" ForeColor=" "></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1"></td>
				</tr>
				<tr>
					<td valign="top" style="padding-bottom: 20px">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 2:&nbsp;Promo Type</div>
						<div class="ms-descriptiontext" style="padding-bottom:	10px">
							Select promotional type.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 1px solid; PADDING-LEFT:	8px; padding-bottom:	20px">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom:2px" colspan="2">
									<label>Select Promo Type<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:dropdownlist id="cboPromoType" CssClass="ms-long" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Promo type' must not be left blank." Display="Dynamic" ControlToValidate="cboPromoType" ForeColor=" "></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="1"></td>
				</tr>
				<tr>
					<td valign="top" style="padding-bottom: 20px">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 3:&nbsp;Date 
							coverage</div>
						<div class="ms-descriptiontext" style="padding-bottom:	10px">
							Enter start date of the promo.</div>
						<div class="ms-descriptiontext" style="padding-bottom:	10px">
							Enter end date of the promo.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 1px solid; PADDING-LEFT:	8px; padding-bottom:	20px">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom:2px" colspan="2">
									<label>Start Date <font color="blue">(yyyy-mm-dd)</font><font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
									<asp:TextBox id="txtStartDate" ondblclick="ontime(this)" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10" ToolTip="Double click to select date from Calendar" Width="72px"></asp:TextBox>
						            <asp:TextBox ID="txtStartTime" runat="server" AccessKey="I" BorderStyle="Groove" CssClass="ms-short" MaxLength="5" Width="46px">00:00</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Promo start date' must not be left blank." Display="Dynamic" ControlToValidate="txtStartDate" ForeColor=" "></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom:2px" colspan="2">
									<label>End Date <font color="blue">(yyyy-mm-dd)</font><font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%">
								    <asp:TextBox id="txtEndDate" ondblclick="ontime(this)" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10" ToolTip="Double click to select date from Calendar" Width="72px"></asp:TextBox>
						            <asp:TextBox ID="txtEndTime" runat="server" AccessKey="I" BorderStyle="Groove" CssClass="ms-short" MaxLength="5" Width="46px">23:59</asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" CssClass="ms-error" ErrorMessage="'Promo end date' must not be left blank." Display="Dynamic" ControlToValidate="txtEndDate" ForeColor=" "></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
