
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User_Portal/User_Portal.Master" CodeBehind="Dashboard.aspx.cs" Inherits="HelpDeskVG.User_Portal.Dashboard" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderId="head">
        <title>Dashboard</title>

    <script type="text/javascript">
        function detailsModal() {
            $(document).ready(function () {
                $("#mdDetailsUsersTicket").modal("show");
            });
        }

        function resolvedDetailsModal() {
            $(document).ready(function () {
                $("#mdResolvedDetailsUsersTicket").modal("show");
            });
        }

        function rejectSolutionModal() {
            $(document).ready(function () {
                $("#mdUserRejectProposedTicket").modal("show");
            });
        }

        function showTicketHistory() {
            $(document).ready(function () {
                $("#mdTicketHistory").modal("show");
            });
        }

        function saveAsDraft() {
            return confirm("Are you sure you want to save it as Draft?");
        }
        function saveActiveTab() {
            localStorage.setItem("activeTab", document.querySelector(".nav-link.active").getAttribute("href"));
        }

        document.addEventListener("DOMContentLoaded", function () {
            let activeTab = localStorage.getItem("activeTab");

            if (activeTab) {
                let tabElement = document.querySelector(`[href="${activeTab}"]`);
                if (tabElement) {
                    let tab = new bootstrap.Tab(tabElement);
                    tab.show();
                }
            }

            document.querySelectorAll('[data-bs-toggle="tab"]').forEach(tab => {
                tab.addEventListener("shown.bs.tab", function (event) {
                    let selectedTab = event.target.getAttribute("href");
                    localStorage.setItem("activeTab", selectedTab);
                });
            });
        });

        function validateFormEdited() {
            var natureOfProblem = document.getElementById('<%= ddlNatureofprobMd.ClientID %>').value;
            var priority = document.getElementById('<%= ddlPriorityMd.ClientID %>').value;
            var category = document.getElementById('<%= ddlCategoryMd.ClientID %>').value;
            var section = document.getElementById('<%= ddlSectionMd.ClientID %>').value;
            var subject = document.getElementById('<%= txtSubjectMd.ClientID %>').value.trim();
            var description = document.getElementById('<%= txtDescriptionMd.ClientID %>').value.trim();

            if (natureOfProblem === "" || priority === "" || category === "" || section === "" ||
                subject === "" || description === "") {

                alert("Please fill up the field that is Required.");
                return false;
            }
            else {  
                return confirm("Are you sure you want to Save Edited Details?");
            }
        }


        function validateRejectSolution() {
            var remarks = document.getElementById('<%= txtRejectRemarks.ClientID%>').value;
            var attachmentdesc = document.getElementById('<%= txtAttachmentDescription.ClientID%>').value;
            var file = document.getElementById('<%= fuUploadAttachment.ClientID%>').value;

            if (remarks === "" || attachmentdesc === "" || file === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }
            return confirm("Are you sure you want to Reject Solution?");

        }

        function validateAcceptSolution() {

            return confirm("Are you sure you want to accept the solution?");;
        }


        $(document).ready(function () {
            $('.custom-select').select2({ width: '100%' });

        });
    </script>
    <link href="../dist/css/select2.css" rel="stylesheet" />
    <script src="../dist/js/select2.min.js"></script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
                        <h1><%=Page.Title %></h1>

        </section>
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div class="row  align-items-end">
                        <div class="col-md-2 mb-3">
                            <asp:Label ID="lblFilterByDateTicketFrom" runat="server" CssClass="form-label status status-primary">Creation Date From:</asp:Label>
                            <asp:TextBox ID="txtFilterDateFrom" runat="server" CssClass="form-control text text-reset mt-2" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 mb-3">
                            <asp:Label ID="lblFilterByDateTicketTo" runat="server" CssClass="form-label status status-primary">Creation Date To:</asp:Label>
                            <asp:TextBox ID="txtFilterDateTo" runat="server" CssClass="form-control text text-reset mt-2" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 mb-3">
                            <asp:Label ID="lblSearchTicket" runat="server" CssClass="form-label status status-primary">Ticket Code:</asp:Label>
                            <asp:TextBox ID="txtSearchTicket" CssClass="form-control text text-reset mt-2" Placeholder="Search Ticket Code.." runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4 mb-3">
                            <asp:Label ID="lblSearchByCreatedBy" runat="server" CssClass="form-label status status-primary mb-2">Filter Created By:</asp:Label>
                            <asp:DropDownList ID="ddlEmployeeVg" CssClass="custom-select text text-reset mt-2" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 mb-3">
                            <asp:Label ID="lblPriorityFilter" runat="server" CssClass="form-label status status-primary">Priority:</asp:Label>
                            <asp:DropDownList ID="ddlPriorityFilter" runat="server" CssClass="form-select mt-2"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="row align-items-end">
                        <div class="col-md-3 mb-3">
                            <asp:Label ID="lblSectionFilter" runat="server" CssClass="form-label status status-primary">Section:</asp:Label>
                            <asp:DropDownList ID="ddlSectionFilter" runat="server" OnSelectedIndexChanged="ddlSectionFilter_SelectedIndexChanged" Enabled="true" AutoPostBack="true" CssClass="form-select text text-reset mt-2"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 mb-3">
                            <asp:Label ID="lblCategoryFilter" runat="server" CssClass="form-label status status-primary">Category:</asp:Label>
                            <asp:DropDownList ID="ddlCategoryFilter" runat="server" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged" AutoPostBack="true" Enabled="false" CssClass="form-select text text-reset mt-2"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 mb-3">
                            <asp:Label ID="lblNatureOfProbFilter" runat="server" CssClass="form-label status status-primary">Nature Of Problem:</asp:Label>
                            <asp:DropDownList ID="ddlNatureOfProbFilter" runat="server" OnSelectedIndexChanged="ddlNatureOfProbFilter_SelectedIndexChanged" AutoPostBack="true" Enabled="false" CssClass="form-select text text-reset mt-2"></asp:DropDownList>
                        </div>
                        <div class="col-md-3 mb-3">
                            <asp:Label ID="lblTicketStatusFilter" CssClass="form-label status status-primary" runat="server">Ticket Status</asp:Label>
                            <asp:DropDownList ID="ddlTicketStatus" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Please Select" Value="" />
                                <asp:ListItem Text="Draft" Value="0" />
                                <asp:ListItem Text="For Assigning" Value="1" />
                                <asp:ListItem Text="Rejected Ticket" Value="2" />
                                <asp:ListItem Text="Assignment Confirmation" Value="3" />
                                <asp:ListItem Text="Assigned" Value="4" />
                                <asp:ListItem Text="For Re-Assigning" Value="5" />
                                <asp:ListItem Text="Resolved" Value="6" />
                                <asp:ListItem Text="Not Resolved" Value="7" />
                                <asp:ListItem Text="Closed" Value="8" />
                                <asp:ListItem Text="Auto Closed" Value="9" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <asp:LinkButton ID="lnkFilterMyTicket" OnClick="lnkFilterMyTicket_Click" runat="server" CssClass="form-control btn btn-primary">
                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-filter"><path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                   <path d="M4 4h16v2.172a2 2 0 0 1 -.586 1.414l-4.414 4.414v7l-6 2v-8.5l-4.48 -4.928a2 2 0 0 1 -.52 -1.345v-2.227z" /></svg>
                               Filter</asp:LinkButton>
                </div>
            </div>
        </div>

