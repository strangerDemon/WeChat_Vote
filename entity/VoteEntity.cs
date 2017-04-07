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
    public class VoteEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 投票起始时间
        /// </summary>
        /// <returns></returns>
        [Column("DATEBEGIN")]
        public DateTime? DateBegin { get; set; }
        /// <summary>
        /// 投票终止时间
        /// </summary>
        /// <returns></returns>
        [Column("DATEEND")]
        public DateTime? DateEnd { get; set; }
        /// <summary>
        /// 投票数
        /// </summary>
        /// <returns></returns>
        [Column("VOTENUM")]
        public int? VoteNum { get; set; }
        /// <summary>
        /// 用户范围，1代表所有 用户id的并集，用,隔开
        /// </summary>
        /// <returns></returns>
        [Column("USERSCOPE")]
        public string UserScope { get; set; }
        /// <summary>
        /// 用户范围，1代表所有，用户名的并集，用,隔开
        /// </summary>
        /// <returns></returns>
        [Column("USERSCOPENAME")]
        public string UserScopeName { get; set; }      
        /// <summary>
        /// 投票标题
        /// </summary>
        /// <returns></returns>
        [Column("TITLE")]
        public string Title { get; set; }
        /// <summary>
        /// 描述
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
        /// 投票计数
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public int? Count { get; set; }
        /// <summary>
        /// 投票类型:1:简单(只有单双选)，2统计(多个单双选一个统计)
        /// </summary>
        /// <returns></returns>
        [Column("VOTETYPE")]
        public int? VoteType { get; set; }
        /// <summary>
        /// 实际发放了几个投票
        /// </summary>
        /// <returns></returns>
        [Column("REALSENDVOTENUM")]
        public int? RealSendVoteNum { get; set; }
        
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