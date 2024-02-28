CREATE DATABASE IF NOT EXISTS PetClinic;
USE PetClinic;

DROP TABLE IF EXISTS sales_items;
DROP TABLE IF EXISTS payment_inventory;
DROP TABLE IF EXISTS payment_clinic;
DROP TABLE IF EXISTS inventory;
DROP TABLE IF EXISTS veterinarians;
DROP TABLE IF EXISTS medical_records;
DROP TABLE IF EXISTS appointments;
DROP TABLE IF EXISTS pets;
DROP TABLE IF EXISTS pet_owners;
DROP TABLE IF EXISTS users;


CREATE TABLE IF NOT EXISTS users (
    user_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) UNIQUE,
    password_hash VARCHAR(255),
    role VARCHAR(50),
    email VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);



CREATE TABLE IF NOT EXISTS pet_owners (
    owner_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    phone VARCHAR(20),
    email VARCHAR(100),
    address VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE IF NOT EXISTS pets (
    pet_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    owner_id INT,
    name VARCHAR(50),
    species VARCHAR(50),
    breed VARCHAR(50),
    age INT,
    gender VARCHAR(10),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (owner_id) REFERENCES pet_owners(owner_id) ON DELETE CASCADE
);
 

CREATE TABLE IF NOT EXISTS appointments (
    appointment_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    pet_id INT,
    vet_id INT,
    appointment_date TIMESTAMP,
    reason VARCHAR(255),
    notes TEXT,
    status VARCHAR(50),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (pet_id) REFERENCES pets(pet_id) ON DELETE CASCADE,
    FOREIGN KEY (vet_id) REFERENCES veterinarians(vet_id);
);


CREATE TABLE IF NOT EXISTS medical_records (
    record_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    pet_id INT,
    visit_date TIMESTAMP,
    description TEXT,
    veterinarian VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (pet_id) REFERENCES pets(pet_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS veterinarians (
    vet_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    specialty VARCHAR(255),
    working_hours DECIMAL(10, 2),
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);



CREATE TABLE IF NOT EXISTS inventory (
    item_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    description TEXT,
    quantity INT,
    reorder_level INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS payment_clinic (
    payment_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    record_id INT,
    owner_id INT,
    amount DECIMAL(10, 2),
    payment_date TIMESTAMP,
    payment_method VARCHAR(50),
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (record_id) REFERENCES medical_records(record_id) ON DELETE CASCADE
    FOREIGN KEY (owner_id) REFERENCES pet_owners(owner_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS payment_inventory (
    payment_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    owner_id INT,
    amount DECIMAL(10, 2),
    payment_date TIMESTAMP,
    payment_method VARCHAR(50),
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (owner_id) REFERENCES pet_owners(owner_id) ON DELETE CASCADE
);


CREATE TABLE IF NOT EXISTS sales_items (
    payment_id INT,
    item_id INT,
    quantity INT,
    FOREIGN KEY (payment_id) REFERENCES payment_inventory(payment_id) ON DELETE CASCADE,
    FOREIGN KEY (item_id) REFERENCES inventory(item_id) ON DELETE CASCADE,
);



