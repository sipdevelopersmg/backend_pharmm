using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Helper
{
    public class UrlStaticHelper
    {

        //kode prefix
        //prefixKodeAntrianResepirja RJA
        //prefixPenjualanObat PO  nomor penjualan obat
        //prefixPenjualanObatTanpaRegister    POTR nomor penjualan obat tanpa register
        //prefixResepDokter RSP untuk prefix nomor resep
        //prefixResepRacikan RAC untuk prefix nomor racikan

        public static string ip = Startup.StaticConfig.GetSection("ServerIp").Value;//"128.199.133.137";
        //public static string ip = "174.138.22.139";
        public static string urlApi = $"http://{ip}";

        public static string socketIOServer = $"{urlApi}:3000/";

        public static string minIoIp = $"{ip}:9090";
        public static string rabbitMqUrl = "rabbitmq://rabbitmq/";
        public static string rabbitMqLocalUrl = $"rabbitmq://{ip}/";

        public static string baseUrlAPI = $"{urlApi}:8888";

        public static string UpdateQtyReturPenjualan = $"{baseUrlAPI}/api/billing/tarif/transdetailobatalkes/returobat";
        public static string BatalPenjualanObat = $"{baseUrlAPI}/api/billing/tarif/transdetailobatalkes/cancelobat";

        public static string GetDataDokter = $"{baseUrlAPI}/api/pis/Person/PersonDokterGetAllByDynamicFilter";

        public static string GetDataUser = $"{baseUrlAPI}/api/pis/User/GetUserByIdUser"; ///{id_user}

        public static string GetDataPasienIRJA = $"{baseUrlAPI}/api/pis/Pasien/GetInformasiPasienIRJAForFarmasiByIdRegister";
        public static string GetDataPasienIRNA = $"{baseUrlAPI}/api/pis/Pasien/GetInformasiPasienIRNAForFarmasiByIdRegister";
        public static string GetDataPasienIRDA = $"{baseUrlAPI}/api/pis/Pasien/GetInformasiPasienIRDAForFarmasiByIdRegister";


    }
}
