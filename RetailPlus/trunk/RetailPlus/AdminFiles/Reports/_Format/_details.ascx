<%@ Control Language="c#" Inherits="AceSoft.RetailPlus._Reports_Format.__Details" Codebehind="_Details.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
		<td><asp:label id="lblReferrer" Visible="False" runat="server"></asp:label></td>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td style="PADDING-RIGHT: 10px; padding-bottom: 20px" valign="top" align="center" colspan="3">
						<table class="ms-toolbar" cellspacing="0" cellpadding="2" border="0">
							<tr>
								<td class="ms-toolbar" align="center">
									<table cellspacing="0" cellpadding="1" border="0">
										<tr>
											<td nowrap="nowrap"><asp:label id="Label1" runat="server" CssClass="ms-toolbar">Preview of the receipt format</asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px" valign="top" colspan="3">
						<table id="Table1" cellspacing="0" cellpadding="0" width="60%" align="center" border="2" frame="box">
							<tr>
								<td>
									<table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center">
										<tr>
											<td class="ms-formspacer"></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblCompanyName" runat="server" CssClass="ms-pagetitle" Width="100%">RetailPlus Business Solutions</asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader1" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader2" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader3" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader4" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader5" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader6" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader7" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader8" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader9" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportHeader10" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader1" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader2" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader3" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader4" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader5" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader6" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader7" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader8" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader9" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageHeader10" runat="server" CssClass="ms-descriptiontext" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td class="ms-formspacer"></td>
										</tr>
										<tr>
											<td class="ms-authoringcontrols" style="padding-bottom: 2px" align="center">
												<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="90%" align="center">
													<tr>
														<td align="left" width="30%">DESC</td>
														<td align="center" width="10%">QTY
														</td>
														<td align="right" width="10%">PRICE</td>
														<td align="right" width="10%">AMOUNT</td>
													</tr>
													<tr>
														<td align="center" colspan="4"><asp:label id="Label5" runat="server" CssClass="ms-pagecaption">--------------------------------------------------------------------------------</asp:label></td>
													</tr>
													<tr>
														<td colspan="3">A&amp;W ROOTBEER 355ML</td>
														<td></td>
													</tr>
													<tr>
														<td></td>
														<td align="right">1
														</td>
														<td align="right" width="10%">18.20</td>
														<td align="right" width="10%">18.20</td>
													</tr>
													<tr>
														<td colspan="3">A&amp;W ROOTBEER 355ML</td>
														<td align="right" width="10%"></td>
													</tr>
													<tr>
														<td></td>
														<td align="right">1
														</td>
														<td align="right" width="10%">18.20</td>
														<td align="right" width="10%">18.20</td>
													</tr>
													<tr>
														<td align="center" colspan="4"><asp:label id="Label6" runat="server" CssClass="ms-pagecaption">--------------------------------------------------------------------------------</asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td height="30"><img alt="" src="/RetailPlus/_layouts/images/blank.gif"></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA1" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA2" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA3" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA4" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA5" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA6" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA7" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA8" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA9" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA10" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA11" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA12" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA13" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA14" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA15" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA16" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA17" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA18" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA19" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterA20" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td class="ms-authoringcontrols" align="center" colspan="3">
												<table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="90%" align="center">
													<tr>
														<td align="left" width="30%">Cash Payment</td>
														<td align="center" width="5%">:
														</td>
														<td align="right" width="65%">36.40</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterB1" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterB2" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterB3" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterB4" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblPageFooterB5" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportFooter1" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportFooter2" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportFooter3" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportFooter4" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblReportFooter5" runat="server" CssClass="ms-pagecaption" Width="100%"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="ms-sectionline" colspan="3" height="2"><img alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>
