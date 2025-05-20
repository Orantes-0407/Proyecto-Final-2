CREATE TABLE HistorialBot (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaHora DATETIME DEFAULT GETDATE(),
    Pregunta NVARCHAR(MAX),
    Respuesta NVARCHAR(MAX)
);