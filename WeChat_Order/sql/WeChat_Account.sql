/*
Navicat SQL Server Data Transfer

Source Server         : oa
Source Server Version : 105000
Source Host           : 192.168.1.110:1433
Source Database       : Zonetop_Base_2.6
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2017-04-05 17:23:11
*/


-- ----------------------------
-- Table structure for WeChat_Account
-- ----------------------------
DROP TABLE [dbo].[WeChat_Account]
GO
CREATE TABLE [dbo].[WeChat_Account] (
[UserId] varchar(50) NOT NULL ,
[WeChatId] varchar(50) NULL ,
[Money] decimal(10,2) NULL ,
[CreateDate] datetime NULL ,
[CreateUserId] varchar(50) NULL ,
[CreateUserName] varchar(50) NULL ,
[UserName] varchar(50) NULL ,
[ModifyDate] datetime NULL ,
[ModifyUserId] varchar(50) NULL ,
[ModifyUserName] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信关联oa的金额账号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信关联oa的金额账号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'UserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户再oa中的账号id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'UserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户再oa中的账号id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'UserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'WeChatId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信id，能够唯一确定用户与微信好的关联'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'WeChatId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信id，能够唯一确定用户与微信好的关联'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'WeChatId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'Money')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'账户金额'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'Money'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'账户金额'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'Money'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'CreateDate')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建账号时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'CreateDate'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建账号时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'CreateDate'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'CreateUserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建者id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'CreateUserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建者id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'CreateUserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'CreateUserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建者姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'CreateUserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建者姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'CreateUserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'UserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'这个账号是谁，方便查看'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'UserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'这个账号是谁，方便查看'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'UserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'ModifyDate')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'修改日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'ModifyDate'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'修改日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'ModifyDate'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'ModifyUserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'修改者id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'ModifyUserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'修改者id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'ModifyUserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Account', 
'COLUMN', N'ModifyUserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'修改者姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'ModifyUserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'修改者姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Account'
, @level2type = 'COLUMN', @level2name = N'ModifyUserName'
GO
