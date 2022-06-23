using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmm.API.Dao.Laporan;
using Pharmm.API.Services.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.ServiceCollections
{
    public static partial class ServicesExtensions
    {
        public static void AddServiceLaporan(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped<ILaporanBuktiMutasiService, LaporanBuktiMutasiService>();
            services.AddScoped<LaporanBuktiMutasiDao>();


            services.AddScoped<ILaporanPenerimaanBarangService, LaporanPenerimaanBarangService>();
            services.AddScoped<LaporanPenerimaanBarangDao>();

            services.AddScoped<ILaporanPemakaianInternalService, LaporanPemakaianInternalService>();
            services.AddScoped<LaporanPemakaianInternalDao>();

            services.AddScoped<ILaporanRepackingService, LaporanRepackingService>();
            services.AddScoped<LaporanRepackingDao>();

            services.AddScoped<ILaporanAssemblyService, LaporanAssemblyService>();
            services.AddScoped<LaporanAssemblyDao>();

            services.AddScoped<ILaporanReturPembelianService, LaporanReturPembelianService>();
            services.AddScoped<LaporanReturPembelianDao>();
        }
    }
}
