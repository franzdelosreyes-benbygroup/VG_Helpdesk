<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Maintenance.aspx.cs" Inherits="HelpDeskVG.Maintenance" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head">
        <script type="text/javascript">
            function assignRoleModal() {
                $(document).ready(function () {
                    $("#assignRoleModal").modal("show");
                });
            }

            function addSectionModal() {
                $(document).ready(function () {
                    $("#addSectionModal").modal("show");
                });
            }

            function addCategoryModal() {
                $(document).ready(function () {
                    $("#addCategoryModal").modal("show");
                });
            }

            function addNatureOfProbModal() {
                $(document).ready(function () {
                    $("#addNatureOfProb").modal("show");
                });
            }
            function addPriorityModal() {
                $(document).ready(function () {
                    $("#addPriority").modal("show");
                });
            }

            function editSectionModal() {
                $(document).ready(function () {
                    $("#editSection").modal("show");
                });
            }

            function editCategoryModal() {
                $(document).ready(function () {
                    $("#editCategory").modal("show");
                });
            }

            function editNatureOfProblemModal() {
                $(document).ready(function () {
                    $("#editNatureOfProb").modal("show");
                });
            }

            function editPriorityModal() {
                $(document).ready(function () {
                    $("#editPriority").modal("show");
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

                // Save modal state when opened
                document.getElementById("addNatureOfProb").addEventListener("shown.bs.modal", function () {
                    localStorage.setItem("modalOpen", "true");
                });

                // Listen for tab changes and update localStorage
                document.querySelectorAll('[data-bs-toggle="tab"]').forEach(tab => {
                    tab.addEventListener("shown.bs.tab", function (event) {
                        let selectedTab = event.target.getAttribute("href");
                        localStorage.setItem("activeTab", selectedTab);
                    });
                });
            });
            function validateRolePermission() {
                   var name = document.getElementById('<%= ddlEmployeeIT.ClientID %>').value;
                   var role = document.getElementById('<%= ddlRole.ClientID %>').value;


                   if (name === "" || role === "") {

                       alert("Please fill up the field that is Required.");
                       return false;
                   }

                   return true;
               }

            function validateAddSection() {
                var section = document.getElementById('<%= txtNewSection.ClientID %>').value;

                if (section === "") {

                    alert("Please fill up the field that is Required.");
                    return false;
                }

                return true;

            }


            function validateAddCategory() {
                var section = document.getElementById('<%= ddlSection.ClientID %>').value;
                var category = document.getElementById('<%= txtNewCategory.ClientID %>').value;

                if (section === "" || category === "") {

                    alert("Please fill up the field that is Required.");
                    return false;
                }

                return true;

            }

            function validateAddNatureOfProb() {
                var section = document.getElementById('<%= ddlSection2.ClientID %>').value;
                var category = document.getElementById('<%= ddlCategory2.ClientID %>').value;
                var natureofprob = document.getElementById('<%= txtNewNatureOfProb.ClientID %>').value;

                if (section === "" || category === "") {

                    alert("Please fill up the field that is Required.");
                    return false;
                }

                return true;
            }

            function validatePriorityEdit() {
                var color = document.getElementById('<%= ddlEditColorPriority.ClientID %>').value;
                var allotedhr = document.getElementById('<%= txtNewEditedAllotedHr.ClientID %>').value;
                var desc_prio = document.getElementById('<%= txtNewEditedDescriptionPriority.ClientID %>').value;

                if (desc_prio === "" || allotedhr === "" || color === "") {

                    alert("Please fill up the field that is Required.");
                    return false;
                }

                return true;
            }

            function validatePriority() {
                var color = document.getElementById('<%= ddlPriorityColor.ClientID %>').value;
                var allotedhr = document.getElementById('<%= txtNewAllotedHrs.ClientID %>').value;
                var desc_prio = document.getElementById('<%= txtNewDescPriority.ClientID %>').value;

                if (desc_prio === "" || allotedhr === "" || color === "") {

                    alert("Please fill up the field that is Required.");
                    return false;
                }

                return true;
            }

        </script>

    <style>
        .color-box {
            display: inline-block;
            width: 50px;
            height: 20px;
            border-radius: 5px;
            text-align: center;
            color: white; /* Adjust depending on background */
            font-weight: bold;
        }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-wrapper">
        <div class="page-body">
            <div class="card">
                <div class="card-header bg-red">
                    <ul class="nav nav-tabs card-header-tabs bg-red nav-fill" data-bs-toggle="tabs" role="tablist">
                        <li class="nav-item text-black" role="presentation">
                            <a class="nav-link active " data-bs-toggle="tab" href="#rolesandpermission" aria-selected="false" role="tab">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-license">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M15 21h-9a3 3 0 0 1 -3 -3v-1h10v2a2 2 0 0 0 4 0v-14a2 2 0 1 1 2 2h-2m2 -4h-11a3 3 0 0 0 -3 3v11" />
                                    <path d="M9 7l4 0" />
                                    <path d="M9 11l4 0" />
                                </svg>
                                Roles & Permission                  
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link" data-bs-toggle="tab" href="#section" aria-selected="false" role="tab" tabindex="-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-settings-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12.483 20.935c-.862 .239 -1.898 -.178 -2.158 -1.252a1.724 1.724 0 0 0 -2.573 -1.066c-1.543 .94 -3.31 -.826 -2.37 -2.37a1.724 1.724 0 0 0 -1.065 -2.572c-1.756 -.426 -1.756 -2.924 0 -3.35a1.724 1.724 0 0 0 1.066 -2.573c-.94 -1.543 .826 -3.31 2.37 -2.37c1 .608 2.296 .07 2.572 -1.065c.426 -1.756 2.924 -1.756 3.35 0a1.724 1.724 0 0 0 2.573 1.066c1.543 -.94 3.31 .826 2.37 2.37a1.724 1.724 0 0 0 1.065 2.572c1.08 .262 1.496 1.308 1.247 2.173" />
                                    <path d="M16 19h6" />
                                    <path d="M19 16v6" />
                                    <path d="M9 12a3 3 0 1 0 6 0a3 3 0 0 0 -6 0" />
                                </svg>
                                Section
                            </a>
                        </li>

                        <li class="nav-item" role="presentation">
                            <a class="nav-link" data-bs-toggle="tab" href="#category" aria-selected="false" role="tab" tabindex="-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-settings-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12.483 20.935c-.862 .239 -1.898 -.178 -2.158 -1.252a1.724 1.724 0 0 0 -2.573 -1.066c-1.543 .94 -3.31 -.826 -2.37 -2.37a1.724 1.724 0 0 0 -1.065 -2.572c-1.756 -.426 -1.756 -2.924 0 -3.35a1.724 1.724 0 0 0 1.066 -2.573c-.94 -1.543 .826 -3.31 2.37 -2.37c1 .608 2.296 .07 2.572 -1.065c.426 -1.756 2.924 -1.756 3.35 0a1.724 1.724 0 0 0 2.573 1.066c1.543 -.94 3.31 .826 2.37 2.37a1.724 1.724 0 0 0 1.065 2.572c1.08 .262 1.496 1.308 1.247 2.173" />
                                    <path d="M16 19h6" />
                                    <path d="M19 16v6" />
                                    <path d="M9 12a3 3 0 1 0 6 0a3 3 0 0 0 -6 0" />
                                </svg>
                                Category
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link" data-bs-toggle="tab" href="#natureofproblem" aria-selected="false" role="tab" tabindex="-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-settings-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12.483 20.935c-.862 .239 -1.898 -.178 -2.158 -1.252a1.724 1.724 0 0 0 -2.573 -1.066c-1.543 .94 -3.31 -.826 -2.37 -2.37a1.724 1.724 0 0 0 -1.065 -2.572c-1.756 -.426 -1.756 -2.924 0 -3.35a1.724 1.724 0 0 0 1.066 -2.573c-.94 -1.543 .826 -3.31 2.37 -2.37c1 .608 2.296 .07 2.572 -1.065c.426 -1.756 2.924 -1.756 3.35 0a1.724 1.724 0 0 0 2.573 1.066c1.543 -.94 3.31 .826 2.37 2.37a1.724 1.724 0 0 0 1.065 2.572c1.08 .262 1.496 1.308 1.247 2.173" />
                                    <path d="M16 19h6" />
                                    <path d="M19 16v6" />
                                    <path d="M9 12a3 3 0 1 0 6 0a3 3 0 0 0 -6 0" />
                                </svg>
                                Nature Of Problem
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a class="nav-link" data-bs-toggle="tab" href="#priority" aria-selected="false" role="tab" tabindex="-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-exclamation-mark">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                    <path d="M12 19v.01" />
                                    <path d="M12 15v-10" />
                                </svg>
                                Priority Level
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="card-body">
                    <div class="tab-content">
                        <div class="tab-pane active show" id="rolesandpermission" role="tabpanel">
                            <div class="col-md-12 d-flex justify-content-end">
                                <asp:LinkButton ID="lnkRolesandPermission" runat="server" CssClass="btn btn-primary position-relative end-0 m-2" OnClick="lnkRolesandPermission_Click">
                                    <svg xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                                     <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                                    Assign Roles and Permission
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblRolesandPermission" runat="server" CssClass="h4" Text="Roles and Permission"></asp:Label>
                            <div class="col-md-12">
                                <div class="table-responsive-xl">
                                    <asp:GridView ID="gvRolesandPermission" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4" AllowPaging="true" PageSize="10" OnPageIndexChanged="gvRolesandPermission_PageIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="employee_info" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="employee_no" HeaderText="Employee No" />
                                            <asp:BoundField DataField="employee_position" HeaderText="Position" />
                                            <asp:BoundField DataField="role_name" HeaderText="Role Name" />
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfAdminEmployeeId" Value='<%#Eval("employee_no") %>' runat="server" />
                                                    <asp:HiddenField ID="hfAdminIsActive" Value='<%# Eval("admin_status")%>' runat="server" />
                                                    <asp:LinkButton ID="lnkDeactivate" runat="server" ToolTip="Deactivate" Visible='<%# (Eval("is_active").ToString() == "True" ? true : false) %>' CommandArgument='<%# Eval("admin_status")%>' OnClick="lnkDeactivate_Click" CssClass="btn btn-warning position--relative w-50">
                                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-x">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M18 6l-12 12" /><path d="M6 6l12 12" /></svg>
                                                   Delete Access
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkActivate" runat="server" Visible='<%# (Eval("is_active").ToString() == "False" ? true : false) %>' CommandArgument='<%# Eval("admin_status")%>' ToolTip="Activate" OnClick="lnkActivate_Click" CssClass="btn btn-primary position-relative w-50">
                                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-check">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M5 12l5 5l10 -10" /></svg>
                                                    Activate Access
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                </div>
                            </div>
                        </div>
                      <div class="tab-pane fade" id="section" role="tabpanel">
                          <div class="col-md-12 d-flex justify-content-end">
                              <asp:LinkButton ID="lnkAddSection" runat="server" CssClass="btn btn-primary position-relative end-0 m-2" OnClick="lnkAddSection_Click">
                               <svg xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                                  Add New Section
                              </asp:LinkButton>
                          </div>
                          <div class="col-md-12">
                              <asp:Label ID="Label2" runat="server" CssClass="h4" Text="Section List"></asp:Label>
                              <div class="col-md-12">
                                  <div class="table-responsive-xl">
                                      <asp:GridView ID="gvSectionList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4">
                                          <Columns>
                                              <asp:BoundField DataField="description_section" HeaderText="Section" />
                                              <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                              <asp:TemplateField HeaderText="Actions">
                                                  <ItemTemplate>
                                                      <asp:HiddenField ID="hfSectionId" Value='<%# Eval("section_id")%>' runat="server" />
                                                      <asp:LinkButton ID="lnkEditSection" OnClick="lnkEditSection_Click" runat="server" CssClass="btn btn-info" ToolTip="Edit Section">
                                                          <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" />
                                                              <path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                                                          Edit Section 
                                                      </asp:LinkButton>
                                                      <asp:LinkButton ID="lnkDeleteSection" runat="server" CssClass="btn btn-danger" OnClick="lnkDeleteSection_Click" ToolTip="Delete Section">
                                                          <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-trash-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 7h16" /><path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" /><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /><path d="M10 12l4 4m0 -4l-4 4" /></svg>
                                                     Delete Section
                                                      </asp:LinkButton>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                      </asp:GridView>
                                  </div>
                              </div>
                          </div>
                      </div>
                       <div class="tab-pane fade" id="category" role="tabpanel">
                          <div class="col-md-12 d-flex justify-content-end">
                              <asp:LinkButton ID="lnkAddCategory" runat="server" CssClass="btn btn-primary position-relative end-0 m-2" OnClick="lnkAddCategory_Click">
                               <svg xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                                  Add New Category
                              </asp:LinkButton>
                          </div>
                          <div class="col-md-12">
                              <asp:Label ID="Label3" runat="server" CssClass="h4" Text="Category List"></asp:Label>
                              <div class="col-md-12">
                                  <div class="table-responsive-xl">
                                      <asp:GridView ID="gvCategoryList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4">
                                          <Columns>
                                              <asp:BoundField DataField="description_section" HeaderText="Section" />
                                              <asp:BoundField DataField="description_category" HeaderText="Category" />
                                              <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                              <asp:TemplateField HeaderText="Actions">
                                                  <ItemTemplate>
                                                      <asp:HiddenField ID="hfCategoryId" Value='<%# Eval("category_id")%>' runat="server" />
                                                      <asp:LinkButton ID="lnkEditCategory" OnClick="lnkEditCategory_Click" runat="server" CssClass="btn btn-info" ToolTip="Edit Section">
                                                      <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" />
                                                          <path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                                                      Edit Category 
                                                      </asp:LinkButton>
                                                      <asp:LinkButton ID="lnkDeleteCategory" runat="server" CssClass="btn btn-danger" ToolTip="Delete Category" OnClick="lnkDeleteCategory_Click">
                                                      <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-trash"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 7l16 0" /><path d="M10 11l0 6" /><path d="M14 11l0 6" /><path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" /><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
                                                       Delete Category                                                 
                                                      </asp:LinkButton>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                      </asp:GridView>
                                  </div>
                              </div>
                          </div>
                      </div>
                        <div class="tab-pane fade" id="natureofproblem" role="tabpanel">
                            <div class="col-md-12 d-flex justify-content-end">
                                <asp:LinkButton ID="lnkAddNatureOfProb" runat="server" CssClass="btn btn-primary position-relative end-0 m-2" OnClick="lnkAddNatureOfProb_Click">
                               <svg xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                                              Add New Nature Of Problem 
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="Label4" runat="server" CssClass="h4" Text="Nature Of Problem List"></asp:Label>
                                <div class="col-md-12">
                                    <div class="table-responsive-xl">
                                        <asp:GridView ID="gvNatureOfProbList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4">
                                            <Columns>
                                                <asp:BoundField DataField="description_section" HeaderText="Section" />
                                                <asp:BoundField DataField="description_category" HeaderText="Category" />
                                                <asp:BoundField DataField="description_natureofprob" HeaderText="Nature of Problem" />
                                                <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfNatureOfProb" Value='<%# Eval("nature_of_prob_id")%>' runat="server" />
                                                        <asp:LinkButton ID="lnkEditNatureOfProb" OnClick="lnkEditNatureOfProb_Click" runat="server" CssClass="btn btn-info" ToolTip="Edit Section">
                                                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" />
                                                             <path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                                                         Edit Nature of Problem
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDeleteNatureOfProb" runat="server" CssClass="btn btn-danger" ToolTip="Delete Nature of Problem" OnClick="lnkDeleteNatureOfProb_Click">
                                                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-trash"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 7l16 0" /><path d="M10 11l0 6" /><path d="M14 11l0 6" /><path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" /><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
                                                        Delete Nature of Problem
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="priority" role="tabpanel">
                            <div class="col-md-12 d-flex justify-content-end">
                                <asp:LinkButton ID="lnkAddNewPriority" runat="server" CssClass="btn btn-primary position-relative end-0 m-2" OnClick="lnkAddNewPriority_Click">
                             <svg xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                                      Add New Priority
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="Label5" runat="server" CssClass="h4" Text="Priority List"></asp:Label>
                                <div class="col-md-12">
                                    <div class="table-responsive-xl">
                                        <asp:GridView ID="gvPriorityList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover card-table table-vcenter text-nowrap datatable mt-4">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server"
                                                            Text='<%# Eval("description") %>'
                                                            Style='<%# "color: " + Eval("color_code") %>' Font-Bold="true">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="alloted_hour" HeaderText="Alloted Hours" />
                                                <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                                <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColorCode" runat="server" CssClass="color-box"
                                                            Style='<%# "background-color:" + Eval("color_code") + "; padding: 5px; display: inline-block; width: 50px; height: 20px;" %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfPriority" Value='<%# Eval("priority_id")%>' runat="server" />
                                                        <asp:LinkButton ID="lnkEditPriority" OnClick="lnkEditPriority_Click" runat="server" CssClass="btn btn-info" ToolTip="Edit Section">
                                                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-edit"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M7 7h-1a2 2 0 0 0 -2 2v9a2 2 0 0 0 2 2h9a2 2 0 0 0 2 -2v-1" />
                                                             <path d="M20.385 6.585a2.1 2.1 0 0 0 -2.97 -2.97l-8.415 8.385v3h3l8.385 -8.415z" /><path d="M16 5l3 3" /></svg>
                                                         Edit Priority
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDeletePriority" runat="server" CssClass="btn btn-danger" OnClick="lnkDeletePriority_Click" ToolTip="Delete Priority">
                                                    <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-trash"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M4 7l16 0" /><path d="M10 11l0 6" /><path d="M14 11l0 6" /><path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" /><path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
                                                      Delete Priority
                                                        </asp:LinkButton>
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
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="assignRoleModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Assign User Role</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="lblITEmployee" CssClass="form-label status status-primary required" runat="server">Select IT Employee</asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:DropDownList ID="ddlEmployeeIT" runat="server" CssClass="form-select text-reset"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="lblRolePosition" CssClass="form-label status status-primary required" runat="server">Select Role</asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select text-reset"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>

                    <asp:LinkButton ID="lnkAssignRole" runat="server" OnClick="lnkAssignRole_Click" OnClientClick="return validateRolePermission();" CssClass="btn btn-primary">
                        <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Assign New Role</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="addSectionModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create New Role</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label1" CssClass="form-label status status-primary required" runat="server">Add New Section</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewSection" runat="server" CssClass="form-control text-reset" Placeholder="Insert New Section"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveNewSection" runat="server" OnClick="lnkSaveNewSection_Click" OnClientClick="return validateAddSection();" CssClass="btn btn-primary">
                             <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save New Section</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="addCategoryModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create New Category</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label7" CssClass="form-label status status-primary required" runat="server">Section</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:DropDownList ID="ddlSection" CssClass="form-select text-reset" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label6" CssClass="form-label status status-primary required" runat="server">Add New Category</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewCategory" runat="server" CssClass="form-control text-reset" Placeholder="Insert New Category"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveCategory" OnClientClick="return validateAddCategory();" runat="server" OnClick="lnkSaveCategory_Click" CssClass="btn btn-primary">
                            <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                            <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save New Category</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="addNatureOfProb">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create New Nature of Problem</h5>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfModalState" runat="server" />
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label8" CssClass="form-label status status-primary required" runat="server">Section</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                                <asp:DropDownList ID="ddlSection2" OnSelectedIndexChanged="ddlSection2_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select text-reset" runat="server"></asp:DropDownList>                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="mb-3">
                                <asp:Label ID="Label10" CssClass="form-label status status-primary required" runat="server">Category</asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:DropDownList ID="ddlCategory2" CssClass="form-select text-reset" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label9" CssClass="form-label status status-primary required" runat="server">Add New Nature Of Problem</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewNatureOfProb" runat="server" CssClass="form-control text-reset" Placeholder="Insert New Nature Of Problem"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveNatureofProb" runat="server" OnClick="lnkSaveNatureofProb_Click" OnClientClick="return validateAddNatureOfProb();" CssClass="btn btn-primary">
                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save New Nature of Problem</asp:LinkButton>
                </div>
            </div>
        </div>
        </div>

    
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="addPriority">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create New Priority</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label11" CssClass="form-label status status-primary required" runat="server">Description</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewDescPriority" runat="server" CssClass="form-control text-reset" Placeholder="Insert Description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label12" CssClass="form-label status status-primary required" runat="server">Alloted Hours</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewAllotedHrs" TextMode="Number" runat="server" CssClass="form-control text-reset" Placeholder="Insert Hours"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:Label ID="Label21" CssClass="form-label status status-primary required" runat="server">Priority Color</asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="mb-3">
                                            <asp:DropDownList ID="ddlPriorityColor" runat="server" CssClass="form-select">
                                                <asp:ListItem Text="Dark Red" Value="#bd081c" style="background-color: #bd081c; color: white;"></asp:ListItem>
                                                <asp:ListItem Text="Orange" Value="#ffa500" style="background-color: #ffa500; color: black;"></asp:ListItem>
                                                <asp:ListItem Text="Light Blue" Value="#1ab7ea" style="background-color: #1ab7ea; color: white;"></asp:ListItem>
                                                <asp:ListItem Text="Pink Red" Value="#e4405f" style="background-color: #e4405f; color: white;"></asp:ListItem>
                                                <asp:ListItem Text="Green" Value="#74b816" style="background-color: #74b816; color: white;"></asp:ListItem>
                                                <asp:ListItem Text="Teal" Value="#17a2b8" style="background-color: #17a2b8; color: white;"></asp:ListItem>
                                                <asp:ListItem Text="Purple" Value="#ae3ec9" style="background-color: #ae3ec9; color: white;"></asp:ListItem>
                                                <asp:ListItem Text="Dark Orange" Value="#f76707" style="background-color: #f76707; color: white;"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveNewPrio" runat="server" OnClick="lnkSaveNewPrio_Click" OnClientClick="return validatePriority();" CssClass="btn btn-primary">
                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save New Priority</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="editSection">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Section</h5>
                </div>
                <asp:HiddenField ID="hfMdSectionId" runat="server" />
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label13" CssClass="form-label" runat="server">Section</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtEditSection" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveNewEditedSection" runat="server" OnClick="lnkSaveNewEditedSection_Click" CssClass="btn btn-primary">
                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save Edited Section</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="editCategory">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Section</h5>
                </div>
                <asp:HiddenField ID="hfMdCategoryId" runat="server" />
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label14" CssClass="form-label" runat="server">Section</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtEditSection2" Enabled="false" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:Label ID="label15" CssClass="form-label" runat="server">Category</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtEditCategory2" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveEditedCategory" runat="server" OnClick="lnkSaveEditedCategory_Click" CssClass="btn btn-primary">
                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save Edited Category</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="editNatureOfProb">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Section</h5>
                </div>
                <asp:HiddenField ID="hfMdNatureOfProbId" runat="server" />
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label16" CssClass="form-label" runat="server">Section</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtEditSection3" Enabled="false" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:Label ID="label17" CssClass="form-label" runat="server">Category</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtEditCategory3" Enabled="false" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:Label ID="label18" CssClass="form-label" runat="server">Nature of Problem</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtEditNatureOfProb" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSavedEditedNatureofProb" runat="server" OnClick="lnkSavedEditedNatureofProb_Click" CssClass="btn btn-primary">
                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save Edited Category</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal modal-blur fade" tabindex="-1" role="dialog" aria-hidden="true" id="editPriority">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Priority</h5>
                </div>
                <asp:HiddenField ID="hfMdPriorityId" runat="server" />
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Label ID="Label19" CssClass="form-label status status-primary required" runat="server">Priority Description</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewEditedDescriptionPriority" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:Label ID="label20" CssClass="form-label status status-primary required" runat="server">Priority Alloted Hour/s</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:TextBox ID="txtNewEditedAllotedHr" TextMode="Number" runat="server" CssClass="form-control text-reset"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:Label ID="lblColorEdit" runat="server" CssClass="form-label status status-primary required">Color</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="mb-3">
                                        <asp:DropDownList ID="ddlEditColorPriority" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Dark Red" Value="#bd081c" style="background-color: #bd081c; color: white;"></asp:ListItem>
                                            <asp:ListItem Text="Orange" Value="#ffa500" style="background-color: #ffa500; color: black;"></asp:ListItem>
                                            <asp:ListItem Text="Light Blue" Value="#1ab7ea" style="background-color: #1ab7ea; color: white;"></asp:ListItem>
                                            <asp:ListItem Text="Pink Red" Value="#e4405f" style="background-color: #e4405f; color: white;"></asp:ListItem>
                                            <asp:ListItem Text="Green" Value="#74b816" style="background-color: #74b816; color: white;"></asp:ListItem>
                                            <asp:ListItem Text="Teal" Value="#17a2b8" style="background-color: #17a2b8; color: white;"></asp:ListItem>
                                            <asp:ListItem Text="Purple" Value="#ae3ec9" style="background-color: #ae3ec9; color: white;"></asp:ListItem>
                                            <asp:ListItem Text="Dark Orange" Value="#f76707" style="background-color: #f76707; color: white;"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    <asp:LinkButton ID="lnkSaveEditedPriority" runat="server" OnClick="lnkSaveEditedPriority_Click" CssClass="btn btn-primary" OnClientClick="return validatePriorityEdit();">
                         <svg  xmlns="http://www.w3.org/2000/svg"  width="24"  height="24"  viewBox="0 0 24 24"  fill="none"  stroke="currentColor"  stroke-width="2"  stroke-linecap="round"  stroke-linejoin="round"  class="icon icon-tabler icons-tabler-outline icon-tabler-plus">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M12 5l0 14" /><path d="M5 12l14 0" /></svg>
                        Save Edited Priority</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
