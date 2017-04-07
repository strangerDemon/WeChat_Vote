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
    public class VoteSubjectOptionEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ѡ��id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// ��Ŀid
        /// </summary>
        /// <returns></returns>
        [Column("SUBJECTID")]
        public string SubjectId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("CONTENT")]
        public string Content { get; set; }
        /// <summary>
        /// ��Ŀ��ͼƬ�����ӣ�http://......
        /// </summary>
        /// <returns></returns>
        [Column("IMGURL")]
        public string ImgUrl { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
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
        /// DeteleMark
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
        /// ѡ�����
        /// </summary>
        /// <returns></returns>
        [Column("OPTIONORDER")]
        public int? OptionOrder { get; set; }
        /// <summary>
        /// ѡ�м���
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public int? Count { get; set; }
        /// <summary>
        /// Ȩֵ(ͶƱ����Ϊ�򵥵Ķ�Ϊ0,ͳ�����͵� ��˫ѡΪ��������ͳ�Ƶ�Ϊ��Сֵ��Ӧ��������ֵ)
        /// </summary>
        /// <returns></returns>
        [Column("VALUE")]
        public int? Value { get; set; }
        /// <summary>
        /// ͳ����Ŀ�����Χ��eg:0<=x<2��
        /// </summary>
        /// <returns></returns>
        [Column("MAXVALUE")]
        public int? MaxValue { get; set; }
        
        
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