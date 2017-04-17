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
using System.Data;
using ZoneTop.Application.Code;
using System.Linq;
using ZoneTop.Util.Extension;
using ZoneTop.Application.Busines.SystemManage;
using ZoneTop.Application.Entity.SystemManage;

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
        /// <summary>
        /// 订餐情况
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderList()
        {
            return View();
        }
        /// <summary>
        /// 订餐情况
        /// </summary>
        /// <returns></returns>
        public ActionResult IncOrDecList()
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
            var watch = CommonHelper.TimerStart();
            var data = accountbll.GetAccountList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 金额变动日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="userId"></param>
        /// <param name="wechatId"></param>
        /// <param name="queryJson"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAccountLogListJson(Pagination pagination, string userId, string wechatId, string queryJson, string type)
        {
            var watch = CommonHelper.TimerStart();
            var data = accountbll.GetAccountLogList(pagination, userId, wechatId, queryJson, type);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 消费流水总表 
        /// </summary>
        ///  <param name="pagination">页面参数</param>
        /// <param name="queryJson">时间参数 date1-date2</param>
        /// <returns></returns>
        public ActionResult GetConsumptionFlow(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            IEnumerable<ConsumptionFlow> flowList = accountbll.GetConsumptionFlow(pagination, queryJson);
            var jsonData = new
            {
                rows = flowList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
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
                if (wechatId == null || wechatId == "")
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
                WeChatApiController.RecordExceptionToFile("========点餐账号：" + accountEntity + "==============");
                string money = "";
                IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("OrderMoney");
                foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
                {
                    money = i.ItemValue;
                }
                if (accountEntity.Money < int.Parse(money))
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
        public ActionResult WeChatUnsubscribe(string code, string accountLogId, string type)
        {
            accountbll.WeChatOrder("", accountLogId, "2", "");
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
        public ActionResult WeChatOrderLog(string code, string type, string num)
        {
            string wechatId = codeToWeChatId(code);
            WeChatApiController.RecordExceptionToFile(code + "==>" + wechatId + "--" + type + "--" + num);
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
        /// <param name="type">1：自己今天的点餐情况 2：所有人今天的订餐情况  alltoday：今天所有人账户变动情况 </param>
        /// <returns></returns>
        public ActionResult GetTodayOrderNum(string code, string type)
        {
            string wechatId = codeToWeChatId(code);
            IEnumerable<WeChatAccountLogEntity> logList;
            if (type == "alltoday")
            {
                string queryJson = "{\"condition\":\"Date\",\"keyword\":\"Date\",\"minDate\":\"" + System.DateTime.Today.ToString() + "\",\"maxDate\":\"" + System.DateTime.Today.AddDays(1).ToString() + "\"}";
                logList = accountbll.GetAccountLogList(null, "all", null, queryJson, "alltoday");//
            }
            else if (type == "1")
            {
                logList = accountbll.WeChatOrderLog(wechatId, "2", 0, 0);//自己今天的
            }
            else
            {
                logList = accountbll.WeChatOrderLog("", "2", 0, 0);//所有今天的
            }
            WeChatAccountEntity entity = accountbll.GetAccountEntityByWechatId(wechatId);
            //是否可以点餐
            DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> IsWeChatOrderList=dataItemCache.GetDataItemList("IsWeChatOrder");//是否可以点餐
            DataItemDetailEntity orderItem= new DataItemDetailEntity();
            foreach (Entity.SystemManage.ViewModel.DataItemModel i in IsWeChatOrderList)
            {
                orderItem = dataItemDetailBLL.GetEntity(i.ItemDetailId);
            }
            var data = new
            {
                IsWeChatOrder=orderItem.ItemValue,
                entity = entity,
                logList = logList,
            };
            return ToJsonResult(data);
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 消费流水总表 导出数据查询
        /// </summary>
        /// <param name="queryJson">时间参数 date1-date2</param>
        /// <returns></returns>
        public ActionResult GetConsumptionFlowExport(string queryJson)
        {
            IEnumerable<ConsumptionFlow> flowList = accountbll.GetConsumptionFlow(null, queryJson);
            var data = new DataTable();
            data.Columns.Add("Date", Type.GetType("System.String"));
            data.Columns.Add("Count", Type.GetType("System.String"));
            data.Columns.Add("SumTotal", Type.GetType("System.String"));
            DataRow row;
            int count = 0;//次数总计
            decimal total = 0;//金钱总计
            string dateStr = "";
            foreach (ConsumptionFlow flow in flowList)
            {
                count += flow.Count;
                total += flow.SumTotal;
                row = data.NewRow();
                row["Date"] = flow.Date.ToString("yyyy年MM月dd日");
                row["Count"] = flow.Count.ToString();
                row["SumTotal"] = flow.SumTotal.ToString() + "元";
                data.Rows.Add(row);
            }
            if (flowList != null)
            {
                dateStr = data.Rows[0]["Date"].ToString() + "-" + data.Rows[data.Rows.Count - 1]["Date"].ToString();
            }
            else
            {
                var queryParam = queryJson.ToJObject();
                if (!queryParam["minDate"].IsEmpty())
                {
                    dateStr += queryParam["minDate"].ToDate().ToString("yyyy年MM月dd日");
                }
                if (!queryParam["maxDate"].IsEmpty())
                {
                    dateStr += "-" + queryParam["maxDate"].ToDate().ToString("yyyy年MM月dd日");
                }
            }
            //空行
            row = data.NewRow();
            data.Rows.Add(row);
            //统计
            row = data.NewRow();
            row["Date"] = "总计：";
            row["Count"] = count.ToString();
            row["SumTotal"] = total.ToString() + "元";
            data.Rows.Add(row);
            //空行
            row = data.NewRow();
            data.Rows.Add(row);
            //制表时间和人
            row = data.NewRow();
            row["Date"] = "时间范围：" + dateStr;
            row["Count"] = "制表时间：" + DateTime.Now.ToString("yyyy年MM月dd日");
            row["SumTotal"] = "制表人：" + OperatorProvider.Provider.Current().UserName;
            data.Rows.Add(row);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 增减款 导出数据查询
        /// </summary>
        /// <param name="queryJson">时间参数 date1-date2</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetIncOrDecExport(string queryJson, string type)
        {
            IEnumerable<WeChatAccountLogEntity> logList = accountbll.GetAccountLogList(null, null, null, queryJson, type).OrderBy(t => t.CreateDate);
            var data = new DataTable();
            data.Columns.Add("UserName", Type.GetType("System.String"));//用户名
            data.Columns.Add("WeChatId", Type.GetType("System.String"));//微信管理账号
            data.Columns.Add("ChangeType", Type.GetType("System.String"));//微信管理账号
            data.Columns.Add("MoneyChange", Type.GetType("System.String"));//金额
            data.Columns.Add("CreateDate", Type.GetType("System.String"));//变动时间
            data.Columns.Add("CreateUserName", Type.GetType("System.String"));//微信管理账号
            data.Columns.Add("Description", Type.GetType("System.String"));//说明
            DataRow row;
            decimal? incTotal = 0;//增款
            decimal? decTotal = 0;//减款
            string dateStr = "";
            foreach (WeChatAccountLogEntity log in logList)
            {
                row = data.NewRow();
                if (log.ChangeType == 1)//充值
                {
                    incTotal += log.MoneyChange;
                    row["MoneyChange"] = "+"+log.MoneyChange.ToString()+"元";
                    row["ChangeType"] = "充值";
                }
                else if (log.ChangeType == 3)//扣款
                {
                    decTotal += log.MoneyChange;
                    row["MoneyChange"] = "-"+log.MoneyChange.ToString()+"元";
                    row["ChangeType"] = "扣款";
                }
                row["CreateDate"] = ((DateTime)log.CreateDate).ToString("yyyy年MM月dd日 HH:mm:ss");
                row["WeChatId"] = log.WeChatId.ToString();
                row["UserName"] = log.UserName.ToString();
                row["CreateUserName"] = log.CreateUserName.ToString();
                row["Description"] = log.Description.ToString();
                data.Rows.Add(row);
            }
            if (logList != null)
            {
                dateStr = data.Rows[0]["CreateDate"].ToString().Substring(0, 11) + "-" + data.Rows[data.Rows.Count - 1]["CreateDate"].ToString().Substring(0, 11);
            }
            else
            {
                var queryParam = queryJson.ToJObject();
                if (!queryParam["minDate"].IsEmpty())
                {
                    dateStr += queryParam["minDate"].ToDate().ToString("yyyy年MM月dd日");
                }
                if (!queryParam["maxDate"].IsEmpty())
                {
                    dateStr += "-" + queryParam["maxDate"].ToDate().ToString("yyyy年MM月dd日");
                }
            }
            //空行
            row = data.NewRow();
            data.Rows.Add(row);
            //统计
            row = data.NewRow();
            row["UserName"] = "总计";
            row["MoneyChange"] = "增款：" + incTotal + "元";
            data.Rows.Add(row);
            row = data.NewRow();
            row["MoneyChange"] = "减款：" + decTotal + "元";
            data.Rows.Add(row);
            //空行
            row = data.NewRow();
            data.Rows.Add(row);
            //制表时间和人
            row = data.NewRow();
            row["UserName"] = "时间范围：" + dateStr;
            row["CreateDate"] = "制表时间：" + DateTime.Now.ToString("yyyy年MM月dd日");
            row["MoneyChange"] = "制表人：" + OperatorProvider.Provider.Current().UserName;
            data.Rows.Add(row);
            return Content(data.ToJson());
        }

        #endregion
    }
}
