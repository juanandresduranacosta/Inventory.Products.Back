# Inventory Products - Documentación Técnica

## Descripción
Bienvenido a la documentación técnica de la plataforma **Inventory Products**. Esta aplicación te permite manejar productos mediante el control de stock. Utiliza diversas tecnologías como C#, SQL Server, Amazon Cognito, Amazon SQS, JWT y Dapper.

## Requisitos Previos
Antes de comenzar, asegúrate de tener instalados los siguientes elementos en tu entorno de desarrollo:

- [Visual Studio](https://visualstudio.microsoft.com/es/) (versión recomendada: [Visual Studio Community](https://visualstudio.microsoft.com/es/vs/community/))
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)

## Configuración del Proyecto

1. **Clonar el Repositorio:**
   ```bash
   git clone [https://github.com/juanandresduranacosta/Inventory.Products.Back.git]
   cd Inventory.Products.Back

2. **Scripts Db**
   ```sql
   CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TypeElaboration INT NOT NULL,
    Name VARCHAR(200),
    Status INT NOT NULL,
    CreationDate DATETIME NOT NULL,
    UpdateDate DATETIME NULL,
    CreationUser INT NOT NULL,
    
    FOREIGN KEY (TypeElaboration) REFERENCES ElaborationTypes(Id),
    FOREIGN KEY (Status) REFERENCES ProductsStatus(Id),
    FOREIGN KEY (CreationUser) REFERENCES Users(Id)
    );
    
    CREATE TABLE ElaborationTypes(
    	Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(200)
    );
    CREATE TABLE ProductsStatus(
    	Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(200)
    );
    
    CREATE TABLE Users(
    	Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(200),
        Email VARCHAR(500),
        Password VARCHAR(500),
        CreationDate DATETIME
    );
    
    SELECT*FROM Users u ;
    EXEC SpInsertUser 'JUAN', 'juan@yopmail.com', '456';
    CREATE PROCEDURE SpInsertUser
        @PuserName VARCHAR(200),
        @PuserEmail VARCHAR(500),
        @PuserPassword VARCHAR(500)
    AS
    BEGIN
        DECLARE @CreationDate DATETIME = GETDATE();
    
        INSERT INTO Users (Name, Email, Password, CreationDate)
        VALUES (@PuserName, @PuserEmail, @PuserPassword, @CreationDate);
    END;
    
    -- EXEC SpInsertProducts 1, 'Medias', 3, 1;
    	CREATE  PROCEDURE SpInsertProducts(
		@PtypeElaboration INT ,
		@Pname VARCHAR(200) ,
		@Pstatus INT ,
		@PcreationUser INT = NULL
	)
	AS
	BEGIN
		INSERT INTO Products(TypeElaboration, Name, Status, CreationDate, UpdateDate, CreationUser)
			VALUES(@PtypeElaboration, @Pname, @Pstatus, GETDATE(), NULL, 1)
	END;
    
    
    -- EXEC SpUpdateProducts 1, 1, 'Boxers', 3;
    CREATE PROCEDURE SpUpdateProducts(
    	@Pid INT, 
    	@PtypeElaboration INT ,
    	@Pname VARCHAR(200) ,
    	@Pstatus INT 
    )
    AS
    BEGIN
    	UPDATE Products SET TypeElaboration= @PtypeElaboration, Name= @Pname, Status= @Pstatus, UpdateDate = GETDATE() where Id = @Pid;
    END;
    
    --EXEC SpGetProducts 1, 2;
    
    CREATE PROCEDURE SpGetProducts(
    	@Ppage INT,
    	@PpageSize INT,
    	@PtypeElaboration INT = NULL,
    	@Pname VARCHAR(200) = NULL,
    	@Pstatus INT = NULL,
    	@PstartDate DATETIME = NULL,
    	@PendDate DATETIME = NULL
    )
    AS
    BEGIN
    	DECLARE @V_ROW_NUM INT;
        DECLARE @V_ROWS INT;
        DECLARE @V_OFFSET INT;
        SET @V_ROW_NUM = 0;
        SET @V_OFFSET = (@Ppage - 1) * @PpageSize;    

    CREATE TABLE T_Products (
        Id INT,
        TypeElaboration VARCHAR(200),
        Name VARCHAR(200),
        Status VARCHAR(200),
        CreationDate DATETIME,
        UpdateDate DATETIME,
        CreationUser VARCHAR(200)
    );
    INSERT INTO T_Products
        SELECT 
			product.Id  Id,
        	elaborationTypes.Name TypeElaboration,
        	product.Name Name,
        	productsStatus.Name Status,
        	product.CreationDate CreationDate,
        	product.UpdateDate UpdateDate,
        	users.Name CreationUser
        FROM Products product WITH (NOLOCK)
        INNER JOIN ElaborationTypes elaborationTypes ON elaborationTypes.Id  = product.TypeElaboration
        INNER JOIN ProductsStatus productsStatus ON productsStatus.Id = product.Status 
        INNER JOIN Users users ON users.Id  = product.CreationUser 
        WHERE 
        1 = 1
        AND( (product.TypeElaboration = @PtypeElaboration) OR @PtypeElaboration IS NULL)
        AND( (product.Status = @Pstatus) OR @Pstatus IS NULL)
        AND(product.Name  LIKE CONCAT('%',@Pname,'%') OR @Pname IS NULL)
        AND(
            (
                @PstartDate IS NOT NULL AND @PendDate IS NOT NULL 
                AND 
                    (
                        product.CreationDate  between @PstartDate and @PendDate
                    ) 
                ) 
            OR (@PstartDate IS NULL AND @PendDate IS NULL))
        ORDER BY product.CreationDate  DESC;
    
		SELECT @V_ROWS = COUNT(1) FROM T_Products;
		SELECT
		    Id,
		    TypeElaboration,
		    Name,
		    Status,
		    CreationDate,
		    UpdateDate,
		    CreationUser
		FROM T_Products WITH (NOLOCK)
		ORDER BY CreationDate DESC
		OFFSET @V_OFFSET ROWS
		FETCH NEXT @PpageSize ROWS ONLY;
        SELECT @V_ROWS AS Total;

        DROP TABLE T_Products;
    END;
    
    
    
    
    
    -- EXEC SpVerifyUser 'Admin@yopmailcom', '123';
    
    CREATE PROCEDURE SpVerifyUser(
    	@Pemail VARCHAR(200),
    	@Ppassword VARCHAR(500)
    )
    AS
    BEGIN
    	SELECT COUNT(*) FROM Users WHERE Email = @Pemail AND Password = @Ppassword;
    END;
    
    -- EXEC SpGetElaborationType 1;
    
    CREATE PROCEDURE SpGetElaborationType(@Pid INT = NULL)
    AS 
    BEGIN
    	SELECT*FROM ElaborationTypes WHERE Id = @Pid or @Pid IS NULL;
    END;
    
    -- EXEC SpGetProductsStatus 1;
    
    CREATE PROCEDURE SpGetProductsStatus(@Pid INT = NULL)
    AS 
    BEGIN
    	SELECT*FROM ProductsStatus WHERE Id = @Pid or @Pid IS NULL;
    END;
    
    
    
    --SELECT*FROM Products;
    --SELECT*FROM ElaborationTypes;
    --SELECT*FROM ProductsStatus;
    --SELECT*FROM Users;
    
    INSERT INTO ProductsStatus(Name)
    VALUES ('En Stock');
    
    INSERT INTO ProductsStatus(Name)
    VALUES ('Defectuoso');
    
    INSERT INTO ProductsStatus(Name)
    VALUES ('En proceso de entrada');
	
    INSERT INTO ProductsStatus(Name)
    VALUES ('En proceso de salida');
    
    INSERT INTO ElaborationTypes(Name)
    VALUES('Elaborado a mano');
    
    INSERT INTO ElaborationTypes(Name)
    VALUES('Elaborado a mano y máquina');
    
    INSERT INTO Users(Name, Email, Password, CreationDate)
    VALUES('Admin', 'Admin@yopmailcom', '123', GETDATE());


 # Funcionalidades Principales


