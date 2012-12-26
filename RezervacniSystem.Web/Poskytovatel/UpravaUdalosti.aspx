<%@ Page Title="Úprava události" Language="C#" MasterPageFile="~/Poskytovatel/Poskytovatel.master" AutoEventWireup="true" CodeBehind="UpravaUdalosti.aspx.cs" Inherits="RezervacniSystem.Web.Poskytovatel.UpravaUdalosti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<asp:Panel ID="pnlDetailPoskytovatele" runat="server">
					<asp:HiddenField ID="hdnIdPoskytovatele" runat="server" />
					Název: <asp:TextBox ID="txtNazev" runat="server" MaxLength="255" /><asp:RequiredFieldValidator ID="vldNazev" runat="server" ErrorMessage="Údaj musí být vyplněn" ControlToValidate="txtNazev" Display="Dynamic" CssClass="message-error"></asp:RequiredFieldValidator>
					<br />
					Maximální počet účastníků: <asp:TextBox ID="txtMaximalniPocetUcastniku" runat="server" /><asp:RequiredFieldValidator ID="vldMaximalniPocetUcastniku" runat="server" ErrorMessage="Údaj musí být vyplněn" ControlToValidate="txtMaximalniPocetUcastniku" Display="Dynamic" CssClass="message-error"></asp:RequiredFieldValidator><asp:RangeValidator ID="vldMaximalniPocetUcastniku_Format" runat="server" ErrorMessage="Zadejte hodnotu od 0 do 9999" ControlToValidate="txtMaximalniPocetUcastniku" MinimumValue="0" MaximumValue="9999" Type="Integer" Display="Dynamic" CssClass="message-error"></asp:RangeValidator>
					<br />
					Zveřejněno: <asp:CheckBox ID="chkZverejneno" runat="server" />
					<br />
					Popis:
					<br />
					<asp:TextBox ID="txtPopis" runat="server" TextMode="MultiLine" Rows="3" />
					<br />
					Událost má opakovaný termín: <asp:Label ID="lblOpakovanyTermin" runat="server" Font-Bold="true"></asp:Label>
					<br />
					<asp:Button ID="btnUlozit" runat="server" Text="Uložit" OnClick="btnUlozit_Click" />
					<asp:Button ID="btnZrusitUdalost" runat="server" Text="Zrušit událost" OnClientClick="if (!confirm('Opravdu chcete zrušit vybranou událost včetně všech zveřejněných termínů?')) { return false; }" OnClick="btnZrusitUdalost_Click" />
					<br />
					<asp:Label ID="lblChyba" runat="server" CssClass="message-error" />
				</asp:Panel>
			</section>

			<hr />

			<section>
				<label>Přehled zveřejněných termínů</label>
				<asp:GridView ID="gvTerminy" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvTerminy_RowCommand">
					<EmptyDataTemplate>
						Nejsou zveřejněny žádené termíny.
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Začátek" DataField="Zacatek" ItemStyle-Width="17%" DataFormatString="{0:g}" />
						<asp:BoundField HeaderText="Konec" DataField="Konec" ItemStyle-Width="17%" DataFormatString="{0:g}" />
						<asp:BoundField HeaderText="Uzávěrka rezervací" DataField="UzaverkaRezervaci" ItemStyle-Width="17%" DataFormatString="{0:g}" />
						<asp:BoundField HeaderText="Poznámka" DataField="Poznamka" ItemStyle-Width="49%" />
						<asp:ButtonField Text="Zrušit" CommandName="Zrusit" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
