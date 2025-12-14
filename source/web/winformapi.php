<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="x-xrds-location" content="https://admin.endora.cz/openid.xml"/>
            <title>Přihlášení | Administrace</title>
    
                            <script>
            var dataLayer = dataLayer || []; // Google Tag Manager
        </script>

        <!-- Google Tag Manager -->
        <script>
            (function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
            new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
            j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
            'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
            })(window,document,'script','dataLayer','GTM-N9D3C5QR');
        </script>
        <!-- End Google Tag Manager -->
    
    <link href="/assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet">

    <link href="/assets_custom/css/OpenSans.css" rel="preload" as="style" crossorigin>
    <link href="/assets_custom/css/OpenSans.css" rel="stylesheet" crossorigin>
    <link href="/assets_custom/css/Roboto.css" rel="preload" as="style" crossorigin>
    <link href="/assets_custom/css/Roboto.css" rel="stylesheet" crossorigin>

    <link href="/assets/admin/pages/css/login.css" rel="stylesheet">
    <link href="/assets/global/css/components.css" rel="stylesheet">
    <link href="/assets/admin/layout/css/layout.css" rel="stylesheet">
            <link href="/assets_custom/css/login_custom_end.css?v=1" rel="stylesheet">
                <link rel="shortcut icon" href="/assets_custom/img/end/favicon.ico" type="image/x-icon" />
        
    
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesnt work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

<body class="login">


    <!-- Google Tag Manager -->
    <noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-N9D3C5QR"
    height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <!-- End Google Tag Manager -->


<!-- BEGIN SIDEBAR TOGGLER BUTTON -->
<div class="menu-toggler sidebar-toggler"></div>
<!-- END SIDEBAR TOGGLER BUTTON -->
<!-- BEGIN LOGO -->
<div class="logo">
            <img class="img-responsive logo_login_top" src="/assets_custom/img/end/end_admin.svg" alt="logo" width="auto" height="auto">
    </div>
<!-- END LOGO -->

<!-- BEGIN MESSAGES AND ERRORS -->
    <div class="alert alert-danger">
        <button type="button" class="close" data-close="alert"></button>
                    Neoprávněná akce<br/>
            </div>

<div class="alert alert-danger alert-danger-check-input display-hide">
    <button type="button" class="close" data-close="alert"></button>
    Nesprávně vyplněný formulář<br/>
</div>

<div class="custom-alert" id="custom-alert">
</div>
<!-- END MESSAGES AND ERRORS -->
<!-- BEGIN LOGIN -->
<!--  <div class="endora-login-warning">
    <img  src="/assets_custom/img/end/red-warning-triangle.svg" width="auto" height="auto">
    <div>Úpravu nastavení dosud nezmigrovaných služeb u účtů založených před 08/2025 provedete <a href="https://webadmin.endora.cz/">ZDE</a>.</div>
</div>
 -->
<!--  -->
<div class="content">
    
    <!-- BEGIN LOGIN FORM -->
    <form class="login-form" role="form" method="POST" action="/auth/login">
        <input type="hidden" name="_token" value="ZfGT6bMyfJMa83kZ9L7Jz16jQYzkB14nKYekpaK8">
        <h3 class="form-title">Přihlaste se</h3>

        <div class="form-group first">
            <label>E-mail / uživatelské jméno</label>
            <input type="text" name="name" autofocus/>
        </div>
        <div class="form-group">
            <label>Heslo</label>
            <input type="password" name="password"/>
        </div>

        <div class="form-group">
            <label><input type="checkbox" name="remember_me" value="1"/> Neodhlašujte mě</label>
        </div>

        <button type="submit" id="wpan20-login-button">Přihlásit se</button>
        <div class="login-options">
            <div class="sb-button-mojeid wpan20-login-but-fix" onclick="window.location.replace('/auth/mojeid-redir')"><span>Přihlásit se přes MojeID</span></div>
            <div class="line lost-pass">Zapomenuté <a href="/auth/lost">heslo</a>?</div>
                            <div class="line second">
                                                <a href="/lang/en_US">English</a> |                                <a href="/lang/sk_SK">Slovensky</a>                                 </div>
            
        </div>
        <script type="application/javascript">
			document.addEventListener("DOMContentLoaded", function() {
				$('form[action="/auth/login"]').on('submit', function () {
					$(this).find('button[type="submit"]').prop('disabled', true);
				});
			});
        </script>

    </form>
    <!-- END LOGIN FORM -->

</div>
<div class="copyright">
        2025 &copy; <a href="https://endora.cz" class="general_terms_link" target="_blank">Endora</a> &nbsp;&nbsp; 
     <br><a href="https://www.endora.cz/vop" class="general_terms_link" target="_blank">Všeobecné obchodní podmínky</a>    </div>
<!-- END LOGIN -->
<!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
<!-- BEGIN CORE PLUGINS -->
<!--[if lt IE 9]>
<script src="/assets/global/plugins/respond.min.js"></script>
<script src="/assets/global/plugins/excanvas.min.js"></script>
<![endif]-->
<script src="/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
<script src="/assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
<script src="/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="/assets/global/scripts/app.js" type="text/javascript"></script>
<script src="/assets_custom/scripts/login_custom.js?v=1" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->

<script>
	document.addEventListener("DOMContentLoaded", function() {
        Login.init();   // init login on "ENTER" submit

		const allKeys = Object.keys(window.localStorage);
		const toBeDeleted = allKeys.filter(value => {
			return (new RegExp('_tld_').test(value))
		});
		toBeDeleted.forEach(value => {
			localStorage.removeItem(value);
		});
	});
</script>
<!-- END JAVASCRIPTS -->

</body>


</html>
