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
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PoomPoomController : ControllerBase
    {
        public DatabaseContext Db { get; }
        public ImageHelper ImageHelper { get; }
        public FirebaseHelper FirebaseHelper { get; }

        public PoomPoomController(DatabaseContext db, ImageHelper imageHelper, FirebaseHelper firebaseHelper)
        {
            this.Db = db;
            this.ImageHelper = imageHelper;
            this.FirebaseHelper = firebaseHelper;
        }

        [HttpPost]
        public object GetPoomPooms(
            [FromForm] string apikey,
            [FromForm] ContentTypes contentType,
            [FromForm] int skipCount)
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

                var Top5Items = default(object[]);
                if (skipCount == 0)
                {
                    var members = this.Db.PoomPooms
                        .Select(x => new
                        {
                            x.MemberId,
                            Likes = x.PoomPoom_Likes
                                .Where(z => z.CreateTime.AddDays(1).Date == DateTime.Today)
                                .Select(z => z.CreateTime)
                                .OrderBy(z => z)
                                .Take(100)
                                .ToArray()
                        })
                        .Where(x => x.Likes.Length >= 100)
                        .Select(x => new
                        {
                            x.MemberId,
                            Time = x.Likes
                                .LastOrDefault()
                        })
                        .OrderBy(x => x.Time)
                        .Take(5)
                        .ToArray();

                    var memberIds = members
                        .Select(x => x.MemberId)
                        .ToArray();

                    Top5Items = this.Db.Members
                        .Where(x => memberIds.Any(z => z == x.Id))
                        .ToArray()
                        .Select(x => new
                        {
                            x.Id,
                            ProfileImage = x.Member_ProfileImages
                                .OrderBy(z => z.CreateTime)
                                .Select(z => z.Url)
                                .FirstOrDefault(),
                            Time = members
                                .Where(z => z.MemberId == x.Id)
                                .Select(z => z.Time)
                                .FirstOrDefault(),
                            
                        })
                        .OrderBy(x => x.Time)
                        .ToArray();


                }

                var Items = this.Db.PoomPooms
                    .Where(x => x.ContentType == contentType)
                    .Where(x => contentType == ContentTypes.Boast ? x.Member.Gender != memberGender : true)
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
                        x.Area,
                        x.Time,
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
                    .Take(18)
                    .ToArray();

                return new { Items };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object GetPoomPoom(
            [FromForm] string apikey,
            [FromForm] int contentId)
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
                        Blocks = x.Member_BlockPartners
                            .Where(z => z.PartnerId.HasValue)
                            .Select(z => z.PartnerId.Value)
                    })
                    .FirstOrDefault();

                var memberId = member.Id;
                var memberLat = member.Lat;
                var memberLng = member.Lng;
                var blocks = member.Blocks.ToArray();

                var Item = this.Db.PoomPooms
                    .Where(x => x.Id == contentId)
                    .Where(x => !blocks.Any(z => z == x.MemberId))
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
                        Range = GeoCalculator.GetDistance(x.Member.Lat, x.Member.Lng, memberLat, memberLng, 1, DistanceUnit.Kilometers),
                        x.Content,
                        x.Area,
                        x.Time,
                        Images = x.PoomPoom_Images
                            .OrderBy(z => z.CreateTime)
                            .Select(z => new
                            {
                                z.Url
                            })
                            .ToArray(),
                        Likes = x.PoomPoom_Likes.Count,
                        UseLike = x.PoomPoom_Likes
                            .Any(z => z.MemberId == memberId),
                        x.UseComment,
                        Comments = x.PoomPoom_Comments
                            .OrderBy(z => z.CreateTime)
                            .Select(z => new
                            {
                                CommentId = z.Id,
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
                                z.CreateTime
                            })
                            .Select(z => new
                            {
                                z.CommentId,
                                z.Comment,
                                MemberId = z.Member.Id,
                                z.Member.Nickname,
                                z.Member.ProfileImage,
                                z.ReplyNickname,
                                z.CreateTime
                            })
                            .ToArray()
                    })
                    .FirstOrDefault();

                return new { Item };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcutePoomPoom(
            [FromForm] string apikey,
            [FromForm] ContentTypes contentType,
            [FromForm] string content,
            [FromForm] string area,
            [FromForm] string time,
            [FromForm] bool useComment,
            [FromForm] IFormFile[] images)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var bw = this.Db.BlockWords
                        .Select(x => x.Word)
                        .ToArray();
                if (bw.Any(x => content.Contains(x)))
                    throw new Exception("금칙어가 포함되어 있습니다.");

                var tran = this.Db.Database.BeginTransaction();

                var item01 = new PoomPoom
                {
                    ContentType = contentType,
                    Content = content,
                    Area = area,
                    Time = time,
                    UseComment = useComment,
                    IsConfirm = false,
                    CreateTime = DateTime.Now,
                    MemberId = member.Id,
                };
                this.Db.Add(item01);
                this.Db.SaveChanges();

                if (images != null)
                {
                    foreach (var image in images)
                    {
                        using var stream = image.OpenReadStream();
                        var path = this.ImageHelper.SaveImageFromStream(stream, "poompoom", image.FileName);

                        var item02 = new Member_ConfirmImage
                        {
                            ContentId = item01.Id,
                            ImageType = ConfirmImageTypes.PoomPoomImage,
                            ImageUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
                            MemberId = member.Id,
                        };

                        //var item02 = new PoomPoom_Image
                        //{
                        //    PoomPoomId = item01.Id,
                        //    Url = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
                        //    CreateTime = DateTime.Now
                        //};

                        this.Db.Add(item02);
                        this.Db.SaveChanges();
                    }
                }

                if (contentType == ContentTypes.Boast ||
                    contentType == ContentTypes.Metting ||
                    contentType == ContentTypes.Sell)
                {
                    member.Point += 1;

                    this.Db.Add(new Member_PointLog
                    {
                        MemberId = member.Id,
                        AcceptPoint = +1,
                        Comment = "뿜뿜 게시글 등록",
                        CurrentPoint = member.Point,
                        CreateTime = DateTime.Now,
                    });
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
        public object ExcuteAlertPoomPoom(
            [FromForm] string apikey,
            [FromForm] string title,
            [FromForm] string message,
            [FromForm] int poomPoomId,
            [FromForm] int alertMemberId)
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

                var tran = this.Db.Database.BeginTransaction();

                var memberId = member.Id;

                var item = new Member_AlertPoomPoom
                {
                    MemberId = memberId,
                    Title = title,
                    Message = message,
                    PoomPoomId = poomPoomId,
                    AlertMemberId = alertMemberId,
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
        public async Task<object> ExcutePoomPoomLike(
            [FromForm] string apikey,
            [FromForm] int contentId)
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

                var memberId = member.Id;

                var item = this.Db.PoomPoom_Likes
                    .Where(x => x.PoomPoomId == contentId && x.MemberId == memberId)
                    .FirstOrDefault();

                var count = this.Db.PoomPoom_Likes
                    .Where(x => x.PoomPoomId == contentId)
                    .Count();

                if (item != null)
                {
                    this.Db.Remove(item);
                    this.Db.SaveChanges();

                    return new { count = count - 1, isLike = false };
                }
                else
                {
                    this.Db.Add(new PoomPoom_Like
                    {
                        CreateTime = DateTime.Now,
                        MemberId = memberId,
                        PoomPoomId = contentId,
                    });
                    this.Db.SaveChanges();

                    /// 알림처리
                    {
                        var receiver = this.Db.PoomPooms
                            .Where(x => x.Id == contentId)
                            .Select(x => new
                            {
                                x.Member.Id,
                                x.Member.PushToken,
                                x.Member.UseNotiAppeal,
                            })
                            .FirstOrDefault();

                        if (receiver != null)
                        {
                            var msg = $"{member.Nickname}님이 게시글을 추천하셨습니다.";
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

                            if (receiver.UseNotiAppeal)
                            {
                                await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                            }
                        }
                    }

                    return new { count = count + 1, isLike = true };
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                return new { ex.Message };
            }
        }

        [HttpPost]
        public async Task<object> ExcutePoomPoomComment(
            [FromForm] string apikey,
            [FromForm] int contentId,
            [FromForm] string comment,
            [FromForm] int? replyMemberId)
        {
            try
            {
                var member = this.Db.Members
                    .Where(x => x.ApiKey == apikey)
                    .Select(x => new
                    {
                        x.Id,
                        x.Nickname,
                        ProfileImage = x.Member_ProfileImages
                            .OrderBy(z => z.CreateTime)
                            .Select(z => z.Url)
                            .FirstOrDefault(),
                    })
                    .FirstOrDefault();

                if (member == null)
                    throw new Exception("잘못된 요청입니다.");

                var bw = this.Db.BlockWords
                        .Select(x => x.Word)
                        .ToArray();
                if (bw.Any(x => comment.Contains(x)))
                    throw new Exception("금칙어가 포함되어 있습니다.");

                var memberId = member.Id;
                var nickname = member.Nickname;
                var profileImage = member.ProfileImage;

                var item = new PoomPoom_Comment
                {
                    Comment = comment,
                    CreateTime = DateTime.Now,
                    MemberId = memberId,
                    PoomPoomId = contentId,
                    ReplyMemberId = replyMemberId
                };
                this.Db.Add(item);
                this.Db.SaveChanges();

                var replyNickname = this.Db.Members
                    .Where(x => x.Id == replyMemberId)
                    .Select(x => nickname)
                    .FirstOrDefault();

                /// 알림처리
                {
                    var receiver = this.Db.PoomPooms
                        .Where(x => x.Id == contentId)
                        .Select(x => new
                        {
                            x.Member.Id,
                            x.Member.PushToken,
                            x.Member.UseNotiAppeal
                        })
                        .FirstOrDefault();

                    if (receiver != null)
                    {
                        var msg = $"{member.Nickname}님이 댓글을 달았습니다.";

                        if (receiver.Id != memberId)
                        {
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

                            if (receiver.UseNotiAppeal)
                            {
                                await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                            }
                        }

                        if (replyMemberId.HasValue && replyMemberId != memberId)
                        {
                            receiver = this.Db.Members
                                .Where(x => x.Id == replyMemberId.Value)
                                .Select(x => new
                                {
                                    x.Id,
                                    x.PushToken,
                                    x.UseNotiAppeal,
                                })
                                .FirstOrDefault();

                            if (receiver != null)
                            {
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

                                if (receiver.UseNotiAppeal)
                                {
                                    await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
                                }
                            }
                        }
                    }
                }

                return new
                {
                    Comment = new
                    {
                        CommentId = item.Id,
                        Comment = item.Comment,
                        MemberId = memberId,
                        Nickname = nickname,
                        ProfileImage = profileImage,
                        ReplyNickname = replyNickname,
                        CreateTime = item.CreateTime,
                    }
                };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object ExcuteAlertPoomPoomComment(
            [FromForm] string apikey,
            [FromForm] int alertCommentId,
            [FromForm] int alertMemberId,
            [FromForm] string title)
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

                this.Db.Add(new Member_AlertComment
                {
                    MemberId = memberId,
                    AlertCommentId = alertCommentId,
                    AlertMemberId = alertMemberId,
                    Title = title,
                    CreateTime = DateTime.Now,
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
        public object RemovePoomPoomComment(
            [FromForm] string apikey,
            [FromForm] int commentId)
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

                var sql 
                    = "DELETE FROM PoomPoom_Comment "
                    + "WHERE Id = {0} "
                    + "  AND MemberId = {1}";
                this.Db.Database.ExecuteSqlRaw(sql, commentId, memberId);

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
    }
}
