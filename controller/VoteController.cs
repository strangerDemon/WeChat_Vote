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
    /// �� �� 2.6
    /// Copyright (c) 2014-2016 
    /// �� ������������Ա
    /// �� �ڣ�2017-03-10 09:47
    /// �� ����΢��ͶƱ��
    /// </summary>
    public class VoteController : MvcControllerBaseOfIgnore
    {
        private VoteBLL votebll = new VoteBLL();
        DataItemCache dataItemCache = new DataItemCache();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteIndex()
        {
            return View();
        }
        /// <summary>
        /// ͶƱҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteForm()
        {
            return View();
        }
        /// <summary>
        /// ��Ŀ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteSubjectIndex()
        {
            return View();
        }
        /// <summary>
        /// ��Ŀҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VoteSubjectForm()
        {
            return View();
        }
        /// <summary>
        /// ͳ��ҳ��
        /// </summary>
        /// <returns></returns>
        public ActionResult VoteCountDetail()
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <param name="voteId">ͶƱid</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡͶƱʵ�� 
        /// </summary>
        /// <param name="VoteId">ͶƱ����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetVoteFormJson(string VoteId)
        {
            var data = votebll.GetVoteEntity(VoteId);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ��Ŀʵ�� 
        /// </summary>
        /// <param name="subjectId">��Ŀid</param>
        /// <returns>���ض���Json</returns>
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
        /// ��ȡͶƱ���� ������Ϣ
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
            //������Ŀ ѡ�� �ֱ�ϲ�
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
                isCheck = errorCode == "" ? votebll.checkIsVoted(VoteId, Code) : errorCode,//�ɹ�=>userid |codeʧЧ=> codeError | ʧ�� =>false| ʱ�����=>timeEnd | �ռ���� =>end  
                entity = data,
                subject = subjectList,
                situation = situationLists,
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// ��ȡ�û��ɼ���ͶƱ�б�
        /// </summary>
        /// <param name="code">�û�</param>
        /// <param name="page">ҳ��</param>
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
        /// ��ȡͶƱ���
        /// </summary>
        /// <param name="VoteId">ͶƱid</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult GetVoteSituationJson(string VoteId)
        {
            return null;
        }
        /// <summary>
        /// ��ȡ��Ŀʵ��  ����ͶƱ��ȡ��Ŀ
        /// </summary>
        /// <param name="VoteId">ͶƱ����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetSubjectListByVoteId(string VoteId)
        {
            var subjectList = votebll.GetSubjectList(VoteId);
            return ToJsonResult(subjectList);
        }
        /// <summary>
        /// ��ȡ��Ŀʵ��  ������Ŀ��ȡѡ��
        /// </summary>
        /// <param name="SubjectId">ͶƱ����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetOptionListBySubjectId(string SubjectId)
        {
            var optionList = votebll.GetOptionList("", SubjectId);
            return ToJsonResult(optionList);
        }

        /// <summary>
        /// �ж�ĳ�����Ƿ�͸��ĳ��ͶƱ
        /// </summary>
        /// <param name="voteId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult checkIsVoted(string voteId, string code)
        {
            return ToJsonResult(votebll.checkIsVoted(voteId, code));
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
            votebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ����ͶƱ�����������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
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
                return Success("�����ɹ���");
            }
            else
            {
                return Error(actionCode);
            }
        }
        /// <summary>
        /// ����ͶƱ��Ŀ�����������޸ģ�
        /// </summary>
        /// <param name="voteId">ͶƱ����</param>
        /// <param name="subjectId">��Ŀ����</param>
        /// <param name="subjectStr">��Ŀ�б�</param>
        /// <param name="optionStrList">ѡ���б�</param>
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
                return Success("�����ɹ���");
            }
            else
            {
                return Error(actionCode);
            }
        }
        /// <summary>
        /// ΢������
        /// </summary>
        /// <param name="voteId">ͶƱid</param>
        /// <returns></returns>
        public ActionResult wxPush(string voteId)
        {
            string actionCode = votebll.wxPush(voteId);
            if (actionCode == "success")
            {
                return Success("�����ɹ���");
            }
            else
            {
                return Error(actionCode);
            }
        }
        /// <summary>
        /// ����ͶƱ���
        /// </summary>
        /// <param name="VoteId">ͶƱid</param>
        /// <param name="Code">�û���Ϣcode ���ٴ�΢��api��ȡ�û���Ϣ</param>
        /// <param name="situationStr">ͶƱ����list</param>
        /// <returns></returns>
        public ActionResult SaveVoteSituation(string VoteId, string Code, string situationStr)
        {
            List<VoteSituationEntity> situationList = situationStr.ToList<VoteSituationEntity>();
            string actionCode = votebll.SaveVoteSituation(VoteId, Code, situationList);
            var situationLists = votebll.GetSituationList(VoteId);
            if (actionCode == "ͶƱ�ɹ�" || actionCode == "�Ѿ�ͶƱ" || actionCode == "ͶƱ�ѽ���")
            {
                var data = votebll.GetVoteEntity(VoteId);
                //������Ŀ ѡ�� �ֱ�ϲ�
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

        #region �ڲ���
        class subject : VoteSubjectEntity
        {
            /// <summary>
            /// ѡ��
            /// </summary>
            public IEnumerable<VoteSubjectOptionEntity> optionList { set; get; }
        }
        #endregion
    }
}
