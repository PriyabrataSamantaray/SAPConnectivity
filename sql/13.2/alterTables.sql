if  not exists (select * from syscolumns where id = object_id('dbo.tempEmployees') and name = 'Cost_Center_Name')
begin
	ALTER TABLE tempEmployees ADD [Cost_Center_Name] nvarchar(75)
	ALTER TABLE tempEmployees ADD [employee_type] nvarchar(75)
	ALTER TABLE tempEmployees ADD [location] nvarchar(75)
end