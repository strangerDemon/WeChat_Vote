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
    public class VoteSituationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 题目id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        /// <returns></returns>
        [Column("USERID")]
        public string UserId { get; set; }
        /// <summary>
        /// 微信id
        /// </summary>
        /// <returns></returns>
        [Column("WECHATID")]
        public string WeChatId { get; set; }
        /// <summary>
        /// 投票id
        /// </summary>
        /// <returns></returns>
        [Column("VOTEID")]
        public string VoteId { get; set; }
        /// <summary>
        /// 题目id
        /// </summary>
        /// <returns></returns>
        [Column("SUBJECTID")]
        public string SubjectId { get; set; }
        /// <summary>
        /// 选项id
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

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID= Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
    }
}