using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database.Tables
{
    [Table(nameof(Setting))]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        public string AdminId { get; set; }

        public string AdminPw { get; set; }

        public bool UseUpdateAdminId { get; set; }

        public string ManagerEmail { get; set; }

        public string UseTerm { get; set; }

        public string PrivacyTerm { get; set; }

        public string LocationTerm { get; set; }

        public string SensitiveTerm { get; set; }

        public string ContentTerm { get; set; }

        public string MarketingTerm { get; set; }

        public string PatentTerm { get; set; }
    }
}
