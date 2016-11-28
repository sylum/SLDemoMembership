USE [SLDemoSecurity]
GO

DECLARE	@return_value int,
		@ApplicationId uniqueidentifier

EXEC	@return_value = [dbo].[aspnet_Applications_CreateApplication]
		@ApplicationName = N'/SLDemo',
		@ApplicationId = @ApplicationId OUTPUT



GO

---

USE [SLDemoSecurity]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[aspnet_Roles_CreateRole]
		@ApplicationName = N'/SLDemo',
		@RoleName = N'ADMINISTRATOR'

GO

USE [SLDemoSecurity]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[aspnet_Roles_CreateRole]
		@ApplicationName = N'/SLDemo',
		@RoleName = N'OPERATOR'

GO

USE [SLDemoSecurity]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[aspnet_Roles_CreateRole]
		@ApplicationName = N'/SLDemo',
		@RoleName = N'SUPERVISOR'

GO
