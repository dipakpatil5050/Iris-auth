USE [master]
GO
/****** Object:  Database [MVVMLoginDb]    Script Date: 19/12/2025 17:10:40 ******/
CREATE DATABASE [IRISLoginDb]
USE [IRISLoginDb]
CREATE TABLE [dbo].[AuditTrail](
	[AuditId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTime] [datetime2](7) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](255) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[HostName] [nvarchar](100) NULL,
	[OldValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NULL,
	[Reason] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BiometricSettings]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BiometricSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MinMatchScore] [float] NOT NULL,
	[EnableBonafide] [bit] NOT NULL,
	[LastUpdated] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentSettings]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentSettings](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[DateAndTime] [datetime] NOT NULL,
	[BatchName] [nvarchar](50) NOT NULL,
	[ParameterID] [int] NULL,
	[Value] [nvarchar](50) NULL,
	[ValueType] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPermissions]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermissions](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
	[LoginTimeout] [int] NULL,
	[LoginType] [int] NULL,
	[Permission1] [bit] NOT NULL,
	[Permission2] [bit] NOT NULL,
	[Permission3] [bit] NOT NULL,
	[Permission4] [bit] NOT NULL,
	[Permission5] [bit] NOT NULL,
	[Permission6] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[Is_Active] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IrisMatchLog]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IrisMatchLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[MatchScore] [float] NULL,
	[LeftEyeScore] [float] NULL,
	[RightEyeScore] [float] NULL,
	[MatchStatus] [nvarchar](20) NOT NULL,
	[IpAddress] [nvarchar](50) NULL,
	[MatchedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IrisTemplates]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IrisTemplates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LeftEyeTemplate] [varbinary](max) NULL,
	[RightEyeTemplate] [varbinary](max) NULL,
	[IIRFile] [varbinary](max) NULL,
	[EnrollmentID] [nvarchar](64) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParameterMinMax]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParameterMinMax](
	[BatchID] [int] NOT NULL,
	[Parameter] [nvarchar](100) NOT NULL,
	[MinValue] [decimal](6, 2) NULL,
	[MaxValue] [decimal](6, 2) NULL,
	[MinDateTime] [datetime] NULL,
	[MaxDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BatchID] ASC,
	[Parameter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parameters]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameters](
	[ParameterID] [int] IDENTITY(1,1) NOT NULL,
	[ParameterName] [nvarchar](255) NOT NULL,
	[Unit] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecuritySettings]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecuritySettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MinPasswordLength] [int] NULL,
	[MinSymbolChars] [int] NULL,
	[MinNumericChars] [int] NULL,
	[MinAlphaChars] [int] NULL,
	[PasswordExpiryDays] [int] NULL,
	[ExpiryNotificationDays] [int] NULL,
	[FailedLoginAttempts] [int] NULL,
	[AutoLogoutMinutes] [int] NULL,
	[LastUpdated] [datetime] NULL,
	[MaxPasswordLength] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[IsNew] [int] NOT NULL,
	[IsBlocked] [int] NOT NULL,
	[FailedAttempts] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[LastLogin] [datetime2](7) NULL,
	[PasswordExpiry] [date] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[Is_Active] [int] NOT NULL,
	[IsBiometricEnabled] [bit] NOT NULL,
	[BiometricLastUpdated] [datetime2](7) NULL,
	[BiometricQualityLeft] [int] NULL,
	[BiometricQualityRight] [int] NULL,
	[IrisLeftBase64] [varbinary](max) NULL,
	[IrisRightBase64] [varbinary](max) NULL,
	[IrisLeftQuality] [int] NULL,
	[IrisRightQuality] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginLog]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginTime] [datetime2](7) NOT NULL,
	[LogoutTime] [datetime2](7) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[LoginStatus] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPasswordLog]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPasswordLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OldPasswordHash] [nvarchar](255) NOT NULL,
	[ChangedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AuditTrail_Action]    Script Date: 19/12/2025 17:10:41 ******/
