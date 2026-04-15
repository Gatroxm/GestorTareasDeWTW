-- ======================================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS Y MODELO DE DATOS
-- PROYECTO: GESTIÓN DE TAREAS (TASK MANAGER)
-- ======================================================

-- 1. Creación de la Base de Datos [cite: 38]
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TaskManagerDB')
BEGIN
    CREATE DATABASE TaskManagerDB;
END
GO

USE TaskManagerDB;
GO

-- 2. Creación de la Tabla de Usuarios [cite: 35]
-- Cada usuario debe contar con nombre y correo electrónico [cite: 12]
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Clave primaria [cite: 36]
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE
);
GO

-- 3. Creación de la Tabla de Tareas [cite: 35, 66]
CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Clave primaria [cite: 36]
    Title NVARCHAR(200) NOT NULL, -- Título obligatorio [cite: 20]
    Description NVARCHAR(MAX) NULL,
    -- Estados permitidos: Pending, InProgress, Done 
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending' 
        CHECK (Status IN ('Pending', 'InProgress', 'Done')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserId INT NOT NULL, -- Debe tener un usuario asignado [cite: 21]
    
    -- Columna para información adicional en formato JSON [cite: 66, 72]
    AdditionalInfo NVARCHAR(MAX) NULL, 

    -- Clave foránea para relación con usuarios [cite: 36]
    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    
    -- Validación de formato JSON nativo 
    CONSTRAINT CHK_Tasks_JSON_Valid CHECK (ISJSON(AdditionalInfo) > 0)
);
GO

-- 4. Optimización: Creación de Índice [cite: 37]
-- Se crea un índice para optimizar la búsqueda por usuario y estado
CREATE INDEX IX_Tasks_User_Status ON Tasks (UserId, Status);
GO

-- 5. Inserción de Datos de Prueba (Seed)
INSERT INTO Users (Name, Email) VALUES 
('Usuario Prueba 1', 'prueba1@taskmanager.com'),
('Usuario Prueba 2', 'prueba2@taskmanager.com');

INSERT INTO Tasks (Title, Description, Status, UserId, AdditionalInfo) VALUES 
('Configurar Base de Datos', 'Script inicial de SQL', 'Done', 1, 
 '{"Priority": "High", "EstimatedDays": 1, "Tags": ["SQL", "Backend"]}'),
('Desarrollar Web API', 'Implementar controladores y servicios', 'InProgress', 1, 
 '{"Priority": "High", "EstimatedDays": 3, "Tags": [".NET", "API"]}'),
('Crear Interfaz Angular', 'Formularios reactivos y listas', 'Pending', 2, 
 '{"Priority": "Medium", "EstimatedDays": 2, "Tags": ["Angular", "Frontend"]}');
GO

-- 6. Consultas de Demostración (Requerimientos de la Prueba)

-- A. Obtener tareas por usuario, filtrado por estado y ordenado por fecha [cite: 40, 41, 42]
SELECT * FROM Tasks 
WHERE UserId = 1 AND Status = 'InProgress'
ORDER BY CreatedAt DESC;

-- B. Uso de funciones nativas JSON (JSON_VALUE) [cite: 74, 79, 82]
-- Extraer la prioridad y etiquetas del campo JSON
SELECT 
    Title, 
    JSON_VALUE(AdditionalInfo, '$.Priority') AS Prioridad,
    JSON_QUERY(AdditionalInfo, '$.Tags') AS Etiquetas
FROM Tasks;

-- C. Filtrar tareas utilizando un valor dentro del JSON [cite: 75]
SELECT * FROM Tasks 
WHERE JSON_VALUE(AdditionalInfo, '$.Priority') = 'High';

-- D. Actualización de un campo específico dentro del JSON [cite: 76]
UPDATE Tasks 
SET AdditionalInfo = JSON_MODIFY(AdditionalInfo, '$.Priority', 'Critical')
WHERE Id = 2;
GO