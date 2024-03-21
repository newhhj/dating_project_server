using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database
{
    public enum MemberStateTypes
    {
        Normal = 0,
        Block,
        JoinWait,
        JoinConfirm,
        JoinDeny
    }

    public enum GenderTypes
    {
        Male = 0,
        Female
    }

    public enum PriorityTypes
    {
        None = 0,
        BeautyOrWealth,
        Age,
        Range,
        Tall,
        Body,
        Religion,
        Alcohol,
        Smoking
    }

    public enum LevelTypes
    {
        Silver = 0,
        Gold,
        Platinum,
        Diamond
    }

    public enum ContentTypes
    {
        /// <summary>
        /// 자랑
        /// </summary>
        Boast = 0,
        /// <summary>
        /// 번개(쏜다)
        /// </summary>
        Metting,
        /// <summary>
        /// 판매
        /// </summary>
        Sell,
        /// <summary>
        /// 수다
        /// </summary>
        Talk
    }

    public enum MessageTypes
    {
        Text = 0,
        Image,
        Voice
    }

    public enum AppraisalTypes
    {
        Manner = 0,
        BadManner
    }

    public enum PurchaseTypes
    {
        Product = 0,
        Subscription = 1
    }

    public enum ADTypes
    {
        MainPopup = 0,
        SettingBanner = 1
    }

    public enum ConfirmImageTypes
    { 
        ProfileImage = 0,
        PoomPoomImage = 1
    }
}
