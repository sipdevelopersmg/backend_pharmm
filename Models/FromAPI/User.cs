using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.FromAPI
{
    public class user
    {
        public short id_user { get; set; }
        public short id_role { get; set; }
        public string nama_role { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public short id_outlet { get; set; }
        public DateTime time_last_login { get; set; }
        public DateTime time_last_logout { get; set; }
        public string app_tipe { get; set; }
        public bool is_active { get; set; }
        public short user_created { get; set; }
        public string user_name_created { get; set; }
        public string full_name_created { get; set; }
        public DateTime time_created { get; set; }
        public short user_deactived { get; set; }
        public string user_name_deactived { get; set; }
        public string full_name_deactived { get; set; }
        public DateTime time_deactived { get; set; }
    }
}
