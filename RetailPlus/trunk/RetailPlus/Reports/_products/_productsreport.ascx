<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__ProductsReport" Codebehind="_ProductsReport.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
					<td class="ms-toolbar">
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
					<td class="ms-toolbar" style="WIDTH: 234px">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap>
									<asp:DropDownList id="cboReportType" runat="server" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="cboReportType_SelectedIndexChanged">
										<asp:ListItem Value="0" Selected="True">Select Report Type</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" nowrap id="align01">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" nowrap align="left">
									<asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click"></asp:Button>
								</td>
							</TR>
						</TABLE>
					</TD>
					<TD class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgBack" title="Back to previous window" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdBack" title="Back to previous window" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap id="align032" width="99%" >
						<IMG SRC="../_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
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
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
									<label>Filter by Branch</label>
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
								<td style="HEIGHT: 15px" colspan="3">
									<asp:dropdownlist id="cboBranch" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    
								</td>
                                <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
								</td>
								<td width="99%" id="Td2" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<tr>
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
									<label>Filter by Supplier</label>
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
								<td style="HEIGHT: 15px" colspan="3">
									<asp:dropdownlist id="cboContact" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    
								</td>
                                <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
                                    <asp:textbox id="txtContactCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgContactCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand; width: 16px;" accessKey="P" 
                                        ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgContactCodeSearch_Click"></asp:imagebutton>
								</td>
								<td width="99%" id="Td3" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            <tr>
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
                                    <label>Filter by Group</label>
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
								<td style="HEIGHT: 15px" colspan="3">
                                    <asp:dropdownlist id="cboProductGroup" CssClass="ms-long" runat="server" AutoPostBack="True" onselectedindexchanged="cboProductGroup_SelectedIndexChanged"></asp:dropdownlist>
								</td>
                                <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
                                    <asp:textbox id="txtProductGroupCode" accessKey="G" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgProductGroupCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand" accessKey="P" 
                                        ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgProductGroupCodeSearch_Click"></asp:imagebutton>
                                </td>
								<td width="99%" id="Td4" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            <tr>
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
									<label>Filter by Sub Group</label>
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
								<td style="HEIGHT: 15px" colspan="3">
									<asp:DropDownList id="cboSubGroup" runat="server" CssClass="ms-long"></asp:DropDownList>
								</td>
                                <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
                                    <asp:textbox id="txtSubGroupCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgSubGroupCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand" accessKey="P" 
                                        ImageUrl="../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgSubGroupCodeSearch_Click"></asp:imagebutton>
                                </td>
								<td width="99%" id="align02" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<asp:PlaceHolder ID="holderWeighted" runat="server" Visible=false>
							<tr>
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
									<label>Check if will Include Buying Price</label>
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
								<td style="HEIGHT: 15px">
									<asp:CheckBox id="chkIncludeBuying" runat=server Checked=true text="" />
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;|&nbsp;&nbsp;</TD>
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
									<label>Check if will Include Margin</label>
								</td>
								<td style="HEIGHT: 15px">
									<asp:CheckBox id="chkIncludeMargin" runat=server Checked=true text="" />
								</td>
								<td width="99%" id="Td1" noWrap align="right"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							</asp:PlaceHolder>
							<tr>
								<td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap>
									<label>Product Code like</label>&nbsp;
								</td>
								<TD class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</TD>
								<td colspan="4">
									<asp:TextBox id="txtProductCode" runat="server" TextMode="MultiLine" Rows="1" Width="100%"></asp:TextBox>
									<asp:Label id="Label4" CssClass="ms-error" runat="server">Enter 'Product Code' separated by semi-colon(;) to filter more than one product code.</asp:Label>
								</td>
								<td width="99%" id="align03" noWrap align="right" style="HEIGHT: 15px"><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="1">
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
					    <cr:crystalreportviewer id="CRViewer" runat="server" Width="100%" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
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