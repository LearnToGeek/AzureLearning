USE [MASTER]
GO
EXEC MASTER.DBO.sp_addlinkedserver @server = N'[Another-Instance-IP]', @srvproduct=N'SQL Server'
EXEC MASTER.DBO.sp_addlinkedsrvlogin @rmtsrvname = N'[Another-Instance-IP]',
                                     @rmtuser = N'[Another-Instance-Username]', @rmtpassword = N'[Another-Instance-Password]',
                                     @locallogin = NULL, @useself = N'False'

GO

SELECT *
FROM ['Another-Instance-IP'].[Database-Name].[Table]