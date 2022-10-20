DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS animals;

CREATE TABLE users (
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    password VARCHAR(256) NOT NULL,
	email_address VARCHAR(30) UNIQUE NOT NULL,
    admin VARCHAR(1) NOT NULL,
);

CREATE TABLE animals (
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(25) NOT NULL,   
    species VARCHAR (16),
    description VARCHAR(220),
    age int,
    color VARCHAR (16),
    type VARCHAR (16),
    image text,
    bgimage text,
    inclusion VARCHAR (12),
    );

    INSERT INTO users (name, password, email_address, admin) VALUES
    ('Admin', 'C1C224B03CD9BC7B6A86D77F5DACE40191766C485CD55DC48CAF9AC873335D6F', 'admin@admin', 'Y'),
    ('aa', '961B6DD3EDE3CB8ECBAACBD68DE040CD78EB2ED5889130CCEB4C49268EA4D506', 'aa@aa', 'N');
