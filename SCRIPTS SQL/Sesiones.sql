CREATE TABLE Sesiones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EstudianteId INT FOREIGN KEY REFERENCES Estudiantes(Id),
    Fecha DATETIME NOT NULL,
    Tema NVARCHAR(100),
    ComentariosIA NVARCHAR(MAX)
);