<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__ClosingInventory" Codebehind="_ClosingInventory.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG SRC="../_layouts/images/blank.gif" width="10" height="1" alt=""></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar" style="WIDTH: 234px">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap>
									<a tabindex="2" ID="idGroup" class="ms-toolbar" ACCESSKEY="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" ToolTip="Show Report" accessKey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="../_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap width="100">
									<asp:Label ID="Label2" Runat="server" text="Select Group :">Report Options</asp:Label>
								</td>
								<td class="ms-toolbar" nowrap>
									<asp:DropDownList id="cboReportOptions" runat="server" Width="150">
										<asp:ListItem Value="0" Selected="True">Web Format</asp:ListItem>
										<asp:ListItem Value="1">Acrobat PDF</asp:ListItem>
										<asp:ListItem Value="2">Microsoft Word</asp:ListItem>
										<asp:ListItem Value="3">Microsoft Excel</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" align="right" nowrap id="align01">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" nowrap align="left">
									<asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click" OnClientClick="NewWindow();"></asp:Button>
								</td>
							</TR>
						</TABLE>
					</TD>
					<TD class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap style="width: 19px"><asp:imagebutton id="imgBack" accessKey="B" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" accessKey="B" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" OnClick="cmdBack_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap id="align032" width="99%" >
						<IMG SRC="../_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
		    <asp:Label ID="lblBranchID" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblType" runat="server" Visible="False"></asp:Label>
		</TD>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellSpacing="0" cellPadding="0" border="0" width="100%">
							<tr>
				                <td style="PADDING-BOTTOM:2px" nowrap>
					                <label>Posting Date From & To</label>&nbsp;
				                &nbsp;&nbsp;&nbsp;</td>
				                <td colspan="35">
					                <asp:TextBox id="txtStartTransactionDate" ondblclick="ontime(this)" accessKey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10" ToolTip="Double click to select date from Calendar" ></asp:TextBox>
                                    &nbsp;-&nbsp;
                                    <asp:TextBox id="txtEndTransactionDate" ondblclick="ontime(this)" accessKey="E" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10" ToolTip="Double click to select date from Calendar" ></asp:TextBox>
                                    <asp:ImageButton id="cmdSearch" title="Search Reference Numbers in specific posting dates" style="CURSOR: hand" accessKey="s" BorderStyle="Groove" runat="server" ImageUrl="../_layouts/images/icongo01.gif" border="0" alt="Execute search" causesvalidation=false Height="14px" OnClick="cmdSearch_Click"></asp:ImageButton>
                                    &nbsp;&nbsp;
                                    <asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
					                <asp:Label id="Label4" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
                                </td>
				                <%--<%--<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				                <td style="PADDING-BOTTOM:2px" nowrap>
					                <%--<label>Posting Date To</label>
				                    
				                </td>
                                <td></td>--%>
				                <td width="99%" id="Td3" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
			                </tr>
			                <tr>
								<td style="PADDING-BOTTOM:2px;" nowrap>
									<label>Filter by Inventory No.</label>&nbsp;&nbsp;&nbsp;</td>
								<td colspan=5>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
									        <asp:DropDownList id="cboInventoryNo" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboInventoryNo_SelectedIndexChanged"></asp:DropDownList>
									    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
								<td width="99%" id="Td1" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<tr>
								<td style="PADDING-BOTTOM:2px;" nowrap>
									<label>Filter by Contact</label>
								&nbsp;&nbsp;&nbsp;</td>
								<td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
									        <asp:dropdownlist id="cboContact" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboInventoryNo" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
								<TD class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
                                <td style="PADDING-BOTTOM:2px;" nowrap>
									<label>Filter by Group</label>
								</td>
                                <td>
									<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
									        <asp:DropDownList id="cboGroup" runat="server" CssClass="ms-short"></asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboInventoryNo" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
								</td>
                                <td></td>
								<td width="99%" id="align02" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<tr>
								<td style="PADDING-BOTTOM:2px;" nowrap>
									<label>Include products without short/over </label>&nbsp;&nbsp;&nbsp;</td>
								<td colspan="5">
									<asp:CheckBox id="chkIncludeShortOverProducts" runat="server" CssClass="ms-short" Checked=true></asp:CheckBox>
								</td>
								<td width="99%" id="Td2" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align=center>
					    <cr:crystalreportviewer id="CRViewer" runat="server" Width="350px" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
					</td>
				</tr>
			</table>
		</TD>
		<td><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>