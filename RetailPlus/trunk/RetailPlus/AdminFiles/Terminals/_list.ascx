<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Security._Terminals.__List" Codebehind="_List.ascx.cs" %>
<script language="JavaScript" src="../../_Scripts/sExpCollapse.js"></script>
<script language="JavaScript" src="../../_Scripts/SelectAll.js"></script>
<script language="JavaScript" src="../../_Scripts/ConfirmDelete.js"></script>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td class="ms-toolbar" noWrap><%--<asp:imagebutton id="imgEdit" title="Update Selected Terminal" accessKey="E" tabIndex="5" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/edit.gif" alt="Update Selected Terminal" border="0" width="16" height="16" OnClick="imgEdit_Click"></asp:imagebutton>--%></td>
								<td noWrap><%--<asp:linkbutton id="cmdEdit" title="Update Selected Terminal" accessKey="E" tabIndex="6" CssClass="ms-toolbar" runat="server" onclick="cmdEdit_Click">Edit Selected Terminal</asp:linkbutton>--%></td>
							</tr>
						</table>
					</td>
					<TD class="ms-toolbar" id="align01" noWrap align="right" width="99%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" noWrap align="right"><asp:label id="lblDataCount1" CssClass="Normal" runat="server"> Go to page </asp:label><asp:dropdownlist id="cboCurrentPage" runat="server" AutoPostBack="True" onselectedindexchanged="cboCurrentPage_SelectedIndexChanged">
										<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblDataCount" CssClass="class=ms-vb-user" runat="server"> of 0</asp:label></td>
							</TR>
						</TABLE>
					</TD>
					<td class="ms-toolbar" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
					</td>
				</TR>
			</TABLE>
		</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<colgroup>
					<col width="1">
					<col width="25%">
					<col width="25%">
					<col width="50%">
				</colgroup>
				<tr>
					<th class="ms-vh2">
						<IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th>
					<th class="ms-vh2">
						<IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></th></tr>
				<tr>
					<td colSpan="4" height="5"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
				</tr>
			</table>
			<asp:datalist id="lstItem" runat="server" Width="100%" ShowFooter="False" CellPadding="0" OnItemDataBound="lstItem_ItemDataBound" OnItemCommand="lstItem_ItemCommand">
				<HeaderTemplate>
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblHeaderTemplate">
						<colgroup>
							<col width="10">
							<col width="10">
							<col width="15%">
							<col width="15%">
							<col width="15%">
							<col width="20%">
							<col width="20%">
							<col width="14%">
							<col width="1%">
						</colgroup>
						<TR>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								&nbsp;&nbsp;&nbsp;</TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByTerminalNo" runat="server">Terminal No</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByTerminalCode" runat="server">Terminal Code</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByTerminalName" runat="server">Terminal Name</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByMachineSerialNo" runat="server">Machine Serial No.</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByAccreditationNo" runat="server">Accreditation No.</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
								<asp:hyperlink id="SortByStatus" runat="server">Status</asp:hyperlink></TH>
							<TH class="ms-vh2" style="padding-bottom: 4px">
							</TH>
						</TR>
					</table>
				</HeaderTemplate>
				<ItemTemplate>
					<TABLE id="tblItemTemplate" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<COLGROUP>
							<COL align="center" width="10">
							<COL align="center" width="10">
							<col width="15%">
							<col width="15%">
							<col width="15%">
							<col width="20%">
							<col width="20%">
							<col width="14%">
							<COL align="center" width="1%">
						</COLGROUP>
						<TR>
							<TD class="ms-vb-user">
								<INPUT id="chkList" type="checkbox" name="chkList" runat="server" visible=false>
								&nbsp;
							</TD>
							<TD class="ms-vb2">
							    <asp:imagebutton id="imgItemEdit" CommandName="imgItemEdit" accessKey="U" tabIndex="1" height="16" width="16" border="0" tooltip="Update this terminal" ImageUrl="../../_layouts/images/edit.gif" runat="server" CssClass="ms-toolbar" ></asp:imagebutton>
						    </TD>
							<TD class="ms-vb-user">
								<asp:HyperLink id="lnkTerminalNo" Runat="server"></asp:HyperLink>
							<TD class="ms-vb-user">
								<asp:HyperLink id="lnkTerminalCode" Runat="server"></asp:HyperLink>
							<TD class="ms-vb-user">
								<asp:HyperLink id="lnkTerminalName" Runat="server"></asp:HyperLink></TD>
							<TD class="ms-vb2">
								<asp:HyperLink id="lnkMachineSerialNo" Runat="server"></asp:HyperLink></TD>
							<TD class="ms-vb2">
								<asp:HyperLink id="lnkAccreditationNo" Runat="server"></asp:HyperLink></TD>
							<TD class="ms-vb2">
								<asp:HyperLink id="lnkStatus" Runat="server"></asp:HyperLink></TD>
							<TD class="ms-vb2"><A class="DropDown" id="anchorDown" href="" runat="server">
									<asp:Image id="divExpCollAsst_img" ImageUrl="../../_layouts/images/DLMAX.gif" runat="server" alt="Show"></asp:Image></A>
							</TD>
						</TR>
						<TR>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
							<TD colSpan="8" height="1">
								<DIV class="ACECollapsed" id="divExpCollAsst" runat="server" border="0">
									<asp:panel id="panCard" runat="server" Width="100%" Height="100%" CssClass="ms-authoringcontrols">
										<TABLE id="tblPanCard" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="ms-formspacer" colSpan="1"><IMG alt="" src="../../_layouts/images/trans.gif" width="10"></TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label1" CssClass="ms-vh2" runat="server" text="<b>Date Created</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label7" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblDateCreated" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
													<asp:Label id="Label4" CssClass="ms-vh2" runat="server" text="<b>Max Receipt Width</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label9" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:Label id="lblMaxReceiptWidth" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label3" CssClass="ms-vh2" runat="server" text="<b>Printer Auto Cutter</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label11" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:CheckBox id="chkIsPrinterAutoCutter" CssClass="ms-vb2" runat="server" Enabled="False"></asp:CheckBox>
												</TD>
												<TD width="19%">
													<asp:Label id="Label8" CssClass="ms-vh2" runat="server" text="<b>Auto Print</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label12" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:CheckBox id="chkAutoPrint" CssClass="ms-vb2" runat="server" Enabled="False"></asp:CheckBox>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label2" CssClass="ms-vh2" runat="server" text="<b>Printer Name</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label13" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblPrinterName" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
													<asp:Label id="Label6" CssClass="ms-vh2" runat="server" text="<b>Cash Drawer Name</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label14" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:Label id="lblCashDrawerName" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label5" CssClass="ms-vh2" runat="server" text="<b>Confirm Item Void</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label15" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:CheckBox id="chkItemVoidConfirmation" CssClass="ms-vb2" runat="server" Enabled="False"></asp:CheckBox>
												</TD>
												<TD width="19%">
													<asp:Label id="Label10" CssClass="ms-vh2" runat="server" text="<b>Enable EVAT</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label16" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="40%">
													<asp:CheckBox id="chkEnableEVAT" CssClass="ms-vb2" runat="server" Enabled="False"></asp:CheckBox>
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label19" CssClass="ms-vh2" runat="server" text="<b>Form Behavior</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label20" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD width="20%">
													<asp:Label id="lblFormBehavior" CssClass="ms-vh2" runat="server"></asp:Label>
												</TD>
												<TD width="19%">
												</TD>
												<TD width="1%">
												</TD>
												<TD width="40%">
												</TD>
											</TR>
											<TR>
												<TD width="19%">
													<asp:Label id="Label17" CssClass="ms-vh2" runat="server" text="<b>Marquee Message</b>"></asp:Label>
												</TD>
												<TD width="1%">
													<asp:Label id="Label18" CssClass="ms-vh2" runat="server" text="<b>:</b>"></asp:Label>
												</TD>
												<TD colspan="5">
													<asp:Label id="lblMarqueeMessage" CssClass="ms-vb2" runat="server"></asp:Label>
												</TD>
											</TR>
										</TABLE>
									</asp:panel></DIV>
							</TD>
							<TD class="ms-vh2" height="1"><IMG height="5" alt="" src="../../_layouts/images/blank.gif" width="1"></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist></TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
