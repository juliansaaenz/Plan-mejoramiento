<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="GestionFichas.aspx.cs" Inherits="PlanMejoramiento.Vista.Administrador.GestionFichas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="container mx-auto p-6">
        <h2 class="text-2xl font-bold mb-6">Gestión de Programas de Formación</h2>

        <div class="bg-white p-6 rounded-lg shadow-md mb-8">
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                
                <div>
                    <label class="block font-semibold">Código del Programa:</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Nombre del Programa:</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Versión:</label>
                    <asp:TextBox ID="txtVersion" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Nivel de Formación:</label>
                    <asp:DropDownList ID="ddlNivel" runat="server" CssClass="w-full border p-2 rounded">
                        <asp:ListItem Text="-- Seleccione Nivel --" Value="" />
                        <asp:ListItem Text="Tecnólogo" Value="Tecnólogo" />
                        <asp:ListItem Text="Técnico" Value="Técnico" />
                        <asp:ListItem Text="Operario" Value="Operario" />
                    </asp:DropDownList>
                </div>

                <div>
                    <label class="block font-semibold">Duración (Meses):</label>
                    <asp:TextBox ID="txtDuracion" runat="server" TextMode="Number" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Programa" 
                OnClick="btnRegistrar_Click" 
                CssClass="mt-6 bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700" />
            
            <asp:Label ID="lblError" runat="server" CssClass="text-red-600 mt-4 block font-bold"></asp:Label>
        </div>

        <div class="bg-white p-6 rounded-lg shadow-md">
            <asp:GridView ID="gvProgramas" runat="server" AutoGenerateColumns="False" CssClass="w-full text-left border-collapse">
                <Columns>
                    <asp:BoundField DataField="CodigoPrograma" HeaderText="Código" />
                    <asp:BoundField DataField="NombrePrograma" HeaderText="Nombre" />
                    <asp:BoundField DataField="Version" HeaderText="Versión" />
                    <asp:BoundField DataField="NivelFormacion" HeaderText="Nivel" />
                    <asp:BoundField DataField="Duracion" HeaderText="Duración (Meses)" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>