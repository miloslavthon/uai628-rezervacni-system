<%@ Page Title="Správa poskytovatelů" Language="C#" MasterPageFile="~/Administrace/Administrace.master" AutoEventWireup="true" CodeBehind="Poskytovatele.aspx.cs" Inherits="RezervacniSystem.Web.Administrace.Poskytovatele" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<section>
		<label>Vytvoření nového poskytovatele</label>
		<asp:Label ID="lblNovyPoskytovatel" runat="server" Text="Název poskytovatele:" />
		<asp:TextBox ID="txtNazevPoskytovatele" runat="server" />
		<asp:Button ID="btnVytvoritPoskytovatele" runat="server" Text="Vytvořit" OnClick="btnVytvoritPoskytovatele_Click" />
		<br />
		<asp:Label ID="lblChybaPriVytvoreniPoskytovatele" runat="server" CssClass="message-error" />
	</section>

	<section>
		<label>Seznam poskytovatelů</label>
		<asp:GridView ID="gvPoskytovatele" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvPoskytovatele_RowCommand">
			<Columns>
				<asp:BoundField HeaderText="Název" DataField="Nazev" />
				<asp:ButtonField Text="Nastavit" CommandName="Nastavit" />
				<asp:ButtonField Text="Odstranit" CommandName="Odstranit" />
			</Columns>
		</asp:GridView>
	</section>

	<section>
		<label>Nastavení poskytovatele</label>

		<asp:Label ID="lblVyberPoskytovatele" runat="server" Text="Není vybrán žádný poskytovatel" />
		<br />

		<asp:HiddenField ID="hdnIdPoskytovatele" runat="server" />

		Název: <asp:Label ID="lblNazev" runat="server" CssClass="strong" />
		<br />
		Uživatelské jméno: <asp:TextBox ID="txtLoginPoskytovatele" runat="server" />
		<br />
		<asp:Button ID="btnUlozit" runat="server" Text="Uložit" OnClick="btnUlozit_Click" />
	</section>
</asp:Content>
