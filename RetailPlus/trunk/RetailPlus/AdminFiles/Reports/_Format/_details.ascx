<%@ Control Language="c#" Inherits="AceSoft.RetailPlus._Reports_Format.__Details" Codebehind="_Details.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD><asp:label id="lblReferrer" Visible="False" runat="server"></asp:label></TD>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td style="PADDING-RIGHT: 10px; PADDING-BOTTOM: 20px" vAlign="top" align="center" colSpan="3">
						<TABLE class="ms-toolbar" cellSpacing="0" cellPadding="2" border="0">
							<TR>
								<td class="ms-toolbar" align="center">
									<table cellSpacing="0" cellPadding="1" border="0">
										<tr>
											<td noWrap><asp:label id="Label1" runat="server" CssClass="ms-toolbar">Preview of the receipt format</asp:label></td>
										</tr>
									</table>
								</td>
							</TR>
						</TABLE>
					</td>
				</TR>
				<tr>
					<td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px" vAlign="top" colSpan="3">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="60%" align="center" border="2" frame="box">
							<TR>
								<TD>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<TR>
											<TD class="ms-formspacer"></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblCompanyName" runat="server" CssClass="ms-pagetitle" Width="100%">RetailPlus Business Solutions</asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader1" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader2" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader3" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader4" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader5" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader6" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader7" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader8" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader9" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportHeader10" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader1" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader2" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader3" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader4" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader5" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader6" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader7" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader8" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader9" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageHeader10" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD class="ms-formspacer"></TD>
										</TR>
										<TR>
											<TD class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" align="center">
												<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="90%" align="center">
													<tr>
														<TD align="left" width="30%">DESC</TD>
														<td align="center" width="10%">QTY
														</td>
														<TD align="right" width="10%">PRICE</TD>
														<td align="right" width="10%">AMOUNT</td>
													</tr>
													<TR>
														<TD align="center" colSpan="4"><asp:label id="Label5" runat="server" CssClass="ms-pagecaption">--------------------------------------------------------------------------------</asp:label></TD>
													</TR>
													<TR>
														<TD colSpan="3">A&amp;W ROOTBEER 355ML</TD>
														<TD></TD>
													</TR>
													<tr>
														<TD></TD>
														<td align="right">1
														</td>
														<TD align="right" width="10%">18.20</TD>
														<td align="right" width="10%">18.20</td>
													</tr>
													<TR>
														<TD colSpan="3">A&amp;W ROOTBEER 355ML</TD>
														<TD align="right" width="10%"></TD>
													</TR>
													<tr>
														<TD></TD>
														<td align="right">1
														</td>
														<TD align="right" width="10%">18.20</TD>
														<td align="right" width="10%">18.20</td>
													</tr>
													<TR>
														<TD align="center" colSpan="4"><asp:label id="Label6" runat="server" CssClass="ms-pagecaption">--------------------------------------------------------------------------------</asp:label></TD>
													</TR>
												</table>
											</TD>
										</TR>
										<TR>
											<TD height="30"><IMG alt="" src="/RetailPlus/_layouts/images/blank.gif"></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA1" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA2" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA3" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA4" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA5" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA6" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA7" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA8" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA9" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA10" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA11" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA12" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA13" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA14" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA15" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA16" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA17" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA18" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA19" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterA20" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<tr>
											<td class="ms-authoringcontrols" align="center" colSpan="3">
												<table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="90%" align="center">
													<tr>
														<TD align="left" width="30%">Cash Payment</TD>
														<td align="center" width="5%">:
														</td>
														<TD align="right" width="65%">36.40</TD>
													</tr>
												</table>
											</td>
										</tr>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterB1" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterB2" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterB3" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterB4" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblPageFooterB5" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportFooter1" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportFooter2" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportFooter3" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportFooter4" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblReportFooter5" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</TD>
	</tr>
	<tr>
		<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
