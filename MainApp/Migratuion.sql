CREATE DATABASE "StudentManagmentSystem_DB";

CREATE TABLE students (
      id SERIAL PRIMARY KEY,
      firstname VARCHAR(100),
      lastname VARCHAR(100),
      birthday DATE,
      address TEXT
);

CREATE TABLE courses (
     id SERIAL PRIMARY KEY,
     title VARCHAR(100),
     description TEXT,
     student_id INTEGER REFERENCES students(id)
);
