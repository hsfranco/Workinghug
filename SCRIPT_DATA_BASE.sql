Create DataBase WorkHub		
GO

Use WorkHub		
GO

If Not Exists (Select * From sys.objects Where name = 'ArquivosImportados')
	Begin 
		Create Table ArquivosImportados(Id Int Primary Key Identity (1,1) Not Null,
										Comprador Varchar(100) Null,
										Descricao Varchar(100) Null,
										PrecoUnitario Float Null,
										Quantidade Int Null,
										Endereco Varchar(100) Null,
										Fornecedor Varchar(100) Null)
	End

