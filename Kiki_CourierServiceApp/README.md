# Kiki_CourierServiceApp (.NET 9)
 
How to use:

        1) unzip Kiki_CourierServiceApp.zip
        2) dotnet build
        3) dotnet run --project Kiki_CourierServiceApp
        #  paste contents of sample input from below when prompted and then press enter
        4) dotnet test

Sample Input :

100 5
PKG1 50 30 OFR001
PKG2 75 125 OFR008
PKG3 175 100 OFR003
PKG4 110 60 OFR002
PKG5 155 95 NA
2 70 200

Sample Output : 

PKG1 0 750 3.98
PKG2 0 1475 1.78
PKG3 0 2350 1.42
PKG4 105 1395 0.85
PKG5 0 2125 4.19