using Geolocation;
using Microsoft.AspNetCore.Mvc;
using Strawberry.Server.Api.Helpers;
using Strawberry.Server.Database;
using Strawberry.Server.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class ChoiceController : ControllerBase
	{
		public DatabaseContext Db { get; }
		public FirebaseHelper FirebaseHelper { get; }

		public ChoiceController(DatabaseContext db, FirebaseHelper firebaseHelper)
		{
			this.Db = db;
			this.FirebaseHelper = firebaseHelper;
		}

		/// <summary>
		/// 아이템들을 조회하는 메서드입니다.
		/// </summary>
		/// <param name="apikey">API 키</param>
		[HttpPost]
		public object GetItems(
		   [FromForm] string apikey)
		{
			try
			{
				// API 키로 회원 조회
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

				if (member == null)
					throw new Exception("잘못된 요청입니다.");

				var memberId = member.Id;
				var memberLat = member.Lat;
				var memberLng = member.Lng;
				var blocks = member.Blocks.ToArray();

				// 아이템 01: ChoicePartners에서 현재 회원이 선택한 상대방들 조회
				var items01 = this.Db.Member_ChoicePartners
					.Where(x => x.PartnerId == member.Id)
					.Where(x => x.IsSkip == false)
					.Select(x => new
					{
						x.Id,
						MemberId = x.Member.Id,
						x.Member.Nickname,
						ProfileImage = x.Member.Member_ProfileImages
							.OrderBy(z => z.CreateTime)
							.Select(z => z.Url)
							.FirstOrDefault(),
						Age = DateTime.Today.Year - x.Member.BirthDay.Year + 1,
						Job = x.Member.Job,
						Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Member.Lat, x.Member.Lng, memberLat, memberLng, 1, DistanceUnit.Kilometers)),
						x.IsConfirm,
						x.Message,
						x.CreateTime
					})
					.ToArray()
					.Where(x => !blocks.Any(z => z == x.MemberId))
					.OrderBy(x => x.CreateTime)
					.ToArray();

				// 아이템 02: ViewProfiles에서 현재 회원이 본 상대방들 조회
				var items02 = this.Db.Member_ViewProfiles
					.Where(x => x.PartnerId == memberId)
					.Where(x => !blocks.Any(z => z == x.MemberId))
					.Select(x => new
					{
						x.Id,
						MemberId = x.Member.Id,
						x.Member.Nickname,
						ProfileImage = x.Member.Member_ProfileImages
							.OrderBy(z => z.CreateTime)
							.Select(z => z.Url)
							.FirstOrDefault(),
						x.CreateTime
					})
					.OrderByDescending(x => x.CreateTime)
					.ToArray();

				// 아이템 03: StarPoints에서 현재 회원이 받은 좋아요 중 별점이 8점 이상인 상대방들 조회
				var items03 = this.Db.Member_StarPoints
					.Where(x => x.PartnerId == memberId)
					.Where(x => x.StarPoint >= 8)
					.Where(x => x.IsSkip == false)
					.Where(x => !x.Partner.Member_ChoicePartners.Any(z => z.PartnerId == x.MemberId))
					.Where(x => !blocks.Any(z => z == x.MemberId))
					.Select(x => new
					{
						x.Id,
						MemberId = x.Member.Id,
						x.Member.Nickname,
						ProfileImage = x.Member.Member_ProfileImages
							.OrderBy(z => z.CreateTime)
							.Select(z => z.Url)
							.FirstOrDefault(),
						Age = DateTime.Today.Year - x.Member.BirthDay.Year + 1,
						x.CreateTime
					})
					.OrderByDescending(x => x.CreateTime)
					.ToArray();

				// 아이템 04: StarPoints에서 현재 회원이 준 별점이 8점 이상인 상대방들 조회
				var items04 = this.Db.Member_StarPoints
					.Where(x => x.MemberId == memberId)
					.Where(x => x.Partner != null)
					.Where(x => x.StarPoint >= 8)
					.Select(x => new
					{
						x.Id,
						PartnerId = x.Partner.Id,
						x.Partner.Nickname,
						ProfileImage = x.Partner.Member_ProfileImages
							.OrderBy(z => z.CreateTime)
							.Select(z => z.Url)
							.FirstOrDefault(),
						Age = DateTime.Today.Year - x.Partner.BirthDay.Year + 1,
						Job = x.Partner.Job,
						Range = (int)Math.Ceiling(GeoCalculator.GetDistance(x.Partner.Lat, x.Partner.Lng, memberLat, memberLng, 1, DistanceUnit.Kilometers)),
						x.CreateTime
					})
					.ToArray()
					.Where(x => !blocks.Any(z => z == x.PartnerId))
					.OrderByDescending(x => x.CreateTime)
					.ToArray();

				return new
				{
					items01,
					items02 = items02
						.Select(x => new
						{
							x.Id,
							x.MemberId,
							x.Nickname,
							x.ProfileImage,
							x.CreateTime,
							Count = items02.Length - 1
						})
						.FirstOrDefault(),
					items03,
					items04
				};
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		/// <summary>
		/// Choice를 확인하는 메서드입니다.
		/// </summary>
		/// <param name="apikey">API 키</param>
		/// <param name="id">Choice ID</param>
		[HttpPost]
		public async Task<object> ExcuteChoiceConfirm(
		   [FromForm] string apikey,
		   [FromForm] int id)
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

				var item = this.Db.Member_ChoicePartners
					.Where(x => x.Id == id)
					.FirstOrDefault();
				item.IsConfirm = true;
				this.Db.SaveChanges();

				// 알림 처리
				var receiver = this.Db.Member_ChoicePartners
					.Where(x => x.Id == id)
					.Select(x => new
					{
						x.Member.Id,
						x.Member.PushToken,
						x.Member.UseNotiSendChoiceConfirm,
					})
					.FirstOrDefault();

				if (receiver != null)
				{
					var msg = $"{member.Nickname}님이 좋아요를 수락했습니다.";
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

					if (receiver.UseNotiSendChoiceConfirm)
					{
						await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
					}
				}

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		/// <summary>
		/// Choice를 스킵하는 메서드입니다.
		/// </summary>
		/// <param name="apikey">API 키</param>
		/// <param name="id">Choice ID</param>
		[HttpPost]
		public object ExcuteChoicePass(
		   [FromForm] string apikey,
		   [FromForm] int id)
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
					.Where(x => x.Id == id)
					.FirstOrDefault();
				item.IsSkip = true;
				this.Db.SaveChanges();

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		/// <summary>
		/// 별점을 스킵하는 메서드입니다.
		/// </summary>
		/// <param name="apikey">API 키</param>
		/// <param name="id">별점 ID</param>
		[HttpPost]
		public object ExcuteStarPointPass(
		   [FromForm] string apikey,
		   [FromForm] int id)
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

				var item = this.Db.Member_StarPoints
					.Where(x => x.Id == id)
					.FirstOrDefault();
				item.IsSkip = true;
				this.Db.SaveChanges();

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		/// <summary>
		/// Choice에 대한 피드백을 처리하는 메서드입니다.
		/// </summary>
		/// <param name="apikey">API 키</param>
		/// <param name="id">별점 ID</param>
		/// <param name="memberId">회원 ID</param>
		[HttpPost]
		public async Task<object> ExcuteFeedbackChoice(
		   [FromForm] string apikey,
		   [FromForm] int id,
		   [FromForm] int memberId)
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

					var item = this.Db.Member_StarPoints
						.Where(x => x.Id == id)
						.FirstOrDefault();
					item.IsSkip = true;

					this.Db.SaveChanges();

					if (!this.Db.Member_ChoicePartners.Any(x => x.MemberId == member.Id && x.PartnerId == memberId))
					{
						this.Db.Member_ChoicePartners.Add(new Member_ChoicePartner
						{
							CreateTime = DateTime.Now,
							IsConfirm = false,
							IsSkip = false,
							MemberId = member.Id,
							PartnerId = memberId
						});
						this.Db.SaveChanges();

						if (member.FreeChoiceTime.HasValue && member.FreeChoiceTime.Value > DateTime.Now)
						{

						}
						else if (member.FreeChoiceCount > 0)
						{
							member.FreeChoiceCount--;
						}
						else if (member.Point >= 5)
						{
							member.Point -= 5;

							this.Db.Add(new Member_PointLog
							{
								AcceptPoint = -5,
								Comment = "좋아요",
								CreateTime = DateTime.Now,
								CurrentPoint = member.Point,
								MemberId = member.Id
							});
							this.Db.SaveChanges();
						}
						else
							throw new Exception("딸기가 부족합니다.");

						var partnerid = memberId;

						// 알림 처리
						var receiver = this.Db.Members
							.Where(x => x.Id == partnerid)
							.Select(x => new
							{
								x.Id,
								x.PushToken,
								x.UseNotiConnect,
							})
							.FirstOrDefault();

						var msg = $"{member.Nickname}님과 연결되었습니다.";
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

						if (receiver.UseNotiConnect)
						{
							await FirebaseHelper.SendPushAsync(receiver.PushToken, "알림", msg, "noti:message", null);
						}

						this.Db.SaveChanges();
					}
					else
					{
						throw new Exception("이미 초이스 한 대상입니다.");
					}

					tran.Commit();

					return new { member.FreeChoiceCount, member.Point };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		/// <summary>
		/// 내 별점을 제거하는 메서드입니다.
		/// </summary>
		/// <param name="apikey">API 키</param>
		/// <param name="id">별점 ID</param>
		[HttpPost]
		public object RemoveMyStarPoint(
		   [FromForm] string apikey,
		   [FromForm] int id)
		{
			try
			{
				var item = this.Db.Member_StarPoints
					.Where(x => x.Id == id && x.Member.ApiKey == apikey)
					.FirstOrDefault();

				if (item == null)
					throw new Exception("잘못된 요청입니다.");

				this.Db.Remove(item);
				this.Db.SaveChanges();

				return new { };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}
	}
}
