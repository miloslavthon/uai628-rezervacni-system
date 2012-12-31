<%@ Page Title="Vyhledání volných termínů" Language="C#" MasterPageFile="~/Klient/Klient.master" AutoEventWireup="true" CodeBehind="VyhledaniTerminu.aspx.cs" Inherits="RezervacniSystem.Web.Klient.VyhledaniTerminu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<section>
		<h1><asp:Literal ID="litUdalost" runat="server"></asp:Literal></h1>
		<em><asp:Literal ID="litPoskytovatel" runat="server"></asp:Literal></em>
		<p><asp:Literal ID="litPopisUdalosti" runat="server"></asp:Literal></p>
		<p>Maximální počet účastníků: <asp:Literal ID="litMaximalniPocetUcastniku" runat="server"></asp:Literal></p>
	</section>

	<hr />

	<section>
		<label>Přehled dostupných termínů</label>

		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				Týden od <asp:Label ID="lblDatumOd" runat="server" Font-Bold="true"></asp:Label> do <asp:Label ID="lblDatumDo" runat="server" Font-Bold="true"></asp:Label>
				<asp:GridView ID="gvTerminy" runat="server" DataKeyNames="IdTerminuUdalosti,Datum" AutoGenerateColumns="false" OnRowCommand="gvTerminy_RowCommand">
					<EmptyDataTemplate>
						Pro toto období nejsou k dispozici žádné volné termíny.
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Datum" DataField="Datum" ItemStyle-Width="22%" DataFormatString="{0:g}" />
						<asp:BoundField HeaderText="Doba trvání" DataField="DobaTrvani" ItemStyle-Width="15%" DataFormatString="{0:g}" />
						<asp:BoundField HeaderText="Uzávěrka rezervací před termínem" DataField="UzaverkaRezervaci" ItemStyle-Width="18%" />
						<asp:BoundField HeaderText="Poznámka" DataField="Poznamka" ItemStyle-Width="45%" />
						<asp:ButtonField Text="Rezervovat" CommandName="Rezervovat" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
				<asp:Button ID="btnPredchoziTyden" runat="server" Text="<< Předchozí týden" Enabled="false" OnClick="btnPredchoziTyden_Click" />
				<asp:Button ID="btnDalsiTyden" runat="server" Text="Další týden >>" OnClick="btnDalsiTyden_Click" />
				<br />
				<asp:Label ID="lblZpravaORezervaci" runat="server" CssClass="message-success" Visible="false" />
				<asp:Label ID="lblChybaPriRezervaciTerminu" runat="server" CssClass="message-error" Visible="false" />
			</ContentTemplate>
		</asp:UpdatePanel>

	</section>

</asp:Content>
