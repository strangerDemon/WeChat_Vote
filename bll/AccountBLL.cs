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
    /// �� �� 2.6
    /// Copyright (c) 2014-2016
    /// �� ������������Ա
    /// �� �ڣ�2017-03-23 09:03
    /// �� ����΢�Ź���oa�Ľ���˺�
    /// </summary>
    public class AccountBLL
    {
        private AccountIService service = new AccountService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
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
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntity(string keyValue)
        {
            return service.GetAccountEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="wechatId">΢�ź�</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntityByWechatId(string wechatId)
        {
            return service.GetAccountEntityByWechatId(wechatId);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// �޸��˻� ��ֵ �ۿ�
        /// </summary>
        /// <param name="logEntity">��־ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(WeChatAccountLogEntity logEntity)
        {
            try
            {
                service.SaveForm(logEntity);
                remindWeChat(logEntity, "oa", true,"");//�����û�//΢���������û���ֵ�ۿ�
            }
            catch (Exception ex)
            {
                remindWeChat(logEntity, "oa", true, ex.ToString());//�����û�
                throw;
            }
        }

        /// <summary>
        /// ���������˻�
        /// </summary>
        /// <param name="accountList">�����б�</param>
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

        #region WeChat ΢�Ŷ�ʹ��
        /// <summary>
        /// ΢�ŵ�� ȡ������
        /// </summary>
        /// <param name="weChatId">���ʱ ��</param>
        /// <param name="accountLogId"> ȡ��ʱ ��</param>
        /// <param name="type"> ���� 1 ��� 2 ȡ��</param>
        /// <param name="Money"> �ֵ��ڶ�������һ�ε����Ҫ����Ǯ�����ʱ</param>
        public void WeChatOrder(string weChatId, string accountLogId, string type,string Money)
        {
            WeChatAccountLogEntity logEntity;
            if (type == "1")//���
            {
                WeChatUserService wxUserService = new WeChatUserService();
                WeChatUserRelationEntity wxRelationEntity = wxUserService.GetEntity(weChatId);

                UserService userService = new UserService();
                UserEntity user = userService.GetEntity(wxRelationEntity.UserId);

                logEntity = new WeChatAccountLogEntity();
                logEntity.ChangeType = 2;
                //logEntity.Create();����ʹ��create������ ��Ϊ���ʱ��û����Ա��¼��
                logEntity.OrderCreate();//ֻ��createDate ��id
                logEntity.CreateUserId = user.UserId;
                logEntity.CreateUserName = user.RealName;

                logEntity.Description=System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +" ���";
                logEntity.IsUnsubscribe = 0;
                logEntity.MoneyChange = int.Parse(Money);
                logEntity.WeChatId = weChatId;
                logEntity.UserId = user.UserId;
                logEntity.UserName = user.RealName;
            }
            else//ȡ��
            {
                logEntity = service.GetAccountLogEntity(accountLogId);
                if (logEntity.IsUnsubscribe == 1)
                {
                    remindWeChat(logEntity, "wechat", false, "�����ظ��˶�");//�����û�
                    return;
                }
                logEntity.IsUnsubscribe = 1;
            }
            if (System.DateTime.Now.Hour >= 18 || System.DateTime.Now.Hour < 8)
            {
                remindWeChat(logEntity, "wechat", false, "���ͺ�ȡ��ֻ����8����18��֮��");//�����û�
                return;
            }
            try
            {
                service.WeChatOrder(logEntity, type);
                remindWeChat(logEntity,"wechat",true,"");//�����û�
            }
            catch (Exception ex)
            {
                remindWeChat(logEntity, "wechat", false,ex.ToString());//�����û�
                throw;
            }
        }
        /// <summary>
        /// ��ͼ�¼ ��ֵ��¼
        /// </summary>
        /// <param name="weChatId">�û���Ϣ</param>
        /// <param name="type">1 ��ͼ�¼ 2 ��ֵ��¼</param>
        /// <param name="page">ҳ��</param>
        /// <param name="pageNum">ÿҳ����</param>
        /// <returns></returns>
        public IEnumerable<WeChatAccountLogEntity> WeChatOrderLog(string weChatId, string type, int page, int pageNum)
        {
            return service.WeChatOrderLog(weChatId, type, page, pageNum);
        }
        
        #endregion

        #region ΢�Ŷ������û�
        /// <summary>
        /// ΢�Ŷ������û�
        /// </summary>
        /// <param name="logEntity"></param>
        /// <param name="requestParty">���� oa ���� ΢��</param>
        /// <param name="isSuccess">�ɹ�����ʧ��</param>
        /// <param name="exception">ʧ�ܵ�ԭ��</param>
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
            string typeName = "��ֵ";
            if (logEntity.ChangeType == 1)
            {
                typeName ="��ֵ";
            }
            else
            {
                if (requestParty == "oa")//oa �����Ķ��ǿۿ�
                {
                    typeName = "�ۿ�";
                } else if (logEntity.IsUnsubscribe == 0)//wechat������
                {
                    typeName = "���";
                    item.url = new Oauth2Authorize()
                    {
                        appid = "��˾scorpid",
                        redirect_uri = "�������url/WeChat/Order/todayOrderList.html?type=1",
                        state = "ping"
                    }.GetAuthorizeUrl();
                }
                else
                {
                    typeName = "�˿�";
                }
                //typeName = requestParty == "wechat" ? logEntity.IsUnsubscribe == 0 ? "���" : "�˿�" : "�ۿ�";
            }
            if (isSuccess)
            {
                item.title = entity.UserName + "��" + typeName + "��¼";
                item.description = typeName + "���:" + logEntity.MoneyChange.ToString() + "Ԫ \n�˻����:" + entity.Money.ToString()+"Ԫ";
            }
            else
            {
                item.title =  typeName + "ʧ��";
                item.description = "�˻����:" + entity.Money.ToString() + "Ԫ";
            }
            itemList.articles.Add(item);
            message.news = itemList;
            result = message.Send();
        }
        #endregion

    }
}
