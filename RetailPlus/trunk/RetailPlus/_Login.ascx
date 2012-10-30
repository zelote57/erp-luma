<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.__Login" Codebehind="_Login.ascx.cs" %>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr>
		<td colspan="3" class="ms-descriptiontext" style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px">Enter 
			your valid user name and password.<br>
			<br>
			<font color="red">*</font> Indicates a required field
		</td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="1">
			<A name="InputFormSection1"></A><img alt="" src="_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td valign="top" style="PADDING-BOTTOM: 20px">
			<div class="ms-sectionheader" style="PADDING-BOTTOM: 8px">User Authentication</div>
			<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
				<asp:Label id="lblMessage" runat="server"> Type your username and password in the textbox</asp:Label>.</div>
			<div class="ms-descriptiontext" style="PADDING-BOTTOM:	10px">
				<asp:Label id="lblError" runat="server" CssClass="ms-error"></asp:Label></div>
		</td>
		<td class="ms-colspace">&nbsp;</td>
		<td class="ms-authoringcontrols ms-formwidth" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 1px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	20px">
			<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellSpacing="0" cellPadding="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2" id="txtName_Label">
						<label for="txtName">User Name:&nbsp;<font color="red">*</font></label>
					</td>
				</tr>
				<tr>
					<td class="ms-formspacer"><img alt="" src="_layouts/images/trans.gif" width="10"></td>
					<td class="ms-authoringcontrols" width="100%">
						<asp:TextBox id="txtUserName" Width="100%" runat="server" accesskey="N" CssClass="ms-long" MaxLength="25"></asp:TextBox>
						<asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" CssClass="ms-error" ErrorMessage="'User Name' must not be left blank." Display="Dynamic" ControlToValidate="txtUserName"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<td class="ms-formspacer"></td>
				</tr>
				<tr>
					<td class="ms-authoringcontrols" style="PADDING-BOTTOM:2px" colspan="2" id="txtDescription_Label"><label for="txtPassword">Password:</label>
						<FONT color="red">*</FONT>
					</td>
				</tr>
				<tr>
					<td class="ms-formspacer"><img alt="" src="_layouts/images/trans.gif" width="10">
					</td>
					<td class="ms-authoringcontrols" width="100%">
						<asp:TextBox id="txtPassword" Width="100%" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="ms-error" ErrorMessage="'Password' must not be left blank." Display="Dynamic" ControlToValidate="txtPassword"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<td class="ms-formspacer"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="3" class="ms-sectionline" height="2">
			<img alt="" src="_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
		<td align="right"><br>
			<asp:Button id="cmdSignIn" runat="server" CssClass="ms-bbutton" Text="Sign In" onclick="cmdSignIn_Click"></asp:Button>
			&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
	</tr>
	<tr>
		<td colspan="3" height="30"><img alt="" src="_layouts/images/empty.gif"></td>
	</tr>
</table>
