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
    public class VoteSituationEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��Ŀid
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// �û�id
        /// </summary>
        /// <returns></returns>
        [Column("USERID")]
        public string UserId { get; set; }
        /// <summary>
        /// ΢��id
        /// </summary>
        /// <returns></returns>
        [Column("WECHATID")]
        public string WeChatId { get; set; }
        /// <summary>
        /// ͶƱid
        /// </summary>
        /// <returns></returns>
        [Column("VOTEID")]
        public string VoteId { get; set; }
        /// <summary>
        /// ��Ŀid
        /// </summary>
        /// <returns></returns>
        [Column("SUBJECTID")]
        public string SubjectId { get; set; }
        /// <summary>
        /// ѡ��id
        /// </summary>
        /// <returns></returns>
        [Column("OPTIONID")]
        public string OptionId { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID= Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
    }
}