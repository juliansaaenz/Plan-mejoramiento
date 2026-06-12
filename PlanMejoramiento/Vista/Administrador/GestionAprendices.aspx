<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="GestionFichas.aspx.cs" Inherits="PlanMejoramiento.Vista.Administrador.GestionFichas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div class="container mx-auto p-6">
        <h2 class="text-2xl font-bold mb-6">Gestión de Aprendices</h2>

        <div class="bg-white p-6 rounded-lg shadow-md mb-8">
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                
                <div>
                    <label class="block font-semibold">Número de Documento:</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Nombres:</label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Apellidos:</label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Correo Electrónico:</label>
                    <asp:TextBox ID="txtCorreo" runat="server" TextMode="Email" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Ficha de Formación:</label>
                    <asp:DropDownList ID="ddlFicha" runat="server" CssClass="w-full border p-2 rounded"></asp:DropDownList>
                </div>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Aprendiz" 
                OnClick="btnRegistrar_Click" 
                CssClass="mt-6 bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700" />
            
            <asp:Label ID="lblError" runat="server" CssClass="text-red-600 mt-4 block font-bold"></asp:Label>
        </div>

        </div>

</asp:Content>