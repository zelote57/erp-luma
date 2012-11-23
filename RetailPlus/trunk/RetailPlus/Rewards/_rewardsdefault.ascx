<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Rewards.__RewardsDefault" Codebehind="_rewardsdefault.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../_Scripts/ComputeMargin.js"></script>
<script language="JavaScript" src="../_Scripts/Rewards.js"></script>
<asp:Label ID="lblReferrer" runat="server" Visible="False"></asp:Label>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
    <TR>
        <td style="PADDING-RIGHT: 10px; PADDING-LEFT: 8px;" vAlign="top" colSpan="3">
            <asp:Chart ID="Chart1" runat="server" Width="400px" BackColor="Transparent" CssClass="ms-formlabel">
                <Legends>
                        <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                                Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False"
                                Name="Series1">
                        </asp:Legend>
                </Legends>
                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                <Titles>
                    <asp:Title Text="Active vs. InActive Members"> </asp:Title>
                </Titles>
                <series>
                    <asp:Series ChartType="Line" Name="Series1"></asp:Series>
                    <asp:Series ChartType="Line" Name="Series2"></asp:Series>
                </series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="transparent" BackSecondaryColor="Transparent"
                            BackColor="transparent" ShadowColor="Transparent">
                            <AxisY IsLabelAutoFit="False" IsStartedFromZero="False">
                            </AxisY>
                            <AxisX LineColor="transparent" IsLabelAutoFit="False" IntervalAutoMode="VariableCount">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="MMM d" />
                            </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </td>
    </TR>
</table>


