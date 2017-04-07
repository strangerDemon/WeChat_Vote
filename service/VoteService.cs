using ZoneTop.Application.Entity.WeChatManage;
using ZoneTop.Application.IService.WeChatManage;
using ZoneTop.Data.Repository;
using ZoneTop.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using ZoneTop.Util.Extension;
using ZoneTop.Util;

namespace ZoneTop.Application.Service.WeChatManage
{
    /// <summary>
    /// �� �� 2.6
    /// Copyright (c) 2014-2016
    /// �� ������������Ա
    /// �� �ڣ�2017-03-10 09:47
    /// �� ����΢��ͶƱ��
    /// </summary>
    public class VoteService : RepositoryFactory, VoteIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<VoteEntity> GetVotePageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<VoteEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Title":            //��Ŀ
                        expression = expression.And(t => t.Title.Contains(keyord));
                        break;                   
                    case "VoteType":            //ͶƱ����
                        int voteType = Int32.Parse(keyord);
                        if (voteType >= 0)
                        {
                            expression = expression.And(t => t.VoteType == voteType);
                        }
                        break;
                    case "Status":          //״̬
                        int status = Int32.Parse(keyord);
                        if (status >= 0)
                        {
                            expression = expression.And(t => t.Status == status);//����Ҫ��==
                        }
                        break;
                    case "UserScope":            //�û���Χ
                        int userScope = Int32.Parse(keyord);
                        if (userScope == 1)//ȫ��˾
                        {
                            expression = expression.And(t => t.UserScope.Equals(keyord));
                        }
                        else if(userScope == 2)//�Զ���
                        {
                            expression = expression.And(t => t.UserScope.Equals(""));
                        }                
                        break;
                    case "Date":         //��������
                        if (!queryParam["DateBegin"].IsEmpty())
                        {
                            DateTime startTime = queryParam["DateBegin"].ToDate();
                            expression = expression.And(t => t.DateBegin >= startTime);//����Ҫ��==
                        }
                        if (!queryParam["DateEnd"].IsEmpty())
                        {
                            DateTime endTime = queryParam["DateEnd"].ToDate().AddDays(1);
                            expression = expression.And(t => t.DateEnd < endTime);//����
                        }
                        break;
                    
