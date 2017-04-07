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
    /// �� �� 2.6
    /// Copyright (c) 2014-2016
    /// �� ������������Ա
    /// �� �ڣ�2017-03-23 09:03
    /// �� ����΢�Ź���oa�Ľ���˺�
    /// </summary>
    public class AccountService : RepositoryFactory, AccountIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<WeChatAccountEntity> GetAccountList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<WeChatAccountEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "UserName":            //�û���
                        expression = expression.And(t => t.UserName.Contains(keyord));
                        break;
                    case "WeChatId":            //΢���˺�
                        expression = expression.And(t => t.WeChatId.Contains(keyord));
                        break;
                    case "Money":            //��Χ
                        if (!queryParam["minMoney"].IsEmpty())
                        {
                            int minMoney = queryParam["minMoney"].ToInt();
                            expression = expression.And(t => t.Money >= minMoney);//����Ҫ��==
                        }
                        if (!queryParam["maxMoney"].IsEmpty())
                        {
                            int maxMoney = queryParam["maxMoney"].ToInt();
                            expression = expression.And(t => t.Money <= maxMoney);//����
                        }
                        break;
                    case "Date":         //�䶯����
                        if (!queryParam["minDate"].IsEmpty())
                        {
                            DateTime startTime = queryParam["minDate"].ToDate();
                            expression = expression.And(t => t.ModifyDate >= startTime);//����Ҫ��==
                        }
                        if (!queryParam["maxDate"].IsEmpty())
                        {
                            DateTime endTime = queryParam["maxDate"].ToDate().AddDays(1);
                            expression = expression.And(t => t.ModifyDate < endTime);//����
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
        /// ��ȡ�˻��䶯��־�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="wechatId">΢��id</param>
        /// <param name="queryJson">��ѯ����</param>
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
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "UserName":            //�û���
                        expression = expression.And(t => t.UserName.Contains(keyord));
                        break;
                    case "WeChatId":            //΢���˺�
                        expression = expression.And(t => t.WeChatId.Contains(keyord));
                        break;
                    case "ChangeType":            //�䶯����
                        int changeType = keyord.ToInt();
                        if (changeType >= 0)
                        {
                            expression = expression.And(t => t.ChangeType == changeType);
                        }
                        break;
                    case "Date":         //�䶯����
                        if (!queryParam["minDate"].IsEmpty())
                        {
                            DateTime startTime = queryParam["minDate"].ToDate();
                            expression = expression.And(t => t.CreateDate >= startTime);//����Ҫ��==
                        }
                        if (!queryParam["maxDate"].IsEmpty())
                        {
                            DateTime endTime = queryParam["maxDate"].ToDate().AddDays(1);
                            expression = expression.And(t => t.CreateDate < endTime);//����
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
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<WeChatAccountEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="wechatId">΢�ź�</param>
        /// <returns></returns>
        public WeChatAccountEntity GetAccountEntityByWechatId(string wechatId)
        {
            return this.BaseRepository().FindEntity<WeChatAccountEntity>(t => t.WeChatId.Equals(wechatId));
        }
        /// <summary>
        /// ��ȡ��־ʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public WeChatAccountLogEntity GetAccountLogEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<WeChatAccountLogEntity>(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete<WeChatAccountEntity>(keyValue);
        }
        /// <summary>
        /// �޸��˻� ��ֵ �ۿ�
        /// </summary>
        /// <param name="logEntity">��־ʵ�����</param>
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
                if (logEntity.ChangeType == 1)//��ֵ
                {
                    entity.Money += logEntity.MoneyChange;
                }
                else //changeType==3 �ۿ� 
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
        /// ���������˻�
        /// </summary>
        /// <param name="accountList">�����б�</param>
        /// <returns></returns>
        public void BatchImport(List<WeChatAccountEntity> accountList)
        {
            try
            {
                WeChatUserService wxUserService=new WeChatUserService();
                WeChatUserRelationEntity relationEntity = new WeChatUserRelationEntity();
                if (accountList != null)//�б�Ϊ��
                {
                    foreach (WeChatAccountEntity account in accountList)//���� ΢�ź� / �û���
                    {
                        relationEntity = wxUserService.GetEntityByUserId(account.UserId);
                        if (relationEntity == null) { continue; }
                        if (this.BaseRepository().FindEntity<WeChatAccountEntity>(account.UserId) == null)//�����Ѿ����ڵ�
                        {
                            account.Create(account.UserId);
                            account.Modify("");
                            account.Money = 0;//�˻���ʼ��
                            account.WeChatId = relationEntity.UserRelationId;
                            this.BaseRepository().Insert<WeChatAccountEntity>(account);//����
                        }
                        else //�˻����Ѿ����ڵ� ���ǿ��ܴ�����Ϣ����������� һ��ʼû��΢�ź�=����������
                        {
                            account.Modify("");
                            account.WeChatId = relationEntity.UserRelationId;
                            this.BaseRepository().Update<WeChatAccountEntity>(account);//����
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

        #region WeChat ΢�Ŷ�ʹ��
        /// <summary>
        /// ΢�ŵ�� ���Ǵ���һ����¼��
        /// ΢��ȡ������Ϊ�޸�һ�� ��¼
        /// </summary>
        /// <param name="logEntity">��־ʵ��</param>
        /// <param name="type">1 ���  2 ȡ������</param>
        /// <returns></returns>
        public void WeChatOrder(WeChatAccountLogEntity logEntity, string type)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WeChatAccountEntity entity = this.BaseRepository().FindEntity<WeChatAccountEntity>(logEntity.UserId);
                if (logEntity.IsUnsubscribe == 1)//�˿� �޸� log���� 
                {
                    logEntity.ModifyDate = DateTime.Now;
                    logEntity.ModifyUserId = logEntity.UserId;
                    logEntity.ModifyUserName = logEntity.UserName;
                    db.Update(logEntity);
                    entity.Money += logEntity.MoneyChange;//�˻���Ǯ
                }
                else//΢�ŵ�� ��������
                {
                    db.Insert(logEntity);
                    entity.Money -= logEntity.MoneyChange;
                }
                //entity.Modify("");//������Modify����Ϊ��ʱû���˺ŵ�¼
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
        /// ��ͼ�¼ ��ֵ��¼ 
        /// </summary>
        /// <param name="weChatId"></param>
        /// <param name="type">1��ֵ 2����</param>
        /// <param name="page"> ҳ�� </param>
        /// <param name="pageNum"> ÿҳ����</param>
        /// <returns></returns>
        public IEnumerable<WeChatAccountLogEntity> WeChatOrderLog(string weChatId, string type, int page, int pageNum)
        {
            string sqlStr = "";
            if (page == 0)
            {
                //AND DateDiff(dd,CreateDate,getdate())=0 ��ȡ���յ�����
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
