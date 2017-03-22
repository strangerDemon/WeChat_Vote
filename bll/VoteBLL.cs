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
    /// 版 本 2.6
    /// 日 期：2017-03-10 09:47
    /// 描 述：微信投票表
    /// </summary>
    public class VoteBLL
    {
        private VoteIService service = new VoteService();

        #region 提交数据
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
                WeChatDeptRelationEntity deptEntity = organizeService.GetEntity("公司id");
                //获得微信的人数
                int readSendNum = 0;//微信人数
                UserSimplelistResult userSimpListRes = new UserSimplelistResult();
                UserSimplelist userSimpList = new UserSimplelist();
                userSimpList.department_id = deptEntity.WeChatDeptId.ToString();//公司在微信端的id
                userSimpList.fetch_child = 1;//递归
                userSimpList.status = Util.WeChat.Model.Request.UserSimplelist.UserStatus.All;//所有 1
                userSimpListRes=userSimpList.Send();
                foreach (Util.WeChat.Model.Request.UserSimplelistResult.UserSimplelistItem userItem in userSimpListRes.userlist)
                {
                    if (vote.UserScope == "1" || (vote.UserScope != "1" && vote.UserScope.IndexOf(userItem.userid) >= 0))
                    {
                        readSendNum++;
                    }                    
                }

                
                SendNews message = new SendNews();
                message.agentid = "7";//后用wechat_app表动态获取
                message.touser = vote.UserScope == "1" ? "@all" : vote.UserScope.Replace(",", "|");
                Util.WeChat.Model.Request.SendMessage.SendNews.SendItem item = new SendNews.SendItem();
                item.description = vote.Description == null ? "" : vote.Description;
                item.title = vote.Title == null ? "" : vote.Title;
                //item.picurl = vote. == 1 ? news.PictureUrl : "";
                var o = new Oauth2Authorize()
                {
                    appid = "wechat企业号id",
                    redirect_uri = "http://www.ztgis.com:8867/WeChat/Vote/index.html?voteId=" + voteId,
                    state = "ping"

                };
                item.url = o.GetAuthorizeUrl();                
                Util.WeChat.Model.Request.SendMessage.SendNews.SendItemLoist itemList = new SendNews.SendItemLoist();
                itemList.articles = new List<SendNews.SendItem>();
                itemList.articles.Add(item);
                message.news = itemList;
                message.Send();
                //修改为收集中
                vote.Status = 1;
                vote.RealSendVoteNum = readSendNum > vote.VoteNum ? vote.VoteNum : readSendNum;//微信人数如果多余最大投票数，则为最大投票数
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
