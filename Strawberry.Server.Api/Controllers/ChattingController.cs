using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
	public class ChattingController : ControllerBase
	{
		// 의존성 주입을 통해 필요한 서비스 및 헬퍼를 가져옴
		public DatabaseContext Db { get; }
		public FirebaseHelper Firebase { get; }
		public ImageHelper ImageHelper { get; }

		public ChattingController(DatabaseContext db, FirebaseHelper firebase, ImageHelper imageHelper)
		{
			this.Db = db;
			this.Firebase = firebase;
			this.ImageHelper = imageHelper;
		}

		[HttpPost]
		public object GetChattingRooms(
			[FromForm] string apikey,
			[FromForm] int skipCount)
		{
			try
			{
				// 제공된 API 키와 관련된 회원 정보 가져오기
				var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.Select(x => new
					{
						x.Id,
						Blocks = x.Member_BlockPartners
							.Where(z => z.PartnerId.HasValue)
							.Select(z => z.PartnerId.Value)
					})
					.FirstOrDefault();

				if (member == null)
					throw new Exception("잘못된 요청입니다.");

				var memberId = member.Id;
				var blocks = member.Blocks.ToArray();

				// 회원과 관련된 채팅방 가져오기
				var items = this.Db.ChattingRooms
					.Where(x => x.Member1Id == memberId || x.Member2Id == memberId)
					.Where(x => x.Member1Id == memberId ? !x.IsCloseMember1 : !x.IsCloseMember2)
					.Select(x => new
					{
						x.Id,
						Partner = x.Member1Id == memberId ?
								  new
								  {
									  x.Member2.Id,
									  x.Member2.Nickname,
									  ProfileImage = x.Member2.Member_ProfileImages
									  .OrderByDescending(z => z.CreateTime)
									  .Select(z => z.Url)
									  .FirstOrDefault()
								  } :
								  new
								  {
									  x.Member1.Id,
									  x.Member1.Nickname,
									  ProfileImage = x.Member1.Member_ProfileImages
									  .OrderByDescending(z => z.CreateTime)
									  .Select(z => z.Url)
									  .FirstOrDefault()
								  },
						LastMessage = x.ChattingMessages
							.Select(z => new
							{
								z.Content,
								z.Type,
								z.CreateTime
							})
							.OrderByDescending(z => z.CreateTime)
							.FirstOrDefault(),
						NotReadCount = x.ChattingMessages
							.Count(z => z.SenderId != memberId && !z.IsShow),
						RoomCreateTime = x.CreateTime
					})
					.ToArray()
					.Where(x => !blocks.Any(z => z == x.Partner.Id))
					.Select(x => new
					{
						x.Id,
						ProfileImage = x.Partner == null ?
									   null :
									   x.Partner.ProfileImage,
						Nickname = x.Partner == null ?
								   "알 수 없음" :
								   x.Partner.Nickname,
						Content = x.LastMessage == null ? "서로 연결되었습니다. 대화를 시작하세요." :
								  x.LastMessage.Type == MessageTypes.Text ? x.LastMessage.Content :
								  "[이미지]",
						x.NotReadCount,
						CreateTime = x.LastMessage == null ? x.RoomCreateTime : x.LastMessage.CreateTime
					})
					.OrderByDescending(x => x.CreateTime)
					.Skip(skipCount)
					.Take(20)
					.ToArray();

				return new { items };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object ExcuteCreateChattingRoom(
			[FromForm] string apikey,
			[FromForm] int partnerId)
		{
			try
			{
				var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)
					.FirstOrDefault();

				if (member == null)
					throw new Exception("잘못된 요청입니다.");

				var room = this.Db.ChattingRooms
					.FirstOrDefault(x => (x.Member1Id == member.Id && x.Member2Id == partnerId) ||
										 (x.Member2Id == member.Id && x.Member1Id == partnerId));

				if (room == null)
				{
					room = new ChattingRoom
					{
						Member1Id = member.Id,
						Member2Id = partnerId,
						CreateTime = DateTime.Now
					};
					this.Db.Add(room);
					this.Db.SaveChanges();
				}
				else
				{
					if (room.Member1Id == member.Id)
					{
						room.IsCloseMember1 = false;
					}
					else
					{
						room.IsCloseMember2 = false;
					}

					this.Db.SaveChanges();
				}

				return new { RoomId = room.Id };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object GetChattingInfo(
			[FromForm] string apikey,
			[FromForm] int roomId)
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

				var myMsg = this.Db.ChattingMessages
					.Where(x => x.ChattingRoomId == roomId)
					.Where(x => x.ReceiverId == memberId);

				foreach (var msg in myMsg)
				{
					msg.IsShow = true;
				}
				this.Db.SaveChanges();

				var item = this.Db.ChattingRooms
					.Where(x => x.Id == roomId)
					.Select(x => new
					{
						Room = new
						{
							x.Id,
							IsPayPoint = x.OpenMemberId.HasValue,
							x.UsePoint,
							x.IsCloseMember1,
							x.IsCloseMember2,
							x.OpenMemberId,
							x.StarPoint,
							Member1 = x.Member1 == null ? null : new
							{
								x.Member1.Id,
								ProfileImage = x.Member1.Member_ProfileImages
									.OrderBy(z => z.CreateTime)
									.Select(z => z.Url)
									.FirstOrDefault(),
								x.Member1.Nickname
							},
							Member2 = x.Member2 == null ? null : new
							{
								x.Member2.Id,
								ProfileImage = x.Member2.Member_ProfileImages
									.OrderBy(z => z.CreateTime)
									.Select(z => z.Url)
									.FirstOrDefault(),
								x.Member2.Nickname
							},
							x.CreateTime,
						},
						Messages = x.ChattingMessages
							.OrderBy(z => z.CreateTime)
							.Select(z => new
							{
								z.Id,
								z.Content,
								z.IsShow,
								z.Type,
								IsMyMsg = z.SenderId == memberId,
								z.CreateTime,
							})
					})
					.FirstOrDefault();

				return new { item };
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object ExcuteOpenChatting(
			[FromForm] string apikey,
			[FromForm] int id)
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

					var room = this.Db.ChattingRooms
						.Where(x => x.Id == id)
						.FirstOrDefault();

					if (member.FreeChattingTime.HasValue && member.FreeChattingTime.Value >= DateTime.Now)
					{
						room.UsePoint = false;
					}
					else if (member.FreeChattingCount > 0)
					{
						room.UsePoint = false;
						member.FreeChattingCount--;
						this.Db.SaveChanges();
					}
					else if (member.Point >= 5)
					{
						room.UsePoint = true;
						member.Point -= 5;

						this.Db.Add(new Member_PointLog
						{
							AcceptPoint = -5,
							Comment = "채팅창 오픈",
							CreateTime = DateTime.Now,
							CurrentPoint = member.Point,
							MemberId = member.Id
						});
						this.Db.SaveChanges();
					}
					else
					{
						throw new Exception("딸기가 부족합니다.");
					}

					room.OpenMemberId = member.Id;
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
		public async Task<object> ExcuteSendTextMessage(
			[FromForm] string apikey,
			[FromForm] int roomId,
			[FromForm] int partnerId,
			[FromForm] string message)
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
						.Where(x => x.ApiKey == apikey)
						.Select(x => new
						{
							x.Id,
							x.Nickname,
						})
						.FirstOrDefault();

					var partner = this.Db.Members
						.Where(x => x.Id == partnerId)
						.Select(x => new
						{
							x.Id,
							x.PushToken,
						})
						.FirstOrDefault();

					var memberId = member.Id;

					var hasRoom = this.Db.ChattingRooms
						.Where(x => x.Id == roomId)
						.Any(x => (x.Member1Id == memberId && x.Member2Id == partnerId) || x.Member2Id == memberId && x.Member1Id == partnerId);

					if (memberId == 0 || !hasRoom)
						throw new Exception("잘못된 요청입니다.");

					var bw = this.Db.BlockWords
						.Select(x => x.Word)
						.ToArray();
					if (bw.Any(x => message.Contains(x)))
						throw new Exception("금칙어가 포함되어 있습니다.");

					var item = new ChattingMessage
					{
						ChattingRoomId = roomId,
						Content = message,
						CreateTime = DateTime.Now,
						IsShow = false,
						ReceiverId = partnerId,
						SenderId = memberId,
						Type = MessageTypes.Text
					};
					this.Db.Add(item);
					this.Db.SaveChanges();
					tran.Commit();

					if (!string.IsNullOrWhiteSpace(partner.PushToken))
					{
						await this.Firebase.SendPushAsync(partner.PushToken, member.Nickname, message, "chatting:message", new
						{
							item.ChattingRoomId,
							Item = new
							{
								item.Id,
								item.Content,
								item.CreateTime,
								IsMyMsg = false,
								item.IsShow,
								item.Type,
							}
						});
					}

					return new { messageId = item.Id };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteSendImageMessage(
			[FromForm] string apikey,
			[FromForm] int roomId,
			[FromForm] int partnerId,
			IFormFile file)
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
						.Where(x => x.ApiKey == apikey)
						.Select(x => new
						{
							x.Id,
							x.Nickname,
						})
						.FirstOrDefault();

					var partner = this.Db.Members
						.Where(x => x.Id == partnerId)
						.Select(x => new
						{
							x.Id,
							x.PushToken,
						})
						.FirstOrDefault();

					var memberId = member.Id;

					var hasRoom = this.Db.ChattingRooms
						.Where(x => x.Id == roomId)
						.Any(x => (x.Member1Id == memberId && x.Member2Id == partnerId) || x.Member2Id == memberId && x.Member1Id == partnerId);

					if (memberId == 0 || !hasRoom)
						throw new Exception("잘못된 요청입니다.");

					var filename = file.FileName;
					var stream = file.OpenReadStream();

					var path = this.ImageHelper.SaveImageFromStream(stream, "chatting", Guid.NewGuid().ToString() + Path.GetExtension(filename));

					var item = new ChattingMessage
					{
						ChattingRoomId = roomId,
						Content = $"{this.Request.Scheme}://{this.Request.Host.Value}{path.url}",
						CreateTime = DateTime.Now,
						IsShow = false,
						ReceiverId = partnerId,
						SenderId = memberId,
						Type = MessageTypes.Image
					};
					this.Db.Add(item);
					this.Db.SaveChanges();
					tran.Commit();

					if (!string.IsNullOrWhiteSpace(partner.PushToken))
					{
						await this.Firebase.SendPushAsync(partner.PushToken, member.Nickname, "사진이 도착했어요", "chatting:message", new
						{
							item.ChattingRoomId,
							Item = new
							{
								item.Id,
								item.Content,
								item.CreateTime,
								IsMyMsg = false,
								item.IsShow,
								item.Type,
							}
						});
					}

					return new { messageId = item.Id, Url = item.Content };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteRemoveChattingRoom(
	[FromForm] string apikey,    // API 키
	[FromForm] int roomId)       // 채팅방 ID
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
						.Where(x => x.ApiKey == apikey)    // API 키로 회원 조회
						.Select(x => new
						{
							x.Id    // 회원 ID
						})
						.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					var memberId = member.Id;   // 회원 ID 저장

					var room = this.Db.ChattingRooms
						.Where(x => x.Id == roomId)  // 채팅방 ID로 채팅방 조회
						.Where(x => x.Member1Id == memberId || x.Member2Id == memberId)   // 회원이 채팅방 멤버인지 확인
						.FirstOrDefault();

					if (room != null)
					{
						if (room.Member1Id == memberId)
						{
							room.IsCloseMember1 = true; // 회원1의 채팅방 종료 상태 업데이트
							this.Db.SaveChanges();
						}

						if (room.Member2Id == memberId)
						{
							room.IsCloseMember2 = true; // 회원2의 채팅방 종료 상태 업데이트
							this.Db.SaveChanges();
						}

						if (room.IsCloseMember1 && room.IsCloseMember2)
						{
							this.Db.Remove(room);   // 두 회원이 모두 채팅방을 종료한 경우 채팅방 제거
							this.Db.SaveChanges();
						}
						else
						{
							var partnerId = room.Member1Id == memberId ? room.Member2Id : room.Member1Id;   // 상대방의 회원 ID

							var partner = this.Db.Members
								.Where(x => x.Id == partnerId)   // 상대방 회원 조회
								.Select(x => new
								{
									x.PushToken,    // 상대방의 푸시 토큰
									x.Nickname      // 상대방의 닉네임
								})
								.FirstOrDefault();

							if (partner != null && !string.IsNullOrWhiteSpace(partner.PushToken))
							{
								await Firebase.SendPushAsync(partner.PushToken, "딸기톡", $"{partner.Nickname}님이 채팅을 종료했습니다.",
									"chatting:closeroom",
									new
									{
										roomId = room.Id   // 푸시 알림에 전달할 데이터: 종료된 채팅방 ID
									});
							}
						}

						tran.Commit();   // 트랜잭션 커밋
					}

					return new { };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object ExcuteShowChattingMessage(
			[FromForm] string apikey,    // API 키
			[FromForm] int messageId)    // 채팅 메시지 ID
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
						.Where(x => x.ApiKey == apikey)    // API 키로 회원 조회
						.Select(x => new
						{
							x.Id    // 회원 ID
						})
						.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					var memberId = member.Id;   // 회원 ID 저장

					var item = this.Db.ChattingMessages
						.Where(x => x.Id == messageId)  // 채팅 메시지 ID로 채팅 메시지 조회
						.FirstOrDefault();

					item.IsShow = true;   // 채팅 메시지를 표시 상태로 변경
					this.Db.SaveChanges();

					tran.Commit();   // 트랜잭션 커밋

					return new { };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public object ExcuteRoomStarPoint(
			[FromForm] string apikey,        // API 키
			[FromForm] int roomId,           // 채팅방 ID
			[FromForm] int starPoint)        // 별점
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
						.Where(x => x.ApiKey == apikey)    // API 키로 회원 조회
						.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					var memberId = member.Id;   // 회원 ID 저장

					member.Point += 1;   // 회원의 포인트에 1 추가
					this.Db.SaveChanges();

					this.Db.Add(new Member_PointLog
					{
						AcceptPoint = +1,   // 적립된 포인트
						Comment = "채팅 별점 평가",
						CreateTime = DateTime.Now,   // 현재 시간
						CurrentPoint = member.Point,   // 업데이트된 포인트
						MemberId = member.Id   // 회원 ID
					});
					this.Db.SaveChanges();

					var item = this.Db.ChattingRooms
						.Where(x => x.Id == roomId && x.StarPoint == 0 && x.OpenMemberId == memberId)   // 채팅방 ID, 별점 없음, 회원이 채팅방을 열었을 때
						.FirstOrDefault();

					item.StarPoint = starPoint;   // 채팅방의 별점 업데이트
					this.Db.SaveChanges();

					tran.Commit();   // 트랜잭션 커밋

					return new { };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteBlockAndRemoveChattingRoom(
			[FromForm] string apikey,    // API 키
			[FromForm] int roomId)       // 채팅방 ID
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)    // API 키로 회원 조회
					.Select(x => new
					{
						x.Id    // 회원 ID
					})
					.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					var memberId = member.Id;   // 회원 ID 저장

					var room = this.Db.ChattingRooms
						.Where(x => x.Id == roomId)  // 채팅방 ID로 채팅방 조회
						.Where(x => x.Member1Id == memberId || x.Member2Id == memberId)   // 회원이 채팅방 멤버인지 확인
						.FirstOrDefault();

					if (room == null)
						throw new Exception("잘못된 요청입니다.");

					var partnerId = room.Member1Id == memberId ? room.Member2Id : room.Member1Id;   // 상대방의 회원 ID

					if (room.Member1Id == memberId)
					{
						room.IsCloseMember1 = true; // 회원1의 채팅방 종료 상태 업데이트
						this.Db.SaveChanges();
					}

					if (room.Member2Id == memberId)
					{
						room.IsCloseMember2 = true; // 회원2의 채팅방 종료 상태 업데이트
						this.Db.SaveChanges();
					}

					if (room.IsCloseMember1 && room.IsCloseMember2)
					{
						this.Db.Remove(room);   // 두 회원이 모두 채팅방을 종료한 경우 채팅방 제거
						this.Db.SaveChanges();
					}
					else
					{
						var partner = this.Db.Members
							.Where(x => x.Id == partnerId)   // 상대방 회원 조회
							.Select(x => new
							{
								x.PushToken,    // 상대방의 푸시 토큰
								x.Nickname      // 상대방의 닉네임
							})
							.FirstOrDefault();

						if (partner != null && !string.IsNullOrWhiteSpace(partner.PushToken))
						{
							await Firebase.SendPushAsync(partner.PushToken, "딸기톡", $"{partner.Nickname}님이 채팅을 종료했습니다.",
								"chatting:closeroom",
								new
								{
									roomId = room.Id   // 푸시 알림에 전달할 데이터: 종료된 채팅방 ID
								});
						}
					}

					if (partnerId.HasValue)
					{
						var blockMember = new Member_BlockPartner
						{
							CreateTime = DateTime.Now,   // 현재 시간
							MemberId = memberId,         // 회원 ID
							PartnerId = partnerId,       // 상대방 회원 ID
						};
						this.Db.Add(blockMember);
						this.Db.SaveChanges();
					}

					tran.Commit();   // 트랜잭션 커밋

					return new { };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteMannerAndRemoveChattingRoom(
			[FromForm] string apikey,            // API 키
			[FromForm] int roomId,               // 채팅방 ID
			[FromForm] AppraisalTypes appraisalType,   // 평가 유형
			[FromForm] string[] comments)        // 평가 코멘트 배열
		{
			try
			{
				if (comments == null || comments.Length == 0)
					throw new Exception("잘못된 요청입니다.");

				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)    // API 키로 회원 조회
					.Select(x => new
					{
						x.Id    // 회원 ID
					})
					.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					var memberId = member.Id;   // 회원 ID 저장

					var room = this.Db.ChattingRooms
						.Where(x => x.Id == roomId)  // 채팅방 ID로 채팅방 조회
						.Where(x => x.Member1Id == memberId || x.Member2Id == memberId)   // 회원이 채팅방 멤버인지 확인
						.FirstOrDefault();

					if (room == null)
						throw new Exception("잘못된 요청입니다.");

					var partnerId = room.Member1Id == memberId ? room.Member2Id : room.Member1Id;   // 상대방의 회원 ID

					foreach (var comment in comments)
					{
						this.Db.Member_Appraisals.Add(new Member_Appraisal
						{
							AppraisalType = appraisalType,   // 평가 유형
							Comment = comment,                // 평가 코멘트
							CreateTime = DateTime.Now,        // 현재 시간
							MemberId = memberId,              // 회원 ID
							PartnerId = partnerId             // 상대방 회원 ID
						});
					}
					this.Db.SaveChanges();

					if (room.Member1Id == memberId)
					{
						room.IsCloseMember1 = true; // 회원1의 채팅방 종료 상태 업데이트
						this.Db.SaveChanges();
					}

					if (room.Member2Id == memberId)
					{
						room.IsCloseMember2 = true; // 회원2의 채팅방 종료 상태 업데이트
						this.Db.SaveChanges();
					}

					if (room.IsCloseMember1 && room.IsCloseMember2)
					{
						this.Db.Remove(room);   // 두 회원이 모두 채팅방을 종료한 경우 채팅방 제거
						this.Db.SaveChanges();
					}
					else
					{
						var partner = this.Db.Members
							.Where(x => x.Id == partnerId)   // 상대방 회원 조회
							.Select(x => new
							{
								x.PushToken,    // 상대방의 푸시 토큰
								x.Nickname      // 상대방의 닉네임
							})
							.FirstOrDefault();

						if (partner != null && !string.IsNullOrWhiteSpace(partner.PushToken))
						{
							await Firebase.SendPushAsync(partner.PushToken, "딸기톡", $"{partner.Nickname}님이 채팅을 종료했습니다.",
								"chatting:closeroom",
								new
								{
									roomId = room.Id   // 푸시 알림에 전달할 데이터: 종료된 채팅방 ID
								});
						}
					}

					tran.Commit();   // 트랜잭션 커밋

					return new { };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}

		[HttpPost]
		public async Task<object> ExcuteRefeelPoint(
			[FromForm] string apikey,    // API 키
			[FromForm] int roomId)       // 채팅방 ID
		{
			try
			{
				using (var tran = this.Db.Database.BeginTransaction())
				{
					var member = this.Db.Members
					.Where(x => x.ApiKey == apikey)    // API 키로 회원 조회
					.FirstOrDefault();

					if (member == null)
						throw new Exception("잘못된 요청입니다.");

					var memberId = member.Id;   // 회원 ID 저장

					var room = this.Db.ChattingRooms
						.Where(x => x.Id == roomId)  // 채팅방 ID로 채팅방 조회
						.Where(x => x.Member1Id == memberId || x.Member2Id == memberId)   // 회원이 채팅방 멤버인지 확인
						.FirstOrDefault();

					if (room == null)
						throw new Exception("잘못된 요청입니다.");

					var partnerId = room.Member1Id == memberId ? room.Member2Id : room.Member1Id;   // 상대방의 회원 ID

					if (!room.UsePoint || !room.OpenMemberId.HasValue || room.OpenMemberId.Value != memberId)
						throw new Exception("잘못된 요청입니다.");

					member.Point += 5;   // 회원의 포인트에 5 추가

					this.Db.Add(new Member_PointLog
					{
						AcceptPoint = 5,   // 적립된 포인트
						Comment = "채팅창 오픈 취소",
						CreateTime = DateTime.Now,   // 현재 시간
						CurrentPoint = member.Point,   // 업데이트된 포인트
						MemberId = member.Id   // 회원 ID
					});
					this.Db.SaveChanges();

					if (room.Member1Id == memberId)
					{
						room.IsCloseMember1 = true; // 회원1의 채팅방 종료 상태 업데이트
						this.Db.SaveChanges();
					}

					if (room.Member2Id == memberId)
					{
						room.IsCloseMember2 = true; // 회원2의 채팅방 종료 상태 업데이트
						this.Db.SaveChanges();
					}

					if (room.IsCloseMember1 && room.IsCloseMember2)
					{
						this.Db.Remove(room);   // 두 회원이 모두 채팅방을 종료한 경우 채팅방 제거
						this.Db.SaveChanges();
					}
					else
					{
						var partner = this.Db.Members
							.Where(x => x.Id == partnerId)   // 상대방 회원 조회
							.Select(x => new
							{
								x.PushToken,    // 상대방의 푸시 토큰
								x.Nickname      // 상대방의 닉네임
							})
							.FirstOrDefault();

						if (partner != null && !string.IsNullOrWhiteSpace(partner.PushToken))
						{
							await Firebase.SendPushAsync(partner.PushToken, "딸기톡", $"{partner.Nickname}님이 채팅을 종료했습니다.",
								"chatting:closeroom",
								new
								{
									roomId = room.Id   // 푸시 알림에 전달할 데이터: 종료된 채팅방 ID
								});
						}
					}

					tran.Commit();   // 트랜잭션 커밋

					return new { };
				}
			}
			catch (Exception ex)
			{
				return new { ex.Message };
			}
		}
	}
}
