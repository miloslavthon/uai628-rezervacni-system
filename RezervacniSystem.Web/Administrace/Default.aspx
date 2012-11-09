<%@ Page Title="Administrace" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RezervacniSystem.Web.Administrace.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<hgroup class="title">
		<h1><%: Title %>.</h1>
		<h2>Správa poskytovatelů a správa uživatelů.</h2>
	</hgroup>
</asp:Content>
