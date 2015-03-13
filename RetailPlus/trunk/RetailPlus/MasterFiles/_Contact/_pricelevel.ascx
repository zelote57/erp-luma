<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Contact.__PriceLevel" Codebehind="_pricelevel.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgAdd" ToolTip="Add New Contact" accessKey="N" tabIndex="1" height="16" width="16" border="0" alt="Add New Contact" ImageUrl="../../_layouts/images/newuser.gif" runat="server" CssClass="ms-toolbar" OnClick="imgAdd_Click"></asp:imagebutton>&nbsp;
								</td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdAdd" ToolTip="Add New Contact" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdAdd_Click">Add Contact</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator1" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgDelete" ToolTip="Remove Selected Contact" accessKey="X" tabIndex="3" height="16" width="16" border="0" alt="Remove Selected Contact" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" OnClick="imgDelete_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdDelete" ToolTip="Remove Selected Contact" accessKey="X" tabIndex="4" runat="server" CssClass="ms-toolbar" onclick="cmdDelete_Click">Remove Selected Contact</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-separator">
						<asp:Label id="lblSeparator2" runat="server">|</asp:Label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="idEdit" ToolTip="Edit Selected Contact" accessKey="E" tabIndex="5" height="16" width="16" border="0" alt="Update Selected Contact" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" OnClick="idEdit_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdEdit" ToolTip="Edit Selected Contact" accessKey="E" tabIndex="6" runat="server" CssClass="ms-toolbar" onclick="cmdEdit_Click">Edit Selected Contact</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align01" nowrap="nowrap" align="right" width="99%">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="right"><asp:label id="lblDataCount1" runat="server" CssClass="Normal"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" runat="server" CssClass="class=ms-vb-user"> of 0</asp:label></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
    <tr>
		<td></td>
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Customer Group</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:DropDownList id="cboGroup" CssClass="ms-short" Width="100%" runat="server"></asp:DropDownList>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Modeofterms</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:dropdownlist id="cboModeOfTerms" CssClass="ms-short" runat="server">
										<asp:ListItem Value="0" Selected="True">Daily</asp:ListItem>
										<asp:ListItem Value="1">Monthly</asp:ListItem>
										<asp:ListItem Value="2">Yearly</asp:ListItem>
									</asp:dropdownlist>
					</td>
					<td width="99%" id="Td2" nowrap="nowrap" align="left">&nbsp;
					</td>
				</tr>
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Customer Code</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtContactCode" accessKey="S" CssClass="ms-short" runat="server" MaxLength="25" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Customer Name</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtContactName" accessKey="E" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="75"></asp:TextBox>
					</td>
					<td width="99%" nowrap="nowrap" align="left">&nbsp;
					    <asp:ImageButton accessKey="s" style="CURSOR: hand" id="cmdSearch" 
                            ImageUrl="../../_layouts/images/icongo01.gif" border="0" 
                            ToolTip="Execute search" runat="server" causesvalidation="false" 
                            onclick="cmdSearch_Click"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
	    <td></td>
		<td class="ms-sectionline" height="2" ><img alt="" src="../../_layouts/images/empty.gif" /></td>
		<td></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></th>
					<th class="ms-vh2">
						<img alt="" height="5" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img alt="" height="5" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<img alt="" height="5" src="../../_layouts/images/blank.gif" width="1"></th>
				</tr>
				<tr>
					<td class="ms-vb2" style="PADDING-RIGHT: 15px; BORDER-TOP: 0px; padding-bottom: 0px; PADDING-TOP: 0px"><input id="idSelectAll" onclick="SelectAll();" type="checkbox" name="selectall" />
					</td>
					<td class="ms-vb2" style="BORDER-TOP: 0px"><label for="idSelectAll"><B>Select All</B></label></td>
					<td class="ms-vb2" style="BORDER-TOP: 0px" colspan="2"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="4" height="5"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
				</tr>
			</table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
			        <asp:datalist id="lstItem" runat="server" CellPadding="0" ShowFooter="False" Width="100%" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				        <HeaderTemplate>
					        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						        <colgroup>
							        <col width="10">
							        <col width="10">
							        <col width="10">
							        <col width="30%">
							        <col width="35%">
							        <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
							        <col width="1%">
						        </colgroup>
						        <tr>
							        <th class="ms-vh2" style="padding-bottom: 4px">
								        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
						            <th class="ms-vh2" style="padding-bottom: 4px"></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"></th>
							        <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByContactCode" runat="server">Contact Code</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByContactName" runat="server">Contact Name</asp:hyperlink></th>
							        <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByPrice" runat="server">SRP</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoPriceAll" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
                                    <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByWSPrice" runat="server">WSPrice</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoWSPriceAll" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
                                    <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByPriceLevel1" runat="server">P. Level1</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoLevel1All" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
                                    <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByPriceLevel2" runat="server">P. Level2</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoLevel2All" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
                                    <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByPriceLevel3" runat="server">P. Level3</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoLevel3All" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
                                    <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByPriceLevel4" runat="server">P. Level4</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoLevel4All" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
                                    <th class="ms-vh2" style="padding-bottom: 4px">
								        <asp:hyperlink id="SortByPriceLevel5" runat="server">P. Level5</asp:hyperlink>
                                        <br /><asp:RadioButton ID="rdoLevel5All" runat="server" GroupName="grpPriceLevel" OnCheckedChanged="rdoPriceLevelAll_CheckedChanged" AutoPostBack="true" CausesValidation="false" alt="Check/Uncheck all" /></th>
							        <th class="ms-vh2" style="padding-bottom: 4px"></th>
						        </tr>
					        </table>
				        </HeaderTemplate>
				        <ItemTemplate>
					        <table id="tblItemTemplate" cellspacing="0" cellpadding="0" width="100%" border="0" onmouseover="this.bgColor='#FFE303'" onmouseout="this.bgColor='transparent'">
						        <colgroup>
							        <col width="10">
							        <col width="10">
							        <col width="10">
							        <col width="30%">
							        <col width="35%">
							        <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
                                    <col width="5%">
							        <col width="1%">
						        </colgroup>
						        <tr>
							        <td class="ms-vb-user">
								        <input type="checkbox" id="chkList" runat="server" name="chkList" />
							        </td>
							        <td class="ms-vb2">
							            <asp:imagebutton id="imgItemDelete" CommandName="imgItemDelete" accessKey="D" tabIndex="1" height="16" width="16" border="0" tooltip="Delete this Contact" ImageUrl="../../_layouts/images/delitem.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </td>
							        <td class="ms-vb2">
							            <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this Contact" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						            </td>
							        <td class="ms-vb-user">
								        <asp:HyperLink ID="lnkContactCode" Runat="server"></asp:HyperLink>
							        </td>
							        <td class="ms-vb-user">
								        <asp:HyperLink ID="lnkContactName" Runat="server"></asp:HyperLink>
							        </td>
							        <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoPrice" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
                                    <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoWSPrice" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
                                    <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoLevel1" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
                                    <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoLevel2" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
                                    <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoLevel3" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
                                    <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoLevel4" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
                                    <td class="ms-vb-user">
                                        <asp:RadioButton ID="rdoLevel5" GroupName="grpPriceLevel" runat="server" OnCheckedChanged="grpPriceLevel_OnCheckedChanged" AutoPostBack="true" CausesValidation="false" />
							        </td>
							        <td class="ms-vb2">
								        <A class="DropDown" id="anchorDown" href="" runat="server">
									        <asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show" Visible="false"></asp:Image></A>
							        </td>
						        </tr>
					        </table>
				        </ItemTemplate>
			        </asp:datalist>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
            </td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
