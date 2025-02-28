<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Dashboard.aspx.cs" Inherits="HelpDeskVG.Dashboard" %>


<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">



    <script type="text/javascript">
        function detailsModal() {
            $(document).ready(function () {
                $("#mdDetailsUsersTicket").modal("show");
            });
        }
        function assignITPICModal() {
            $(document).ready(function () {
                $("#mdAssignTicketToITPIC").modal('show');
            });
        }
        function rejectUserTicketRemarks() {
            $(document).ready(function () {
                $("#mdRejectTicketRemarks").modal('show');
            });
        }

        function rejectSolutionModal() {
            $(document).ready(function () {
                $("#mdUserRejectProposedTicket").modal("show");
            });
        }

        function resolvedDetailsModal() {
            $(document).ready(function () {
                $("#mdResolvedDetailsUsersTicket").modal("show");
            });
        }
        function validateRejectSolution() {
            var remarks = document.getElementById('<%= txtRejectRemarks.ClientID%>').value;
                    var attachmentdesc = document.getElementById('<%= txtAttachmentDescReject.ClientID%>').value;
                    var file = document.getElementById('<%= fuUploadAttachmentReject.ClientID%>').value;

                    if (remarks === "" || attachmentdesc === "" || file === "") {
                        alert("Please fill up the field that is Required.");
                        return false;
                    }
                    return true;
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


        function validateForm() {
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

            return true;
        }

        function validateAssignTo() {
            var assignToEmp = document.getElementById('<%= ddlMdEmployeeITPIC.ClientID %>').value;

            if (assignToEmp === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }

            return true;
        }

        function validateRejectTicketToUser() {
            var remarks = document.getElementById('<%= txtRejectTicketRemarks.ClientID%>').value;

            if (remarks === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }

            return true;
        }



        $(document).ready(function () {
            $('.custom-select').select2({width:'100%'});

        });

    </script>
    <link href="dist/css/select2.css" rel="stylesheet" />
    <script src="dist/js/select2.min.js"></script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                    <div class="col-md-2 mb-3">
                        <asp:Label ID="lblSearchByCreatedBy" runat="server" CssClass="form-label status status-primary mb-2">Filter Created By:</asp:Label>
                        <div class="col-md-12">
                        <asp:DropDownList ID="ddlEmployeeVg" CssClass="custom-select text text-reset mt-2" runat="server">
                        </asp:DropDownList>
                            </div>
                    </div>
                    <div class="col-md-2 mb-3">
                        <asp:Label ID="Label3" runat="server" CssClass="form-label status status-primary">Filter Created For:</asp:Label>
                        <asp:DropDownList ID="ddlCreatedForVg" CssClass="form-select text text-reset mt-2" runat="server">
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
                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 26 26"  fill="none"  stroke="crimson"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-filter"><path stroke="none" d="M0 0h24v24H0z" fill="none"/>
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
                            <a class="nav-link tab-container active" data-bs-toggle="tab" href="#myCreatedTicket" aria-selected="false" role="tab" tabindex="-1">
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
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-ticket">
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
                        <li class="nav-item text-black" role="presentation">
                            <a class="nav-link tab-container " data-bs-toggle="tab" href="#userTickets" aria-selected="false" role="tab">
                                <span id="ticketAssignTicket" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-plus">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                        <path d="M16 19h6" />
                                        <path d="M19 16v6" />
                                        <path d="M6 21v-2a4 4 0 0 1 4 -4h4" />
                                    </svg>
                                    <asp:Label ID="lblAssignTicketCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                    <path d="M16 19h6" />
                                    <path d="M19 16v6" />
                                    <path d="M6 21v-2a4 4 0 0 1 4 -4h4" />
                                </svg>
                                Assign Ticket                    
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#forReassignITPIC" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketReassignITPICTicket" class="custom-count">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-align-left-2">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M4 4v16" />
                                    <path d="M8 6h12" />
                                    <path d="M8 12h6" />
                                    <path d="M8 18h10" />
                                </svg>
                                    <asp:Label ID="lblReassignTicketCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-align-left-2">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M4 4v16" />
                                    <path d="M8 6h12" />
                                    <path d="M8 12h6" />
                                    <path d="M8 18h10" />
                                </svg>
                                Reassign Ticket IT PIC
                            </a>
                        </li>

                        <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#assignedTickets" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketITPICAssignedTicket" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-checklist">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M9.615 20h-2.615a2 2 0 0 1 -2 -2v-12a2 2 0 0 1 2 -2h8a2 2 0 0 1 2 2v8" />
                                        <path d="M14 19l2 2l4 -4" />
                                        <path d="M9 8h4" />
                                        <path d="M9 12h2" />
                                    </svg>
                                    <asp:Label ID="ITPICAssignedTicketCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-checklist">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M9.615 20h-2.615a2 2 0 0 1 -2 -2v-12a2 2 0 0 1 2 -2h8a2 2 0 0 1 2 2v8" />
                                    <path d="M14 19l2 2l4 -4" />
                                    <path d="M9 8h4" />
                                    <path d="M9 12h2" />
                                </svg>
                                IT PIC Assigned Tickets
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#rejectedTicketAdmin" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketAdminRejectedToUserTicket" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-x">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                        <path d="M6 21v-2a4 4 0 0 1 4 -4h3.5" />
                                        <path d="M22 22l-5 -5" />
                                        <path d="M17 22l5 -5" />
                                    </svg>
                                    <asp:Label ID="lblAdminRejectedTicketToUserCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-x">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                    <path d="M6 21v-2a4 4 0 0 1 4 -4h3.5" />
                                    <path d="M22 22l-5 -5" />
                                    <path d="M17 22l5 -5" />
                                </svg>
                                Admin Rejected Tickets to User
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="card-body">
                    <div class="tab-content">
                        <div class="tab-pane active show" id="myCreatedTicket" role="tabpanel">
                            <div class="col-md-12 mt-2">
                                <asp:Label ID="lblMyCreatedTicket" runat="server" CssClass="h4" Text="My Created Tickets"></asp:Label>
                            </div>

                            <asp:GridView ID="gvMyTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMyTicketList_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:BoundField DataField="created_for" HeaderText="Created For" />
                                    <asp:BoundField DataField="created_by" HeaderText="Created By" />
                                    <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderMyTicket" Value='<%# Eval("ticket_id")%>' runat="server" />
                                            <asp:HiddenField ID="hfIsDraftId" Value='<%# Eval("approval_transactional_level") %>' runat="server" />
                                            <asp:LinkButton ID="lnkDetailsMyTicket" OnClick="lnkDetailsMyTicket_Click" CssClass="btn btn-info" runat="server">
                                             <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                                        View Details
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkDeleteDraft" runat="server" ToolTip="Delete Draft" Visible='<%# (Eval("is_draft").ToString() == "True" ? true : false) %>' CommandArgument='<%# Eval("ticket_id")%>' OnClick="lnkDeleteDraft_Click" CssClass="btn btn-danger position--relative w-50">
                                               <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-x">
                                                   <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                                   <path d="M18 6l-12 12" />
                                                   <path d="M6 6l12 12" />
                                               </svg>
                                               Delete
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="tab-pane fade" id="pendingApprovalTickets" role="tabpanel">
                            <asp:Label ID="lblforPendingApproval" runat="server" CssClass="h4" Text="Pending List of Resolved Tickets."></asp:Label>
                            <asp:GridView ID="gvMyTicketPendingApproval" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMyTicketPendingApproval_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:BoundField DataField="created_for" HeaderText="Created By" />
                                    <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
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
                        <div class="tab-pane fade" id="rejectedTicketByAdmin" role="tabpanel">
                            <asp:Label ID="Label9" runat="server" CssClass="h4" Text="Rejected Tickets due to incomplete details."></asp:Label>
                            <asp:GridView ID="gvMyTicketRejectedByAdmin" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMyTicketRejectedByAdmin_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="description" HeaderText="Description" />
                                    <asp:BoundField DataField="admin_recent_reject_remarks" HeaderText="Admin Reject Remarks" />
                                    <asp:BoundField DataField="itpic_recent_reject_remarks" HeaderText="IT PIC Reject Remarks" />
                                    <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                    <asp:BoundField DataField="created_by" HeaderText="Created At" />
                                    <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderIdRejectedList" runat="server" Value='<%#Eval("ticket_id") %>' />
                                            <asp:LinkButton ID="lnkMyTicketRejectedTicketList" OnClick="lnkMyTicketRejectedTicketList_Click" CssClass="btn btn-info w-50" runat="server">
               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                  View Details</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="tab-pane fade" id="userTickets" role="tabpanel">
                            <asp:Label ID="lblUserTicketsContent" runat="server" CssClass="h4" Text="User's tickets"></asp:Label>
                            <asp:GridView ID="gvAdminTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAdminTicketList_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:BoundField DataField="created_for" HeaderText="Created For" />
                                    <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />

                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderId" Value='<%# Eval("ticket_id")%>' runat="server" />
                                            <asp:LinkButton ID="lnkDetailsUserListTicket" OnClick="lnkDetailsUserListTicket_Click" CssClass="btn btn-info" runat="server">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                        View Details</asp:LinkButton>

                                            <%--   <asp:LinkButton ID="btnAssignTo" OnClick="btnAssignTo_Click" CssClass="btn btn-success w-25" runat="server">Assign</asp:LinkButton>
                                            <asp:LinkButton ID="btnRejectTicket" OnClick="btnRejectTicket_Click" CssClass="btn btn-danger w-25" runat="server">Reject Ticket</asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="tab-content">
                            <div class="tab-pane fade" id="forReassignITPIC" role="tabpanel">
                                <asp:Label ID="lblForReassignITPIC" runat="server" CssClass="h4" Text="Reassign Ticket will be displayed here."></asp:Label>
                                <asp:GridView ID="gvAdminForReassignTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAdminForReassignTicketList_PageIndexChanging" EmptyDataTe="No Data Found">
                                    <Columns>
                                        <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                        <asp:BoundField DataField="description_section" HeaderText="Section" />
                                        <asp:BoundField DataField="description_category" HeaderText="Category" />
                                        <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                        <asp:BoundField DataField="itpic_recent_reject_remarks" HeaderText="Rejected Reason IT PIC" />
                                        <asp:BoundField DataField="previous_itpic_assigned" HeaderText="Previous IT PIC" />
                                        <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                        <asp:BoundField DataField="created_for" HeaderText="Created For" />
                                        <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />

                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfTicketHeaderIdReassignITPIC" runat="server" Value='<%# Eval("ticket_id")%>' />
                                                <asp:LinkButton ID="lnkDetailsReassignITPIC" OnClick="lnkDetailsReassignITPIC_Click" CssClass="btn btn-info" runat="server">
                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                            View Details</asp:LinkButton>
                                                <%--  <asp:LinkButton ID="btnAssignToReassign" OnClick="btnAssignToReassign_Click" CssClass="btn btn-success w-25" runat="server">Assign</asp:LinkButton>
                                                <asp:LinkButton ID="btnRejectTicketReassign" OnClick="btnRejectTicketReassign_Click" CssClass="btn btn-danger w-25" runat="server">Reject Ticket</asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane fade" id="assignedTickets" role="tabpanel">
                                <asp:Label ID="Label2" runat="server" CssClass="h4" Text="Assigned Tickets."></asp:Label>
                                <asp:GridView ID="gvAdminAssignedTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAdminAssignedTicketList_PageIndexChanging" EmptyData="No Data Found">
                                    <Columns>
                                        <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                        <asp:BoundField DataField="description_section" HeaderText="Section" />
                                        <asp:BoundField DataField="description_category" HeaderText="Category" />
                                        <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                        <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                        <asp:BoundField DataField="created_for" HeaderText="Created For" />
                                        <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />

                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfTicketHeaderIdAssignedTicketList" runat="server" Value='<%#Eval("ticket_id")%>' />
                                                <asp:LinkButton ID="lnkDetailsAssignedTicketList" OnClick="lnkDetailsAssignedTicketList_Click" CssClass="btn btn-info" runat="server">
                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>                        
                                            View Details
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane fade" id="rejectedTicketAdmin" role="tabpanel">
                                <asp:Label ID="Label1" runat="server" CssClass="h4" Text="Rejected Tickets due to incomplete details."></asp:Label>
                                <asp:GridView ID="gvAdminForRejectedTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAdminForRejectedTicketList_PageIndexChanging" EmptyDataTe="No Data Found">
                                    <Columns>
                                        <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                        <asp:BoundField DataField="admin_recent_reject_remarks" HeaderText="Admin Reject Remarks" />
                                        <asp:BoundField DataField="itpic_recent_reject_remarks" HeaderText="IT PIC Reject Remarks" />
                                        <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                        <asp:BoundField DataField="created_for" HeaderText="Created For" />
                                        <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfTicketHeaderIdRejectedList" runat="server" Value='<%#Eval("ticket_id") %>' />
                                                <asp:LinkButton ID="lnkDetailsRejectedTicketList" OnClick="lnkDetailsRejectedTicketList_Click" CssClass="btn btn-info w-50" runat="server">
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
                <div class="modal-header">
                    <h5 class="modal-title">Ticket Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfMdTicketHeaderId" runat="server" />
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <asp:Label ID="lblCreatedBy" runat="server" CssClass="form-label status status-primary">Created By:</asp:Label>
                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control text-reset mt-2" Value='<%# Eval("created_by") %>' Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label class="form-label status status-primary">Created For:</label>
                                    <asp:TextBox ID="txtCreatedFor" runat="server" CssClass="form-control text-reset" Visible="false" Value='<%# Eval("created_for")%>' Enabled="true"></asp:TextBox>
                                    <asp:DropDownList ID="ddlCreatedForMd" runat="server" CssClass="form-select text-reset mt-2">
                                    </asp:DropDownList>
                                </div>
                           </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="lblassigntomd" runat="server" CssClass="form-label status status-primary required">Assign To:</asp:Label>
                                    <asp:DropDownList ID="ddlAssignToEmpITMd" runat="server" CssClass="form-select text-reset mt-2">

                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label class="form-label status status-primary required">Subject:</label>
                                <asp:TextBox ID="txtSubjectMd" runat="server" CssClass="form-control text-reset mt-2" Value='<%# Eval("subject") %>'></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label class="form-label status status-primary required">Description:</label>
                                <asp:TextBox ID="txtDescriptionMd" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2" Value='<%# Eval("description")%>'></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="mb-3">
                                <label class="form-label status status-primary required">Section</label>
                                <asp:DropDownList ID="ddlSectionMd" CssClass="form-select text-reset mt-2" OnSelectedIndexChanged="ddlSectionMd_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="mb-3">
                                <label class="form-label status status-primary required">Category</label>
                                <asp:DropDownList ID="ddlCategoryMd" CssClass="form-select text-reset mt-2" OnSelectedIndexChanged="ddlCategoryMd_SelectedIndexChanged" AutoPostBack="true"  Enabled="false" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="mb-3">
                                <label class="form-label status status-primary required">Nature Of Problem</label>
                            </div>
                            <asp:DropDownList ID="ddlNatureofprobMd" CssClass="form-select text-reset mb-3 mt-2" OnSelectedIndexChanged="ddlNatureofprobMd_SelectedIndexChanged" AutoPostBack="true"  Enabled="false" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <label class="form-label status status-primary">Attachment</label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvDownloadableAttachment" runat="server" CssClass="table table-hover table-bordered table-striped no-wrap" GridLines="None" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="file_name" HeaderText="File Name" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hfAttachmentId" Value='<%# Eval("attachment_id")%>' runat="server" />--%>
                                                <div class="d-flex justify-content-center gap-2">
                                                    <%--<asp:LinkButton ID="lnkViewFile" OnClick="lnkViewFile_Click" CommandName="ViewFile" CommandArgument='<%# Eval("attachment_id") %>' Text="View" CssClass="btn btn-primary" runat="server">
                                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                              
                                                    </asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lnkDownloadFile" OnClick="lnkDownloadFile_Click" CommandName="DownloadFile" CommandArgument='<%# Eval("attachment_id") %>' Text="Download" CssClass="btn btn-success" runat="server">
                                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" /><path d="M12 4l0 12" /></svg>
                                               Download</asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="mb-2 mt-2">
                                    <asp:Label ID="lblAttachmentDescription" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtAttachmentDescriptionMd" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                </div>
                                <div class="col-md-12 mt-2">
                                    <asp:Label ID="lblAttachNewAttachment" CssClass="form-label status status-primary required mt-2" runat="server">Upload an Attachment</asp:Label>
                                </div>
                                <div class="col-md-12 mt-2">
                                    <asp:FileUpload ID="fuUploadAttachmentInEdit" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="lblNewAttachmentInEdit" runat="server" CssClass="form-label status status-primary required mt-2">New Attachment Description:</asp:Label>
                                    <asp:TextBox ID="txtNewAttachmentInEdit" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="New Attachment Description"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="mb-3 mt-2">
                                <label class="form-label status status-primary required">Priority Level</label>
                            </div>
                            <asp:DropDownList ID="ddlPriorityMd" CssClass="form-select text-reset mb-3" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 modal-footer d-block justify-content-between gap-1">
                    <div class="row">
                        <div class="col-md-12">
                        <asp:LinkButton ID="lnkAcceptTicketProposal" runat="server" CssClass="btn btn-success" OnClick="lnkAcceptTicketProposal_Click">
                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>                               
                        Accept Ticket
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkRejectTicketProposal" runat="server" CssClass="btn btn-danger" OnClick="lnkRejectTicketProposal_Click">
                                <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M18 6l-12 12" /><path d="M6 6l12 12" /></svg>
                                Reject Ticket
                        </asp:LinkButton>
                            <asp:LinkButton ID="lnkAssignTicketToITPIC" runat="server" CssClass="btn btn-success" OnClick="lnkAssignTicketToITPIC_Click">
                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-user-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" /><path d="M16 19h6" /><path d="M19 16v6" /><path d="M6 21v-2a4 4 0 0 1 4 -4h4" /></svg>
                        Assign Ticket
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkRejectTicketUser" runat="server" CssClass="btn btn-danger" OnClick="lnkRejectTicketUser_Click">
                                <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M18 6l-12 12" /><path d="M6 6l12 12" /></svg>
                                Reject Ticket
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkEditDetails" runat="server" CssClass="btn btn-primary" OnClick="lnkEditDetails_Click" OnClientClick="return validateForm();">
                                <!-- Download SVG icon from http://tabler.io/icons/icon/plus -->
                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" /><path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                              Save Edited Details
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdRejectTicketRemarks">
             <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Reject Ticket Remarks</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <asp:Label ID="lblRemarksRejectAdmin" runat="server" CssClass="form-label status status-primary required">Remarks</asp:Label>
                <asp:TextBox ID="txtRejectTicketRemarks" runat="server" CssClass="form-control text-area text-reset mt-2" TextMode="MultiLine" Rows="6"></asp:TextBox>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                <asp:LinkButton ID="lnkMdRejectTicketAdminToUser" runat="server" OnClick="lnkMdRejectTicketAdminToUser_Click" OnClientClick="return validateRejectTicketToUser();" CssClass="btn btn-danger">Reject Ticket</asp:LinkButton>
            </div>
        </div>
      </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdAssignTicketToITPIC">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Assign Ticket To</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:DropDownList ID="ddlMdEmployeeITPIC" CssClass="form-select text-reset" runat="server"></asp:DropDownList>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkAssignITPICTicket" runat="server" OnClick="lnkAssignITPICTicket_Click" OnClientClick="return validateAssignTo();" CssClass="btn btn-success">Save changes</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdResolvedDetailsUsersTicket">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
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
                                        <label class="form-label">Attachment Description:</label>
                                        <asp:TextBox ID="txtDescriptionAttachmentProposed" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset" Value='<%# Eval("description_attachment")%>'></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer d-flex justify-content-center">
                        <asp:LinkButton ID="lnkAcceptResolvedTicket" runat="server" CssClass="btn btn-success" OnClick="lnkAcceptResolvedTicket_Click">
                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-user-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" /><path d="M16 19h6" /><path d="M19 16v6" /><path d="M6 21v-2a4 4 0 0 1 4 -4h4" /></svg>
                    Accept Ticket
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkRejectResolvedTicket" runat="server" CssClass="btn btn-danger" OnClick="lnkRejectResolvedTicket_Click">
                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M18 6l-12 12" /><path d="M6 6l12 12" /></svg>
                            Reject Ticket
                        </asp:LinkButton>
                    </div>
            </div>
        </div>
    </div>
</div>

        <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdUserRejectProposedTicket">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Reject Proposed Ticket</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfUserRejectHeaderID" runat="server" />
                <div class="modal-body">
                    <asp:Label ID="lblRejectRemarks" runat="server" Text="Remarks" Placeholder="Input Remarks" CssClass="form-label status status-primary required"></asp:Label>
                    <asp:TextBox ID="txtRejectRemarks" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                    <asp:Label ID="lblRejectAttachment" runat="server" Text="Attachment" CssClass="form-label status status-primary required"></asp:Label>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:FileUpload ID="fuUploadAttachmentReject" CssClass="form-control mt-2" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-2">
                                        <asp:Label ID="Label10" runat="server" CssClass="form-label status status-primary required">Attachment Description:</asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtAttachmentDescReject" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvHDUploadedAttachmentReject" runat="server" AutoGenerateColumns="false" CssClass="table table-vcenter table-hover text-nowrap">
                        <Columns>
                            <asp:BoundField DataField="file_name" runat="server" HeaderText="File Name" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <%-- <asp:LinkButton ID="lnkDeleteAttachment" OnClick="lnkDeleteAttachment_Click" runat="server"><i class="ti ti-pencil">Edit</i></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Found
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <div class="modal-footer">
                        <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <asp:LinkButton ID="lnkUserRejectProposedSolution" runat="server" OnClick="lnkUserRejectProposedSolution_Click" OnClientClick="return validateRejectSolution();" CssClass="btn btn-success">Save as Reject Solution</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>


