CREATE CREDENTIAL [STORAGE-ENDPOINT]
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE'
    SECRET = '<SAS TOKEN>'
GO

RESTORE DATABSE <DATABASE-NAME>
    FROM URL = N'[STORAGE-ENDPOINT]/FILENAME.BAK';