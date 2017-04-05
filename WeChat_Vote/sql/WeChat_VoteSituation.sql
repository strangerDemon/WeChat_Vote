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

Date: 2017-03-22 16:35:42
*/


-- ----------------------------
-- Table structure for WeChat_VoteSituation
-- ----------------------------
DROP TABLE [dbo].[WeChat_VoteSituation]
GO
CREATE TABLE [dbo].[WeChat_VoteSituation] (
[ID] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[WeChatId] varchar(50) NULL ,
[VoteId] varchar(50) NULL ,
[SubjectId] varchar(50) NULL ,
[OptionId] varchar(50) NULL ,
[CreateDate] datetime NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票情况'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票情况'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'ID')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'ID'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'ID'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'UserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'UserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'UserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'WeChatId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信id，和用户id二选一，一般设计为临时用户'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'WeChatId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信id，和用户id二选一，一般设计为临时用户'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'WeChatId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'VoteId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'VoteId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'VoteId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'SubjectId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目id，统计由题目表实现，这里不做保存'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'SubjectId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目id，统计由题目表实现，这里不做保存'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'SubjectId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'OptionId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'选项id，多选分为多个，统计由选项表记录，这里不作保存'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'OptionId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'选项id，多选分为多个，统计由选项表记录，这里不作保存'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'OptionId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSituation', 
'COLUMN', N'CreateDate')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'CreateDate'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSituation'
, @level2type = 'COLUMN', @level2name = N'CreateDate'
GO
