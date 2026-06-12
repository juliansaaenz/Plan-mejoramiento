<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PlanMejoramiento.Vista.Index" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SENA - Sistema de Planes de Mejoramiento</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
</head>
<body class="bg-gray-50 font-sans">
    <form id="form1" runat="server">
        
        <nav class="bg-white shadow-md fixed w-full top-0 left-0 z-50 h-20 flex justify-between items-center px-8 border-b-4 border-emerald-500">
            <div class="flex items-center gap-3">
                <img src="https://upload.wikimedia.org/wikipedia/commons/8/83/SENA_LOGO.svg" alt="Logo SENA" class="h-14 w-14 object-contain" />
                <div>
                    <span class="font-black text-xl text-gray-800 tracking-tight block">SENA</span>
                    <span class="text-xs text-emerald-600 font-bold uppercase tracking-wider block">Planes de Mejoramiento</span>
                </div>
            </div>

            <div class="hidden md:flex items-center gap-8 font-medium text-gray-600">
                <a href="#que-es" class="hover:text-emerald-600 transition">¿Qué es?</a>
                <a href="#especificaciones" class="hover:text-emerald-600 transition">Especificaciones</a>
                <a href="#fases" class="hover:text-emerald-600 transition">Fases del Proceso</a>
            </div>

            <div class="relative inline-block text-left">
                <button type="button" id="btnDropdown" class="bg-emerald-600 hover:bg-emerald-700 text-white px-5 py-2.5 rounded-lg font-semibold shadow flex items-center gap-2 transition cursor-pointer">
                    <i class="fas fa-user-circle text-lg"></i>
                    Iniciar Sesión
                    <i class="fas fa-chevron-down text-xs transition-transform duration-200" id="flechaDropdown"></i>
                </button>

                <div id="menuDropdown" class="hidden absolute right-0 mt-2 w-56 rounded-xl bg-white shadow-xl ring-1 ring-black ring-opacity-5 divide-y divide-gray-100 focus:outline-none z-50 transform opacity-0 scale-95 transition-all duration-200">
                    <div class="py-1">
                        <a href="Login.aspx?rol=1" class="text-gray-700 group flex items-center px-4 py-3 text-sm hover:bg-emerald-50 hover:text-emerald-700 transition">
                            <i class="fas fa-user-shield text-emerald-600 w-6 text-base"></i> Administrador
                        </a>
                        <a href="Login.aspx?rol=2" class="text-gray-700 group flex items-center px-4 py-3 text-sm hover:bg-emerald-50 hover:text-emerald-700 transition">
                            <i class="fas fa-chalkboard-user text-emerald-600 w-6 text-base"></i> Instructor
                        </a>
                        <a href="Login.aspx?rol=3" class="text-gray-700 group flex items-center px-4 py-3 text-sm hover:bg-emerald-50 hover:text-emerald-700 transition">
                            <i class="fas fa-user-graduate text-emerald-600 w-6 text-base"></i> Aprendiz
                        </a>
                    </div>
                </div>
            </div>
        </nav>

        <header class="pt-32 pb-20 bg-gradient-to-br from-emerald-900 to-slate-900 text-white text-center px-4">
            <div class="max-w-4xl mx-auto">
                <span class="bg-emerald-500/20 text-emerald-300 text-xs font-bold tracking-widest uppercase px-4 py-1.5 rounded-full border border-emerald-500/30">Portal Oficial Institucional</span>
                <h1 class="text-4xl md:text-5xl font-black tracking-tight mt-6 mb-4">Optimiza tu Ruta de Aprendizaje</h1>
                <p class="text-lg md:text-xl text-gray-300 max-w-2xl mx-auto font-light">
                    Gestiona, carga y evalúa las actividades de refuerzo de manera ágil y digital.
                </p>
            </div>
        </header>

        <main class="max-w-6xl mx-auto px-4 py-16 space-y-20">
            
            <section id="que-es" class="grid md:grid-template-columns md:grid-cols-2 gap-12 items-center">
                <div class="space-y-4">
                    <div class="h-1 w-12 bg-emerald-500 rounded"></div>
                    <h2 class="text-3xl font-extrabold text-gray-900 tracking-tight">¿Qué es un Plan de Mejoramiento?</h2>
                    <p class="text-gray-600 leading-relaxed text-base">
                        Es una <strong>medida formativa</strong> concertada entre el Instructor y el Aprendiz. Se diseña cuando no se han alcanzado los Resultados de Aprendizaje programados, permitiendo nivelar las competencias técnicas o transversales mediante nuevas evidencias.
                    </p>
                </div>
                <div class="bg-white p-8 rounded-2xl shadow-md border border-gray-100 flex flex-col justify-center text-center">
                    <span class="text-5xl font-black text-emerald-600 block">85%</span>
                    <span class="text-gray-700 font-bold block mt-2">Índice de Eficacia</span>
                    <span class="text-sm text-gray-500 block mt-1">Los aprendices que ejecutan su plan digital logran culminar su etapa lectiva con éxito.</span>
                </div>
            </section>

            <section id="especificaciones" class="space-y-8">
                <div class="text-center max-w-2xl mx-auto">
                    <h2 class="text-3xl font-extrabold text-gray-900 tracking-tight">Especificaciones del Sistema</h2>
                    <p class="text-gray-500 mt-2">Estructura del proceso bajo la normativa del Reglamento del Aprendiz SENA.</p>
                </div>

                <div class="grid md:grid-cols-3 gap-8">
                    <div class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition">
                        <div class="w-12 h-12 bg-emerald-100 text-emerald-700 rounded-lg flex items-center justify-center text-xl font-bold mb-4">01</div>
                        <h3 class="font-bold text-lg text-gray-800 mb-2">Evaluación por Variables</h3>
                        <p class="text-gray-600 text-sm leading-relaxed">El sistema evalúa tres frentes cruciales: evidencias de <strong>Conocimiento</strong>, <strong>Desempeño</strong> y <strong>Producto</strong>.</p>
                    </div>
                    <div class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition">
                        <div class="w-12 h-12 bg-emerald-100 text-emerald-700 rounded-lg flex items-center justify-center text-xl font-bold mb-4">02</div>
                        <h3 class="font-bold text-lg text-gray-800 mb-2">Carga Masiva Eficiente</h3>
                        <p class="text-gray-600 text-sm leading-relaxed">Los instructores pueden asociar listados completos de aprendices mediante plantillas estandarizadas de Excel de forma inmediata.</p>
                    </div>
                    <div class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition">
                        <div class="w-12 h-12 bg-emerald-100 text-emerald-700 rounded-lg flex items-center justify-center text-xl font-bold mb-4">03</div>
                        <h3 class="font-bold text-lg text-gray-800 mb-2">Alertas y Comités</h3>
                        <p class="text-gray-600 text-sm leading-relaxed">Si un plan no se aprueba en el tiempo pactado, la plataforma escala automáticamente el caso al estado de <strong>Plan por Comité</strong>.</p>
                    </div>
                </div>
            </section>
        </main>

        <footer class="bg-gray-900 text-gray-400 py-8 border-t border-gray-800 text-center text-sm">
            <p>&copy; 2026 Servicio Nacional de Aprendizaje (SENA). Todos los derechos reservados.</p>
        </footer>

    </form>

    <script>
        const btnDropdown = document.getElementById('btnDropdown');
        const menuDropdown = document.getElementById('menuDropdown');
        const flechaDropdown = document.getElementById('flechaDropdown');

        btnDropdown.addEventListener('click', (e) => {
            e.stopPropagation();
            
            if (menuDropdown.classList.contains('hidden')) {
                menuDropdown.classList.remove('hidden');
                setTimeout(() => {
                    menuDropdown.classList.remove('opacity-0', 'scale-95');
                    menuDropdown.classList.add('opacity-100', 'scale-100');
                }, 10);
                flechaDropdown.classList.add('rotate-180');
            } else {
                cerrarDropdown();
            }
        });

        document.addEventListener('click', () => {
            cerrarDropdown();
        });

        function cerrarDropdown() {
            if (!menuDropdown.classList.contains('hidden')) {
                menuDropdown.classList.remove('opacity-100', 'scale-100');
                menuDropdown.classList.add('opacity-0', 'scale-95');
                flechaDropdown.classList.remove('rotate-180');
                setTimeout(() => {
                    menuDropdown.classList.add('hidden');
                }, 200);
            }
        }
    </script>
</body>
</html>
