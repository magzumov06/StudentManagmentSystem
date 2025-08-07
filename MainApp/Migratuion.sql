CREATE DATABASE "StudentManagmentSystem_DB";
create table students
(
    id        serial primary key,
    Firstname varchar(50) not null,
    lastname  varchar(50),
    address   varchar(100),
    birthdate  date
);

create table courses
(
    id        serial primary key,
    title varchar(50),
    Description varchar(50)
);