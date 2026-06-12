<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="HistorialPlanes.aspx.cs" Inherits="PlanMejoramiento.Vista.Instructor.HistorialPlanes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="space-y-6">
        <div class="flex items-center gap-4">
            <a href="ConsultarAprendices.aspx" class="bg-white p-2.5 rounded-xl border border-gray-200 text-gray-500 hover:text-blue-600 hover:border-blue-200 transition shadow-sm">
                <i class="fas fa-arrow-left"></i>
            </a>
            <div>
                <h1 class="text-2xl font-black text-gray-800 tracking-tight">Historial Académico del Aprendiz</h1>
                <p class="text-sm text-gray-500">Consulta los planes de mejoramiento previos y asigna nuevos procesos.</p>
            </div>
        </div>

        <div class="bg-white rounded-2xl shadow-sm border border-gray-100 overflow-hidden">
            <div class="bg-blue-600 px-6 py-3">
                <span class="text-white text-xs font-bold uppercase tracking-widest opacity-80">Datos Básicos</span>
            </div>
            <div class="p-6 grid grid-cols-1 md:grid-cols-4 gap-6">
                <div class="flex items-center gap-4 md:col-span-2 border-r border-gray-100 pr-4">
                    <div class="w-16 h-16 bg-blue-50 rounded-2xl flex items-center justify-center text-blue-600 text-2xl font-black shadow-inner">
                        <asp:Label ID="lblIniciales" runat="server" Text="A"></asp:Label>
                    </div>
                    <div>
                        <h2 class="text-xl font-bold text-gray-800 leading-none mb-1">
                            <asp:Label ID="lblNombreCompleto" runat="server" Text="Cargando..."></asp:Label>
                        </h2>
                        <p class="text-sm text-gray-500 font-medium">Documento: <asp:Label ID="lblDocumento" runat="server"></asp:Label></p>
                    </div>
                </div>
                <div>
                    <span class="block text-[10px] font-bold text-gray-400 uppercase mb-1">Programa de Formación</span>
                    <p class="text-sm font-bold text-gray-700"><asp:Label ID="lblPrograma" runat="server" Text="-"></asp:Label></p>
                </div>
                <div>
                    <span class="block text-[10px] font-bold text-gray-400 uppercase mb-1">Ficha Actual</span>
                    <p class="text-sm font-bold text-emerald-600"><asp:Label ID="lblFicha" runat="server" Text="-"></asp:Label></p>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="p-4 rounded-xl text-sm flex items-start gap-3 shadow-sm bg-blue-50 border-l-4 border-blue-500 text-blue-700">
            <i class="fas fa-info-circle text-base mt-0.5"></i>
            <asp:Label ID="lblTextoMensaje" runat="server"></asp:Label>
        </asp:Panel>

        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 space-y-4">
            <div class="flex justify-between items-center pb-2">
                <h3 class="font-bold text-gray-800 text-lg flex items-center gap-2">
                    <i class="fas fa-history text-blue-600"></i> Planes de Mejoramiento Registrados
                </h3>
                <asp:LinkButton ID="btnNuevoPlan" runat="server" OnClick="btnNuevoPlan_Click"
                                CssClass="bg-emerald-600 hover:bg-emerald-700 text-white font-bold py-2.5 px-5 rounded-xl shadow-lg shadow-emerald-600/20 transition flex items-center gap-2 text-sm cursor-pointer">
                    <i class="fas fa-plus"></i> Crear Nuevo Plan
                </asp:LinkButton>
            </div>

            <div class="overflow-x-auto rounded-xl border border-gray-100">
                <asp:GridView ID="gvHistorial" runat="server" AutoGenerateColumns="False" DataKeyNames="IdPlan"
                              OnRowCommand="gvHistorial_RowCommand"
                              CssClass="w-full min-w-full text-left text-sm text-gray-600 border-collapse">
                    <HeaderStyle CssClass="bg-gray-50 border-b border-gray-100 text-gray-700 uppercase tracking-wider text-xs font-bold p-4" />
                    <RowStyle CssClass="border-b border-gray-50 hover:bg-gray-50/50 transition p-4" />
                    <Columns>
                        <asp:BoundField DataField="FechaAsignacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="p-4 font-mono text-gray-500" />
                        <asp:BoundField DataField="TipoPlan" HeaderText="Tipo" ItemStyle-CssClass="p-4 font-bold" />
                        
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# ObtenerEstiloEstadoPlan(Eval("EstadoPlan").ToString()) %>'>
                                    <%# Eval("EstadoPlan") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Instructor" HeaderText="Asignado por" ItemStyle-CssClass="p-4 text-xs" />
                        
                        <asp:TemplateField HeaderText="Detalles" ItemStyle-CssClass="p-4 text-right">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnVer" runat="server" CommandName="VerDetalle" CommandArgument='<%# Eval("IdPlan") %>'
                                                CssClass="text-blue-600 hover:text-blue-800 font-bold flex items-center justify-end gap-1 group">
                                    Ver proceso <i class="fas fa-chevron-right text-[10px] group-hover:translate-x-1 transition"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center py-16 text-gray-400 font-medium">
                            <i class="fas fa-clipboard-check text-5xl mb-4 block text-gray-200"></i>
                            Este aprendiz no tiene antecedentes de planes de mejoramiento.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>