# Kiki Courier Service App

A .NET Console application that calculates delivery cost, discount based on offer codes, and estimates delivery times using optimized vehicle scheduling. This project also includes automated tests to validate business logic.

## Project Structure

```
Kiki_CourierServiceApp/
│
├── Kiki_CourierServiceApp/          # Main application source
│   ├── Models/                      # Model classes (Package, Vehicle)
│   ├── Services/                    # Business logic (Cost + Delivery estimators)
│   └── Program.cs                   # Application entry point
│
├── Kiki_CourierServiceApp.Tests/    # Unit tests
│
└── README.md
```

## How to Use: Follow the steps below to extract, build, run, and test the application.

### 1) Unzip the Project

Unzip:
```
Kiki_CourierServiceApp.zip
```

It will create:
```
Kiki_CourierServiceApp/
```

### 2) Build the Project : Open a terminal inside the extracted folder and run:

```bash
dotnet build
```
This will restore dependencies and compile the project.

### 3) Run the Console Application

```bash
dotnet run --project Kiki_CourierServiceApp
```

### Or else you can run the project from menu > Run > Start debugging/ Run without debugging or F5.


Paste contents of sample input from below when prompted, select menu option and then press enter: 

### DELIVERY COST..................

Sample Input

100 3
PKG1 5 5 OFR001
PKG2 15 5 OFR002
PKG3 10 100 OFR003

OutPut: PackageID Discount TotalCost: 

PKG1 0 175
PKG2 0 275
PKG3 35 665

### DELIVERY SCHEDULER.......................
Sample Input : 
format >>
<base_delivery_cost> <number_of_packages>
<package_id1> <package_weight1_in_kg> <distance1_in_km> <offer_code1>
.....
.....
<number_of_vehicles> <max_speed> <max_carriable_weight>
```
100 5
PKG1 50 30 OFR001
PKG2 75 125 OFR008
PKG3 175 100 OFR003
PKG4 110 60 OFR002
PKG5 155 95 NA
2 70 200
```

Sample Output : 

## Expected Output Format

```
PKG1 <discount> <totalCost> <deliveryTime>
PKG2 <discount> <totalCost> <deliveryTime>
```

PKG1 0 750 3.98
PKG2 0 1475 1.78
PKG3 0 2350 1.42
PKG4 105 1395 0.85
PKG5 0 2125 4.19

### 4) Run Unit Tests : 

```bash
dotnet test
```
This validates the cost estimation and delivery time logic.



## Troubleshooting

- Set `"console": "integratedTerminal"` inside `.vscode/launch.json` if input is not accepted.
- Run `dotnet restore` if build fails.
 
## Technologies Used

.NET / C#

LINQ & Collections

NUnit / xUnit for Testing

Console Application Architecture

Object-Oriented Design Principles