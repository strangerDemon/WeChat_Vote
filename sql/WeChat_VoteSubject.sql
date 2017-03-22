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

Date: 2017-03-22 16:35:48
*/


-- ----------------------------
-- Table structure for WeChat_VoteSubject
-- ----------------------------
DROP TABLE [dbo].[WeChat_VoteSubject]
GO
CREATE TABLE [dbo].[WeChat_VoteSubject] (
[ID] varchar(50) NOT NULL ,
[Description] varchar(MAX) NULL ,
[SubjectType] int NULL ,
[Max] int NULL ,
[Min] int NULL ,
[ImgUrl] varchar(MAX) NULL ,
[CreateUserId] varchar(50) NULL ,
[CreateUserName] varchar(50) NULL ,
[CreateDate] datetime NULL ,
[ModifyUserId] varchar(50) NULL ,
[ModifyUserName] varchar(50) NULL ,
[ModifyDate] datetime NULL ,
[DeleteMark] int NULL ,
[EnabledMark] int NULL ,
[VoteId] varchar(50) NULL ,
[Title] varchar(MAX) NULL ,
[SubjectOrder] int NULL ,
[Count] int NULL ,
[Value] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信投票题目表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信投票题目表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'ID')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'ID'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'ID'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'Description')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'描述'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Description'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'描述'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Description'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'SubjectType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目类型，1单选，2 多选，3统计'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'SubjectType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目类型，1单选，2 多选，3统计'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'SubjectType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'Max')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'多选最多几个，默认1'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Max'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'多选最多几个，默认1'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Max'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'Min')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'多选最少几个，默认1'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Min'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'多选最少几个，默认1'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Min'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'ImgUrl')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目中图片的连接，http://......'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'ImgUrl'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目中图片的连接，http://......'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'ImgUrl'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'VoteId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'投票id，用题目去关联投票，题目不可复用'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'VoteId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'投票id，用题目去关联投票，题目不可复用'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'VoteId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'Title')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Title'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Title'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'SubjectOrder')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目序号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'SubjectOrder'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目序号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'SubjectOrder'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'Count')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'统计题目有多少人选择'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Count'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'统计题目有多少人选择'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Count'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubject', 
'COLUMN', N'Value')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'这道题得了多少分（用于统计投票中的统计题目）其他暂时未0'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Value'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'这道题得了多少分（用于统计投票中的统计题目）其他暂时未0'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubject'
, @level2type = 'COLUMN', @level2name = N'Value'
GO
