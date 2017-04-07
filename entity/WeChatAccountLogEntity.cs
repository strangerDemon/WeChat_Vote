using System;
using ZoneTop.Application.Code;

namespace ZoneTop.Application.Entity.WeChatManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2014-2016
    /// �� ������������Ա
    /// �� �ڣ�2017-03-23 09:03
    /// �� ����΢�Ź���oa�Ľ���˺�
    /// </summary>
    public class WeChatAccountLogEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ������id
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// �û���oa�е��˺�id
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// ΢��id���ܹ�Ψһȷ���û���΢�źõĹ���
        /// </summary>
        /// <returns></returns>
        public string WeChatId { get; set; }
        /// <summary>
        /// ����˺���˭������鿴
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// ���䶯
        /// </summary>
        /// <returns></returns>
        public decimal? MoneyChange { get; set; }
        /// <summary>
        /// �����˺�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ������id
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ���䶯������ 1��ֵ 2����
        /// </summary>
        /// <returns></returns>
        public int? ChangeType { get; set; }
        /// <summary>
        /// ��ϸ
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// �Ƿ��˶���ζ���
        /// </summary>
        /// <returns></returns>
        public int? IsUnsubscribe { get; set; }
        /// <summary>
        /// �޸����ڣ��˲��˿�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û����˿���
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸����������˿�������
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
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
        /// ���ʱ������¼
        /// </summary>
        public void OrderCreate()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        #endregion
    }
}