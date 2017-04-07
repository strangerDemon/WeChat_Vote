using ZoneTop.Application.Entity.WeChatManage;
using ZoneTop.Application.IService.WeChatManage;
using ZoneTop.Application.Service.WeChatManage;
using ZoneTop.Util.WebControl;
using System.Collections.Generic;
using System;
using ZoneTop.Util.WeChat.Model.Request.SendMessage;
using ZoneTop.Util.WeChat.Model.Request;
using ZoneTop.Application.Entity.BaseManage;
using ZoneTop.Application.Service.BaseManage;
using ZoneTop.Application;

namespace ZoneTop.Application.Busines.WeChatManage
{
    /// <summary>
    /// 版 本 2.6
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-23 09:03
    /// 描 述：微信关联oa的金额账号
    /// </summary>
    public class AccountBLL
    {
        private AccountIService service = new AccountService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<WeChatAccountEntity> GetAccountList(Pagination pagination, string queryJson)
        {
            return service.GetAccountList(pagination,queryJson);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="userId"></param>
        /// <param name="wechatId"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<WeChatAccountLogEntity> GetAccountLogList(Pagination pagination,string userId, string wechatId, string queryJson)
        {
            return service.GetAccountLogList(pagination, userId,  wechatId,  queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntity(string keyValue)
        {
            return service.GetAccountEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="wechatId">微信号</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntityByWechatId(string wechatId)
        {
            return service.GetAccountEntityByWechatId(wechatId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 修改账户 充值 扣款
        /// </summary>
        /// <param name="logEntity">日志实体对象</param>
        /// <returns></returns>
        public void SaveForm(WeChatAccountLogEntity logEntity)
        {
            try
            {
                service.SaveForm(logEntity);
                remindWeChat(logEntity, "oa", true,"");//提醒用户//微信上提醒用户充值扣款
            }
            catch (Exception ex)
            {
                remindWeChat(logEntity, "oa", true, ex.ToString());//提醒用户
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
                service.BatchImport(accountList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region WeChat 微信端使用
        /// <summary>
        /// 微信点餐 取消订餐
        /// </summary>
        /// <param name="weChatId">点餐时 有</param>
        /// <param name="accountLogId"> 取消时 有</param>
        /// <param name="type"> 类型 1 点餐 2 取消</param>
        /// <param name="Money"> 字典内读出来的一次点餐需要多少钱，点餐时</param>
        public void WeChatOrder(string weChatId, string accountLogId, string type,string Money)
        {
            WeChatAccountLogEntity logEntity;
            if (type == "1")//点餐
            {
                WeChatUserService wxUserService = new WeChatUserService();
                WeChatUserRelationEntity wxRelationEntity = wxUserService.GetEntity(weChatId);

                UserService userService = new UserService();
                UserEntity user = userService.GetEntity(wxRelationEntity.UserId);

                logEntity = new WeChatAccountLogEntity();
                logEntity.ChangeType = 2;
                //logEntity.Create();不能使用create来创建 因为点餐时是没有人员登录的
                logEntity.OrderCreate();//只有createDate 和id
                logEntity.CreateUserId = user.UserId;
                logEntity.CreateUserName = user.RealName;

                logEntity.Description=System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +" 点餐";
                logEntity.IsUnsubscribe = 0;
                logEntity.MoneyChange = int.Parse(Money);
                logEntity.WeChatId = weChatId;
                logEntity.UserId = user.UserId;
                logEntity.UserName = user.RealName;
            }
            else//取消
            {
                logEntity = service.GetAccountLogEntity(accountLogId);
                if (logEntity.IsUnsubscribe == 1)
                {
                    remindWeChat(logEntity, "wechat", false, "不能重复退订");//提醒用户
                    return;
                }
                logEntity.IsUnsubscribe = 1;
            }
            if (System.DateTime.Now.Hour >= 18 || System.DateTime.Now.Hour < 8)
            {
                remindWeChat(logEntity, "wechat", false, "订餐和取消只能在8点至18点之间");//提醒用户
                return;
            }
            try
            {
                service.WeChatOrder(logEntity, type);
                remindWeChat(logEntity,"wechat",true,"");//提醒用户
            }
            catch (Exception ex)
            {
                remindWeChat(logEntity, "wechat", false,ex.ToString());//提醒用户
                throw;
            }
        }
        /// <summary>
        /// 点餐记录 充值记录
        /// </summary>
        /// <param name="weChatId">用户信息</param>
        /// <param name="type">1 点餐记录 2 充值记录</param>
        /// <param name="page">页码</param>
        /// <param name="pageNum">每页数量</param>
        /// <returns></returns>
        public IEnumerable<WeChatAccountLogEntity> WeChatOrderLog(string weChatId, string type, int page, int pageNum)
        {
            return service.WeChatOrderLog(weChatId, type, page, pageNum);
        }
        
        #endregion

        #region 微信端提醒用户
        /// <summary>
        /// 微信端提醒用户
        /// </summary>
        /// <param name="logEntity"></param>
        /// <param name="requestParty">请求方 oa 还是 微信</param>
        /// <param name="isSuccess">成功还是失败</param>
        /// <param name="exception">失败的原因</param>
        private void remindWeChat(WeChatAccountLogEntity logEntity,string requestParty,bool isSuccess,string exception)
        {
            AccountBLL bll = new AccountBLL();//
            WeChatAccountEntity entity = bll.GetAccountEntityByWechatId(logEntity.WeChatId);
            MessageSendResult result = null;
            SendNews message = new SendNews();
            message.agentid = "8";
            message.touser = logEntity.WeChatId;
            ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItem item = new SendNews.SendItem();
            ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItemLoist itemList = new SendNews.SendItemLoist();
            itemList.articles = new List<SendNews.SendItem>();          
            string typeName = "充值";
            if (logEntity.ChangeType == 1)
            {
                typeName ="充值";
            }
            else
            {
                if (requestParty == "oa")//oa 端来的都是扣款
                {
                    typeName = "扣款";
                } else if (logEntity.IsUnsubscribe == 0)//wechat端来的
                {
                    typeName = "点餐";
                    item.url = new Oauth2Authorize()
                    {
                        appid = "公司scorpid",
                        redirect_uri = "程序部署的url/WeChat/Order/todayOrderList.html?type=1",
                        state = "ping"
                    }.GetAuthorizeUrl();
                }
                else
                {
                    typeName = "退款";
                }
                //typeName = requestParty == "wechat" ? logEntity.IsUnsubscribe == 0 ? "点餐" : "退款" : "扣款";
            }
            if (isSuccess)
            {
                item.title = entity.UserName + "的" + typeName + "记录";
                item.description = typeName + "金额:" + logEntity.MoneyChange.ToString() + "元 \n账户余额:" + entity.Money.ToString()+"元";
            }
            else
            {
                item.title =  typeName + "失败";
                item.description = "账户余额:" + entity.Money.ToString() + "元";
            }
            itemList.articles.Add(item);
            message.news = itemList;
            result = message.Send();
        }
        #endregion

    }
}
