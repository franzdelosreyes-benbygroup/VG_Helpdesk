<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Reports.aspx.cs" Inherits="HelpDeskVG.Reports" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">

    <style>
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-wrapper">
        <div class="page-body">
            <div class="card">
                <div class="card-header bg-red">
                </div>
                <div class="card-body">
                    <div class="card-title">
                        Reports
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblNoOfTixClosed" runat="server" CssClass="form-label status status-primary mb-2">No. of Tickets Fully Closed</asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblNoOfTicketsResolved" runat="server" CssClass="form-label status status-primary mb-2">No. of Tickets Fully Resolved</asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblNoOfTicketsAutoClosed" runat="server" CssClass="form-label status status-primary mb-2">No. of Tickets Auto Closed</asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:LinkButton ID="lnkTixClosedReport" runat="server" OnClick="lnkTixClosedReport_Click" CssClass="btn btn-primary w-100 mb-2">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24" fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                        Tickets Closed Report</asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="lnkTixFullyResolved" runat="server" OnClick="lnkTixFullyResolved_Click" CssClass="btn btn-primary w-100 mb-2">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                        Tickets Fully Resolved Report</asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="lnkTixAutoClosed" runat="server" OnClick="lnkTixAutoClosed_Click" CssClass="btn btn-primary w-100 mb-2">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                        Tickets Auto Closed Report</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblTixReportTimeToBeAssigned" runat="server" CssClass="form-label status status-primary mb-2">No. of Tickets Fully Closed</asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblTixUnresolved" runat="server" CssClass="form-label status status-primary mb-2">No. of Tickets Fully Resolved</asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblTixRawReport" runat="server" CssClass="form-label status status-primary mb-2">No. of Tickets Auto Closed</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lnkTixReportTimeToBeAssigned" OnClick="lnkTixReportTimeToBeAssigned_Click" CssClass="btn btn-primary w-100 mb-2" runat="server">
                                      <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                      Tickets Time Taken to be Assigned</asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lnkTixUnresolved" runat="server" OnClick="lnkTixUnresolved_Click" CssClass="btn btn-primary w-100 mb-2">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                        Tickets Unresolved Report</asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lnkTixRawReport" runat="server" OnClick="lnkTixRawReport_Click" CssClass="btn btn-primary w-100 mb-2">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                        Tickets Raw Report</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>

</asp:Content>