<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._TransferIn.__Insert" Codebehind="_Insert.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgSaveAddItem" ToolTip="Add New TransferIn Order and add item(s)" accessKey="I" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Add New TransferIn Order and add item(s)" border="0" width="16" height="16" OnClick="imgSaveAddItem_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap><asp:linkbutton id="cmdSaveAddItem" ToolTip="Add New TransferIn Order and add item(s)" accessKey="I" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveAddItem_Click">Save and Additem</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgSave" ToolTip="Add New TransferIn Order" accessKey="S" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Add New TransferIn Order" border="0" width="16" height="16" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap><asp:linkbutton id="cmdSave" ToolTip="Add New TransferIn Order" accessKey="S" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgSaveBack" ToolTip="Add New TransferIn Order" accessKey="B" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/saveitem.gif" alt="Add New TransferIn Order" border="0" width="16" height="16" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap><asp:linkbutton id="cmdSaveBack" ToolTip="Add New TransferIn Order" accessKey="B" tabIndex="2" CssClass="ms-toolbar" runat="server" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap><asp:imagebutton id="imgCancel" title="Cancel Adding New TransferIn Order" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Cancel Adding New TransferIn Order" border="0" width="16" height="16" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap><asp:linkbutton id="cmdCancel" title="Cancel Adding New TransferIn Order" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ControlToValidate="txtRequiredDeliveryDate" Display="Dynamic" ErrorMessage="'Required Delivery Date' must be a valid date." Type="Date" Operator="DataTypeCheck" ForeColor=" "></asp:CompareValidator></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 4px; PADDING-TOP: 8px" colSpan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan=3 class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 20px" valign="top">
					    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
			                <ContentTemplate >
                                <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>TransferIn Number<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>TransferIn Date<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Required Delivery Date<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:label id="lblTransferInNo" CssClass="ms-error" runat="server"></asp:label>
                                        </td>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:label id="lblTransferInDate" CssClass="ms-error" runat="server"></asp:label>
                                        </td>
                                        <td class="ms-formspacer"><img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan="3">
                                            <asp:textbox id="txtRequiredDeliveryDate" accessKey="D" CssClass="ms-short" ToolTip="Double Click to Select Date From Calendar" ondblclick="ontime(this)" runat="server" BorderStyle="Groove" MaxLength="10"></asp:textbox><asp:label id="Label1" CssClass="ms-error" runat="server"> 'yyyy-mm-dd' format</asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Select Supplier<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Supplier Contact<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Supplier Tel No.<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Terms<font color="red">*</font></label></td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:dropdownlist id="cboSupplier" CssClass="ms-short" runat="server" AutoPostBack="True" onselectedindexchanged="cboSupplier_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" CssClass="ms-error" runat="server" ControlToValidate="cboSupplier" Display="Dynamic" ErrorMessage="'Supplier' must not be left blank."></asp:requiredfieldvalidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtSupplierContact" accessKey="C" CssClass="ms-short" runat="server" BorderStyle="Groove"></asp:textbox>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:textbox id="txtSupplierTelephoneNo" accessKey="T" CssClass="ms-short" runat="server" BorderStyle="Groove"></asp:textbox>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:label id="lblTerms" CssClass="ms-error" runat="server"></asp:label>
									        <asp:label id="lblModeOfterms" CssClass="ms-error" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Supplier Address<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan=7>
                                            <asp:textbox id="txtSupplierAddress" accessKey="A" CssClass="ms-short" runat="server" TextMode=MultiLine BorderStyle="Groove" Width="100%"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Transfer IN to Branch<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Branch Address<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols">
                                            <asp:dropdownlist id="cboBranch" CssClass="ms-short" runat="server" AutoPostBack="True" onselectedindexchanged="cboBranch_SelectedIndexChanged"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator2" CssClass="ms-error" runat="server" ControlToValidate="cboBranch" Display="Dynamic" ErrorMessage="'Branch' must not be left blank."></asp:requiredfieldvalidator>
                                        </td>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan=5>
                                            <asp:textbox id="txtBranchAddress" accessKey="B" CssClass="ms-long" runat="server" ReadOnly="True" BorderStyle="Groove" Width="100%"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer" height="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            <label>Remarks<font color="red">*</font></label></td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                        <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                            <img src="../../_layouts/images/trans.gif" width="10" /></td>
                                        <td class="ms-authoringcontrols" colspan=7>
                                            <asp:textbox id="txtRemarks" accessKey="R" CssClass="ms-long" runat="server" BorderStyle="Groove" Rows="5" TextMode="MultiLine" Width="100%" MaxLength="150"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ms-formspacer">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
			            <Triggers> 
                        </Triggers>
			        </asp:UpdatePanel>
					</td>
				</tr>
				<tr>
					<td class="ms-sectionline" colSpan="3" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
