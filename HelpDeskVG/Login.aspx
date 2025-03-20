<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HelpDeskVG.Login" %>


<!DOCTYPE html>
<html lang="en">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, viewport-fit=cover" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <title>Login</title>

    <!-- CSS files -->
    <link rel="icon" href="img/VGlogonobg.png" type="image/png" />
    <link href="tabler/dist/css/tabler.min.css" rel="stylesheet" />
    <link href="tabler/dist/css/tabler-flags.min.css" rel="stylesheet" />
    <link href="tabler/dist/css/tabler-payments.min.css" rel="stylesheet" />
    <link href="tabler/dist/css/tabler-vendors.min.css" rel="stylesheet" />
    <link href="tabler/dist/css/demo.min.css" rel="stylesheet" />
    
    <link href="Content/toastr.css" rel="stylesheet"/>
        <!-- Include jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="Scripts/toastr.js"></script>
    <script>
        window.onload = function () {
            // Clear localStorage and sessionStorage
            localStorage.clear();
            sessionStorage.clear();
        };
    </script>
    <style>
        @import url('https://rsms.me/inter/inter.css');

        :root {
            --tblr-font-sans-serif: 'Inter Var', -apple-system, BlinkMacSystemFont, San Francisco, Segoe UI, Roboto, Helvetica Neue, sans-serif;
        }

        body {
            font-feature-settings: "cv03", "cv04", "cv11";
        }

    .form-control:focus {
    color: #B22222; /* Firebrick color for the text */
    background-color: #fff; /* White background */
    border-color: #B22222; /* Firebrick color for the border */
    outline: 0;
    box-shadow: 0 0 0 0.25rem rgba(178, 34, 34, 0.25); /* Lighter Firebrick for the box shadow */
}
    </style>
</head>
<body class=" d-flex flex-column bg-white">
    <div class="row g-0 flex-fill">
        <div class="col-12 col-lg-6 col-xl-4 border-top-wide border-danger d-flex flex-column justify-content-center mb-8">
            <div class="container container-tight my-5 px-lg-5 mb-8">
                <div class="text-center mb-4">
                    <a href="." class="navbar-brand navbar-brand-autodark">
                        <img src="img/benbygroup_logo.png" height="150" alt=""></a>
                </div>
                <div class ="h5 text-center mb-3">
        <img src="img/logoVGnewpng.png" class="img-fluid" style="height: auto; width: auto; max-width: 100%; max-height: 100%; object-fit: contain; margin-bottom: 20px;">
                    </div>
                        <div class ="h2 text-center mb-3">
         HELPDESK PORTAL 
        </div>
                <h2 class="h3 text-center mb-3">Login to your account
                </h2>
                <form id="form1" runat="server">
                    <div class="mb-3">
                        <label class="form-label">Employee No.</label>
                            <input type="text" class="form-control text-reset" placeholder="Employee No" id="txtEmployeeNo" runat="server" ClientIDMode="Static"/>
                    </div>
                    <div class="mb-2">
                        <label class="form-label">Password</label>
                            <input type="password" class="form-control text-reset" placeholder="Password" id="txtPassword" runat="server" ClientIDMode="Static"/>
                        </div>
                    <div class="form-footer">
                        <asp:Linkbutton ID="lnkLogin" OnClick="lnkLogin_Click" runat="server" class="btn btn-danger w-100">Sign in</asp:Linkbutton>
                    </div>
                </form>
                <div class="text-end text-secondary mt-3">
                    <a href="https://vgsciportal.com/VG_HR/ChangePassword.aspx" tabindex="-1">Forgot Password? Click me</a>
                </div>
            </div>
        </div>
    <div class="col-12 col-lg-6 col-xl-8 d-none d-lg-block">
        <!-- Photo -->
        <div class="bg-cover h-75 min-vh-50 d-flex justify-content-center align-items-center">
<img src="img/vghelpdesk_login_bg.svg" class="img-fluid" style="width: auto; margin-top:100px;max-width: 90vw; height: auto; max-height: 90vh; object-fit: contain;">        </div>
    </div>
    </div>
    <!-- Tabler Core -->
    <script src="tabler/dist/js/tabler.min.js"></script>
    <script src="tabler/dist/js/demo.min.js"></script>
</body>
</html>
