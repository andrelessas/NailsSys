EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'NailsBD'
GO
use [master];
GO
USE [master]
GO
ALTER DATABASE [NailsBD] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [NailsBD]    Script Date: 25/08/2022 19:45:48 ******/
DROP DATABASE [NailsBD]
GO