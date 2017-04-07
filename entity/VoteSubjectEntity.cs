using System;
using System.ComponentModel.DataAnnotations.Schema;
using ZoneTop.Application.Code;

namespace ZoneTop.Application.Entity.WeChatManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-10 09:47
    /// 描 述：微信投票表
    /// </summary>
    public class VoteSubjectEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 题目id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 题目类型，1单选，2 多选，3统计
        /// </summary>
        /// <returns></returns>
        [Column("SUBJECTTYPE")]
        public int? SubjectType { get; set; }
        /// <summary>
        /// 多选最多几个，默认1
        /// </summary>
        /// <returns></returns>
        [Column("MAX")]
        public int? Max { get; set; }
        /// <summary>
        /// 多选最少几个，默认1
        /// </summary>
        /// <returns></returns>
        [Column("MIN")]
        public int? Min { get; set; }
        /// <summary>
        /// 题目中图片的连接，http://......
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
        /// 投票id，用题目去关联投票，题目不可复用
        /// </summary>
        /// <returns></returns>
        [Column("VOTEID")]
        public string VoteId { get; set; }
        /// <summary>
        /// 题目内容
        /// </summary>
        /// <returns></returns>
        [Column("Title")]
        public string Title { get; set; }
        /// <summary>
        /// 题目序号
        /// </summary>
        /// <returns></returns>
        [Column("SUBJECTORDER")]
        public int? SubjectOrder { get; set; }
        /// <summary>
        /// 选中计数
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public int? Count { get; set; }
        /// <summary>
        /// 这道题得了多少分（用于统计投票中的统计题目）其他暂时为0
        /// </summary>
        /// <returns></returns>
        [Column("VALUE")]
        public int? Value { get; set; }
        
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
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