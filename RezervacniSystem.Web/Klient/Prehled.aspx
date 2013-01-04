<%@ Page Title="Přehled" Language="C#" MasterPageFile="~/Klient/Klient.master" AutoEventWireup="true" CodeBehind="Prehled.aspx.cs" Inherits="RezervacniSystem.Web.Klient.Prehled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<section id="secUpozorneni" runat="server">
		<label class="message-error">Upozornění</label>
		Dosud nemáte provedeno osobní nastavení. Bez vyplnění povinných osobních údajů nebude možné provádět rezervace. Nastavení můžete provést <asp:HyperLink ID="lnkZde" runat="server" NavigateUrl="~/Klient/OsobniNastaveni.aspx">zde</asp:HyperLink>.
	</section>

	<hr />

	<section>
		<label>Přehled provedených rezervací</label>

		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<asp:CheckBox ID="chkPouzePlatne" runat="server" CssClass="checkbox" Text="zobrazit pouze aktuálně platné rezervace" AutoPostBack="true" OnCheckedChanged="chkPouzePlatne_CheckedChanged" />
				<asp:GridView ID="gvRezervace" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowDataBound="gvRezervace_RowDataBound" OnRowCommand="gvRezervace_RowCommand">
					<EmptyDataTemplate>
						Nebyly nalezeny žádné rezervace.
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Datum" DataField="Datum" ItemStyle-Width="22%" DataFormatString="{0:g}" />
						<asp:TemplateField HeaderText="Doba trvání" ItemStyle-Width="15%">
							<ItemTemplate><%# RezervacniSystem.Infrastructure.DateTimeUtils.VypisDobu((TimeSpan)Eval("DobaTrvani")) %></ItemTemplate>
						</asp:TemplateField>
						<%--<asp:BoundField HeaderText="Doba trvání" DataField="DobaTrvani" ItemStyle-Width="15%" DataFormatString="{0:g}" />--%>
						<asp:BoundField HeaderText="Událost" DataField="Udalost" ItemStyle-Width="31%" />
						<asp:BoundField HeaderText="Poskytovatel" DataField="Poskytovatel" ItemStyle-Width="32%" />
						<asp:ButtonField Text="Zrušit" CommandName="Zrusit" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
				<br />
				<asp:Label ID="lblChybaPriZruseniTerminu" runat="server" CssClass="message-error" Visible="false" />
			</ContentTemplate>
		</asp:UpdatePanel>
	</section>
</asp:Content>
