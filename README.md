# _Hair Salon_

https://github.com/maxdhs/HairSalon.Solution

#### A web app for a hair salon stylist and client organizer.

#### By **Maxwell Dubin**

## Description

A c# web app which keeps track of stylists which work for a hair salon and all their individual clients and specialties.

### Specs
| Spec | Input | Output |
| :-------------     | :------------- | :------------- |
| See a list of all stylists. | |  |
| Select a stylist, see their details, and see a list of all clients that belong to that stylist. | | |
| Add new stylists to the system when they are hired. |  |  | Add new clients to a specific stylist| |   |

## Setup/Installation Requirements

In MySQL:

* CREATE DATABASE maxwell_dubin;
* USE maxwell_dubin;
* CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistId INT);
* CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
* CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255));
* Clone this repository: $ git clone https://github.com/maxdhs/HairSalon.Solution
* To run the program, first navigate to the location of the HairSalon.cs file then Dotnet run;
* To run the tests, use these commands: $ cd HairSalon.Tests $ dotnet test

## Known Bugs
* No known bugs at this time.

## Technologies Used

* _C#_
* _.Net_
* _VS CODE_

## Support and contact details

_Contact Maxwell Dubin - maxdhs@gmail.com._

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2018 **_Maxwell Dubin_**