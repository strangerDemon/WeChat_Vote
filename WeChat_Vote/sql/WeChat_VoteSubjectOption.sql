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

Date: 2017-03-22 16:35:55
*/


-- ----------------------------
-- Table structure for WeChat_VoteSubjectOption
-- ----------------------------
DROP TABLE [dbo].[WeChat_VoteSubjectOption]
GO
CREATE TABLE [dbo].[WeChat_VoteSubjectOption] (
[ID] varchar(50) NOT NULL ,
[SubjectId] varchar(50) NULL ,
[Content] varchar(MAX) NULL ,
[ImgUrl] varchar(MAX) NULL ,
[CreateUserId] varchar(50) NULL ,
[CreateUserName] varchar(50) NULL ,
[CreateDate] datetime NULL ,
[ModifyUserId] varchar(50) NULL ,
[ModifyUserName] varchar(50) NULL ,
[ModifyDate] datetime NULL ,
[DeleteMark] int NULL ,
[EnabledMark] int NULL ,
[OptionOrder] int NULL ,
[Count] int NULL ,
[Value] int NULL ,
[MaxValue] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目选项表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目选项表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'ID')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'选项id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'ID'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'选项id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'ID'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'SubjectId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'题目id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'SubjectId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'题目id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'SubjectId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'Content')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'Content'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'Content'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'ImgUrl')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'图片url http://....'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'ImgUrl'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'图片url http://....'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'ImgUrl'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'OptionOrder')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'选项序号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'OptionOrder'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'选项序号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'OptionOrder'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'Count')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'统计选项有多少人选择'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'Count'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'统计选项有多少人选择'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'Count'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'Value')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权值(投票类型为简单的都为0,统计类型的 单双选为正负数，统计的为最小值）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'Value'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权值(投票类型为简单的都为0,统计类型的 单双选为正负数，统计的为最小值）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'Value'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_VoteSubjectOption', 
'COLUMN', N'MaxValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'统计题目的最大范围（eg:0<=x<2）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'MaxValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'统计题目的最大范围（eg:0<=x<2）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_VoteSubjectOption'
, @level2type = 'COLUMN', @level2name = N'MaxValue'
GO
