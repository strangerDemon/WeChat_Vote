using ZoneTop.Application.Entity.WeChatManage;
using ZoneTop.Application.Busines.WeChatManage;
using ZoneTop.Util;
using ZoneTop.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using ZoneTop.Util.WeChat.Model.Request.SendMessage;
using ZoneTop.Util.WeChat.Model.Request;
using ZoneTop.Application.Cache;
using System;

namespace ZoneTop.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 版 本 2.6
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-23 09:03
    /// 描 述：微信关联oa的金额账号
    /// </summary>
    public class AccountController : MvcControllerBaseOfIgnore
    {
        private AccountBLL accountbll = new AccountBLL();
        DataItemCache dataItemCache = new DataItemCache();
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountIndex()
        {
            return View();
        }
        /// <summary>
        /// 日志列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountLogIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountForm()
        {
            return View();
        }
        /// <summary>
        /// 成员表单
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 账号列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAccountListJson(Pagination pagination, string queryJson)
        {
            var data = accountbll.GetAccountList(pagination,queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 金额变动日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="userId"></param>
        /// <param name="wechatId"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAccountLogListJson(Pagination pagination, string userId,string wechatId,string queryJson)
        {
            var data = accountbll.GetAccountLogList(pagination, userId, wechatId,queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetAccountFormJson(string keyValue)
        {
            var data = accountbll.GetAccountEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            accountbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 修改账户 充值 扣款
        /// </summary>
        /// <param name="logEntity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(WeChatAccountLogEntity logEntity)
        {
            accountbll.SaveForm(logEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 批量导入账户
        /// </summary>
        /// <param name="entityListStr">对象列表string</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchImport(string entityListStr)
        {
            List<WeChatAccountEntity> accountList = entityListStr.ToList<WeChatAccountEntity>();
            accountbll.BatchImport(accountList);
            return Success("操作成功。");
        }
        #endregion

        #region WeChat 微信端使用
        /// <summary>
        /// 微信点餐
        /// </summary>
        /// <param name="code">用户信息</param>
        /// <returns></returns>
        public ActionResult WeChatOrder(string code)
        {
            
            try
            {
                string wechatId = codeToWeChatId(code);
                if (wechatId == null || wechatId=="")
                {
                    WeChatApiController.RecordExceptionToFile("订餐失败。信息有误");
                    return ToJsonResult("订餐失败。信息有误");
                }
                WeChatAccountEntity accountEntity = accountbll.GetAccountEntityByWechatId(wechatId);
                if (accountEntity == null)
                {
                    WeChatApiController.RecordExceptionToFile("订餐失败。您尚未开通微信点餐账号");
                    return ToJsonResult("订餐失败。您尚未开通微信点餐账号");
                }
                WeChatApiController.RecordExceptionToFile("========点餐账号："+accountEntity + "==============");             
                string money = "";
                IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("OrderMoney");
                foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
                {
                    money = i.ItemValue;
                }
                if (accountEntity.Money<int.Parse(money))
                {
                    WeChatApiController.RecordExceptionToFile("订餐失败。账户余额不足");
                    return ToJsonResult("订餐失败。账户余额不足");
                }
                accountbll.WeChatOrder(wechatId, "", "1", money);//订餐
                var entity = accountbll.GetAccountEntityByWechatId(wechatId);
                var logList = accountbll.WeChatOrderLog(wechatId, "2", 0, 0);//所有今天的
                var data = new
                {
                    entity = entity,
                    logList = logList,
                };
                return ToJsonResult(data);
            }
            catch (Exception ex)
            {
                WeChatApiController.RecordExceptionToFile(ex.ToString());
                return ToJsonResult("订餐失败。");
            }
            
        }
        /// <summary>
        /// 取消订餐
        /// </summary>
        /// <param name="code">用户信息</param>
        /// <param name="accountLogId">日志表id</param>
        /// <param name="type">请求源 返回今日的还是 前五条</param>
        /// <returns></returns>
        public ActionResult WeChatUnsubscribe(string code, string accountLogId,string type)
        {
            accountbll.WeChatOrder("",accountLogId,"2","");
            string wechatId = codeToWeChatId(code);
            string pageNum = "0";
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("OrderLog");
            foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
            {
                pageNum = i.ItemValue;
            }
            var logList = type == "1" ? accountbll.WeChatOrderLog(wechatId, "2", 0, 0) : accountbll.WeChatOrderLog(wechatId, "2", 1, int.Parse(pageNum));
            WeChatAccountEntity entity = accountbll.GetAccountEntityByWechatId(wechatId);
            var data = new
            {
                entity = entity,
                logList = logList,
            };
            return ToJsonResult(data);
        }
        /// <summary>
        /// 点餐记录
        /// </summary>
        /// <param name="code">用户信息</param>
        /// <param name="type">获取类型</param>
        /// <param name="num">页码</param>
        /// <returns></returns>
        public ActionResult WeChatOrderLog(string code,string type,string num)
        {
            string wechatId = codeToWeChatId(code);
            WeChatApiController.RecordExceptionToFile(code+"==>"+wechatId + "--" + type + "--" + num);
            string pageNum = "0";
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("OrderLog");
            foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
            {
                pageNum = i.ItemValue;
            }
            var logList = accountbll.WeChatOrderLog(wechatId, type, int.Parse(num), int.Parse(pageNum));
            WeChatAccountEntity entity = accountbll.GetAccountEntityByWechatId(wechatId);
            var data = new
            {
                entity = entity,
                logList = logList,
            };
            return ToJsonResult(data);
        }
        /// <summary>
        /// code 转成微信 账号
        /// 现在session中找（刷新），没有找到在去微信接口获取（第一次）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string codeToWeChatId(string code)
        {
            if (code == "system") { return "heqi"; }//测试代码段

            string wechatId = WebHelper.GetSession(code);//session中获取wechatid
            if (wechatId == null || wechatId == "")//session中没有，重新获取 Code保存到session中
            {
                UserGetuserinfo userInfo = new UserGetuserinfo();
                userInfo.code = code;
                userInfo.agentid = "8";//应用号，固定
                wechatId = userInfo.Send().UserId;
                WebHelper.WriteSession(code, wechatId);
            }
            WeChatApiController.RecordExceptionToFile("Session[" + code + "]==>" + wechatId);
            return wechatId;
        }
        /// <summary>
        /// 获取今日点餐情况 和用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetTodayOrderNum(string code,string type)
        {
            string wechatId =codeToWeChatId(code);
            var logList = type == "1" ? accountbll.WeChatOrderLog(wechatId, "2", 0, 0) : accountbll.WeChatOrderLog("", "2", 0, 0);//所有今天的
            WeChatAccountEntity entity = accountbll.GetAccountEntityByWechatId(wechatId);
            var data = new
            {
                entity=entity,
                logList = logList,
            };
            return ToJsonResult(data);
        }
        #endregion
    }
}
