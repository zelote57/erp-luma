<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__Synchronize" Codebehind="_Synchronize.ascx.cs" %>
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
								<td class="ms-toolbar" nowrap="nowrap" style="height: 21px"><asp:imagebutton id="imgCancel" ToolTip="Cancel Synchronizing Products" accessKey="C" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" BorderWidth="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td nowrap="nowrap" style="height: 21px"><asp:linkbutton id="cmdCancel" ToolTip="Cancel Synchronizing Products" accessKey="C" tabIndex="1" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right" width="99%"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
            <table width="100%" border="0">
                <tr>
                    <td style="padding-bottom: 10px; PADDING-TOP: 8px" class="ms-descriptiontext" colspan="3">
                        <FONT color=red>*</FONT> Indicates a required field </td>
                </tr>
                <tr>
                    <td class="ms-sectionline" colspan="3" height="1">
                        <img alt="" src="../../_layouts/images/empty.gif" /></td>
                </tr>
                <tr>
                    <td valign=top colspan="3">
                        <DIV style="padding-bottom: 8px" class="ms-sectionheader">Option 1:&nbsp;Copy Products from this server to Branch.</DIV>
                        <DIV style="padding-bottom: 10px" class="ms-descriptiontext">Make sure the branch is connected to your network then select the branch you want&nbsp; to synchronize then click 'Synchronize now'.</DIV>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 20px" valign=top></td>
                    <td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; padding-bottom: 20px" class="ms-authoringcontrols ms-formwidth" valign=top colspan=2>
                        <table cellspacing="0" cellpadding="1" border="0" nowrap="nowrap">
                            <tbody><tr><td class="ms-toolbar" >
                                        <asp:Label id="Label4" runat="server">Select branch to synchronize TO:</asp:Label> 
                                        <asp:DropDownList id="cboSynchronizeToBranch" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" tabindex="2"></asp:DropDownList> 
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:ImageButton accessKey="T" id="imgSynchronize" tabIndex="3" onclick="imgSynchronize_Click" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Height="16" Width="16">
                                        </asp:ImageButton> <asp:LinkButton accessKey="T" id="cmdSynchronize" tabIndex="4" onclick="cmdSynchronize_Click" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Text="Synchronize the selected branch." enableviewstate="False"></asp:LinkButton> 
                                    </td>
                                    
                                    
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
            <table width="100%" border="0">
                <tr>
                    <td style="padding-bottom: 10px; PADDING-TOP: 8px" class="ms-descriptiontext" colspan="3">
                        <FONT color=red>*</FONT> Indicates a required field </td>
                </tr>
                <tr>
                    <td class="ms-sectionline" colspan="3" height="1">
                        <img alt="" src="../../_layouts/images/empty.gif" /></td>
                </tr>
                <tr>
                    <td valign=top colspan="3">
                        <DIV style="padding-bottom: 8px" class="ms-sectionheader">Option 2:&nbsp;Copy Products FROM BRANCH to this server.</DIV>
                        <DIV style="padding-bottom: 10px" class="ms-descriptiontext">Make sure the branch is connected to your network then select the branch you want&nbsp; to synchronize then click 'Synchronize now'.</DIV>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 20px" valign=top></td>
                    <td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; padding-bottom: 20px" class="ms-authoringcontrols ms-formwidth" valign=top colspan=2>
                        <table cellspacing="0" cellpadding="1" border="0" nowrap="nowrap">
                            <tbody><tr><td class="ms-toolbar" >
                                        <asp:Label id="Label3" runat="server">Select branch to synchronize FROM:</asp:Label> 
                                        <asp:DropDownList id="cboSynchronizeFromBranch" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" tabindex="2"></asp:DropDownList> 
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:ImageButton accessKey="T" id="imgSynchronizeFromBranch" tabIndex=5 onclick="imgSynchronizeFromBranch_Click" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Height="16" Width="16"></asp:ImageButton> 
                                        <asp:LinkButton accessKey="T" id="cmdSynchronizeFromBranch" tabIndex=6 onclick="cmdSynchronizeFromBranch_Click" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Text="Synchronize FROM the selected branch." enableviewstate="False"></asp:LinkButton> 
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
            <table width="100%" border="0">
                <tr>
                    <td class="ms-sectionline" colspan="3" height="1">
                        <img alt="" src="../../_layouts/images/empty.gif" /></td>
                </tr>
                <tr>
                    <td valign=top colspan="3">
                        <DIV style="padding-bottom: 8px" class="ms-sectionheader">Option 3:&nbsp;Upload Products from file.</DIV>
                        <DIV style="padding-bottom: 10px" class="ms-descriptiontext">Click the textbox address or click the 'Browse' button to select a file then click 'Upload now'.</DIV>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 20px" valign=top></td>
                    <td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; padding-bottom: 20px" class="ms-authoringcontrols ms-formwidth" valign=top colspan=2 align=left>
                        <table cellspacing="0" cellpadding="1" border="0" nowrap="nowrap">
                            <tbody>
                                <tr>
                                    <td class="ms-toolbar" >
                                        <asp:Label id="Label2" runat="server">File to upload :</asp:Label> 
                                        <asp:FileUpload ID="txtPath" runat="server" cssclass="ms-long" tabindex="7" visible="False"></asp:FileUpload><asp:TextBox ID="TextBox1" runat="server" CssClass="ms-long"></asp:TextBox>
                                    </td>
                                    <td class="ms-toolbar" >
                                        &nbsp;&nbsp;Upload to Branch <asp:DropDownList id="cboBranchUpload" CausesValidation="false" runat="server" tabindex="8"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:ImageButton accessKey="U" id="imgUpload" tabIndex=9 ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Start uploading products." Height="16" Width="16" onclick="imgUpload_Click"></asp:ImageButton> 
                                        <asp:LinkButton id="cmdUpload" tabIndex=10 runat="server" CssClass="ms-toolbar" tooltip="Start Branch Product Upload" Text="<- Upload now"></asp:LinkButton> 
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                
            </table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
            <table width="100%" border="0">
                <tr>
                    <td class="ms-sectionline" colspan="3" height="1">
                        <img alt="" src="../../_layouts/images/empty.gif" /></td>
                </tr>
                <tr>
                    <td valign=top colspan="3">
                        <DIV style="padding-bottom: 8px" class="ms-sectionheader">
                            Export Products to file.</DIV>
                        <DIV style="padding-bottom: 10px" class="ms-descriptiontext">Select the branch to download from then click 'Download now'.</DIV>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 20px" valign=top></td>
                    <td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; padding-bottom: 20px" class="ms-authoringcontrols ms-formwidth" valign=top colspan=2>
                        <table cellspacing="0" cellpadding="1" border="0" nowrap="nowrap">
                            <tbody><tr>
                                    <td class="ms-toolbar" >
                                        <asp:Label id="Label1" runat="server">Download from Branch: </asp:Label> 
                                    </td>
                                    <td class="ms-toolbar" >
                                        <asp:DropDownList id="cboBranchDownload" CausesValidation="false" runat="server" tabindex="11"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:ImageButton accessKey="T" id="imgDownload" tabIndex="12" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Create file for transfer to other branch." Height="16" Width="16" onclick="imgDownload_Click"></asp:ImageButton> 
                                        <asp:LinkButton accessKey="T" id="cmdDownload" tabIndex="13" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start Branch Product to Download" Text="<- Download now" enabletheming="False"></asp:LinkButton> 
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
    <tr>
        <td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
    </tr>
    <asp:Label id="lblError" runat="server" CssClass="ms-error"></asp:Label> 
</table>