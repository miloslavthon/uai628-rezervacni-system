<%@ Page Title="Přehled provedených rezervací" Language="C#" MasterPageFile="~/Poskytovatel/Poskytovatel.master" AutoEventWireup="true" CodeBehind="PrehledRezervaci.aspx.cs" Inherits="RezervacniSystem.Web.Poskytovatel.PrehledRezervaci" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<h1><asp:Literal ID="litUdalost" runat="server"></asp:Literal></h1>
				<em><asp:Literal ID="litPopisUdalosti" runat="server"></asp:Literal></em>
				<p>Datum: <asp:Label ID="lblDatum" runat="server" Font-Bold="true"></asp:Label></p>
				<p>Doba trvání: <asp:Label ID="lblDobaTrvani" runat="server" Font-Bold="true"></asp:Label></p>
				<p>Poznámka: <asp:Literal ID="litPoznamka" runat="server"></asp:Literal></p>
				<p>Maximální počet účastníků: <asp:Literal ID="litMaximalniPocetUcastniku" runat="server"></asp:Literal></p>
				<p>Počet rezervací: <asp:Literal ID="litPocetRezervaci" runat="server"></asp:Literal></p>
			</section>

			<hr />

			<section>
				<label>Seznam klientů</label>

				<asp:GridView ID="gvRezervace" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowDataBound="gvRezervace_RowDataBound" OnRowCommand="gvRezervace_RowCommand" Width="100%">
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:TemplateField HeaderText="Jméno klienta">
							<ItemTemplate><%# Eval("Klient.Prijmeni") + " " + Eval("Klient.Jmeno") %></ItemTemplate>
							<ItemStyle Width="40%" />
						</asp:TemplateField>
						<asp:BoundField HeaderText="Adresa" DataField="Klient.Adresa" ItemStyle-Width="60%" />
						<asp:ButtonField Text="Zrušit rezervaci" CommandName="ZrusitRezervaci" ControlStyle-Width="100px" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
				<br />
				<asp:Label ID="lblZpravaOZruseniRezervace" runat="server" CssClass="message-success" Visible="false" />
				<asp:Label ID="lblChybaPriZruseniRezervace" runat="server" CssClass="message-error" Visible="false" />

			</section>

		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
