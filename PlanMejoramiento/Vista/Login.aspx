<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PlanMejoramiento.Vista.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SENA - Iniciar Sesión</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
</head>
<body class="bg-slate-900 min-h-screen flex items-center justify-center p-4 font-sans relative overflow-hidden">
    
    <div class="absolute w-96 h-96 bg-emerald-500/10 rounded-full blur-3xl -top-20 -left-20"></div>
    <div class="absolute w-96 h-96 bg-blue-500/10 rounded-full blur-3xl -bottom-20 -right-20"></div>

    <form id="form1" runat="server" class="w-full max-w-md bg-white rounded-2xl shadow-2xl p-8 space-y-6 relative z-10 border border-gray-100">
        
        <a href="Index.aspx" class="inline-flex items-center gap-2 text-sm font-medium text-gray-500 hover:text-emerald-600 transition">
            <i class="fas fa-arrow-left"></i> Volver al inicio
        </a>

        <div class="text-center">
            <img src="https://upload.wikimedia.org/wikipedia/commons/8/83/SENA_LOGO.svg" alt="Logo SENA" class="h-20 w-20 mx-auto object-contain mb-4" />
            <h2 class="text-2xl font-black text-gray-800 tracking-tight">Ingreso al Sistema</h2>
            <asp:Label ID="lblRolSeleccionado" runat="server" CssClass="inline-block mt-2 text-xs font-bold uppercase tracking-wider bg-emerald-50 text-emerald-700 px-3 py-1 rounded-full border border-emerald-200"></asp:Label>
        </div>

        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="bg-red-50 border-l-4 border-red-500 text-red-700 p-4 rounded-r-xl text-sm flex items-start gap-3">
            <i class="fas fa-exclamation-circle text-base mt-0.5"></i>
            <div>
                <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <div class="space-y-4">
            <div>
                <label class="block text-xs font-bold text-gray-700 uppercase tracking-wider mb-2">Correo Electrónico</label>
                <div class="relative">
                    <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-gray-400">
                        <i class="fas fa-envelope"></i>
                    </div>
                    <asp:TextBox ID="txtCorreo" runat="server" TextMode="Email" placeholder="ejemplo@sena.edu.co" 
                                 CssClass="w-full pl-10 pr-4 py-3 rounded-xl border border-gray-200 focus:outline-none focus:border-emerald-500 focus:ring-4 focus:ring-emerald-500/10 transition text-sm text-gray-800"></asp:TextBox>
                </div>
            </div>

            <div>
                <label class="block text-xs font-bold text-gray-700 uppercase tracking-wider mb-2">Contraseña</label>
                <div class="relative">
                    <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-gray-400">
                        <i class="fas fa-lock"></i>
                    </div>
                    <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" placeholder="••••••••" 
                                 CssClass="w-full pl-10 pr-4 py-3 rounded-xl border border-gray-200 focus:outline-none focus:border-emerald-500 focus:ring-4 focus:ring-emerald-500/10 transition text-sm text-gray-800"></asp:TextBox>
                </div>
            </div>
        </div>

        <div>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar de forma segura" OnClick="btnIngresar_Click" 
                        CssClass="w-full bg-emerald-600 hover:bg-emerald-700 text-white font-bold py-3 px-4 rounded-xl shadow-lg hover:shadow-emerald-600/20 transition duration-200 cursor-pointer flex justify-center items-center gap-2" />
        </div>

    </form>
</body>
</html>