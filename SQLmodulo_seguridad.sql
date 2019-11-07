create database modulo_seguridad
/*
db_security_model: "1.0.0"
info:
  description: 
  "Gestiona el almacenamiento del modulo de seguridad del sitema de control de labores"
  
  title: db_security_model 
  version: 1.0.0

  license: "Microsoft"
    name: "Microsoft SQL server 2017 developer edition"
    url:  "https://www.microsoft.com/es-es/sql-server/sql-server-2017-pricing"

  contact: ""
    email: ""
*/

--tablas
create table S_Usuario(
id_Usuario int identity(1,1),
S_Nombre varchar(30)not null,
S_Apellido varchar(30)not null,
S_Contraseña varchar(30)not null,
S_Edad int not null,
S_Tipo_Usuario varchar(5)not null,
Constraint PK_S_Usuario Primary key(id_Usuario),
Constraint CH_TSCL_Usuario CHECK(S_Tipo_Usuario IN ('admin','colab','coord'))
)
select * from  S_Usuario
insert into S_Usuario values('Gabriel','Navarro','12345',21,'admin')
insert into S_Usuario values('123','123','123',21,'admin')

create table S_Roles(
id_Rol int identity(1,1),
S_Nombre_rol varchar(30)not null,
S_descripcion_rol varchar(30)not null,
Constraint PK_S_Roles Primary key(id_Rol)
)

create table S_Permisos(
id_Permiso int identity(1,1),
S_Nombre_permiso varchar(30)not null,
S_descripcion_permiso varchar(30)not null,
Constraint PK_S_Permiso Primary key(id_Permiso)
)drop table S_Permisos

/*Stablas intermedias*/
create table S_usuario_rol(
S_id_rol_usuario int identity(1,1),
S_id_Usuario int,
S_id_rol int,
Constraint FK_S_usuario_rol_usuario
FOREIGN KEY(S_id_Usuario)REFERENCES S_Usuario(id_Usuario),
Constraint FK_S_usuario_rol_rol
FOREIGN KEY(S_id_rol) REFERENCES S_Roles(id_Rol)
)


create table S_rol_permiso(
S_id_rol_permiso int identity(1,1), 
S_id_rol int,
S_id_permiso int,
Constraint FK_S_rol_permiso_rol
FOREIGN KEY(S_id_rol)REFERENCES S_Roles(id_Rol),
Constraint FK_S_rol_permiso_permiso
FOREIGN KEY(S_id_permiso) REFERENCES S_Permisos(id_Permiso)
)drop table S_rol_permiso

/**Procedimientos almacenados Usuario**/
--all users
create procedure PA_Consulta_Todos_Usuario
AS BEGIN
	BEGIN TRY
		Select id_Usuario, S_Nombre, S_Apellido,S_Contraseña,S_Edad,S_Tipo_Usuario  from S_Usuario
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--inicia sesion
create procedure PA_inicia_sesion  @S_Nombre varchar(30), @S_Apellido varchar(30),
@S_Contraseña varchar(255)
AS BEGIN
	BEGIN TRY
		Select id_Usuario, S_Nombre, S_Apellido,S_Contraseña,S_Edad,S_Tipo_Usuario  from S_Usuario
		where S_Usuario.S_Nombre=@S_Nombre and S_Usuario.S_Contraseña=@S_Contraseña and S_Usuario.S_Apellido=@S_Apellido
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--inserta uuario
create procedure PA_Insertar_usuario  @S_Nombre varchar(30), @S_Apellido varchar(30),
@S_Contraseña varchar(255),@S_Edad int,@S_Tipo_Usuario varchar(5)
AS BEGIN
	BEGIN TRY
		insert into S_Usuario values(@S_Nombre,@S_Apellido,@S_Contraseña,@S_Edad,@S_Tipo_Usuario)
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

/**Procedimientos almacenados Rol**/

--all rol
create procedure PA_Obtener_todos_rol
AS BEGIN
	BEGIN TRY
		Select id_Rol, S_Nombre_rol, S_descripcion_rol  from S_Roles
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--obtener rol por id
create procedure PA_obtener_rol_por_id  @id_rol int
AS BEGIN
	BEGIN TRY
		Select id_Rol, S_Nombre_rol, S_descripcion_rol  from S_Roles
		where @id_rol = S_Roles.id_Rol
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--- insertar rol 
create procedure PA_Insertar_Rol  @id_Rol int, @S_Nombre_rol varchar(30),
@S_id_rol varchar(30)
AS BEGIN
	BEGIN TRY
		insert into S_Rol values(@id_Rol,@S_Nombre_rol,@S_id_rol)
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

/**Procedimientos almacenados Permisos**/

--all permisos
create procedure PA_consulta_todos_permiso
AS BEGIN
	BEGIN TRY
		Select id_Permiso, S_Nombre_permiso, S_descripcion_permiso  from S_Permisos
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--obtener Permiso por id
create procedure PA_obtener_permiso_id  @id_Permiso int
AS BEGIN
	BEGIN TRY
		Select id_Permiso, S_Nombre_permiso, S_descripcion_permiso  from S_Permisos
		where @id_Permiso = S_Permisos.id_Permiso
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--insertar permiso
create procedure PA_Insertar_permiso  @id_Permiso int, @S_Nombre_permiso varchar(30),
@S_descripcion_permiso varchar(30)
AS BEGIN
	BEGIN TRY
		insert into S_Permiso values(@id_Permiso,@S_Nombre_permiso,@S_descripcion_permiso)
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

/**Procedimientos almacenados UsuarioRol**/

--all UsuarioRol
create procedure PA_Obtener_todos_UsuarioRol
AS BEGIN
	BEGIN TRY
		Select S_id_rol_usuario, S_id_Usuario, S_id_rol  from S_usuario_rol
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

-- UsuarioRol por ID
create procedure PA_obtener_UsuarioRol_por_id  @S_id_rol_usuario int
AS BEGIN
	BEGIN TRY
		Select  S_id_rol_usuario, S_id_Usuario, S_id_rol from S_usuario_rol
		where @S_id_rol_usuario = S_usuario_rol.S_id_rol_usuario
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--insertar RolUsuario
create procedure PA_Insertar_UsuarioRol @S_id_Usuario int,
@S_id_rol int
AS BEGIN
	BEGIN TRY
		insert into S_usuario_rol values(@S_id_Usuario,@S_id_rol)
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

/**Procedimientos almacenados rolPermiso**/

--all rolPermiso
create procedure PA_Obtener_todos_rolPermiso
AS BEGIN
	BEGIN TRY
		Select  S_id_rol_permiso, S_id_rol, S_id_permiso from S_rol_permiso
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--RolPermiso por ID
create procedure PA_obtener_rolPermiso_por_id @S_id_rol_permiso int
AS BEGIN
	BEGIN TRY
		Select S_id_rol_permiso, S_id_rol, S_id_permiso from S_rol_permiso
		where @S_id_rol_permiso = S_rol_permiso.S_id_rol_permiso
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END

--insertar RolPermiso

create procedure PA_Insertar_RolPermiso  @S_id_rol int,  @S_id_permiso int
AS BEGIN
	BEGIN TRY
		insert into S_usuario_rol values(@S_id_rol, @S_id_permiso)
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumero,
		ERROR_MESSAGE() AS ErrorMensaje,
		ERROR_PROCEDURE() AS ErrorProcedimiento;
	END CATCH
END