<div class="page-wrapper">
    <div class="page-body">
        <div class="card">
            <div class="card-header bg-red">
                    <ul class="nav nav-tabs card-header-tabs bg-red nav-fill" data-bs-toggle="tabs" role="tablist">
                <li class="nav-item text-black" role="presentation">
                    <a class="nav-link tab-container active" data-bs-toggle="tab" href="#userTickets" aria-selected="false" role="tab">
                        <span id="ticketCount" class="custom-count">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-ticket">
                                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                <path d="M15 5l0 2" />
                                <path d="M15 11l0 2" />
                                <path d="M15 17l0 2" />
                                <path d="M5 5h14a2 2 0 0 1 2 2v3a2 2 0 0 0 0 4v3a2 2 0 0 1 -2 2h-14a2 2 0 0 1 -2 -2v-3a2 2 0 0 0 0 -4v-3a2 2 0 0 1 2 -2" />
                            </svg>
                            <asp:Label ID="lblMyCreatedTicketCount" CssClass="ticket-number" runat="server"></asp:Label>
                        </span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-lfinejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-ticket">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M15 5l0 2" />
                            <path d="M15 11l0 2" />
                            <path d="M15 17l0 2" />
                            <path d="M5 5h14a2 2 0 0 1 2 2v3a2 2 0 0 0 0 4v3a2 2 0 0 1 -2 2h-14a2 2 0 0 1 -2 -2v-3a2 2 0 0 0 0 -4v-3a2 2 0 0 1 2 -2" />
                        </svg>
                        My Created Tickets               
                    </a>
                </li>
                <li class="nav-item" role="presentation">
                    <a class="nav-link tab-container" data-bs-toggle="tab" href="#pendingApprovalTickets" aria-selected="false" role="tab" tabindex="-1">
                        <span id="ticketPendingApprovalResolvedCount" class="custom-count">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-list-check">
                                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                <path d="M3.5 5.5l1.5 1.5l2.5 -2.5" />
                                <path d="M3.5 11.5l1.5 1.5l2.5 -2.5" />
                                <path d="M3.5 17.5l1.5 1.5l2.5 -2.5" />
                                <path d="M11 6l9 0" />
                                <path d="M11 12l9 0" />
                                <path d="M11 18l9 0" />
                            </svg>
                            <asp:Label ID="lblPendingApprovalResolvedCount" runat="server" CssClass="ticket-number"></asp:Label>
                        </span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-list-check">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M3.5 5.5l1.5 1.5l2.5 -2.5" />
                            <path d="M3.5 11.5l1.5 1.5l2.5 -2.5" />
                            <path d="M3.5 17.5l1.5 1.5l2.5 -2.5" />
                            <path d="M11 6l9 0" />
                            <path d="M11 12l9 0" />
                            <path d="M11 18l9 0" />
                        </svg>
                        Pending Approval Resolved Ticket
                    </a>
                </li>
                <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#rejectedTicketByAdmin" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketMyRejectTicket" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-xbox-x">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z" />
                                        <path d="M9 8l6 8" />
                                        <path d="M15 8l-6 8" />
                                    </svg>
                                    <asp:Label ID="lblMyRejectedTicketCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-xbox-x">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z" />
                                    <path d="M9 8l6 8" />
                                    <path d="M15 8l-6 8" />
                                </svg>
                                My Rejected Tickets
                        </a>
                    </li>
                </ul>
            </div>
            <div class="card-body">
                <div class="tab-content">
                    <div class="tab-pane active show" id="userTickets" role="tabpanel">
                        <asp:Label ID="lblUserTicketsContent" runat="server" CssClass="h4" Text="My Tickets"></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="gvUserTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvUserTicketList_PageIndexChanging" PagerStyle-CssClass="GridPager" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Priority">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server"
                                                Text='<%# Eval("priority_level") %>' Style='<%# "color: " + Eval("color_code") %>' Font-Bold="true">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderId" Value='<%# Eval("ticket_id")%>' runat="server" />
                                            <asp:HiddenField ID="hfIsDraftId" Value='<%# Eval("approval_transactional_level") %>' runat="server" />

                                            <asp:LinkButton ID="lnkDetailsUserListTicket" OnClick="lnkDetailsUserListTicket_Click" CssClass="btn btn-info" runat="server">
                                                  <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                                                View Details
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkViewHistory" OnClick="lnkViewHistory_Click" ToolTip="View History" Visible='<%# (Eval("is_draft").ToString() != "True" ? true : false) %>' runat="server" CssClass="btn btn-warning position-relative w-50">
                                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  
                                                        class="icon icon-tabler icons-tabler-outline icon-tabler-history">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 8l0 4l2 2" />
                                                        <path d="M3.05 11a9 9 0 1 1 .5 4m-.5 5v-5h5" />
                                                    </svg>
                                                View History
                                            </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDeleteDraft" runat="server" ToolTip="Delete Draft" Visible='<%# (Eval("is_draft").ToString() == "True" ? true : false) %>' CommandArgument='<%# Eval("ticket_id")%>' OnClick="lnkDeleteDraft_Click" CssClass="btn btn-danger position--relative w-50">
                                                  <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-trash"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 7l16 0" /><path d="M10 11l0 6" /><path d="M14 11l0 6" />
                                                                <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" /><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
                                                                Delete
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="pendingApprovalTickets" role="tabpanel">
                        <asp:Label ID="lblforPendingApproval" runat="server" CssClass="h4" Text="Pending List of Resolved Tickets."></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="gvUserPendingApproval" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" PagerStyle-CssClass="GridPager" OnPageIndexChanging="gvUserPendingApproval_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Priority">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server"
                                                Text='<%# Eval("priority_level") %>'
                                                Style='<%# "color: " + Eval("color_code") %>' Font-Bold="true">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderIdAcceptTicket" runat="server" Value='<%# Eval("ticket_id")%>' />
                                            <asp:LinkButton ID="lnkTicketApprovalUser" OnClick="lnkTicketApprovalUser_Click" CssClass="btn btn-info w-50" runat="server">
                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                        View Details</asp:LinkButton>
                                            <%--  <asp:LinkButton ID="btnAssignToReassign" OnClick="btnAssignToReassign_Click" CssClass="btn btn-success w-25" runat="server">Assign</asp:LinkButton>
                            <asp:LinkButton ID="btnRejectTicketReassign" OnClick="btnRejectTicketReassign_Click" CssClass="btn btn-danger w-25" runat="server">Reject Ticket</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkTicketApprovalResolvedDetails" runat="server" CssClass="btn btn-success" OnClick="lnkTicketApprovalResolvedDetails_Click">
                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-thumb-up"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 11v8a1 1 0 0 1 -1 1h-2a1 1 0 0 1 -1 -1v-7a1 1 0 0 1 1 -1h3a4 4 0 0 0 4 -4v-1a2 2 0 0 1 4 0v5h3a2 2 0 0 1 2 2l-1 5a2 3 0 0 1 -2 2h-7a3 3 0 0 1 -3 -3" /></svg>
                        Resolved Details
                    </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="rejectedTicketByAdmin" role="tabpanel">
                        <asp:Label ID="Label1" runat="server" CssClass="h4" Text="Rejected Tickets due to incomplete details."></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="gvRejectedTicketByAdmin" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" PagerStyle-CssClass="GridPager" OnPageIndexChanging="gvRejectedTicketByAdmin_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Priority">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server"
                                                Text='<%# Eval("priority_level") %>'
                                                Style='<%# "color: " + Eval("color_code") %>' Font-Bold="true">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:BoundField DataField="admin_recent_reject_remarks" HeaderText="Admin Reject Remarks" />
                                    <asp:BoundField DataField="admin_rejector" HeaderText="Admin Disapprover" />
                                    <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderIdRejectedList" runat="server" Value='<%#Eval("ticket_id") %>' />
                                            <asp:LinkButton ID="lnkDetailsRejectedTicketList" OnClick="lnkDetailsRejectedTicketList_Click" CssClass="btn btn-info" runat="server">
                                                <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                                View Details</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
            </div>
        </div>
    </div>
 </div>
