create database `db_lib`;

create user 'user_lib'@'localhost' identified by '123456';
grant select, insert, delete, update on `db_lib`.* to 'user_lib'@'localhost';