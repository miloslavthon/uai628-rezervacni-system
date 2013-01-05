<%@ Page Title="Přehled rezervovaných termínů" Language="C#" MasterPageFile="~/Poskytovatel/Poskytovatel.master" AutoEventWireup="true" CodeBehind="PrehledRezervovanychTerminu.aspx.cs" Inherits="RezervacniSystem.Web.Poskytovatel.PrehledRezervovanychTerminu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<label>Přehled událostí</label>
				<asp:CheckBox ID="chkPouzeZverejnene" runat="server" CssClass="checkbox" Checked="true" Text="zobrazit pouze zveřejněné události" AutoPostBack="true" OnCheckedChanged="chkPouzeZverejnene_CheckedChanged" />
				<asp:GridView ID="gvUdalosti" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvUdalosti_RowCommand" EmptyDataText="Nejsou k dispozici žádé události">
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Název události" DataField="Nazev" ItemStyle-Width="30%" />
						<asp:BoundField HeaderText="Popis události" DataField="Popis" ItemStyle-Width="70%" />
						<asp:ButtonField Text="Zobrazit termíny" CommandName="ZobrazitTerminy" ControlStyle-Width="110px" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
					<SelectedRowStyle BackColor="#b8ddeb" />
				</asp:GridView>
			</section>

			<hr />

			<section>
				<label>Přehled rezervovaných termínů</label>
				Týden od <asp:Label ID="lblDatumOd" runat="server" Font-Bold="true">-</asp:Label> do <asp:Label ID="lblDatumDo" runat="server" Font-Bold="true">-</asp:Label>
				<asp:HiddenField ID="hdnIdUdalosti" runat="server" />
				<asp:GridView ID="gvTerminy" runat="server" DataKeyNames="IdTerminuRezervace" AutoGenerateColumns="false" OnRowDataBound="gvTerminy_RowDataBound" OnRowCommand="gvTerminy_RowCommand">
					<EmptyDataTemplate>
						V tomto období nejsou rezervovány žádné termíny.
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Datum" DataField="Datum" ItemStyle-Width="22%" DataFormatString="{0:g}" />
						<asp:BoundField HeaderText="Doba trvání" DataField="DobaTrvani" ItemStyle-Width="15%" />
						<asp:BoundField HeaderText="Poznámka" DataField="Poznamka" ItemStyle-Width="45%" />
						<asp:BoundField HeaderText="Počet rezervací" DataField="PocetRezervaci" ItemStyle-Width="18%" />
						<asp:ButtonField Text="Přehled rezervací" CommandName="PrehledRezervaci" ControlStyle-Width="110px" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
				<asp:Button ID="btnPredchoziTyden" runat="server" Text="<< Předchozí týden" Enabled="false" OnClick="btnPredchoziTyden_Click" />
				<asp:Button ID="btnDalsiTyden" runat="server" Text="Další týden >>" Enabled="false" OnClick="btnDalsiTyden_Click" />
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
