using Geolocation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;
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
    public class OptionController : ControllerBase
    {
        public IWebHostEnvironment Env { get; }
        public DatabaseContext Db { get; }
        public ImageHelper ImageHelper { get; }
        public FirebaseHelper Firebase { get; }

        public OptionController(IWebHostEnvironment env, DatabaseContext db, ImageHelper imageHelper)
        {
            this.Env = env;
            this.Db = db;
            this.ImageHelper = imageHelper;
        }

        [HttpPost]
        public object GetOptionData(
            [FromForm] string apikey)
        {
            try
            {
                var ad = this.Db.ADDatas.Where(z => z.ADType == ADTypes.SettingBanner)
                                        .OrderByDescending(z => z.CreateTime)
                                        .FirstOrDefault();

                if (ad == null)
                {
                    ad = new ADData
                    {
                        Id = 0,
                        JsonData = "{}"
                    };
                }

                var adData = JsonConvert.DeserializeAnonymousType(ad.JsonData, new { ImageUrl = default(string) });

                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImage = x.Member_ProfileImages
                            .OrderBy(x => x.CreateTime)
                            .Select(x => x.Url)
                            .FirstOrDefault(),
                        x.Nickname,
                        Age = DateTime.Today.Year - x.BirthDay.Year + 1,
                        x.LevelType,
                        x.IsRoyal,
                        x.IsVIP,
                        Is10Star = x.HasStarBadge,
                        x.Job,
                        IsConfirmStep1 = x.Member_ProfileImages.Count >= 2,
                        IsConfirmStep2 = x.Member_ProfileImages.Count >= 6,
                        IsConfirmStep3 = !string.IsNullOrWhiteSpace(x.JobName)
                                      && !string.IsNullOrWhiteSpace(x.SchoolName)
                                      && !string.IsNullOrWhiteSpace(x.Personality)
                                      && !string.IsNullOrWhiteSpace(x.Blood),
                        IsConfirmStep4 = x.Member_CharmingPoints.Count >= 3
                                      && x.Member_Interests.Count >= 3,
                        x.Point,
                        ADId = ad.Id,
                        ADImageUrl = adData.ImageUrl,
                        ADLink = ad.Link
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                return new { Item = member };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetSettingPageData(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.UseNotiRecommand,
                        x.UseNotiReceiveChoice,
                        x.UseNotiSendChoiceConfirm,
                        x.UseNotiReceiveFavorite,
                        x.UseNotiConnect,
                        x.UseNotiChattingMessage,
                        x.UseNotiAppeal,
                        x.UseNotiMarketing
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                return new { Item = member };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiRecommand(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiRecommand = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiReceiveChoice(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiReceiveChoice = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiSendChoiceConfirm(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiSendChoiceConfirm = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiReceiveFavorite(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiReceiveFavorite = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiConnect(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiConnect = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiChattingMessage(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiChattingMessage = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiAppeal(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiAppeal = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteToggleUseNotiMarketing(
            [FromForm] string apikey,
            [FromForm] bool use)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                member.UseNotiMarketing = use;
                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetProfileLevelPageData(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        ProfileImages = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url),
                        x.LevelType,
                        x.LevelPoint
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                return new { Item = member };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteMemberLevelReCheck(
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

                    var memberId = member.Id;
                    this.Db.Add(new Member_RequestMemberLevel
                    {
                        CreateTime = DateTime.Now,
                        MemberId = memberId,
                    });
                    this.Db.SaveChanges();

                    member.Point -= 10;
                    this.Db.SaveChanges();

                    this.Db.Add(new Member_PointLog
                    {
                        AcceptPoint = -10,
                        Comment = "등급평가 요청",
                        CreateTime = DateTime.Now,
                        CurrentPoint = member.Point,
                        MemberId = member.Id
                    });
                    this.Db.SaveChanges();

                    tran.Commit();

                    return new { };
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetHasProfileRecheckLog(
            [FromForm] string apikey)
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

                var memberId = member.Id;
                var hasValue = this.Db.Member_RequestMemberLevels
                    .Any(x => x.MemberId == memberId && !x.IsComplete);

                return new { hasValue };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetPointLogs(
            [FromForm] string apikey,
            [FromForm] int skipCount)
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

                var memberId = member.Id;

                var items = this.Db.Member_PointLogs
                    .Where(x => x.MemberId == memberId)
                    .OrderByDescending(x => x.CreateTime)
                    .Skip(skipCount)
                    .Take(50)
                    .ToArray();

                return new { items };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetRoyalCenterRequestPageData(
            [FromForm] string apikey)
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

                var managerEmail = this.Db.Settings
                    .Select(x => x.ManagerEmail)
                    .FirstOrDefault();

                return new { managerEmail };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetHasRequestRoyal(
            [FromForm] string apikey)
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

                var hasItem = this.Db.Member_RequestRoyals
                    .Any(x => x.MemberId == member.Id && !x.IsComplete);

                return new { hasItem };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetShareCode(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.RecommandCode
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                return new { Code = member.RecommandCode };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetFirstMessagePageData(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.FirstMessage,
                        x.FirstVoice,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                return new
                {
                    FirstMessage = member.FirstMessage,
                    VoicePath = member.FirstVoice
                };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteFirstMessage(
            [FromForm] string apikey,
            [FromForm] string firstMessage,
            [FromForm] bool isRemoveVoice,
            IFormFile voice)
        {
            try
            {
                if (firstMessage?.Length > 300)
                    throw new Exception("300자 이내로 작성해주세요.");

                using (var tran = this.Db.Database.BeginTransaction())
                {
                    var member = this.Db.Members
                        .Where(x => x.ApiKey == apikey)
                        .FirstOrDefault();

                    if (member == null)
                        throw new Exception("잘못된 요청입니다.");

                    var bw = this.Db.BlockWords
                        .Select(x => x.Word)
                        .ToArray();
                    if (bw.Any(x => firstMessage.Contains(x)))
                        throw new Exception("금칙어가 포함되어 있습니다.");

                    member.FirstMessage = firstMessage;
                    if (isRemoveVoice && !string.IsNullOrWhiteSpace(member.FirstVoice))
                    {
                        var filePath = member.FirstVoice.Replace($"{Request.Scheme}://{Request.Host}", this.Env.WebRootPath);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        member.FirstVoice = null;
                    }

                    if (voice != null)
                    {
                        var createFilePath = Path.Combine(this.Env.WebRootPath, "voice", $"{Guid.NewGuid().ToString().ToLower().Replace("-", "")}{Path.GetExtension(voice.FileName)}");
                        var fileInfo = new FileInfo(createFilePath);
                        
                        if (!fileInfo.Directory.Exists)
                            fileInfo.Directory.Create();

                        using (var stream = voice.OpenReadStream())
                        using (var fileStream = fileInfo.Create())
                        {
                            stream.CopyTo(fileStream);
                        }

                        var url = createFilePath.Replace(this.Env.WebRootPath, $"{Request.Scheme}://{Request.Host}").Replace("\\", "/");

                        member.FirstVoice = url;
                    }

                    this.Db.SaveChanges();
                    tran.Commit();

                    return new { }; 
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetSendChoicesPageData(
            [FromForm] string apikey,
            [FromForm] int skipCount)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var memberId = member.Id;
                var items = this.Db.Member_ChoicePartners
                    .Where(x => x.MemberId == memberId)
                    .Where(x => x.PartnerId.HasValue)
                    .Where(x => !x.IsConfirm)
                    .Select(x => new
                    {
                        x.Id,
                        MemberId = x.PartnerId,
                        ProfileImage = x.Partner.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .FirstOrDefault(),
                        Nickname = x.Partner.Nickname,
                        Age = DateTime.Today.Year - x.Partner.BirthDay.Year + 1,
                        Job = x.Partner.Job,
                        x.CreateTime
                    })
                    .OrderBy(x => x.CreateTime)
                    .Skip(skipCount)
                    .Take(20)
                    .ToArray();

                return new
                {
                    Items = items
                };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteRemoveChoice(
            [FromForm] string apikey,
            [FromForm] int choiceId,
            [FromForm] int partnerId)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var memberId = member.Id;

                var item = this.Db.Member_ChoicePartners
                    .FirstOrDefault(x => x.Id == choiceId && x.MemberId == memberId && x.PartnerId == partnerId);

                this.Db.Remove(item);

                this.Db.SaveChanges();

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetManagerEmail(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var managerEmail = this.Db.Settings
                    .Select(x => x.ManagerEmail)
                    .FirstOrDefault();

                return new { managerEmail };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetHelpPageData(
            [FromForm] string apikey)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var items = this.Db.HelpMessages
                    .OrderBy(x => x.CreateTime)
                    .Select(x => new
                    {
                        x.Id,
                        x.Title,
                        x.Content
                    })
                    .ToArray();

                return new { items };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetUpdateProfilePageData(
            [FromForm] string apikey)
        {
            try
            {
                var item = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImages = x.Member_ProfileImages
                            .OrderBy(x => x.CreateTime)
                            .Select(z => new
                            {
                                ProfileImageId = z.Id,
                                ProfileImage = z.Url
                            })
                            .ToArray(),
                        x.Nickname,
                        x.Gender,
                        x.BirthDay,
                        x.Tall,
                        x.BodyStyle,
                        x.JobName,
                        x.Job,
                        x.School,
                        x.SchoolName,
                        x.Personality,
                        x.Religion,
                        x.Alcohol,
                        x.Smoking,
                        x.Blood,
                        IsConfirmPhoneNumber = !string.IsNullOrWhiteSpace(x.Member_Account.PhoneNumber),
                        CharmingPoints = x.Member_CharmingPoints
                            .Select(z => new
                            {
                                z.Id,
                                z.Name
                            })
                            .ToArray(),
                        Interests = x.Member_Interests
                            .Select(z => new
                            {
                                z.Id,
                                z.Name
                            })
                            .ToArray(),
                        x.IsRoyal
                    })
                    .FirstOrDefault();

                return item;
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteUpdateMember(
            [FromForm] string apikey,
            [FromForm] int[] newImageIds,
            [FromForm] string bodyStyle,
            [FromForm] string jobName,
            [FromForm] string school,
            [FromForm] string schoolName,
            [FromForm] string personality,
            [FromForm] string religion,
            [FromForm] string alcohol,
            [FromForm] string smoking,
            [FromForm] string blood,
            [FromForm] string[] charmingPoints,
            [FromForm] string[] interests,
            IFormFile[] newImages)
        {
            try
            {
                using (var tran = this.Db.Database.BeginTransaction())
                {
                    var member = this.Db.Members
                        .Where(x => x.ApiKey == apikey)
                        .FirstOrDefault();

                    /// 회원정보 적용
                    {
                        member.BodyStyle = bodyStyle;
                        member.JobName = jobName;
                        member.School = school;
                        member.SchoolName = schoolName;
                        member.Personality = personality;
                        member.Religion = religion;
                        member.Alcohol = alcohol;
                        member.Smoking = smoking;
                        member.Blood = blood;
                        member.Blood = blood;

                        this.Db.SaveChanges();
                    }

                    /// 프로필 이미지 적용
                    {
                        if (newImageIds != null)
                        {
                            for (int i = 0; i < newImageIds.Length; i++)
                            {
                                var id = newImageIds[i];
                                var image = newImages[i];

                                using var stream = image.OpenReadStream();
                                var path = this.ImageHelper.SaveImageFromStream(stream, "profile", image.FileName);

                                if (id == 0)
                                {
                                    this.Db.Add(new Member_ConfirmImage
                                    {
                                        ContentId = member.Id,
                                        ImageType = ConfirmImageTypes.ProfileImage,
                                        ImageUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
                                        MemberId = member.Id,
                                    });

                                    //this.Db.Add(new Member_ProfileImage
                                    //{
                                    //    MemberId = member.Id,
                                    //    Url = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
                                    //    Path = path.local,
                                    //    Ratio = 0d,
                                    //    CreateTime = DateTime.Now,
                                    //});
                                }
                                else
                                {
                                    this.Db.Add(new Member_ConfirmImage
                                    {
                                        ContentId = member.Id,
                                        ImageType = ConfirmImageTypes.ProfileImage,
                                        ImageUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
                                        MemberId = member.Id,
                                        ImageId = id,
                                    });

                                    //var member_ProfileImage = this.Db.Member_ProfileImages
                                    //    .Where(x => x.Id == id)
                                    //    .FirstOrDefault();
                                    //if (member_ProfileImage != null)
                                    //{
                                    //    member_ProfileImage.Url = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}";
                                    //    member_ProfileImage.Path = path.local;
                                    //}
                                }
                            }
                        }

                        this.Db.SaveChanges();
                    }

                    /// 매력포인트 적용
                    {
                        var member_CharmingPoints = this.Db.Member_CharmingPoints
                            .Where(x => x.MemberId == member.Id)
                            .ToArray();
                        this.Db.RemoveRange(member_CharmingPoints);

                        if (charmingPoints != null)
                        {
                            foreach (var name in charmingPoints)
                            {
                                this.Db.Add(new Member_CharmingPoint
                                {
                                    MemberId = member.Id,
                                    Name = name
                                });
                            }
                        }

                        this.Db.SaveChanges();
                    }

                    /// 관심사 적용
                    {
                        var member_Interests = this.Db.Member_Interests
                            .Where(x => x.MemberId == member.Id)
                            .ToArray();
                        this.Db.RemoveRange(member_Interests);

                        if (interests != null)
                        {
                            foreach (var name in interests)
                            {
                                this.Db.Add(new Member_Interest
                                {
                                    MemberId = member.Id,
                                    Name = name
                                });
                            }
                        }

                        this.Db.SaveChanges();
                    }

                    /// 프로필 완성도 포인트 지급
                    {
                        var data = this.Db.Members
                            .Where(x => x.ApiKey == apikey)
                            .Select(x => new bool[]
                            {
                                x.Member_ProfileImages.Count >= 2,
                                x.Member_ProfileImages.Count >= 6,
                                !string.IsNullOrWhiteSpace(x.JobName)
                                && !string.IsNullOrWhiteSpace(x.SchoolName)
                                && !string.IsNullOrWhiteSpace(x.Personality)
                                && !string.IsNullOrWhiteSpace(x.Blood),
                                x.Member_CharmingPoints.Count >= 3
                                && x.Member_Interests.Count >= 3,
                            })
                            .FirstOrDefault();

                        if (data.Count(x => x == true) >= 2)
                        {
                            var comment = "프로필 2단계 완성";
                            var hasValue = this.Db.Member_PointLogs
                                .Any(x => x.Comment == comment);
                            if (!hasValue)
                            {
                                member.Point += 10;
                                this.Db.SaveChanges();

                                this.Db.Add(new Member_PointLog
                                {
                                    MemberId = member.Id,
                                    AcceptPoint = 10,
                                    CurrentPoint = member.Point,
                                    Comment = comment,
                                    CreateTime = DateTime.Now,
                                });
                                this.Db.SaveChanges();
                            }
                        }

                        if (data.Count(x => x == true) >= 3)
                        {
                            var comment = "프로필 3단계 완성";
                            var hasValue = this.Db.Member_PointLogs
                                .Any(x => x.Comment == comment);
                            if (!hasValue)
                            {
                                member.Point += 15;
                                this.Db.SaveChanges();

                                this.Db.Add(new Member_PointLog
                                {
                                    MemberId = member.Id,
                                    AcceptPoint = 15,
                                    CurrentPoint = member.Point,
                                    Comment = comment,
                                    CreateTime = DateTime.Now,
                                });
                                this.Db.SaveChanges();
                            }
                        }
                    }

                    tran.Commit();

                    var profileImages = this.Db.Member_ProfileImages
                        .Where(x => x.MemberId == member.Id)
                        .OrderBy(x => x.CreateTime)
                        .Select(x => new
                        {
                            x.Id,
                            ProfileImageSource = x.Url
                        })
                        .ToArray();

                    return new { profileImages, member.Point };
                }
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetUpdatePreferencePageData(
            [FromForm] string apikey)
        {
            try
            {
                var item = this.Db.Member_Preferences
                    .Where(x => x.Member.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Member.Gender,
                        x.MinAge,
                        x.MaxAge,
                        x.Range,
                        x.MinTall,
                        x.MaxTall,
                        x.BeautyOrWealth,
                        x.Body,
                        x.Religion,
                        x.Alcohol,
                        x.Smoking,
                        x.Priority
                    })
                    .FirstOrDefault();

                return item;
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteUpdatePreference(
            [FromForm] string apikey,
            [FromForm] int minAge,
            [FromForm] int maxAge,
            [FromForm] int range,
            [FromForm] int minTall,
            [FromForm] int maxTall,
            [FromForm] bool beautyOrWealth,
            [FromForm] string body,
            [FromForm] string religion,
            [FromForm] string alcohol,
            [FromForm] string smoking,
            [FromForm] PriorityTypes priority)
        {
            try
            {
                using (var tran = this.Db.Database.BeginTransaction())
                {
                    var item = this.Db.Member_Preferences
                        .Where(x => x.Member.ApiKey == apikey)
                        .FirstOrDefault();

                    if (item == null)
                        throw new Exception("잘못된 요청입니다.");

                    item.MinAge = minAge;
                    item.MaxAge = maxAge;
                    item.Range = range;
                    item.MinTall = minTall;
                    item.MaxTall = maxTall;
                    item.BeautyOrWealth = beautyOrWealth;
                    item.Body = body;
                    item.Religion = religion;
                    item.Alcohol = alcohol;
                    item.Smoking = smoking;
                    item.Priority = priority;

                    this.Db.SaveChanges();

                    tran.Commit();
                }

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetSettingAccountPageData(
            [FromForm] string apikey)
        {
            try
            {
                var PageData = this.Db.Member_Accounts
                    .Where(x => x.Member.ApiKey == apikey)
                    .Select(x => new
                    {
                        Email = x.Email,
                        HasPassword = !string.IsNullOrWhiteSpace(x.Passwd),
                        PhoneNumber = x.PhoneNumber,
                    })
                    .FirstOrDefault();

                return new { PageData };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
    }
}
