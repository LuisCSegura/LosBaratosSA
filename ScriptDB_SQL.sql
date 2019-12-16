DROP TABLE IF EXISTS usuarios;
CREATE TABLE usuarios(
	id serial,
	username VARCHAR(100) NOT NULL,
	nombre VARCHAR(100)NOT NULL,
	contrasenna VARCHAR(500)NOT NULL,
	telefono VARCHAR (20)NOT NULL,
	correo VARCHAR(100) NOT NULL,
	cedula VARCHAR(20)NOT NULL,
	activo BOOL DEFAULT true,
	administrador BOOL DEFAULT false,
	CONSTRAINT pk_usuario PRIMARY KEY(id),
	CONSTRAINT unq_user_usu UNIQUE(username)
);
CREATE TABLE clientes(
	id serial,
	nombre VARCHAR(100) NOT NULL,
	telefono VARCHAR (20)NOT NULL,
	correo VARCHAR(100) NOT NULL,
	cedula VARCHAR(20)NOT NULL,
	activo BOOL DEFAULT true,
	CONSTRAINT pk_cliente PRIMARY KEY(id)
);
CREATE TABLE usuarios_clientes(
	id_usuario INT NOT NULL,
	id_cliente INT NOT NULL,
	CONSTRAINT fk_usu_usucli FOREIGN KEY(id_usuario)REFERENCES usuarios(id),
	CONSTRAINT fk_cli_usucli FOREIGN KEY (id_cliente)REFERENCES clientes(id)
);

CREATE TABLE productos(
	id serial,
	nombre VARCHAR(100)NOT NULL,
	codigo VARCHAR(100)NOT NULL,
	categoria VARCHAR(100)NOT NULL,
	cantidad int default 1,
	precio numeric default 1,
	activo bool default true,
	CONSTRAINT pk_producto PRIMARY KEY(id),
	CONSTRAINT unq_cod_pro UNIQUE(codigo)
);
CREATE TABLE facturas(
	id serial,
	fecha timestamp without time zone NOT NULL,
	id_cliente int NOT NULL,
	total numeric default 0,
	activo bool DEFAULT true,
	CONSTRAINT pk_factura PRIMARY KEY(id),
	CONSTRAINT fk_cli_fac FOREIGN KEY(id_cliente) REFERENCES clientes(id)
);
CREATE OR REPLACE FUNCTION fc_insrt_usuario() RETURNS TRIGGER
AS
$$
BEGIN
	IF(NEW.Administrador=false)
	THEN
	BEGIN
	INSERT INTO clientes(nombre,telefono,correo,cedula)
	VALUES(NEW.nombre,NEW.telefono,NEW.correo,NEW.cedula);
	INSERT INTO usuarios_clientes(id_usuario,id_cliente)
	VALUES((SELECT MAX(id) FROM usuarios),(SELECT MAX(id) FROM clientes));
	END;
	END IF;
	
RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER tr_insrt_usuario AFTER INSERT ON usuarios
FOR EACH ROW EXECUTE PROCEDURE fc_insrt_usuario();





INSERT INTO usuarios(username,nombre,contrasenna,telefono,correo,cedula,administrador)
VALUES('LuisKa777','Luis Carlos Segura','sele2014','87187947','louis710229@gmail.com','208090758',true)


select*from usuarios

