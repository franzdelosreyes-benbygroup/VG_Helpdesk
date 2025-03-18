<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/IT_PIC_Portal/IT_PIC.Master" CodeBehind="Dashboard.aspx.cs" Inherits="HelpDeskVG.IT_PIC_Portal.Dashboard" %>


<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">

        <title>Dashboard</title>

        <link href="../dist/css/select2.css" rel="stylesheet" />
    <script src="../dist/js/select2.min.js"></script>


</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

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

        function showTicketHistory3rdPtLogs() {
            $(document).ready(function () {
                $("#mdTicketThirdPartyHistory").modal("show");
            });
        }


        function rejectITPICTicketRemarks() {
            $(document).ready(function () {
                $("#mdITPICRejectTicketRemarks").modal('show');
            });
        }

        function acceptTicketThirdParty() {
            $(document).ready(function () {
                $("#mdITPICAcceptTicket3rdParty").modal('show');
            });
        }

        function resolvedTicket() {
            $(document).ready(function () {
                $("#mdITPICProposedSolution").modal('show');
            });
        }

        function resolvedTicketAgain() {
            $(document).ready(function () {
                $("#mdITPICProposedSolutionAgain").modal('show');
            });
        }

        function rejectedResolvedTicket() {
            $(document).ready(function () {
                $("#mdUSERRejectedSolution").modal('show');
            });
        }

        function reject3rdPartyITPIC() {
            $(document).ready(function () {
                $("#mdITPICRejected3rdParty").modal('show');
            });
        }


        function received3rdPartyITPIC() {
            $(document).ready(function () {
                $("#mdITPICReceivedTicket3rdParty").modal('show');
            });
        }

        function validateDateReceivedThirdParty() {
            $(document).ready(function () {
                $("#mdValidateReceivedThirdParty").modal('show');
            });
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

            // Listen for tab changes and update localStorage
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

                return confirm("Do you wish to proceed?");
        }

        function validateResolveForm() {
            var remarks = document.getElementById('<%= txtRemarksProposedSolution.ClientID%>').value;

            if (remarks === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }

            return confirm("Do want to proceed?");
        }

        function validateResolveAgainForm() {
                var remarks = document.getElementById('<%= txtRemarksProposedSolutionAgain.ClientID%>').value;

                if (remarks === "") {
                    alert("Please fill up the field that is Required.");
                    return false;
                }

                return confirm("Do you want to proceed?");
        }

        function validateReject3rdPartySolution() {
            var thirdptgiven = document.getElementById('<%= hfMdTicketDateGiven3rdParty.ClientID %>').value;
            var daterejected3rdpartysolution = document.getElementById('<%= txt3rdPartyRejectedDate.ClientID%>').value;

            var date = new Date(daterejected3rdpartysolution);
            date.setHours(23, 59, 0, 0); // Set end of the day to avoid timezone issues
            var rejectformattedDate = date.toISOString().slice(0, 16).replace('T', ' ');

            var givenDate = new Date(thirdptgiven);
            givenDate.setHours(23, 59, 0, 0);
            var givenformattedDate = givenDate.toISOString().slice(0, 16).replace('T', ' ');

            if (rejectformattedDate < givenformattedDate) {
                alert("The received date cannot be earlier than the date given.");
                document.getElementById('<%= txt3rdPartyRejectedDate.ClientID%>').value = "";
                return false;
            }

            return confirm("Do you want to proceed?");
        }

        function validateRejectTicketToUser() {
            var remarks = document.getElementById('<%= txtITPICRejectTicketRemarks.ClientID%>').value;

            if (remarks === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }

            return confirm("Do you want to reject ticket?");
        }

        function validateReceivedDateWith3rdPt() {
            var thirdptgiven = document.getElementById('<%= hfMdTicketDateGiven3rdParty.ClientID %>').value;
            var datereceivedto3rdpt = document.getElementById('<%= txt3rdPtReceivedDate.ClientID%>').value;

            var date = new Date(datereceivedto3rdpt);
            date.setHours(23, 59, 0, 0); // Set end of the day to avoid timezone issues
            var receivedformattedDate = date.toISOString().slice(0, 16).replace('T', ' ');

            var givenDate = new Date(thirdptgiven);
            givenDate.setHours(23, 59, 0, 0);
            var givenformattedDate = givenDate.toISOString().slice(0, 16).replace('T', ' ');

            if (receivedformattedDate < givenformattedDate) {
                alert("The received date cannot be earlier than the date given.");
                document.getElementById('<%= txt3rdPtReceivedDate.ClientID%>').value = "";
                return false;
            }

            return confirm("Do you want to proceed?");
        }


        function CheckDateIfFutureGiven3rdPt(txtinput) {

            var thirdptname = document.getElementById('<%= txt3rdPartyName.ClientID%>').value;
            var dategivento3rdpt = document.getElementById('<%= txtCalendarGivenTo.ClientID%>').value;

            if (thirdptname === "" || dategivento3rdpt === "") {
                alert("Please fill up the required fields.");
                return false;
            }

            var _input = document.getElementById(txtinput);
            var _num = _input.value;


            var _newNum = new Date(_num);
            var _dateNow = new Date();

            var _dateNowNew = new Date();
            var _getDateNow;
            if (_dateNowNew.getMonth() < 10 && _dateNowNew.getDate() < 10) {
                _getDateNow = _dateNowNew.getFullYear() + '-' + '0' + (_dateNowNew.getMonth() + 1) + '-' + '0' + _dateNowNew.getDate();
            }
            else if (_dateNowNew.getMonth() < 10 && _dateNowNew.getDate() >= 10) {
                _getDateNow = _dateNowNew.getFullYear() + '-' + '0' + (_dateNowNew.getMonth() + 1) + '-' + _dateNowNew.getDate();
            }

            if (_newNum > _dateNow) {
                alert("Select Future Date is not allowed.");
                _input.value = "";
                return false;
            }
            return false;
        }

        function validateRejectSolution() {
            var remarks = document.getElementById('<%= txtRejectRemarks.ClientID%>').value;

            if (remarks === "") {
                alert("Please fill up the field that is Required.");
                return false;
            }
            return confirm("Do you want to proceed?");
        }

        $(document).ready(function () {
            $('.custom-select').select2({ width: '100%' });
        });

    </script>

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
                            <a class="nav-link tab-container active" data-bs-toggle="tab" href="#myCreatedTicket" aria-selected="false" role="tab">
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
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#acceptorrejectTicketsITPIC" aria-selected="false" role="tab">
                                <span id="ticketMyPendingTicketsToAccept" class="custom-count">                                
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-file-like">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M3 16m0 1a1 1 0 0 1 1 -1h1a1 1 0 0 1 1 1v3a1 1 0 0 1 -1 1h-1a1 1 0 0 1 -1 -1z" />
                                    <path d="M6 20a1 1 0 0 0 1 1h3.756a1 1 0 0 0 .958 -.713l1.2 -3c.09 -.303 .133 -.63 -.056 -.884c-.188 -.254 -.542 -.403 -.858 -.403h-2v-2.467a1.1 1.1 0 0 0 -2.015 -.61l-1.985 3.077v4z" />
                                    <path d="M14 3v4a1 1 0 0 0 1 1h4" />
                                    <path d="M5 12.1v-7.1a2 2 0 0 1 2 -2h7l5 5v11a2 2 0 0 1 -2 2h-2.3" />
                                </svg>
                                    <asp:Label ID="lblITPICPendingTicketsToAcceptCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-file-like">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M3 16m0 1a1 1 0 0 1 1 -1h1a1 1 0 0 1 1 1v3a1 1 0 0 1 -1 1h-1a1 1 0 0 1 -1 -1z" />
                                    <path d="M6 20a1 1 0 0 0 1 1h3.756a1 1 0 0 0 .958 -.713l1.2 -3c.09 -.303 .133 -.63 -.056 -.884c-.188 -.254 -.542 -.403 -.858 -.403h-2v-2.467a1.1 1.1 0 0 0 -2.015 -.61l-1.985 3.077v4z" />
                                    <path d="M14 3v4a1 1 0 0 0 1 1h4" />
                                    <path d="M5 12.1v-7.1a2 2 0 0 1 2 -2h7l5 5v11a2 2 0 0 1 -2 2h-2.3" />
                                </svg>
                                Pending Tickets To Accept                     
                                </a>
                            </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#acceptedTicketsITPIC" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketAcceptedTicketCount" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-circle-check">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0" />
                                        <path d="M9 12l2 2l4 -4" />
                                    </svg>
                                    <asp:Label ID="lblAcceptedTicketCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-circle-check">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0" />
                                    <path d="M9 12l2 2l4 -4" />
                                </svg>
                                Accepted Tickets
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#rejectedTicketITPIC" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketITPICRejectedToAdminTicket" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-x">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                        <path d="M6 21v-2a4 4 0 0 1 4 -4h3.5" />
                                        <path d="M22 22l-5 -5" />
                                        <path d="M17 22l5 -5" />
                                    </svg>
                                    <asp:Label ID="lblITPICRejectedTicketToAdminCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-user-x">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                    <path d="M6 21v-2a4 4 0 0 1 4 -4h3.5" />
                                    <path d="M22 22l-5 -5" />
                                    <path d="M17 22l5 -5" />
                                </svg>
                                IT PIC Rejected Ticket
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link tab-container" data-bs-toggle="tab" href="#rejectedSolutionByUser" aria-selected="false" role="tab" tabindex="-1">
                                <span id="ticketITPICRejectedSolutionByUser" class="custom-count">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-xbox-x">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z" />
                                        <path d="M9 8l6 8" />
                                        <path d="M15 8l-6 8" />
                                    </svg>
                                    <asp:Label ID="lblITPICRejectedSolutionByUserCount" runat="server" CssClass="ticket-number"></asp:Label>
                                </span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-xbox-x">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z" />
                                    <path d="M9 8l6 8" />
                                    <path d="M15 8l-6 8" />
                                </svg>
                                Rejected Proposed Solution
                            </a>
                        </li>
                    </ul>
            </div>
            <div class="card-body">
                <div class="tab-content">
                    <div class="tab-pane active show" id="myCreatedTicket" role="tabpanel">
                        <asp:Label ID="lblMyCreatedTicket" runat="server" CssClass="h4" Text="Ny Created Tickets"></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="gvMyTicketList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMyTicketList_PageIndexChanging" EmptyDataTe="No Data Found">
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
                                    <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                    <asp:BoundField DataField="description_section" HeaderText="Section" />
                                    <asp:BoundField DataField="description_category" HeaderText="Category" />
                                    <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />      
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderMyTicket" Value='<%# Eval("ticket_id")%>' runat="server" />
                                            <asp:HiddenField ID="hfIsDraftId" Value='<%# Eval("approval_transactional_level") %>' runat="server" />
                                            <asp:LinkButton ID="lnkDetailsMyTicket" OnClick="lnkDetailsMyTicket_Click" CssClass="btn btn-info" runat="server">
                                             <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                       View Details
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkViewHistory" runat="server" Visible='<%# (Eval("is_draft").ToString() != "True" ? true : false) %>' OnClick="lnkViewHistory_Click" CssClass="btn btn-warning position-relative w-50">
                                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  
                                                        class="icon icon-tabler icons-tabler-outline icon-tabler-history">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 8l0 4l2 2" />
                                                        <path d="M3.05 11a9 9 0 1 1 .5 4m-.5 5v-5h5" />
                                                    </svg>
                                                View History
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkDeleteDraft" runat="server" ToolTip="Delete Draft" Visible='<%# (Eval("is_draft").ToString() == "True" ? true : false)%>' CommandArgument='<%# Eval("ticket_id")%>' OnClick="lnkDeleteDraft_Click" CssClass="btn btn-danger position--relative w-50">
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
                            <asp:GridView ID="gvMyTicketPendingApproval" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMyTicketPendingApproval_PageIndexChanging" EmptyDataTe="No Data Found">
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
                                    <asp:BoundField DataField="created_for" HeaderText="Ticket Owner" />
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
                        <asp:Label ID="Label9" runat="server" CssClass="h4" Text="Rejected Tickets due to incomplete details."></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="gvMyTicketRejectedByAdmin" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMyTicketRejectedByAdmin_PageIndexChanging" EmptyDataTe="No Data Found">
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
                                    <asp:BoundField DataField="admin_recent_reject_remarks" HeaderText="Admin Reject Remarks" />
                                    <asp:BoundField DataField="admin_rejector" HeaderText="Admin Disapprover" />
                                    <asp:BoundField DataField="created_at" HeaderText="Rejected At" />
                                    <asp:BoundField DataField="created_for" HeaderText="Ticket Owner" />
                                    <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderIdRejectedList" runat="server" Value='<%#Eval("ticket_id") %>' />
                                            <asp:LinkButton ID="lnkMyTicketRejectedTicketList" OnClick="lnkMyTicketRejectedTicketList_Click" CssClass="btn btn-info" runat="server">
                                   <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                      View Details</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="acceptorrejectTicketsITPIC" role="tabpanel">
                        <asp:Label ID="lblITPIC" runat="server" CssClass="h4" Text="Accept Or Reject Tickets"></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="gvITPICAcceptOrRejectList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvITPICAcceptOrRejectList_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                                    <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
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
                                    <asp:BoundField DataField="created_for" HeaderText="Ticket Owner" />
                                    <asp:BoundField DataField="priority_level" HeaderText="Priority Level" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfTicketHeaderIdITPIC" Value='<%#Eval("ticket_id")%>' runat="server" />
                                            <asp:LinkButton ID="lnkDetailsUserListTicket" OnClick="lnkDetailsUserListTicket_Click" CssClass="btn btn-info" runat="server">
                                              <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                                 Ticket Details
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="tab-content">
                        <div class="tab-pane fade" id="acceptedTicketsITPIC" role="tabpanel">
                            <asp:Label ID="lblForReassignITPIC" runat="server" CssClass="h4" Text="Accepted Tickets."></asp:Label>
                          <div class="table-responsive">
                            <asp:GridView ID="gvITPICAcceptedTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvITPICAcceptedTickets_PageIndexChanging" EmptyDataTe="No Data Found">
                                <Columns>
                               <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                    <asp:TemplateField HeaderText="Priority">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server"
                                                Text='<%# Eval("priority_level") %>'
                                                Style='<%# "color: " + Eval("color_code") %>' Font-Bold="true">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="third_party_name" HeaderText="Third Party" />
                                <asp:BoundField DataField="third_party_date_given" HeaderText="Given Date to 3rd Party" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />  
                                <asp:BoundField DataField="third_party_date_received" HeaderText="Date Received 3rd Party" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />  
                                <asp:BoundField DataField="description_section" HeaderText="Section" />
                                <asp:BoundField DataField="description_category" HeaderText="Category" />
                                <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                <asp:BoundField DataField="created_for" HeaderText="Ticket Owner" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfThirdPtDateGiven" runat="server" Value='<%# Eval("third_party_date_given")%>' />
                                            <asp:HiddenField ID="hfTicketHeaderIdAcceptedTicket" runat="server" Value='<%# Eval("ticket_id")%>' />
                                            <asp:LinkButton ID="lnkAcceptedTicketDetails" OnClick="lnkAcceptedTicketDetails_Click" CssClass="btn btn-info" runat="server">
                                             <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                                 <path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                                Ticket Details
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkViewHistory3rdParty" runat="server" OnClick="lnkViewHistory3rdParty_Click" CssClass="btn btn-warning position-relative w-50">
                                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  
                                                        class="icon icon-tabler icons-tabler-outline icon-tabler-history">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 8l0 4l2 2" />
                                                        <path d="M3.05 11a9 9 0 1 1 .5 4m-.5 5v-5h5" />
                                                    </svg>
                                                History 3rd Party Logs
                                                
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                              </div>
                        </div>
                        <div class="tab-pane fade" id="rejectedTicketITPIC" role="tabpanel">
                            <asp:Label ID="Label1" runat="server" CssClass="h4" Text="Rejected Tickets due to incomplete details."></asp:Label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvITPICRejectedTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvITPICRejectedTickets_PageIndexChanging" EmptyDataTe="No Data Found">
                                    <Columns>
                                        <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                        <asp:BoundField DataField="third_party_name" HeaderText="Third Party" />
                                        <asp:BoundField DataField="third_party_date_given" HeaderText="Given Date to 3rd Party" />
                                        <asp:BoundField DataField="third_party_date_received" HeaderText="Date Received 3rd Party" />
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
                                        <asp:BoundField DataField="created_for" HeaderText="Ticket Owner" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfTicketHeaderIdITPICRejectedList" runat="server" Value='<%#Eval("ticket_id") %>' />
                                                <asp:LinkButton ID="lnkDetailsRejectedTicketList" OnClick="lnkDetailsRejectedTicketList_Click" CssClass="btn btn-info" runat="server">
                                                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>
                                               Ticket Details</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="rejectedSolutionByUser" role="tabpanel">
                            <asp:Label ID="Label2" runat="server" CssClass="h4" Text="Rejected Solution"></asp:Label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvRejectedSolution" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvRejectedSolution_PageIndexChanging" EmptyDataTe="No Data Found">
                                    <Columns>
                                        <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                        <asp:BoundField DataField="third_party_name" HeaderText="Third Party" />
                                        <asp:BoundField DataField="third_party_date_given" HeaderText="Given Date to 3rd Party" />
                                        <asp:BoundField DataField="third_party_date_received" HeaderText="Received Date from 3rd Party" />
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
                                        <asp:BoundField DataField="user_recent_rejected_solution_remarks" HeaderText="Reason Rejected" />
                                        <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                        <asp:BoundField DataField="created_for" HeaderText="Created By" />

                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfTicketHeaderIdITPICRejectedSolution" runat="server" Value='<%#Eval("ticket_id") %>' />
                                                <asp:LinkButton ID="lnkUserRejectProposalDetails" OnClick="lnkUserRejectProposalDetails_Click" runat="server" CssClass=""></asp:LinkButton>
                                                <asp:LinkButton ID="lnkUserRejectRemarks" runat="server" OnClick="lnkUserRejectRemarks_Click" CssClass="btn btn-info">
                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-eye"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" /><path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" /></svg>

                                                 Reject Details</asp:LinkButton>
                                                <%-- <asp:LinkButton ID="lnkDetailsRejectedTicketList" OnClick="lnkDetailsRejectedTicketList_Click" CssClass="btn btn-info w-25" runat="server">Details</asp:LinkButton>--%>
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
</div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdDetailsUsersTicket">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Ticket Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfMdTicketHeaderId" runat="server" />
                <asp:HiddenField ID="hfMdTicketDateGiven3rdParty" runat="server" />
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label class="form-label status status-primary">Created By:</label>
                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control text-reset mt-2" Value='<%# Eval("created_by") %>' Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label class="form-label status status-primary">Created For:</label>
                                    <asp:TextBox ID="txtCreatedFor" runat="server" CssClass="form-control text-reset mt-2" Visible="false" Value='<%# Eval("created_for")%>' Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlCreatedForMd" runat="server" CssClass="form-select text-reset mt-2" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label class="form-label status status-primary required-label">Subject:</label>
                                <asp:TextBox ID="txtSubjectMd" runat="server" CssClass="form-control text-reset mt-2" Enabled="false" Value='<%# Eval("subject") %>'></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label class="form-label status status-primary required-label">Description:</label>
                                <asp:TextBox ID="txtDescriptionMd" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2" Enabled="false" Value='<%# Eval("description")%>'></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="mb-3">
                                <label class="form-label status status-primary required-label">Section</label>
                                <asp:DropDownList ID="ddlSectionMd" CssClass="form-select text-reset mt-2" AutoPostBack="true" OnSelectedIndexChanged="ddlSectionMd_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="mb-3">
                                <label class="form-label status status-primary required-label">Category</label>
                                <asp:DropDownList ID="ddlCategoryMd" CssClass="form-select text-reset mt-2" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryMd_SelectedIndexChanged" Enabled="false" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="mb-3">
                                <label class="form-label status status-primary required-label">Nature Of Problem</label>
                            </div>
                            <asp:DropDownList ID="ddlNatureofprobMd" CssClass="form-select mt-2" AutoPostBack="true" OnSelectedIndexChanged="ddlNatureofprobMd_SelectedIndexChanged" Enabled="false" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <label class="form-label status status-primary mt-2 mb-2">Attachment</label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvDownloadableAttachment" runat="server" CssClass="table table-hover table-bordered table-striped no-wrap" GridLines="None" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="file_name" HeaderText="File Name" />
                                        <asp:BoundField DataField="created_at" HeaderText="Created At" />                                        
                                        <asp:BoundField DataField="description_attachment" HeaderText="Attachment Description" />
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
                                    <asp:Label ID="lblAttachDesccc" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtAttachmentDescriptionMd" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                </div>
                                <div class="col-md-12 mt-2">
                                    <asp:Label ID="lblAttachNewAttachment" CssClass="form-label status status-primary mt-2" runat="server">Upload an Attachment</asp:Label>
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
                                <label class="form-label status status-primary required-label">Priority Level</label>
                            </div>
                            <asp:DropDownList ID="ddlPriorityMd" CssClass="form-select text-reset" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 modal-footer d-block">
                    <div class="row d-flex flex-wrap gap-2">
                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkAcceptTicket" runat="server" CssClass="btn w-100 btn-block btn-success" OnClick="lnkAcceptTicket_Click">
                                <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>                               
                                Accept Ticket
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkRejectTicketUser" runat="server" CssClass="btn btn-block btn-danger w-100" OnClick="lnkRejectTicketUser_Click">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M18 6l-12 12" /><path d="M6 6l12 12" /></svg>
                                        Reject Ticket
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkAcceptWithThirdParty" CssClass="btn w-100 btn-block btn-success" OnClick="lnkAcceptWithThirdParty_Click" runat="server">
                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  
                                            stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-users-plus">
                                            <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M5 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" />
                                            <path d="M3 21v-2a4 4 0 0 1 4 -4h4c.96 0 1.84 .338 2.53 .901" /><path d="M16 3.13a4 4 0 0 1 0 7.75" /><path d="M16 19h6" />
                                            <path d="M19 16v6" /></svg>
                                            Accept w/ 3rd Party
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkEditDetails" runat="server" CssClass="btn w-100 btn-block btn-primary" OnClick="lnkEditDetails_Click" OnClientClick="return validateForm();">
                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" /><path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                                                Save Edited Details
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkITPICReject3rdParty" runat="server" CssClass="btn w-100 btn-block btn-red" Visible="false" OnClick="lnkITPICReject3rdParty_Click">
                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-flag-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M13.533 15.028a4.988 4.988 0 0 1 -1.533 -1.028a5 5 0 0 0 -7 0v-9a5 5 0 0 1 7 0a5 5 0 0 0 7 0v8.5" /><path d="M5 21v-7" /><path d="M22 22l-5 -5" /><path d="M17 22l5 -5" /></svg>
                                    Reset 3rd Party Information
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkTagThisToThirdParty" runat="server" CssClass="btn w-100 btn-blue btn-success" OnClick="lnkTagThisToThirdParty_Click">
                                           <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-users-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M5 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" /><path d="M3 21v-2a4 4 0 0 1 4 -4h4c.96 0 1.84 .338 2.53 .901" /><path d="M16 3.13a4 4 0 0 1 0 7.75" /><path d="M16 19h6" /><path d="M19 16v6" /></svg>
                                                Update Tag to Third Party
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkSaveReceivedDate" runat="server" CssClass="btn w-100 btn-block btn-indigo" OnClick="lnkSaveReceivedDate_Click">
                                               <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-navigation-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M16.573 12.914l-4.573 -9.914l-7.97 17.275c-.07 .2 -.017 .424 .135 .572c.15 .148 .374 .193 .57 .116l7.265 -2.463" /><path d="M16 19h6" /><path d="M19 16v6" /></svg>
                                                Save 3rd Party Received
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="d-grid gap-2 d-md-flex">
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkProposedTicketResolution" runat="server" CssClass="btn w-100 btn-block btn-info" OnClick="lnkProposedTicketResolution_Click">
                                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>                               
                                                Save as Resolved
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdITPICAcceptTicket3rdParty">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Accept Ticket w/ Third Party</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblThirdPtName" runat="server" Text="Third Party Name" Placeholder="Insert 3rd Party Name" CssClass="form-label status status-primary required"></asp:Label>
                    <asp:TextBox ID="txt3rdPartyName" runat="server" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                    <asp:Label ID="lblCalender3rdPt" runat="server" Text="Given Date to 3rd Party" CssClass="form-label status status-primary required mt-2"></asp:Label>
                    <asp:TextBox ID="txtCalendarGivenTo" CssClass="form-control mt-2" TextMode="Date" onchange="javascript:CheckDateIfFutureGiven3rdPt(this.id);" placeholder="Select a date" runat="server"></asp:TextBox>
                    <script type="text/javascript">
                        // Automatically set the maximum date to today
                        var txtBox = document.getElementById('<%= txtCalendarGivenTo.ClientID %>');
                        var today = new Date().toISOString().split('T')[0];
                        txtBox.max = today;
                    </script>

                    <div class="modal-footer">
                        <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <asp:LinkButton ID="lnkAcceptThirdPartMd" runat="server" OnClick="lnkAcceptThirdPartMd_Click" OnClientClick="return CheckDateIfFutureGiven3rdPt();" CssClass="btn btn-success">Accept Ticket</asp:LinkButton>
                        <asp:LinkButton ID="lnkUpdateToThirdPartyMd" runat="server" OnClick="lnkUpdateToThirdPartyMd_Click" OnClientClick="return CheckDateIfFutureGiven3rdPt();" CssClass="btn btn-success">Update Tag to Third Party</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdITPICReceivedTicket3rdParty">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Accept Ticket w/ Third Party</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label12" runat="server" Text="Received Date from 3rd Party" CssClass="form-label status status-primary required mb-2 mt-2"></asp:Label>
                    <asp:TextBox ID="txt3rdPtReceivedDate" runat="server" CssClass="form-control text-reset mt-2" TextMode="Date" Placeholder="Select a Date"></asp:TextBox>
                    <script type="text/javascript">
                        // Automatically set the maximum date to today
                        var txtBox = document.getElementById('<%= txt3rdPtReceivedDate.ClientID %>');
                        var today = new Date().toISOString().split('T')[0];
                        txtBox.max = today;
                     </script>
                    
                    <div class="modal-footer">
                        <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <asp:LinkButton ID="lnkSaveDateReceived3rdPt" runat="server" OnClick="lnkSaveDateReceived3rdPt_Click" OnClientClick="return validateReceivedDateWith3rdPt();" CssClass="btn btn-success">Save Received Ticket</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdITPICRejectTicketRemarks">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Reject Ticket Remarks</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtITPICRejectTicketRemarks" runat="server" CssClass="form-control text-area text-reset" TextMode="MultiLine" Rows="6"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkMdITPICRejectTicketToAdmin" runat="server" OnClick="lnkMdITPICRejectTicketToAdmin_Click" OnClientClick="return validateRejectTicketToUser();" CssClass="btn btn-danger">Reject Ticket</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

  <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdITPICProposedSolution">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Proposed Solution</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfHeaderTicketProposed" runat="server" />
                <div class="modal-body">
                    <asp:Label ID="lblRemarksProposed" runat="server" Text="Remarks" Placeholder="Input Remarks" CssClass="form-label status status-primary required-label"></asp:Label>
                    <asp:TextBox ID="txtRemarksProposedSolution" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                    <asp:Label ID="lblAttachmentProposed" runat="server" Text="Attachment Solution" CssClass="form-label status status-primary mt-2"></asp:Label>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:FileUpload ID="fuUploadAttachment" CssClass="form-control mt-2" runat="server" />
                            </div>
                   <%--         <div class="col-md-6">
                                <asp:LinkButton ID="lnkUploadAttachment" CssClass="btn btn-info" OnClick="lnkUploadAttachment_Click" AutoPostBack="false" runat="server">Upload Attachment</asp:LinkButton>
                            </div>--%>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-2">
                                        <asp:Label ID="lblAttachmentDescription" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtAttachmentDescription" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <%--<asp:LinkButton ID="lnkResolveAgain" runat="server" OnClick="lnkResolveAgain_Click" CssClass="btn btn-success">Save Resolved</asp:LinkButton>--%>
                        <asp:LinkButton ID="lnkSaveProposedSolution" runat="server" OnClick="lnkSaveProposedSolution_Click" OnClientClick="return validateResolveForm();" CssClass="btn btn-success">Save Resolved</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

      <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdITPICProposedSolutionAgain">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Proposed Solution</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfHeaderTicketProposedAgain" runat="server" />
                <div class="modal-body">
                    <asp:Label ID="Label3" runat="server" Text="Remarks:" Placeholder="Input Remarks" CssClass="form-label status status-primary required"></asp:Label>
                    <asp:TextBox ID="txtRemarksProposedSolutionAgain" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="Attachment:" CssClass="form-label status status-primary mt-2"></asp:Label>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:FileUpload ID="fuUploadAttachmentAgain" CssClass="form-control mt-2" runat="server" />
                            </div>
                   <%--         <div class="col-md-6">
                                <asp:LinkButton ID="lnkUploadAttachment" CssClass="btn btn-info" OnClick="lnkUploadAttachment_Click" AutoPostBack="false" runat="server">Upload Attachment</asp:LinkButton>
                            </div>--%>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-2">
                                        <asp:Label ID="Label7" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtAttachmentDescriptionAgain" runat="server" CssClass="form-control text-reset mt-2" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <%--<asp:LinkButton ID="lnkResolveAgain" runat="server" OnClick="lnkResolveAgain_Click" CssClass="btn btn-success">Save Resolved</asp:LinkButton>--%>
                        <asp:LinkButton ID="lnkSaveAsResolvedAgain" runat="server" OnClick="lnkSaveAsResolvedAgain_Click" OnClientClick="return validateResolveAgainForm();" CssClass="btn btn-success">Save as Resolved</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>



  <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdUSERRejectedSolution">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Rejected Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfTicketHeaderUserReject" runat="server" />

                <div class="modal-body">
                    <asp:Label ID="lblRemarks" runat="server" Text="Reject Remarks" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtRejectProposedRemarks" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="Attachment" CssClass="form-label mt-2"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="gvUserRejectSolutionAttachment" runat="server" AutoGenerateColumns="false" CssClass="table table-vcenter table-hover text-nowrap">
                            <Columns>
                                <asp:BoundField DataField="file_name" runat="server" HeaderText="File Name" />
                                <asp:BoundField DataField="created_at" runat="server" HeaderText="Rejected At" />
                                <asp:BoundField DataField="description_attachment" HeaderText="Reject Remarks" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <%--                                    <asp:LinkButton ID="lnkDeleteAttachment" OnClick="lnkDeleteAttachment_Click" runat="server"><i class="ti ti-pencil">Edit</i></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkDownloadFileRejectAttachment" OnClick="lnkDownloadFileRejectAttachment_Click" CommandArgument='<%# Eval("attachment_id") %>' Text="Download" CssClass="btn btn-success"
                                            runat="server"> <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  
                                            stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-download">
                                            <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" /><path d="M7 11l5 5l5 -5" />
                                            <path d="M12 4l0 12" /></svg>
                                        Download
                                        </asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-2">
                                        <asp:Label ID="Label5" runat="server" Visible="false" CssClass="form-label">Attachment Description:</asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtAttachmentRejectDesc" runat="server" CssClass="form-control text-reset" TextMode="MultiLine" Rows="3" Visible="false" placeholder="Attachment Description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <asp:LinkButton ID="lnkResolveAgain" runat="server" OnClick="lnkResolveAgain_Click" CssClass="btn btn-success">Save Resolved</asp:LinkButton>
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
                                            <asp:BoundField DataField="created_at" HeaderText="Attachment Description" />
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
                                            <asp:TextBox ID="txtDescriptionAttachmentProposed" runat="server" TextMode="MultiLine" Visible="false" Rows="6" CssClass="form-control text-area text-reset" Value='<%# Eval("description_attachment")%>'></asp:TextBox>
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
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Reject Solution</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <asp:HiddenField ID="hfUserRejectHeaderID" runat="server" />
                <div class="modal-body">
                    <asp:Label ID="lblRejectRemarks" runat="server" Text="Remarks" Placeholder="Input Remarks" CssClass="form-label status status-primary required"></asp:Label>
                    <asp:TextBox ID="txtRejectRemarks" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                    <asp:Label ID="lblRejectAttachment" runat="server" Text="Attachment" CssClass="form-label status status-primary mt-2"></asp:Label>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:FileUpload ID="fuUploadAttachmentReject" CssClass="form-control mt-2" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-2">
                                        <asp:Label ID="Label10" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
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

    <div class="modal modal-blur fade" tabindex="-1" aria-modal="true" role="dialog" id="mdValidateReceivedThirdParty">
        <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
            <div class="modal-content">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                <div class="modal-status bg-danger"></div>
                <div class="modal-body text-center py-4">
                    <!-- Download SVG icon from http://tabler.io/icons/icon/alert-triangle -->
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon mb-2 text-danger icon-lg">
                        <path d="M12 9v4"></path>
                        <path d="M10.363 3.591l-8.106 13.534a1.914 1.914 0 0 0 1.636 2.871h16.214a1.914 1.914 0 0 0 1.636 -2.87l-8.106 -13.536a1.914 1.914 0 0 0 -3.274 0z"></path>
                        <path d="M12 16h.01"></path>
                    </svg>
                    <h3>Warning</h3>
                    <div class="text-secondary">Please Save the "Third Party Resolved Received Date" first!</div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="mdITPICRejected3rdParty">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">Reject 3rd Party</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <asp:Label ID="Label8" runat="server" Text="Rejected Date from 3rd Party:" CssClass="form-label status status-primary required mb-2 mt-2"></asp:Label>
                        <asp:TextBox ID="txt3rdPartyRejectedDate" runat="server" CssClass="form-control text-reset mt-2" TextMode="Date" Placeholder="Select a Date"></asp:TextBox>
                    </div>
                    <script type="text/javascript">
                        // Automatically set the maximum date to today
                        var txtBox = document.getElementById('<%= txt3rdPartyRejectedDate.ClientID %>');
                        var today = new Date().toISOString().split('T')[0];
                        txtBox.max = today;
                    </script>
                    <div class="col-md-12">
                        <asp:Label ID="lbl3rdptrejectreason" runat="server" CssClass="form-label status status-primary required mt-2">Remarks:</asp:Label>
                        <asp:TextBox ID="txt3rdPtRejectReason" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control text-area text-reset mt-2"></asp:TextBox>
                        <div class="modal-footer">
                            <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                            <asp:LinkButton ID="lnkReject3rdParty" runat="server" CssClass="btn btn-danger" OnClientClick="return validateReject3rdPartySolution();" OnClick="lnkReject3rdParty_Click">Reject 3rd Party</asp:LinkButton>
                            <%--<asp:LinkButton ID="lnkReject3rdPartySolution" runat="server" CssClass="btn btn-danger" OnClientClick=" return validateReject3rdPartySolution();" OnClick="lnkReject3rdPartySolution_Click">Reject 3rd Party Solution</asp:LinkButton>--%>
                        </div>
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

        <div class="modal modal-blur fade show" tabindex="-1" aria-modal="true" role="dialog" id="mdTicketThirdPartyHistory">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h5 class="modal-title">3rd Party History</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    <asp:HiddenField ID="hfTicketHeaderForHistory3rdPtLog" runat="server" />
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvThirdPartyLogsHistory" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4">
                            <Columns>
                                <asp:BoundField DataField="ticket_code" HeaderText="Ticket Code" />
                                <asp:BoundField DataField="third_party_name" HeaderText="Third Party Name" />
                                <asp:BoundField DataField="ticket_given_date" HeaderText="Given Date to Third Party" />
                                <asp:BoundField DataField="ticket_received_date" HeaderText="Received Date from Third Party" />
                                <asp:BoundField DataField="transaction_type" HeaderText="Transaction Type" />
                                <asp:BoundField DataField="transacted_by" HeaderText="Transacted By" />
                                <asp:BoundField DataField="logs_created_at" HeaderText="Date Transacted" />
                                <asp:BoundField DataField="reason_rejected_solution" HeaderText="Reject Remarks" />
                                <asp:BoundField DataField="status" HeaderText="Status" />
                                <asp:BoundField DataField="priority_description" HeaderText="Priority Level" />
                                <asp:BoundField DataField="description_section" HeaderText="Section" />
                                <asp:BoundField DataField="description_category" HeaderText="Category" />
                                <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
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

</asp:Content>