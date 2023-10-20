CREATE DATABASE Escuelas
USE Escuelas

CREATE TABLE Escuela
(
	ID_Escuela INT PRIMARY KEY not NULL,
	Nombre varchar(100),
	Direccion varchar(100),
	Telefono varchar(100),
	Correo varchar(100),
	Director varchar(100)
)

CREATE TABLE Departamento
(
	ID_Departamento INT PRIMARY KEY not NULL,
	Nombre varchar(100),
	Descripcion varchar(100),
	ID_Escuela INT not NULL,
	FOREIGN KEY (ID_Escuela) REFERENCES Escuela (ID_Escuela)
)

CREATE TABLE Maestro
(
	ID_Maestro INT PRIMARY KEY not NULL,
	Nombre varchar(100),
	Apellido varchar(100),
	Titulo varchar(100),
	ID_Departamento INT not NULL,
	FOREIGN KEY (ID_Departamento) REFERENCES Departamento (ID_Departamento)
)

CREATE TABLE Carrera
(
	ID_Carrera INT PRIMARY KEY not NULL,
	Nombre varchar(100),
	Descripcion varchar(100),
	Duracion varchar(100)
)

CREATE TABLE Materia
(
	ID_Materia INT PRIMARY KEY not NULL,
	Nombre varchar(100),
	Descripcion varchar(100),
	Creditos varchar(100),
	ID_Carrera INT not NULL
	FOREIGN KEY (ID_Carrera) REFERENCES Carrera (ID_Carrera)
)

CREATE TABLE Estudiante
(
	ID_Estudiante INT PRIMARY KEY not NULL,
	Nombre varchar(100),
	Apellido varchar(100),
	FechaNacimiento varchar(100),
	Genero varchar(100),
	DireccionCasa varchar(100),
	Telefono varchar(100),
	Correo varchar(100),
	ID_Carrera INT not NULL,
	ID_Escuela INT not NULL,
	FOREIGN KEY (ID_Carrera) REFERENCES Carrera (ID_Carrera),
	FOREIGN KEY (ID_Escuela) REFERENCES Escuela (ID_Escuela)
)