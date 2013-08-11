<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._ContactDetailed.__Insert" Codebehind="_Insert.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/DocumentScripts.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" ToolTip="Add New Customer" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Save New Customer" border="0" width="16" height="16" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSave" ToolTip="Add New Customer" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" ToolTip="Add New Customer" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Add New Customer" border="0" width="16" height="16" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Customer" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Adding New Customer" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Adding New Customer" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New Customer" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
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
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colspan="3">
						<asp:ValidationSummary id="ValidationSummary2" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></TD>
				</TR>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Group Code<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Department<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboGroup" CssClass="ms-short" runat="server" style="min-width: 190px"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" CssClass="ms-error" ForeColor=" " ControlToValidate="cboGroup" Display="Dynamic" ErrorMessage="'Group' must not be left blank."></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboDepartment" CssClass="ms-short" runat="server" style="min-width: 190px">
									</asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" CssClass="ms-error" ErrorMessage="'Department.' must not be left blank." Display="Dynamic" ControlToValidate="cboDepartment" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Business Name</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Position<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtBusinessName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75" style="min-width: 190px"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboPosition" CssClass="ms-short" runat="server" style="min-width: 190px"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" CssClass="ms-error" ErrorMessage="'Position.' must not be left blank." Display="Dynamic" ControlToValidate="cboPosition" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>First Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Middle Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Last Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboSalutation" CssClass="ms-short" runat="server" style="width:auto"></asp:dropdownlist>
                                    <asp:textbox id="txtFirstName" accessKey="C" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="85"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'First name.' must not be left blank." Display="Dynamic" ControlToValidate="txtFirstName" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtMiddleName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="85" style="min-width: 190px"></asp:textbox>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="txtMiddleName" Display="Dynamic" ErrorMessage="'Middle name.' must not be left blank." ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:textbox id="txtLastName" accessKey="G" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="85"></asp:textbox>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator5" CssClass="ms-error" runat="server" ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="'Last name.' must not be left blank." ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
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
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Address<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Address2</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:textbox id="txtAddress1" accessKey="A" runat="server" CssClass="ms-long" MaxLength="150" Height="40px" TextMode="MultiLine" Rows="5" BorderStyle="Groove" style="width: 465px"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" CssClass="ms-error" ErrorMessage="'Address' must not be left blank." Display="Dynamic" ControlToValidate="txtAddress1" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols"  colspan="3">
                                    <asp:textbox id="txtAddress2" accessKey="A" runat="server" CssClass="ms-long" MaxLength="150" Height="40px" TextMode="MultiLine" Rows="5" BorderStyle="Groove" style="width: 465px"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>City<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>State<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>ZipCode<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Country<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtCity" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator21" runat="server" CssClass="ms-error" ErrorMessage="'Credit.' must not be left blank." Display="Dynamic" ControlToValidate="txtCity" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtState" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtZipCode" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboCountry" CssClass="ms-short" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator18" runat="server" CssClass="ms-error" ErrorMessage="'Country' must not be left blank." Display="Dynamic" ControlToValidate="cboCountry" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Bussiness Phone No</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Home Phone No</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Mobile No</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Fax No</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtBusinessPhoneNo" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtHomePhoneNo" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtMobileNo" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtFaxNo" runat="server" accesskey="G" CssClass="ms-short" MaxLength="11" BorderStyle="Groove"></asp:TextBox>
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
					<td class="ms-sectionline" colspan="3" height="1"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" vAlign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Birth date<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Spouse Birth date<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Spouse Name<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Anniversary Date<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtBirthDate" runat="server" accesskey="B" CssClass="ms-short" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtSpouseBirthDate" runat="server" accesskey="G" CssClass="ms-short" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtSpouseName" runat="server" accesskey="G" CssClass="ms-short" MaxLength="85" BorderStyle="Groove"></asp:TextBox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:TextBox id="txtAnniversaryDate" runat="server" accesskey="G" CssClass="ms-short" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="4" style="padding-bottom: 2px">
                                    <label>Email Address<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Sold By</label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Confirmed By</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols" colspan="3">
                                    <asp:textbox id="txtEmailAddress" accessKey="A" runat="server" CssClass="ms-short" MaxLength="150" BorderStyle="Groove" style="width: 80%;"></asp:textbox>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboSoldBy" CssClass="ms-short" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" CssClass="ms-error" ErrorMessage="'Sold By' must not be left blank." Display="Dynamic" ControlToValidate="cboSoldBy" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                                <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboConfirmedBy" CssClass="ms-short" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" CssClass="ms-error" ErrorMessage="'Confirmed By' must not be left blank." Display="Dynamic" ControlToValidate="cboConfirmedBy" ForeColor=" "></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="8" style="padding-bottom: 2px">
                                    <label>Remarks</label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img src="../../_layouts/images/trans.gif" width="10" alt="" /></td>
                                <td class="ms-authoringcontrols" colspan="7">
                                    <asp:TextBox id="txtRemarks" runat="server" CssClass="ms-long" MaxLength="120" BorderStyle="Groove" style="min-width: 1000px;width:90%" TextMode="MultiLine" Rows="3" ></asp:TextBox>
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
					<td class="ms-sectionline" colspan="3" height="2"><IMG alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