</div>
        <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdDetailsUsersTicket">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                <div class="modal-content">
                    <div class="modal-header bg-red">
                        <h5 class="modal-title">Ticket Details</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                 <asp:HiddenField ID="hfMdTicketHeaderId" runat="server" />
                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label class="form-label status status-primary mb-2">Created By:</label>
                                        <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control text-reset" Value='<%# Eval("created_by") %>' Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label class="form-label status status-primary mb-2">Created For:</label>
                                        <asp:TextBox ID="txtCreatedFor" runat="server" CssClass="form-control text-reset" Value='<%# Eval("created_for")%>' Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlCreatedForMd" runat="server" CssClass="form-select text-reset">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="lblCreatedAt" runat="server" CssClass="form-label status status-primary">Created At:</asp:Label>
                                    <asp:TextBox ID="txtCreatedAt" runat="server" CssClass="form-control text-reset mt-2" TextMode="DateTimeLocal"></asp:TextBox>
                                </div>
                                <script type="text/javascript">
                                    // Automatically set the maximum date to today
                                    var txtBox = document.getElementById('<%= txtCreatedAt.ClientID %>');
                                    var today = new Date().toISOString().split('T')[0];
                                    txtBox.max = today;
                                </script>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label class="form-label status status-primary mb-2 required">Subject:</label>
                                    <asp:TextBox ID="txtSubjectMd" runat="server" CssClass="form-control text-reset" Value='<%# Eval("subject") %>'></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label class="form-label status status-primary mb-2 required">Description:</label>
                                    <asp:TextBox ID="txtDescriptionMd" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset" Value='<%# Eval("description")%>'></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="mb-3">
                                    <label class="form-label status status-primary mb-2 required">Section</label>
                                    <asp:DropDownList ID="ddlSectionMd" CssClass="form-select text-reset" OnSelectedIndexChanged="ddlSectionMd_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="mb-3">
                                    <label class="form-label status status-primary mb-2 required">Category</label>
                                    <asp:DropDownList ID="ddlCategoryMd" CssClass="form-select text-reset" OnSelectedIndexChanged="ddlCategoryMd_SelectedIndexChanged" AutoPostBack="true"  Enabled="false" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="mb-3">
                                    <label class="form-label status status-primary mb-2 required">Nature Of Problem</label>
                                </div>
                                <asp:DropDownList ID="ddlNatureofprobMd" OnSelectedIndexChanged="ddlNatureofprobMd_SelectedIndexChanged" Enabled="false" CssClass="form-select text-reset mb-3" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label class="form-label status status-primary mb-2">Attachment</label>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDownloadableAttachment" runat="server" CssClass="table table-hover table-bordered table-striped no-wrap" GridLines="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="file_name" HeaderText="File Name" />
                                            <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                            <asp:BoundField DataField="description_attachment" HeaderText="Attachment Description" />
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <div class="d-flex justify-content-center gap-2">   
                                                        <%--<asp:LinkButton ID="lnkViewFile" OnClick="lnkViewFile_Click" CommandName="ViewFile" CommandArgument='<%# Eval("attachment_id") %>' Text="View" CssClass="btn btn-primary" runat="server">
                                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                                    </asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkDownloadFile1" OnClick="lnkDownloadFile1_Click" CommandName="DownloadFile" CommandArgument='<%# Eval("attachment_id") %>' Text="Download" CssClass="btn btn-success" runat="server">
                                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                               Download</asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                        <div class="mb-2 mt-2">
                                            <asp:Label ID="lblAttachDesc" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtAttachmentDescriptionMd" runat="server" CssClass="form-control text-reset mt-2" Enabled="false" TextMode="MultiLine" Rows="3" placeholder="Precvious Attachment Description"></asp:TextBox>
                                        </div>
                                    <div class="col-md-12 mt-2">
                                        <asp:Label ID="lblAttachNewAttachment" CssClass="form-label status status-primary mt-2" runat="server">Upload an Attachment</asp:Label>
                                    </div>
                                        <div class="col-md-12 mt-2">
                                            <asp:FileUpload ID="fuUploadAttachmentInEdit" CssClass="form-control" runat="server" />
                                        </div>
                                             <div class="mb-2 mt-2">
                                                 <asp:Label ID="lblNewAttachmentEdit" runat="server" CssClass="form-label status status-primary">New Attachment Description:</asp:Label>
                                             </div>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtNewAttachmentInEdit" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="New Attachment Description"></asp:TextBox>
                                        </div>
                                </div>
                            </div>
                     </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="mb-3 mt-2">
                                    <label class="form-label status status-primary mt-2 required">Priority Level</label>
                                </div>
                                <asp:DropDownList ID="ddlPriorityMd" CssClass="form-select text-reset mb-3" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer d-flex justify-content-center">
                        <div class="col-md-12">
                            <asp:LinkButton ID="lnkEditDetails" runat="server" CssClass="btn btn-primary w-100" OnClick="lnkEditDetails_Click" OnClientClick="return validateFormEdited();">
                                <!-- Download SVG icon from http://tabler.io/icons/icon/plus -->
                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" /><path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                                Save Edited Ticket
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-12">
                            <asp:LinkButton ID="lnkSaveAsDraft" OnClick="lnkSaveAsDraft_Click" CssClass="btn btn-success w-100" OnClientClick="return saveAsDraft();" runat="server">
                                <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-pencil-check"><path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                    <path d="M4 20h4l10.5 -10.5a2.828 2.828 0 1 0 -4 -4l-10.5 10.5v4" /><path d="M13.5 6.5l4 4" />
                                    <path d="M15 19l2 2l4 -4" />
                                </svg>
                                Save as Draft
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdUserRejectProposedTicket">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                <div class="modal-content">
                    <div class="modal-header bg-red">
                        <h5 class="modal-title">Reject Proposed Solution</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                 <asp:HiddenField ID="hfUserRejectHeaderID" runat="server" />
                    <div class="modal-body">
                        <asp:Label ID="lblRejectRemarks" runat="server" Text="Remarks" Placeholder="Input Remarks" CssClass="form-label status status-primary required"></asp:Label>
                        <asp:TextBox ID="txtRejectRemarks" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                        <asp:Label ID="lblRejectAttachment" runat="server" Text="Attachment" CssClass="form-label status status-primary required mt-2 mb-2"></asp:Label>
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:FileUpload ID="fuUploadAttachment" CssClass="form-control mt-2" runat="server" />
                                </div>
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="mb-2 mt-2">
                                            <asp:Label ID="lblAttachmentDescription" runat="server" CssClass="form-label status status-primary required">Attachment Description:</asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtAttachmentDescription" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvHDUploadedAttachment" runat="server" AutoGenerateColumns="false" CssClass="table table-vcenter table-hover text-nowrap">
                            <Columns>
                                <asp:BoundField DataField="file_name" runat="server" HeaderText="File Name" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <%-- <asp:LinkButton ID="lnkDeleteAttachment" OnClick="lnkDeleteAttachment_Click" runat="server"><i class="ti ti-pencil">Edit</i></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="modal-footer">
                            <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                            <asp:LinkButton ID="lnkUserRejectProposedSolution" runat="server" OnClick="lnkUserRejectProposedSolution_Click" OnClientClick="return validateRejectSolution();" CssClass="btn btn-success">Save as Reject Solution</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdResolvedDetailsUsersTicket">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                <div class="modal-content">
                    <div class="modal-header bg-red">
                        <h5 class="modal-title">Resolution</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                 <asp:HiddenField ID="hfTicketHeaderIdforResolved" runat="server" />
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label class="form-label">Proposed Remarks:</label>
                                    <asp:TextBox ID="txtProposedRemarksMd" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset" Value='<%# Eval("proposed_remarks")%>'></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <label class="form-label">Proposed Attachment</label>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDownloadAttachmentInResolved" runat="server" CssClass="table table-hover table-bordered table-striped no-wrap" GridLines="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="file_name" HeaderText="File Name" />
                                            <asp:BoundField DataField="created_at" HeaderText="Attachment Date" />
                                            <asp:BoundField DataField="proposed_remarks" HeaderText="Proposed Remarks" />
                                            <asp:BoundField DataField="description_attachment" HeaderText="Attachment Description" />
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <%--<asp:HiddenField ID="hfAttachmentId" Value='<%# Eval("attachment_id")%>' runat="server" />--%>
                                                    <div class="d-flex justify-content-center gap-2">
                                                        <%--<asp:LinkButton ID="lnkViewFile" OnClick="lnkViewFile_Click" CommandName="ViewFile" CommandArgument='<%# Eval("attachment_id") %>' Text="View" CssClass="btn btn-primary" runat="server">
                                             <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                            
                                                  </asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkDownloadFile" OnClick="lnkDownloadFile_Click" CommandName="DownloadFile" CommandArgument='<%# Eval("attachment_proposed_id") %>' Text="Download" CssClass="btn btn-success" runat="server">
                                                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                                         Download</asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="col-md-12">
                                        <div class="mb-3">
                                            <asp:Label ID="lblAttachDescriptionProposed" CssClass="form-label" runat="server">Attachment Description:</asp:Label>
                                            <asp:TextBox ID="txtDescriptionAttachmentProposed" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset" Value='<%# Eval("description_attachment")%>'></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer d-flex justify-content-center">
                            <asp:LinkButton ID="lnkAcceptResolvedTicket" runat="server" CssClass="btn btn-success" OnClientClick="return validateAcceptSolution();" OnClick="lnkAcceptResolvedTicket_Click">
                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-user-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" /><path d="M16 19h6" /><path d="M19 16v6" /><path d="M6 21v-2a4 4 0 0 1 4 -4h4" /></svg>
                        Accept Solution
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkRejectResolvedTicket" runat="server" CssClass="btn btn-danger" OnClick="lnkRejectResolvedTicket_Click">
                                <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M18 6l-12 12" /><path d="M6 6l12 12" /></svg>
                                Reject Solution
                            </asp:LinkButton>
                        </div>
                </div>
            </div>
        </div>
    </div>

        <div class="modal modal-blur fade show" tabindex="-1" aria-modal="true" role="dialog" id="mdTicketHistory">
            <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header bg-red">
                        <h5 class="modal-title">Ticket History</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        <asp:HiddenField ID="hfTicketHeaderForHistory" runat="server" />
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gvTicketHistory" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:BoundField DataField="created_at" HeaderText="Date Transacted" />
                                    <asp:BoundField DataField="priority_description" HeaderText="Priority Level" />
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="transacted_by" HeaderText="Transacted By" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No Data Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </main>
</asp:Content>