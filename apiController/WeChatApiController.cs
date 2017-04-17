using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoneTop.Util.WeChat.Model;
using ZoneTop.Util.WeChat.Model.Request;
using ZoneTop.Util.WeChat.Model.Request.SendMessage;
using ZoneTop.Util;
using ZoneTop.Util.WeChat.Helper;
using ZoneTop.Application.Code;
using ZoneTop.SOA.SSO;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using ZoneTop.Application.Busines.WeChatManage;
using ZoneTop.Application.Entity.WeChatManage;
using System.Xml;
using ZoneTop.Application.Cache;
using ZoneTop.Application.Busines.SystemManage;
using ZoneTop.Application.Entity.SystemManage;

namespace ZoneTop.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 微信app 验证
    /// </summary>
    public class WeChatApiController : ApiControllerBase
    {
        //公众平台上开发者设置的token, corpID, EncodingAESKey
        private string sToken = "公司token";
        private string sCorpID = "公司scorpid";
        private string sEncodingAESKey = "应用秘钥";
        //字典
        DataItemCache dataItemCache = new DataItemCache();
        DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        //是否点餐 控制点餐的开关
        Entity.SystemManage.ViewModel.DataItemModel dataItemModel;
        DataItemDetailEntity orderItem;
        /// <summary>
        /// 点餐的action 操作 账户余额，订餐记录， 充值记录，
        /// </summary>
        /// <param name="wechatId"></param>
        /// <param name="AgentID"></param>
        /// <param name="type">类型</param>
        public void orderAction(string wechatId, string AgentID, int type)
        {
            RecordExceptionToFile("---------------------myAtion------------------------");
            RecordExceptionToFile("wechatId:" + wechatId + " AgentID:" + AgentID+" type:"+type);
            if (wechatId != null && wechatId != "" && AgentID != null && AgentID != "")
            {
                AccountBLL bll = new AccountBLL();//
                WeChatAccountEntity entity = bll.GetAccountEntityByWechatId(wechatId);
                MessageSendResult result = null;
                IEnumerable<WeChatAccountLogEntity> logEntityList;
                SendNews message = new SendNews();
                message.agentid = AgentID;
                message.touser = wechatId;
                ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItem item = new SendNews.SendItem();
                ZoneTop.Util.WeChat.Model.Request.SendMessage.SendNews.SendItemLoist itemList = new SendNews.SendItemLoist();
                itemList.articles = new List<SendNews.SendItem>();
                if (entity == null)//没有点餐账号
                {
                    item.description = "您还未有订餐账户！请联系OA管理员添加！";
                    item.title = "账户不存在";
                    itemList.articles.Add(item);
                    message.news = itemList;
                    message.Send();
                    return;
                }
               
                string pageNum = "0";
                int count = 0;
                pageNum = GetDataItemModel("OrderLog") == null ? "0" : GetDataItemModel("OrderLog").ItemValue;
                switch (type)
                {
                    case 1://账户余额
                        RecordExceptionToFile("我的账户余额：");
                        RecordExceptionToFile("money:" + entity.Money);
                        item.description ="账户余额： "+ entity.Money.ToString() + "元";
                        item.title = entity.UserName;
                        itemList.articles.Add(item);
                        break;
                    case 2:
                        RecordExceptionToFile("我的充值记录：" + "条数：" +int.Parse(pageNum));
                        logEntityList = bll.WeChatOrderLog(wechatId, "1",1,int.Parse(pageNum));
                        if (logEntityList.Count() == 0)
                        {
                            item.description = "未找到最近的充值记录";
                        }
                        else
                        {
                            item.description = ((DateTime)logEntityList.First().CreateDate).ToString("yyyy年MM月dd日 HH:mm:ss") + "   充值金额：" + logEntityList.First().MoneyChange;
                        }
                        RecordExceptionToFile("描述：" + item.description);
                        item.title = "充值记录";
                        item.url= new Oauth2Authorize()
                                    {
                                        appid = "应用的id",
                                        redirect_uri = "公司url/WeChat/Order/orderList.html?type=1",
                                        state = "ping"

                                    }.GetAuthorizeUrl();
                        itemList.articles.Add(item);
                        break;
                    case 3:
                        RecordExceptionToFile("我的消费记录：" + " 条数：" + int.Parse(pageNum));
                        logEntityList = bll.WeChatOrderLog(wechatId, "2",1,int.Parse(pageNum));
                        if (logEntityList.Count() == 0)
                        {
                            item.description = "未找到最近的消费记录";
                        }
                        else
                        {
                            item.description = ((DateTime)logEntityList.First().CreateDate).ToString("yyyy年MM月dd日 HH:mm:ss") + "   消费金额：" + logEntityList.First().MoneyChange;
                        }
                        RecordExceptionToFile("描述：" + item.description);
                        item.title = "消费记录";
                        item.url=new Oauth2Authorize()
                        {
                            appid = "应用的id",
                            redirect_uri = "公司url/WeChat/Order/orderList.html?type=2",
                            state = "ping"

                        }.GetAuthorizeUrl();
                        itemList.articles.Add(item);
                        break;
                    case 4:
                        break;
                    case 5:
                        RecordExceptionToFile("管理员获取今日点餐情况：");
                        logEntityList = bll.WeChatOrderLog("", "2", 0, 0);//bll.GetAccountLogList(null, "all", null, queryJson);这个是今天所有的，包括充钱
                        RecordExceptionToFile("总条数：" + logEntityList.Count());
                        count = 0;
                        foreach (WeChatAccountLogEntity logEntity in logEntityList)
                        {
                            if (logEntity.IsUnsubscribe == 0)//今日有效的点餐记录
                            {
                                count++;
                            }
                        }
                        item.description = "今日点餐人数：" + count+" 人";
                        RecordExceptionToFile("今日点餐人数：" + count + " 人");
                        item.title = "今日点餐情况";
                        item.url= new Oauth2Authorize()
                                    {
                                        appid = "应用的id",
                                        redirect_uri = "公司url/WeChat/Order/todayOrderList.html?type=2",
                                        state = "ping"

                                    }.GetAuthorizeUrl();
                        itemList.articles.Add(item);
                        break;
                    case 6:
                        RecordExceptionToFile("管理员获取今日账户变动情况：");
                        string queryJson = "{\"condition\":\"Date\",\"keyword\":\"Date\",\"minDate\":\"" + System.DateTime.Today.ToString() + "\",\"maxDate\":\"" + System.DateTime.Today.AddDays(1).ToString() + "\"}";
                        RecordExceptionToFile("queryJson：" + queryJson);
                        logEntityList = bll.GetAccountLogList(null, "all", null, queryJson,"alltoday");//这个是今天所有的，包括充钱
                        RecordExceptionToFile("总条数：" + logEntityList.Count());
                        count = 0;
                        foreach (WeChatAccountLogEntity logEntity in logEntityList)
                        {
                            if (logEntity.IsUnsubscribe == 0)//今日有效的点餐记录
                            {
                                count++;
                            }
                        }
                        item.description = "今日账户变动情况：" + count + " 人次";
                        RecordExceptionToFile("今日账户变动情况：" + count + " 人次");
                        item.title = "今日账户变动情况";
                        item.url = new Oauth2Authorize()
                        {
                            appid = "应用的id",
                            redirect_uri = "公司url/WeChat/Order/todayOrderList.html?type=alltoday",
                            state = "ping"

                        }.GetAuthorizeUrl();
                        itemList.articles.Add(item);
                        break;
                    case 7://关闭点餐
                        //IsWeChatOrderList=dataItemCache.GetDataItemList("IsWeChatOrder");//是否可以点餐
                        //orderItem =new DataItemDetailEntity();
                        //foreach (Entity.SystemManage.ViewModel.DataItemModel i in IsWeChatOrderList)
                        //{
                        //    orderItem=dataItemDetailBLL.GetEntity(i.ItemDetailId);
                        //    orderItem.ItemValue = "false";
                        //}
                        RecordExceptionToFile("关闭点餐功能");
                        try
                        {
                            dataItemModel = GetDataItemModel("IsWeChatOrder");
                            orderItem = new DataItemDetailEntity();
                            orderItem = dataItemDetailBLL.GetEntity(dataItemModel.ItemDetailId);
                            orderItem.ItemValue = "false";
                            dataItemDetailBLL.WeChatUpdateItemDetail(orderItem.ItemDetailId, wechatId,orderItem);
                            item.description = "关闭点餐功能";
                            item.title = "关闭点餐功能：成功";
                        }
                        catch (Exception ex)
                        {
                            RecordExceptionToFile("关闭点餐功能：失败 " + ex.ToString());
                            item.description = "失败：" + ex.ToString();
                            item.title = "关闭点餐功能：失败";
                        }
                        itemList.articles.Add(item);
                        break;
                    case 8://开启点餐
                        RecordExceptionToFile("打开点餐功能");
                        try
                        {
                            dataItemModel = GetDataItemModel("IsWeChatOrder");
                            orderItem = new DataItemDetailEntity();
                            orderItem = dataItemDetailBLL.GetEntity(dataItemModel.ItemDetailId);
                            orderItem.ItemValue = "true";
                            dataItemDetailBLL.WeChatUpdateItemDetail(orderItem.ItemDetailId,wechatId, orderItem);
                            item.description = "打开点餐功能";
                            item.title = "打开点餐功能:成功";
                        }catch(Exception ex){
                            RecordExceptionToFile("打开点餐功能：失败 " + ex.ToString());
                            item.description = "失败："+ex.ToString();
                            item.title = "打开点餐功能：失败";
                        }
                        itemList.articles.Add(item);
                        break;
                    default:
                        break;
                }
                message.news = itemList;
                lock (this)
                {
                    if (result == null)
                    {
                        result = message.Send();
                    }
                }
            }
        }

        /// <summary>
        /// 投票的action 获取可以得到的投票列表
        /// </summary>
        /// <param name="wechatId"></param>
        /// <param name="AgentID"></param>
        /// <param name="voteID"></param>
        public void voteAction(string wechatId, string AgentID, int voteID)
        {

        }

        /// <summary>
        /// 投票的action 获取可以得到的投票列表
        /// </summary>
        /// <param name="wechatId"></param>
        /// <param name="AgentID"></param>
        /// <param name="voteID"></param>
        public void leaveAction(string wechatId, string AgentID, int voteID)
        {

        }
        ///<summary>
        ///接口验证
        ///</summary>
        ///<returns></returns>
        [HttpGet]
        public void checkAppEchostr()
        {
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            //string url = Request.RequestUri.Query.ToString();
            //url=url.Substring(1,url.Length-1);
            string msg_signature = HttpContext.Current.Request.QueryString["msg_signature"]; //ParseUrl(url, "msg_signature");
            string timestamp = timestamp = HttpContext.Current.Request.QueryString["timestamp"]; //ParseUrl(url, "timestamp");
            string nonce = HttpContext.Current.Request.QueryString["nonce"];// ParseUrl(url, "nonce");
            string echostr = HttpContext.Current.Request.QueryString["echostr"];// ParseUrl(url, "echostr"); //HttpUtils.ParseUrl("echostr");
            RecordExceptionToFile("---------------------------------------------");
            RecordExceptionToFile("msg_signature:" + msg_signature);
            RecordExceptionToFile("timestamp:" + timestamp);
            RecordExceptionToFile("nonce:" + nonce);
            RecordExceptionToFile("echostr:" + echostr);
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(msg_signature, timestamp, nonce, echostr, ref sEchoStr);
            RecordExceptionToFile("ret:" + ret);
            RecordExceptionToFile("sEchoStr:" + sEchoStr);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
                //return Error("ERR: VerifyURL fail, ret: " + ret);
                HttpContext.Current.Response.Write("ERR: VerifyURL fail, ret: " + ret);
            }
            else
            {
                //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业号。
                HttpContext.Current.Response.Write(sEchoStr);
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 处理请求  点击click不是在app测试的接口OrderMenuEventHandle处理，而是在HandleRequest
        /// 当时url跳转事件时，不进入
        /// HandleRequest 文本信息和点击事件
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            #region 用户请求数据处理
            RecordExceptionToFile("-------------------HandleRequest--------------------------");
            //RecordExceptionToFile("RequestUri:" + Request.RequestUri);
            //RecordExceptionToFile("Headers:" + Request.Headers);
            //验证标签属性
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            string msg_signature = HttpContext.Current.Request.QueryString["msg_signature"]; //
            string timestamp = timestamp = HttpContext.Current.Request.QueryString["timestamp"]; //
            string nonce = HttpContext.Current.Request.QueryString["nonce"];//
            RecordExceptionToFile("msg_signature:" + msg_signature);
            RecordExceptionToFile("timestamp:" + timestamp);
            RecordExceptionToFile("nonce:" + nonce);
            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    //获取微信提交过来的参数
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }
            }
            //RecordExceptionToFile("postString:" + postString);//postString为密文
            string sMsg = "";  // 解析之后的明文
            int ret = 0;
            ret = wxcpt.DecryptMsg(msg_signature, timestamp, nonce, postString, ref sMsg);
            RecordExceptionToFile("ret:" + ret);
            if (ret != 0)
            {
                return "error" + ret;
            }
            RecordExceptionToFile("sMsg:" + sMsg);
            //click demo
            //<xml><ToUserName><![CDATA[应用的id]]></ToUserName>
            //<FromUserName><![CDATA[用户账号]]></FromUserName>
            //<CreateTime>1490597539</CreateTime>
            //<MsgType><![CDATA[event]]></MsgType>
            //<AgentID>8</AgentID>
            //<Event><![CDATA[click]]></Event>
            //<EventKey><![CDATA[按钮key]]></EventKey>
            //</xml>

            //content demo
            //<xml><ToUserName><![CDATA[应用的id]]></ToUserName>
            //<FromUserName><![CDATA[用户账号]]></FromUserName>
            //<CreateTime>1490661824</CreateTime>
            //<MsgType><![CDATA[text]]></MsgType>
            //<Content><![CDATA[1]]></Content>
            //<MsgId>2582168375981020805</MsgId>
            //<AgentID>8</AgentID>
            //</xml>

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(sMsg);
            XmlNode Event = xdoc.SelectSingleNode("/xml/Event");//事件类型，CLICK
            XmlNode EventKey = xdoc.SelectSingleNode("/xml/EventKey");//事件KEY值，与自定义菜单接口中KEY值对应
            XmlNode ToUserName = xdoc.SelectSingleNode("/xml/ToUserName");//开发者微信号
            XmlNode FromUserName = xdoc.SelectSingleNode("/xml/FromUserName");//发送方账号一个openid
            XmlNode AgentID = xdoc.SelectSingleNode("/xml/AgentID");//应用号id
            XmlNode Content = xdoc.SelectSingleNode("/xml/Content");//文本
            #endregion
            //Event=click;EventKey=按钮key;ToUserName=应用的id;FromUserName=用户账号。
            //点击事件
            if (Event != null)
            {
                //菜单单击事件
                if (Event.InnerText.ToUpper().Equals("CLICK"))
                {
                    if (AgentID.InnerText == "8")//报餐应用
                    {
                        if (EventKey.InnerText.Equals("按钮key"))//账户余额
                        {
                            orderAction(FromUserName.InnerText, AgentID.InnerText, 1);
                        }
                        else if (EventKey.InnerText.Equals("按钮key"))//充值记录
                        {
                            orderAction(FromUserName.InnerText, AgentID.InnerText, 2);
                        }
                        else if (EventKey.InnerText.Equals("按钮key"))//点餐记录
                        {
                            orderAction(FromUserName.InnerText, AgentID.InnerText, 3);
                        }
                        else if (EventKey.InnerText.Equals("按钮key"))//我要点餐
                        {

                        }
                    }else if (AgentID.InnerText == "9")//请假
                    {
                        if (EventKey.InnerText.Equals("leaveIndex"))//我要请假
                        {
                            leaveAction(FromUserName.InnerText, AgentID.InnerText, 1);
                        }
                    }
                }
            }
            //文本事件
            else if (Content != null)
            {
                string cont = Content.InnerText;
                if (AgentID.InnerText == "8")//报餐应用
                {
                    if (cont.Contains("余额") || cont.Contains("账户") || cont.Contains("钱"))
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 1);
                    }
                    else if (cont.Contains("充值记录") || cont.Contains("加钱记录"))
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 2);
                    }
                    else if (cont.Contains("点餐记录") || cont.Contains("消费记录") || cont.Contains("订餐记录"))
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 3);
                    }
                    else if ((cont.Contains("今日点餐情况") || cont.ToLower().Contains("today") || cont.ToLower().Contains("system") || cont.Contains("都有谁点餐")) && IsContains("字典key", FromUserName.InnerText))//管理员查看 预留接口
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 5);
                    }
                    else if ((cont.Contains("今日账户变动情况") || cont.ToLower().Contains("all")) && IsContains("字典key", FromUserName.InnerText))//管理员查看 预留接口
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 6);
                    }
                    else if ((cont.ToLower().Contains("gbdc") || cont.ToLower().Contains("关闭点餐")) && IsContains("字典key", FromUserName.InnerText))//现在开始不能点餐
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 7);
                    }
                    else if ((cont.ToLower().Contains("dkdc") || cont.ToLower().Contains("打开点餐")) && IsContains("字典key", FromUserName.InnerText))//现在开始可以点餐
                    {
                        orderAction(FromUserName.InnerText, AgentID.InnerText, 8);
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 字典中是否包含 ，当前值
        /// </summary>
        /// <param name="enCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsContains(string enCode,string value)
        {
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> ItemList = dataItemCache.GetDataItemList(enCode);//点餐的管理人员
            foreach (Entity.SystemManage.ViewModel.DataItemModel Item in ItemList)
            {
                if (Item.ItemValue.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 返回第一个匹配的item
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        private Entity.SystemManage.ViewModel.DataItemModel GetDataItemModel(string enCode)
        {
            IEnumerable<Entity.SystemManage.ViewModel.DataItemModel> dateItem = dataItemCache.GetDataItemList(enCode);

            foreach (Entity.SystemManage.ViewModel.DataItemModel i in dateItem)
            {
                return i;
            }
            return null;
        }
        #region 异常信息写入日志文件
        /// <summary>
        ///  异常信息写入日志文件
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool RecordExceptionToFile(string ex)
        {
            try
            {
                //取得当前需要写入的日志文件名称及路径
                string strFullPath = @"F:\\webchat\Log\" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                //取得异常信息的内容
                //不存在则创建路径
                if (!Directory.Exists(@"F:\\webchat\Log\"))
                {
                    Directory.CreateDirectory(strFullPath);//创建路径
                }
                //判断当前的日志文件是否创建，如果未创建，执行创建并加入异常内容；
                //如果已经创建则直接追加填写
                if (!File.Exists(strFullPath))
                {
                    using (StreamWriter sw = File.CreateText(strFullPath))
                    {
                        sw.Write("\r\n\t" + ex + "\n\n");
                        sw.Flush();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(strFullPath))
                    {
                        sw.Write("\r\n\t" + ex + "\n\n");
                        sw.Flush();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
