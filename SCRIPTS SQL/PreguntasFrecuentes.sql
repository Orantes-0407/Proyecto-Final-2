CREATE TABLE PreguntasFrecuentes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Tema NVARCHAR(100) NOT NULL,
    Pregunta NVARCHAR(MAX) NOT NULL,
    Respuesta NVARCHAR(MAX) NOT NULL
);
