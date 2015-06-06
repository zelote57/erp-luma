<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.___HorizontalNavBarCorner" Codebehind="_HorizontalNavBarCorner.ascx.cs" %>
<asp:HyperLink id="lnkMyAccount" runat="server" ToolTip="My Account" AccessKey="A">My Account</asp:HyperLink>&nbsp;&nbsp;&nbsp;
<asp:linkbutton id="cmdLogout" title="Log Out" runat="server" AccessKey="L" CausesValidation="False" onclick="cmdLogout_Click">Log Out</asp:linkbutton>&nbsp;&nbsp;&nbsp;
<asp:HyperLink id="lnkHelp" runat="server" ToolTip="Help" AccessKey="H" NavigateUrl="/RetailPlus/User Guide - Backend.pdf">Help</asp:HyperLink>&nbsp;&nbsp;&nbsp;
