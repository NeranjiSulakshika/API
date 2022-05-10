
SELECT * FROM slbfe.user_details;

CREATE TABLE `slbfe`.`user_details` (
    UserId int NOT NULL AUTO_INCREMENT,
    NIC varchar(15) NOT NULL,
    Name varchar(50) NOT NULL,
    Address varchar(100) NOT NULL,
    Age int NOT NULL,
    Profession varchar(50) NOT NULL,
    Email varchar(50) NOT NULL,
    Password varchar(50) NOT NULL,
    Affiliation varchar(10) NOT NULL,
    Qualification varchar(100) NULL,
    PRIMARY KEY (UserId)
);


SELECT * FROM slbfe.complaints;

CREATE TABLE `slbfe`.`complaints` (
    ComplaintId int NOT NULL AUTO_INCREMENT,
    Complaint varchar(1000) NOT NULL,
    Reply varchar(1000) NULL,
    PRIMARY KEY (ComplaintId)
);



SELECT * FROM slbfe.admin;

CREATE TABLE `slbfe`.`admin` (
    AdminId int NOT NULL AUTO_INCREMENT,
    UserName varchar(1000) NOT NULL,
    Password varchar(1000) NULL,
    PRIMARY KEY (AdminId)
);


INSERT INTO `slbfe`.`admin`
(`AdminId`,
`UserName`,
`Password`)
VALUES
(1,
'AdminSLBFE',
'Admin123');


