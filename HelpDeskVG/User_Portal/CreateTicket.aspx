<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User_Portal/User_Portal.Master" CodeBehind="CreateTicket.aspx.cs" Inherits="HelpDeskVG.User_Portal.CreateTicket" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderId="head">
        <title>Create Ticket</title>

    <script type="text/javascript">
    function validateForm() {
        var natureOfProblem = document.getElementById('<%= ddlNatureOfProblem.ClientID %>').value;
        var priority = document.getElementById('<%= ddlPriority.ClientID %>').value;
        var category = document.getElementById('<%= ddlCategory.ClientID %>').value;
        var section = document.getElementById('<%= ddlSection.ClientID %>').value;
        var subject = document.getElementById('<%= txtSubject.ClientID %>').value.trim();
        var description = document.getElementById('<%= txtDescription.ClientID %>').value.trim();
        var requestId = '<%= Request.QueryString["Id"] != null ? Request.QueryString["Id"].ToString() : "" %>';

        if (natureOfProblem === "" || priority === "" || category === "" || section === "" || 
            subject === "" || description === "" || requestId === "") {
            alert("Please fill up the field that is Required.");
            return false; // Prevent form submission
        }

            return confirm("Do you like to Submit the Ticket?");
        }

        function validateDraftForm() {
            return confirm("Do you like to Save as Draft??");

        }

        $(document).ready(function () {
            $('.custom-select').select2({ width: '100%' });

        });
    </script>

    </asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <main>

        <section class="row" aria-labelledby="aspnetTitle">
<%--        <h1><%=Page.Title %></h1>--%>
    </section>
    <div class="col-12">
        <%--<div class="card">
                <div class="card-header">
                    <h3 class="card-title"></h3>
                </div>
            </div>--%>
        <div class="card">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">Create New Ticket</h3>
                            <div class="card-actions d-flex gap-2">
                            <asp:LinkButton ID="lnkSaveDraftTicket" runat="server" OnClick="lnkSaveDraftTicket_Click" OnClientClick="return validateDraftForm();" CssClass="btn btn-success">Save as Draft</asp:LinkButton>
                         <asp:LinkButton ID="lnkSaveTicket" runat="server" OnClick="lnkSaveTicket_Click" OnClientClick="return validateForm();" CssClass="btn btn-primary w-100 end-0">Submit as Ticket</asp:LinkButton>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <div class="card-body">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSection" runat="server" CssClass="form-label mb-2 status status-primary required">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSection" CssClass="form-select text-reset" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblCategory" runat="server" CssClass="form-label mb-2 status status-primary required">Category</asp:Label>
                                        <asp:DropDownList ID="ddlCategory" CssClass="form-select text-reset" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">

                                        <asp:Label ID="lblNatureOfProb" runat="server" CssClass="form-label mb-2 status status-primary required">Nature of Problem</asp:Label>
                                        <asp:DropDownList ID="ddlNatureOfProblem" OnSelectedIndexChanged="ddlNatureOfProblem_SelectedIndexChanged" CssClass="form-select text-reset" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mt-2 mb-2">
                                            <asp:Label ID="lblSubject" runat="server" CssClass="form-label status status-primary required">Subject</asp:Label>
                                        </div>
                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control text-reset" placeholder="Enter Subject"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="mt-2 mb-2">
                                            <asp:Label ID="lblPriority" runat="server" CssClass="form-label status status-primary required">Priority Level</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-select text-reset" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="mt-2 mb-2">
                                            <asp:Label ID="lblCreatedFor" runat="server" CssClass="form-label status status-primary">Created For:</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="custom-select text-reset" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="mt-2 mb-2">
                                            <asp:Label ID="lblCreatedAt" runat="server" CssClass="form-label status status-primary">Created At:</asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtCreatedAt" runat="server" CssClass="form-control text-reset" AutoPostBack="false" TextMode="DateTimeLocal"></asp:TextBox>
                                    </div>
                                    <script type="text/javascript">
                                        window.onload = function () {
                                            var txtBox = document.getElementById('<%= txtCreatedAt.ClientID %>');

                                            // Build Local Date in format YYYY-MM-DDTHH:mm (for datetime-local input)
                                            var now = new Date();
                                            var year = now.getFullYear();
                                            var month = String(now.getMonth() + 1).padStart(2, '0');
                                            var day = String(now.getDate()).padStart(2, '0');
                                            var hours = String(now.getHours()).padStart(2, '0');
                                            var minutes = String(now.getMinutes()).padStart(2, '0');

                                            var localDateTime = year + '-' + month + '-' + day + 'T' + hours + ':' + minutes;

                                            // If empty, set value to now
                                            if (!txtBox.value) {
                                                txtBox.value = localDateTime;
                                            }

                                            // Optional: Max should be today and current time
                                            txtBox.max = localDateTime;
                                        }
                                    </script>
                                </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-2">
                                        <asp:Label ID="lblDescription" runat="server" CssClass="form-label status status-primary required">Description</asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control text-reset" TextMode="MultiLine" Rows="5" AutoPostBack="false" placeholder="Description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="mb-2 mt-4">
                                        <asp:Label ID="lblAttachment" runat="server" CssClass="form-label status status-primary">Please attach with your application:</asp:Label>
                                        <asp:Label ID="lblAttachment123" runat="server" CssClass="form-label ">Maximum upload file size: 25MB. Kindly wait for the file to finish uploading before filling up the form.</asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuUploadAttachment" CssClass="form-control" runat="server" />
                                            </div>
                             <%--               <div class="col-md-6">
                                                <asp:LinkButton ID="lnkUploadAttachkment" CssClass="btn btn-info" OnClick="lnkUploadAttachkment_Click" AutoPostBack="false" runat="server">Upload Attachment</asp:LinkButton>
                                            </div>--%>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="mb-2 mt-2">
                                                        <asp:Label ID="lblAttachmentDescription" runat="server" CssClass="form-label status status-primary">Attachment Description:</asp:Label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtAttachmentDescription" runat="server" CssClass="form-control text-reset" TextMode="MultiLine" Rows="3" placeholder="Attachment Description"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfTicketStageId" runat="server"></asp:HiddenField>
                            <div class="col-md-12 mt-2">
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvHDUploadedAttachment" runat="server" AutoGenerateColumns="false" CssClass="table table-vcenter table-hover text-nowrap">

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</main>
</asp:Content>
