CREATE DATABASE proyectoFideGames;

use proyectoFideGames;

CREATE TABLE Employee(
	    employeeId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	    firstName nvarchar(75) NOT NULL,
		lastName nvarchar(50) NULL,
	    gender char(10) NOT NULL CHECK(gender IN ('Hombre','Mujer','Otro')),
	    birthDate datetime NULL,
	    hireDate datetime NULL,
	    salary int NULL,
);

CREATE TABLE Roles(
	    rolId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	    roleName nvarchar(50) NOT NULL
);

INSERT INTO Roles (roleName)
VALUES('Administrador'),
('Supervisor'),
('Vendedor');

CREATE TABLE Users(
	    userId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	    userName nvarchar(50) NOT NULL,
	    password nvarchar(500) NOT NULL,
		user_active CHAR(1),
        userImage nvarchar(150),
		employeeId int NOT NULL FOREIGN KEY REFERENCES Employee(employeeId),
        rol int NOT NULL FOREIGN KEY REFERENCES Roles(rolId)
);

CREATE TABLE clients(
    client_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    client_name1 VARCHAR(60) NOT NULL,
    client_name2 VARCHAR(60),
    client_lastname1 VARCHAR(60) NOT NULL,
    client_lastname2 VARCHAR(60),
    client_id_card VARCHAR(50) NOT NULL,
    client_email VARCHAR(80) NOT NULL,
    client_direcction VARCHAR(250)
);

CREATE TABLE type_product(
    type_product_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    type_product_name VARCHAR(60) NOT NULL
);

CREATE TABLE deparment_product(
    deparment_product_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    deparment_product_name VARCHAR(80) NOT NULL
);

CREATE TABLE brand_product(
    brand_product_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    brand_product_name VARCHAR(80) NOT NULL
);

CREATE TABLE product(
    product_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    product_name VARCHAR(60) NOT NULL,
    type_product_id int NOT NULL FOREIGN KEY REFERENCES type_product(type_product_id),
    deparment_id int NOT NULL FOREIGN KEY REFERENCES deparment_product(deparment_product_id),
    brand_id int NOT NULL FOREIGN KEY REFERENCES brand_product(brand_product_id),
    product_description VARCHAR(500),
    quantities int NOT NULL,
    price MONEY NOT NULL
);

CREATE TABLE invoice_status(
   invoice_status_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
   invoice_status_name VARCHAR(80) NOT NULL
);

INSERT INTO invoice_status (invoice_status_name)
VALUES('Creada'),
('Pendiente de Pago'),
('Pagada'),
('Anulada'),
('Completada');


CREATE TABLE invoice(
    id_invoice int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    user_id int NOT NULL FOREIGN KEY REFERENCES clients(client_id),
    seller int NOT NULL FOREIGN KEY REFERENCES Users(userId),
    date_sell timestamp,
    sale_total MONEY NOT NULL,
    payment_des VARCHAR(60) NOT NULL,
    invoice_status INT FOREIGN KEY REFERENCES invoice_status(invoice_status_id),
);

CREATE TABLE invoice_detail(
    detail_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_invoice INT NOT NULL FOREIGN KEY REFERENCES invoice(id_invoice),
    product_id INT FOREIGN KEY REFERENCES product(product_id),
    quantities int NOT NULL,
    product_price MONEY NOT NULL
);

INSERT INTO Employee (firstName,lastName,gender,birthDate,hireDate,salary)
VALUES ('Admin','Admin','Otro','2022-01-01 12:00:00.000','2022-01-01 12:00:00.000',0);

INSERT INTO Users (userName,password,user_active,employeeId,rol)
VALUES ('admin','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918',1,1,1);

-- User:admin
--password:admin
--DROP TABLE invoice_detail,invoice,invoice_status,product,brand_product,deparment_product,type_product,clients,UserRolesMapping,Roles,Users,Employee;
