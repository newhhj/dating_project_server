using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Helpers
{
    public class FirebaseHelper
    {
        private const string developerPayload = "";

        public IWebHostEnvironment Env { get; }

        public FirebaseHelper(IWebHostEnvironment env)
        {
            this.Env = env;
        }

        public async Task SendPushAsync(string pushToken, string notifyTitle, string notifyBody, string command = null, object data = null)
        {
            await this.SendPushAsync(null, pushToken, notifyTitle, notifyBody, command, data);
        }

        private async Task SendPushAsync(string packageName, string pushToken, string notifyTitle, string notifyBody, string command = null, object data = null)
        {
            FirebaseApp firebaseApp;

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var path = Path.Combine(this.Env.ContentRootPath, "GoogleResources");
                var files = Directory.GetFiles(path);
                var file = files.OrderBy(x => x).FirstOrDefault(x => x.EndsWith(".json"));

                if (string.IsNullOrWhiteSpace(file))
                    throw new Exception("인증파일을 찾을 수 없습니다.");

                packageName = Path.GetFileNameWithoutExtension(file);
            }

            firebaseApp = FirebaseApp.GetInstance(packageName);

            if (firebaseApp == null)
            {
                var filePath = Path.Combine(this.Env.ContentRootPath, "GoogleResources", $"{packageName}.json");
                if (File.Exists(filePath))
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = Google.Apis.Auth.OAuth2.GoogleCredential
                        .FromFile(filePath)
                        .CreateScoped(
                            "https://www.googleapis.com/auth/firebase.messaging",
                            "https://www.googleapis.com/auth/androidpublisher"
                        )
                    }, packageName);

                    firebaseApp = FirebaseApp.GetInstance(packageName);
                }
            }

            if (firebaseApp == null)
                throw new Exception("인증파일을 찾을 수 없습니다.");

            var notifyData = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(command))
            {
                notifyData.Add("command", command);
            }

            if (data != null)
            {
                notifyData.Add("data", JsonConvert.SerializeObject(data));
            }

            try
            {
                var firebaseMessaging = FirebaseMessaging.GetMessaging(firebaseApp);
                var result = await firebaseMessaging.SendAsync(new Message
                {
                    Token = pushToken,
                    Notification = new Notification
                    {
                        Title = notifyTitle,
                        Body = notifyBody
                    },
                    Data = notifyData
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendPushAsync(string[] pushTokens, string notifyTitle, string notifyBody, string command = null, object data = null)
        {
            await this.SendPushAsync(null, pushTokens, notifyTitle, notifyBody, command, data);
        }

        private async Task SendPushAsync(string packageName, string[] pushTokens, string notifyTitle, string notifyBody, string command = null, object data = null)
        {
            FirebaseApp firebaseApp;

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var path = Path.Combine(this.Env.ContentRootPath, "GoogleResources");
                var files = Directory.GetFiles(path);
                var file = files.OrderBy(x => x).FirstOrDefault(x => x.EndsWith(".json"));

                if (string.IsNullOrWhiteSpace(file))
                    throw new Exception("인증파일을 찾을 수 없습니다.");

                packageName = Path.GetFileNameWithoutExtension(file);
            }

            firebaseApp = FirebaseApp.GetInstance(packageName);

            if (firebaseApp == null)
            {
                var filePath = Path.Combine(this.Env.ContentRootPath, "GoogleResources", $"{packageName}.json");
                if (File.Exists(filePath))
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = Google.Apis.Auth.OAuth2.GoogleCredential
                        .FromFile(filePath)
                        .CreateScoped(
                            "https://www.googleapis.com/auth/firebase.messaging",
                            "https://www.googleapis.com/auth/androidpublisher"
                        )
                    }, packageName);

                    firebaseApp = FirebaseApp.GetInstance(packageName);
                }
            }

            if (firebaseApp == null)
                throw new Exception("인증파일을 찾을 수 없습니다.");

            var notifyData = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(command))
            {
                notifyData.Add("command", command);
            }

            if (data != null)
            {
                notifyData.Add("data", JsonConvert.SerializeObject(data));
            }

            var firebaseMessaging = FirebaseMessaging.GetMessaging(firebaseApp);
            await firebaseMessaging.SendMulticastAsync(new MulticastMessage
            {
                Tokens = pushTokens,
                Notification = new Notification
                {
                    Title = notifyTitle,
                    Body = notifyBody
                },
                Data = notifyData
            });
        }

        public async Task<ProductsResult> CheckProductsAsync(string productId, string purchaseToken)
        {
            return await this.CheckProductsAsync(null, productId, purchaseToken);
        }

        private async Task<ProductsResult> CheckProductsAsync(string packageName, string productId, string purchaseToken)
        {
            FirebaseApp firebaseApp;

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var path = Path.Combine(this.Env.ContentRootPath, "GoogleResources");
                var files = Directory.GetFiles(path);
                var file = files.OrderBy(x => x).FirstOrDefault(x => x.EndsWith(".json"));

                if (string.IsNullOrWhiteSpace(file))
                    throw new Exception("인증파일을 찾을 수 없습니다.");

                packageName = Path.GetFileNameWithoutExtension(file);
            }

            firebaseApp = FirebaseApp.GetInstance(packageName);

            if (firebaseApp == null)
            {
                var filePath = Path.Combine(this.Env.ContentRootPath, "GoogleResources", $"{packageName}.json");
                if (File.Exists(filePath))
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = Google.Apis.Auth.OAuth2.GoogleCredential
                        .FromFile(filePath)
                        .CreateScoped(
                            "https://www.googleapis.com/auth/firebase.messaging",
                            "https://www.googleapis.com/auth/androidpublisher"
                        )
                    }, packageName);

                    firebaseApp = FirebaseApp.GetInstance(packageName);
                }
            }

            if (firebaseApp == null)
                throw new Exception("인증파일을 찾을 수 없습니다.");

            var accessToken = await firebaseApp.Options.Credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            var url = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{purchaseToken}?access_token={accessToken}";

            using (var http = new HttpClient())
            {
                var content = await http.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<ProductsResult>(content);

                return result;
            }
        }

        public async Task<SubscriptionsResult> CheckSubscriptionsAsync(string productId, string purchaseToken)
        {
            return await this.CheckSubscriptionsAsync(null, productId, purchaseToken);
        }

        private async Task<SubscriptionsResult> CheckSubscriptionsAsync(string packageName, string productId, string purchaseToken)
        {
            FirebaseApp firebaseApp;

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var path = Path.Combine(this.Env.ContentRootPath, "GoogleResources");
                var files = Directory.GetFiles(path);
                var file = files.OrderBy(x => x).FirstOrDefault(x => x.EndsWith(".json"));

                if (string.IsNullOrWhiteSpace(file))
                    throw new Exception("인증파일을 찾을 수 없습니다.");

                packageName = Path.GetFileNameWithoutExtension(file);
            }

            firebaseApp = FirebaseApp.GetInstance(packageName);

            if (firebaseApp == null)
            {
                var filePath = Path.Combine(this.Env.ContentRootPath, "GoogleResources", $"{packageName}.json");
                if (File.Exists(filePath))
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = Google.Apis.Auth.OAuth2.GoogleCredential
                        .FromFile(filePath)
                        .CreateScoped(
                            "https://www.googleapis.com/auth/firebase.messaging",
                            "https://www.googleapis.com/auth/androidpublisher"
                        )
                    }, packageName);

                    firebaseApp = FirebaseApp.GetInstance(packageName);
                }
            }

            if (firebaseApp == null)
                throw new Exception("인증파일을 찾을 수 없습니다.");

            var accessToken = await firebaseApp.Options.Credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            var url = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/subscriptions/{productId}/tokens/{purchaseToken}?access_token={accessToken}";


            using (var http = new HttpClient())
            {
                var content = await http.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<SubscriptionsResult>(content);

                return result;
            }
        }

        public async Task AcknowledgeProductsAsync(string packageName, string productId, string purchaseToken)
        {
            FirebaseApp firebaseApp;

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var path = Path.Combine(this.Env.ContentRootPath, "GoogleResources");
                var files = Directory.GetFiles(path);
                var file = files.OrderBy(x => x).FirstOrDefault(x => x.EndsWith(".json"));

                if (string.IsNullOrWhiteSpace(file))
                    throw new Exception("인증파일을 찾을 수 없습니다.");

                packageName = Path.GetFileNameWithoutExtension(file);
            }

            firebaseApp = FirebaseApp.GetInstance(packageName);

            if (firebaseApp == null)
            {
                var filePath = Path.Combine(this.Env.ContentRootPath, "GoogleResources", $"{packageName}.json");
                if (File.Exists(filePath))
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = Google.Apis.Auth.OAuth2.GoogleCredential
                        .FromFile(filePath)
                        .CreateScoped(
                            "https://www.googleapis.com/auth/firebase.messaging",
                            "https://www.googleapis.com/auth/androidpublisher"
                        )
                    }, packageName);

                    firebaseApp = FirebaseApp.GetInstance(packageName);
                }
            }

            if (firebaseApp == null)
                throw new Exception("인증파일을 찾을 수 없습니다.");

            var accessToken = await firebaseApp.Options.Credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            var url = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{purchaseToken}:acknowledge?access_token={accessToken}";

            using (var http = new HttpClient())
            {
                var jsonText = JsonConvert.SerializeObject(new { developerPayload });
                var content = new StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
                await http.PostAsync(url, content);
            }
        }

        public async Task AcknowledgeSubscriptionsAsync(string packageName, string productId, string purchaseToken)
        {
            FirebaseApp firebaseApp;

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var path = Path.Combine(this.Env.ContentRootPath, "GoogleResources");
                var files = Directory.GetFiles(path);
                var file = files.OrderBy(x => x).FirstOrDefault(x => x.EndsWith(".json"));

                if (string.IsNullOrWhiteSpace(file))
                    throw new Exception("인증파일을 찾을 수 없습니다.");

                packageName = Path.GetFileNameWithoutExtension(file);
            }

            firebaseApp = FirebaseApp.GetInstance(packageName);

            if (firebaseApp == null)
            {
                var filePath = Path.Combine(this.Env.ContentRootPath, "GoogleResources", $"{packageName}.json");
                if (File.Exists(filePath))
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = Google.Apis.Auth.OAuth2.GoogleCredential
                        .FromFile(filePath)
                        .CreateScoped(
                            "https://www.googleapis.com/auth/firebase.messaging",
                            "https://www.googleapis.com/auth/androidpublisher"
                        )
                    }, packageName);

                    firebaseApp = FirebaseApp.GetInstance(packageName);
                }
            }

            if (firebaseApp == null)
                throw new Exception("인증파일을 찾을 수 없습니다.");

            var accessToken = await firebaseApp.Options.Credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            var url = $"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/subscriptions/{productId}/tokens/{purchaseToken}:acknowledge?access_token={accessToken}";

            using (var http = new HttpClient())
            {
                var jsonText = JsonConvert.SerializeObject(new { developerPayload });
                var content = new StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
                await http.PostAsync(url, content);
            }
        }

        /// <summary>
        /// 구매상태 타입
        /// </summary>
        public enum PurchaseStateTypes
        {
            /// <summary>
            /// 구매함
            /// </summary>
            Success = 0,
            /// <summary>
            /// 취소됨
            /// </summary>
            Canceled = 1,
            /// <summary>
            /// 보류중
            /// </summary>
            Wait = 2
        }

        /// <summary>
        /// 소비상태 타입
        /// </summary>
        public enum ConsumptionStateTypes
        {
            /// <summary>
            /// 대기중
            /// </summary>
            Wait = 0,
            /// <summary>
            /// 소비됨
            /// </summary>
            Success = 1
        }

        /// <summary>
        /// 승인상태 타입
        /// </summary>
        public enum AcknowledgementStateTypes
        {
            /// <summary>
            /// 대기중
            /// </summary>
            Wait = 0,
            /// <summary>
            /// 승인됨
            /// </summary>
            Success = 1
        }

        /// <summary>
        /// 취소이유 타입
        /// </summary>
        public enum CancelReasonTypes
        {
            /// <summary>
            /// 사용자 취소
            /// </summary>
            UserCancel = 0,
            /// <summary>
            /// 결제 실패
            /// </summary>
            SystemCancel = 1,
            /// <summary>
            /// 구독 변경
            /// </summary>
            ChangeSubscription = 2,
            /// <summary>
            /// 개발자 취소
            /// </summary>
            DeveloperCancel = 3
        }

        /// <summary>
        /// 일반상품 정보
        /// </summary>
        public class ProductsResult
        {
            /// <summary>
            /// 구매시간 (밀리초)
            /// </summary>
            public string PurchaseTimeMillis { get; set; }

            /// <summary>
            /// 구매시간
            /// </summary>
            public DateTime PurchaseTime { get => new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(PurchaseTimeMillis)).AddHours(9); }

            /// <summary>
            /// 구매상태
            /// </summary>
            public PurchaseStateTypes PurchaseState { get; set; }

            /// <summary>
            /// 소비상태
            /// </summary>
            public ConsumptionStateTypes ConsumptionState { get; set; }

            /// <summary>
            /// 개발자 지정 문자열
            /// </summary>
            public string DeveloperPayload { get; set; }

            /// <summary>
            /// 주문 ID
            /// </summary>
            public string OrderId { get; set; }

            /// <summary>
            /// 승인상태
            /// </summary>
            public AcknowledgementStateTypes AcknowledgementState { get; set; }
        }

        /// <summary>
        /// 구독상품 정보
        /// </summary>
        public class SubscriptionsResult
        {
            /// <summary>
            /// 구독시작시간 (밀리초)
            /// </summary>
            public string StartTimeMillis { get; set; }

            /// <summary>
            /// 구독시작시간
            /// </summary>
            public DateTime StartTime { get => new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(StartTimeMillis)).AddHours(9); }

            /// <summary>
            /// 구독만료시간 (밀리초)
            /// </summary>
            public string ExpiryTimeMillis { get; set; }

            /// <summary>
            /// 구독만료시간
            /// </summary>
            public DateTime ExpiryTime { get => new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(ExpiryTimeMillis)).AddHours(9); }

            /// <summary>
            /// 자동갱신 여부
            /// </summary>
            public bool AutoRenewing { get; set; }

            /// <summary>
            /// 개발자 지정 문자열
            /// </summary>
            public string DeveloperPayload { get; set; }

            /// <summary>
            /// 취소 사유
            /// </summary>
            public CancelReasonTypes? CancelReason { get; set; }

            /// <summary>
            /// 주문 ID
            /// </summary>
            public string OrderId { get; set; }

            /// <summary>
            /// 승인상태
            /// </summary>
            public AcknowledgementStateTypes AcknowledgementState { get; set; }
        }
    }
}
