<%@ Page Title="Úvod" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RezervacniSystem.Web.Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
	<section class="featured">
		<div class="content-wrapper">
			<hgroup class="title">
				<h1><%: Title %>.</h1>
				<h2>Rezervační systém pro víceoborové použití.</h2>
			</hgroup>
			<p>
				Pro více informací o tomto projektu navštivte jeho domovskou stránku na adrese <a href="http://code.google.com/p/uai628-rezervacni-system/" title="UAI628 - Rezervační systém">http://code.google.com/p/uai628-rezervacni-system/</a>.
				V sekci <mark><a href="http://code.google.com/p/uai628-rezervacni-system/w/list" title="UAI628 - Rezervační systém">Wiki</a></mark> na stránce projektu naleznete původní zadání projektu a další informace o jeho funcionalitě. V sekci <mark><a href="http://code.google.com/p/uai628-rezervacni-system/downloads/list" title="UAI628 - Rezervační systém">Downloads</a></mark> naleznete projektovou dokumentaci.
				Pokud naleznete v aplikaci nějaký problém, nebo máte-li nějaký námět na vylepšení, můžete jej zadat do seznamu požadavků v sekci <mark><a href="http://code.google.com/p/uai628-rezervacni-system/issues/list" title="UAI628 - Rezervační systém">Issues</a></mark> na stránce projektu.
			</p>
		</div>
	</section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<h3>Pokud jste zde poprvé, vyberte si z následujících bodů ten, který Vám nejvíce vyhovuje:</h3>
	<ol class="round">
		<li class="one">
			<h5>Klient</h5>
			Pokud chcete aktivně využívat rezervační systém jako klient, zaregistrujte si uživatelský účet a pokračujte do sekce pro klienty.
			Každý registrovaný uživatel automaticky získává oprávnění klienta.
		</li>
		<li class="two">
			<h5>Poskytovatel služeb</h5>
			Pokud chcete pomocí rezervačního systému poskytovat své služby veřejnosti, zaregistrujte si uživatelský účet a kontaktujte provozovatele. Po uzavření smlouvy Vám bude zpřístupněna sekce pro poskytovatele.
		</li>
		<li class="three">
			<h5>Programátor</h5>
			Pokud se chcete naučit naprogramovat podobný rezervační systém, přihlaste se ke studiu oboru Aplikovaná informatika na Přírodovědecké fakultě Jihočeské univerzity.
		</li>
	</ol>
</asp:Content>
