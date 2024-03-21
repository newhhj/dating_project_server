using Geolocation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Strawberry.Server.Api.Helpers;
using Strawberry.Server.Database;
using Strawberry.Server.Database.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MainController : ControllerBase
    {
        public DatabaseContext Db { get; }
        public ImageHelper ImageHelper { get; }
        public FirebaseHelper FirebaseHelper { get; }

        public MainController(DatabaseContext db, ImageHelper imageHelper, FirebaseHelper firebaseHelper)
        {
            this.Db = db;
            this.ImageHelper = imageHelper;
            this.FirebaseHelper = firebaseHelper;
        }

        [HttpPost]
        public object InitState(
            [FromForm] string apikey,
            [FromForm] string platform,
            [FromForm] string pushtoken)
        {
            try
            {
                var sql = ""
                    + "UPDATE Member "
                    + "SET Platform = {0}, "
                    + "    PushToken = {1}, "
                    + "    MemberState = {2}, "
                    + "    LastLoginTime = {3} "
                    + "WHERE ApiKey = {4}";
                var updateCount = this.Db.Database.ExecuteSqlRaw(sql, platform, pushtoken, MemberStateTypes.Normal, DateTime.Now, apikey);

                return new { updateCount };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetMainItems(
            [FromForm] string apikey,
            [FromForm] int page)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Gender,
                        x.Lat,
                        x.Lng,
                        x.Member_Preference.MinAge,
                        x.Member_Preference.MaxAge,
                        x.Member_Preference.Range,
                        x.Member_Preference.MinTall,
                        x.Member_Preference.MaxTall,
                        Member_NotShowMembers = x.Member_NotShowMembers
                            .Select(z => z.PartnerId)
                            .ToArray(),
                        HotStrawberry = x.Member_Hotstrawberrys
                            .OrderByDescending(z => z.StartTime)
                            .Select(z => new
                            {
                                z.Id,
                                HotStrawberryStartTime = z.StartTime,
                                ViewCount = x.Member_StarPoints.Count(c => c.CreateTime >= z.StartTime)
                            })
                            .FirstOrDefault(),
                        StarPoints = x.Member_StarPoints
                            .Where(z => z.PartnerId.HasValue)
                            .Select(z => z.PartnerId.Value),
                        Choices = x.Member_ChoicePartners
                            .Where(z => z.PartnerId.HasValue)
                            .Select(z => z.PartnerId.Value)
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                if (page >= 12)
                {
                    return new { };
                }

                var r = new Random();

                if (page == 0)
                {
                    var view01Items = this.Db.Members
                        .Where(x => x.MemberState == MemberStateTypes.Normal)
                        .Where(x => x.LevelType >= LevelTypes.Platinum)
                        .Where(x => x.Gender != member.Gender)
                        .Where(x => member.MinTall <= x.Tall && member.MaxTall >= x.Tall)
                        .Where(x => !member.Member_NotShowMembers.Any(z => z == x.Id))
                        .Where(x => !member.Choices.Any(z => z == x.Id))
                        .Select(x => new
                        {
                            Id = x.Id,
                            Thumbails = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .ToArray(),
                            Nickname = x.Nickname,
                            Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                            Job = x.Job,
                            IsRoyal = x.IsRoyal,
                            IsVIP = x.IsVIP,
                            HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                            HasStarBadge = x.HasStarBadge,
                            FirstMessage = x.FirstMessage,
                            SecondMessage = $"음주는 {(x.Alcohol == "안함" ? "안해요" : x.Alcohol + "해요")}",
                            Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            IsNowJoin = x.CreateTime.Date == DateTime.Today,
                            IsLive = x.LastLoginTime.HasValue ? (DateTime.Now - x.LastLoginTime.Value).TotalHours < 8 : false,
                            CharmingPoints = x.Member_CharmingPoints
                                .Select(z => z.Name)
                                .ToArray(),
                            Interests = x.Member_Interests
                                .Select(z => z.Name)
                                .ToArray()
                        })
                        .ToArray()
                        .Where(x => x.Range <= 200)
                        .Where(x => member.Range >= x.Range)
                        .Where(x => member.MinAge <= x.Age && x.Age <= member.MaxAge)
                        .OrderBy(x => r.Next())
                        .Take(2)
                        .ToArray();

                    var view02Items = this.Db.Members
                        .Where(x => x.MemberState == MemberStateTypes.Normal)
                        .Where(x => x.Gender != member.Gender)
                        .Where(x => member.MinTall <= x.Tall && member.MaxTall >= x.Tall)
                        .Where(x => !member.Member_NotShowMembers.Any(z => z == x.Id))
                        .Select(x => new
                        {
                            Id = x.Id,
                            ProfileImage = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .FirstOrDefault(),
                            Nickname = x.Nickname,
                            Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                            Job = x.Job,
                            Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                            IsNowJoin = x.CreateTime.Date == DateTime.Today,
                            IsVIP = x.IsVIP,
                        })
                        .ToArray()
                        .Where(x => x.Range <= 200)
                        .Where(x => member.Range >= x.Range)
                        .Where(x => member.MinAge <= x.Age && x.Age <= member.MaxAge)
                        .Where(x => !view01Items.Any(z => z.Id == x.Id))
                        .OrderBy(x => r.Next())
                        .Take(4)
                        .ToArray();

                    var view03Items = member.HotStrawberry;

                    var view04Items = this.Db.Members
                        .Where(x => x.MemberState == MemberStateTypes.Normal)
                        .Where(x => x.Gender != member.Gender)
                        .Where(x => !member.StarPoints.Any(z => z == x.Id))
                        .Select(x => new
                        {
                            Id = x.Id,
                            ProfileImages = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .ToArray(),
                            Nickname = x.Nickname,
                            Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                            Job = x.Job,
                            IsRoyal = x.IsRoyal,
                            IsVIP = x.IsVIP,
                            HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                            HasStarBadge = x.HasStarBadge,
                            FirstMessage = x.FirstMessage,
                            SecondMessage = $"음주는 {(x.Alcohol == "안함" ? "안해요" : x.Alcohol + "해요")}",
                            Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            IsNowJoin = x.CreateTime.Date == DateTime.Today,
                            IsLive = x.LastLoginTime.HasValue ? (DateTime.Now - x.LastLoginTime.Value).TotalHours < 8 : false,
                            CharmingPoints = x.Member_CharmingPoints
                                .Select(z => z.Name)
                                .ToArray(),
                            Interests = x.Member_Interests
                                .Select(z => z.Name)
                                .ToArray(),
                            Hotstrawberrys = x.Member_Hotstrawberrys
                                .OrderByDescending(z => z.StartTime)
                                .Select(z => z.StartTime)
                                .FirstOrDefault(),
                            x.CreateTime
                        })
                        .ToArray()
                        .Where(x => x.Range <= 200)
                        .OrderBy(x => x.Hotstrawberrys.HasValue && x.Hotstrawberrys.Value.AddHours(1) > DateTime.Now ? 0 : 1)
                        .ThenByDescending(x => x.CreateTime)
                        .FirstOrDefault();

                    var View06Items = this.Db.Members
                        .Where(x => x.MemberState == MemberStateTypes.Normal)
                        .Where(x => x.Gender != member.Gender)
                        .Select(x => new
                        {
                            x.Id,
                            ProfileImage = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .FirstOrDefault(),
                            x.Nickname,
                            x.BirthDay
                        })
                        .OrderBy(x => x.BirthDay)
                        .ToArray()
                        .Where(x => new DateTime(DateTime.Today.Year, x.BirthDay.Month, x.BirthDay.Day) >= DateTime.Today.AddDays(-7))
                        .Where(x => new DateTime(DateTime.Today.Year, x.BirthDay.Month, x.BirthDay.Day) <= DateTime.Today.AddDays(7))
                        .Select(x => new
                        {
                            x.Id,
                            x.ProfileImage,
                            x.Nickname,
                            Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                        })
                        .Take(5)
                        .ToArray();

                    return new
                    {
                        view01Items,
                        view02Items,
                        view03Items,
                        view04Items,
                        View06Items
                    };
                }
                else
                {
                    var view01Items = this.Db.Members
                        .Where(x => x.MemberState == MemberStateTypes.Normal)
                        .Where(x => x.Gender != member.Gender)
                        .Where(x => !member.Member_NotShowMembers.Any(z => z == x.Id))
                        .Select(x => new
                        {
                            Id = x.Id,
                            Thumbails = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .ToArray(),
                            Nickname = x.Nickname,
                            Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                            Job = x.Job,
                            IsRoyal = x.IsRoyal,
                            IsVIP = x.IsVIP,
                            HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                            HasStarBadge = x.HasStarBadge,
                            FirstMessage = x.FirstMessage,
                            SecondMessage = $"음주는 {(x.Alcohol == "안함" ? "안해요" : x.Alcohol + "해요")}",
                            Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            IsNowJoin = x.CreateTime.Date == DateTime.Today,
                            IsLive = x.LastLoginTime.HasValue ? (DateTime.Now - x.LastLoginTime.Value).TotalHours < 8 : false,
                            CharmingPoints = x.Member_CharmingPoints
                                .Select(z => z.Name)
                                .ToArray(),
                            Interests = x.Member_Interests
                                .Select(z => z.Name)
                                .ToArray(),
                            RandIdx = this.Db.Rand()
                        })
                        .ToArray()
                        .OrderBy(x => x.RandIdx)
                        .Where(x => x.Range <= 200)
                        .Where(x => member.Range >= x.Range)
                        .Where(x => member.MinAge <= x.Age && x.Age <= member.MaxAge)
                        .Skip((page - 1) * 8)
                        .Take(8)
                        .ToArray();

                    var view02Items = this.Db.Members
                        .Where(x => x.MemberState == MemberStateTypes.Normal)
                        .Where(x => x.Gender != member.Gender)
                        .Where(x => !member.Member_NotShowMembers.Any(z => z == x.Id))
                        .Select(x => new
                        {
                            Id = x.Id,
                            ProfileImage = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .FirstOrDefault(),
                            Nickname = x.Nickname,
                            Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                            Job = x.Job,
                            Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                            IsNowJoin = x.CreateTime.Date == DateTime.Today,
                            IsVIP = x.IsVIP,
                        })
                        .OrderBy(x => this.Db.Rand())
                        .ToArray()
                        .Where(x => x.Range <= 200)
                        .Where(x => member.Range >= x.Range)
                        .Where(x => member.MinAge <= x.Age && x.Age <= member.MaxAge)
                        .Where(x => !view01Items.Any(z => z.Id == x.Id))
                        .Skip(8 + ((page - 1) * 4))
                        .Take(4)
                        .ToArray();

                    return new
                    {
                        view01Items,
                        view02Items,
                    };
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public async Task<object> GetPartnerProfile(
            [FromForm] string apikey,
            [FromForm] int partnerid)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname,
                        StarPoint = x.Member_StarPoints
                            .Where(z => z.PartnerId == partnerid)
                            .Select(z => z.StarPoint)
                            .FirstOrDefault(),
                        HasChoice = x.Member_ChoicePartners
                            .Where(z => z.PartnerId == partnerid)
                            .Any(),
                        x.Lat,
                        x.Lng,
                        x.Point,
                        IsViewProfile = x.Member_ViewProfiles
                            .Any(z => z.PartnerId == partnerid),
                        x.Gender
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                if (member.Point < 5)
                    throw new Exception("딸기가 부족합니다.");

                var tran = this.Db.Database.BeginTransaction();

                {
                    var sql = ""
                        + "UPDATE Member "
                        + "SET Point = {0} "
                        + "WHERE Id = {1}";
                    this.Db.Database.ExecuteSqlRaw(sql, member.Point - 5, member.Id);
                }

                this.Db.Add(new Member_PointLog
                {
                    AcceptPoint = -5,
                    Comment = "프로필 조회",
                    CreateTime = DateTime.Now,
                    CurrentPoint = member.Point - 5,
                    MemberId = member.Id
                });
                this.Db.SaveChanges();

                if (!member.IsViewProfile)
                {
                    this.Db.Add(new Member_ViewProfile
                    {
                        MemberId = member.Id,
                        PartnerId = partnerid
                    });
                    this.Db.SaveChanges();
                }

                var partner = this.Db.Members
                    .Where(x => x.Id == partnerid)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImages = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => new
                            {
                                z.Id,
                                ProfileImageSource = z.Url
                            }),
                        x.Nickname,
                        IsReciveStarPoint = member.StarPoint >= 4,
                        IsSendStarPoint = x.Member_StarPoints
                            .Where(z => z.PartnerId == x.Id && z.StarPoint >= 4)
                            .Select(z => z.StarPoint)
                            .Any(),
                        IsReciveChoice = member.HasChoice,
                        IsSendChoice = x.Member_ChoicePartners
                            .Where(z => z.PartnerId == x.Id)
                            .Any(),
                        IsShowMyProfile = x.Member_ViewProfiles
                            .Where(z => z.PartnerId == member.Id)
                            .Any(),
                        IsRoyal = x.IsRoyal,
                        IsVIP = x.IsVIP,
                        Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                        HasStarBadge = x.HasStarBadge,
                        Job = x.Job,
                        Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                        HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                        x.Tall,
                        x.School,
                        TotalViewCount = this.Db.Member_ViewProfiles
                            .Where(z => z.PartnerId == x.Id)
                            .Count(),
                        TodayViewCount = this.Db.Member_ViewProfiles
                            .Where(z => z.PartnerId == x.Id && z.CreateTime.Date == DateTime.Today)
                            .Count(),
                        RateCharming = x.RateCharming,
                        RateResponse = x.RateResponse,
                        FirstMessage = x.FirstMessage,
                        BodyStyle = x.BodyStyle,
                        Religion = x.Religion,
                        Alcohol = x.Alcohol,
                        Smoking = x.Smoking,
                        StarPoint = member.StarPoint,
                        CharmingPoints = x.Member_CharmingPoints
                            .Select(z => z.Name),
                        Interests = x.Member_Interests
                            .Select(z => z.Name),
                    })
                    .FirstOrDefault();

                var birthdayMembers = this.Db.Members
                    .Where(x => x.MemberState == MemberStateTypes.Normal)
                    .Where(x => x.Gender != member.Gender)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImage = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .FirstOrDefault(),
                        x.Nickname,
                        x.BirthDay
                    })
                    .OrderBy(x => x.BirthDay)
                    .ToArray()
                    .Where(x => new DateTime(DateTime.Today.Year, x.BirthDay.Month, x.BirthDay.Day) >= DateTime.Today.AddDays(-7))
                    .Where(x => new DateTime(DateTime.Today.Year, x.BirthDay.Month, x.BirthDay.Day) <= DateTime.Today.AddDays(7))
                    .Select(x => new
                    {
                        x.Id,
                        x.ProfileImage,
                        x.Nickname,
                        Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                    })
                    .Take(5)
                    .ToArray();

                /// 알림처리
                {
                    var receiver = this.Db.Members
                        .Where(x => x.Id == partnerid)
                        .Select(x => new
                        {
                            x.Id,
                            x.PushToken,
                            x.UseNotiReceiveFavorite,
                        })
                        .FirstOrDefault();

                    if (receiver != null)
                    {
                        var msg = $"{member.Nickname}님이 프로필을 확인했습니다.";
                        this.Db.Add(new Member_Notification
                        {
                            CreateTime = DateTime.Now,
                            IsShow = false,
                            MemberId = receiver.Id,
                            Message = msg
                        });
                        this.Db.SaveChanges();

                        var removeItems = this.Db.Member_Notifications
                            .Where(x => x.MemberId == receiver.Id)
                            .OrderByDescending(x => x.CreateTime)
                            .Skip(5)
                            .ToArray();
                        this.Db.RemoveRange(removeItems);
                        this.Db.SaveChanges();

                        if (receiver.UseNotiReceiveFavorite)
                        {
                            await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                        }
                    }
                }

                tran.Commit();

                return new { point = member.Point - 5, ProfilePagePartnerData = partner, birthdayMembers };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public async Task<object> GetFreeViewPartnerProfile(
            [FromForm] string apikey,
            [FromForm] int partnerid)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname,
                        StarPoint = x.Member_StarPoints
                            .Where(z => z.PartnerId == partnerid)
                            .Select(z => z.StarPoint)
                            .FirstOrDefault(),
                        HasChoice = x.Member_ChoicePartners
                            .Where(z => z.PartnerId == partnerid)
                            .Any(),
                        x.Lat,
                        x.Lng,
                        x.Point,
                        IsViewProfile = x.Member_ViewProfiles
                            .Any(z => z.PartnerId == partnerid),
                        x.Gender
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var tran = this.Db.Database.BeginTransaction();

                if (!member.IsViewProfile)
                {
                    this.Db.Add(new Member_ViewProfile
                    {
                        MemberId = member.Id,
                        PartnerId = partnerid
                    });
                    this.Db.SaveChanges();
                }

                var partner = this.Db.Members
                    .Where(x => x.Id == partnerid)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImages = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => new
                            {
                                z.Id,
                                ProfileImageSource = z.Url
                            }),
                        x.Nickname,
                        IsReciveStarPoint = member.StarPoint >= 4,
                        IsSendStarPoint = x.Member_StarPoints
                            .Where(z => z.PartnerId == x.Id && z.StarPoint >= 4)
                            .Select(z => z.StarPoint)
                            .Any(),
                        IsReciveChoice = member.HasChoice,
                        IsSendChoice = x.Member_ChoicePartners
                            .Where(z => z.PartnerId == x.Id)
                            .Any(),
                        IsShowMyProfile = x.Member_ViewProfiles
                            .Where(z => z.PartnerId == member.Id)
                            .Any(),
                        IsRoyal = x.IsRoyal,
                        IsVIP = x.IsVIP,
                        Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                        HasStarBadge = x.HasStarBadge,
                        Job = x.Job,
                        Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                        HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                        x.Tall,
                        x.School,
                        TotalViewCount = this.Db.Member_ViewProfiles
                            .Where(z => z.PartnerId == x.Id)
                            .Count(),
                        TodayViewCount = this.Db.Member_ViewProfiles
                            .Where(z => z.PartnerId == x.Id && z.CreateTime.Date == DateTime.Today)
                            .Count(),
                        RateCharming = x.RateCharming,
                        RateResponse = x.RateResponse,
                        FirstMessage = x.FirstMessage,
                        BodyStyle = x.BodyStyle,
                        Religion = x.Religion,
                        Alcohol = x.Alcohol,
                        Smoking = x.Smoking,
                        StarPoint = member.StarPoint,
                        CharmingPoints = x.Member_CharmingPoints
                            .Select(z => z.Name),
                        Interests = x.Member_Interests
                            .Select(z => z.Name)
                    })
                    .FirstOrDefault();

                var birthdayMembers = this.Db.Members
                    .Where(x => x.MemberState == MemberStateTypes.Normal)
                    .Where(x => x.Gender != member.Gender)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImage = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .FirstOrDefault(),
                        x.Nickname,
                        x.BirthDay
                    })
                    .OrderBy(x => x.BirthDay)
                    .ToArray()
                    .Where(x => new DateTime(DateTime.Today.Year, x.BirthDay.Month, x.BirthDay.Day) >= DateTime.Today.AddDays(-7))
                    .Where(x => new DateTime(DateTime.Today.Year, x.BirthDay.Month, x.BirthDay.Day) <= DateTime.Today.AddDays(7))
                    .Select(x => new
                    {
                        x.Id,
                        x.ProfileImage,
                        x.Nickname,
                        Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                    })
                    .Take(5)
                    .ToArray();

                tran.Commit();

                /// 알림처리
                {
                    var receiver = this.Db.Members
                        .Where(x => x.Id == partnerid)
                        .Select(x => new
                        {
                            x.Id,
                            x.PushToken,
                            x.UseNotiReceiveFavorite,
                        })
                        .FirstOrDefault();

                    if (receiver != null)
                    {
                        var msg = $"{member.Nickname}님이 프로필을 확인했습니다.";
                        this.Db.Add(new Member_Notification
                        {
                            CreateTime = DateTime.Now,
                            IsShow = false,
                            MemberId = receiver.Id,
                            Message = msg
                        });
                        this.Db.SaveChanges();

                        var removeItems = this.Db.Member_Notifications
                            .Where(x => x.MemberId == receiver.Id)
                            .OrderByDescending(x => x.CreateTime)
                            .Skip(5)
                            .ToArray();
                        this.Db.RemoveRange(removeItems);
                        this.Db.SaveChanges();

                        if (receiver.UseNotiReceiveFavorite)
                        {
                            await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                        }
                    }
                }

                return new { ProfilePagePartnerData = partner, birthdayMembers };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetAppealOnPartnerProfile(
            [FromForm] string apikey,
            [FromForm] int id,
            [FromForm] int skipCount)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Lat,
                        x.Lng,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var memberId = member.Id;
                var memberLat = member.Lat;
                var memberLng = member.Lng;

                var items = this.Db.PoomPooms
                    .Where(x => x.MemberId == id)
                    .Select(x => new
                    {
                        x.Id,
                        x.MemberId,
                        ProfileImage = x.Member.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .FirstOrDefault(),
                        x.Member.Nickname,
                        Age = DateTime.Today.Year - x.Member.BirthDay.Year + 1,
                        x.Member.Job,
                        Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Member.Lat, x.Member.Lng, memberLat, memberLng, 1, DistanceUnit.Kilometers)),
                        x.Content,
                        ContentImages = x.PoomPoom_Images
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .ToArray(),
                        IsLike = x.PoomPoom_Likes
                            .Any(z => z.MemberId == memberId),
                        LikeCount = x.PoomPoom_Likes
                            .Count,
                        CommentCount = x.PoomPoom_Comments.Count,
                        x.CreateTime,
                        Comment = x.PoomPoom_Comments
                            .OrderByDescending(z => z.CreateTime)
                            .Select(z => new
                            {
                                Member = this.Db.Members
                                    .Where(c => c.Id == z.MemberId)
                                    .Select(c => new
                                    {
                                        c.Id,
                                        c.Nickname,
                                        ProfileImage = c.Member_ProfileImages
                                            .OrderBy(v => v.CreateTime)
                                            .Select(v => v.Url)
                                            .FirstOrDefault()
                                    })
                                    .FirstOrDefault(),
                                ReplyNickname = this.Db.Members
                                    .Where(c => c.Id == z.ReplyMemberId)
                                    .Select(c => c.Nickname)
                                    .FirstOrDefault(),
                                z.Comment,
                            })
                            .Select(z => new
                            {
                                z.Comment,
                                MemberId = z.Member.Id,
                                z.Member.Nickname,
                                z.Member.ProfileImage,
                                z.ReplyNickname
                            })
                            .FirstOrDefault()
                    })
                    .OrderByDescending(x => x.CreateTime)
                    .Skip(skipCount)
                    .Take(10)
                    .ToArray();

                return new { items };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object UpdateNowShowMember(
            [FromForm] string apikey,
            [FromForm] int partnerid)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 접근입니다.");

                this.Db.Add(new Database.Tables.Member_NotShowMember
                {
                    MemberId = member.Id,
                    PartnerId = partnerid
                });
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public async Task<object> ExcuteChoice(
            [FromForm] string apikey,
            [FromForm] int partnerid)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname,
                        x.FreeChoiceTime,
                        x.FreeChoiceCount,
                        x.Point
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var tran = this.Db.Database.BeginTransaction();

                var freeChoiceTime = member.FreeChoiceTime;
                var freeChoiceCount = member.FreeChoiceCount;
                var point = member.Point;

                if (freeChoiceTime.HasValue && freeChoiceTime.Value > DateTime.Now)
                {

                }
                else if (freeChoiceCount > 0)
                {
                    freeChoiceCount--;
                    this.Db.Database.ExecuteSqlInterpolated($"UPDATE Member SET FreeChoiceCount = {freeChoiceCount} WHERE Id = {member.Id}");
                }
                else if (point >= 5)
                {
                    point -= 5;
                    this.Db.Database.ExecuteSqlInterpolated($"UPDATE Member SET Point = {point} WHERE Id = {member.Id}");

                    this.Db.Member_PointLogs.Add(new Member_PointLog
                    {
                        AcceptPoint = -5,
                        Comment = "좋아요",
                        CreateTime = DateTime.Now,
                        CurrentPoint = point + 5,
                        MemberId = member.Id
                    });
                    this.Db.SaveChanges();
                }
                else
                {
                    throw new Exception("딸기가 부족합니다.");
                }

                var item = new Member_ChoicePartner
                {
                    MemberId = member.Id,
                    PartnerId = partnerid,
                    CreateTime = DateTime.Now
                };
                this.Db.Add(item);
                this.Db.SaveChanges();

                tran.Commit();

                /// 알림처리
                {
                    var receiver = this.Db.Members
                        .Where(x => x.Id == partnerid)
                        .Select(x => new
                        {
                            x.Id,
                            x.PushToken,
                            x.UseNotiReceiveChoice,
                        })
                        .FirstOrDefault();

                    if (receiver != null)
                    {
                        var msg = $"{member.Nickname}님에게 좋아요를 받았습니다.";
                        this.Db.Add(new Member_Notification
                        {
                            CreateTime = DateTime.Now,
                            IsShow = false,
                            MemberId = receiver.Id,
                            Message = msg
                        });
                        this.Db.SaveChanges();

                        var removeItems = this.Db.Member_Notifications
                            .Where(x => x.MemberId == receiver.Id)
                            .OrderByDescending(x => x.CreateTime)
                            .Skip(5)
                            .ToArray();
                        this.Db.RemoveRange(removeItems);
                        this.Db.SaveChanges();

                        if (receiver.UseNotiReceiveChoice)
                        {
                            await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                        }
                    }
                }

                return new { freeChoiceCount, point };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public async Task<object> ExcuteSmartChoice(
            [FromForm] string apikey,
            [FromForm] int partnerid,
            [FromForm] string message)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname,
                        x.FreeSmartChoiceTime,
                        x.Point
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var tran = this.Db.Database.BeginTransaction();

                var freeSmartChoiceTime = member.FreeSmartChoiceTime;
                var point = member.Point;

                if (freeSmartChoiceTime.HasValue && freeSmartChoiceTime.Value > DateTime.Now)
                {

                }
                else if (point >= 10)
                {
                    point -= 10;
                    this.Db.Database.ExecuteSqlInterpolated($"UPDATE Member SET Point = {point} WHERE Id = {member.Id}");

                    this.Db.Add(new Member_PointLog
                    {
                        AcceptPoint = -10,
                        Comment = "스마트 딸기",
                        CreateTime = DateTime.Now,
                        CurrentPoint = point + 10,
                        MemberId = member.Id
                    });
                    this.Db.SaveChanges();
                }
                else
                {
                    throw new Exception("딸기가 부족합니다.");
                }

                var item = new Member_ChoicePartner
                {
                    MemberId = member.Id,
                    PartnerId = partnerid,
                    Message = message,
                    CreateTime = DateTime.Now
                };
                this.Db.Add(item);
                this.Db.SaveChanges();

                tran.Commit();

                /// 알림처리
                {
                    var receiver = this.Db.Members
                        .Where(x => x.Id == partnerid)
                        .Select(x => new
                        {
                            x.Id,
                            x.PushToken,
                            x.UseNotiReceiveChoice,
                        })
                        .FirstOrDefault();

                    if (receiver != null)
                    {
                        var msg = $"{member.Nickname}님에게 좋아요를 받았습니다.";
                        this.Db.Add(new Member_Notification
                        {
                            CreateTime = DateTime.Now,
                            IsShow = false,
                            MemberId = receiver.Id,
                            Message = msg
                        });
                        this.Db.SaveChanges();

                        var removeItems = this.Db.Member_Notifications
                            .Where(x => x.MemberId == receiver.Id)
                            .OrderByDescending(x => x.CreateTime)
                            .Skip(5)
                            .ToArray();
                        this.Db.RemoveRange(removeItems);
                        this.Db.SaveChanges();

                        if (receiver.UseNotiReceiveChoice)
                        {
                            await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                        }
                    }
                }

                return new { point };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExecuteHotstrawberry(
            [FromForm] string apikey)
        {
            try
            {
                using (var tran = this.Db.Database.BeginTransaction())
                {
                    var member = this.Db.Members
                        .Where(x => x.ApiKey == apikey)
                        .FirstOrDefault();

                    if (member == null)
                        throw new Exception("잘못된 요청입니다.");

                    var time = DateTime.Now;
                    var item = new Database.Tables.Member_Hotstrawberry
                    {
                        MemberId = member.Id,
                        StartTime = time,
                        CreateTime = time,
                    };
                    this.Db.Add(item);
                    this.Db.SaveChanges();

                    if (member.Point < 15)
                        throw new Exception("딸기가 부족합니다.");

                    member.Point -= 15;
                    this.Db.SaveChanges();

                    this.Db.Add(new Member_PointLog
                    {
                        AcceptPoint = -15,
                        Comment = "핫딸기 부스팅",
                        CreateTime = DateTime.Now,
                        CurrentPoint = member.Point,
                        MemberId = member.Id
                    });
                    this.Db.SaveChanges();

                    tran.Commit();

                    return new { item.Id, item.StartTime };
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object CheckHotStrawberry(
            [FromForm] string apikey)
        {
            try
            {
                var startTime = this.Db.Member_Hotstrawberrys
                    .OrderByDescending(x => x.StartTime)
                    .Select(x => x.StartTime)
                    .FirstOrDefault();

                var viewCount = 0;
                if (startTime != null)
                {
                    viewCount = this.Db.Member_StarPoints
                        .Count(x => x.Member.ApiKey == apikey && x.CreateTime >= startTime.Value);
                }

                return new { viewCount };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public async Task<object> ExcuteStarPoint(
            [FromForm] string apikey,
            [FromForm] int partnerid,
            [FromForm] int starpoint)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname,
                        x.Gender,
                        StarPoints = x.Member_StarPoints
                            .Where(z => z.PartnerId.HasValue)
                            .Select(z => z.PartnerId.Value),
                        x.Lat,
                        x.Lng,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var item = this.Db.Member_StarPoints
                    .Where(x => x.MemberId == member.Id && x.PartnerId == partnerid)
                    .FirstOrDefault();

                if (item == null)
                {
                    this.Db.Add(new Member_StarPoint
                    {
                        MemberId = member.Id,
                        PartnerId = partnerid,
                        StarPoint = starpoint,
                        CreateTime = DateTime.Now,
                    });
                }
                else
                {
                    item.StarPoint = starpoint;
                    item.CreateTime = DateTime.Now;
                }
                
                this.Db.SaveChanges();

                /// 알림처리
                {
                    var receiver = this.Db.Members
                        .Where(x => x.Id == partnerid)
                        .Select(x => new
                        {
                            x.Id,
                            x.PushToken,
                            x.UseNotiReceiveFavorite,
                        })
                        .FirstOrDefault();

                    if (receiver != null)
                    {
                        var msg = $"{member.Nickname}님이 관심을 보입니다.";
                        this.Db.Add(new Member_Notification
                        {
                            CreateTime = DateTime.Now,
                            IsShow = false,
                            MemberId = receiver.Id,
                            Message = msg
                        });
                        this.Db.SaveChanges();

                        var removeItems = this.Db.Member_Notifications
                            .Where(x => x.MemberId == receiver.Id)
                            .OrderByDescending(x => x.CreateTime)
                            .Skip(5)
                            .ToArray();
                        this.Db.RemoveRange(removeItems);
                        this.Db.SaveChanges();

                        if (receiver.UseNotiReceiveFavorite)
                        {
                            await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                        }
                    }
                }

                var view04Item = this.Db.Members
                    .Where(x => x.MemberState == MemberStateTypes.Normal)
                    .Where(x => x.Gender != member.Gender)
                    .Where(x => !member.StarPoints.Any(z => z == x.Id))
                    .Where(x => x.Id != partnerid)
                    .Select(x => new
                    {
                        Id = x.Id,
                        ProfileImages = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .ToArray(),
                        Nickname = x.Nickname,
                        Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                        Job = x.Job,
                        IsRoyal = x.IsRoyal,
                        IsVIP = x.IsVIP,
                        HasVoice = !string.IsNullOrWhiteSpace(x.FirstVoice),
                        HasStarBadge = x.HasStarBadge,
                        FirstMessage = x.FirstMessage,
                        SecondMessage = $"음주는 {(x.Alcohol == "안함" ? "안해요" : x.Alcohol + "해요")}",
                        Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                        IsNowJoin = x.CreateTime.Date == DateTime.Today,
                        IsLive = x.LastLoginTime.HasValue ? (DateTime.Now - x.LastLoginTime.Value).TotalHours < 8 : false,
                        CharmingPoints = x.Member_CharmingPoints
                            .Select(z => z.Name)
                            .ToArray(),
                        Interests = x.Member_Interests
                            .Select(z => z.Name)
                            .ToArray(),
                        Hotstrawberrys = x.Member_Hotstrawberrys
                            .OrderByDescending(z => z.StartTime)
                            .Select(z => z.StartTime)
                            .FirstOrDefault(),
                        x.CreateTime
                    })
                    .ToArray()
                    .Where(x => x.Range <= 200)
                    .OrderBy(x => x.Hotstrawberrys.HasValue && x.Hotstrawberrys.Value.AddHours(1) > DateTime.Now ? 0 : 1)
                    .ThenByDescending(x => x.CreateTime)
                    .FirstOrDefault();

                return new { view04Item };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteProfileAlert(
            [FromForm] string apikey,
            [FromForm] int partnerid,
            [FromForm] int alertType,
            [FromForm] string alertMessage,
            [FromForm] bool isBlocked)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var tran = this.Db.Database.BeginTransaction();

                var title = default(string);
                var message = default(string);

                switch (alertType)
                {
                    case 1:
                        title = "불쾌한 대화";
                        message = "욕설, 비난, 모욕, 성희롱, 차별";
                        break;
                    case 2:
                        title = "불쾌한 노출";
                        message = "상반신, 하반신 과다노출";
                        break;
                    case 3:
                        title = "허위 프로필, 스팸";
                        message = "다른 이를 사칭, 사진 도용, 상업적인 성 서비스";
                        break;
                    case 4:
                        title = "부적절한 프로필 정보";
                        message = "잘못된 프로필 정보, SNS 입력";
                        break;
                    case 5:
                        title = "기타";
                        message = alertMessage;
                        break;
                    default:
                        break;
                }

                var item = new Member_AlertProfile
                {
                    MemberId = member.Id,
                    PartnerId = partnerid,
                    Title = title,
                    Message = message,
                    CreateTime = DateTime.Now,
                };
                this.Db.Add(item);
                this.Db.SaveChanges();

                if (isBlocked)
                {
                    var item2 = new Member_BlockPartner
                    {
                        MemberId = member.Id,
                        PartnerId = partnerid,
                        CreateTime = DateTime.Now,
                    };
                    this.Db.Add(item2);
                    this.Db.SaveChanges();
                }

                tran.Commit();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteProfileBlock(
            [FromForm] string apikey,
            [FromForm] int partnerid)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var tran = this.Db.Database.BeginTransaction();

                var item = new Member_BlockPartner
                {
                    MemberId = member.Id,
                    PartnerId = partnerid,
                    CreateTime = DateTime.Now,
                };
                this.Db.Add(item);
                this.Db.SaveChanges();

                tran.Commit();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetRecommandPartners(
            [FromForm] string apikey,
            [FromForm] int datatype,
            [FromForm] int skipcount)
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
                        ChoicePartners = x.Member_ChoicePartners
                            .Select(z => z.PartnerId),
                        Priority = x.Member_Preference
                            .Priority,
                        x.Member_Preference,
                        BlockPartners = x.Member_BlockPartners
                            .Select(x => x.PartnerId)
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var choicePartners = member.ChoicePartners.ToArray();
                var blockPartners = member.BlockPartners.ToArray();

                var query = this.Db.Members
                    .Where(x => x.Gender != member.Gender)
                    .Where(x => x.MemberState == MemberStateTypes.Normal)
                    .Where(x => !choicePartners.Any(z => z == x.Id))
                    .Where(x => !blockPartners.Any(z => z == x.Id));

                switch (datatype)
                {
                    case 1:
                    {
                        var partners = query
                            .Where(x => x.LevelType >= LevelTypes.Platinum || x.IsRoyal || x.IsVIP)
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderByDescending(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 2:
                    {
                        var partners = query
                            .Where(x => x.HasStarBadge)
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderByDescending(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 3:
                    {
                        var partners = query
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                                x.Tall,
                            })
                            .ToArray()
                            .Where(x => member.Member_Preference.MinTall <= x.Tall && member.Member_Preference.MaxTall >= x.Tall)
                            .Where(x => member.Member_Preference.MinAge <= x.Age && member.Member_Preference.MaxAge >= x.Age)
                            .Where(x => member.Member_Preference.Range >= x.Range)
                            .OrderByDescending(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 4:
                    {
                        var partners = query
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                                x.RateResponse
                            })
                            .ToArray()
                            .OrderByDescending(x => x.RateResponse)
                            .ThenBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 5:
                    {
                        var partners = query
                            .Where(x => x.Alcohol == "자주")
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 6:
                    {
                        var partners = query
                            .Where(x => x.Smoking == "자주" || x.Alcohol == "자주")
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 7:
                    {
                        var partners = query
                            .Where(x => x.Member_Interests.Any(z => z.Name.Contains("맛집")))
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 8:
                    {
                        var partners = query
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .Where(x => x.Range <= 10)
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 9:
                    {
                        var partners = query
                            .Where(x => x.Member_Interests.Any(z => z.Name.Contains("술") || z.Name.Contains("게임") || z.Name.Contains("패션") || z.Name.Contains("반려동물")))
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 10:
                    {
                        var partners = query
                            .Where(x => x.BodyStyle == member.Member_Preference.Body)
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 11:
                    {
                        var partners = query
                            .Where(x => member.Member_Preference.MinTall <= x.Tall && member.Member_Preference.MaxTall >= x.Tall)
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    case 12:
                    {
                        var partners = query
                            .Where(x => x.Member_Interests.Any(z => z.Name.Contains("스포츠") || z.Name.Contains("운동")))
                            .Select(x => new
                            {
                                x.Id,
                                ProfileImage = x.Member_ProfileImages
                                        .OrderBy(z => z.CreateTime)
                                        .Select(z => z.Url)
                                        .FirstOrDefault(),
                                x.Nickname,
                                Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                                x.Job,
                                Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Lat, x.Lng, member.Lat, member.Lng, 1, DistanceUnit.Kilometers)),
                            })
                            .ToArray()
                            .OrderBy(x => x.Range)
                            .Skip(skipcount)
                            .Take(20)
                            .ToArray();

                        return new { partners };
                    }
                    default:
                        return new { };
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetFortunePartnet(
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
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var gender = member.Gender;

                var partner = this.Db.Members
                    .Where(x => x.MemberState == MemberStateTypes.Normal)
                    .Where(x => x.LevelType >= LevelTypes.Platinum)
                    .Where(x => x.HasStarBadge)
                    .Where(x => x.Gender != gender)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImage = x.Member_ProfileImages
                            .OrderBy(x => x.CreateTime)
                            .Select(x => x.Url)
                            .FirstOrDefault(),
                        OrderIdx = this.Db.Rand()
                    })
                    .OrderBy(x => x.OrderIdx)
                    .FirstOrDefault();

                var message = this.Db.LoveFortunes
                    .OrderBy(x => this.Db.Rand())
                    .Select(x => x.Message)
                    .FirstOrDefault();

                return new { partner, message };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetNotifications(
            [FromForm] string apikey)
        {
            try
            {
                using (var tran = this.Db.Database.BeginTransaction())
                {
                    var items = this.Db.Member_Notifications
                        .Where(x => x.Member.ApiKey == apikey)
                        .OrderByDescending(x => x.CreateTime)
                        .ToArray();

                    foreach (var item in items)
                    {
                        item.IsShow = true;
                        this.Db.SaveChanges();
                    }

                    tran.Commit();

                    return new
                    {
                        items = items
                            .Select(x => x.Message)
                            .ToArray()
                    };
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetHasNewNotifications(
            [FromForm] string apikey)
        {
            try
            {
                var hasValue = this.Db.Member_Notifications
                    .Any(x => x.Member.ApiKey == apikey && !x.IsShow);

                return new { hasValue };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetMainPopupAds()
        {
            try
            {
                var datas = this.Db.ADDatas
                    .Where(x => x.ADType == ADTypes.MainPopup)
                    .OrderByDescending(x => x.CreateTime)
                    .ToArray();

                return new { datas };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetSettingBannerAds()
        {
            try
            {
                var data = this.Db.ADDatas
                    .Where(x => x.ADType == ADTypes.SettingBanner)
                    .OrderByDescending(x => x.CreateTime)
                    .FirstOrDefault();

                return new { data };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
    }
}
