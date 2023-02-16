CREATE FUNCTION [dbo].[accountTotal](@IncreaseMode int)
RETURNS decimal(18,2)
AS
-- Returns the stock level for the product.
BEGIN
    DECLARE @ret decimal(18,2), @DEBIT decimal(18,2), @CREDIT decimal(18,2);
	select @DEBIT = P.AccDebit, @CREDIT =  P.AccCredit 
	from GP_Demo.dbo.TB_FMS_Account P
	if (@IncreaseMode = 1)
	Set @ret = @DEBIT - @CREDIT;
	else
	set @ret = @CREDIT - @DEBIT;
	RETURN @ret;
END;