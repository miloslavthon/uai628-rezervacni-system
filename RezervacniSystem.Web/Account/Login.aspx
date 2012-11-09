﻿<%@ Page Title="Přihlášení" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RezervacniSystem.Web.Account.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<hgroup class="title">
		<h1><%: Title %>.</h1>
	</hgroup>
	<section id="loginForm">
		<h2>Use a local account to log in.</h2>
		<asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
			<LayoutTemplate>
				<p class="validation-summary-errors">
					<asp:Literal runat="server" ID="FailureText" />
				</p>
				<fieldset>
					<legend>Přihlašovací formulář</legend>
					<ol>
						<li>
							<asp:Label runat="server" AssociatedControlID="UserName">Uživatelské jméno</asp:Label>
							<asp:TextBox runat="server" ID="UserName" />
							<asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
						</li>
						<li>
							<asp:Label runat="server" AssociatedControlID="Password">Heslo</asp:Label>
							<asp:TextBox runat="server" ID="Password" TextMode="Password" />
							<asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
						</li>
						<li>
							<asp:CheckBox runat="server" ID="RememberMe" />
							<asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Zapamatovat přihlášení?</asp:Label>
						</li>
					</ol>
					<asp:Button runat="server" CommandName="Login" Text="Přihlásit" />
				</fieldset>
			</LayoutTemplate>
		</asp:Login>
		<p>
			<asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Zaregistrujte se</asp:HyperLink>, pokud nemáte účet.
		</p>
	</section>

</asp:Content>
