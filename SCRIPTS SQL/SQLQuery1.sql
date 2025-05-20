CREATE TABLE Estudiantes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Grado INT,
    ProgresoMatematicas FLOAT,
    ProgresoLectura FLOAT,
    UltimaSesion DATETIME
);
