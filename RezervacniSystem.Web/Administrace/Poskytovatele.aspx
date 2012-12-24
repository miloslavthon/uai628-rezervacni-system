<%@ Page Title="Správa poskytovatelů" Language="C#" MasterPageFile="~/Administrace/Administrace.master" AutoEventWireup="true" CodeBehind="Poskytovatele.aspx.cs" Inherits="RezervacniSystem.Web.Administrace.Poskytovatele" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<label>Vytvoření nového poskytovatele</label>
				<asp:Label ID="lblNovyPoskytovatel" runat="server" Text="Název poskytovatele:" />
				<asp:TextBox ID="txtNazevPoskytovatele" runat="server" />
				<asp:Button ID="btnVytvoritPoskytovatele" runat="server" Text="Vytvořit" OnClick="btnVytvoritPoskytovatele_Click" />
				<br />
				<asp:Label ID="lblChybaPriVytvoreniPoskytovatele" runat="server" CssClass="message-error" />
			</section>

			<hr />

			<section>
				<label>Seznam poskytovatelů</label>
				<asp:GridView ID="gvPoskytovatele" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvPoskytovatele_RowCommand">
					<Columns>
						<asp:BoundField HeaderText="Název" DataField="Nazev" />
						<asp:ButtonField Text="Vybrat" CommandName="Vybrat" />
						<asp:ButtonField Text="Odstranit" CommandName="Odstranit" />
					</Columns>
				</asp:GridView>
			</section>

			<hr />

			<section>
				<label>Nastavení poskytovatele</label>

				<asp:Panel ID="pnlZadnyPoskytovatele" runat="server">Není vybrán žádný poskytovatel</asp:Panel>
				<asp:Panel ID="pnlDetailPoskytovatele" runat="server">
					<asp:HiddenField ID="hdnIdPoskytovatele" runat="server" />
					Název: <asp:Label ID="lblNazev" runat="server" CssClass="strong" />
					<br />
					Uživatelské jméno správce: <asp:TextBox ID="txtLoginPoskytovatele" runat="server" />
					<br />
					Maximální počet zveřejněných událostí: <asp:TextBox ID="txtMaximalniPocetZverejnenychUdalosti" runat="server" /><asp:RequiredFieldValidator ID="vldMaximalniPocetZverejnenychUdalosti" runat="server" ErrorMessage="Údaj musí být vyplněn" ControlToValidate="txtMaximalniPocetZverejnenychUdalosti" Display="Dynamic" CssClass="message-error"></asp:RequiredFieldValidator><asp:RangeValidator ID="vldMaximalniPocetZverejnenychUdalosti_Format" runat="server" ErrorMessage="Zadejte hodnotu od 0 do 9999" ControlToValidate="txtMaximalniPocetZverejnenychUdalosti" MinimumValue="0" MaximumValue="9999" Type="Integer" Display="Dynamic" CssClass="message-error"></asp:RangeValidator>
					<br />
					Maximální počet rezervací jednoho klienta: <asp:TextBox ID="txtMaximalniPocetRezervaciJednohoKlienta" runat="server" /><asp:RequiredFieldValidator ID="vldMaximalniPocetRezervaciJednohoKlienta" runat="server" ErrorMessage="Údaj musí být vyplněn" ControlToValidate="txtMaximalniPocetRezervaciJednohoKlienta" Display="Dynamic" CssClass="message-error"></asp:RequiredFieldValidator><asp:RangeValidator ID="vldMaximalniPocetRezervaciJednohoKlienta_Format" runat="server" ErrorMessage="Zadejte hodnotu od 0 do 9999" ControlToValidate="txtMaximalniPocetRezervaciJednohoKlienta" MinimumValue="0" MaximumValue="9999" Type="Integer" Display="Dynamic" CssClass="message-error"></asp:RangeValidator>
					<br />
					Registrace klientů podléhá schválení: <asp:CheckBox ID="chkRegistraceKlientuPodlehaSchvaleni" runat="server" />
					<br />
					Události jsou určeny pro více osob: <asp:CheckBox ID="chkUdalostiProViceOsob" runat="server" />
					<br />
					Události mají opakovaný temín: <asp:CheckBox ID="chkUdalostiMajiOpakovanyTermin" runat="server" />
					<br />
					<asp:Button ID="btnUlozit" runat="server" Text="Uložit" OnClick="btnUlozit_Click" />
				</asp:Panel>
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
