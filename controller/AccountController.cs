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
    /// �� �� 2.6
    /// Copyright (c) 2014-2016
    /// �� ������������Ա
    /// �� �ڣ�2017-03-23 09:03
    /// �� ����΢�Ź���oa�Ľ���˺�
    /// </summary>
    public class AccountController : MvcControllerBaseOfIgnore
    {
        private AccountBLL accountbll = new AccountBLL();
        DataItemCache dataItemCache = new DataItemCache();
        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountIndex()
        {
            return View();
        }
        /// <summary>
        /// ��־�б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountLogIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountForm()
        {
            return View();
        }
        /// <summary>
        /// ��Ա��
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberForm()
        {
            return View();
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderList()
        {
            return View();
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public ActionResult IncOrDecList()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// �˺��б�
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
        /// ���䶯��־
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
        /// ������ˮ�ܱ� 
        /// </summary>
        ///  <param name="pagination">ҳ�����</param>
        /// <param name="queryJson">ʱ����� date1-date2</param>
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetAccountFormJson(string keyValue)
        {
            var data = accountbll.GetAccountEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            accountbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// �޸��˻� ��ֵ �ۿ�
        /// </summary>
        /// <param name="logEntity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(WeChatAccountLogEntity logEntity)
        {
            accountbll.SaveForm(logEntity);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ���������˻�
        /// </summary>
        /// <param name="entityListStr">�����б�string</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchImport(string entityListStr)
        {
            List<WeChatAccountEntity> accountList = entityListStr.ToList<WeChatAccountEntity>();
            accountbll.BatchImport(accountList);
            return Success("�����ɹ���");
        }
        #endregion

        #region WeChat ΢�Ŷ�ʹ��
        /// <summary>
        /// ΢�ŵ��
        /// </summary>
        /// <param name="code">�û���Ϣ</param>
        /// <returns></returns>
        public ActionResult WeChatOrder(string code)
        {

            try
            {
                string wechatId = codeToWeChatId(code);
                if (wechatId == null || wechatId == "")
                {
                    WeChatApiController.RecordExceptionToFile("����ʧ�ܡ���Ϣ����");
                    return ToJsonResult("����ʧ�ܡ���Ϣ����");
                }
                WeChatAccountEntity accountEntity = accountbll.GetAccountEntityByWechatId(wechatId);
                if (accountEntity == null)
                {
                    WeChatApiController.RecordExceptionToFile("����ʧ�ܡ�����δ��ͨ΢�ŵ���˺�");
                    return ToJsonResult("����ʧ�ܡ�����δ��ͨ΢�ŵ���˺�");
                }
                WeChatApiController.RecordExceptionToFile("========����˺ţ�" + accountEntity + "==============");
                string money = "";
                IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("OrderMoney");
                foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
                {
                    money = i.ItemValue;
                }
                if (accountEntity.Money < int.Parse(money))
                {
                    WeChatApiController.RecordExceptionToFile("����ʧ�ܡ��˻�����");
                    return ToJsonResult("����ʧ�ܡ��˻�����");
                }
                accountbll.WeChatOrder(wechatId, "", "1", money);//����
                var entity = accountbll.GetAccountEntityByWechatId(wechatId);
                var logList = accountbll.WeChatOrderLog(wechatId, "2", 0, 0);//���н����
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
                return ToJsonResult("����ʧ�ܡ�");
            }

        }
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="code">�û���Ϣ</param>
        /// <param name="accountLogId">��־��id</param>
        /// <param name="type">����Դ ���ؽ��յĻ��� ǰ����</param>
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
        /// ��ͼ�¼
        /// </summary>
        /// <param name="code">�û���Ϣ</param>
        /// <param name="type">��ȡ����</param>
        /// <param name="num">ҳ��</param>
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
        /// code ת��΢�� �˺�
        /// ����session���ң�ˢ�£���û���ҵ���ȥ΢�Žӿڻ�ȡ����һ�Σ�
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string codeToWeChatId(string code)
        {
            if (code == "system") { return "heqi"; }//���Դ����

            string wechatId = WebHelper.GetSession(code);//session�л�ȡwechatid
            if (wechatId == null || wechatId == "")//session��û�У����»�ȡ Code���浽session��
            {
                UserGetuserinfo userInfo = new UserGetuserinfo();
                userInfo.code = code;
                userInfo.agentid = "8";//Ӧ�úţ��̶�
                wechatId = userInfo.Send().UserId;
                WebHelper.WriteSession(code, wechatId);
            }
            WeChatApiController.RecordExceptionToFile("Session[" + code + "]==>" + wechatId);
            return wechatId;
        }
        /// <summary>
        /// ��ȡ���յ����� ���û���Ϣ
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type">1���Լ�����ĵ����� 2�������˽���Ķ������  alltoday�������������˻��䶯��� </param>
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
                logList = accountbll.WeChatOrderLog(wechatId, "2", 0, 0);//�Լ������
            }
            else
            {
                logList = accountbll.WeChatOrderLog("", "2", 0, 0);//���н����
            }
            WeChatAccountEntity entity = accountbll.GetAccountEntityByWechatId(wechatId);
            //�Ƿ���Ե��
            DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> IsWeChatOrderList=dataItemCache.GetDataItemList("IsWeChatOrder");//�Ƿ���Ե��
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

        #region ��������
        /// <summary>
        /// ������ˮ�ܱ� �������ݲ�ѯ
        /// </summary>
        /// <param name="queryJson">ʱ����� date1-date2</param>
        /// <returns></returns>
        public ActionResult GetConsumptionFlowExport(string queryJson)
        {
            IEnumerable<ConsumptionFlow> flowList = accountbll.GetConsumptionFlow(null, queryJson);
            var data = new DataTable();
            data.Columns.Add("Date", Type.GetType("System.String"));
            data.Columns.Add("Count", Type.GetType("System.String"));
            data.Columns.Add("SumTotal", Type.GetType("System.String"));
            DataRow row;
            int count = 0;//�����ܼ�
            decimal total = 0;//��Ǯ�ܼ�
            string dateStr = "";
            foreach (ConsumptionFlow flow in flowList)
            {
                count += flow.Count;
                total += flow.SumTotal;
                row = data.NewRow();
                row["Date"] = flow.Date.ToString("yyyy��MM��dd��");
                row["Count"] = flow.Count.ToString();
                row["SumTotal"] = flow.SumTotal.ToString() + "Ԫ";
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
                    dateStr += queryParam["minDate"].ToDate().ToString("yyyy��MM��dd��");
                }
                if (!queryParam["maxDate"].IsEmpty())
                {
                    dateStr += "-" + queryParam["maxDate"].ToDate().ToString("yyyy��MM��dd��");
                }
            }
            //����
            row = data.NewRow();
            data.Rows.Add(row);
            //ͳ��
            row = data.NewRow();
            row["Date"] = "�ܼƣ�";
            row["Count"] = count.ToString();
            row["SumTotal"] = total.ToString() + "Ԫ";
            data.Rows.Add(row);
            //����
            row = data.NewRow();
            data.Rows.Add(row);
            //�Ʊ�ʱ�����
            row = data.NewRow();
            row["Date"] = "ʱ�䷶Χ��" + dateStr;
            row["Count"] = "�Ʊ�ʱ�䣺" + DateTime.Now.ToString("yyyy��MM��dd��");
            row["SumTotal"] = "�Ʊ��ˣ�" + OperatorProvider.Provider.Current().UserName;
            data.Rows.Add(row);
            return Content(data.ToJson());
        }

        /// <summary>
        /// ������ �������ݲ�ѯ
        /// </summary>
        /// <param name="queryJson">ʱ����� date1-date2</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetIncOrDecExport(string queryJson, string type)
        {
            IEnumerable<WeChatAccountLogEntity> logList = accountbll.GetAccountLogList(null, null, null, queryJson, type).OrderBy(t => t.CreateDate);
            var data = new DataTable();
            data.Columns.Add("UserName", Type.GetType("System.String"));//�û���
            data.Columns.Add("WeChatId", Type.GetType("System.String"));//΢�Ź����˺�
            data.Columns.Add("ChangeType", Type.GetType("System.String"));//΢�Ź����˺�
            data.Columns.Add("MoneyChange", Type.GetType("System.String"));//���
            data.Columns.Add("CreateDate", Type.GetType("System.String"));//�䶯ʱ��
            data.Columns.Add("CreateUserName", Type.GetType("System.String"));//΢�Ź����˺�
            data.Columns.Add("Description", Type.GetType("System.String"));//˵��
            DataRow row;
            decimal? incTotal = 0;//����
            decimal? decTotal = 0;//����
            string dateStr = "";
            foreach (WeChatAccountLogEntity log in logList)
            {
                row = data.NewRow();
                if (log.ChangeType == 1)//��ֵ
                {
                    incTotal += log.MoneyChange;
                    row["MoneyChange"] = "+"+log.MoneyChange.ToString()+"Ԫ";
                    row["ChangeType"] = "��ֵ";
                }
                else if (log.ChangeType == 3)//�ۿ�
                {
                    decTotal += log.MoneyChange;
                    row["MoneyChange"] = "-"+log.MoneyChange.ToString()+"Ԫ";
                    row["ChangeType"] = "�ۿ�";
                }
                row["CreateDate"] = ((DateTime)log.CreateDate).ToString("yyyy��MM��dd�� HH:mm:ss");
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
                    dateStr += queryParam["minDate"].ToDate().ToString("yyyy��MM��dd��");
                }
                if (!queryParam["maxDate"].IsEmpty())
                {
                    dateStr += "-" + queryParam["maxDate"].ToDate().ToString("yyyy��MM��dd��");
                }
            }
            //����
            row = data.NewRow();
            data.Rows.Add(row);
            //ͳ��
            row = data.NewRow();
            row["UserName"] = "�ܼ�";
            row["MoneyChange"] = "���" + incTotal + "Ԫ";
            data.Rows.Add(row);
            row = data.NewRow();
            row["MoneyChange"] = "���" + decTotal + "Ԫ";
            data.Rows.Add(row);
            //����
            row = data.NewRow();
            data.Rows.Add(row);
            //�Ʊ�ʱ�����
            row = data.NewRow();
            row["UserName"] = "ʱ�䷶Χ��" + dateStr;
            row["CreateDate"] = "�Ʊ�ʱ�䣺" + DateTime.Now.ToString("yyyy��MM��dd��");
            row["MoneyChange"] = "�Ʊ��ˣ�" + OperatorProvider.Provider.Current().UserName;
            data.Rows.Add(row);
            return Content(data.ToJson());
        }

        #endregion
    }
}
