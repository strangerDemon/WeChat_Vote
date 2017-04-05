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

Date: 2017-03-22 16:35:34
*/


-- ----------------------------
-- Table structure for WeChat_Vote
-- ----------------------------
DROP TABLE [dbo].[WeChat_Vote]
GO
CREATE TABLE [dbo].[WeChat_Vote] (
[ID] varchar(50) NOT NULL ,
[DateBegin] datetime NULL ,
[DateEnd] datetime NULL ,
[VoteNum] int NULL ,
[UserScope] varchar(MAX) NULL ,
[Title] varchar(255) NULL ,
[Description] varchar(MAX) NULL ,
[CreateUserId] varchar(50) NULL ,
[CreateDate] datetime NULL ,
[CreateUserName] varchar(50) NULL ,
[ModifyUserId] varchar(50) NULL ,
[ModifyUserName] varchar(50) NULL ,
[ModifyDate] datetime NULL ,
[DeleteMark] int NULL ,
[EnabledMark] int NULL ,
[UserScopeName] varchar(MAX) NULL ,
[Status] int NULL ,
[Count] int NULL ,
[VoteType] int NULL ,
[RealSendVoteNum] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信投票表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信投票表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'ID')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'ID'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'ID'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'DateBegin')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票起始时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'DateBegin'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票起始时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'DateBegin'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'DateEnd')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票终止时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'DateEnd'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票终止时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'DateEnd'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'VoteNum')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票数,创建时设计最多可以投票的数'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'VoteNum'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票数,创建时设计最多可以投票的数'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'VoteNum'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'UserScope')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户范围，1代表所有，用户id的并集，用,隔开'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'UserScope'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户范围，1代表所有，用户id的并集，用,隔开'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'UserScope'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'Title')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票标题'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Title'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票标题'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Title'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'Description')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'描述'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Description'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'描述'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Description'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'UserScopeName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户范围名字，只有自定义时使用'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'UserScopeName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户范围名字，只有自定义时使用'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'UserScopeName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'Status')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'状态码,0未发布，1收集中，2已结束'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Status'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'状态码,0未发布，1收集中，2已结束'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Status'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'Count')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票统计，统计有多少人投票了'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Count'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票统计，统计有多少人投票了'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'Count'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'VoteType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票类型:1:简单(只有单双选)，2统计(多个单选一个统计)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'VoteType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票类型:1:简单(只有单双选)，2统计(多个单选一个统计)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'VoteType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_Vote', 
'COLUMN', N'RealSendVoteNum')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'实际发放的投票'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'RealSendVoteNum'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'实际发放的投票'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_Vote'
, @level2type = 'COLUMN', @level2name = N'RealSendVoteNum'
GO
