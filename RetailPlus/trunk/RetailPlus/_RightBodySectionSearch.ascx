<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.__RightBodySectionSearch" Codebehind="_RightBodySectionSearch.ascx.cs" %>
<script language="JavaScript" src="/RetailPlus/_Scripts/DocumentScripts.js"></script>
<div width="100%" id="SBX" align="right">
	<table width="100%" cellspacing="0" cellpadding="0" border="0">
		<tr class="ms-sbrow">
			<td class="ms-sbtable ms-searchform ms-sbcellwhite100"></td>
			<td class="ms-sbtopcorner"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" border="0" style="WIDTH:12px" /></td>
			<td class="ms-sbtable ms-searchform" nowrap="nowrap">
				<asp:ImageButton id="cmdSearch1" title="Execute search" style="CURSOR: hand" accessKey="s" runat="server" alt="Execute search" border="0" ImageUrl="/RetailPlus/_layouts/images/SPSSearch2.gif"></asp:ImageButton></td>
			<td class="ms-sbtable ms-searchform" style="PADDING-LEFT:	4px">
				<asp:DropDownList id="cboSearchID" runat="server" Enabled="False" CssClass="ms-sbdropdown ms-descriptiontext" Title="Select a scope" CausesValidation="false"></asp:DropDownList>
			</td>
			<td class="ms-sbtable ms-searchform ms-descriptiontext" style="PADDING-LEFT:	4px">
				<asp:TextBox id="txtSearchKeyword" MaxLength="300" runat="server" CssClass="ms-sbkeyword ms-descriptiontext" Width="170px" AccessKey="s" title="Enter keyword(s)" onKeyPress="ValidSearch()"></asp:TextBox></td>
			<td nowrap="nowrap" class="ms-sbtable ms-searchform ms-sbgo">&nbsp;
				<asp:ImageButton id="cmdSearch" title="Execute search" style="CURSOR: hand" accessKey="s" runat="server" ImageUrl="/RetailPlus/_layouts/images/icongo01.gif" border="0" alt="Execute search" causesvalidation="false"></asp:ImageButton>
			</td>
		</tr>
		<tr>
			<td class="ms-sbtable ms-searchform ms-sbcellwhite"></td>
			<td class="ms-sblbcorner"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" border="0"></td>
			<td class="ms-sbtable ms-searchform" colspan="4"><img alt="" src="/RetailPlus/_layouts/images/trans.gif" border="0"></td>
		</tr>
	</table>
</div>
