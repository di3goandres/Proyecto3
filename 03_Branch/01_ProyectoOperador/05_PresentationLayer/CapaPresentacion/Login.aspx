<%@ Page Title="" Language="C#" MasterPageFile="~/login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
      <table class="table">
        <tr><td>
            <br />
            <div>
                <table class="tableIndicadores" style="width: 70%; margin-left: 0px">
                </table>
                 <h4></h4>
                <table class="tableIndicadores" style="width: 90%; margin-left: 0px">
                    <tr>
                        <td>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="El nombre de usuario es obligatorio."
                                ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td>

                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>
                <div style="margin-top:auto" class="form-group">
                    <!-- Button -->
                        <div>
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Iniciar Sesión"
                                ValidationGroup="LoginUserValidationGroup" CssClass="loginButton center-block btn_cafe btn-success btn btn-lg" OnClick="LoginButton_Click1" />
                        </div>
				</div>
                        <div style="border-top: 1px solid#888; padding-top:15px; font-size:85%" >
                            Si no puede ingresar 
                            <a class="link_cafe" href="./RestablecerContrasena/RecuperarContrasenaMail.aspx">
                            Haga click aqui y comuniquese con el administrador
                            </a>
                        </div> 
                 <div style="border-top: 1px solid#888; padding-top:15px; font-size:85%" >
                          
                            <a class="link_cafe" href="./Registro/RegistrarPaciente.aspx">
                            Registrese 
                            </a>
                        </div> 
            </div>
        </table>
      </center>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentScripts" runat="Server">
</asp:Content>
