USE [DATABASE]
GO
DROP SYNONYM IF EXISTS [SYNONYM-NAM-1]
DROP SYNONYM IF EXISTS [SYNONYM-NAME-N]
GO

CREATE SYNONYM [SYNONYM-NAM-1] FOR [DATABASE].DBO.[TABLE-NAME]
CREATE SYNONYM [SYNONYM-NAM-N] FOR [DATABASE].DBO.[TABLE-NAME]

GO