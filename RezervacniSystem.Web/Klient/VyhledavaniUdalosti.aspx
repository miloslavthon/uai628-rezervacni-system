<%@ Page Title="Vyhledávání událsotí" Language="C#" MasterPageFile="~/Klient/Klient.master" AutoEventWireup="true" CodeBehind="VyhledavaniUdalosti.aspx.cs" Inherits="RezervacniSystem.Web.Klient.VyhledavaniUdalosti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>

			<section>
				Poskytovatel: <asp:TextBox ID="txtHledatPoskytovatele" runat="server" />
				<asp:Button ID="btnHledatPoskytovatele" runat="server" Text="Hledat" OnClick="btnHledatPoskytovatele_Click" />
				<asp:Button ID="btnZrusitFiltr" runat="server" Text="Zrušit filtr" OnClick="btnZrusitFiltr_Click" />
				<asp:GridView ID="gvPoskytovatele" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvPoskytovatele_RowCommand">
					<EmptyDataTemplate>
						Zadanému filtru neodpovídá žádný záznam.
					</EmptyDataTemplate>
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Poskytovatel" DataField="Nazev" />
						<asp:TemplateField>
							<ItemTemplate>
								<asp:Image ID="imgSchvaleniRegistrace" runat="server" ImageUrl="~/Images/klic.gif" ToolTip="Registrace u poskytovatele podléhá schválení" Visible="<%# ((RezervacniSystem.Domain.Model.Poskytovatele.Poskytovatel)Container.DataItem).RegistraceKlientuPodlehaSchvaleni %>" />
							</ItemTemplate>
						</asp:TemplateField>
						<asp:ButtonField Text="Zobrazit události" CommandName="ZobrazitUdalosti" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
					<SelectedRowStyle BackColor="#b8ddeb" />
				</asp:GridView>
				<div id="divRegistrace" runat="server" visible="false">
					<hr />
					<label>Upozornění</label>
					<p>Rezervace u vybraného poskytovatele služeb vyžadují schválení registrace klienta.</p>
					<asp:Button ID="btnRegistrovat" runat="server" Text="Registrovat u poskytovatele" OnClick="btnRegistrovat_Click" />
				</div>
				<br />
				<asp:Label ID="lblZpravaORegistrani" runat="server" CssClass="message-success" Visible="false" />
				<asp:Label ID="lblChybaPriRegistraci" runat="server" CssClass="message-error" Visible="false" />
			</section>

			<hr />

			<section>
				<label>Přehled událostí poskytovatele</label>
				<asp:GridView ID="gvUdalosti" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gvUdalosti_RowCommand">
					<EmptyDataRowStyle Font-Italic="true" />
					<Columns>
						<asp:BoundField HeaderText="Název události" DataField="Nazev" ItemStyle-Width="30%" />
						<asp:BoundField HeaderText="Popis události" DataField="Popis" ItemStyle-Width="70%" />
						<asp:ButtonField Text="Vyhledat volné termíny" CommandName="VyhledatTerminy" ControlStyle-Width="150px" />
					</Columns>
					<AlternatingRowStyle BackColor="#e5e5e5" />
				</asp:GridView>
			</section>

		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>
