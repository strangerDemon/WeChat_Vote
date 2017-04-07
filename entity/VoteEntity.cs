using System;
using System.ComponentModel.DataAnnotations.Schema;
using ZoneTop.Application.Code;

namespace ZoneTop.Application.Entity.WeChatManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2014-2016 
    /// �� ������������Ա
    /// �� �ڣ�2017-03-10 09:47
    /// �� ����΢��ͶƱ��
    /// </summary>
    public class VoteEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// ͶƱ��ʼʱ��
        /// </summary>
        /// <returns></returns>
        [Column("DATEBEGIN")]
        public DateTime? DateBegin { get; set; }
        /// <summary>
        /// ͶƱ��ֹʱ��
        /// </summary>
        /// <returns></returns>
        [Column("DATEEND")]
        public DateTime? DateEnd { get; set; }
        /// <summary>
        /// ͶƱ��
        /// </summary>
        /// <returns></returns>
        [Column("VOTENUM")]
        public int? VoteNum { get; set; }
        /// <summary>
        /// �û���Χ��1�������� �û�id�Ĳ�������,����
        /// </summary>
        /// <returns></returns>
        [Column("USERSCOPE")]
        public string UserScope { get; set; }
        /// <summary>
        /// �û���Χ��1�������У��û����Ĳ�������,����
        /// </summary>
        /// <returns></returns>
        [Column("USERSCOPENAME")]
        public string UserScopeName { get; set; }      
        /// <summary>
        /// ͶƱ����
        /// </summary>
        /// <returns></returns>
        [Column("TITLE")]
        public string Title { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// EnabledMark
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// ͶƱ����
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public int? Count { get; set; }
        /// <summary>
        /// ͶƱ����:1:��(ֻ�е�˫ѡ)��2ͳ��(�����˫ѡһ��ͳ��)
        /// </summary>
        /// <returns></returns>
        [Column("VOTETYPE")]
        public int? VoteType { get; set; }
        /// <summary>
        /// ʵ�ʷ����˼���ͶƱ
        /// </summary>
        /// <returns></returns>
        [Column("REALSENDVOTENUM")]
        public int? RealSendVoteNum { get; set; }
        
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}