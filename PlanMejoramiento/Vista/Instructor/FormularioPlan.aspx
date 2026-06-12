<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="FormularioPlan.aspx.cs" Inherits="PlanMejoramiento.Vista.Instructor.FormularioPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="space-y-6 max-w-5xl mx-auto">
        <div class="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
            <div class="flex items-center gap-4">
                <asp:LinkButton ID="btnVolver" runat="server" OnClick="btnVolver_Click" CssClass="bg-white p-2.5 rounded-xl border border-gray-200 text-gray-500 hover:text-blue-600 transition shadow-sm">
                    <i class="fas fa-arrow-left"></i>
                </asp:LinkButton>
                <div>
                    <h1 class="text-2xl font-black text-gray-800 tracking-tight">
                        <asp:Label ID="lblTituloPagina" runat="server" Text="Nuevo Plan de Mejoramiento"></asp:Label>
                    </h1>
                    <p class="text-sm text-gray-500">Diligencie los campos para definir las actividades de recuperación.</p>
                </div>
            </div>
            <div class="flex gap-2">
                <asp:Label ID="lblEstadoPlan" runat="server" CssClass="px-4 py-2 rounded-xl text-xs font-black uppercase tracking-widest bg-blue-50 text-blue-600 border border-blue-100 shadow-sm" Text="Borrador"></asp:Label>
            </div>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="p-4 rounded-xl text-sm flex items-start gap-3 shadow-sm mb-4">
             <i id="iconoMensaje" runat="server" class="fas text-base mt-0.5"></i>
             <asp:Label ID="lblTextoMensaje" runat="server"></asp:Label>
        </asp:Panel>

        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
            
            <div class="lg:col-span-1 space-y-6">
                <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
                    <h3 class="text-xs font-black text-gray-400 uppercase tracking-widest mb-4 border-b border-gray-50 pb-2">Información General</h3>
                    <div class="space-y-4">
                        <div>
                            <label class="block text-[10px] font-bold text-gray-500 uppercase mb-1">Aprendiz</label>
                            <p class="text-sm font-bold text-gray-800"><asp:Label ID="lblAprendiz" runat="server" Text="-"></asp:Label></p>
                        </div>
                        <div>
                            <label class="block text-[10px] font-bold text-gray-500 uppercase mb-1">Tipo de Plan</label>
                            <asp:RadioButtonList ID="rblTipoPlan" runat="server" RepeatDirection="Horizontal" CssClass="flex gap-4 text-sm font-medium text-gray-700">
                                <asp:ListItem Value="Interno" Selected="True" Text="&nbsp;Interno&nbsp;&nbsp;"></asp:ListItem>
                                <asp:ListItem Value="Comite" Text="&nbsp;Comité"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="grid grid-cols-2 gap-4 pt-2">
                            <div>
                                <label class="block text-[10px] font-bold text-gray-500 uppercase mb-1">Fecha Asignación</label>
                                <asp:TextBox ID="txtFechaAsignacion" runat="server" TextMode="Date" CssClass="w-full p-2 bg-gray-50 border border-gray-200 rounded-lg text-xs font-bold"></asp:TextBox>
                            </div>
                            <div>
                                <label class="block text-[10px] font-bold text-gray-500 uppercase mb-1">Fecha Límite</label>
                                <asp:TextBox ID="txtFechaLimite" runat="server" TextMode="Date" CssClass="w-full p-2 border border-gray-200 rounded-lg text-xs font-bold focus:border-blue-500 transition"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="bg-blue-900 p-6 rounded-2xl shadow-lg shadow-blue-900/20 text-white">
                    <h3 class="text-[10px] font-black opacity-60 uppercase tracking-widest mb-3">Recomendación Técnica</h3>
                    <p class="text-xs leading-relaxed opacity-90">
                        Asegúrese de que las actividades propuestas sean medibles y correspondan directamente a los resultados de aprendizaje seleccionados.
                    </p>
                </div>
            </div>

            <div class="lg:col-span-2 space-y-6">
                <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
                    <h3 class="text-xs font-black text-gray-400 uppercase tracking-widest mb-4 border-b border-gray-50 pb-2">Competencias y Resultados (RAP)</h3>
                    <div class="space-y-4">
                        <div>
                            <label class="block text-[10px] font-bold text-gray-600 uppercase mb-1">1. Seleccione Competencia</label>
                            <asp:DropDownList ID="ddlCompetencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompetencia_SelectedIndexChanged"
                                              CssClass="w-full p-2.5 bg-white border border-gray-200 rounded-xl text-sm font-medium focus:ring-4 focus:ring-blue-500/10 transition">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label class="block text-[10px] font-bold text-gray-600 uppercase mb-1">2. Resultados de Aprendizaje afectados</label>
                            <div class="border border-gray-100 rounded-xl p-4 bg-gray-50/50 max-h-40 overflow-y-auto">
                                <asp:CheckBoxList ID="cblResultados" runat="server" CssClass="text-xs text-gray-700 space-y-2 w-full" RepeatLayout="UnorderedList">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
                    <h3 class="text-xs font-black text-gray-400 uppercase tracking-widest mb-4 border-b border-gray-50 pb-2">Actividades a Realizar</h3>
                    <div class="space-y-4">
                        <asp:TextBox ID="txtActividades" runat="server" TextMode="MultiLine" Rows="5" placeholder="Describa detalladamente las evidencias que el aprendiz debe entregar..."
                                     CssClass="w-full p-4 border border-gray-200 rounded-xl text-sm focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10 transition"></asp:TextBox>
                        
                        <div>
                            <label class="block text-[10px] font-bold text-gray-600 uppercase mb-1">Observaciones Adicionales</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="w-full p-2 border border-gray-200 rounded-lg text-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="flex justify-end gap-3 pt-4">
                    <asp:Button ID="btnGuardarPlan" runat="server" Text="Generar Plan de Mejoramiento" OnClick="btnGuardarPlan_Click"
                                CssClass="bg-blue-600 hover:bg-blue-700 text-white font-black py-3 px-8 rounded-2xl shadow-xl shadow-blue-600/20 transition cursor-pointer text-sm" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
