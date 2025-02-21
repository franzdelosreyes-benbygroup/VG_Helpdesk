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


        function validateForm() {
            var natureOfProblem = document.getElementById('<%= ddlNatureofprobMd.ClientID %>').value;
            var priority = document.getElementById('<%= ddlPriorityMd.ClientID %>').value;
            var category = document.getElementById('<%= ddlCategoryMd.ClientID %>').value;
            var section = document.getElementById('<%= ddlSectionMd.ClientID %>').value;
                var subject = document.getElementById('<%= txtSubjectMd.ClientID %>').value.trim();
                var description = document.getElementById('<%= txtDescriptionMd.ClientID %>').value.trim();
            var requestId = '<%= Request.QueryString["Id"] != null ? Request.QueryString["Id"].ToString() : "" %>';

            if (natureOfProblem === "" || priority === "" || category === "" || section === "" ||
                subject === "" || description === "" || requestId === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }

            return true;
        }

        function validateDateFilter() {
           
        }

    </script>


</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="row" aria-labelledby="aspnetTitle">
        <h1><%=Page.Title %></h1>
    </section>
    <%--        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title"></h3>
                </div>
            </div>--%>


<div class="page-wrapper">
    <div class="page-body">
        <div class="card">
            <div class="card-header bg-red">
                    <ul class="nav nav-tabs card-header-tabs bg-red nav-fill" data-bs-toggle="tabs" role="tablist">
                        <li class="nav-item text-black" role="presentation">
                            <a class="nav-link active" data-bs-toggle="tab" href="#myCreatedTicket" aria-selected="false" role="tab">
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
                        <li class="nav-item text-black" role="presentation">
                            <a class="nav-link " data-bs-toggle="tab" href="#userTickets" aria-selected="false" role="tab">
                                <svg xmlns="http://www.w3.org/2000/svg" class="icon me-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none"></path><path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0"></path><path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2"></path></svg>
                                   Assign Ticket                    
                                </a>
                            </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link" data-bs-toggle="tab" href="#forReassignITPIC" aria-selected="false" role="tab" tabindex="-1">
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
                    <a class="nav-link" data-bs-toggle="tab" href="#assignedTickets" aria-selected="false" role="tab" tabindex="-1">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-list-check">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M3.5 5.5l1.5 1.5l2.5 -2.5" />
                            <path d="M3.5 11.5l1.5 1.5l2.5 -2.5" />
                            <path d="M3.5 17.5l1.5 1.5l2.5 -2.5" />
                            <path d="M11 6l9 0" />
                            <path d="M11 12l9 0" />
                            <path d="M11 18l9 0" />
                        </svg>
                            IT PIC Assigned Tickets
                    </a>
                </li>
                <li class="nav-item" role="presentation">
                    <a class="nav-link" data-bs-toggle="tab" href="#rejectedTicketAdmin" aria-selected="false" role="tab" tabindex="-1">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-xbox-x">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z" />
                            <path d="M9 8l6 8" />
                            <path d="M15 8l-6 8" />
                        </svg>
                            Admin Rejected Tickets to User
                        </a>
                        </li>
                </ul>
            </div>
            <div class="card-body">
            <div class="tab-content">
                <div class="tab-pane active show" id="myCreatedTicket" role="tabpanel">
                    <div class="col-md-12">
                        <div class="row">
                                <div class="col-md-12">
                                        <div class="row align-items-end"> 
                                        <div class="col-md-2 mb-3">
                                            <asp:Label ID="lblFilterByDateTicketFrom" runat="server" CssClass="form-label status status-primary">Creation Date From:</asp:Label>
                                            <asp:TextBox ID="txtFilterDateFrom" runat="server" CssClass="form-control text text-reset mt-2" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 mb-3">
                                            <asp:Label ID="lblFilterByDateTicketTo" runat="server" CssClass="form-label status status-primary">Creation Date To:</asp:Label>
                                            <asp:TextBox ID="txtFilterDateTo" runat="server" CssClass="form-control text text-reset mt-2" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 mb-3">
                                            <asp:Label ID="lblSearchTicket" runat="server" CssClass="form-label status status-primary">Search:</asp:Label>
                                            <asp:TextBox ID="txtSearchTicket" CssClass="form-control text text-reset mt-2" Placeholder="Search Ticket Code.." runat="server"></asp:TextBox>
                                        </div>
                                         <div class="col-md-2 mb-3 mt-5">
                                            <asp:LinkButton ID="lnkSearchTicket" OnClick="lnkSearchTicket_Click" CssClass="btn btn-primary" runat="server">+
                                                 <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  
                                                     class="icon icon-tabler icons-tabler-outline icon-tabler-search"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 10m-7 0a7 7 0 1 0 14 0a7 7 0 1 0 -14 0" /><path d="M21 21l-6 -6" />
                                                 </svg> Search Information                              
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-md-2 mb-3">
                                         <asp:Label ID="lblSearchByCreatedBy" runat="server" CssClass="form-label status status-primary">Filter Created By:</asp:Label>
                                            <asp:DropDownList ID="ddlEmployeeVg" CssClass="form-select text text-reset mt-2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 mb-3 mt-5">
                                            <asp:LinkButton ID="lnkSearchByCreatedBy" OnClick="lnkSearchByCreatedBy_Click" runat="server" CssClass="btn btn-primary">
                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-filter"><path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                                <path d="M4 4h16v2.172a2 2 0 0 1 -.586 1.414l-4.414 4.414v7l-6 2v-8.5l-4.48 -4.928a2 2 0 0 1 -.52 -1.345v-2.227z" />
                                            </svg>Filter By Created By:
                                            </asp:LinkButton>
                                        </div>
                                   </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row align-items-end">
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblSectionFilter" runat="server" CssClass="form-label status status-primary">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSectionFilter" runat="server" CssClass="form-select text text-reset mt-2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblCategoryFilter" runat="server" CssClass="form-label status status-primary">Category</asp:Label>
                                        <asp:DropDownList ID="ddlCategoryFilter" runat="server" CssClass="form-select text text-reset mt-2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <asp:Label ID="lblNatureOfProbFilter" runat="server" CssClass="form-label status status-primary">Nature Of Problem</asp:Label>
                                        <asp:DropDownList ID="ddlNatureOfProbFilter" runat="server" CssClass="form-select text text-reset mt-2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mb-3 mt-5">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 mt-2">
                        <asp:Label ID="lblMyCreatedTicket" runat="server" CssClass="h4" Text="My Created Tickets"></asp:Label>
                    </div>
                </div>

                <asp:GridView ID="gvMyTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanged="gvAdminTicketList_PageIndexChanged" EmptyDataTe="No Data Found">
                        <Columns>
                            <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                            <asp:BoundField DataField="status" HeaderText="Status" />
                            <asp:BoundField DataField="description_section" HeaderText="Section" />
                            <asp:BoundField DataField="description_category" HeaderText="Category" />
                            <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                            <asp:BoundField DataField="created_at" HeaderText="Created At" />
                            <asp:BoundField DataField="created_by" HeaderText="Created By" />
                            <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfTicketHeaderMyTicket" Value='<%# Eval("ticket_id")%>' runat="server" />
                                    <asp:HiddenField ID ="hfIsDraftId" Value='<%# Eval("approval_transactional_level") %>' runat="server" />
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

                <div class="tab-pane fade" id="userTickets" role="tabpanel">
                    <asp:Label ID="lblUserTicketsContent" runat="server" CssClass="h4" Text="User's tickets"></asp:Label>
                    <asp:GridView ID="gvAdminTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanged="gvAdminTicketList_PageIndexChanged" EmptyDataTe="No Data Found">
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
                        <asp:GridView ID="gvAdminForReassignTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanged="gvAdminForReassignTicketList_PageIndexChanged" EmptyDataTe="No Data Found">
                            <Columns>
                                <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                <asp:BoundField DataField="description_section" HeaderText="Section" />
                                <asp:BoundField DataField="description_category" HeaderText="Category" />
                                <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                <asp:BoundField DataField="assigned_emp_no_log" HeaderText="Previous IT PIC" />
                                <asp:BoundField DataField="itpic_recent_reject_remarks" HeaderText="IT PIC Reject Remarks" />
                                <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                <asp:BoundField DataField="created_for" HeaderText="Created For" />
                                <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfTicketHeaderIdReassignITPIC" runat="server" Value='<%# Eval("ticket_id")%>' />
                                        <asp:LinkButton ID="lnkDetailsReassignITPIC" OnClick="lnkDetailsReassignITPIC_Click" CssClass="btn btn-info w-50" runat="server">
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
                        <asp:GridView ID="gvAdminAssignedTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanged="gvAdminAssignedTicketList_PageIndexChanged" EmptyData="No Data Found">
                            <Columns>
                                <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                               <asp:BoundField DataField="description_section" HeaderText="Section" />
                               <asp:BoundField DataField="description_category" HeaderText="Category" />
                               <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                               <asp:BoundField DataField="created_at" HeaderText="Created At" />
                               <asp:BoundField DataField="created_by" HeaderText="Created At" />
                               <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfTicketHeaderIdAssignedTicketList" runat="server" Value='<%#Eval("ticket_id")%>' />
                                        <asp:LinkButton ID="lnkDetailsAssignedTicketList" OnClick="lnkDetailsAssignedTicketList_Click" CssClass="btn btn-info w-50" runat="server">
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
                        <asp:GridView ID="gvAdminForRejectedTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanged="gvAdminForRejectedTicketList_PageIndexChanged" EmptyDataTe="No Data Found">
                            <Columns>
                                <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                <asp:BoundField DataField="admin_recent_reject_remarks" HeaderText="Admin Reject Remarks" />
                                <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                <asp:BoundField DataField="created_by" HeaderText="Created At" /> 
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
                                    <asp:Label ID="lblCreatedBy" runat="server" CssClass="form-label status status-pink">Created By:</asp:Label>
                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control text-reset mt-2" Value='<%# Eval("created_by") %>' Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label class="form-label status status-pink">Created For:</label>
                                    <asp:TextBox ID="txtCreatedFor" runat="server" CssClass="form-control text-reset" Value='<%# Eval("created_for")%>' Enabled="true"></asp:TextBox>
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
                                    <asp:Label ID="lblassigntomd" runat="server" CssClass="form-label status status-pink">Assign To:</asp:Label>
                                    <asp:DropDownList ID="ddlAssignToEmpITMd" runat="server" CssClass="form-select text-reset mt-2">

                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label class="form-label status status-pink">Subject:</label>
                                <asp:TextBox ID="txtSubjectMd" runat="server" CssClass="form-control text-reset mt-2" Value='<%# Eval("subject") %>'></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label class="form-label status status-pink">Description:</label>
                                <asp:TextBox ID="txtDescriptionMd" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2" Value='<%# Eval("description")%>'></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12 mt-2">
                            <label class="form-label status status-pink">Others:</label>
                            <div class="mb-3">
                                <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control text-reset mt-2" Rows="5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="mb-3">
                                <label class="form-label status status-pink">Section</label>
                                <asp:DropDownList ID="ddlSectionMd" CssClass="form-select text-reset mt-2" OnSelectedIndexChanged="ddlSectionMd_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="mb-3">
                                <label class="form-label status status-pink">Category</label>
                                <asp:DropDownList ID="ddlCategoryMd" CssClass="form-select text-reset mt-2" OnSelectedIndexChanged="ddlCategoryMd_SelectedIndexChanged" AutoPostBack="true"  Enabled="false" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="mb-3">
                                <label class="form-label status status-pink">Nature Of Problem</label>
                            </div>
                            <asp:DropDownList ID="ddlNatureofprobMd" CssClass="form-select text-reset mb-3 mt-2" OnSelectedIndexChanged="ddlNatureofprobMd_SelectedIndexChanged" AutoPostBack="true"  Enabled="false" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <label class="form-label status status-pink">Attachment</label>
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
                                <div class="col-md-12 mt-2">
                                    <asp:Label ID="lblAttachNewAttachment" CssClass="form-label status status-pink mt-2" runat="server">Upload an Attachment</asp:Label>
                                </div>
                                <div class="col-md-12 mt-2">
                                    <asp:FileUpload ID="fuUploadAttachmentInEdit" CssClass="form-control" runat="server" />
                                </div>
                                <div class="mb-2 mt-2">
                                    <asp:Label ID="lblAttachmentDescription" runat="server" CssClass="form-label status status-pink">Attachment Description:</asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtAttachmentDescriptionMd" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="mb-3 mt-2">
                                <label class="form-label status status-pink">Priority Level</label>
                            </div>
                            <asp:DropDownList ID="ddlPriorityMd" CssClass="form-select text-reset mb-3" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
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

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdRejectTicketRemarks">
             <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Reject Ticket Remarks</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <asp:TextBox ID="txtRejectTicketRemarks" runat="server" CssClass="form-control text-area text-reset" TextMode="MultiLine" Rows="6"></asp:TextBox>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                <asp:LinkButton ID="lnkMdRejectTicketAdminToUser" runat="server" OnClick="lnkMdRejectTicketAdminToUser_Click" CssClass="btn btn-danger">Reject Ticket</asp:LinkButton>
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
                    <asp:LinkButton ID="lnkAssignITPICTicket" runat="server" OnClick="lnkAssignITPICTicket_Click" CssClass="btn btn-success">Save changes</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>


</asp:Content>


