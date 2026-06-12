<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true" CodeBehind="CargaMasiva.aspx.cs" Inherits="PlanMejoramiento.Vista.Administrador.CargaMasiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="space-y-6">
        <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
            <div>
                <h1 class="text-2xl font-black text-gray-800 tracking-tight">Carga Masiva de Aprendices</h1>
                <p class="text-sm text-gray-500 mt-1">Sube listados completos de aprendices en formato CSV o Excel para matricularlos al instante.</p>
            </div>
            <span class="bg-emerald-50 text-emerald-700 text-xs font-bold uppercase tracking-wider px-3 py-1 rounded-full border border-emerald-200">
                Módulo Administrador
            </span>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="p-4 rounded-xl text-sm flex items-start gap-3 shadow-sm">
            <i id="iconoMensaje" runat="server" class="fas text-base mt-0.5"></i>
            <div>
                <asp:Label ID="lblTextoMensaje" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6 items-start">
            
            <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 space-y-5 lg:col-span-1">
                <div class="border-b border-gray-100 pb-3">
                    <h2 class="font-bold text-gray-800 text-lg flex items-center gap-2">
                        <i class="fas fa-file-excel text-emerald-600"></i> Importar Archivo
                    </h2>
                </div>

                <div>
                    <label class="block text-xs font-bold text-gray-700 uppercase tracking-wider mb-2">1. Seleccione la Ficha Destino</label>
                    <asp:DropDownList ID="ddlFichaDestino" runat="server" 
                                      CssClass="w-full px-4 py-2.5 rounded-xl border border-gray-200 bg-white focus:outline-none focus:border-emerald-500 focus:ring-4 focus:ring-emerald-500/10 transition text-sm text-gray-800 font-medium cursor-pointer">
                    </asp:DropDownList>
                </div>

                <div>
                    <label class="block text-xs font-bold text-gray-700 uppercase tracking-wider mb-2">2. Seleccione el Archivo (.csv / .xlsx)</label>
                    <div class="border-2 border-dashed border-gray-200 rounded-2xl p-6 text-center hover:border-emerald-500 transition relative bg-gray-50/50">
                        <i class="fas fa-cloud-upload-alt text-4xl text-gray-400 mb-3 block"></i>
                        <span class="text-xs text-gray-500 block mb-3 font-medium">Límite de archivo: 5MB</span>
                        
                        <asp:FileUpload ID="fuListado" runat="server" CssClass="block w-full text-sm text-gray-500 file:mr-4 file:py-2 file:px-4 file:rounded-full file:border-0 file:text-xs file:font-bold file:bg-emerald-50 file:text-emerald-700 hover:file:bg-emerald-100 cursor-pointer" />
                    </div>
                </div>

                <div class="pt-2">
                    <asp:Button ID="btnProcesar" runat="server" Text="Procesar e Importar Aprendices" OnClick="btnProcesar_Click" 
                                CssClass="w-full bg-emerald-600 hover:bg-emerald-700 text-white font-bold py-3 px-4 rounded-xl shadow-md hover:shadow-emerald-600/10 transition duration-200 cursor-pointer text-sm text-center flex justify-center items-center gap-2" />
                </div>
            </div>

            <div class="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 lg:col-span-2 space-y-4">
                <div class="border-b border-gray-100 pb-3">
                    <h2 class="font-bold text-gray-800 text-lg flex items-center gap-2">
                        <i class="fas fa-info-circle text-blue-600"></i> Estructura Requerida del Archivo
                    </h2>
                </div>
                
                <p class="text-sm text-gray-600 leading-relaxed">
                    Para que el sistema lea los datos correctamente, el archivo debe estar separado por comas o columnas ordenadas exactamente de la siguiente forma **sin incluir la fila de títulos**:
                </p>

                <div class="overflow-x-auto rounded-xl border border-gray-100">
                    <table class="w-full text-left text-sm text-gray-600 border-collapse">
                        <thead>
                            <tr class="bg-gray-50 border-b border-gray-100 text-gray-700 uppercase tracking-wider text-xs font-bold">
                                <th class="p-3">Columna 1</th>
                                <th class="p-3">Columna 2</th>
                                <th class="p-3">Columna 3</th>
                                <th class="p-3">Columna 4</th>
                                <th class="p-3">Columna 5</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-50 font-medium">
                            <tr class="bg-white text-gray-800">
                                <td class="p-3 text-gray-400 font-bold">Documento</td>
                                <td class="p-3">Nombres</td>
                                <td class="p-3">Apellidos</td>
                                <td class="p-3 text-xs text-blue-600">Correo Electrónico</td>
                                <td class="p-3 text-gray-500">Teléfono</td>
                            </tr>
                            <tr class="bg-gray-50/30 text-gray-500 text-xs">
                                <td class="p-3 font-mono">102444555</td>
                                <td class="p-3">Juan Camilo</td>
                                <td class="p-3">Rincón Díaz</td>
                                <td class="p-3 font-mono">jc@soy.sena.edu.co</td>
                                <td class="p-3 font-mono">3129998877</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="bg-amber-50 border-l-4 border-amber-500 text-amber-800 p-4 rounded-r-xl text-xs space-y-1">
                    <p class="font-bold"><i class="fas fa-exclamation-triangle mr-1"></i> Consideraciones Importantes:</p>
                    <ul class="list-disc list-inside space-y-0.5 opacity-90">
                        <li>El sistema ignorará registros duplicados basados en el número de Documento.</li>
                        <li>Los correos deben tener una estructura válida (ejemplo@dominio.com).</li>
                        <li>Si hay registros con errores, el sistema procesará los correctos y le informará cuáles fallaron.</li>
                    </ul>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
