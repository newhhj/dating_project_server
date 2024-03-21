using Strawberry.Server.Database.Tables;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Strawberry.Server.Api.Controllers.Authentication
{
	// 회원 가입 데이터 모델 클래스
	public class JoinDataModel
	{
		public int Id { get; set; } // 회원 고유 식별자

		public long? KakaoKey { get; set; } // 카카오 계정 연동을 위한 키

		public string PhoneNumber { get; set; } // 전화번호

		public string Email { get; set; } // 이메일 주소

		public string Password { get; set; } // 비밀번호

		public bool ConsentMarketing { get; set; } // 마케팅 동의 여부

		public GenderTypes Gender { get; set; } // 성별

		public DateTime BirthDay { get; set; } // 생년월일

		public string Nickname { get; set; } // 닉네임

		public int Tall { get; set; } // 키(신장)

		public string BodyStyle { get; set; } // 체형

		public string School { get; set; } // 학교 정보

		public string Job { get; set; } // 직업 정보

		public string Religion { get; set; } // 종교 정보

		public string Alcohol { get; set; } // 음주 여부

		public string Smoking { get; set; } // 흡연 여부

		public double Lat { get; set; } // 위도

		public double Lng { get; set; } // 경도

		public int[] ProfileImageIds { get; set; } // 프로필 이미지의 고유 식별자 배열

		public IFormFile[] ProfileImages { get; set; } // 업로드된 프로필 이미지 파일 배열

		public int Preference_MinAge { get; set; } // 선호하는 최소 나이

		public int Preference_MaxAge { get; set; } // 선호하는 최대 나이

		public int Preference_Range { get; set; } // 선호하는 연령 범위

		public int Preference_MinTall { get; set; } // 선호하는 최소 키

		public int Preference_MaxTall { get; set; } // 선호하는 최대 키

		public bool Preference_BeautyOrWealth { get; set; } // 외모 또는 재력 우선순위

		public string Preference_BodyStyle { get; set; } // 선호하는 체형

		public string Preference_Religion { get; set; } // 선호하는 종교

		public string Preference_Alcohol { get; set; } // 선호하는 음주 여부

		public string Preference_Smoking { get; set; } // 선호하는 흡연 여부

		public PriorityTypes Preference_Priority { get; set; } // 선호도 우선순위

		public string Referrer { get; set; } // 추천인 정보
	}
}
