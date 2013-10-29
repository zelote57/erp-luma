<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Rewards.__Default" Codebehind="_default.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
<script language="JavaScript" src="../_Scripts/ComputeMargin.js"></script>
<script language="JavaScript" src="../_Scripts/Rewards.js"></script>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/blank.gif"></td>
	</tr>
	<tr>
        <td class="ms-sectionline" colspan="3" height="1">
            <asp:Label ID="lblReferrer" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td class="ms-authoringcontrols" style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; padding-bottom: 20px" valign="top" colspan="3">
            <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif"></td>
	                <td class="ms-authoringcontrols" style="padding-bottom: 2px; PADDING-TOP: 10px" colspan="3">
                        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr >
                                <td nowrap>&nbsp;
                                </td>
                                <td nowrap>
                                    <asp:Chart ID="Chart1" runat="server" runat="server" BorderlineColor="Black" 
                                                BorderlineDashStyle="Solid" BackColor="#B6D6EC" BackGradientStyle="TopBottom" 
                                                BackSecondaryColor="#B6D6EC" Height="450px" Width="850px">
                                        <Legends>
                                                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                                                        Font="Trebuchet MS, 7.25pt" IsTextAutoFit="true" Enabled="true"
                                                        Name="Series1Legend" Docking="Bottom" LegendStyle="Table" IsDockedInsideChartArea ="false"
                                                        Title="Active Members vs. New members vs. Inactive Members">
                                                </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss" backcolor="Olive" bordercolor="Olive"></BorderSkin>
                                        <series>
                                            <asp:Series Name="Series1" CustomProperties="DrawingStyle=Cylinder, MaxPixelPointWidth=50" ShadowOffset="2" xvaluetype="Auto" IsValueShownAsLabel="true" IsVisibleInLegend="true"
                                                Font="Trebuchet MS, 6.25pt, style=Bold" ChartArea="ChartArea1" Legend="Series1Legend"></asp:Series>
                                            <asp:Series Name="Series2" CustomProperties="DrawingStyle=Cylinder, MaxPixelPointWidth=50" ShadowOffset="2" xvaluetype="Auto" IsValueShownAsLabel="true" IsVisibleInLegend="true"
                                                Font="Trebuchet MS, 6.25pt, style=Bold" ChartArea="ChartArea1" Legend="Series1Legend"></asp:Series>
                                            <asp:Series Name="Series3" CustomProperties="DrawingStyle=Cylinder, MaxPixelPointWidth=50" ShadowOffset="2" xvaluetype="Auto" IsValueShownAsLabel="true" IsVisibleInLegend="true"
                                                Font="Trebuchet MS, 6.25pt, style=Bold" ChartArea="ChartArea1" Legend="Series1Legend"></asp:Series>
                                            <asp:Series Name="Series4" CustomProperties="DrawingStyle=Cylinder, MaxPixelPointWidth=50" ShadowOffset="2" xvaluetype="Auto" IsValueShownAsLabel="true" IsVisibleInLegend="true"
                                                Font="Trebuchet MS, 6.25pt, style=Bold" ChartArea="ChartArea1" Legend="Series1Legend"></asp:Series>
                                        </series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2" >
                                                    <AxisY IsLabelAutoFit="False" IsStartedFromZero="False" Minimum="0" LineWidth="0" LineColor="Transparent">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="#,###" />
                                                    </AxisY>
                                                    <AxisX LineColor="transparent" IsLabelAutoFit="False" IntervalAutoMode="VariableCount" LineWidth="0">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="MMM dd"   />
                                                    </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                <td width="100%"></td>
                            </tr>
                            <tr>
	                            <td class="ms-formspacer" colspan="3">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
	                <td class="ms-formspacer"><img alt="" src="../_layouts/images/trans.gif" width="10"></td>
                </tr>
                <tr>
	                <td class="ms-formspacer" colspan="5"></td>
                </tr>
            </table>
        </td>
    </TR>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/blank.gif"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>


