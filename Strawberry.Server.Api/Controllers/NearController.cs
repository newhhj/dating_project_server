using Geolocation;
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
    public class NearController : ControllerBase
    {
        public DatabaseContext Db { get; }

        public NearController(DatabaseContext db)
        {
            this.Db = db;
        }

        [HttpPost]
        public object GetPartners(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Gender,
                        x.Lat,
                        x.Lng,
                        Blocks = x.Member_BlockPartners
                            .Where(z => z.PartnerId.HasValue)
                            .Select(z => z.PartnerId.Value)
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var memberId = member.Id;
                var memberGender = member.Gender;
                var memberLat = member.Lat;
                var memberLng = member.Lng;
                var blocks = member.Blocks.ToArray();

                var items = this.Db.Members
                    .Where(x => x.Gender != memberGender)
                    .Where(x => !blocks.Any(z => z == x.Id))
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImage = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .FirstOrDefault(),
                        Range = GeoCalculator.GetDistance(x.Lat, x.Lng, memberLat, memberLng, 1, DistanceUnit.Kilometers),
                        IsLive = x.LastLoginTime.HasValue ? (DateTime.Now - x.LastLoginTime.Value).TotalHours < 8 : false,
                        OrderIdx = this.Db.Rand()
                    })
                    .ToArray()
                    .OrderBy(x => x.OrderIdx)
                    .Where(x => x.Range < 10)
                    .Take(5)
                    .ToArray();

                return new { items };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
    }
}
