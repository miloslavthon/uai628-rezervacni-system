<%@ Page Title="Správa účtu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="RezervacniSystem.Web.Account.Manage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<hgroup class="title">
		<h1><%: Title %>.</h1>
	</hgroup>

	<section id="passwordForm">
		<asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
			<p class="message-success"><%: SuccessMessage %></p>
		</asp:PlaceHolder>

		<p>Jste přihlášen jako <strong><%: User.Identity.Name %></strong>.</p>

		<asp:PlaceHolder runat="server" ID="changePassword">
			<h3>Změna hesla</h3>
			<asp:ChangePassword runat="server" CancelDestinationPageUrl="~/" ViewStateMode="Disabled" RenderOuterTable="false" SuccessPageUrl="Manage.aspx?m=ChangePwdSuccess">
				<ChangePasswordTemplate>
					<p class="validation-summary-errors">
						<asp:Literal runat="server" ID="FailureText" />
					</p>
					<fieldset class="changePassword">
						<legend>Změna hesla</legend>
						<ol>
							<li>
								<asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Současné heslo</asp:Label>
								<asp:TextBox runat="server" ID="CurrentPassword" CssClass="passwordEntry" TextMode="Password" />
								<asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
									CssClass="field-validation-error" ErrorMessage="Současné heslo musí být vyplněno."
									ValidationGroup="ChangePassword" />
							</li>
							<li>
								<asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">Nové heslo</asp:Label>
								<asp:TextBox runat="server" ID="NewPassword" CssClass="passwordEntry" TextMode="Password" />
								<asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
									CssClass="field-validation-error" ErrorMessage="Heslo musí být vyplněno."
									ValidationGroup="ChangePassword" />
							</li>
							<li>
								<asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Potvrzení nového hesla</asp:Label>
								<asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="passwordEntry" TextMode="Password" />
								<asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
									CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Potvrzení hesla musí být vyplněno."
									ValidationGroup="ChangePassword" />
								<asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
									CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Potvrzení hesla se neshoduje se zadaným novým heslem."
									ValidationGroup="ChangePassword" />
							</li>
						</ol>
						<asp:Button runat="server" CommandName="ChangePassword" Text="Změnit heslo" ValidationGroup="ChangePassword" />
					</fieldset>
				</ChangePasswordTemplate>
			</asp:ChangePassword>
		</asp:PlaceHolder>
	</section>

</asp:Content>