## Tecnologías Utilizadas
- C#: El backend de la aplicación está desarrollado en C#.
- SQL Server: La base de datos utiliza SQL Server para almacenar la información de los productos y el stock.
- Amazon Cognito: Se utiliza para la autenticación y gestión de usuarios.
- Amazon SQS: Se utiliza para la gestión de colas de mensajes para procesos asíncronos.
- JWT (JSON Web Tokens): Los tokens JWT se utilizan para la autenticación y autorización de usuarios.
- Dapper: Se utiliza como ORM para interactuar con la base de datos.


## Funcionalidades Principales
- Manejo de Productos: Agregar, editar, eliminar y consultar productos.
- Gestión de Stock: Controlar el stock de los productos.
- Autenticación de Usuarios: Utiliza Amazon Cognito para la autenticación segura de usuarios.
- Cola de Mensajes: Emplea Amazon SQS para procesos asíncronos y manejo de eventos.
- Proceso carga masiva: Hace uso de SQS para enviar los productos a crear y un worker "Microservicio" esta consumiendo de ella e insertando en la tabla.


# Información adicional
- Despliegue: Esta Api se desplego en un [Amazon Elastic Beanstalk](https://aws.amazon.com/es/elasticbeanstalk/) para las pruebas de funcionalidad desde
  el FRONT, de igual manera en este archivo se dejan los scripts de db para las pruebas.
- Autenticacion: Como se comento en la parte de arriba use Cognito para la autenticacion y uso de JWT Tokens.



## ⚠️ Importante
El back esta configurado con una cuenta de AWS personal, en donde esta a base de datos que es un (Amazon Relational Database Service)[https://aws.amazon.com/es/rds/], la cola de 
(Amazon SQS)[https://aws.amazon.com/es/sqs/], la configuracion de (Amazon Cognito)[https://aws.amazon.com/es/cognito/] y por obvias razones el [Amazon Elastic Beanstalk](https://aws.amazon.com/es/elasticbeanstalk/).

