 TaskManager Full-Stack Solution

Este proyecto es una solución integral para la gestión de tareas, desarrollada como parte de una prueba técnica. Implementa un sistema completo con un Backend robusto en .NET y un Frontend moderno en Angular.

## 🚀 Tecnologías Utilizadas

### Backend
* **Lenguaje:** C# / .NET 8
* **Base de Datos:** SQL Server (Express/LocalDB)
* **ORM:** Entity Framework Core (Code First)
* **Documentación:** Swagger / OpenAPI

### Frontend
* **Framework:** Angular 17+ (Basado en Módulos)
* **UI Library:** Angular Material
* **Estilos:** SCSS (Variables y mixins)
* **Patrones:** Servicios genéricos, Formularios Reactivos (Reactive Forms)

---

## 🛠️ Características Principales

1.  **Reglas de Negocio Senior:** Implementación de validaciones en el Backend para el flujo de estados (Pendiente -> En Proceso -> Completada), impidiendo saltos de estado inválidos.
2.  **Información Adicional Dinámica:** Almacenamiento de metadatos (Prioridad y Tags) mediante una columna JSON en SQL Server, demostrando manejo de datos no estructurados en una base relacional.
3.  **Dashboard Funcional:** Listado con filtros dinámicos por estado y visualización clara de la prioridad mediante badges de colores.
4.  **Arquitectura Escalable:** Uso de un `ApiService` genérico en Angular para centralizar peticiones HTTP, manejo de errores y headers.

---

## 📦 Configuración e Instalación

### 1. Requisitos Previos
* SDK de .NET 8.0
* Node.js (v18+) y Angular CLI
* SQL Server (LocalDB o instancia de Express)

### 2. Base de Datos
Asegúrate de configurar tu cadena de conexión en el archivo `appsettings.json` del Backend. Puedes ejecutar las migraciones de Entity Framework o el script SQL incluido:

```bash
dotnet ef database update
```

### 3. Ejecución del Backend
```bash
cd Backend
dotnet restore
dotnet run
```
*La API estará disponible en `https://localhost:7052/swagger`*

### 4. Ejecución del Frontend
```bash
cd Frontend
npm install
ng serve
```
*Accede a la aplicación en `http://localhost:4200`*

---

## 🧠 Decisiones de Arquitectura

* **Monorepo:** Se optó por una estructura de monorepositorio para facilitar la revisión de la solución completa.
* **Separación de Responsabilidades:** El Backend contiene toda la lógica de validación de estados para asegurar la integridad de los datos, mientras que el Frontend se encarga de guiar la experiencia del usuario.
* **Reactive Forms:** Se utilizaron formularios reactivos para asegurar una validación robusta en tiempo real antes de enviar los datos al servidor.

---

Desarrollado con ❤️ por Gustavo Adolfo Muñoz Reyes.