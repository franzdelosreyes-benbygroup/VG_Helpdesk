<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreateTicket.aspx.cs" Inherits="HelpDeskVG.CreateTicket" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">
        <title>Create Ticket</title>

    <script type="text/javascript">
        function validateForm() {
            var natureOfProblem = document.getElementById('<%= ddlNatureOfProblem.ClientID %>').value;
            var priority = document.getElementById('<%= ddlPriority.ClientID %>').value;
            var category = document.getElementById('<%= ddlCategory.ClientID %>').value;
            var section = document.getElementById('<%= ddlSection.ClientID %>').value;
            var assignTo = document.getElementById('<%= ddlEmployeeIT.ClientID %>').value;
            var subject = document.getElementById('<%= txtSubject.ClientID %>').value.trim();
            var description = document.getElementById('<%= txtDescription.ClientID %>').value.trim();
            var requestId = '<%= Request.QueryString["Id"] != null ? Request.QueryString["Id"].ToString() : "" %>';

            if (natureOfProblem === "" || priority === "" || category === "" || section === "" || assignTo === "" ||
                subject === "" || description === "" || requestId === "") {
                alert("Please fill up the field that is Required.");
                return false; // Prevent form submission
            }

            return true;
        }
    </script>

    <style>
        .required-label::after {
            content: "*";
            color: red;
            font-weight: bold;
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <main>
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
                            <div class="card-actions">
                                <asp:LinkButton ID="lnkSaveTicket" runat="server" OnClick="lnkSaveTicket_Click" OnClientClick="return validateForm();" CssClass="btn btn-success w-100"> Save Ticket</asp:LinkButton>

                            </div>
                        </div>
                        <div class="card-body p-0">
                            <div class="card-body">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblAssignTo" runat="server" CssClass="form-label mb-2 status status-primary required-label">Assign Ticket To:</asp:Label>
                                            <asp:DropDownList ID="ddlEmployeeIT" runat="server" CssClass="form-select text-reset"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblSection" runat="server" CssClass="form-label mb-2 status status-primary required-label">Section</asp:Label>
                                            <asp:DropDownList ID="ddlSection" CssClass="form-select text-reset" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblCategory" runat="server" CssClass="form-label mb-2 status status-primary required-label">Category</asp:Label>
                                            <asp:DropDownList ID="ddlCategory" CssClass="form-select text-reset" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblNatureOfProb" runat="server" CssClass="form-label mb-2 status status-primary required-label">Nature of Problem</asp:Label>
                                            <asp:DropDownList ID="ddlNatureOfProblem" OnSelectedIndexChanged="ddlNatureOfProblem_SelectedIndexChanged" CssClass="form-control text-reset" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="mt-2 mb-2">
                                                <asp:Label ID="lblSubject" runat="server" CssClass="form-label status status-primary required-label">Subject</asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control text-reset" placeholder="Enter Subject"></asp:TextBox>
                                        </div>
                                            <div class="col-md-2">
                                                <div class="mt-2 mb-2">
                                                    <asp:Label ID="lblPriority" runat="server" CssClass="form-label status status-primary required-label">Priority Level</asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-select text-reset" AutoPostBack="false"></asp:DropDownList>
                                            </div>
                                        <div class="col-md-2">
                                            <div class="mt-2 mb-2">
                                                <asp:Label ID="lblCreatedFor" runat="server" CssClass="form-label status status-primary">Created For:</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-select text-reset" AutoPostBack="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="mb-2 mt-2">
                                            <asp:Label ID="lblDescription" runat="server" CssClass="form-label status status-primary required-label">Description</asp:Label>
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


