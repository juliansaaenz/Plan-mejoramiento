<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="GestionFichas.aspx.cs" Inherits="PlanMejoramiento.Vista.Administrador.GestionFichas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="container mx-auto p-6">
        <h2 class="text-2xl font-bold mb-6">Gestión de Fichas de Formación</h2>

        <div class="bg-white p-6 rounded-lg shadow-md mb-8">
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                
                <div>
                    <label class="block font-semibold">Número de Ficha:</label>
                    <asp:TextBox ID="txtNumeroFicha" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Programa:</label>
                    <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="w-full border p-2 rounded"></asp:DropDownList>
                </div>

                <div>
                    <label class="block font-semibold">Jornada:</label>
                    <asp:DropDownList ID="ddlJornada" runat="server" CssClass="w-full border p-2 rounded">
                        <asp:ListItem Text="-- Seleccione Jornada --" Value="" />
                        <asp:ListItem Text="Diurna" Value="Diurna" />
                        <asp:ListItem Text="Nocturna" Value="Nocturna" />
                        <asp:ListItem Text="Mixta" Value="Mixta" />
                    </asp:DropDownList>
                </div>

                <div>
                    <label class="block font-semibold">Fecha Inicio:</label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>

                <div>
                    <label class="block font-semibold">Fecha Fin:</label>
                    <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date" CssClass="w-full border p-2 rounded"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Ficha" 
                OnClick="btnRegistrar_Click" 
                CssClass="mt-6 bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700" />
            
            <asp:Label ID="lblError" runat="server" CssClass="text-red-600 mt-4 block font-bold"></asp:Label>
        </div>

        <div class="bg-white p-6 rounded-lg shadow-md">
            <asp:GridView ID="gvFichas" runat="server" AutoGenerateColumns="False" CssClass="w-full text-left border-collapse">
                <Columns>
                    <asp:BoundField DataField="NumeroFicha" HeaderText="Número Ficha" />
                    <asp:BoundField DataField="Jornada" HeaderText="Jornada" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="F. Inicio" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="FechaFinalizacion" HeaderText="F. Fin" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>