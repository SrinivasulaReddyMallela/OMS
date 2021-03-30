IF EXISTS (SELECT 1 FROM SYS.procedures WHERE NAME ='USP_GET_PRODUCT')
DROP PROCEDURE USP_GET_PRODUCT
GO
/*********************************************************
**
**
**
**
**********************************************************/
CREATE PROCEDURE USP_GET_PRODUCT
AS
BEGIN
	SELECT * FROM ORM_PRODUCT
END