# Library-System <br>
DBMS used is MySQL <br>
Using MySQL server 83 <br>

Assuming the console app is only used by the librarian <br>
Assuming that there is a root@localhost user <br>
Open any powershell/terminal, login to root user:<br>
mysql -u root -p <br>
Enter root user password <br>
MySQL server command line queries <br>
CREATE USER 'Librarian'@'localhost' IDENTIFIED BY 'password123'; <br>
grant all privileges on *.* to 'Librarian'@'localhost'; <br>

Database was manually created <br>
