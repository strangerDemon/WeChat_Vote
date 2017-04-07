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
using ZoneTop.Util;
namespace ZoneTop.Application.Busines.WeChatManage
{
    /// <summary>
    /// 版 本 2.6
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-10 09:47
    /// 描 述：微信投票表
    /// </summary>
    public class VoteBLL
    {
        private VoteIService service = new VoteService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<VoteEntity> GetVotePageList(Pagination pagination, string queryJson)
        {
            return service.GetVotePageList(pagination, queryJson);
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
            return service.GetSubjectPageList(pagination, queryJson, voteId);
        }
        /// <summary>
        /// 获取投票实体
        /// </summary>
        /// <param name="voteId">主键值</param>
        /// <returns></returns>
        public VoteEntity GetVoteEntity(string voteId)
        {
            return service.GetVoteEntity(voteId);
        }
        /// <summary>
        /// 获取题目实体
        /// </summary>
        /// <param name="subjectId">主键值</param>
        /// <returns></returns>
        public VoteSubjectEntity GetSubjectEntity(string subjectId)
        {
            return service.GetSubjectEntity(subjectId);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="VoteId">投票主键值</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectEntity> GetSubjectList(string VoteId)
        {
            return service.GetSubjectList(VoteId);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="VoteId">投票主键值</param>
        /// <param name="SubjectId">题目主键值</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectOptionEntity> GetOptionList(string VoteId, string SubjectId)
        {
            return service.GetOptionList(VoteId, SubjectId);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="VoteId">投票主键值</param>
        /// <returns></returns>
        public IEnumerable<VoteSituationEntity> GetSituationList(string VoteId)
        {
            return service.GetSituationList(VoteId);
        }
        /// <summary>
        /// 判断某个人是否透过某次投票
        /// </summary>
        /// <param name="voteId"></param>
        /// <param name="code">微信code，用户获取userid=>微信id 后台使用时为system</param>
        /// <returns></returns>
        public string checkIsVoted(string voteId, string code)
        {
            string userId = "";//用户id
            string weChatId="";
            userId = weChatId = analysisCode(code);//微信id
            if (userId == null || userId == "")//code 超时 已经使用过
            {
                return "codeError";
            }
            if (service.checkIsVoted(voteId, userId, weChatId))
            {
                return "true";
            }
            else
            {
                return "false";//用户信息传回页面
            }

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
        /// 保存投票表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveVote(string keyValue, VoteEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue) && service.GetVoteEntity(keyValue).Status == 2)
                {
                    return "已结束投票,不能编辑";
                }
                service.SaveVote(keyValue, entity);
                return "success";
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
        }
        /// <summary>
        /// 保存投票表单（新增、修改）
        /// </summary>
        /// <param name="voteId"></param>
        /// <param name="subjectId"></param>
        /// <param name="subject"></param>
        /// <param name="optionList"></param>
        public string SaveSubject(string voteId, string subjectId, VoteSubjectEntity subject, List<VoteSubjectOptionEntity> optionList)
        {
            try
            {
                VoteEntity vote = service.GetVoteEntity(voteId);
                if (vote.Status == 2)
                {
                    return "已结束投票,不能编辑";
                }
                service.SaveSubject(voteId, subjectId, subject, optionList);
                return "success";
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
        }
        /// <summary>
        /// 微信推送
        /// </summary>
        /// <param name="voteId">投票id</param>
        /// <returns></returns>
        public string wxPush(string voteId)
        {
            try
            {
                VoteEntity vote = service.GetVoteEntity(voteId);
                if (vote.Status == 2)
                {
                    return "已结束投票,不能推送";
                }
                //获取公司在微信段的部门id
                IWeChatOrganizeService organizeService = new WeChatOrganizeService();
                WeChatDeptRelationEntity deptEntity = organizeService.GetEntity("e8dafa1d-eefe-4992-8003-fa2ff85ff5fc");
                //获得微信的人数
                int readSendNum = 0;//微信人数
                UserSimplelistResult userSimpListRes = new UserSimplelistResult();
                UserSimplelist userSimpList = new UserSimplelist();
                userSimpList.department_id = deptEntity.WeChatDeptId.ToString();//全公司在微信端的id
                userSimpList.fetch_child = 1;
                userSimpList.status = ZoneTop.Util.WeChat.Model.Request.UserSimplelist.UserStatus.All;//所有
                userSimpListRes = userSimpList.Send();
                SendNews message = new SendNews();
                message.agentid = "7";//后用wechat_app表动态获取
                message.touser = vote.UserScope == "1" ? "@all" : vote.UserScope;//用户范围xx|xxx;为wechatid
          
                foreach (ZoneTop.Util.WeChat.Model.Request.UserSimplelistResult.UserSimplelistItem userItem in userSimpListRes.userlist)
                {
                    //userItem name和wechatId userItem.userid
                    if (vote.UserScope == "1" || (vote.UserScope != "1" && vote.UserScope.IndexOf(userItem.userid) >= 0))
                    {
                        readSendNum++;
                    }
                }
                ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItem item = new SendNews.SendItem();
                item.description = vote.Description == null ? "" : vote.Description;
                item.title = vote.Title == null ? "" : vote.Title;
                //item.picurl = vote. == 1 ? news.PictureUrl : "";
                var o = new Oauth2Authorize()
                {
                    appid = "公司scorpid",
                    redirect_uri = "公司部署url/WeChat/Vote/index.html?voteId=" + voteId,
                    state = "ping"

                };
                item.url = o.GetAuthorizeUrl();
                //item.url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=CORPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";
                ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItemLoist itemList = new SendNews.SendItemLoist();
                itemList.articles = new List<SendNews.SendItem>();
                itemList.articles.Add(item);
                message.news = itemList;
                message.Send();
                //修改为收集中
                vote.Status = 1;
                vote.RealSendVoteNum = readSendNum > vote.VoteNum ? vote.VoteNum : readSendNum;//微信人数如果多余最大投票数，则为最大投票数
                service.SaveVote(voteId, vote);
                return "success";
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
        }
        /// <summary>
        /// 保存投票情况
        /// </summary>
        /// <param name="voteId">投票id</param>
        /// <param name="userId">用户信息code ，再从微信api获取用户信息</param>
        /// <param name="situationList">题目选择情况</param>
        public string SaveVoteSituation(string voteId, string userId, List<VoteSituationEntity> situationList)
        {
            try
            {
                //string userId = "";//用户id
                string weChatId = "";//微信id
                userId = weChatId = analysisCode(userId);
                if (userId == null || userId == "")//code 超时
                {
                    return "页面失效，请重新刷新页面！";
                }
                return service.SaveVoteSituation(voteId, userId, weChatId, situationList);
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }
        /// <summary>
        /// 将code转换成用户信息userid =>微信id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string analysisCode(string code)
        {          
            if (code == "system") { return "xxx"; }//测试代码段
            string wechatId = WebHelper.GetSession(code);//session中获取wechatid
            if (wechatId == null || wechatId == "")//session中没有，重新获取 Code保存到session中
            {
                UserGetuserinfo userInfo = new UserGetuserinfo();
                userInfo.code = code;
                userInfo.agentid = "7";//应用号，固定
                wechatId = userInfo.Send().UserId;
                WebHelper.WriteSession(code, wechatId);
            }
            return wechatId;
        }
        /// <summary>
        /// 获取用户可见的投票列表
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="page">页码</param>
        /// <param name="pageNum">每页多少</param>
        /// <returns></returns>
        public IEnumerable<VoteEntity> getMyVoteList(string code, int page, int pageNum)
        {
            string wechatId=analysisCode(code);
            return service.getMyVoteList(wechatId, page, pageNum);
        }
        #endregion
    }
}
