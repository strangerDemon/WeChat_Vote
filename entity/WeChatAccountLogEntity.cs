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
    public class WeChatAccountLogEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 无意义id
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 用户在oa中的账号id
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// 微信id，能够唯一确定用户与微信好的关联
        /// </summary>
        /// <returns></returns>
        public string WeChatId { get; set; }
        /// <summary>
        /// 这个账号是谁，方便查看
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 金额变动
        /// </summary>
        /// <returns></returns>
        public decimal? MoneyChange { get; set; }
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
        /// 金额变动的类型 1充值 2消费
        /// </summary>
        /// <returns></returns>
        public int? ChangeType { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 是否退订这次订餐
        /// </summary>
        /// <returns></returns>
        public int? IsUnsubscribe { get; set; }
        /// <summary>
        /// 修改日期：退餐退款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户：退款人
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改人姓名：退款人姓名
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
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
        /// 点餐时创建记录
        /// </summary>
        public void OrderCreate()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        #endregion
    }
}