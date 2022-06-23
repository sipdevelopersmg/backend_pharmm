using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record akun_setup_grup_coa
    {

        public Int16? id_grup_coa { get; set; } //smallint()
        public string grup_coa { get; set; } //character varying(20)
        public string deskripsi { get; set; } //character varying(100)
        public bool is_active { get; set; } //bool

    }


    public record akun_setup_grup_coa_insert
    {

        public string grup_coa { get; set; } //character varying(20)
        public string deskripsi { get; set; } //character varying(100)
        public bool is_active { get; set; } //bool

    }



}
