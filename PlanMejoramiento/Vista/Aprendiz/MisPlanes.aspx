<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="MisPlanes.aspx.cs" Inherits="PlanMejoramiento.Vista.Aprendiz.MisPlanes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="space-y-6">
        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
            <div>
                <h1 class="text-2xl font-black text-gray-800 tracking-tight">Mis Planes de Mejoramiento</h1>
                <p class="text-sm text-gray-500 mt-1">Consulta tus procesos académicos activos, descarga talleres y sube tus evidencias de recuperación.</p>
            </div>
            <span class="bg-orange-50 text-orange-700 text-xs font-bold uppercase tracking-wider px-3 py-1 rounded-full border border-orange-200">
                Módulo Aprendiz
            </span>
        </div>

        <div class="grid grid-cols-1 md:grid-cols-3 gap-5">
            <div class="bg-white p-5 rounded-2xl border border-gray-100 flex items-center gap-4 shadow-sm">
                <div class="w-12 h-12 rounded-xl bg-blue-50 text-blue-600 flex items-center justify-center text-xl">
                    <i class="fas fa-clock"></i>
                </div>
                <div>
                    <span class="block text-xs font-bold text-gray-400 uppercase tracking-wider">Por Entregar</span>
                    <h3 class="text-2xl font-black text-gray-800 mt-0.5"><asp:Label ID="lblPendientes" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>

            <div class="bg-white p-5 rounded-2xl border border-gray-100 flex items-center gap-4 shadow-sm">
                <div class="w-12 h-12 rounded-xl bg-amber-50 text-amber-600 flex items-center justify-center text-xl">
                    <i class="fas fa-file-export"></i>
                </div>
                <div>
                    <span class="block text-xs font-bold text-gray-400 uppercase tracking-wider">En Evaluación</span>
                    <h3 class="text-2xl font-black text-gray-800 mt-0.5"><asp:Label ID="lblEntregados" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>

            <div class="bg-white p-5 rounded-2xl border border-gray-100 flex items-center gap-4 shadow-sm">
                <div class="w-12 h-12 rounded-xl bg-emerald-50 text-emerald-600 flex items-center justify-center text-xl">
                    <i class="fas fa-check-double"></i>
                </div>
                <div>
                    <span class="block text-xs font-bold text-gray-400 uppercase tracking-wider">Aprobados</span>
                    <h3 class="text-2xl font-black text-emerald-600 mt-0.5"><asp:Label ID="lblAprobados" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>
        </div>

        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 space-y-4">
            <div class="border-b border-gray-100 pb-3">
                <h2 class="font-bold text-gray-800 text-lg flex items-center gap-2">
                    <i class="fas fa-clipboard-list text-orange-600"></i> 
                    Historial de Planes Asignados
                </h2>
            </div>

            <div class="overflow-x-auto rounded-xl border border-gray-100">
                <asp:GridView ID="gvMisPlanes" runat="server" AutoGenerateColumns="False" DataKeyNames="IdPlan"
                              OnRowCommand="gvMisPlanes_RowCommand"
                              CssClass="w-full min-w-full text-left text-sm text-gray-600 border-collapse">
                    <HeaderStyle CssClass="bg-gray-50 border-b border-gray-100 text-gray-700 uppercase tracking-wider text-xs font-bold p-4" />
                    <RowStyle CssClass="border-b border-gray-50 hover:bg-gray-50/50 transition p-4 font-medium" />
                    <Columns>
                        <asp:BoundField DataField="IdPlan" HeaderText="Código" ItemStyle-CssClass="p-4 text-gray-400 font-mono text-xs" />
                        <asp:BoundField DataField="Competencia" HeaderText="Competencia Afectada" ItemStyle-CssClass="p-4 text-gray-800 font-bold max-w-xs truncate" />
                        <asp:BoundField DataField="FechaLimite" HeaderText="Fecha Límite" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="p-4 font-mono text-red-600 font-bold" />
                        
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# ObtenerEstiloEstado(Eval("Estado").ToString()) %>'>
                                    <%# Eval("Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Instructor" HeaderText="Instructor" ItemStyle-CssClass="p-4 text-xs text-gray-500" />
                        
                        <asp:TemplateField HeaderText="Acción" ItemStyle-CssClass="p-4 text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnResponder" runat="server" CommandName="Responder" CommandArgument='<%# Eval("IdPlan") %>'
                                                CssClass='<%# ObtenerEstiloBoton(Eval("Estado").ToString()) %>'>
                                    <i class="fas fa-cloud-upload-alt"></i> Ver y Enviar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center py-16 text-gray-400 font-medium">
                            <i class="fas fa-smile-beam text-5xl mb-4 block text-emerald-500"></i>
                            ¡Felicitaciones! No tienes planes de mejoramiento pendientes ni registrados en el sistema.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
