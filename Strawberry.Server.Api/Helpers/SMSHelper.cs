using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Strawberry.Server.Api.Helpers
{
    public class SMSHelper
    {
        /// <summary>
        /// Cafe24 아이디
        /// </summary>
        private static string UserId { get; set; }
        /// <summary>
        /// 인증키
        /// </summary>
        private static string SecureKey { get; set; }
        /// <summary>
        /// 발신자 번호 ('-'포함)
        /// </summary>
        private static string Sender { get; set; }

        /// <summary>
        /// 모듈 초기화
        /// </summary>
        /// <param name="userid">Cafe24 아이디</param>
        /// <param name="secureKey">인증키</param>
        /// <param name="sender">발신자 번호 ('-'포함)</param>
        public static void Init(string userid, string secureKey, string sender)
        {
            UserId = userid;
            SecureKey = secureKey;
            Sender = sender;
        }

        /// <summary>
        /// SMS 발송
        /// </summary>
        /// <param name="receiver">수신자 번호 ('-' 포함), 여러명일경우 콤마(',')로 구분</param>
        /// <param name="message">문자 내용 (SMS = 90 byte 이하, LMS = 2,000 byte 이하 까지 입력)</param>
        /// <returns>발송 성공 여부</returns>
        public static bool SendSMS(string receiver, string message)
        {
            var url = "https://sslsms.cafe24.com/sms_sender.php";
            var sender1 = Sender.Split('-')[0].Trim();
            var sender2 = Sender.Split('-')[1].Trim();
            var sender3 = Sender.Split('-')[2].Trim();

            receiver = Regex.Replace(receiver, "[^\\+0-9]", "");

            using (var http = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "user_id", UserId },
                    { "secure", SecureKey },
                    { "sphone1", sender1 },
                    { "sphone2", sender2 },
                    { "sphone3", sender3 },
                    { "rphone", receiver },
                    { "msg", message },
                };

                var content = new FormUrlEncodedContent(parameters);

                var response = http.PostAsync(url, content).Result;
                var contentText = response.Content.ReadAsStringAsync().Result;

                if (contentText.StartsWith("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
