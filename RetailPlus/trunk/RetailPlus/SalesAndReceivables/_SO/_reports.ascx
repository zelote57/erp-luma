<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.SalesAndReceivables._SO.__Reports" Codebehind="_Reports.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
    rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
            </tr>
            <tr>
                <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
                <td>
                    <table style="margin-left: 3px" class="ms-toolbar" cellspacing="0" cellpadding="2" border="0">
                        <tbody>
                            <tr>
                                <td style="WIDTH: 234px" class="ms-toolbar">
                                    <table cellspacing="0" cellpadding="1" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="ms-toolbar" nowrap="nowrap">
                                                    <asp:imagebutton accessKey="V" id="imgView" tabIndex="1" runat="server" CssClass="ms-toolbar" ImageUrl="../../_layouts/images/tabpub.gif" ToolTip="View Report" border="0" width="16" height="16"></asp:imagebutton> 
                                                </td>
                                                <td class="ms-toolbar" nowrap="nowrap" width="100"><asp:Label id="Label2" Runat="server">Report Options</asp:Label> </td>
                                                <td class="ms-toolbar" nowrap="nowrap">
                                                    <asp:DropDownList id="cboReportOptions" runat="server" Width="150">
										                <asp:ListItem Value="0" Selected="True">Web Format</asp:ListItem>
										                <asp:ListItem Value="1">Acrobat PDF</asp:ListItem>
										                <asp:ListItem Value="2">Microsoft Word</asp:ListItem>
										                <asp:ListItem Value="3">Microsoft Excel</asp:ListItem>
									                </asp:DropDownList> 
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td id="align01" class="ms-toolbar" nowrap="nowrap" align="right">
                                    <asp:UpdatePanel id="updPrint" runat="server">
                                        <ContentTemplate>
						                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
							                    <tr>
								                    <td class="ms-toolbar" nowrap="nowrap" align="left">
									                    <asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click" OnClientClick="NewWindow();"></asp:Button>
								                    </td>
							                    </tr>
						                    </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></td>
                                <td class="ms-toolbar">
                                    <table cellspacing="0" cellpadding="1" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="WIDTH: 19px" class="ms-toolbar" nowrap="nowrap"><asp:imagebutton accessKey="B" id="imgBack" tabIndex="3" onclick="imgBack_Click" runat="server" CssClass="ms-toolbar" ImageUrl="../../_layouts/images/impitem.gif" border="0" width="16" height="16" CausesValidation="False" alt="Back to previous window"></asp:imagebutton></td>
                                                <td nowrap="nowrap"><asp:linkbutton accessKey="B" id="cmdBack" tabIndex="4" onclick="cmdBack_Click" runat="server" CssClass="ms-toolbar" CausesValidation="False">Back to previous window</asp:linkbutton></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td id="align032" class="ms-toolbar" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /> </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:label id="lblReferrer" runat="server" Visible="False"></asp:label> <asp:label id="lblSOID" runat="server" Visible="False"></asp:label> <asp:label id="lblReportType" runat="server" Visible="False"></asp:label>
                </td>
            </tr>
            <tr>
                <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="ms-sectionline" colspan="3" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
                            </tr>
                            <tr><td align="center" colspan="3" height="2">
								    <cr:crystalreportviewer id="CRViewer" runat="server" Width="350px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false"  Height="50px" HasCrystalLogo="False"></cr:crystalreportviewer> 
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
            </tr>
            <tr>
                <td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
            </tr>
        </tbody>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
