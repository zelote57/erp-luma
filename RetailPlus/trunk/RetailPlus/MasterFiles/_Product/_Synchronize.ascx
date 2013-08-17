<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product.__Synchronize" Codebehind="_Synchronize.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap style="height: 21px"><asp:imagebutton id="imgCancel" ToolTip="Cancel Synchronizing Products" accessKey="C" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" BorderWidth="0" width="16" height="16" CausesValidation="False"></asp:imagebutton></td>
								<td noWrap style="height: 21px"><asp:linkbutton id="cmdCancel" ToolTip="Cancel Synchronizing Products" accessKey="C" tabIndex="1" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label></TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
            <TABLE width="100%" border=0>
                <TR>
                    <TD style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" class="ms-descriptiontext" colSpan=3>
                        <FONT color=red>*</FONT> Indicates a required field </TD>
                </TR>
                <TR>
                    <TD class="ms-sectionline" colSpan=3 height=1>
                        <IMG alt="" src="../../_layouts/images/empty.gif" /></TD>
                </TR>
                <TR>
                    <TD vAlign=top colSpan=3>
                        <DIV style="PADDING-BOTTOM: 8px" class="ms-sectionheader">Option 1:&nbsp;Copy Products from this server to Branch.</DIV>
                        <DIV style="PADDING-BOTTOM: 10px" class="ms-descriptiontext">Make sure the branch is connected to your network then select the branch you want&nbsp; to synchronize then click 'Synchronize now'.</DIV>
                    </TD>
                </TR>
                <TR>
                    <TD style="PADDING-BOTTOM: 20px" vAlign=top></TD>
                    <TD style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" class="ms-authoringcontrols ms-formwidth" vAlign=top colSpan=2>
                        <TABLE cellSpacing=0 cellPadding=1 border=0 noWrap>
                            <TBODY><TR><TD class="ms-toolbar" >
                                        <asp:Label id="Label4" runat="server">Select branch to synchronize TO:</asp:Label> 
                                        <asp:DropDownList id="cboSynchronizeToBranch" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" tabindex="2"></asp:DropDownList> 
                                    </TD>
                                    <TD noWrap>
                                        <asp:ImageButton accessKey="T" id="imgSynchronize" tabIndex=3 onclick="imgSynchronize_Click" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Height="16" Width="16">
                                        </asp:ImageButton> <asp:LinkButton accessKey="T" id="cmdSynchronize" tabIndex=4 onclick="cmdSynchronize_Click" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Text="Synchronize the selected branch." enableviewstate="False"></asp:LinkButton> 
                                    </TD>
                                    
                                    
                                </TR>
                            </TBODY>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
            <TABLE width="100%" border=0>
                <TR>
                    <TD style="PADDING-BOTTOM: 10px; PADDING-TOP: 8px" class="ms-descriptiontext" colSpan=3>
                        <FONT color=red>*</FONT> Indicates a required field </TD>
                </TR>
                <TR>
                    <TD class="ms-sectionline" colSpan=3 height=1>
                        <IMG alt="" src="../../_layouts/images/empty.gif" /></TD>
                </TR>
                <TR>
                    <TD vAlign=top colSpan=3>
                        <DIV style="PADDING-BOTTOM: 8px" class="ms-sectionheader">Option 2:&nbsp;Copy Products FROM BRANCH to this server.</DIV>
                        <DIV style="PADDING-BOTTOM: 10px" class="ms-descriptiontext">Make sure the branch is connected to your network then select the branch you want&nbsp; to synchronize then click 'Synchronize now'.</DIV>
                    </TD>
                </TR>
                <TR>
                    <TD style="PADDING-BOTTOM: 20px" vAlign=top></TD>
                    <TD style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" class="ms-authoringcontrols ms-formwidth" vAlign=top colSpan=2>
                        <TABLE cellSpacing=0 cellPadding=1 border=0 noWrap>
                            <TBODY><TR><TD class="ms-toolbar" >
                                        <asp:Label id="Label3" runat="server">Select branch to synchronize FROM:</asp:Label> 
                                        <asp:DropDownList id="cboSynchronizeFromBranch" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboBranch_SelectedIndexChanged" tabindex="2"></asp:DropDownList> 
                                    </TD>
                                    <TD noWrap>
                                        <asp:ImageButton accessKey="T" id="imgSynchronizeFromBranch" tabIndex=5 onclick="imgSynchronizeFromBranch_Click" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Height="16" Width="16"></asp:ImageButton> 
                                        <asp:LinkButton accessKey="T" id="cmdSynchronizeFromBranch" tabIndex=6 onclick="cmdSynchronizeFromBranch_Click" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start products synchronization" Text="Synchronize FROM the selected branch." enableviewstate="False"></asp:LinkButton> 
                                    </TD>
                                </TR>
                            </TBODY>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
            <TABLE width="100%" border=0>
                <TR>
                    <TD class="ms-sectionline" colSpan=3 height=1>
                        <IMG alt="" src="../../_layouts/images/empty.gif" /></TD>
                </TR>
                <TR>
                    <TD vAlign=top colSpan=3>
                        <DIV style="PADDING-BOTTOM: 8px" class="ms-sectionheader">Option 3:&nbsp;Upload Products from file.</DIV>
                        <DIV style="PADDING-BOTTOM: 10px" class="ms-descriptiontext">Click the textbox address or click the 'Browse' button to select a file then click 'Upload now'.</DIV>
                    </TD>
                </TR>
                <TR>
                    <TD style="PADDING-BOTTOM: 20px" vAlign=top></TD>
                    <TD style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" class="ms-authoringcontrols ms-formwidth" vAlign=top colSpan=2 align=left>
                        <TABLE cellSpacing=0 cellPadding=1 border=0 noWrap>
                            <TBODY>
                                <TR>
                                    <TD class="ms-toolbar" >
                                        <asp:Label id="Label2" runat="server">File to upload :</asp:Label> 
                                        <asp:FileUpload ID="txtPath" runat="server" cssclass="ms-long" tabindex="7" visible="False"></asp:FileUpload><asp:TextBox ID="TextBox1" runat="server" CssClass="ms-long"></asp:TextBox>
                                    </TD>
                                    <TD class="ms-toolbar" >
                                        &nbsp;&nbsp;Upload to Branch <asp:DropDownList id="cboBranchUpload" CausesValidation="false" runat="server" tabindex="8"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    </TD>
                                    <TD noWrap>
                                        <asp:ImageButton accessKey="U" id="imgUpload" tabIndex=9 ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Start uploading products." Height="16" Width="16" onclick="imgUpload_Click"></asp:ImageButton> 
                                        <asp:LinkButton id="cmdUpload" tabIndex=10 runat="server" CssClass="ms-toolbar" tooltip="Start Branch Product Upload" Text="<- Upload now"></asp:LinkButton> 
                                    </TD>
                                </TR>
                            </TBODY>
                        </TABLE>
                    </TD>
                </TR>
                
            </TABLE>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
            <TABLE width="100%" border=0>
                <TR>
                    <TD class="ms-sectionline" colSpan=3 height=1>
                        <IMG alt="" src="../../_layouts/images/empty.gif" /></TD>
                </TR>
                <TR>
                    <TD vAlign=top colSpan=3>
                        <DIV style="PADDING-BOTTOM: 8px" class="ms-sectionheader">
                            Export Products to file.</DIV>
                        <DIV style="PADDING-BOTTOM: 10px" class="ms-descriptiontext">Select the branch to download from then click 'Download now'.</DIV>
                    </TD>
                </TR>
                <TR>
                    <TD style="PADDING-BOTTOM: 20px" vAlign=top></TD>
                    <TD style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 20px" class="ms-authoringcontrols ms-formwidth" vAlign=top colSpan=2>
                        <TABLE cellSpacing=0 cellPadding=1 border=0 noWrap>
                            <TBODY><TR>
                                    <TD class="ms-toolbar" >
                                        <asp:Label id="Label1" runat="server">Download from Branch: </asp:Label> 
                                    </TD>
                                    <TD class="ms-toolbar" >
                                        <asp:DropDownList id="cboBranchDownload" CausesValidation="false" runat="server" tabindex="11"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    </TD>
                                    <TD noWrap>
                                        <asp:ImageButton accessKey="T" id="imgDownload" tabIndex="12" CausesValidation="False" ImageUrl="../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" tooltip="Create file for transfer to other branch." Height="16" Width="16" onclick="imgDownload_Click"></asp:ImageButton> 
                                        <asp:LinkButton accessKey="T" id="cmdDownload" tabIndex="13" CausesValidation="false" runat="server" CssClass="ms-toolbar" tooltip="Start Branch Product to Download" Text="<- Download now" enabletheming="False"></asp:LinkButton> 
                                    </TD>
                                </TR>
                            </TBODY>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
    <tr>
        <td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
    </tr>
    <asp:Label id="lblError" runat="server" CssClass="ms-error"></asp:Label> 
</table>