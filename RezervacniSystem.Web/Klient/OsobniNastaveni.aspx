<%@ Page Title="Osobní nastavení" Language="C#" MasterPageFile="~/Klient/Klient.master" AutoEventWireup="true" CodeBehind="OsobniNastaveni.aspx.cs" Inherits="RezervacniSystem.Web.Klient.OsobniNastaveni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<asp:HiddenField ID="hdnIdKlienta" runat="server" />
			Jméno: <asp:TextBox ID="txtJmeno" runat="server"></asp:TextBox>
			<br />
			Příjmení: <asp:TextBox ID="txtPrijmeni" runat="server"></asp:TextBox>
			<br />
			Adresa:
			<br />
			<asp:TextBox ID="txtAdresa" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
			<br />
			<asp:Button ID="btnUlozit" runat="server" Text="Uložit nastavení" OnClick="btnUlozit_Click" />
			<br />
			<asp:Label ID="lblZpravaOUlozeni" runat="server" CssClass="message-success" Visible="false" />
			<asp:Label ID="lblChybaPriUlozeni" runat="server" CssClass="message-error" Visible="false" />
		</ContentTemplate>
	</asp:UpdatePanel>

	Změnu hesla můžete provést <asp:HyperLink ID="lnkZmenaHesla" runat="server" NavigateUrl="~/Account/Manage.aspx">zde</asp:HyperLink>.

</asp:Content>
