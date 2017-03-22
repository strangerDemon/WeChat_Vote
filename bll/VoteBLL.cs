using Application.Entity.WeChatManage;
using Application.IService.WeChatManage;
using Application.Service.WeChatManage;
using Util.WebControl;
using System.Collections.Generic;
using System;
using Util.WeChat.Model.Request.SendMessage;
using Util.WeChat.Model.Request;

namespace Application.Busines.WeChatManage
{
    /// <summary>
    /// �� �� 2.6
    /// �� �ڣ�2017-03-10 09:47
    /// �� ����΢��ͶƱ��
    /// </summary>
    public class VoteBLL
    {
        private VoteIService service = new VoteService();

        #region �ύ����
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
                WeChatDeptRelationEntity deptEntity = organizeService.GetEntity("��˾id");
                //���΢�ŵ�����
                int readSendNum = 0;//΢������
                UserSimplelistResult userSimpListRes = new UserSimplelistResult();
                UserSimplelist userSimpList = new UserSimplelist();
                userSimpList.department_id = deptEntity.WeChatDeptId.ToString();//��˾��΢�Ŷ˵�id
                userSimpList.fetch_child = 1;//�ݹ�
                userSimpList.status = Util.WeChat.Model.Request.UserSimplelist.UserStatus.All;//���� 1
                userSimpListRes=userSimpList.Send();
                foreach (Util.WeChat.Model.Request.UserSimplelistResult.UserSimplelistItem userItem in userSimpListRes.userlist)
                {
                    if (vote.UserScope == "1" || (vote.UserScope != "1" && vote.UserScope.IndexOf(userItem.userid) >= 0))
                    {
                        readSendNum++;
                    }                    
                }

                
                SendNews message = new SendNews();
                message.agentid = "7";//����wechat_app��̬��ȡ
                message.touser = vote.UserScope == "1" ? "@all" : vote.UserScope.Replace(",", "|");
                Util.WeChat.Model.Request.SendMessage.SendNews.SendItem item = new SendNews.SendItem();
                item.description = vote.Description == null ? "" : vote.Description;
                item.title = vote.Title == null ? "" : vote.Title;
                //item.picurl = vote. == 1 ? news.PictureUrl : "";
                var o = new Oauth2Authorize()
                {
                    appid = "wechat��ҵ��id",
                    redirect_uri = "http://www.ztgis.com:8867/WeChat/Vote/index.html?voteId=" + voteId,
                    state = "ping"

                };
                item.url = o.GetAuthorizeUrl();                
                Util.WeChat.Model.Request.SendMessage.SendNews.SendItemLoist itemList = new SendNews.SendItemLoist();
                itemList.articles = new List<SendNews.SendItem>();
                itemList.articles.Add(item);
                message.news = itemList;
                message.Send();
                //�޸�Ϊ�ռ���
                vote.Status = 1;
                vote.RealSendVoteNum = readSendNum > vote.VoteNum ? vote.VoteNum : readSendNum;//΢����������������ͶƱ������Ϊ���ͶƱ��
                service.SaveVote(voteId,vote);
                return "success";
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
        }
        
}
