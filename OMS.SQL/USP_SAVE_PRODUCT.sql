IF EXISTS (SELECT 1 FROM SYS.procedures WHERE NAME ='USP_SAVE_PRODUCT')
DROP PROCEDURE USP_SAVE_PRODUCT
GO
/*********************************************************
**
**
**
**
**********************************************************/
CREATE PROCEDURE USP_SAVE_PRODUCT
(
		@ProductID int ,
		@ProductName varchar(500),
		@weight  int ,
		@height int ,
		@image binary NULL,
		@SKU varchar(500) NULL,
		@barcode varchar(500) NULL,
		@quantity int NULL
)
AS
BEGIN
	IF ISNULL(@ProductID,0) =0 
	BEGIN
		INSERT INTO ORM_Product(ProductName,weight,height,image,SKU,barcode,quantity,INSERTEDDATE,INSERTBY)
		SELECT @ProductName ProductName,@weight weight,@height height,@image image,@SKU SKU,@barcode barcode,@quantity quantity,GETDATE() INSERTEDDATE,'ADMIN' INSERTBY
	END
	ELSE
	BEGIN
		UPDATE ORM_Product 
			SET ProductName = CASE WHEN ISNULL(@ProductName,'') != ProductName THEN @ProductName ELSE ProductName END,
				[weight]= CASE WHEN ISNULL(@weight,'') != [weight] THEN @weight ELSE [weight] END,
				[height]= CASE WHEN ISNULL(@height,'') != [height] THEN @height ELSE [height] END,
				image= CASE WHEN @image  != image THEN @image ELSE image END,
				SKU= CASE WHEN ISNULL(@SKU,'') != SKU THEN @SKU ELSE SKU END,
				barcode= CASE WHEN ISNULL(@barcode,'') != barcode THEN @barcode ELSE barcode END,
				quantity= CASE WHEN ISNULL(@quantity,'') != quantity THEN @quantity ELSE quantity END
		WHERE ProductID = @ProductID
	END

END



