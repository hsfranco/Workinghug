# Workinhub

O projeto foi desenvolvido em ASP.Net com MVC 4 em C#.
Conexão do banco de dados através do EntityFrameWork com banco de dados SQL Server!

1 - Executar o script SQL para criação da base de dados e tabela "ArquivosImportados" (SCRIPT_DATA_BASE.sql);
2 - Abrir projeto no Visual Studio;
3 - Alterar Connection String "WorkhubEntities" para apontar o local onde sua base de dados se localiza, basta colocar Host, Nome do banco de dados e usuário e senha para conexão;

Substituir essa linha do Web Config

<add name="WorkhubEntities" connectionString="metadata=res://*/DB.csdl|res://*/DB.ssdl|res://*/DB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=HOST;initial catalog=WorkHub;integrated security=False; User Id=USUARIO; Password=SUASENHA MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

4 - Executar projeto!
