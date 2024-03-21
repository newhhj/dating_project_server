using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strawberry.Server.Api.Controllers.Authentication;
using Strawberry.Server.Api.Helpers;
using Strawberry.Server.Database;
using Strawberry.Server.Database.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Strawberry.Server.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AuthenticationController : ControllerBase
	{
		public DatabaseContext Db { get; }
		public ImageHelper ImageHelper { get; }
		public FirebaseHelper FirebaseHelper { get; }

		public AuthenticationController(DatabaseContext db, ImageHelper imageHelper, FirebaseHelper firebaseHelper)
		{
			this.Db = db;
			this.ImageHelper = imageHelper;
			this.FirebaseHelper = firebaseHelper;
		}

		// 휴대폰 번호 인증을 위해 SMS를 전송합니다.
		[HttpPost]
		public object SendSMS([FromForm] string phone)
		{
			try
			{
				var phoneNumber = phone.Replace("-", "").Replace(" ", "");

				if (phoneNumber.Length == 10)
					phoneNumber = phoneNumber.Insert(6, "-").Insert(3, "-");
				else
					phoneNumber = phoneNumber.Insert(7, "-").Insert(3, "-");

				var code = new Random().Next(1000, 10000).ToString();
				SMSHelper.SendSMS(phoneNumber, $"휴대전화번호 인증을 위한 인증번호입니다.\n{code}");

				return new { phoneNumber, code };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// 이미 등록된 이메일인지 확인합니다.
		[HttpPost]
		public object CheckEmail([FromForm] string email)
		{
			try
			{
				var hasEmail = this.Db.Member_Accounts
					.Any(x => x.Email == email);

				if (hasEmail)
					throw new Exception("해당 이메일로 가입이 되어있습니다.");

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// 이메일과 비밀번호로 로그인을 실행합니다.
		[HttpPost]
		public object ExcuteLogin(
			[FromForm] string email,
			[FromForm] string password)
		{
			try
			{
				var apiKey = this.Db.Members
					.Where(x => x.Member_Account.Email == email && x.Member_Account.Passwd == password)
					.Select(x => x.ApiKey)
					.FirstOrDefault();

				if (string.IsNullOrWhiteSpace(apiKey))
					throw new Exception("아이디 또는 비밀번호가 일치하지 않습니다.");

				return new { apiKey };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// 카카오 인증으로 로그인을 실행합니다.
		[HttpPost]
		public object ExcuteKakaoLogin(
			[FromForm] long kakaoKey)
		{
			try
			{
				var apiKey = this.Db.Members
					.Where(x => x.Member_Account.KakaoKey == kakaoKey)
					.Select(x => x.ApiKey)
					.FirstOrDefault();

				if (string.IsNullOrWhiteSpace(apiKey))
				{
					return new { apiKey = default(string) };
				}
				else
				{
					return new { apiKey };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// API 키를 사용하여 회원 정보를 가져옵니다.
		[HttpPost]
		public async Task<object> GetMemberFromApiKey([FromForm] string apikey)
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
						.Where(x => x.ApiKey == apikey)
						.FirstOrDefault();

					var purchases = this.Db.Member_PurchaseLogs
						.Where(x => x.Member.ApiKey == apikey && x.PurchaseType == PurchaseTypes.Subscription && !x.IsExpire)
						.ToArray();

					foreach (var purchase in purchases)
					{
						var result = await this.FirebaseHelper.CheckSubscriptionsAsync(purchase.ProductId, purchase.PurchaseToken);
						purchase.IsExpire = result.CancelReason.HasValue;
						purchase.ExpireTime = result.ExpiryTime;

						// 상품 ID에 따라 회원의 프리미엄 시간을 업데이트합니다.
						switch (purchase.ProductId)
						{
							case "item09":
								member.FreeChoiceTime = result.ExpiryTime;
								break;
							case "item10":
								member.FreeSmartChoiceTime = result.ExpiryTime;
								break;
							case "item11":
								member.FreeChattingTime = result.ExpiryTime;
								break;
							case "item12":
								member.FreeChoiceTime = result.ExpiryTime;
								break;
							case "item13":
								member.FreeSmartChoiceTime = result.ExpiryTime;
								break;
							case "item14":
								member.AddChatting3Time = result.ExpiryTime;
								break;
							case "item15":
								member.AddChoice3Time = result.ExpiryTime;
								break;
							case "item16":
								member.AddChatting1Time = result.ExpiryTime;
								break;
							default:
								break;
						}

						this.Db.SaveChanges();
					}

					tran.Commit();
				}

				// 회원 정보를 조회하여 필요한 데이터를 선택적으로 반환합니다.
				var item = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.Select(x => new
					{
						x.Id,
						MemberState = x.MemberState,
						ProfileImages = x.Member_ProfileImages
							.OrderBy(z => z.CreateTime)
							.Select(z => new
							{
								z.Id,
								ProfileImageSource = z.Url
							}),
						Gender = x.Gender,
						BirthDay = x.BirthDay,
						Nickname = x.Nickname,
						Tall = x.Tall,
						BodyStyle = x.BodyStyle,
						School = x.School,
						Job = x.Job,
						Religion = x.Religion,
						Alcohol = x.Alcohol,
						Smoking = x.Smoking,
						Point = x.Point,
						IsRoyal = x.IsRoyal,
						IsVIP = x.IsVIP,
						FreeChattingTime = x.FreeChattingTime,
						FreeChoiceTime = x.FreeChoiceTime,
						FreeSmartChoiceTime = x.FreeSmartChoiceTime,
						FreeChattingCount = x.FreeChattingCount,
						FreeChoiceCount = x.FreeChoiceCount,
						Preference_MinAge = x.Member_Preference.MinAge,
						Preference_MaxAge = x.Member_Preference.MaxAge,
						Preference_Range = x.Member_Preference.Range,
						Preference_MinTall = x.Member_Preference.MinTall,
						Preference_MaxTall = x.Member_Preference.MaxTall,
						Preference_BeautyOrWealth = x.Member_Preference.BeautyOrWealth,
						Preference_BodyStyle = x.Member_Preference.Body,
						Preference_Religion = x.Member_Preference.Religion,
						Preference_Alcohol = x.Member_Preference.Alcohol,
						Preference_Smoking = x.Member_Preference.Smoking,
						Preference_Priority = x.Member_Preference.Priority
					})
					.FirstOrDefault();

				return new { member = item };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// 매력 포인트를 가져옵니다.
		[HttpPost]
		public object GetCharmingPoints([FromForm] string apikey)
		{
			try
			{
				var items = this.Db.Member_CharmingPoints
					.Where(x => x.Member.ApiKey == apikey)
					.Select(x => x.Name)
					.ToArray();

				return new { items };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// 관심사를 가져옵니다.
		[HttpPost]
		public object GetInterests([FromForm] string apikey)
		{
			try
			{
				var items = this.Db.Member_Interests
					.Where(x => x.Member.ApiKey == apikey)
					.Select(x => x.Name)
					.ToArray();

				return new { items };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		// 회원 가입을 처리합니다.
		[HttpPost]
		public object JoinMember([FromForm] JoinDataModel joinData)
		{
			try
			{
				if (joinData.Nickname.Length > 10)
					throw new Exception("닉네임은 10자까지 가능합니다.");
				if (joinData.Nickname.IndexOf(" ") >= 0)
					throw new Exception("닉네임에 공백이 있습니다.");

				var transaction = this.Db.Database.BeginTransaction();

				var member = this.Db.Members
					.Where(x => x.Id == joinData.Id)
					.Include(x => x.Member_Account)
					.Include(x => x.Member_Preference)
					.FirstOrDefault();

				// 새로운 회원인 경우 회원 정보를 생성합니다.
				if (member == null)
				{
					var r = new Random();
					var recommandCode = Guid.NewGuid().ToString().ToLower().Replace("-", "");
					member = new Database.Tables.Member
					{
						ApiKey = Guid.NewGuid().ToString().ToLower(),
#if DEBUG
                        MemberState = MemberStateTypes.JoinConfirm,
#else
						MemberState = MemberStateTypes.JoinWait,
#endif
						LevelType = 0,
						TermCheckTime = DateTime.Now,
						PrivacyCheckTime = DateTime.Now,
						LocationCheckTime = DateTime.Now,
						SensitiveCheckTime = DateTime.Now,
						MarketingCheckTime = joinData.ConsentMarketing ? DateTime.Now : null,
						Gender = joinData.Gender,
						BirthDay = joinData.BirthDay,
						Nickname = joinData.Nickname,
						Tall = joinData.Tall,
						BodyStyle = joinData.BodyStyle,
						School = joinData.School,
						Job = joinData.Job,
						Religion = joinData.Religion,
						Alcohol = joinData.Alcohol,
						Smoking = joinData.Smoking,
						Lat = joinData.Lat,
						Lng = joinData.Lng,
						CreateTime = DateTime.Now,
						LastLoginTime = DateTime.Now,
						RateCharming = r.Next(75, 101),
						RateResponse = r.Next(75, 101),
						RecommandCode = recommandCode,
						Referrer = joinData.Referrer,
					};
					this.Db.Add(member);

					// 추천인이 있는 경우 추천인에게 포인트를 지급합니다.
					if (!string.IsNullOrWhiteSpace(joinData.Referrer))
					{
						var recommander = this.Db.Members
						.Where(x => x.MemberState == MemberStateTypes.Normal)
						.Where(x => joinData.Referrer.Contains(x.RecommandCode))
						.FirstOrDefault();

						if (recommander != null)
						{
							recommander.Point += 15;

							this.Db.Add(new Member_PointLog
							{
								MemberId = recommander.Id,
								AcceptPoint = +15,
								Comment = "추천친구 가입",
								CurrentPoint = recommander.Point,
								CreateTime = DateTime.Now,
							});
						}
					}
				}
				else
				{
					member.MemberState = MemberStateTypes.JoinWait;
					member.Gender = joinData.Gender;
					member.BirthDay = joinData.BirthDay;
					member.Nickname = joinData.Nickname;
					member.Tall = joinData.Tall;
					member.BodyStyle = joinData.BodyStyle;
					member.School = joinData.School;
					member.Job = joinData.Job;
					member.Religion = joinData.Religion;
					member.Alcohol = joinData.Alcohol;
					member.Smoking = joinData.Smoking;
					member.Lat = joinData.Lat;
					member.Lng = joinData.Lng;
				}
				this.Db.SaveChanges();

				// 회원 계정 정보를 생성합니다.
				if (member.Member_Account == null)
				{
					var member_account = new Database.Tables.Member_Account
					{
						Id = member.Id,
						KakaoKey = joinData.KakaoKey,
						PhoneNumber = joinData.PhoneNumber,
						Email = joinData.Email,
						Passwd = joinData.Password,
					};
					this.Db.Add(member_account);
					this.Db.SaveChanges();
				}

				// 회원 선호도 정보를 생성합니다.
				if (member.Member_Preference == null)
				{
					var member_Preference = new Database.Tables.Member_Preference
					{
						Id = member.Id,
						Alcohol = joinData.Preference_Alcohol,
						BeautyOrWealth = joinData.Preference_BeautyOrWealth,
						Body = joinData.Preference_BodyStyle,
						Range = joinData.Preference_Range,
						MaxAge = joinData.Preference_MaxAge,
						MaxTall = joinData.Preference_MaxTall,
						MinAge = joinData.Preference_MinAge,
						MinTall = joinData.Preference_MinTall,
						Priority = joinData.Preference_Priority,
						Religion = joinData.Preference_Religion,
						Smoking = joinData.Preference_Smoking,
					};
					this.Db.Add(member_Preference);
				}
				else
				{
					member.Member_Preference.Alcohol = joinData.Preference_Alcohol;
					member.Member_Preference.BeautyOrWealth = joinData.Preference_BeautyOrWealth;
					member.Member_Preference.Body = joinData.Preference_BodyStyle;
					member.Member_Preference.Range = joinData.Preference_Range;
					member.Member_Preference.MaxAge = joinData.Preference_MaxAge;
					member.Member_Preference.MaxTall = joinData.Preference_MaxTall;
					member.Member_Preference.MinAge = joinData.Preference_MinAge;
					member.Member_Preference.MinTall = joinData.Preference_MinTall;
					member.Member_Preference.Priority = joinData.Preference_Priority;
					member.Member_Preference.Religion = joinData.Preference_Religion;
					member.Member_Preference.Smoking = joinData.Preference_Smoking;
				}
				this.Db.SaveChanges();

				// 프로필 이미지를 저장합니다.
				if (joinData.ProfileImages != null && joinData.ProfileImages.Length > 0)
				{
					for (int i = 0; i < joinData.ProfileImages.Length; i++)
					{
						var id = joinData.ProfileImageIds[i];
						var image = joinData.ProfileImages[i];

						using var stream = image.OpenReadStream();
						var path = this.ImageHelper.SaveImageFromStream(stream, "profile", image.FileName);

						var member_profileImage = this.Db.Member_ProfileImages
								.Where(x => x.Id == id)
								.FirstOrDefault();

						if (member_profileImage == null)
						{
							member_profileImage = new Database.Tables.Member_ProfileImage
							{
								MemberId = member.Id,
								Path = path.local,
								Url = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
								CreateTime = DateTime.Now
							};
							this.Db.Add(member_profileImage);
						}
						else
						{
							var fileInfo = new FileInfo(member_profileImage.Path);
							if (fileInfo.Exists)
								fileInfo.Delete();

							member_profileImage.Path = path.local;
							member_profileImage.Url = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}";
							member_profileImage.CreateTime = DateTime.Now;
						}
						this.Db.SaveChanges();
					}
				}

				// 프로필 완성도에 따라 포인트를 지급합니다.
				{
					var data = this.Db.Members
						.Where(x => x.Id == member.Id)
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

					if (data.Count(x => x) >= 1)
					{
						var comment = "프로필 1단계 완성";
						var hasValue = this.Db.Member_PointLogs
							.Any(x => x.Comment == comment);
						if (!hasValue)
						{
							member.Point += 5;
							this.Db.SaveChanges();

							this.Db.Add(new Member_PointLog
							{
								MemberId = member.Id,
								AcceptPoint = 5,
								CurrentPoint = member.Point,
								Comment = comment,
								CreateTime = DateTime.Now,
							});
							this.Db.SaveChanges();
						}
					}

					if (data.Count(x => x) >= 2)
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

					if (data.Count(x => x) >= 3)
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

				transaction.Commit();

				// 회원 프로필 이미지를 조회합니다.
				var profileImages = this.Db.Member_ProfileImages
					.Where(x => x.MemberId == member.Id)
					.Select(x => new
					{
						x.Id,
						ProfileImageSource = x.Url
					})
					.ToArray();

				return new
				{
					member.ApiKey,
					member.Id,
					profileImages
				};
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}


#if DEBUG
        [HttpPost]
        public object JoinTempMember([FromForm] JoinDataModel joinData)
        {
            try
            {
                var transaction = this.Db.Database.BeginTransaction();

                var r = new Random();
                var namecode = r.Next(1000, 10000).ToString();
                var member = new Database.Tables.Member
                {
                    ApiKey = Guid.NewGuid().ToString().ToLower(),
                    MemberState = MemberStateTypes.Normal,
                    LevelType = (LevelTypes)r.Next(0, 4),
                    TermCheckTime = DateTime.Now,
                    PrivacyCheckTime = DateTime.Now,
                    LocationCheckTime = DateTime.Now,
                    SensitiveCheckTime = DateTime.Now,
                    MarketingCheckTime = joinData.ConsentMarketing ? DateTime.Now : null,
                    Gender = joinData.Gender,
                    BirthDay = new DateTime(r.Next(1953, 2003), r.Next(1, 13), r.Next(1, 29)),
                    Nickname = joinData.Nickname + namecode,
                    Tall = r.Next(150, 192),
                    BodyStyle = joinData.BodyStyle,
                    School = joinData.School,
                    Job = joinData.Job,
                    Religion = joinData.Religion,
                    Alcohol = joinData.Alcohol,
                    Smoking = joinData.Smoking,
                    Lat = joinData.Lat,
                    Lng = joinData.Lng,
                    CreateTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    RateCharming = r.Next(75, 101),
                    RateResponse = r.Next(75, 101),
                };
                this.Db.Add(member);
                this.Db.SaveChanges();

                var member_account = new Database.Tables.Member_Account
                {
                    Id = member.Id,
                    PhoneNumber = $"010{r.Next(1000, 10000)}{r.Next(1000, 10000)}",
                    Email = $"test{namecode}@test.com",
                    Passwd = "111111",
                };
                this.Db.Add(member_account);
                this.Db.SaveChanges();

                var member_Preference = new Database.Tables.Member_Preference
                {
                    Id = member.Id,
                    Alcohol = joinData.Preference_Alcohol,
                    BeautyOrWealth = joinData.Preference_BeautyOrWealth,
                    Body = joinData.Preference_BodyStyle,
                    Range = joinData.Preference_Range,
                    MaxAge = joinData.Preference_MaxAge,
                    MaxTall = joinData.Preference_MaxTall,
                    MinAge = joinData.Preference_MinAge,
                    MinTall = joinData.Preference_MinTall,
                    Priority = joinData.Preference_Priority,
                    Religion = joinData.Preference_Religion,
                    Smoking = joinData.Preference_Smoking,
                };
                this.Db.Add(member_Preference);
                this.Db.SaveChanges();

                if (joinData.ProfileImages != null && joinData.ProfileImages.Length > 0)
                {
                    var list = joinData.ProfileImages
                        .OrderBy(x => r.NextDouble())
                        .ToArray();

                    for (int i = 0; i < list.Length; i++)
                    {
                        var image = list[i];

                        using var stream = image.OpenReadStream();
                        var path = this.ImageHelper.SaveImageFromStream(stream, "profile", image.FileName);

                        var member_profileImage = new Database.Tables.Member_ProfileImage
                        {
                            MemberId = member.Id,
                            Path = path.local,
                            Url = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
                            CreateTime = DateTime.Now
                        };
                        this.Db.Add(member_profileImage);
                        this.Db.SaveChanges();
                    }
                }

                transaction.Commit();

                var profileImages = this.Db.Member_ProfileImages
                    .Where(x => x.MemberId == member.Id)
                    .Select(x => new
                    {
                        x.Id,
                        ProfileImageSource = x.Url
                    })
                    .ToArray();

                return new
                {
                    member.ApiKey,
                    member.Id,
                    profileImages
                };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
#endif

		[HttpPost]
		public object UpdatePushToken([FromForm] string apikey, [FromForm] string pushtoken)
		{
			try
			{
				var sql = ""
					+ "UPDATE Member "
					+ "SET PushToken = {0} "
					+ "WHERE ApiKey = {1}";

				// 회원의 PushToken을 업데이트하는 SQL 쿼리를 실행합니다.
				this.Db.Database.ExecuteSqlRaw(sql, pushtoken, apikey);

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object UpdateCharmingPoints([FromForm] string apikey, [FromForm] string[] items)
		{
			try
			{
				var transaction = this.Db.Database.BeginTransaction();

				// 주어진 apikey를 가진 회원을 검색합니다.
				var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.Include(x => x.Member_CharmingPoints)
					.FirstOrDefault();

				if (member == null)
					throw new Exception("잘못된 요청입니다.");

				// 회원의 Member_CharmingPoints 컬렉션을 비웁니다.
				member.Member_CharmingPoints.Clear();

				if (items != null)
				{
					// 주어진 items 배열을 사용하여 Member_CharmingPoints 컬렉션을 업데이트합니다.
					foreach (var item in items)
					{
						member.Member_CharmingPoints.Add(new Database.Tables.Member_CharmingPoint
						{
							Name = item
						});
					}
				}

				this.Db.SaveChanges();
				transaction.Commit();

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object UpdateInterests([FromForm] string apikey, [FromForm] string[] items)
		{
			try
			{
				var transaction = this.Db.Database.BeginTransaction();

				// 주어진 apikey를 가진 회원을 검색합니다.
				var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.Include(x => x.Member_Interests)
					.FirstOrDefault();

				if (member == null)
					throw new Exception("잘못된 요청입니다.");

				// 회원의 Member_Interests 컬렉션을 비웁니다.
				member.Member_Interests.Clear();

				if (items != null)
				{
					// 주어진 items 배열을 사용하여 Member_Interests 컬렉션을 업데이트합니다.
					foreach (var item in items)
					{
						member.Member_Interests.Add(new Database.Tables.Member_Interest
						{
							Name = item
						});
					}
				}

				this.Db.SaveChanges();
				transaction.Commit();

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteChoice([FromForm] string apikey, [FromForm] int partnerid)
		{
			try
			{
				// 주어진 apikey를 가진 회원을 검색하고 필요한 속성들을 선택합니다.
				var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.Select(x => new
					{
						x.Id,
						x.Nickname,
						x.FreeChoiceCount,
						x.FreeChoiceTime,
						x.Point
					})
					.FirstOrDefault();

				if (member == null)
					throw new Exception("잘못된 요청입니다.");

				var tran = this.Db.Database.BeginTransaction();

				var freeChoiceCount = member.FreeChoiceCount;
				var freeChoiceTime = member.FreeChoiceTime;
				var point = member.Point;

				if (member.FreeChoiceTime.HasValue && member.FreeChoiceTime.Value > DateTime.Now)
				{
					// FreeChoiceTime이 현재 시간보다 이후인 경우 아무 작업도 수행하지 않습니다.
				}
				else if (member.FreeChoiceCount > 0)
				{
					// FreeChoiceCount가 0보다 큰 경우 하나를 감소시키고 데이터베이스를 업데이트합니다.
					freeChoiceCount = freeChoiceCount - 1;
					var sql = ""
						+ "UPDATE Member "
						+ "SET FreeChoiceCount = {0} "
						+ "WHERE Id = {1}";
					this.Db.Database.ExecuteSqlRaw(sql, freeChoiceCount, member.Id);
				}
				else
				{
					if (member.Point < 5)
						throw new Exception("딸기가 부족합니다.");

					// Point가 5보다 작은 경우 5를 차감하고 데이터베이스를 업데이트합니다.
					point = point - 5;
					var sql = ""
						+ "UPDATE Member "
						+ "SET Point = {0} "
						+ "WHERE Id = {1}";
					this.Db.Database.ExecuteSqlRaw(sql, point, member.Id);
				}

				// Member_ChoicePartner에 새로운 항목을 추가하고 데이터베이스를 저장합니다.
				this.Db.Add(new Member_ChoicePartner
				{
					MemberId = member.Id,
					PartnerId = partnerid,
					CreateTime = DateTime.Now
				});
				this.Db.SaveChanges();

				/// 알림처리
				{
					// 선택된 파트너에 대한 정보를 가져옵니다.
					var receiver = this.Db.Members
						.Where(x => x.Id == partnerid)
						.Select(x => new
						{
							x.Id,
							x.PushToken,
							x.UseNotiReceiveChoice
						})
						.FirstOrDefault();

					var message = $"{member.Nickname}님에게 좋아요를 받았습니다.";

					// Member_Notification에 새로운 알림을 추가하고 데이터베이스를 저장합니다.
					this.Db.Add(new Member_Notification
					{
						CreateTime = DateTime.Now,
						IsShow = false,
						MemberId = receiver.Id,
						Message = message
					});
					this.Db.SaveChanges();

					// Member_Notifications에서 최신 5개의 알림을 유지하도록 제한합니다.
					var removeItems = this.Db.Member_Notifications
						.Where(x => x.MemberId == receiver.Id)
						.OrderByDescending(x => x.CreateTime)
						.Skip(5)
						.ToArray();
					this.Db.RemoveRange(removeItems);
					this.Db.SaveChanges();

					if (receiver.UseNotiReceiveChoice)
					{
						// FirebaseHelper를 사용하여 푸시 알림을 보냅니다.
						await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", message, "noti:message", null);
					}
				}

				tran.Commit();

				return new { freeChoiceCount, point };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteSmartChoice([FromForm] string apikey, [FromForm] int partnerid, [FromForm] string message)
		{
			try
			{
				// 주어진 apikey를 가진 회원을 검색하고 필요한 속성들을 선택합니다.
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

				var point = member.Point;

				if (member.FreeSmartChoiceTime.HasValue && member.FreeSmartChoiceTime.Value > DateTime.Now)
				{
					// FreeSmartChoiceTime이 현재 시간보다 이후인 경우 아무 작업도 수행하지 않습니다.
				}
				else
				{
					if (member.Point < 10)
						throw new Exception("딸기가 부족합니다.");

					// Point가 10보다 작은 경우 10을 차감하고 데이터베이스를 업데이트합니다.
					point = member.Point - 10;

					var sql = ""
						+ "UPDATE Member "
						+ "SET Point = {0} "
						+ "WHERE Id = {1}";
					this.Db.Database.ExecuteSqlRaw(sql, point, member.Id);

					// Member_PointLog에 구매 로그를 추가하고 데이터베이스를 저장합니다.
					this.Db.Add(new Member_PointLog
					{
						AcceptPoint = -10,
						Comment = "스마트 딸기",
						CreateTime = DateTime.Now,
						CurrentPoint = point,
						MemberId = member.Id
					});
					this.Db.SaveChanges();
				}

				// Member_ChoicePartner에 새로운 항목을 추가하고 데이터베이스를 저장합니다.
				this.Db.Add(new Member_ChoicePartner
				{
					MemberId = member.Id,
					PartnerId = partnerid,
					Message = message,
					CreateTime = DateTime.Now
				});
				this.Db.SaveChanges();

				/// 알림처리
				{
					// 선택된 파트너에 대한 정보를 가져옵니다.
					var receiver = this.Db.Members
						.Where(x => x.Id == partnerid)
						.Select(x => new
						{
							x.Id,
							x.PushToken,
							x.UseNotiReceiveChoice
						})
						.FirstOrDefault();

					var msg = $"{member.Nickname}님에게 좋아요를 받았습니다.";

					// Member_Notification에 새로운 알림을 추가하고 데이터베이스를 저장합니다.
					this.Db.Add(new Member_Notification
					{
						CreateTime = DateTime.Now,
						IsShow = false,
						MemberId = receiver.Id,
						Message = msg
					});
					this.Db.SaveChanges();

					// Member_Notifications에서 최신 5개의 알림을 유지하도록 제한합니다.
					var removeItems = this.Db.Member_Notifications
						.Where(x => x.MemberId == receiver.Id)
						.OrderByDescending(x => x.CreateTime)
						.Skip(5)
						.ToArray();
					this.Db.RemoveRange(removeItems);
					this.Db.SaveChanges();

					if (receiver.UseNotiReceiveChoice)
					{
						// FirebaseHelper를 사용하여 푸시 알림을 보냅니다.
						await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
					}
				}

				tran.Commit();

				return new { point };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcutePurchase(
			[FromForm] string apikey,
			[FromForm] string platform,
			[FromForm] string productId,
			[FromForm] string purchaseId,
			[FromForm] string purchaseToken,
			[FromForm] DateTime purchaseTime)
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					// 주어진 apikey를 가진 회원을 검색합니다.
					var member = this.Db.Members
								.Where(x => x.ApiKey == apikey)
								.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					// Member_PurchaseLog에 구매 로그 항목을 추가합니다.
					var purchaseLogItem = new Member_PurchaseLog
					{
						MemberId = member.Id,
						Platform = platform,
						ProductId = productId,
						PurchaseId = purchaseId,
						PurchaseToken = purchaseToken,
						PurchaseUTCTime = purchaseTime,
						IsExpire = false,
						CreateTime = DateTime.Now,
					};

					switch (productId)
					{
						case "item01":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								var point = 15;
								member.Point += point;
								this.Db.SaveChanges();

								// Member_PointLog에 포인트 로그 항목을 추가합니다.
								var pointLogItem = new Member_PointLog
								{
									MemberId = member.Id,
									AcceptPoint = point,
									CurrentPoint = member.Point,
									Comment = "딸기 구매",
									CreateTime = DateTime.Now,
								};
								this.Db.Add(pointLogItem);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item02":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								var point = 50;
								member.Point += point;
								this.Db.SaveChanges();

								// Member_PointLog에 포인트 로그 항목을 추가합니다.
								var pointLogItem = new Member_PointLog
								{
									MemberId = member.Id,
									AcceptPoint = point,
									CurrentPoint = member.Point,
									Comment = "딸기 구매",
									CreateTime = DateTime.Now,
								};
								this.Db.Add(pointLogItem);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item03":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								var point = 150;
								member.Point += point;
								this.Db.SaveChanges();

								// Member_PointLog에 포인트 로그 항목을 추가합니다.
								var pointLogItem = new Member_PointLog
								{
									MemberId = member.Id,
									AcceptPoint = point,
									CurrentPoint = member.Point,
									Comment = "딸기 구매",
									CreateTime = DateTime.Now,
								};
								this.Db.Add(pointLogItem);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item04":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								var point = 400;
								member.Point += point;
								this.Db.SaveChanges();

								// Member_PointLog에 포인트 로그 항목을 추가합니다.
								var pointLogItem = new Member_PointLog
								{
									MemberId = member.Id,
									AcceptPoint = point,
									CurrentPoint = member.Point,
									Comment = "딸기 구매",
									CreateTime = DateTime.Now,
								};
								this.Db.Add(pointLogItem);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item05":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								var point = 1000;
								member.Point += point;
								this.Db.SaveChanges();

								// Member_PointLog에 포인트 로그 항목을 추가합니다.
								var pointLogItem = new Member_PointLog
								{
									MemberId = member.Id,
									AcceptPoint = point,
									CurrentPoint = member.Point,
									Comment = "딸기 구매",
									CreateTime = DateTime.Now,
								};
								this.Db.Add(pointLogItem);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item06":
						case "item07":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								// Member_RequestRoyal에 요청 로열 항목을 추가합니다.
								var requestRoyal = new Member_RequestRoyal
								{
									MemberId = member.Id,
									IsFastWork = false,
									IsComplete = false,
									CreateTime = DateTime.Now,
								};
								this.Db.Add(requestRoyal);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item08":
							{
								var result = await this.FirebaseHelper.CheckProductsAsync(productId, purchaseToken);

								// Member_RequestRoyal에 요청 로열 항목을 추가합니다.
								var requestRoyal = new Member_RequestRoyal
								{
									MemberId = member.Id,
									IsFastWork = true,
									IsComplete = false,
									CreateTime = DateTime.Now,
								};
								this.Db.Add(requestRoyal);
								this.Db.SaveChanges();

								purchaseLogItem.PurchaseType = PurchaseTypes.Product;
								break;
							}
						case "item09":
							{
								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.FreeChoiceTime = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item10":
							{
								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.FreeSmartChoiceTime = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item11":
							{
								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.FreeChattingTime = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item12":
							{
								if (!member.IsVIP)
									member.IsVIP = true;

								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.FreeChoiceTime = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item13":
							{
								if (!member.IsVIP)
									member.IsVIP = true;

								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.FreeSmartChoiceTime = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item14":
							{
								if (!member.IsVIP)
									member.IsVIP = true;

								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.AddChatting3Time = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item15":
							{
								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.AddChoice3Time = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						case "item16":
							{
								var result = await this.FirebaseHelper.CheckSubscriptionsAsync(productId, purchaseToken);
								member.AddChatting1Time = result.ExpiryTime;
								this.Db.SaveChanges();

								purchaseLogItem.ExpireTime = result.ExpiryTime;
								purchaseLogItem.PurchaseType = PurchaseTypes.Subscription;
								break;
							}
						default:
							break;
					}

					// Member_PurchaseLog에 구매 로그 항목을 추가하고 데이터베이스를 저장합니다.
					this.Db.Add(purchaseLogItem);
					this.Db.SaveChanges();

					tran.Commit();

					return new
					{
						member.Point,
						member.FreeChoiceCount,
						member.FreeChattingCount,
						member.FreeChattingTime,
						member.FreeChoiceTime,
						member.FreeSmartChoiceTime,
						member.IsVIP,
					};
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object GetPointInfo(
			[FromForm] string apikey)
		{
			try
			{
				// 주어진 apikey를 가진 회원의 포인트 정보를 가져옵니다.
				var info = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.Select(x => new
					{
						x.Point,
						x.FreeChoiceCount,
						x.FreeChattingCount,
						x.FreeChattingTime,
						x.FreeChoiceTime,
						x.FreeSmartChoiceTime,
					})
					.FirstOrDefault();

				if (info == null)
					throw new Exception("잘못된 요청입니다.");

				return info;
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}
	}
}
