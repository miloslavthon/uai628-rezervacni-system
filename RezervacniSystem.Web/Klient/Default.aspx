<%@ Page Title="Klient" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RezervacniSystem.Web.Klient.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<hgroup class="title">
		<h1><%: Title %>.</h1>
		<h2>Vyhledávání událostí, termínů a jejich rezervace.</h2>
	</hgroup>

	<section>
		<header>
			<h3>Nadpis:</h3>
		</header>
		<p>
			<span class="label">Popisek:</span>
			<span>údaj</span>
		</p>
	</section>

	<aside>
		<h3>Boční sloupec</h3>
		<p>
			Nějaké informace.
		</p>
		<ul>
			<li><a runat="server" href="~/">Úvod</a></li>
			<li><a runat="server" href="~/">Úvod</a></li>
			<li><a runat="server" href="~/">Úvod</a></li>
		</ul>
	</aside>
</asp:Content>