                    default:
                        break;
                }
            }
            //δɾ�� ��ʱû��ɾ��
            //expression = expression.And(t => t.DeleteMark == 0);
            if (pagination != null)
            {
                return this.BaseRepository().FindList<VoteEntity>(expression, pagination);
            }
            else
            {
                return this.BaseRepository().FindList<VoteEntity>(expression);
            }
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <param name="voteId">ͶƱid</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<VoteSubjectEntity> GetSubjectPageList(Pagination pagination, string queryJson, string voteId)
        {
            //ԤͶƱ ������ͶƱ�¼���״̬
            this.SaveVoteSituation(voteId, "", "", null);

            var expression = LinqExtensions.True<VoteSubjectEntity>();
            expression = expression.And(t => t.VoteId.Equals(voteId));

            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Title":            //��Ŀ
                        expression = expression.And(t => t.Title.Contains(keyord));
                        break;
                    case "SubjectType":            //ͶƱ����
                        int subjectType = Int32.Parse(keyord);
                        if (subjectType >= 0)
                        {
                            expression = expression.And(t => t.SubjectType == subjectType);
                        }
                        break;                  
                    default:
                        break;
                }
            }
            //δɾ�� ��ʱû��ɾ��
            //expression = expression.And(t => t.DeleteMark == 0);
            if (pagination != null)
            {
                return this.BaseRepository().FindList<VoteSubjectEntity>(expression, pagination);
            }
            else
            {
                return this.BaseRepository().FindList<VoteSubjectEntity>(expression);
            }
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="voteId">����ֵ</param>
        /// <returns></returns>
        public VoteEntity GetVoteEntity(string voteId)
        {
            //ԤͶƱ ������ͶƱ�¼���״̬
            this.SaveVoteSituation(voteId, "", "", null);
            return this.BaseRepository().FindEntity<VoteEntity>(voteId);
        }
        /// <summary>
        /// ��ȡ��Ŀʵ��
        /// </summary>
        /// <param name="subjectId">����ֵ</param>
        /// <returns></returns>
        public VoteSubjectEntity GetSubjectEntity(string subjectId)
        {
            return this.BaseRepository().FindEntity<VoteSubjectEntity>(subjectId);
        }
        /// <summary>
        /// ��ȡѡ��ʵ��
        /// </summary>
        /// <param name="optionId">����ֵ</param>
        /// <returns></returns>
        public VoteSubjectOptionEntity GetOptionEntity(string optionId)
        {
            return this.BaseRepository().FindEntity<VoteSubjectOptionEntity>(optionId);
        }

        /// <summary>
        /// ��ȡ��Ŀ�б���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectEntity> GetSubjectList(string VoteId)
        {
            return this.BaseRepository().FindList<VoteSubjectEntity>("select * from WeChat_VoteSubject where VoteId='" + VoteId + "' order by SubjectOrder asc");
        }
        /// <summary>
        /// ��ȡѡ����ϸ��Ϣ
        /// </summary>
        /// <param name="VoteId">ͶƱ����</param>
        /// <param name="SubjectId">��Ŀ����</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectOptionEntity> GetOptionList(string VoteId, string SubjectId)
        {
            return this.BaseRepository().FindList<VoteSubjectOptionEntity>("SELECT * FROM WeChat_VoteSubjectOPtion WHERE SubjectId IN ((SELECT id	FROM WeChat_VoteSubject	WHERE VoteId = '" + VoteId + "'	) UNION (SELECT	id	FROM WeChat_VoteSubject	WHERE id = '" + SubjectId + "')) order by OptionOrder asc");
        }

        /// <summary>
        /// ��ȡ˭ͶƱ��
        /// </summary>
        /// <param name="VoteId">ͶƱ����ֵ</param>
        /// <returns></returns>
        public IEnumerable<VoteSituationEntity> GetSituationList(string VoteId)
        {
            return this.BaseRepository().FindList<VoteSituationEntity>("SELECT * FROM WeChat_VoteSituation WHERE VoteId = '" + VoteId + "' order by createDate asc");
        }
        /// <summary>
        /// �ж�ĳ�����Ƿ�͸��ĳ��ͶƱ
        /// </summary>
        /// <param name="voteId"></param>
        /// <param name="userId"></param>
        /// <param name="weChatId"></param>
        /// <returns></returns>
        public bool checkIsVoted(string voteId, string userId, string weChatId)
        {
            return this.BaseRepository().FindList<VoteSituationEntity>("select * from WeChat_VoteSituation where VoteId='" + voteId + "' and (userId='" + userId + "' or WeChatId='" + weChatId + "')").Count() > 0;
        }
        /// <summary>
        /// �ڲ�ʹ�� ��ȡһ��ͶƱ �Ѿ�������Ͷ��
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        private int GetVotedNum(string voteId)
        {
            return this.BaseRepository().FindList<VoteSituationEntity>("select count(*) from WeChat_VoteSituation where VoteId='" + voteId + "' GROUP BY UserId,WeChatId").Count();
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<VoteEntity>(keyValue);
                db.Delete<VoteSubjectEntity>(t => t.VoteId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// ����ͶƱ�����������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveVote(string keyValue, VoteEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //����
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //����
                entity.Create();
                entity.Status = 0;//δ����
                entity.Count = 0;//δ����ͳ��
                entity.RealSendVoteNum = 0;//ʵ�ʷ���Ʊ��
                this.BaseRepository().Insert(entity);
            }
            this.BaseRepository().Commit();

        }
        /// <summary>
        /// �����޸���Ŀ�б�
        /// </summary>
        /// <param name="voteId">ͶƱid</param>
        /// <param name="subjectId">��Ŀid</param>
        /// <param name="subject">��Ŀ�б�</param>
        /// <param name="optionList">ѡ���б�</param>
        public void SaveSubject(string voteId, string subjectId, VoteSubjectEntity subject, List<VoteSubjectOptionEntity> optionList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                int order = 1;
                if (!string.IsNullOrEmpty(subjectId))//�༭
                {
                    subject.Modify(subjectId);
                    db.Update(subject);
                    db.Delete<VoteSubjectOptionEntity>(t => t.SubjectId.Equals(subjectId));//ѡ���

                    foreach (VoteSubjectOptionEntity option in optionList)
                    {
                        option.Create();
                        option.OptionOrder = order++;//ѡ�����
                        option.SubjectId = subject.ID;
                        option.Count = 0;
                        if (option.MaxValue == null)
                        {
                            option.MaxValue = option.Value;
                        }
                        db.Insert(option);
                    }
                }
                else//����
                {
                    IEnumerable<VoteSubjectEntity> iSubjectList = this.GetSubjectList(voteId);
                    foreach (VoteSubjectEntity e in iSubjectList)
                    {//ͳ���Ѿ��м�����Ŀ
                        order++;
                    }
                    subject.Create();
                    subject.VoteId = voteId;
                    subject.SubjectOrder = order;//��Ŀ���
                    subject.Count = 0;
                    subject.Value = 0;//��ĿȨֵ
                    db.Insert(subject);
                    order = 1;//����
                    foreach (VoteSubjectOptionEntity option in optionList)
                    {
                        option.Create();
                        option.OptionOrder = order++;//ѡ�����
                        option.SubjectId = subject.ID;
                        option.Count = 0;
                        if (option.MaxValue == null)
                        {
                            option.MaxValue = option.Value;
                        }
                        db.Insert(option);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// ����ͶƱ���
        /// </summary>
        /// <param name="voteId">ͶƱid</param>
        /// <param name="userId">�û�id userid �����û�΢�ŵ�id</param>
        /// <param name="weChatId">΢��id</param>
        /// <param name="situationList">��Ŀѡ�����</param>
        public string SaveVoteSituation(string voteId, string userId, string weChatId, List<VoteSituationEntity> situationList)
        {
            VoteEntity vote = this.BaseRepository().FindEntity<VoteEntity>(voteId);//������ this.GetEntity(),

            var expression = LinqExtensions.True<VoteSituationEntity>();
            expression = expression.And(t => t.VoteId.Contains(voteId));
            expression = expression.And(t => t.UserId.Equals(userId));
            VoteSituationEntity situation=this.BaseRepository().FindEntity<VoteSituationEntity>(expression);//�ᵽ���棬���ж�ʱ���Ѿ�Ͷ��Ʊ
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //ͶƱδ��ʼ
                if (vote.DateBegin > DateTime.Now)
                {
                    return "ͶƱδ��ʼ";
                }
                //���Ѿ��ﵽͶƱ���� ��ͶƱʱ���Ѿ����� 
                else  if (vote.VoteNum <= this.GetVotedNum(voteId) || vote.DateEnd < DateTime.Now)
                {
                    vote.Status=2;
                    db.Update(vote);
                    db.Commit();
                    return "ͶƱ�ѽ���";
                }
                //���userid �Ѿ�Ͷ��Ʊ��
                else if (situation!=null)
                {
                    return "�Ѿ�ͶƱ";
                }
                else if (!string.IsNullOrEmpty(voteId) && situationList!=null)//ԤͶƱ���������
                {
                    VoteSubjectEntity subject;
                    VoteSubjectOptionEntity option;
                    vote.Count++;//ͶƱ+1
                    db.Update(vote);

                    string[] subjectIdArray =new string[situationList.Count];
                    int i = 0;
                    foreach (VoteSituationEntity situa in situationList)
                    {
                        //��ͳ�Ƽǵ���Ŀ��ѡ����ȥ
                        if (!subjectIdArray.Contains(situa.SubjectId))//��ѡ��� �޳��ظ���subjectid
                        {
                            subjectIdArray[i++] = situa.SubjectId;
                            subject = this.GetSubjectEntity(situa.SubjectId);
                            subject.Count++;
                            db.Update(subject);
                        } 
                        option = this.GetOptionEntity(situa.OptionId);                                               
                        option.Count++;                        
                        db.Update(option);
                    }
                    //�����û�ͶƱ�¼�һ��
                    situation=new VoteSituationEntity();
                    situation.Create();
                    situation.VoteId = voteId;//δ����
                    situation.UserId = userId;
                    situation.WeChatId = weChatId;
                    situation.OptionId = "";
                    situation.SubjectId = "";
                    db.Insert(situation);
                }
                db.Commit();
                return "ͶƱ�ɹ�";
            }
            catch (Exception ex)
            {
                db.Rollback();
                return ex.ToString();
                throw;
            }
        }
        #endregion

        #region ΢�Ŷ˽ӿ�
        /// <summary>
        /// ��ȡ�û��ɼ���ͶƱ�б�
        /// </summary>
        /// <param name="weChatId">΢��id</param>
        /// <param name="page">ҳ��</param>
        /// <param name="pageNum">ÿҳ����</param>
        /// <returns></returns>
        public IEnumerable<VoteEntity> getMyVoteList(string weChatId,int page,int pageNum)
        {
            string sqlStr = "";
            string userStr = "  1=1 ";////��ȡ���� ���� ���Լ��ɼ����� 
            if (weChatId != "")
            {
                userStr += " and (UserScope='1' or UserScope like '%" + weChatId + "%')";
            }
            sqlStr = " select TOP " + pageNum + " * from WeChat_Vote " +
                               " where  id NOT IN ( " +
                                    " SELECT TOP " + (page * pageNum - pageNum) + " id FROM WeChat_Vote " +
                                        " WHERE " + userStr + " and Status!=0 order BY CreateDate DESC " +
                                ") and " +
                                userStr+" and Status!=0 order BY CreateDate DESC";           
            return this.BaseRepository().FindList<VoteEntity>(sqlStr);
        }

        #endregion

    }
}
