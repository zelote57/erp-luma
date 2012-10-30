<%@ Control Language="c#" Inherits="AceSoft.RetailPlus._Reports_Format.__Update" Codebehind="_Update.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
	<TR>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD><asp:label id="lblReferrer" Visible="False" runat="server"></asp:label></TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
		<TD>
		    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
		        <ContentTemplate>
		                <table cellSpacing="0" cellPadding="0" width="100%" border="0">
				            <tr>
					    <td style="PADDING-RIGHT: 10px; BORDER-TOP: white 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 10px" vAlign="top" colSpan="3">
						    <table cellSpacing="0" cellPadding="0" width="60%" align="center" border="2" frame="box">
							    <tr>
								    <td>
									    <table cellSpacing="0" cellPadding="0" width="100%" align="center">
										    <tr>
											    <td class="ms-formspacer" colSpan="3"></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="center" colSpan="3">No. of space in Report 
												    Header:
												    <asp:textbox onkeypress="AllNum()" id="txtReportHeaderSpacer" runat="server" CssClass="ms-short" BorderStyle="Groove">0</asp:textbox></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="center" colSpan="3"><asp:label id="lblCompanyName1" runat="server" CssClass="ms-pagetitle">AceSoft Software Development</asp:label></td>
										    </tr>
										    <TR>
											    <TD style="PADDING-BOTTOM: 2px" align="center" colSpan="3"><FONT color="red" size="2">(The 
													    10 lines below is called Report Header. These lines will be printed in all 
													    Front-End reports.)</FONT></TD>
										    </TR>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader1" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader1" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader1_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation1" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader2" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader2" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader2_SelectedIndexChanged"></asp:dropdownlist></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation2" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader3" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader3" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader3_SelectedIndexChanged"></asp:dropdownlist></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation3" runat="server" CssClass="ms-short" onselectedindexchanged="cboReportHeader1_SelectedIndexChanged"></asp:dropdownlist></td>
										    </tr>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader4" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader4" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader4_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation4" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader5" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader5" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader5_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation5" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader6" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader6" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader6_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation6" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader7" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader7" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader7_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation7" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader8" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader8" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader8_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation8" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader9" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader9" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader9_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation9" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportHeader10" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeader10" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportHeader10_SelectedIndexChanged"></asp:dropdownlist></TD>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportHeaderOrientation10" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <TD style="PADDING-BOTTOM: 2px" align="center" colSpan="3"><FONT color="red" size="2">(The 
													    10 lines below is called PageHeader. These lines will be printed only in 
													    receipts)</FONT></TD>
										    </TR>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader1" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader1" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader1_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation1" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader2" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader2" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader2_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation2" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader3" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader3" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader3_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation3" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader4" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader4" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader4_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation4" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader5" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader5" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader5_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation5" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader6" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader6" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader6_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation6" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader7" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader7" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader7_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation7" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader8" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader8" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader8_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation8" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader9" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader9" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader9_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation9" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageHeader10" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeader10" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageHeader10_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageHeaderOrientation10" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td class="ms-formspacer" colSpan="3"></td>
										    </tr>
										    <tr>
											    <td class="ms-authoringcontrols" style="PADDING-BOTTOM: 2px" align="center" colSpan="3">
												    <table class="ms-authoringcontrols" cellSpacing="0" cellPadding="0" width="90%" align="center">
													    <tr>
														    <TD align="left" width="30%">DESC</TD>
														    <td align="center" width="10%">QTY
														    </td>
														    <TD align="right" width="10%">PRICE</TD>
														    <td align="right" width="10%">AMOUNT</td>
													    </tr>
													    <TR>
														    <TD align="center" colSpan="4"><asp:label id="Label3" runat="server" CssClass="ms-pagecaption">--------------------------------------------------------------------------------------------------------------------------------------</asp:label></TD>
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
														    <TD align="center" colSpan="4"><asp:label id="Label4" runat="server" CssClass="ms-pagecaption">--------------------------------------------------------------------------------------------------------------------------------------</asp:label></TD>
													    </TR>
												    </table>
											    </td>
										    </tr>
										    <tr>
											    <td colSpan="3" height="30">
												    <P align="center"><FONT color="red"><IMG alt="" src="../../../_layouts/images/blank.gif"><FONT size="2">
															    (The&nbsp;20 lines below is called PageFooterA. These lines will be printed 
															    only in receipts)</FONT></FONT></P>
											    </td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA1" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA1" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA1_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation1" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA2" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA2" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA2_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation2" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA3" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA3" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA3_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation3" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA4" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA4" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA4_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation4" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA5" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA5" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA5_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation5" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA6" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA6" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA6_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation6" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA7" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA7" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA7_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation7" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA8" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA8" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA8_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation8" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA9" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA9" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA9_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation9" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA10" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA10" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA10_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation10" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA11" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA11" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA11_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation11" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA12" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA12" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA12_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation12" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA13" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA13" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA13_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation13" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA14" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA14" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA14_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation14" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA15" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA15" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA15_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation15" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA16" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA16" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA16_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation16" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA17" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA17" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA17_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation17" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA18" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA18" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA18_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation18" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA19" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA19" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA19_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation19" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterA20" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterA20" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterA20_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterAOrientation20" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
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
										    <tr>
											    <td colSpan="3" height="30">
												    <P align="center"><FONT color="red"><IMG alt="" src="../../../_layouts/images/blank.gif"><FONT size="2">
															    (The&nbsp;20 lines below is called PageFooterB. These lines will be printed 
															    only in receipts)</FONT></FONT></P>
											    </td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterB1" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterB1" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterB1_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterBOrientation1" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterB2" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterB2" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterB2_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterBOrientation2" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterB3" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterB3" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterB3_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterBOrientation3" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterB4" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterB4" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterB4_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterBOrientation4" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtPageFooterB5" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <TD style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterB5" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboPageFooterB5_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboPageFooterBOrientation5" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </TR>
										    <TR>
											    <TD style="PADDING-BOTTOM: 2px" align="center" colSpan="3">
												    <P align="center"><FONT color="red"><IMG alt="" src="../../../_layouts/images/blank.gif"><FONT size="2">
															    (The 5 lines below is called ReportFooter. These lines will be printed in all 
															    Front-End reports)</FONT></FONT></P>
											    </TD>
										    </TR>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportFooter1" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooter1" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportFooter1_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooterOrientation1" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportFooter2" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooter2" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportFooter2_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooterOrientation2" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportFooter3" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooter3" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportFooter3_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooterOrientation3" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportFooter4" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooter4" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportFooter4_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooterOrientation4" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="left"><asp:textbox id="txtReportFooter5" runat="server" CssClass="ms-long" MaxLength="15"></asp:textbox></td>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooter5" runat="server" CssClass="ms-long" AutoPostBack="True" onselectedindexchanged="cboReportFooter5_SelectedIndexChanged"></asp:dropdownlist>
											    <td style="PADDING-BOTTOM: 2px" align="right"><asp:dropdownlist id="cboReportFooterOrientation5" runat="server" CssClass="ms-short"></asp:dropdownlist></td>
										    </tr>
										    <tr>
											    <td style="PADDING-BOTTOM: 2px" align="center" colSpan="3">No. of space in Report 
												    Footer:
												    <asp:textbox onkeypress="AllNum()" id="txtReportFooterSpacer" runat="server" CssClass="ms-short" BorderStyle="Groove">1</asp:textbox></td>
										    </tr>
									    </table>
								    </td>
							    </tr>
						    </table>
					    </td>
				    </tr>
				            <TR>
					    <td style="PADDING-RIGHT: 10px; PADDING-BOTTOM: 20px" vAlign="top" align="center" colSpan="3">
						    <table class="ms-toolbar" id="onetidGrpsTC" cellSpacing="0" cellPadding="2" border="0" width="100%">
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
											    <TD height="30"><IMG alt="" src="../../../_layouts/images/blank.gif"></TD>
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
				    <TR>
					    <td style="PADDING-RIGHT: 10px; PADDING-BOTTOM: 20px" vAlign="top" align="center" colSpan="3">
						    <TABLE class="ms-toolbar" cellSpacing="0" cellPadding="2" border="0">
							    <TR>
								    <td class="ms-toolbar" align="center">
									    <table cellSpacing="0" cellPadding="1" border="0">
										    <tr>
											    <td class="ms-toolbar" noWrap><asp:imagebutton id="imgSave" title="Save the receipt format" accessKey="V" tabIndex="1" runat="server" CssClass="ms-toolbar" ImageUrl="../../../_layouts/images/newuser.gif" alt="Save the receipt format" border="0" width="16" height="16"></asp:imagebutton>&nbsp;
											    </td>
											    <td noWrap><asp:linkbutton id="cmdSave" title="Save the receipt format" accessKey="N" tabIndex="2" runat="server" CssClass="ms-toolbar" onclick="cmdSave_Click">Save the receipt format</asp:linkbutton></td>
										    </tr>
									    </table>
								    </td>
							    </TR>
						    </TABLE>
					    </td>
				    </TR>
		            <tr>
			            <td class="ms-formspacer"></td>
		            </tr>
			    </table>
		        </ContentTemplate>
            </asp:UpdatePanel>
		</TD>
		<td><IMG height="1" alt="" src="../../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td class="ms-sectionline" colSpan="3" height="2"><IMG alt="" src="../../../_layouts/images/empty.gif"></td>
	</tr>
	<tr>
		<td colSpan="3"><IMG height="10" alt="" src="../../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
