USE [Northwind]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[AllOrders]

SELECT	@return_value as 'Return Value'

GO



	SELECT	OD.OrderID,
			C.CompanyName,
			C.ContactName,
			SUM(OD.UnitPrice*OD.Quantity) AS TotalPrice--,
			--*

	FROM	Orders O
	INNER JOIN Customers C ON O.CustomerID = C.CustomerID
	LEFT OUTER JOIN [Order Details] OD ON O.OrderID = OD.OrderID
	GROUP BY OD.OrderID,
				C.CompanyName,
			C.ContactName




	SELECT	*
	FROM	Orders O
	INNER JOIN Customers C ON O.CustomerID = C.CustomerID
	LEFT OUTER JOIN [Order Details] OD ON O.OrderID = OD.OrderID
	GROUP BY OD.OrderID


