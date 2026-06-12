<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="SubirEvidencias.aspx.cs" Inherits="PlanMejoramiento.Vista.Aprendiz.SubirEvidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="space-y-6 max-w-4xl mx-auto">
        <div class="flex items-center gap-4">
            <a href="MisPlanes.aspx" class="bg-white p-2.5 rounded-xl border border-gray-200 text-gray-500 hover:text-orange-600 hover:border-orange-200 transition shadow-sm">
                <i class="fas fa-arrow-left"></i>
            </a>
            <div>
                <h1 class="text-2xl font-black text-gray-800 tracking-tight">Detalle y Entrega de Evidencias</h1>
                <p class="text-sm text-gray-500">Revisa las pautas institucionales y adjunta tus archivos de soporte.</p>
            </div>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="p-4 rounded-xl text-sm flex items-start gap-3 shadow-sm">
             <i id="iconoMensaje" runat="server" class="fas text-base mt-0.5"></i>
             <asp:Label ID="lblTextoMensaje" runat="server"></asp:Label>
        </asp:Panel>

        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
            
            <div class="lg:col-span-1 space-y-5">
                <div class="bg-white p-5 rounded-2xl border border-gray-100 shadow-sm">
                    <h3 class="text-xs font-black text-gray-400 uppercase tracking-widest mb-4 border-b border-gray-50 pb-2">Datos del Plan</h3>
                    <div class="space-y-3">
                        <div>
                            <span class="block text-[10px] font-bold text-gray-400 uppercase">Código de Radicado</span>
                            <p class="text-sm font-mono font-bold text-gray-700"><asp:Label ID="lblCodPlan" runat="server" Text="-"></asp:Label></p>
                        </div>
                        <div>
                            <span class="block text-[10px] font-bold text-gray-400 uppercase">Instructor Asignante</span>
                            <p class="text-sm font-bold text-gray-800"><asp:Label ID="lblInstructor" runat="server" Text="-"></asp:Label></p>
                        </div>
                        <div>
                            <span class="block text-[10px] font-bold text-gray-400 uppercase">Fecha Límite de Entrega</span>
                            <p class="text-sm font-bold text-red-600 flex items-center gap-1.5 mt-0.5">
                                <i class="fas fa-calendar-day"></i>
                                <asp:Label ID="lblFechaLimite" runat="server" Text="-"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="bg-gradient-to-br from-orange-600 to-amber-500 p-5 rounded-2xl shadow-md shadow-orange-600/10 text-white">
                    <h4 class="font-bold text-sm flex items-center gap-2 mb-2">
                        <i class="fas fa-exclamation-triangle"></i> Importante
                    </h4>
                    <p class="text-xs leading-relaxed opacity-90">
                        Recuerda que solo se permiten formatos de archivo comprimidos (.zip, .rar) o documentos en formato (.pdf) que no superen los 10MB.
                    </p>
                </div>
            </div>

            <div class="lg:col-span-2 space-y-6">
                
                <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
                    <h3 class="text-xs font-black text-gray-400 uppercase tracking-widest mb-3 border-b border-gray-50 pb-2">Actividades a Desarrollar</h3>
                    <div class="bg-gray-50/70 rounded-xl p-4 border border-gray-100">
                        <p class="text-sm text-gray-700 font-medium whitespace-pre-line leading-relaxed">
                            <asp:Label ID="lblActividadesDesc" runat="server" Text="Cargando actividades descritas por el instructor..."></asp:Label>
                        </p>
                    </div>
                </div>

                <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 space-y-4">
                    <h3 class="text-xs font-black text-gray-400 uppercase tracking-widest border-b border-gray-50 pb-2">Cargar Evidencia Digital</h3>
                    
                    <div class="space-y-2">
                        <label class="block text-xs font-bold text-gray-600 uppercase">Seleccione su archivo comprimido o informe</label>
                        <div class="flex items-center w-full">
                            <asp:FileUpload ID="fuEvidencia" runat="server" 
                                            CssClass="w-full text-sm text-gray-500 file:mr-4 file:py-2.5 file:px-4 file:rounded-xl file:border-0 file:text-xs file:font-black file:uppercase file:tracking-wider file:bg-orange-50 file:text-orange-700 hover:file:bg-orange-100 transition cursor-pointer border border-gray-200 rounded-xl p-1 bg-gray-50/30" />
                        </div>
                    </div>

                    <div class="pt-2">
                        <label class="block text-xs font-bold text-gray-600 uppercase mb-2">Comentarios para el Instructor (Opcional)</label>
                        <asp:TextBox ID="txtComentariosAprendiz" runat="server" TextMode="MultiLine" Rows="3" placeholder="Ej: Quedo atento a sus correcciones, profe..."
                                     CssClass="w-full p-3 border border-gray-200 rounded-xl text-sm focus:outline-none focus:border-orange-500 focus:ring-4 focus:ring-orange-500/10 transition"></asp:TextBox>
                    </div>

                    <div class="flex justify-end pt-2">
                        <asp:Button ID="btnEnviarEntrega" runat="server" Text="Enviar Evidencias al Instructor" OnClick="btnEnviarEntrega_Click"
                                    CssClass="w-full md:w-auto bg-orange-600 hover:bg-orange-700 text-white font-black py-3 px-8 rounded-2xl shadow-xl shadow-orange-600/20 transition cursor-pointer text-sm" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>