using System;
using ZoneTop.Application.Code;

namespace ZoneTop.Application.Entity.WeChatManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2014-2016
    /// 创 建：超级管理员
    /// 日 期：2017-03-23 09:03
    /// 描 述：微信关联oa的金额账号
    /// </summary>
    public class WeChatAccountEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户再oa中的账号id
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// 微信id，能够唯一确定用户与微信好的关联
        /// </summary>
        /// <returns></returns>
        public string WeChatId { get; set; }
        /// <summary>
        /// 账户金额
        /// </summary>
        /// <returns></returns>
        public decimal? Money { get; set; }
        /// <summary>
        /// 创建账号时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 这个账号是谁，方便查看
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改者id
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改者姓名
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create(string keyValue)
        {
            this.UserId = keyValue;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">无用</param>
        public override void Modify(string keyValue)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}