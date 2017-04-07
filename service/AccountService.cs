using ZoneTop.Application.Entity.WeChatManage;
using ZoneTop.Application.IService.WeChatManage;
using ZoneTop.Data.Repository;
using ZoneTop.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System;
using ZoneTop.Application.Service.BaseManage;
using ZoneTop.Application.Entity.BaseManage;
using ZoneTop.Util.Extension;
using ZoneTop.Util;
using ZoneTop.Application.Service.WeChatManage;

namespace ZoneTop.Application.Service.WeChatManage
{
    /// <summary>
    /// 版 本 2.6
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-23 09:03
    /// 描 述：微信关联oa的金额账号
    /// </summary>
    public class AccountService : RepositoryFactory, AccountIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<WeChatAccountEntity> GetAccountList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<WeChatAccountEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "UserName":            //用户名
                        expression = expression.And(t => t.UserName.Contains(keyord));
                        break;
                    case "WeChatId":            //微信账号
                        expression = expression.And(t => t.WeChatId.Contains(keyord));
                        break;
                    case "Money":            //金额范围
                        if (!queryParam["minMoney"].IsEmpty())
                        {
                            int minMoney = queryParam["minMoney"].ToInt();
                            expression = expression.And(t => t.Money >= minMoney);//数字要用==
                        }
                        if (!queryParam["maxMoney"].IsEmpty())
                        {
                            int maxMoney = queryParam["maxMoney"].ToInt();
                            expression = expression.And(t => t.Money <= maxMoney);//日期
                        }
                        break;
                    case "Date":         //变动日期
                        if (!queryParam["minDate"].IsEmpty())
                        {
                            DateTime startTime = queryParam["minDate"].ToDate();
                            expression = expression.And(t => t.ModifyDate >= startTime);//数字要用==
                        }
                        if (!queryParam["maxDate"].IsEmpty())
                        {
                            DateTime endTime = queryParam["maxDate"].ToDate().AddDays(1);
                            expression = expression.And(t => t.ModifyDate < endTime);//日期
                        }
                        break;
                    default:
                        break;
                }
            }
            if (pagination != null)
            {
                return this.BaseRepository().FindList<WeChatAccountEntity>(expression, pagination);
            }
            else
            {
                return this.BaseRepository().FindList<WeChatAccountEntity>(expression);
            }
        }
        /// <summary>
        /// 获取账户变动日志列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="wechatId">微信id</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<WeChatAccountLogEntity> GetAccountLogList(Pagination pagination, string userId, string wechatId, string queryJson)
        {
            var expression = LinqExtensions.True<WeChatAccountLogEntity>();
            if (userId != null && userId != "all" && userId != "orderList")
            {
                expression = expression.And(t => t.UserId.Equals(userId));
            }
            if (wechatId != null)
            {
                expression = expression.And(t => t.WeChatId.Equals(wechatId));
            }
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "UserName":            //用户名
                        expression = expression.And(t => t.UserName.Contains(keyord));
                        break;
                    case "WeChatId":            //微信账号
                        expression = expression.And(t => t.WeChatId.Contains(keyord));
                        break;
                    case "ChangeType":            //变动类型
                        int changeType = keyord.ToInt();
                        if (changeType >= 0)
                        {
                            expression = expression.And(t => t.ChangeType == changeType);
                        }
                        break;
                    case "Date":         //变动日期
                        if (!queryParam["minDate"].IsEmpty())
                        {
                            DateTime startTime = queryParam["minDate"].ToDate();
                            expression = expression.And(t => t.CreateDate >= startTime);//数字要用==
                        }
                        if (!queryParam["maxDate"].IsEmpty())
                        {
                            DateTime endTime = queryParam["maxDate"].ToDate().AddDays(1);
                            expression = expression.And(t => t.CreateDate < endTime);//日期
                        }
                        break;
                    default:
                        break;
                }
            }
            if (pagination != null)
            {
                return this.BaseRepository().FindList<WeChatAccountLogEntity>(expression, pagination);
            }
            else
            {
                return this.BaseRepository().FindList<WeChatAccountLogEntity>(expression);
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<WeChatAccountEntity>(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="wechatId">微信号</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntityByWechatId(string wechatId)
        {
            return this.BaseRepository().FindEntity<WeChatAccountEntity>(t => t.WeChatId.Equals(wechatId));
        }
        /// <summary>
        /// 获取日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeChatAccountLogEntity GetAccountLogEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<WeChatAccountLogEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete<WeChatAccountEntity>(keyValue);
        }
        /// <summary>
        /// 修改账户 充值 扣款
        /// </summary>
        /// <param name="logEntity">日志实体对象</param>
        /// <returns></returns>
        public void SaveForm(WeChatAccountLogEntity logEntity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                logEntity.Create();
                logEntity.IsUnsubscribe = 0;
                db.Insert(logEntity);

                WeChatAccountEntity entity = this.BaseRepository().FindEntity<WeChatAccountEntity>(logEntity.UserId);
                if (logEntity.ChangeType == 1)//充值
                {
                    entity.Money += logEntity.MoneyChange;
                }
                else //changeType==3 扣款 
                {
                    entity.Money -= logEntity.MoneyChange;
                }
                entity.Modify("");
                db.Update(entity);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
            }

        }

        /// <summary>
        /// 批量导入账户
        /// </summary>
        /// <param name="accountList">对象列表</param>
        /// <returns></returns>
        public void BatchImport(List<WeChatAccountEntity> accountList)
        {
            try
            {
                WeChatUserService wxUserService=new WeChatUserService();
                WeChatUserRelationEntity relationEntity = new WeChatUserRelationEntity();
                if (accountList != null)//列表不为空
                {
                    foreach (WeChatAccountEntity account in accountList)//遍历 微信号 / 用户名
                    {
                        relationEntity = wxUserService.GetEntityByUserId(account.UserId);
                        if (relationEntity == null) { continue; }
                        if (this.BaseRepository().FindEntity<WeChatAccountEntity>(account.UserId) == null)//不是已经存在的
                        {
                            account.Create(account.UserId);
                            account.Modify("");
                            account.Money = 0;//账户初始化
                            account.WeChatId = relationEntity.UserRelationId;
                            this.BaseRepository().Insert<WeChatAccountEntity>(account);//插入
                        }
                        else //账户是已经存在的 但是可能存在信息不完整的情况 一开始没有微信号=》后来才有
                        {
                            account.Modify("");
                            account.WeChatId = relationEntity.UserRelationId;
                            this.BaseRepository().Update<WeChatAccountEntity>(account);//更新
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region WeChat 微信端使用
        /// <summary>
        /// 微信点餐 都是创建一条记录；
        /// 微信取消订餐为修改一条 记录
        /// </summary>
        /// <param name="logEntity">日志实体</param>
        /// <param name="type">1 点餐  2 取消订餐</param>
        /// <returns></returns>
        public void WeChatOrder(WeChatAccountLogEntity logEntity, string type)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WeChatAccountEntity entity = this.BaseRepository().FindEntity<WeChatAccountEntity>(logEntity.UserId);
                if (logEntity.IsUnsubscribe == 1)//退款 修改 log对象 
                {
                    logEntity.ModifyDate = DateTime.Now;
                    logEntity.ModifyUserId = logEntity.UserId;
                    logEntity.ModifyUserName = logEntity.UserName;
                    db.Update(logEntity);
                    entity.Money += logEntity.MoneyChange;//账户加钱
                }
                else//微信点餐 创建对象
                {
                    db.Insert(logEntity);
                    entity.Money -= logEntity.MoneyChange;
                }
                //entity.Modify("");//不能用Modify，因为此时没有账号登录
                entity.ModifyDate = DateTime.Now;
                entity.ModifyUserId = logEntity.UserId;
                entity.ModifyUserName = logEntity.UserName;
                db.Update(entity);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 点餐记录 充值记录 
        /// </summary>
        /// <param name="weChatId"></param>
        /// <param name="type">1充值 2消费</param>
        /// <param name="page"> 页码 </param>
        /// <param name="pageNum"> 每页数量</param>
        /// <returns></returns>
        public IEnumerable<WeChatAccountLogEntity> WeChatOrderLog(string weChatId, string type, int page, int pageNum)
        {
            string sqlStr = "";
            if (page == 0)
            {
                //AND DateDiff(dd,CreateDate,getdate())=0 获取今日的数据
                string weChatIdStr = weChatId == "" ? "" : " and WeChatId='" + weChatId + "'";
                sqlStr = "SELECT * FROM WeChat_AccountLog WHERE  ChangeType=" + type + "and IsUnsubscribe=0 AND DateDiff(dd,CreateDate,getdate())=0 " + weChatIdStr + " order BY CreateDate DESC ";
            }
            else
            {
                sqlStr = " select TOP " + pageNum + " * from WeChat_AccountLog " +
                               " where  id NOT IN ( " +
                                    " SELECT TOP " + (page * pageNum - pageNum) + " id FROM WeChat_AccountLog " +
                                        " WHERE WeChatId='" + weChatId + "' and ChangeType=" + type + " order BY CreateDate DESC " +
                                ")" +
                                " and WeChatId='" + weChatId + "' and ChangeType=" + type + " order BY CreateDate DESC";
            }
            return this.BaseRepository().FindList<WeChatAccountLogEntity>(sqlStr);
        }
        #endregion
    }
}
