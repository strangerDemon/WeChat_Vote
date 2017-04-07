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
    /// �� �� 2.6
    /// Copyright (c) 2014-2016
    /// �� ������������Ա
    /// �� �ڣ�2017-03-10 09:47
    /// �� ����΢��ͶƱ��
    /// </summary>
    public class VoteBLL
    {
        private VoteIService service = new VoteService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<VoteEntity> GetVotePageList(Pagination pagination, string queryJson)
        {
            return service.GetVotePageList(pagination, queryJson);
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
            return service.GetSubjectPageList(pagination, queryJson, voteId);
        }
        /// <summary>
        /// ��ȡͶƱʵ��
        /// </summary>
        /// <param name="voteId">����ֵ</param>
        /// <returns></returns>
        public VoteEntity GetVoteEntity(string voteId)
        {
            return service.GetVoteEntity(voteId);
        }
        /// <summary>
        /// ��ȡ��Ŀʵ��
        /// </summary>
        /// <param name="subjectId">����ֵ</param>
        /// <returns></returns>
        public VoteSubjectEntity GetSubjectEntity(string subjectId)
        {
            return service.GetSubjectEntity(subjectId);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="VoteId">ͶƱ����ֵ</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectEntity> GetSubjectList(string VoteId)
        {
            return service.GetSubjectList(VoteId);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="VoteId">ͶƱ����ֵ</param>
        /// <param name="SubjectId">��Ŀ����ֵ</param>
        /// <returns></returns>
        public IEnumerable<VoteSubjectOptionEntity> GetOptionList(string VoteId, string SubjectId)
        {
            return service.GetOptionList(VoteId, SubjectId);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="VoteId">ͶƱ����ֵ</param>
        /// <returns></returns>
        public IEnumerable<VoteSituationEntity> GetSituationList(string VoteId)
        {
            return service.GetSituationList(VoteId);
        }
        /// <summary>
        /// �ж�ĳ�����Ƿ�͸��ĳ��ͶƱ
        /// </summary>
        /// <param name="voteId"></param>
        /// <param name="code">΢��code���û���ȡuserid=>΢��id ��̨ʹ��ʱΪsystem</param>
        /// <returns></returns>
        public string checkIsVoted(string voteId, string code)
        {
            string userId = "";//�û�id
            string weChatId="";
            userId = weChatId = analysisCode(code);//΢��id
            if (userId == null || userId == "")//code ��ʱ �Ѿ�ʹ�ù�
            {
                return "codeError";
            }
            if (service.checkIsVoted(voteId, userId, weChatId))
            {
                return "true";
            }
            else
            {
                return "false";//�û���Ϣ����ҳ��
            }

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
        /// ����ͶƱ�����������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public string SaveVote(string keyValue, VoteEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue) && service.GetVoteEntity(keyValue).Status == 2)
                {
                    return "�ѽ���ͶƱ,���ܱ༭";
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
        /// ����ͶƱ�����������޸ģ�
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
                    return "�ѽ���ͶƱ,���ܱ༭";
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
        /// ΢������
        /// </summary>
        /// <param name="voteId">ͶƱid</param>
        /// <returns></returns>
        public string wxPush(string voteId)
        {
            try
            {
                VoteEntity vote = service.GetVoteEntity(voteId);
                if (vote.Status == 2)
                {
                    return "�ѽ���ͶƱ,��������";
                }
                //��ȡ��˾��΢�ŶεĲ���id
                IWeChatOrganizeService organizeService = new WeChatOrganizeService();
                WeChatDeptRelationEntity deptEntity = organizeService.GetEntity("e8dafa1d-eefe-4992-8003-fa2ff85ff5fc");
                //���΢�ŵ�����
                int readSendNum = 0;//΢������
                UserSimplelistResult userSimpListRes = new UserSimplelistResult();
                UserSimplelist userSimpList = new UserSimplelist();
                userSimpList.department_id = deptEntity.WeChatDeptId.ToString();//ȫ��˾��΢�Ŷ˵�id
                userSimpList.fetch_child = 1;
                userSimpList.status = ZoneTop.Util.WeChat.Model.Request.UserSimplelist.UserStatus.All;//����
                userSimpListRes = userSimpList.Send();
                SendNews message = new SendNews();
                message.agentid = "7";//����wechat_app��̬��ȡ
                message.touser = vote.UserScope == "1" ? "@all" : vote.UserScope;//�û���Χxx|xxx;Ϊwechatid
          
                foreach (ZoneTop.Util.WeChat.Model.Request.UserSimplelistResult.UserSimplelistItem userItem in userSimpListRes.userlist)
                {
                    //userItem name��wechatId userItem.userid
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
                    appid = "��˾scorpid",
                    redirect_uri = "��˾����url/WeChat/Vote/index.html?voteId=" + voteId,
                    state = "ping"

                };
                item.url = o.GetAuthorizeUrl();
                //item.url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=CORPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";
                ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItemLoist itemList = new SendNews.SendItemLoist();
                itemList.articles = new List<SendNews.SendItem>();
                itemList.articles.Add(item);
                message.news = itemList;
                message.Send();
                //�޸�Ϊ�ռ���
                vote.Status = 1;
                vote.RealSendVoteNum = readSendNum > vote.VoteNum ? vote.VoteNum : readSendNum;//΢����������������ͶƱ������Ϊ���ͶƱ��
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
        /// ����ͶƱ���
        /// </summary>
        /// <param name="voteId">ͶƱid</param>
        /// <param name="userId">�û���Ϣcode ���ٴ�΢��api��ȡ�û���Ϣ</param>
        /// <param name="situationList">��Ŀѡ�����</param>
        public string SaveVoteSituation(string voteId, string userId, List<VoteSituationEntity> situationList)
        {
            try
            {
                //string userId = "";//�û�id
                string weChatId = "";//΢��id
                userId = weChatId = analysisCode(userId);
                if (userId == null || userId == "")//code ��ʱ
                {
                    return "ҳ��ʧЧ��������ˢ��ҳ�棡";
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
        /// ��codeת�����û���Ϣuserid =>΢��id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string analysisCode(string code)
        {          
            if (code == "system") { return "xxx"; }//���Դ����
            string wechatId = WebHelper.GetSession(code);//session�л�ȡwechatid
            if (wechatId == null || wechatId == "")//session��û�У����»�ȡ Code���浽session��
            {
                UserGetuserinfo userInfo = new UserGetuserinfo();
                userInfo.code = code;
                userInfo.agentid = "7";//Ӧ�úţ��̶�
                wechatId = userInfo.Send().UserId;
                WebHelper.WriteSession(code, wechatId);
            }
            return wechatId;
        }
        /// <summary>
        /// ��ȡ�û��ɼ���ͶƱ�б�
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="page">ҳ��</param>
        /// <param name="pageNum">ÿҳ����</param>
        /// <returns></returns>
        public IEnumerable<VoteEntity> getMyVoteList(string code, int page, int pageNum)
        {
            string wechatId=analysisCode(code);
            return service.getMyVoteList(wechatId, page, pageNum);
        }
        #endregion
    }
}
