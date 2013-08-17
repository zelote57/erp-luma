<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._Stock.__Upload" Codebehind="_Upload.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel uploading stocks" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel uploading stocks" border="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel uploading stocks" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><A name="InputFormSection1"></A><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="3">
						<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">Step 1:&nbsp;Upload 
							Stocks to Inventory</div>
						<div class="ms-descriptiontext" style="PADDING-BOTTOM: 10px">Browse for the XML 
							file that you want to upload then click 'Upload and Back'.</div>
					</td>
				</tr>
				<tr>
					<td style="PADDING-BOTTOM: 20px" vAlign="top"></td>
					<td class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" vAlign="top" colSpan="2">
					<INPUT type="file" id="txtPath" name="txtPath" accept="*.xml" runat="server" BorderStyle="Groove" Class="ms-long">
					<font color="red">*</font>
					<asp:requiredfieldvalidator id="Requiredfieldvalidator4" CssClass="ms-error" runat="server" ControlToValidate="txtPath" Display="Dynamic" ErrorMessage="'Path' must not be left blank."></asp:requiredfieldvalidator>
					<br>
					<asp:Label id="Label1" runat="server"></asp:Label><br>
				</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD id="AddUserTextTDID2">
			<TABLE class="ms-toolbar" id="onetidGrpsTB1" style="MARGIN-LEFT: 3px" cellSpacing="0" cellPadding="2" border="0">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgUpload" title="Upload Stocks to Inventory now" accessKey="B" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Add New Stock Transaction" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdUpload" title="Upload Stocks to Inventory now" accessKey="U" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdUpload_Click">Upload and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align03" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
