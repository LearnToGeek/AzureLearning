DROP EXTERNAL DATA SOURCE STANDARD StandardBlobStorage
GO

CREATE EXTERNAL DATA SOURCE StandardBlobStorage
WITH ( TYPE = BLOB_STORAGE, LOCATION = '[AZURE-STORAGE-ACCOUNT-ENDPOINT]' )
GO

BULK INSERT [DBO].[TABLE-NAME]
FROM '[FILE-NAME].csv'
WITH ( DATA_SOURCE = 'StandardBlobStorage',
       FIRSTROW=2,
       FIELDTERMINATOR = ',')
GO

-- Can use this script into sql server agent
-- can also rebuild index after updating database
ALTER TABLE [DBO].[TABLE-NAME] REBUILD