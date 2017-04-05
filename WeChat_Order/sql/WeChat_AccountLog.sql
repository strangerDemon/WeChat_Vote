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

Date: 2017-04-05 17:23:18
*/


-- ----------------------------
-- Table structure for WeChat_AccountLog
-- ----------------------------
DROP TABLE [dbo].[WeChat_AccountLog]
GO
CREATE TABLE [dbo].[WeChat_AccountLog] (
[ID] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[WeChatId] varchar(50) NULL ,
[MoneyChange] decimal(10,2) NULL ,
[CreateDate] datetime NULL ,
[UserName] varchar(50) NULL ,
[ChangeType] int NULL ,
[Description] varchar(MAX) NULL ,
[CreateUserId] varchar(50) NULL ,
[CreateUserName] varchar(50) NULL ,
[IsUnsubscribe] int NULL ,
[ModifyDate] datetime NULL ,
[ModifyUserId] varchar(50) NULL ,
[ModifyUserName] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信账号金额变动记录表 账户日志'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信账号金额变动记录表 账户日志'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'ID')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'无意义主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ID'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'无意义主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ID'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'WeChatId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'微信id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'WeChatId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'微信id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'WeChatId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'MoneyChange')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'变动的金额，±'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'MoneyChange'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'变动的金额，±'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'MoneyChange'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'CreateDate')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'金额变动的时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'CreateDate'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'金额变动的时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'CreateDate'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'UserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'UserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'UserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'ChangeType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'金额变动的类型 1充值（oa） 2消费（wechat） 3扣款（oa）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ChangeType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'金额变动的类型 1充值（oa） 2消费（wechat） 3扣款（oa）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ChangeType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'Description')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'详细'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'Description'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'详细'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'Description'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'CreateUserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'金额变动的人，充值的话就不是账号本人'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'CreateUserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'金额变动的人，充值的话就不是账号本人'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'CreateUserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'CreateUserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名，充值的话就不是账号本人'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'CreateUserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名，充值的话就不是账号本人'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'CreateUserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'IsUnsubscribe')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'是否退订 0初始化不是 1是，加这个为了取消订餐'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'IsUnsubscribe'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'是否退订 0初始化不是 1是，加这个为了取消订餐'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'IsUnsubscribe'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'ModifyDate')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'修改日期：退餐退款日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ModifyDate'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'修改日期：退餐退款日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ModifyDate'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'ModifyUserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'修改用户：退款人'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ModifyUserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'修改用户：退款人'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ModifyUserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'WeChat_AccountLog', 
'COLUMN', N'ModifyUserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'修改人姓名：退款人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ModifyUserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'修改人姓名：退款人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'WeChat_AccountLog'
, @level2type = 'COLUMN', @level2name = N'ModifyUserName'
GO
