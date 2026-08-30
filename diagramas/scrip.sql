CREATE TABLE usuario (
    id               INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre           VARCHAR(100) NOT NULL,
    correo           VARCHAR(150) NOT NULL UNIQUE,
    contrasena_hash  VARCHAR(255) NOT NULL,
    rol              VARCHAR(20)  NOT NULL CHECK (rol IN ('Estudiante','Instructor','Administrador')),
    estado           VARCHAR(20)  NOT NULL DEFAULT 'Activo'
);

CREATE TABLE estudiante (
    id            INT PRIMARY KEY,
    es_premium    BOOLEAN   NOT NULL DEFAULT FALSE,
    fecha_acceso  TIMESTAMP,
    CONSTRAINT fk_estudiante_usuario FOREIGN KEY (id) REFERENCES usuario(id)
);

CREATE TABLE instructor (
    id           INT PRIMARY KEY,
    especialidad VARCHAR(100),
    biografia    TEXT,
    CONSTRAINT fk_instructor_usuario FOREIGN KEY (id) REFERENCES usuario(id)
);

CREATE TABLE administrador (
    id            INT PRIMARY KEY,
    nivel_acceso  INT NOT NULL DEFAULT 1,
    CONSTRAINT fk_administrador_usuario FOREIGN KEY (id) REFERENCES usuario(id)
);

CREATE TABLE tematica (
    id              INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nombre          VARCHAR(100) NOT NULL,
    imagen_portada  VARCHAR(255)
);

CREATE TABLE modulo (
    id             INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nivel          VARCHAR(20) NOT NULL CHECK (nivel IN ('Basico','Intermedio','Avanzado')),
    precio         DECIMAL(10,2) NOT NULL DEFAULT 0,
    incluye_kit    BOOLEAN NOT NULL DEFAULT FALSE,
    id_tematica    INT NOT NULL,
    id_instructor  INT NOT NULL,
    CONSTRAINT fk_modulo_tematica   FOREIGN KEY (id_tematica)   REFERENCES tematica(id),
    CONSTRAINT fk_modulo_instructor FOREIGN KEY (id_instructor) REFERENCES instructor(id)
);

CREATE TABLE leccion (
    id         INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    titulo     VARCHAR(150) NOT NULL,
    tipo       VARCHAR(20)  NOT NULL CHECK (tipo IN ('Video','Documento','Prueba','Otro')),
    orden      INT NOT NULL,
    id_modulo  INT NOT NULL,
    CONSTRAINT fk_leccion_modulo FOREIGN KEY (id_modulo) REFERENCES modulo(id)
);

CREATE TABLE tarea (
    id             INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    titulo         VARCHAR(150) NOT NULL,
    descripcion    TEXT,
    fecha_entrega  TIMESTAMP NOT NULL,
    id_modulo      INT NOT NULL,
    CONSTRAINT fk_tarea_modulo FOREIGN KEY (id_modulo) REFERENCES modulo(id)
);

CREATE TABLE entrega_tarea (
    id                     INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    url_archivo            VARCHAR(255) NOT NULL,
    fecha_entrega          TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    calificacion           DECIMAL(5,2),
    comentario             TEXT,
    id_tarea               INT NOT NULL,
    id_estudiante          INT NOT NULL,
    id_instructor_califica INT,
    CONSTRAINT fk_entrega_tarea       FOREIGN KEY (id_tarea)               REFERENCES tarea(id),
    CONSTRAINT fk_entrega_estudiante  FOREIGN KEY (id_estudiante)          REFERENCES estudiante(id),
    CONSTRAINT fk_entrega_instructor  FOREIGN KEY (id_instructor_califica) REFERENCES instructor(id)
);

CREATE TABLE clase_en_vivo (
    id             INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    fecha_hora     TIMESTAMP NOT NULL,
    url_sala       VARCHAR(255) NOT NULL,
    estado         VARCHAR(20) NOT NULL CHECK (estado IN ('Programada','EnCurso','Finalizada','Cancelada')),
    id_modulo      INT NOT NULL,
    id_instructor  INT NOT NULL,
    CONSTRAINT fk_clase_modulo     FOREIGN KEY (id_modulo)     REFERENCES modulo(id),
    CONSTRAINT fk_clase_instructor FOREIGN KEY (id_instructor) REFERENCES instructor(id)
);

CREATE TABLE clase_participante (
    id_clase       INT NOT NULL,
    id_estudiante  INT NOT NULL,
    fecha_ingreso  TIMESTAMP,
    PRIMARY KEY (id_clase, id_estudiante),
    CONSTRAINT fk_participante_clase      FOREIGN KEY (id_clase)      REFERENCES clase_en_vivo(id),
    CONSTRAINT fk_participante_estudiante FOREIGN KEY (id_estudiante) REFERENCES estudiante(id)
);

CREATE TABLE compra (
    id                 INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    monto              DECIMAL(10,2) NOT NULL,
    metodo_pago        VARCHAR(30) NOT NULL CHECK (metodo_pago IN ('Tarjeta','PSE','Nequi','Otro')),
    estado             VARCHAR(20) NOT NULL CHECK (estado IN ('Pendiente','Aprobado','Rechazado')),
    referencia_wompi   VARCHAR(100),
    fecha_compra       TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_estudiante      INT NOT NULL,
    id_modulo          INT NOT NULL,
    CONSTRAINT fk_compra_estudiante FOREIGN KEY (id_estudiante) REFERENCES estudiante(id),
    CONSTRAINT fk_compra_modulo     FOREIGN KEY (id_modulo)     REFERENCES modulo(id)
);

CREATE TABLE progreso (
    id                 INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    completado         BOOLEAN NOT NULL DEFAULT FALSE,
    porcentaje         DECIMAL(5,2) NOT NULL DEFAULT 0,
    fecha_completado   TIMESTAMP,
    id_estudiante      INT NOT NULL,
    id_modulo          INT NOT NULL,
    CONSTRAINT fk_progreso_estudiante FOREIGN KEY (id_estudiante) REFERENCES estudiante(id),
    CONSTRAINT fk_progreso_modulo     FOREIGN KEY (id_modulo)     REFERENCES modulo(id)
);

CREATE TABLE certificado (
    id                    INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    fecha_emision         TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    url_pdf               VARCHAR(255) NOT NULL,
    codigo_verificacion   VARCHAR(100) NOT NULL UNIQUE,
    id_estudiante         INT NOT NULL,
    id_modulo             INT NOT NULL,
    CONSTRAINT fk_certificado_estudiante FOREIGN KEY (id_estudiante) REFERENCES estudiante(id),
    CONSTRAINT fk_certificado_modulo     FOREIGN KEY (id_modulo)     REFERENCES modulo(id)
);
