<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security._AccessUser.__AccessRights" Codebehind="_AccessRights.ascx.cs" %>
<script language="JavaScript" src="../../../_Scripts/SelectAll.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgSaveBack" title="Update Access Rights" accessKey="S" tabIndex="1" height="16" width="16" border="0" alt="Update Access Rights" ImageUrl="../../../_layouts/images/saveitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgSaveBack_Click"></asp:imagebutton>&nbsp;
								</td>
								<td noWrap><asp:linkbutton id="cmdSaveBack" title="Update Access Rights" accessKey="S" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSaveBack_Click">Save and Back</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap>Reload access to group </td>
								<td noWrap>
								    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                        <ContentTemplate >
								            <asp:dropdownlist id="cboGroup" runat="server" CssClass="ms-long" Width="157px" OnSelectedIndexChanged="cboGroup_SelectedIndexChanged" AutoPostBack="True"></asp:dropdownlist>
								        </ContentTemplate>
                                    <Triggers> 
                                        <asp:AsyncPostBackTrigger ControlID="imgReload" EventName="Click" />
                                    </Triggers> 
                                </asp:UpdatePanel>
							    </td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
							    <td noWrap><asp:imagebutton accessKey="C" id="imgReload" title="Reload Now" tabIndex=3 height="16" width="16" border="0" alt="Reload Now" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgReload_Click"></asp:imagebutton></td>
								<td class="ms-toolbar" noWrap>Reload to previous settings </td>
							</tr>
						</table>
					</td>
					<TD class="ms-separator">|</TD>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><asp:imagebutton id="imgCancel" title="Cancel Updating Access Rights" accessKey="C" tabIndex="3" height="16" width="16" border="0" alt="Cancel Updating Access Rights" ImageUrl="../../../_layouts/images/impitem.gif" runat="server" CssClass="ms-toolbar" CausesValidation="False" OnClick="imgCancel_Click"></asp:imagebutton></td>
								<td noWrap><asp:linkbutton id="cmdCancel" title="Cancel Updating Access Rights" accessKey="C" tabIndex="4" runat="server" CssClass="ms-toolbar" CausesValidation="False" onclick="cmdCancel_Click">Cancel</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" noWrap align="right" width="99%"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:Label id="lblUID" runat="server" Visible="False"></asp:Label></TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td class="ms-vb2">
		</td>
		<td class="ms-vb2" style="BORDER-TOP-WIDTH: 0px">
		    
		</td>
		<td class="ms-vb2" style="BORDER-TOP: 0px" colSpan="2"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<th class="ms-vh2">
			<IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
		<th class="ms-vh2">
			<IMG height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
		<th class="ms-vh2">
			<IMG height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
		<th class="ms-vh2">
			<IMG height="5" alt="" src="../../../_layouts/images/blank.gif" width="1"></th>
	</tr>
	<tr>
		<td class="ms-vb2">
		</td>
		<td class="ms-vb2" style="BORDER-TOP-WIDTH: 0px"><INPUT id="idSelectAllRead" onclick="SelectAllRead();" type="checkbox" name="selectallread"><label for="idSelectAll"><B>Check 
					All 'Read'</B></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="idSelectAllWrite" onclick="SelectAllWrite();" type="checkbox" name="selectallwrite"><label for="idSelectAll"><B>Check 
					All 'Write'</B></label></td>
		<td class="ms-vb2" style="BORDER-TOP: 0px" colSpan="2"><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
		    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate >
		            <asp:datalist id="lstAccessCategory" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstAccessCategory_ItemDataBound">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
						            <col width="10">
							        <col width="5%">
							        <col width="94%">
							        <col width="1%">
						        </colgroup>
						        <TR>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							        <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
						        </TR>
					        </table>
				        </HeaderTemplate>
				        <ItemTemplate>
					        <TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						        <colgroup>
						            <col width="10">
							        <col width="5%">
							        <col width="94%">
							        <col width="1%">
						        </colgroup>
						        <TR>
						            <TD class="ms-vb-user">
							        </TD>
							        <TD class="ms-vb-user" colspan=2>
							            <asp:Label ID="lblCategory" Runat="server" Font-Bold="true"></asp:Label>
							        </TD>
							        <TD class="ms-vb2">
							        </TD>
						        </TR>
						        <TR>
							        <TD class="ms-vb-user">
							        </TD>
							        <TD class="ms-vb-user">
							        </TD>
							        <TD class="ms-vb-user">
							            <asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound">
				                            <HeaderTemplate>
					                            <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						                            <colgroup>
							                            <col width="1">
							                            <col width="27%">
							                            <col width="10px" align="center">
							                            <col width="2%">
							                            <col width="10px" align="center">
							                            <col width="2%">
							                            <col width="65%">
							                            <col width="1%">
						                            </colgroup>
						                            <TR>
							                            <TH class="ms-vh2" style="padding-bottom: 4px"></TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                            <asp:hyperlink id="SortByTypeName" runat="server">Type Name</asp:hyperlink></TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px" align="center">
								                            <asp:hyperlink id="Hyperlink1" runat="server">Read</asp:hyperlink></TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px">
							                            </TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                            <asp:hyperlink id="Hyperlink2" runat="server">Write</asp:hyperlink></TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px">
							                            </TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px">
								                            <asp:hyperlink id="SortByRemarks" runat="server">Remarks</asp:hyperlink></TH>
							                            <TH class="ms-vh2" style="padding-bottom: 4px">
							                            </TH>
						                            </TR>
					                            </table>
				                            </HeaderTemplate>
				                            <ItemTemplate>
					                            <TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						                            <colgroup>
							                             <col width="1">
							                            <col width="27%">
							                            <col width="10px" align="center">
							                            <col width="2%">
							                            <col width="10px" align="center">
							                            <col width="2%">
							                            <col width="65%">
							                            <col width="1%">
						                            </colgroup>
						                            <TR>
							                            <TD class="ms-vb-user">
								                            <input type="checkbox" id="chkList" runat="server" name="chkList" visible="false" />
							                            </TD>
							                            <TD class="ms-vb-user">
								                            <asp:Label ID="lblTypeName" Runat="server"></asp:Label>
							                            </TD>
							                            <TD class="ms-vb-user">
								                            <input type="checkbox" id="chkRead" runat="server" NAME="chkRead">
							                            </TD>
							                            <TD class="ms-vh2" style="padding-bottom: 4px">&nbsp;
							                            </TD>
							                            <TD class="ms-vb-user">
								                            <input type="checkbox" id="chkWrite" runat="server" NAME="chkWrite">
							                            </TD>
							                            <TD class="ms-vh2" style="padding-bottom: 4px">&nbsp;
							                            </TD>
							                            <TD class="ms-vb-user">
								                            <asp:Label ID="lblRemarks" Runat="server"></asp:Label>
							                            </TD>
							                            <TD class="ms-vb2">
								                            <A class="DropDown" id="anchorDown" href="" runat="server">
									                            <asp:Image id="divExpCollAsst_img" ImageUrl="../../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							                            </TD>
						                            </TR>
					                            </table>
				                            </ItemTemplate>
			                            </asp:datalist>
							        </TD>
							        <TD class="ms-vb2">
							        </TD>
						        </TR>
					        </table>
				        </ItemTemplate>
			        </asp:datalist>
			    </ContentTemplate>
                <Triggers> 
                    <asp:AsyncPostBackTrigger ControlID="cboGroup" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgReload" EventName="Click" />
                </Triggers> 
            </asp:UpdatePanel>
			<br />
			</TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</tr>
</table>
