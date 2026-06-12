<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="ConsultarAprendices.aspx.cs" Inherits="PlanMejoramiento.Vista.Instructor.ConsultarAprendices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="space-y-6">
        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
            <div>
                <h1 class="text-2xl font-black text-gray-800 tracking-tight">Fichas y Aprendices Asignados</h1>
                <p class="text-sm text-gray-500 mt-1">Selecciona una ficha de caracterización para gestionar los estados académicos y planes de mejoramiento.</p>
            </div>
            <span class="bg-blue-50 text-blue-700 text-xs font-bold uppercase tracking-wider px-3 py-1 rounded-full border border-blue-200">
                Módulo Instructor
            </span>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="p-4 rounded-xl text-sm flex items-start gap-3 shadow-sm">
            <i id="iconoMensaje" runat="server" class="fas text-base mt-0.5"></i>
            <div>
                <asp:Label ID="lblTextoMensaje" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
            <div class="max-w-md">
                <label class="block text-xs font-bold text-gray-700 uppercase tracking-wider mb-2">Seleccione la Ficha de Formación</label>
                <div class="flex gap-4">
                    <asp:DropDownList ID="ddlFichasAsignadas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFichasAsignadas_SelectedIndexChanged"
                                      CssClass="w-full px-4 py-2.5 rounded-xl border border-gray-200 bg-white focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10 transition text-sm text-gray-800 font-medium cursor-pointer">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 space-y-4">
            <div class="border-b border-gray-100 pb-3 flex justify-between items-center">
                <h2 class="font-bold text-gray-800 text-lg flex items-center gap-2">
                    <i class="fas fa-users text-blue-600"></i> 
                    Listado de Aprendices Matriculados
                </h2>
                <asp:Label ID="lblContador" runat="server" CssClass="text-xs font-bold bg-gray-100 text-gray-600 px-2.5 py-1 rounded-md"></asp:Label>
            </div>

            <div class="overflow-x-auto rounded-xl border border-gray-100">
                <asp:GridView ID="gvAprendicesFicha" runat="server" AutoGenerateColumns="False" DataKeyNames="IdAprendiz"
                              OnRowCommand="gvAprendicesFicha_RowCommand"
                              CssClass="w-full min-w-full text-left text-sm text-gray-600 border-collapse">
                    <HeaderStyle CssClass="bg-gray-50 border-b border-gray-100 text-gray-700 uppercase tracking-wider text-xs font-bold p-4" />
                    <RowStyle CssClass="border-b border-gray-50 hover:bg-gray-50/50 transition p-4 font-medium" />
                    <Columns>
                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Documento" ItemStyle-CssClass="p-4 text-gray-900 font-bold" />
                        <asp:TemplateField HeaderText="Aprendiz" ItemStyle-CssClass="p-4 text-gray-800">
                            <ItemTemplate>
                                <%# Eval("Nombres") %> <%# Eval("Apellidos") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Correo" HeaderText="Correo Electrónico" ItemStyle-CssClass="p-4 text-xs text-gray-500 font-mono" />
                        
                        <asp:TemplateField HeaderText="Estado Académico" ItemStyle-CssClass="p-4">
                            <ItemTemplate>
                                <span class='<%# ObtenerEstiloEstado(Eval("Estado").ToString()) %>'>
                                    <%# Eval("Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="p-4">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnGestionar" runat="server" CommandName="Gestionar" CommandArgument='<%# Eval("IdAprendiz") %>'
                                                CssClass="inline-flex items-center justify-center py-2 px-3.5 rounded-xl bg-blue-600 hover:bg-blue-700 text-white font-bold transition shadow-sm text-xs gap-2 cursor-pointer">
                                    <i class="fas fa-folder-open"></i> Historial y Planes
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center py-12 text-gray-400 font-medium">
                            <i class="fas fa-filter text-4xl mb-3 block text-gray-300"></i>
                            Por favor, seleccione una ficha de la lista superior para cargar los aprendices asignados.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>