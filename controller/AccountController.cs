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
            var data = accountbll.GetAccountList(pagination,queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ���䶯��־
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
                if (wechatId == null || wechatId=="")
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
                WeChatApiController.RecordExceptionToFile("========����˺ţ�"+accountEntity + "==============");             
                string money = "";
                IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("OrderMoney");
                foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
                {
                    money = i.ItemValue;
                }
                if (accountEntity.Money<int.Parse(money))
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
        /// ��ͼ�¼
        /// </summary>
        /// <param name="code">�û���Ϣ</param>
        /// <param name="type">��ȡ����</param>
        /// <param name="num">ҳ��</param>
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
        /// <returns></returns>
        public ActionResult GetTodayOrderNum(string code,string type)
        {
            string wechatId =codeToWeChatId(code);
            var logList = type == "1" ? accountbll.WeChatOrderLog(wechatId, "2", 0, 0) : accountbll.WeChatOrderLog("", "2", 0, 0);//���н����
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
