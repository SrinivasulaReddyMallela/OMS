/*********************************************************
**
**
**
**
**********************************************************/
CREATE PROCEDURE USP_Save_ORDERDETAILSByUserID
(
		@OrderDetailID INT,      
        @OrderID INT,
        @ProductID INT,
        @Quantity INT,
        @OrderQuantity INT,
        @StatusID INT,
        @OrderDate DATETIME,
        @UserID INT,                        
        @AdressLine1 VARCHAR(500),
        @AdressLine2 VARCHAR(500),
        @State VARCHAR(500),
        @ZipCode VARCHAR(500),
        @EmailID VARCHAR(500),
        @Landmark VARCHAR(500)
)
AS
BEGIN
	DECLARE @ADDRESSID INT
			--@OrderID INT

	IF ISNULL(@OrderDetailID,0) = 0
	BEGIN
	
		INSERT INTO ORM_Address(AdressLine1,AdressLine2,State,ZipCode,EmailID,Landmark,INSERTEDDATE,INSERTBY)
		SELECT @AdressLine1 AdressLine1,@AdressLine2 AdressLine2, @State State,@ZipCode ZipCode,@EmailID EmailID,@Landmark Landmark,GETDATE() INSERTEDDATE,'VUE ADMIN' INSERTBY
		SET @ADDRESSID =SCOPE_IDENTITY()  
		INSERT INTO ORM_ORDER(StatusID,AddressID,OrderDate,UserID,INSERTEDDATE,INSERTBY)
		SELECT @StatusID StatusID,@ADDRESSID AddressID,@OrderDate OrderDate,@UserID UserID,GETDATE() INSERTEDDATE,'VUE ADMIN' INSERTBY
		SET @OrderID=SCOPE_IDENTITY()  
		INSERT INTO ORM_ORDERDETAILS(OrderID,ProductID,Quantity,INSERTBY,INSERTEDDATE)
		SELECT @OrderID OrderID,@ProductID ProductID,@Quantity Quantity,'VUE ADMIN' INSERTBY,GETDATE() INSERTEDDATE
	END
	ELSE 
	BEGIN
		UPDATE ORM_ORDER SET StatusID = @StatusID WHERE UserID = @UserID AND OrderID = @OrderID
	END
END

