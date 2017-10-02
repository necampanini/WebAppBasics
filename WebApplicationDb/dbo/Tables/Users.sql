CREATE TABLE [dbo].[Users]
(
	[UserId]	INT NOT NULL IDENTITY(1,1),
	[Email]		NVARCHAR(50) NOT NULL,
	[PasswordHash] NVARCHAR(50) NOT NULL,
	[PasswordSalt] NVARCHAR(50) NOT NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[PasswordResetToken] NVARCHAR(50) NULL, -- null for now
)
