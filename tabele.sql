DROP DATABASE IF EXISTS flightsManagement;
CREATE DATABASE flightsManagement;
USE flightsManagement;

CREATE TABLE IF NOT EXISTS customer(
id_customer INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
username VARCHAR(40),
password_c VARCHAR(12),
ID VARCHAR(20),
first_name VARCHAR(20),
last_name VARCHAR(20),
nationality VARCHAR(20),
date_birth DATE
); 

CREATE TABLE IF NOT EXISTS phone(
id_phone INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
type_phone VARCHAR(20),
phone VARCHAR(20),
id_customer INT,
FOREIGN KEY (id_customer) REFERENCES customer(id_customer)
);

CREATE TABLE IF NOT EXISTS luggage(
id_luggage INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
id_customer INT, 
luggage_no INT,
max_weight INT,
FOREIGN KEY (id_customer) REFERENCES customer(id_customer)
); 

CREATE TABLE IF NOT EXISTS aircraft(
id_aircraft INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
model VARCHAR(20),
sits_no INT,
max_weight FLOAT,
engine_check DATE
);

CREATE TABLE IF NOT EXISTS company(
id_company INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
company_name VARCHAR(20),
fiscal_number INT,
employees_number INT,
clients_services_phone VARCHAR(20),
contact_email VARCHAR(40),
website VARCHAR(50) 
);

CREATE TABLE IF NOT EXISTS flight(
id_flight INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
id_aircraft INT,
id_company INT,
departure_city VARCHAR(20),
arrival_city VARCHAR(20),
departure_time TIME,
arrival_time TIME,
time_flight TIME,
departure_airport_adress VARCHAR(200),
arrival_airport_adress VARCHAR(200),
first_class_seats_no INT,
second_class_seats_no INT,
economic_class_seats_no INT,
price_first_class_per_seat INT,
price_second_class_per_seat INT,
price_economic_class_per_seat INT,
discount_under18 INT,
FOREIGN KEY(id_aircraft) REFERENCES aircraft(id_aircraft),
FOREIGN KEY (id_company) REFERENCES company(id_company)
);

CREATE TABLE IF NOT EXISTS days(
id_day INT PRIMARY KEY UNIQUE,
operation_days VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS flightDays(
id_flight INT,
id_day INT,
CONSTRAINT pk_fd PRIMARY KEY (id_flight,id_day),
FOREIGN KEY (id_flight) REFERENCES flight(id_flight),
FOREIGN KEY (id_day) REFERENCES days(id_day)
); 

CREATE TABLE IF NOT EXISTS staff(
id_staff INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
first_name VARCHAR(20),
last_name VARCHAR(20),
gender VARCHAR(2),
date_of_birth DATE,
nationality VARCHAR(30), 
salary FLOAT, 
job VARCHAR(20),
date_of_employment DATE
);

CREATE TABLE IF NOT EXISTS homeAdress(
id_staff INT PRIMARY KEY,
street VARCHAR(40),
street_no INT,
building INT,
floor INT,
apartament INT,
city VARCHAR(40),
zip_code VARCHAR(20),
country VARCHAR(40),
FOREIGN KEY (id_staff) REFERENCES staff(id_staff)
);

CREATE TABLE IF NOT EXISTS flightStaff(
id_flight INT,
id_staff INT,
CONSTRAINT pk_fs PRIMARY KEY (id_flight, id_staff),
FOREIGN KEY (id_flight) REFERENCES flight(id_flight),
FOREIGN KEY (id_staff) REFERENCES staff(id_staff)
);

CREATE TABLE IF NOT EXISTS booking(
id_booking INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
id_customer INT,
id_flight INT,
departure_date DATE,
seats_first_class INT,
seats_second_class INT,
seats_economic_class INT,
under18_persons INT,
price_total FLOAT,
FOREIGN KEY (id_customer) REFERENCES customer(id_customer),
FOREIGN KEY (id_flight) REFERENCES flight(id_flight)
);

CREATE TABLE IF NOT EXISTS payment(
id_payment INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
type_card VARCHAR(20),
name_on_card VARCHAR(80),
card_no VARCHAR(20),
expiration_date DATE,
cvv INT,
money_on_card FLOAT,
id_booking INT UNIQUE,
FOREIGN KEY (id_booking) REFERENCES booking(id_booking)
);

CREATE TABLE IF NOT EXISTS rentCar(
id_rentCar INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
car_name VARCHAR(20),
car_model VARCHAR(20),
price_per_day FLOAT
); 

CREATE TABLE IF NOT EXISTS modelAvailability(
id_availability INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
countries_availability VARCHAR(200),
id_rentCar INT,
FOREIGN KEY (id_rentCar) REFERENCES rentCar(id_rentCar)
);

CREATE TABLE IF NOT EXISTS callCab(
id_callCab INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
company_name VARCHAR(100),
phone_num VARCHAR(20),
city VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS medicalServices(
id_medicalServices INT PRIMARY KEY UNIQUE AUTO_INCREMENT,
type_service VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS services(
id_service INT PRIMARY KEY AUTO_INCREMENT UNIQUE,
id_booking INT,
id_rentCar INT,
id_callCab INT,
id_medicalServices INT,
FOREIGN KEY (id_booking) REFERENCES booking(id_booking),
FOREIGN KEY (id_rentCar) REFERENCES rentCar(id_rentCar),
FOREIGN KEY (id_callCab) REFERENCES callCab(id_callCab),
FOREIGN KEY (id_medicalServices) REFERENCES medicalServices(id_medicalServices) 
);


