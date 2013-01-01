<%@ Page Title="Přehled vlastních rezervací" Language="C#" MasterPageFile="~/Klient/Klient.master" AutoEventWireup="true" CodeBehind="Prehled.aspx.cs" Inherits="RezervacniSystem.Web.Klient.Prehled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<section id="secUpozorneni" runat="server">
		<label class="message-error">Upozornění</label>
		Dosud nemáte provedeno osobní nastavení. Bez vyplnění povinných osobních údajů nebude možné provádět rezervace. Nastavení můžete provést <asp:HyperLink ID="lnkZde" runat="server" NavigateUrl="~/Klient/OsobniNastaveni.aspx">zde</asp:HyperLink>.
	</section>

</asp:Content>
