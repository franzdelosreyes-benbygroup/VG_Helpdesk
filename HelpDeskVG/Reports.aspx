<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Reports.aspx.cs" Inherits="HelpDeskVG.Reports" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">
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
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lnkTixClosedReport" runat="server" OnClick="lnkTixClosedReport_Click" CssClass="btn btn-outline-primary w-100">Tickets Closed Report</asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lnkTixFullyResolved" runat="server" OnClick="lnkTixFullyResolved_Click" CssClass="btn btn-outline-primary w-100">Tickets Fully Resolved Report</asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lnkTixAutoClosed" runat="server" OnClick="lnkTixAutoClosed_Click" CssClass="btn btn-outline-primary w-100">Tickets Auto Closed Report</asp:LinkButton>
                                </div>
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
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>