CREATE NONCLUSTERED INDEX [IX_AuditTrail_Action] ON [dbo].[AuditTrail]
(
	[Action] ASC
)
INCLUDE([EventTime],[Username],[Details]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AuditTrail_EventTime]    Script Date: 19/12/2025 17:10:41 ******/
CREATE NONCLUSTERED INDEX [IX_AuditTrail_EventTime] ON [dbo].[AuditTrail]
(
	[EventTime] ASC
)
INCLUDE([Action],[Username],[IpAddress],[HostName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AuditTrail_Username]    Script Date: 19/12/2025 17:10:41 ******/
CREATE NONCLUSTERED INDEX [IX_AuditTrail_Username] ON [dbo].[AuditTrail]
(
	[Username] ASC
)
INCLUDE([EventTime],[Action],[Details]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditTrail] ADD  DEFAULT (sysdatetime()) FOR [EventTime]
GO
ALTER TABLE [dbo].[BiometricSettings] ADD  DEFAULT ((0.7)) FOR [MinMatchScore]
GO
ALTER TABLE [dbo].[BiometricSettings] ADD  DEFAULT ((1)) FOR [EnableBonafide]
GO
ALTER TABLE [dbo].[BiometricSettings] ADD  DEFAULT (sysdatetime()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Permission1]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Permission2]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Permission3]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Permission4]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Permission5]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Permission6]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT (sysdatetime()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((0)) FOR [Is_Active]
GO
ALTER TABLE [dbo].[IrisMatchLog] ADD  DEFAULT (sysdatetime()) FOR [MatchedAt]
GO
ALTER TABLE [dbo].[IrisTemplates] ADD  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[IrisTemplates] ADD  DEFAULT (sysdatetime()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((8)) FOR [MinPasswordLength]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((1)) FOR [MinSymbolChars]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((1)) FOR [MinNumericChars]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((1)) FOR [MinAlphaChars]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((90)) FOR [PasswordExpiryDays]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((10)) FOR [ExpiryNotificationDays]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((3)) FOR [FailedLoginAttempts]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT ((15)) FOR [AutoLogoutMinutes]
GO
ALTER TABLE [dbo].[SecuritySettings] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT ((1)) FOR [IsNew]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT ((0)) FOR [IsBlocked]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT ((0)) FOR [FailedAttempts]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT (sysdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT (sysdatetime()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT ((0)) FOR [Is_Active]
GO
ALTER TABLE [dbo].[UserAccounts] ADD  DEFAULT ((0)) FOR [IsBiometricEnabled]
GO
ALTER TABLE [dbo].[UserLoginLog] ADD  DEFAULT (sysdatetime()) FOR [LoginTime]
GO
ALTER TABLE [dbo].[UserPasswordLog] ADD  DEFAULT (sysdatetime()) FOR [ChangedAt]
GO
ALTER TABLE [dbo].[EquipmentSettings]  WITH CHECK ADD  CONSTRAINT [FK__Equipment__Param__4F47C5E3] FOREIGN KEY([ParameterID])
REFERENCES [dbo].[Parameters] ([ParameterID])
GO
ALTER TABLE [dbo].[EquipmentSettings] CHECK CONSTRAINT [FK__Equipment__Param__4F47C5E3]
GO
ALTER TABLE [dbo].[EquipmentSettings]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentSettings_Parameters] FOREIGN KEY([ParameterID])
REFERENCES [dbo].[Parameters] ([ParameterID])
GO
ALTER TABLE [dbo].[EquipmentSettings] CHECK CONSTRAINT [FK_EquipmentSettings_Parameters]
GO
ALTER TABLE [dbo].[IrisMatchLog]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([UserId])
GO
ALTER TABLE [dbo].[IrisTemplates]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([UserId])
GO
ALTER TABLE [dbo].[UserAccounts]  WITH NOCHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[GroupPermissions] ([GroupId])
GO
ALTER TABLE [dbo].[UserLoginLog]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([UserId])
GO
ALTER TABLE [dbo].[UserPasswordLog]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([UserId])
GO
/****** Object:  StoredProcedure [dbo].[usp_BlockUser]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_BlockUser]
    @UserId     INT,
    @UpdatedBy  NVARCHAR(50) = N'SYSTEM',
    @Reason     NVARCHAR(500) = N'Manual block by admin'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @Username   NVARCHAR(50),
        @WasBlocked INT;

    -- 🔹 Get Username & previous block state
    SELECT 
        @Username   = Username,
        @WasBlocked = IsBlocked
    FROM dbo.UserAccounts
    WHERE UserId = @UserId;

    -- 🔹 User not found
    IF @Username IS NULL
    BEGIN
        RAISERROR('UserId not found.', 16, 1);
        RETURN;
    END

    -- 🔹 Block user
    UPDATE dbo.UserAccounts
    SET 
        IsBlocked = 1,
        UpdatedAt = SYSDATETIME()
    WHERE UserId = @UserId;

    -- 🔹 Audit trail (USERNAME used)
    INSERT INTO dbo.AuditTrail
    (
        EventTime,
        Username,
        Action,
        Details,
        OldValue,
        NewValue,
        Reason
    )
    VALUES
    (
        SYSDATETIME(),
        @UpdatedBy,
        N'BlockUser',
        N'User "' + @Username + N'" blocked',
        N'IsBlocked=' + CAST(@WasBlocked AS NVARCHAR(10)),
        N'IsBlocked=1',
        @Reason
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ChangePassword]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_ChangePassword]
    @Username         NVARCHAR(50),
    @OldPasswordHash  NVARCHAR(255),
    @NewPasswordHash  NVARCHAR(255),
    @Reason NVARCHAR(500) = N'User-initiated password change'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT, @CurrentHash NVARCHAR(255);

    SELECT @UserId = UserId,
           @CurrentHash = PasswordHash
    FROM dbo.UserAccounts
    WHERE Username = @Username;

    IF @UserId IS NULL
    BEGIN
        RAISERROR('User not found.', 16, 1);
        RETURN;
    END;

    IF @CurrentHash <> @OldPasswordHash
    BEGIN
        RAISERROR('Old password is incorrect.', 16, 1);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM dbo.UserPasswordLog WHERE UserId = @UserId AND OldPasswordHash = @NewPasswordHash)
    BEGIN
        RAISERROR('Cannot reuse an old password.', 16, 1);
        RETURN;
    END;

    INSERT INTO dbo.UserPasswordLog (UserId, OldPasswordHash)
    VALUES (@UserId, @CurrentHash);

    UPDATE dbo.UserAccounts
    SET PasswordHash = @NewPasswordHash,
        IsNew = 0,
        UpdatedAt = SYSDATETIME()
    WHERE UserId = @UserId;

    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason)
    VALUES (SYSDATETIME(), @Username, N'ChangePassword',
            N'User changed own password',
            @CurrentHash, @NewPasswordHash, @Reason);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateGroup]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_CreateGroup]
    @GroupName NVARCHAR(50),
    @LoginTimeout INT = NULL,
    @LoginType INT = NULL,
    @L1 BIT = 0,
    @L2 BIT = 0,
    @L3 BIT = 0,
    @L4 BIT = 0,
    @L5 BIT = 0,
    @L6 BIT = 0,
    @UpdatedBy NVARCHAR(50) = N'SYSTEM',
    @Reason NVARCHAR(500) = N'Create group'
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.GroupPermissions WHERE GroupName = @GroupName AND Is_Active = 0)
    BEGIN
        RAISERROR('Group already exists.',16,1);
        RETURN;
    END;

    INSERT INTO dbo.GroupPermissions
        (GroupName, LoginTimeout, LoginType, Permission1, Permission2, Permission3, Permission4, Permission5, Permission6, CreatedAt, UpdatedAt, Is_Active)
    VALUES
        (@GroupName, @LoginTimeout, @LoginType, @L1, @L2, @L3, @L4, @L5, @L6, SYSDATETIME(), SYSDATETIME(), 0);

    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason)
    VALUES (SYSDATETIME(), @UpdatedBy, N'CreateGroup',
            N'Group (' + @GroupName + N') created',
            NULL,
            N'GroupName=' + @GroupName, @Reason);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateUser]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CreateUser]
    @Username       NVARCHAR(50),
    @PasswordHash   NVARCHAR(255),
    @GroupId        INT,
    @UpdatedBy      NVARCHAR(50) = N'SYSTEM',
    @Reason         NVARCHAR(500) = N'Create user'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @Now DATETIME2(0) = SYSDATETIME(),
        @ExpiryDays INT,
        @GroupName NVARCHAR(100);

    -- 🔹 Prevent duplicate username
    IF EXISTS (SELECT 1 FROM dbo.UserAccounts WHERE Username = @Username)
    BEGIN
        RAISERROR('Username already exists.', 16, 1);
        RETURN;
    END

    -- 🔹 Get password expiry policy
    SELECT TOP 1 
        @ExpiryDays = PasswordExpiryDays
    FROM dbo.SecuritySettings;

    -- 🔹 Get GroupName for audit
    SELECT 
        @GroupName = GroupName
    FROM dbo.GroupPermissions
    WHERE GroupId = @GroupId;

    IF @GroupName IS NULL
    BEGIN
        RAISERROR('Group not found.', 16, 1);
        RETURN;
    END

    -- 🔹 Create user
    INSERT INTO dbo.UserAccounts
    (
        Username,
        PasswordHash,
        GroupId,
        CreatedAt,
        UpdatedAt,
        FailedAttempts,
        IsBlocked,
        LastLogin,
        PasswordExpiry
    )
    VALUES
    (
        @Username,
        @PasswordHash,
        @GroupId,
        @Now,
        @Now,
        0,
        0,
        NULL,
        CASE 
            WHEN @ExpiryDays IS NOT NULL AND @ExpiryDays > 0
                THEN DATEADD(DAY, @ExpiryDays, @Now)
            ELSE NULL
        END
    );

    -- 🔹 Audit trail (USERNAME + GROUP NAME)
    INSERT INTO dbo.AuditTrail
    (
        EventTime,
        Username,
        Action,
        Details,
        OldValue,
        NewValue,
        Reason
    )
    VALUES
    (
        @Now,
        @UpdatedBy,
        N'CreateUser',
        N'User "' + @Username + N'" created in group "' + @GroupName + N'"',
        NULL,
        N'Username=' + @Username + 
        N';Group=' + @GroupName,
        @Reason
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_EditGroup]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_EditGroup]
    @GroupId        INT,
    @GroupName      NVARCHAR(100),   -- new name
    @LoginTimeout   INT,
    @LoginType      INT,
    @L1 BIT,
    @L2 BIT,
    @L3 BIT,
    @L4 BIT,
    @L5 BIT,
    @L6 BIT,
    @UpdatedBy      NVARCHAR(50) = N'SYSTEM',
    @Reason         NVARCHAR(500) = N'Update group'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @OldGroupName     NVARCHAR(100),
        @OldLoginTimeout INT,
        @OldLoginType    INT,
        @OldP1 BIT, @OldP2 BIT, @OldP3 BIT,
        @OldP4 BIT, @OldP5 BIT, @OldP6 BIT;

    -- 🔹 Fetch existing values using GroupId
    SELECT
        @OldGroupName     = GroupName,
        @OldLoginTimeout = LoginTimeout,
        @OldLoginType    = LoginType,
        @OldP1 = Permission1,
        @OldP2 = Permission2,
        @OldP3 = Permission3,
        @OldP4 = Permission4,
        @OldP5 = Permission5,
        @OldP6 = Permission6
    FROM dbo.GroupPermissions
    WHERE GroupId = @GroupId;

    -- 🔹 Group not found
    IF @OldGroupName IS NULL
    BEGIN
        RAISERROR('Group not found.', 16, 1);
        RETURN;
    END

    -- 🔹 Update group
    UPDATE dbo.GroupPermissions
    SET
        GroupName    = @GroupName,
        LoginTimeout = @LoginTimeout,
        LoginType    = @LoginType,
        Permission1  = @L1,
        Permission2  = @L2,
        Permission3  = @L3,
        Permission4  = @L4,
        Permission5  = @L5,
        Permission6  = @L6,
        UpdatedAt    = SYSDATETIME()
    WHERE GroupId = @GroupId;

    -- 🔹 Audit trail (GROUP NAME used, not GroupId)
    INSERT INTO dbo.AuditTrail
    (
        EventTime,
        Username,
        Action,
        Details,
        OldValue,
        NewValue,
        Reason
    )
    VALUES
    (
        SYSDATETIME(),
        @UpdatedBy,
        N'EditGroup',
        N'Group "' + @OldGroupName + N'" updated',
        N'Name=' + @OldGroupName +
        N';LoginTimeout=' + COALESCE(CAST(@OldLoginTimeout AS NVARCHAR(20)), N'NULL') +
        N';LoginType=' + COALESCE(CAST(@OldLoginType AS NVARCHAR(20)), N'NULL') +
        N';P1=' + CAST(@OldP1 AS NVARCHAR(5)) +
        N';P2=' + CAST(@OldP2 AS NVARCHAR(5)) +
        N';P3=' + CAST(@OldP3 AS NVARCHAR(5)) +
        N';P4=' + CAST(@OldP4 AS NVARCHAR(5)) +
        N';P5=' + CAST(@OldP5 AS NVARCHAR(5)) +
        N';P6=' + CAST(@OldP6 AS NVARCHAR(5)),
        N'Name=' + @GroupName +
        N';LoginTimeout=' + CAST(@LoginTimeout AS NVARCHAR(20)) +
        N';LoginType=' + CAST(@LoginType AS NVARCHAR(20)) +
        N';P1=' + CAST(@L1 AS NVARCHAR(5)) +
        N';P2=' + CAST(@L2 AS NVARCHAR(5)) +
        N';P3=' + CAST(@L3 AS NVARCHAR(5)) +
        N';P4=' + CAST(@L4 AS NVARCHAR(5)) +
        N';P5=' + CAST(@L5 AS NVARCHAR(5)) +
        N';P6=' + CAST(@L6 AS NVARCHAR(5)),
        @Reason
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_EnrollIris]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_EnrollIris]
(
    @UserId INT,
    @EnrollmentID NVARCHAR(64),
    @LeftTemplate VARBINARY(MAX),
    @RightTemplate VARBINARY(MAX),
    @IIRFile VARBINARY(MAX),
    @QualityLeft INT = NULL,
    @QualityRight INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.UserAccounts WHERE UserId = @UserId)
    BEGIN
        RAISERROR('UserId not found.',16,1);
        RETURN;
    END;

    INSERT INTO dbo.IrisTemplates
        (UserId, EnrollmentID, LeftEyeTemplate, RightEyeTemplate, IIRFile)
    VALUES
        (@UserId, @EnrollmentID, @LeftTemplate, @RightTemplate, @IIRFile);

    UPDATE dbo.UserAccounts
    SET IsBiometricEnabled = 1,
        BiometricQualityLeft = @QualityLeft,
        BiometricQualityRight = @QualityRight,
        BiometricLastUpdated = SYSDATETIME()
    WHERE UserId = @UserId;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_ForceLogoutUser]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_ForceLogoutUser]
    @Username NVARCHAR(50),
    @UpdatedBy NVARCHAR(50) = N'SYSTEM',
    @Reason NVARCHAR(500) = N'Force logout by admin'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT, @LogId INT, @Now DATETIME2(0) = SYSDATETIME(), @OldStatus NVARCHAR(50);

    SELECT @UserId = UserId FROM dbo.UserAccounts WHERE Username = @Username;

    IF @UserId IS NULL
    BEGIN
        RAISERROR('User not found.', 16, 1);
        RETURN;
    END;

    SELECT TOP 1 @LogId = LogId, @OldStatus = LoginStatus
    FROM dbo.UserLoginLog
    WHERE UserId = @UserId AND LogoutTime IS NULL
    ORDER BY LoginTime DESC;

    IF @LogId IS NULL
    BEGIN
        RAISERROR('No active session found for this user.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.UserLoginLog
    SET LogoutTime = @Now,
        LoginStatus = N'ForceLogout'
    WHERE LogId = @LogId;

    UPDATE dbo.UserAccounts
    SET FailedAttempts = 0,
        IsBlocked = 0,
        UpdatedAt = @Now
    WHERE UserId = @UserId;

    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason)
    VALUES (@Now, @UpdatedBy, N'ForceLogout',
            N'User (' + @Username + N') forcefully logged out',
            N'LoginStatus=' + COALESCE(@OldStatus,N'NULL'),
            N'LoginStatus=ForceLogout',
            @Reason);
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActiveSessions]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_GetActiveSessions]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT l.LogId, u.Username, g.GroupName,
           l.LoginTime, l.IpAddress
    FROM dbo.UserLoginLog l
    JOIN dbo.UserAccounts u ON l.UserId = u.UserId
    LEFT JOIN dbo.GroupPermissions g ON u.GroupId = g.GroupId
    WHERE l.LogoutTime IS NULL
      AND l.LoginStatus LIKE 'Success%';
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSecuritySettings]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_GetSecuritySettings]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM SecuritySettings ORDER BY Id DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserByEnrollmentID]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_GetUserByEnrollmentID]
(
    @EnrollmentID NVARCHAR(64)
)
AS
BEGIN
    SELECT t.TemplateId, u.UserId, u.Username, t.EnrollmentID
    FROM dbo.IrisTemplates t
    INNER JOIN dbo.UserAccounts u ON t.UserId = u.UserId
    WHERE t.EnrollmentID = @EnrollmentID;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserPermissions]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_GetUserPermissions]
    @Username NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT u.Username, u.IsBlocked, u.FailedAttempts,
           g.GroupName, g.LoginTimeout, g.LoginType,
           g.Permission1, g.Permission2, g.Permission3,
           g.Permission4, g.Permission5, g.Permission6
    FROM dbo.UserAccounts u
    INNER JOIN dbo.GroupPermissions g ON u.GroupId = g.GroupId
    WHERE u.Username = @Username;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_IrisMatchRecord]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_IrisMatchRecord]
(
    @UserId INT = NULL,
    @MatchScore FLOAT,
    @LeftEyeScore FLOAT = NULL,
    @RightEyeScore FLOAT = NULL,
    @MatchStatus NVARCHAR(20),
    @IpAddress NVARCHAR(50) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.IrisMatchLog
        (UserId, MatchScore, LeftEyeScore, RightEyeScore, MatchStatus, IpAddress)
    VALUES
        (@UserId, @MatchScore, @LeftEyeScore, @RightEyeScore, @MatchStatus, @IpAddress);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_LogAudit]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_LogAudit]
    @Username   NVARCHAR(50),
    @Action     NVARCHAR(100),
    @Details    NVARCHAR(500),
    @OldValue   NVARCHAR(MAX) = NULL,
    @NewValue   NVARCHAR(MAX) = NULL,
    @Reason     NVARCHAR(500) = NULL,
    @IpAddress  NVARCHAR(50) = NULL,
    @HostName   NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Capture client IP if not provided
    IF @IpAddress IS NULL
        SET @IpAddress = CONVERT(NVARCHAR(50), CONNECTIONPROPERTY('client_net_address'));

    -- Capture client host name if not provided
    IF @HostName IS NULL
        SET @HostName = HOST_NAME();

    INSERT INTO dbo.AuditTrail (
        EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress, HostName
    )
    VALUES (
        SYSDATETIME(), @Username, @Action, @Details, 
        @OldValue, @NewValue, @Reason, @IpAddress, @HostName
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LoginUser]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_LoginUser]
    @Username        NVARCHAR(50),
    @PasswordHash    NVARCHAR(255),
    @IpAddress       NVARCHAR(50) = NULL,
    @UpdatedBy       NVARCHAR(50) = N'SYSTEM',
    @GroupId         INT OUTPUT,
    @LoginStatus     INT OUTPUT,        -- 0=Failed,1=Success,2=Expired,3=Blocked
    @DaysToExpire    INT OUTPUT,
    @ShowExpiryWarning BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT, 
            @DbHash NVARCHAR(255), 
            @IsBlocked BIT,
            @PwdExpiry DATE,
            @FailedAttempts INT,
            @Now DATETIME2(0) = SYSDATETIME();

    DECLARE @ExpiryDays INT, @WarnDays INT, @MaxAttempts INT;
    SELECT TOP 1
        @ExpiryDays   = PasswordExpiryDays,
        @WarnDays     = ExpiryNotificationDays,
        @MaxAttempts  = FailedLoginAttempts
    FROM dbo.SecuritySettings;

    SET @GroupId = NULL;
    SET @LoginStatus = 0;
    SET @DaysToExpire = NULL;
    SET @ShowExpiryWarning = 0;

    SELECT @UserId = UserId,
           @DbHash = PasswordHash,
           @IsBlocked = IsBlocked,
           @FailedAttempts = FailedAttempts,
           @GroupId = GroupId,
           @PwdExpiry = PasswordExpiry
    FROM dbo.UserAccounts
    WHERE Username = @Username;

    IF @UserId IS NULL
    BEGIN
        INSERT INTO dbo.UserLoginLog (UserId, LoginStatus, IpAddress) VALUES (NULL, 'Failed-UnknownUser', @IpAddress);
        INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress)
        VALUES (@Now, @Username, N'LoginFailed', N'Unknown user attempted login', NULL, NULL, N'Invalid username', @IpAddress);

        SET @LoginStatus = 0;
        RAISERROR('Invalid username or password.',16,1);
        RETURN;
    END;

    IF @IsBlocked = 1
    BEGIN
        INSERT INTO dbo.UserLoginLog (UserId, LoginStatus, IpAddress) VALUES (@UserId, 'Blocked', @IpAddress);
        INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress)
        VALUES (@Now, @Username, N'LoginBlocked', N'Login attempt while blocked', N'IsBlocked=1', N'IsBlocked=1', N'Blocked account', @IpAddress);

        SET @LoginStatus = 3;
        RAISERROR('User account is blocked.',16,1);
        RETURN;
    END;

    -- Wrong password
    IF @DbHash <> @PasswordHash
    BEGIN
        UPDATE dbo.UserAccounts
        SET FailedAttempts = FailedAttempts + 1,
            UpdatedAt = @Now
        WHERE UserId = @UserId;

        IF @FailedAttempts + 1 >= @MaxAttempts
        BEGIN
            UPDATE dbo.UserAccounts SET IsBlocked = 1 WHERE UserId = @UserId;

            INSERT INTO dbo.UserLoginLog (UserId, LoginStatus, IpAddress) VALUES (@UserId, 'Blocked', @IpAddress);
            INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress)
            VALUES (@Now, @Username, N'LoginBlocked', N'User blocked after failed attempts',
                    N'FailedAttempts=' + CAST(@FailedAttempts AS NVARCHAR(10)),
                    N'IsBlocked=1;FailedAttempts=' + CAST(@FailedAttempts + 1 AS NVARCHAR(10)),
                    N'Exceeded max attempts', @IpAddress);

            SET @LoginStatus = 3;
            RAISERROR('Account blocked due to multiple failed attempts.',16,1);
            RETURN;
        END;

        INSERT INTO dbo.UserLoginLog (UserId, LoginStatus, IpAddress) VALUES (@UserId, 'Failed-Password', @IpAddress);
        INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress)
        VALUES (@Now, @Username, N'LoginFailed', N'Incorrect password',
                N'FailedAttempts=' + CAST(@FailedAttempts AS NVARCHAR(10)),
                N'FailedAttempts=' + CAST(@FailedAttempts + 1 AS NVARCHAR(10)),
                N'Invalid credentials', @IpAddress);

        SET @LoginStatus = 0;
        RAISERROR('Invalid username or password.',16,1);
        RETURN;
    END;

    -- Password expiry check
    IF @PwdExpiry IS NOT NULL
    BEGIN
        SET @DaysToExpire = DATEDIFF(DAY, @Now, @PwdExpiry);

        IF @DaysToExpire < 0
        BEGIN
            UPDATE dbo.UserAccounts SET IsBlocked = 1 WHERE UserId = @UserId;

            INSERT INTO dbo.UserLoginLog (UserId, LoginStatus, IpAddress) VALUES (@UserId, 'Expired', @IpAddress);
            INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress)
            VALUES (@Now, @Username, N'LoginExpired', N'Password expired',
                    N'PasswordExpiry=' + CONVERT(NVARCHAR(30), @PwdExpiry, 120),
                    N'IsBlocked=1',
                    N'Password expired', @IpAddress);

            SET @LoginStatus = 2;
            RAISERROR('Password expired. Contact admin.',16,1);
            RETURN;
        END;

        IF @DaysToExpire >= 0 AND @DaysToExpire <= @WarnDays
            SET @ShowExpiryWarning = 1;
    END;

    -- Success login
    UPDATE dbo.UserAccounts
    SET FailedAttempts = 0,
        LastLogin = @Now,
        UpdatedAt = @Now
    WHERE UserId = @UserId;

    INSERT INTO dbo.UserLoginLog (UserId, LoginStatus, IpAddress) VALUES (@UserId, 'Success', @IpAddress);
    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason, IpAddress)
    VALUES (@Now, @Username, N'LoginSuccess', N'Successful login',
            N'FailedAttempts=' + CAST(@FailedAttempts AS NVARCHAR(10)),
            N'FailedAttempts=0;LastLogin=' + CONVERT(NVARCHAR(30), @Now, 120),
            N'Valid credentials', @IpAddress);

    SET @LoginStatus = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_ResetPasswordByAdmin]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_ResetPasswordByAdmin]
    @Username NVARCHAR(50),
    @TempPasswordHash NVARCHAR(255),
    @UpdatedBy NVARCHAR(50) = N'SYSTEM',
    @Reason NVARCHAR(500) = N'Admin reset password'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT, @OldHash NVARCHAR(255);

    SELECT @UserId = UserId, @OldHash = PasswordHash
    FROM dbo.UserAccounts WHERE Username = @Username;

    IF @UserId IS NULL
    BEGIN
        RAISERROR('User not found.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.UserAccounts
    SET PasswordHash = @TempPasswordHash,
        IsNew = 3,
        IsBlocked = 0,
        FailedAttempts = 0,
        UpdatedAt = SYSDATETIME()
    WHERE UserId = @UserId;

    -- Log old password hash
    INSERT INTO dbo.UserPasswordLog (UserId, OldPasswordHash, ChangedAt)
    VALUES (@UserId, @OldHash, SYSDATETIME());

    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason)
    VALUES (SYSDATETIME(), @UpdatedBy, N'PasswordReset',
            N'Password reset for user (' + @Username + N')',
            @OldHash, @TempPasswordHash, @Reason);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_SetPasswordNewLogin]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_SetPasswordNewLogin]
    @Username          NVARCHAR(50),
    @NewPasswordHash   NVARCHAR(255),
    @PasswordExpiryDays INT = NULL,
    @UpdatedBy NVARCHAR(50) = N'SYSTEM',
    @Reason NVARCHAR(500) = N'User set password on first login'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Now DATETIME2(0) = SYSDATETIME();
    DECLARE @PolicyDays INT;
    DECLARE @OldHash NVARCHAR(255);

    SELECT @OldHash = PasswordHash FROM dbo.UserAccounts WHERE Username = @Username;

    IF @PasswordExpiryDays IS NULL
        SELECT TOP 1 @PolicyDays = PasswordExpiryDays FROM dbo.SecuritySettings;
    ELSE
        SET @PolicyDays = @PasswordExpiryDays;

    UPDATE dbo.UserAccounts
    SET PasswordHash   = @NewPasswordHash,
        FailedAttempts = 0,
        IsBlocked      = 0,
        IsNew          = 2,
        UpdatedAt      = @Now,
        PasswordExpiry = CASE 
                            WHEN @PolicyDays IS NOT NULL AND @PolicyDays > 0
                                THEN DATEADD(DAY, @PolicyDays, @Now)
                            ELSE NULL END
    WHERE Username = @Username;

    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason)
    VALUES (@Now, @UpdatedBy, N'SetPasswordNewLogin',
            N'User (' + @Username + N') set new password on first login',
            @OldHash, @NewPasswordHash, @Reason);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UnblockUser]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UnblockUser]
    @UserId     INT,
    @UpdatedBy  NVARCHAR(50) = N'SYSTEM',
    @Reason     NVARCHAR(500) = N'Manual unblock by admin'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @Username   NVARCHAR(50),
        @WasBlocked INT;

    -- 🔹 Get Username & previous block state
    SELECT 
        @Username   = Username,
        @WasBlocked = IsBlocked
    FROM dbo.UserAccounts
    WHERE UserId = @UserId;

    -- 🔹 User not found
    IF @Username IS NULL
    BEGIN
        RAISERROR('UserId not found.', 16, 1);
        RETURN;
    END

    -- 🔹 Unblock user
    UPDATE dbo.UserAccounts
    SET 
        IsBlocked = 0,
        FailedAttempts = 0,
        UpdatedAt = SYSDATETIME()
    WHERE UserId = @UserId;

    -- 🔹 Audit trail (USERNAME used)
    INSERT INTO dbo.AuditTrail
    (
        EventTime,
        Username,
        Action,
        Details,
        OldValue,
        NewValue,
        Reason
    )
    VALUES
    (
        SYSDATETIME(),
        @UpdatedBy,
        N'UnblockUser',
        N'User "' + @Username + N'" unblocked',
        N'IsBlocked=' + CAST(@WasBlocked AS NVARCHAR(10)),
        N'IsBlocked=0;FailedAttempts=0',
        @Reason
    );
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateSecuritySettings]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_UpdateSecuritySettings]
    @MinPasswordLength INT,
	@MaxPasswordLength INT,
    @MinSymbolChars INT,
    @MinNumericChars INT,
    @MinAlphaChars INT,
    @PasswordExpiryDays INT,
    @ExpiryNotificationDays INT,
    @FailedLoginAttempts INT,
    @AutoLogoutMinutes INT,
    @UpdatedBy NVARCHAR(50) = N'SYSTEM',
    @Reason NVARCHAR(500) = N'Update security settings'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Action NVARCHAR(50);
    DECLARE @Old NVARCHAR(MAX) = NULL, @New NVARCHAR(MAX);

    -- Build NEW snapshot
    SET @New = 
        N'MinPasswordLength=' + CAST(@MinPasswordLength AS NVARCHAR(10)) + N', ' +
		N'MaxPasswordLength=' + CAST(@MaxPasswordLength AS NVARCHAR(10)) + N', ' +
        N'MinSymbolChars=' + CAST(@MinSymbolChars AS NVARCHAR(10)) + N', ' +
        N'MinNumericChars=' + CAST(@MinNumericChars AS NVARCHAR(10)) + N', ' +
        N'MinAlphaChars=' + CAST(@MinAlphaChars AS NVARCHAR(10)) + N', ' +
        N'PasswordExpiryDays=' + CAST(@PasswordExpiryDays AS NVARCHAR(10)) + N', ' +
        N'ExpiryNotificationDays=' + CAST(@ExpiryNotificationDays AS NVARCHAR(10)) + N', ' +
        N'FailedLoginAttempts=' + CAST(@FailedLoginAttempts AS NVARCHAR(10)) + N', ' +
        N'AutoLogoutMinutes=' + CAST(@AutoLogoutMinutes AS NVARCHAR(10));

    -- Load OLD snapshot if exists
    IF EXISTS (SELECT 1 FROM SecuritySettings)
    BEGIN
        SELECT TOP 1
            @Old = 
                N'MinPasswordLength=' + CAST(MinPasswordLength AS NVARCHAR(10)) + N', ' +
				N'MaxPasswordLength=' + CAST(MaxPasswordLength AS NVARCHAR(10)) + N', ' +
                N'MinSymbolChars=' + CAST(MinSymbolChars AS NVARCHAR(10)) + N', ' +
                N'MinNumericChars=' + CAST(MinNumericChars AS NVARCHAR(10)) + N', ' +
                N'MinAlphaChars=' + CAST(MinAlphaChars AS NVARCHAR(10)) + N', ' +
                N'PasswordExpiryDays=' + CAST(PasswordExpiryDays AS NVARCHAR(10)) + N', ' +
                N'ExpiryNotificationDays=' + CAST(ExpiryNotificationDays AS NVARCHAR(10)) + N', ' +
                N'FailedLoginAttempts=' + CAST(FailedLoginAttempts AS NVARCHAR(10)) + N', ' +
                N'AutoLogoutMinutes=' + CAST(AutoLogoutMinutes AS NVARCHAR(10))
        FROM SecuritySettings
        ORDER BY Id DESC;

        UPDATE SecuritySettings
        SET MinPasswordLength = @MinPasswordLength,
			MaxPasswordLength = @MaxPasswordLength,
            MinSymbolChars = @MinSymbolChars,
            MinNumericChars = @MinNumericChars,
            MinAlphaChars = @MinAlphaChars,
            PasswordExpiryDays = @PasswordExpiryDays,
            ExpiryNotificationDays = @ExpiryNotificationDays,
            FailedLoginAttempts = @FailedLoginAttempts,
            AutoLogoutMinutes = @AutoLogoutMinutes,
            LastUpdated = GETDATE();

        SET @Action = N'UpdateSecuritySettings';
    END
    ELSE
    BEGIN
        INSERT INTO SecuritySettings (
            MinPasswordLength,
			MaxPasswordLength,
            MinSymbolChars,
            MinNumericChars,
            MinAlphaChars,
            PasswordExpiryDays,
            ExpiryNotificationDays,
            FailedLoginAttempts,
            AutoLogoutMinutes,
            LastUpdated
        )
        VALUES (
            @MinPasswordLength,
			@MaxPasswordLength,
            @MinSymbolChars,
            @MinNumericChars,
            @MinAlphaChars,
            @PasswordExpiryDays,
            @ExpiryNotificationDays,
            @FailedLoginAttempts,
            @AutoLogoutMinutes,
            GETDATE()
        );

        SET @Action = N'InsertSecuritySettings';
    END

    INSERT INTO dbo.AuditTrail (EventTime, Username, Action, Details, OldValue, NewValue, Reason)
    VALUES (SYSDATETIME(), @UpdatedBy, @Action, N'Security settings changed', @Old, @New, @Reason);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateUserGroup]    Script Date: 19/12/2025 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateUserGroup]
    @UserId     INT,
    @GroupId    INT,
    @UpdatedBy  NVARCHAR(50) = N'SYSTEM',
    @Reason     NVARCHAR(500) = N'Change user group'
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE 
            @Username       NVARCHAR(50),
            @OldGroupId     INT,
            @OldGroupName   NVARCHAR(100),
            @NewGroupName   NVARCHAR(100);

        -- 🔹 Validate user
        SELECT 
            @Username   = Username,
            @OldGroupId = GroupId
        FROM dbo.UserAccounts
        WHERE UserId = @UserId;

        IF @Username IS NULL
            RAISERROR('User not found.', 16, 1);

        -- 🔹 Validate new group (active only)
        SELECT 
            @NewGroupName = GroupName
        FROM dbo.GroupPermissions
        WHERE GroupId = @GroupId
          AND Is_Active = 0;   -- 0 = active (as per your table)

        IF @NewGroupName IS NULL
            RAISERROR('Group not found or inactive.', 16, 1);

        -- 🔹 Get old group name
        SELECT 
            @OldGroupName = GroupName
        FROM dbo.GroupPermissions
        WHERE GroupId = @OldGroupId;

        -- 🔹 No change protection (optional but clean)
        IF @OldGroupId = @GroupId
            RAISERROR('User already belongs to this group.', 16, 1);

        -- 🔹 Update user group
        UPDATE dbo.UserAccounts
        SET 
            GroupId   = @GroupId,
            UpdatedAt = SYSDATETIME()
        WHERE UserId = @UserId;

        -- 🔹 Audit trail
        INSERT INTO dbo.AuditTrail
        (
            EventTime,
            Username,
            Action,
            Details,
            OldValue,
            NewValue,
            Reason
        )
        VALUES
        (
            SYSDATETIME(),
            @UpdatedBy,
            N'UserGroupChange',
            N'User "' + @Username + N'" group changed to "' + @NewGroupName + N'"',
            N'Group=' + COALESCE(@OldGroupName, N'') +
            N';GroupId=' + CAST(@OldGroupId AS NVARCHAR(20)),
            N'Group=' + @NewGroupName +
            N';GroupId=' + CAST(@GroupId AS NVARCHAR(20)),
            @Reason
        );

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE 
            @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE(),
            @ErrSeverity INT = ERROR_SEVERITY();

        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END
GO
INSERT INTO dbo.[User] (Id, Username, Password, Name, LastName, Email, Role)
VALUES
('443e85be-b810-41fb-8246-2960cdf2e119','velox','Velox%123','Velox','Automation','iot.support@veloxautomation.com','SuperAdmin'),
('4880f2f7-eb5d-4c66-a417-ce22600d8691','admin','admin','Velox','Automation','veloxautomation@gmail.com','Admin');
GO
USE [master]
GO
ALTER DATABASE [IRISLoginDb] SET  READ_WRITE 
GO
