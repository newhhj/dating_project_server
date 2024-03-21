using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strawberry.Server.Api.Helpers;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SystemController : ControllerBase
    {
        public DatabaseContext Db { get; }
        public FirebaseHelper FirebaseHelper { get; }

        public SystemController(DatabaseContext db, FirebaseHelper firebaseHelper)
        {
            this.Db = db;
            this.FirebaseHelper = firebaseHelper;
        }

        [HttpGet]
        public async Task CheckFreeCount()
        {
            try
            {
                if (this.Request.Host.Host != "localhost")
                    throw new Exception("Permission Deny");

                var time = DateTime.Now;
                if (time.Hour == 0 && time.Minute < 10)
                {
                    using (var tran = this.Db.Database.BeginTransaction())
                    {
                        var purchases = this.Db.Member_PurchaseLogs
                            .Where(x => x.PurchaseType == PurchaseTypes.Subscription)
                            .Where(x => !x.IsExpire && x.ExpireTime.HasValue && x.ExpireTime.Value < DateTime.Now)
                            .Include(x => x.Member)
                            .ToArray();

                        foreach (var purchase in purchases)
                        {
                            var result = await this.FirebaseHelper.CheckSubscriptionsAsync(purchase.ProductId, purchase.PurchaseToken);
                            purchase.IsExpire = result.CancelReason.HasValue;
                            purchase.ExpireTime = result.ExpiryTime;

                            switch (purchase.ProductId)
                            {
                                case "item09":
                                    purchase.Member.FreeChoiceTime = result.ExpiryTime;
                                    break;
                                case "item10":
                                    purchase.Member.FreeSmartChoiceTime = result.ExpiryTime;
                                    break;
                                case "item11":
                                    purchase.Member.FreeChattingTime = result.ExpiryTime;
                                    break;
                                case "item12":
                                    purchase.Member.FreeChoiceTime = result.ExpiryTime;
                                    break;
                                case "item13":
                                    purchase.Member.FreeSmartChoiceTime = result.ExpiryTime;
                                    break;
                                case "item14":
                                    purchase.Member.AddChatting3Time = result.ExpiryTime;
                                    break;
                                case "item15":
                                    purchase.Member.AddChoice3Time = result.ExpiryTime;
                                    break;
                                case "item16":
                                    purchase.Member.AddChatting1Time = result.ExpiryTime;
                                    break;
                                default:
                                    break;
                            }

                            if (purchase.Member.FreeChoiceCount < 2)
                                purchase.Member.FreeChoiceCount += 1;

                            if (purchase.Member.AddChoice3Time.HasValue && purchase.Member.AddChoice3Time.Value > time)
                                purchase.Member.FreeChoiceCount = 5;

                            purchase.Member.FreeChattingCount = 0;

                            if (purchase.Member.AddChatting1Time.HasValue && purchase.Member.AddChatting1Time.Value > time)
                                purchase.Member.FreeChattingCount += 1;

                            if (purchase.Member.AddChatting3Time.HasValue && purchase.Member.AddChatting3Time.Value > time)
                                purchase.Member.FreeChattingCount += 3;

                            this.Db.SaveChanges();
                        }

                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                Console.WriteLine("====================================================================================================");
                Console.WriteLine("[일일 무료 이용권 체크 프로세스 오류]");
                Console.WriteLine(ex);
                Console.WriteLine("====================================================================================================");
            }
        }

        [HttpGet]
        public IActionResult UseTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.UseTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult PrivacyTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.PrivacyTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult LocationTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.LocationTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult SensitiveTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.SensitiveTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult ContentTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.ContentTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult MarketingTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.MarketingTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult PatentTerm()
        {
            var term = this.Db.Settings
                .Select(x => x.PatentTerm)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(term))
                term = "준비중입니다.";

            var html = ""
                + "<html lang=\"kr\">"
                + "    <head>"
                + "        <meta charset=\"utf-8\">"
                + "        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                + "        <title>약관</title>"
                + "        <style>"
                + "            * {"
                + "                -webkit-user-select: none;"
                + "            }"
                + "        </style>"
                + "    </head>"
                + "    <body>"
                + $"        <pre style='white-space: pre-wrap;'>{term}</pre>"
                + "    </body>"
                + "</html>";

            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }
    }
}
