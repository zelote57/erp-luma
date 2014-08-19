<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._Setup.__PostingDates" Codebehind="_PostingDates.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../../_Scripts/calendar.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" title="Update Vendor" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Vendor" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" title="Update Vendor" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel UpdatingContact" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel UpdatingContact" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel UpdatingContact" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
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
					<td class="ms-descriptiontext" style="padding-bottom: 5px; PADDING-TOP: 5px" colspan="3">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<COLGROUP>
								<COL align="center" width="1%">
								<COL width="99%">
							</COLGROUP>
							<tr>
								<td></td>
								<td>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Allowing Date From.' must not be left blank." Display="Dynamic" ControlToValidate="txtDateFrom" ForeColor=" "></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td></td>
								<td>
									<asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ControlToValidate="txtDateFrom" Display="Dynamic" ErrorMessage="'Allowable Date From' must be a valid date." Type="Date" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:requiredfieldvalidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtDateTo" Display="Dynamic" ErrorMessage="'Allowing Date To.' must not be left blank." ForeColor=" "></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td></td>
								<td>
									<asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ControlToValidate="txtDateTo" Display="Dynamic" ErrorMessage="'Allowable Date To' must be a valid date." Type="Date" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><A name="InputFormSection1"></A><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td style="padding-bottom: 20px" valign="top">
						<div class="ms-sectionheader" style="padding-bottom: 8px">Step 1:&nbsp;Date of 
							allowable posting date</div>
						<div class="ms-descriptiontext" style="padding-bottom: 10px">Enter the minimum 
							allowing date to post.</div>
						<div class="ms-descriptiontext" style="padding-bottom: 10px">Enter the maximum 
							allowing date to post.</div>
					</td>
					<td class="ms-colspace">&nbsp;</td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
						<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2">
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label>Allowing 
										Date to Post - From (yyyy-mm-dd):<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:textbox id="txtDateFrom" accessKey="F" CssClass="ms-short" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" BorderStyle="Groove" MaxLength="10"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" CssClass="ms-error" ErrorMessage="l" Display="Dynamic" ControlToValidate="txtDateFrom" Font-Names="Wingdings" ForeColor=" "></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
							<tr>
								<td class="ms-authoringcontrols" style="padding-bottom: 2px" colspan="2"><label>Allowing 
										Date to Post - To (yyyy-mm-dd)<font color="red">*</font></label>
								</td>
							</tr>
							<tr>
								<td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10"></td>
								<td class="ms-authoringcontrols" width="100%"><asp:textbox id="txtDateTo" accessKey="T" CssClass="ms-short" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" BorderStyle="Groove" MaxLength="75"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator10" runat="server" CssClass="ms-error" ErrorMessage="l" Display="Dynamic" ControlToValidate="txtDateTo" Font-Names="Wingdings" ForeColor=" "></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="ms-formspacer"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
