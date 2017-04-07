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
    /// 版 本 2.6
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-10 09:47
    /// 描 述：微信投票表
    /// </summary>
    public class VoteService : RepositoryFactory, VoteIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<VoteEntity> GetVotePageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<VoteEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Title":            //题目
                        expression = expression.And(t => t.Title.Contains(keyord));
                        break;                   
                    case "VoteType":            //投票类型
                        int voteType = Int32.Parse(keyord);
                        if (voteType >= 0)
                        {
                            expression = expression.And(t => t.VoteType == voteType);
                        }
                        break;
                    case "Status":          //状态
                        int status = Int32.Parse(keyord);
                        if (status >= 0)
                        {
                            expression = expression.And(t => t.Status == status);//数字要用==
                        }
                        break;
                    case "UserScope":            //用户范围
                        int userScope = Int32.Parse(keyord);
                        if (userScope == 1)//全公司
                        {
                            expression = expression.And(t => t.UserScope.Equals(keyord));
                        }
                        else if(userScope == 2)//自定义
                        {
                            expression = expression.And(t => t.UserScope.Equals(""));
                        }                
                        break;
                    case "Date":         //申请日期
                        if (!queryParam["DateBegin"].IsEmpty())
                        {
                            DateTime startTime = queryParam["DateBegin"].ToDate();
                            expression = expression.And(t => t.DateBegin >= startTime);//数字要用==
                        }
                        if (!queryParam["DateEnd"].IsEmpty())
                        {
                            DateTime endTime = queryParam["DateEnd"].ToDate().AddDays(1);
                            expression = expression.And(t => t.DateEnd < endTime);//日期
                        }
                        break;
                    
                    default:
                        break;
                }
            }
            //未删除 暂时没有删除
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="voteId">投票id</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<VoteSubjectEntity> GetSubjectPageList(Pagination pagination, string queryJson, string voteId)
        {
            //预投票 ，更新投票事件的状态
            this.SaveVoteSituation(voteId, "", "", null);

            var expression = LinqExtensions.True<VoteSubjectEntity>();
            expression = expression.And(t => t.VoteId.Equals(voteId));

            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Title":            //题目
                        expression = expression.And(t => t.Title.Contains(keyord));
                        break;
                    case "SubjectType":            //投票类型
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
            //未删除 暂时没有删除
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
        /// 获取实体
        /// </summary>
        /// <param name="voteId">主键值</param>
        /// <returns></returns>
        public VoteEntity GetVoteEntity(string voteId)
        {
            //预投票 ，更新投票事件的状态
            this.SaveVoteSituation(voteId, "", "", null);
            return this.BaseRepository().FindEntity<VoteEntity>(voteId);
        }
        /// <summary>
        /// 获取题目实体
        /// </summary>
        /// <param name="subjectId">主键值</param>
        /// <returns></returns>
        public VoteSubjectEntity GetSubjectEntity(string subjectId)
        {
            return this.BaseRepository().FindEntity<VoteSubjectEntity>(subjectId);
        }
        /// <summary>
        /// 获取选项实体
        /// </summary>
        /// <param name="optionId">主键值</param>
        /// <returns></returns>
        public VoteSubjectOptionEntity GetOptionEntity(string optionId)
        {
            return this.BaseRepository().FindEntity<VoteSubjectOptionEntity>(optionId);
        }

        /// <summary>
        /// 获取题目列表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectEntity> GetSubjectList(string VoteId)
        {
            return this.BaseRepository().FindList<VoteSubjectEntity>("select * from WeChat_VoteSubject where VoteId='" + VoteId + "' order by SubjectOrder asc");
        }
        /// <summary>
        /// 获取选项详细信息
        /// </summary>
        /// <param name="VoteId">投票主键</param>
        /// <param name="SubjectId">题目主键</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectOptionEntity> GetOptionList(string VoteId, string SubjectId)
        {
            return this.BaseRepository().FindList<VoteSubjectOptionEntity>("SELECT * FROM WeChat_VoteSubjectOPtion WHERE SubjectId IN ((SELECT id	FROM WeChat_VoteSubject	WHERE VoteId = '" + VoteId + "'	) UNION (SELECT	id	FROM WeChat_VoteSubject	WHERE id = '" + SubjectId + "')) order by OptionOrder asc");
        }

        /// <summary>
        /// 获取谁投票了
        /// </summary>
        /// <param name="VoteId">投票主键值</param>
        /// <returns></returns>
        public IEnumerable<VoteSituationEntity> GetSituationList(string VoteId)
        {
            return this.BaseRepository().FindList<VoteSituationEntity>("SELECT * FROM WeChat_VoteSituation WHERE VoteId = '" + VoteId + "' order by createDate asc");
        }
        /// <summary>
        /// 判断某个人是否透过某次投票
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
        /// 内部使用 获取一次投票 已经几个人投了
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        private int GetVotedNum(string voteId)
        {
            return this.BaseRepository().FindList<VoteSituationEntity>("select count(*) from WeChat_VoteSituation where VoteId='" + voteId + "' GROUP BY UserId,WeChatId").Count();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
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
        /// 保存投票表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveVote(string keyValue, VoteEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //主表
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //主表
                entity.Create();
                entity.Status = 0;//未发布
                entity.Count = 0;//未有人统计
                entity.RealSendVoteNum = 0;//实际发放票数
                this.BaseRepository().Insert(entity);
            }
            this.BaseRepository().Commit();

        }
        /// <summary>
        /// 保存修改题目列表
        /// </summary>
        /// <param name="voteId">投票id</param>
        /// <param name="subjectId">题目id</param>
        /// <param name="subject">题目列表</param>
        /// <param name="optionList">选项列表</param>
        public void SaveSubject(string voteId, string subjectId, VoteSubjectEntity subject, List<VoteSubjectOptionEntity> optionList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                int order = 1;
                if (!string.IsNullOrEmpty(subjectId))//编辑
                {
                    subject.Modify(subjectId);
                    db.Update(subject);
                    db.Delete<VoteSubjectOptionEntity>(t => t.SubjectId.Equals(subjectId));//选项表

                    foreach (VoteSubjectOptionEntity option in optionList)
                    {
                        option.Create();
                        option.OptionOrder = order++;//选项序号
                        option.SubjectId = subject.ID;
                        option.Count = 0;
                        if (option.MaxValue == null)
                        {
                            option.MaxValue = option.Value;
                        }
                        db.Insert(option);
                    }
                }
                else//创建
                {
                    IEnumerable<VoteSubjectEntity> iSubjectList = this.GetSubjectList(voteId);
                    foreach (VoteSubjectEntity e in iSubjectList)
                    {//统计已经有几个题目
                        order++;
                    }
                    subject.Create();
                    subject.VoteId = voteId;
                    subject.SubjectOrder = order;//题目序号
                    subject.Count = 0;
                    subject.Value = 0;//题目权值
                    db.Insert(subject);
                    order = 1;//重置
                    foreach (VoteSubjectOptionEntity option in optionList)
                    {
                        option.Create();
                        option.OptionOrder = order++;//选项序号
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
        /// 保存投票情况
        /// </summary>
        /// <param name="voteId">投票id</param>
        /// <param name="userId">用户id userid 就是用户微信的id</param>
        /// <param name="weChatId">微信id</param>
        /// <param name="situationList">题目选择情况</param>
        public string SaveVoteSituation(string voteId, string userId, string weChatId, List<VoteSituationEntity> situationList)
        {
            VoteEntity vote = this.BaseRepository().FindEntity<VoteEntity>(voteId);//不能用 this.GetEntity(),

            var expression = LinqExtensions.True<VoteSituationEntity>();
            expression = expression.And(t => t.VoteId.Contains(voteId));
            expression = expression.And(t => t.UserId.Equals(userId));
            VoteSituationEntity situation=this.BaseRepository().FindEntity<VoteSituationEntity>(expression);//提到外面，先判断时候已经投过票
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //投票未开始
                if (vote.DateBegin > DateTime.Now)
                {
                    return "投票未开始";
                }
                //当已经达到投票上限 或投票时间已经结束 
                else  if (vote.VoteNum <= this.GetVotedNum(voteId) || vote.DateEnd < DateTime.Now)
                {
                    vote.Status=2;
                    db.Update(vote);
                    db.Commit();
                    return "投票已结束";
                }
                //这个userid 已经投过票了
                else if (situation!=null)
                {
                    return "已经投票";
                }
                else if (!string.IsNullOrEmpty(voteId) && situationList!=null)//预投票可能走这边
                {
                    VoteSubjectEntity subject;
                    VoteSubjectOptionEntity option;
                    vote.Count++;//投票+1
                    db.Update(vote);

                    string[] subjectIdArray =new string[situationList.Count];
                    int i = 0;
                    foreach (VoteSituationEntity situa in situationList)
                    {
                        //将统计记到题目和选项中去
                        if (!subjectIdArray.Contains(situa.SubjectId))//多选情况 剔除重复的subjectid
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
                    //创建用户投票事件一次
                    situation=new VoteSituationEntity();
                    situation.Create();
                    situation.VoteId = voteId;//未发布
                    situation.UserId = userId;
                    situation.WeChatId = weChatId;
                    situation.OptionId = "";
                    situation.SubjectId = "";
                    db.Insert(situation);
                }
                db.Commit();
                return "投票成功";
            }
            catch (Exception ex)
            {
                db.Rollback();
                return ex.ToString();
                throw;
            }
        }
        #endregion

        #region 微信端接口
        /// <summary>
        /// 获取用户可见的投票列表
        /// </summary>
        /// <param name="weChatId">微信id</param>
        /// <param name="page">页码</param>
        /// <param name="pageNum">每页多少</param>
        /// <returns></returns>
        public IEnumerable<VoteEntity> getMyVoteList(string weChatId,int page,int pageNum)
        {
            string sqlStr = "";
            string userStr = "  1=1 ";////获取所有 还是 尽自己可见部分 
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
