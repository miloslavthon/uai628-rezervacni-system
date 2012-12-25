<%@ Page Title="Správa událostí" Language="C#" MasterPageFile="~/Poskytovatel/Poskytovatel.master" AutoEventWireup="true" CodeBehind="Udalosti.aspx.cs" Inherits="RezervacniSystem.Web.Poskytovatel.Udalosti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				<label>Založení nové události</label>
				<asp:Label ID="lblNovaUdalost" runat="server" Text="Název události:" />
				<asp:TextBox ID="txtNovaUdalost" runat="server" />
				<asp:Button ID="btnZalozitUdalost" runat="server" Text="Vytvořit" OnClick="btnZalozitUdalost_Click" />
				<br />
				<asp:Label ID="lblChybaPriZalozeniUdalosti" runat="server" CssClass="message-error" />
			</section>

			<hr />

			<section>
				<label>Seznam událostí</label>
				<asp:GridView ID="gvUdalosti" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvUdalosti_RowCommand">
					<EmptyDataTemplate>
						Nejsou vytvořeny žádené události.
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Název události" DataField="Nazev" ItemStyle-Width="20%" />
						<asp:TemplateField HeaderText="Stav">
							<ItemTemplate><%# (bool)Eval("Zverejneno") ? "Zveřejněno" : "Nezveřejněno" %></ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField HeaderText="Popis" DataField="Popis" ItemStyle-Width="80%" />
						<asp:ButtonField Text="Upravit" CommandName="Upravit" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
