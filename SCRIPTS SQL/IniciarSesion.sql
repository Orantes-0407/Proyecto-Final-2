CREATE TABLE IniciosSesion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EstudianteId INT NOT NULL,
    FechaHora DATETIME NOT NULL DEFAULT GETDATE(),
    Dispositivo NVARCHAR(100),         -- Opcional: para guardar nombre del dispositivo o IP
    Observaciones NVARCHAR(255),       -- Opcional: puede guardar algo como "Inicio correcto"

    FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id)
);