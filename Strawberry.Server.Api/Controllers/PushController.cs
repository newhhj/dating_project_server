using Geolocation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Strawberry.Server.Api.Helpers;
using Strawberry.Server.Database;
using Strawberry.Server.Database.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PushController : ControllerBase
    {
        public IWebHostEnvironment Env { get; }
        public DatabaseContext Db { get; }
        public FirebaseHelper Firebase { get; }

        public PushController(IWebHostEnvironment env, DatabaseContext db, FirebaseHelper firebase)
        {
            this.Env = env;
            this.Db = db;
            this.Firebase = firebase;
        }

        [HttpPost]
        public async Task<object> SendChatMessage(
            [FromForm] string apikey,
            [FromForm] int partnerId,
            [FromForm] string packagename,
            [FromForm] string message,
            [FromForm] string command)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var partner = this.Db.Members
                    .Where(x => x.Id == partnerId)
                    .Select(x => new
                    {
                        x.Id,
                        x.PushToken
                    })
                    .FirstOrDefault();

                await this.Firebase.SendPushAsync(partner.PushToken, member.Nickname, message, command, new { });

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
    }
}
