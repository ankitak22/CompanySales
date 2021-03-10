# CompanySales

Aim: In the given project the Sales of a company has been displayed across regions in particular month/year.

Technologies used: .Net MVC 4, JavaScript, SQL Server (hosted on AWS)

The display of the sales data across regions was projected in Pivot using LINQ.
This project structure implements a ISalesService interface to handle all Database connections.

Assumptions
•	It was considered that the Database structure for the Entity with the sales details were (Month, State/County, Sale).
•	Month, State and Sale cannot be null.
•	For a Sate in a particular month if sale is 0, a new data row will not be created.

![](CompanySales/CompanySales/Screenshots/Screenshot1.png)
