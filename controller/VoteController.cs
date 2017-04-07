using ZoneTop.Application.Entity.WeChatManage;
using ZoneTop.Application.Busines.WeChatManage;
using ZoneTop.Util;
using ZoneTop.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using ZoneTop.Application.Code;
using ZoneTop.Application.Cache;

namespace ZoneTop.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 版 本 2.6
    /// Copyright (c) 2014-2016 
    /// 创 建：超级管理员
    /// 日 期：2017-03-10 09:47
    /// 描 述：微信投票表
    /// </summary>
    public class VoteController : MvcControllerBaseOfIgnore
    {
        private VoteBLL votebll = new VoteBLL();
        DataItemCache dataItemCache = new DataItemCache();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteIndex()
        {
            return View();
        }
        /// <summary>
        /// 投票页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteForm()
        {
            return View();
        }
        /// <summary>
        /// 题目列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteSubjectIndex()
        {
            return View();
        }
        /// <summary>
        /// 题目页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteSubjectForm()
        {
            return View();
        }
        /// <summary>
        /// 统计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult VoteCountDetail()
        {
            return View();
        }
        /// <summary>
        /// 成员表单
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetVotePageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = votebll.GetVotePageList(pagination, queryJson);
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="voteId">投票id</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetSubjectPageListJson(Pagination pagination, string queryJson, string voteId)
        {
            var watch = CommonHelper.TimerStart();
            var data = votebll.GetSubjectPageList(pagination, queryJson, voteId);
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
        /// 获取投票实体 
        /// </summary>
        /// <param name="VoteId">投票主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetVoteFormJson(string VoteId)
        {
            var data = votebll.GetVoteEntity(VoteId);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取题目实体 
        /// </summary>
        /// <param name="subjectId">题目id</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetSubjectFormJson(string subjectId)
        {
            VoteSubjectEntity entity = votebll.GetSubjectEntity(subjectId);

            subject sub = new subject();
            sub.ID = entity.ID;
            sub.Title = entity.Title;
            sub.Description = entity.Description;
            sub.Max = entity.Max;
            sub.Min = entity.Min;
            sub.ImgUrl = entity.ImgUrl;
            sub.VoteId = entity.VoteId;
            sub.SubjectOrder = entity.SubjectOrder;
            sub.SubjectType = entity.SubjectType;
            sub.Count = entity.Count;
            sub.Value = entity.Value;
            sub.optionList = votebll.GetOptionList("", entity.ID);

            return ToJsonResult(sub);
        }
        /// <summary>
        /// 获取投票详情 所有信息
        /// </summary>
        /// <param name="VoteId"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult GetVoteJson(string VoteId, string Code)
        {
            var data = votebll.GetVoteEntity(VoteId);
            var situationLists = votebll.GetSituationList(VoteId);
            //处理题目 选项 分表合并
            var subjectData = votebll.GetSubjectList(VoteId);
            var subjectList = new List<subject>();
            subject sub;
            foreach (VoteSubjectEntity entity in subjectData)
            {
                sub = new subject();
                sub.ID = entity.ID;
                sub.Title = entity.Title;
                sub.Description = entity.Description;
                sub.Max = entity.Max;
                sub.Min = entity.Min;
                sub.ImgUrl = entity.ImgUrl;
                sub.VoteId = entity.VoteId;
                sub.SubjectOrder = entity.SubjectOrder;
                sub.SubjectType = entity.SubjectType;
                sub.Count = entity.Count;
                sub.Value = entity.Value;
                sub.optionList = votebll.GetOptionList("", entity.ID);
                subjectList.Add(sub);
            }
            var errorCode = "";
            var count = 0;
            if (data == null)
            {
                errorCode = "notFound";
            }else if (data.DateEnd < System.DateTime.Now)
            {
                errorCode = "timeEnd";
            }else
            {
                foreach (VoteSituationEntity situation in situationLists)
                {
                    count++;
                }
                if (data.VoteNum <= count)
                {
                    errorCode = "end";
                }
            }
            var jsonData = new
            {
                isCheck = errorCode == "" ? votebll.checkIsVoted(VoteId, Code) : errorCode,//成功=>userid |code失效=> codeError | 失败 =>false| 时间结束=>timeEnd | 收集完毕 =>end  
                entity = data,
                subject = subjectList,
                situation = situationLists,
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取用户可见的投票列表
        /// </summary>
        /// <param name="code">用户</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult getMyVoteList(string code, int page)
        {
            string pageNum = "10";
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> item = dataItemCache.GetDataItemList("VoteLog");
            foreach (Entity.SystemManage.ViewModel.DataItemModel i in item)
            {
                pageNum = i.ItemValue;
            }
            var voteList = votebll.getMyVoteList(code, page, int.Parse(pageNum));
            var jsonData = new
            {
                voteList = voteList,
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取投票情况
        /// </summary>
        /// <param name="VoteId">投票id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult GetVoteSituationJson(string VoteId)
        {
            return null;
        }
        /// <summary>
        /// 获取题目实体  根据投票获取题目
        /// </summary>
        /// <param name="VoteId">投票主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetSubjectListByVoteId(string VoteId)
        {
            var subjectList = votebll.GetSubjectList(VoteId);
            return ToJsonResult(subjectList);
        }
        /// <summary>
        /// 获取题目实体  根据题目获取选项
        /// </summary>
        /// <param name="SubjectId">投票主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetOptionListBySubjectId(string SubjectId)
        {
            var optionList = votebll.GetOptionList("", SubjectId);
            return ToJsonResult(optionList);
        }

        /// <summary>
        /// 判断某个人是否透过某次投票
        /// </summary>
        /// <param name="voteId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult checkIsVoted(string voteId, string code)
        {
            return ToJsonResult(votebll.checkIsVoted(voteId, code));
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            votebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存投票表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveVote(string keyValue, string strEntity)
        {
            var entity = strEntity.ToObject<VoteEntity>();
            string actionCode = votebll.SaveVote(keyValue, entity);
            if (actionCode == "success")
            {
                return Success("操作成功。");
            }
            else
            {
                return Error(actionCode);
            }
        }
        /// <summary>
        /// 保存投票题目表单（新增、修改）
        /// </summary>
        /// <param name="voteId">投票主键</param>
        /// <param name="subjectId">题目主键</param>
        /// <param name="subjectStr">题目列表</param>
        /// <param name="optionStrList">选项列表</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveVoteSubject(string voteId, string subjectId, string subjectStr, string optionStrList)
        {
            var subject = subjectStr.ToObject<VoteSubjectEntity>();
            var optionList = optionStrList.ToList<VoteSubjectOptionEntity>();
            string actionCode = votebll.SaveSubject(voteId, subjectId, subject, optionList);
            if (actionCode == "success")
            {
                return Success("操作成功。");
            }
            else
            {
                return Error(actionCode);
            }
        }
        /// <summary>
        /// 微信推送
        /// </summary>
        /// <param name="voteId">投票id</param>
        /// <returns></returns>
        public ActionResult wxPush(string voteId)
        {
            string actionCode = votebll.wxPush(voteId);
            if (actionCode == "success")
            {
                return Success("操作成功。");
            }
            else
            {
                return Error(actionCode);
            }
        }
        /// <summary>
        /// 保存投票情况
        /// </summary>
        /// <param name="VoteId">投票id</param>
        /// <param name="Code">用户信息code ，再从微信api获取用户信息</param>
        /// <param name="situationStr">投票详情list</param>
        /// <returns></returns>
        public ActionResult SaveVoteSituation(string VoteId, string Code, string situationStr)
        {
            List<VoteSituationEntity> situationList = situationStr.ToList<VoteSituationEntity>();
            string actionCode = votebll.SaveVoteSituation(VoteId, Code, situationList);
            var situationLists = votebll.GetSituationList(VoteId);
            if (actionCode == "投票成功" || actionCode == "已经投票" || actionCode == "投票已结束")
            {
                var data = votebll.GetVoteEntity(VoteId);
                //处理题目 选项 分表合并
                var subjectData = votebll.GetSubjectList(VoteId);
                var subjectList = new List<subject>();
                subject sub;
                foreach (VoteSubjectEntity entity in subjectData)
                {
                    sub = new subject();
                    sub.ID = entity.ID;
                    sub.Title = entity.Title;
                    sub.Description = entity.Description;
                    sub.Max = entity.Max;
                    sub.Min = entity.Min;
                    sub.ImgUrl = entity.ImgUrl;
                    sub.VoteId = entity.VoteId;
                    sub.SubjectOrder = entity.SubjectOrder;
                    sub.SubjectType = entity.SubjectType;
                    sub.Count = entity.Count;
                    sub.optionList = votebll.GetOptionList("", entity.ID);
                    subjectList.Add(sub);
                }
                var jsonData = new
                {
                    commitCode = actionCode,
                    entity = data,
                    subject = subjectList,
                    situation = situationLists,
                };
                return ToJsonResult(jsonData);
            }
            else
            {
                var jsonData = new
                {
                    commitCode = actionCode,
                };
                return ToJsonResult(jsonData);
            }
        }
        #endregion

        #region 内部类
        class subject : VoteSubjectEntity
        {
            /// <summary>
            /// 选项
            /// </summary>
            public IEnumerable<VoteSubjectOptionEntity> optionList { set; get; }
        }
        #endregion
    }
}
