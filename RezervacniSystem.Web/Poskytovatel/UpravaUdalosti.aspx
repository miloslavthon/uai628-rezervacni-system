<%@ Page Title="Úprava události" Language="C#" MasterPageFile="~/Poskytovatel/Poskytovatel.master" AutoEventWireup="true" CodeBehind="UpravaUdalosti.aspx.cs" Inherits="RezervacniSystem.Web.Poskytovatel.UpravaUdalosti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<asp:Panel ID="pnlDetailPoskytovatele" runat="server">
					<asp:HiddenField ID="hdnIdPoskytovatele" runat="server" />
					Název: <asp:TextBox ID="txtNazev" runat="server" MaxLength="255" /><asp:RequiredFieldValidator ID="vldNazev" runat="server" ErrorMessage="Údaj musí být vyplněn" ControlToValidate="txtNazev" Display="Dynamic" CssClass="message-error"></asp:RequiredFieldValidator>
					<br />
					Maximální počet účastníků: <asp:TextBox ID="txtMaximalniPocetUcastniku" runat="server" TextMode="Number" /><asp:RequiredFieldValidator ID="vldMaximalniPocetUcastniku" runat="server" ErrorMessage="Údaj musí být vyplněn" ControlToValidate="txtMaximalniPocetUcastniku" Display="Dynamic" CssClass="message-error"></asp:RequiredFieldValidator><asp:RangeValidator ID="vldMaximalniPocetUcastniku_Format" runat="server" ErrorMessage="Zadejte hodnotu od 0 do 9999" ControlToValidate="txtMaximalniPocetUcastniku" MinimumValue="0" MaximumValue="9999" Type="Integer" Display="Dynamic" CssClass="message-error"></asp:RangeValidator>
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
					<asp:Label ID="lblChybaPriUkladaniUdalosti" runat="server" CssClass="message-error" Visible="false" />
				</asp:Panel>
			</section>

			<hr />

			<section>
				<label>Nový termín</label>
				<asp:MultiView ID="mvTermin" runat="server">
					<asp:View ID="viewJednorazovyTermin" runat="server">
						Datum:
						<asp:TextBox ID="txtTermin_Datum" runat="server" MaxLength="12" TextMode="Date" Width="130px" />
					</asp:View>
					<asp:View ID="viewOpakovanyTermin" runat="server">
						Den:
						<asp:DropDownList ID="cmbDen" runat="server">
							<asp:ListItem Text="Po" Value="Po" Selected="True" />
							<asp:ListItem Text="Út" Value="Ut" />
							<asp:ListItem Text="St" Value="St" />
							<asp:ListItem Text="Čt" Value="Ct" />
							<asp:ListItem Text="Pá" Value="Pa" />
							<asp:ListItem Text="So" Value="So" />
							<asp:ListItem Text="Ne" Value="Ne" />
						</asp:DropDownList>
					</asp:View>
				</asp:MultiView>
				Čas:
				<asp:TextBox ID="txtTermin_Cas" runat="server" MaxLength="12" TextMode="Time" Width="100px" />
				<span id="spanPlatnyDo" runat="server">Platný do:
				<asp:TextBox ID="txtPlatnyDo" runat="server" MaxLength="12" TextMode="Date" Width="130px" /></span>
				Doba trvání:
				<asp:TextBox ID="txtDobaTrvani_Hodiny" runat="server" MaxLength="2" TextMode="Number" Width="35px" Text="1" /> hod.
				<asp:TextBox ID="txtDobaTrvani_Minuty" runat="server" MaxLength="2" TextMode="Number" Width="35px" Text="0" /> min.
				<br />
				Uzávěrka rezervací před termínem:
				<asp:TextBox ID="txtUzaverkaRezervaci_Dny" runat="server" MaxLength="2" TextMode="Number" Width="35px" Text="0" /> d.
				<asp:TextBox ID="txtUzaverkaRezervaci_Hodiny" runat="server" MaxLength="2" TextMode="Number" Width="35px" Text="1" /> hod.
				<asp:TextBox ID="txtUzaverkaRezervaci_Minuty" runat="server" MaxLength="2" TextMode="Number" Width="35px" Text="0" /> min.
				<br />
				Poznámka:
				<asp:TextBox ID="txtPoznamka" runat="server" MaxLength="255" />
				<asp:Button ID="btnZverejnitNovyTermin" runat="server" Text="Zveřejnit termín" OnClick="btnZverejnitNovyTermin_Click" />
				<br />
				<asp:Label ID="lblChybaPriZverejneniTerminu" runat="server" CssClass="message-error" Visible="false" />
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
						<asp:BoundField HeaderText="Termín" DataField="Termin" ItemStyle-Width="22%" />
						<asp:BoundField HeaderText="Doba trvání" DataField="DobaTrvani" ItemStyle-Width="15%" />
						<asp:BoundField HeaderText="Uzávěrka rezervací před termínem" DataField="UzaverkaRezervaci" ItemStyle-Width="18%" />
						<asp:BoundField HeaderText="Poznámka" DataField="Poznamka" ItemStyle-Width="45%" />
						<asp:ButtonField Text="Zrušit" CommandName="Zrusit" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
