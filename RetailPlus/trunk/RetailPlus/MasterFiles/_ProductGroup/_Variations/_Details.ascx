<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._Group._Variations.__Details" Codebehind="_Details.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap style="width: 19px"><asp:imagebutton id="imgBack" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Product Group Variation" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="cmdBack_Click" >Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:Label id="lblReferrer" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductGroupVariationID" runat="server" Visible="False"></asp:Label>
			<asp:Label id="lblProductGroupID" runat="server" Visible="False"></asp:Label>
		</TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" colSpan="3"></TD>
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
                                    </td>
                            </tr>
                            <tr>
                                <td class="ms-formspacer"><img src="../../../_layouts/images/trans.gif" width="10" /></td>
                                <td class="ms-authoringcontrols" colspan=3>
                                    <asp:dropdownlist id="cboVariationType" CssClass="ms-long-disabled" runat="server" Enabled="False"></asp:dropdownlist>
									<asp:imagebutton id="imgAdd" ToolTip="Add New Variation" accessKey="N" tabIndex="1" CssClass="ms-toolbar" runat="server" ImageUrl="../../../_layouts/images/newuser.gif" alt="Add New Variation" border="0" width="16" height="16" CausesValidation="False" OnClick="imgAdd_Click"></asp:imagebutton>
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
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
