<%@ Page Title="Přehled registrovaných klientů" Language="C#" MasterPageFile="~/Poskytovatel/Poskytovatel.master" AutoEventWireup="true" CodeBehind="PrehledKlientu.aspx.cs" Inherits="RezervacniSystem.Web.Poskytovatel.PrehledKlientu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<label>Požadavky na registraci klientů</label>
				<asp:GridView ID="gvPozadavkyNaRegistraci" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvPozadavkyNaRegistraci_RowDataBound" OnRowCommand="gvPozadavkyNaRegistraci_RowCommand">
					<EmptyDataTemplate>
						Nejsou k dispozici žádné požadavky na registraci klientů
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Datum" DataField="Datum" ItemStyle-Width="18%" DataFormatString="{0:g}" />
						<asp:TemplateField HeaderText="Jméno klienta">
							<ItemTemplate><%# Eval("Klient.Prijmeni") + " " + Eval("Klient.Jmeno") %></ItemTemplate>
							<ItemStyle Width="35%" />
						</asp:TemplateField>
						<asp:BoundField HeaderText="Adresa" DataField="Klient.Adresa" ItemStyle-Width="47%" />
						<asp:ButtonField Text="Schválit" CommandName="Schvalit" />
						<asp:ButtonField Text="Zamítnout" CommandName="Zamitnout" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
				<br />
				<asp:Label ID="lblZpravaORegistraci" runat="server" CssClass="message-success" Visible="false" />
				<asp:Label ID="lblChybaPriRegistraci" runat="server" CssClass="message-error" Visible="false" />
			</section>

			<hr />

			<section>
				<label>Přehled registrovaných klientů</label>
				<asp:GridView ID="gvKlienti" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvKlienti_RowDataBound" OnRowCommand="gvKlienti_RowCommand">
					<EmptyDataTemplate>
						Nejsou k dispozici žádní registrovaní klienti
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:TemplateField HeaderText="Jméno klienta">
							<ItemTemplate><%# Eval("Prijmeni") + " " + Eval("Jmeno") %></ItemTemplate>
							<ItemStyle Width="40%" />
						</asp:TemplateField>
						<asp:BoundField HeaderText="Adresa" DataField="Adresa" ItemStyle-Width="60%" />
						<asp:ButtonField Text="Zrušit registraci" CommandName="ZrusitRegistraci" ControlStyle-Width="100px" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
