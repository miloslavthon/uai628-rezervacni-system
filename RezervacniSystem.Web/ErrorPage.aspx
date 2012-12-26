<%@ Page Title="Chyba aplikace" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="RezervacniSystem.Web.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

	<hgroup class="title">
		<h2><%: Page.Title %>.</h2>
	</hgroup>

	<section>
		<asp:Label ID="lblMessage" runat="server" CssClass="message-error"></asp:Label>
	</section>

</asp:Content>
