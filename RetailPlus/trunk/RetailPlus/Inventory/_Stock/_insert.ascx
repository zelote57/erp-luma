<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Inventory._Stock.__Insert" Codebehind="_Insert.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="cmdSaveAddItem" ToolTip="Add New Stock Transaction and add item(s)" accessKey="I" tabIndex="1" height="16" width="16" border="0" alt="Add New Stock Transaction and add item(s)" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="cmdSaveAddItem_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="imgSaveAddItem" ToolTip="Add New Stock Transaction and add item(s)" accessKey="I" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="imgSaveAddItem_Click">Save and Additem</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSave" ToolTip="Add New Stock Transaction" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Add New Stock Transaction" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSave_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSave" ToolTip="Add New Stock Transaction" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save and New</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgSaveBack" ToolTip="Add New Stock Transaction" accessKey="B" tabIndex="1" height="16" width="16" border="0" alt="Add New Stock Transaction" ImageUrl="../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdSaveBack" ToolTip="Add New Stock Transaction" accessKey="B" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">|</td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgCancel" title="Cancel Adding New Stock Transaction" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Adding New Stock Transaction" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdCancel" title="Cancel Adding New Stock Transaction" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
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
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
			    <tr>
					<td class="ms-descriptiontext" style="padding-bottom: 4px; PADDING-TOP: 8px" colspan="3"><font color="red">*</font>
						Indicates a required field
					</td>
				</tr>
				<tr>
					<td class="ms-descriptiontext" style="padding-bottom: 10px; PADDING-TOP: 8px" colspan="3">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" CssClass="ms-error" ForeColor=" "></asp:ValidationSummary></td>
				</tr>
				<tr>
					<td class="ms-sectionline" colspan="3" height="1"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" class="ms-authoringcontrols ms-formwidth" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="ms-authoringcontrols" width="90%">
                            <tr>
                                <td class="ms-authoringcontrols" colspan="3" style="padding-bottom: 2px">
                                    <label>Select Branch&nbsp;<b>:</b>&nbsp;</label><asp:dropdownlist id="cboBranch" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
                                <td class="ms-authoringcontrols" colspan="3" style="padding-bottom: 2px">
                                    <label>Transaction Number&nbsp;<b>:</b>&nbsp;</label><asp:Label id="lblTransactionNo" CssClass="ms-error" runat="server"></asp:Label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Stock Date&nbsp;<b>:</b></label>&nbsp;<asp:Label id="lblStockDate" CssClass="ms-error" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Supplier<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Stock Type<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Description<font color="red">*</font></label></td>
                                <td class="ms-authoringcontrols" colspan="2" style="padding-bottom: 2px">
                                    <label>Direction<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboSupplier" CssClass="ms-short" runat="server" AutoPostBack="True"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Supplier' must not be left blank." Display="Dynamic" ControlToValidate="cboSupplier"></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:dropdownlist id="cboStockTypes" CssClass="ms-short" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboStockTypes_SelectedIndexChanged"></asp:dropdownlist>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'Stock Type' must not be left blank." Display="Dynamic" ControlToValidate="cboStockTypes"></asp:RequiredFieldValidator>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox id="txtStockTypeDescription" accessKey="C" CssClass="ms-short" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboStockTypes" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="ms-formspacer"><img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox id="txtStockDirection" accessKey="C" CssClass="ms-short" runat="server" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboStockTypes" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer" height="20">
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-authoringcontrols" colspan="8" style="padding-bottom: 2px">
                                    <label>Remarks<font color="red">*</font></label></td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer">
                                    <img alt="" src="../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=7>
                                    <asp:TextBox id="txtRemarks" runat="server" accesskey="R" CssClass="ms-long" MaxLength="150" BorderStyle="Groove" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
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
					<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
