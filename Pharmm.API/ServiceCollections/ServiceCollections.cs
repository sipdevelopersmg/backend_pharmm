using DapperPostgreSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmm.API.Dao;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Helper;
using Pharmm.API.Services;
using Pharmm.API.Services.Setup;
using Pharmm.API.Services.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.ServiceCollections
{
    public static class ServicesExtensions
    {

        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            string constr = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            services.AddScoped(
              c =>
                  new SQLConn(
                      constr
                  )
              );


            #region Localization

            services.AddScoped<LocalizationDao>();
            services.AddScoped<LocalizationHelper>();

            #endregion

            #region Setup

            services.AddHttpContextAccessor();
            
            services.AddScoped<ISetupJenisSettingAuditStokOpnameService, SetupJenisSettingAuditStokOpnameService>();
            services.AddScoped<SetupJenisSettingAuditStokOpnameDao>();

            services.AddScoped<ISetupAuditRakStorageService, SetupAuditRakStorageService>();
            services.AddScoped<SetupAuditRakStorageDao>();

            services.AddScoped<ISetupAuditPenanggungJawabRakStorageService, SetupAuditPenanggungJawabRakStorageService>();
            services.AddScoped<SetupAuditPenanggungJawabRakStorageDao>();

            services.AddScoped<ISetupTipeStockroomService, SetupTipeStockroomService>();
            services.AddScoped<SetupTipeStockroomDao>();

            services.AddScoped<ISetupStockroomService, SetupStockroomService>();
            services.AddScoped<SetupStockroomDao>();

            services.AddScoped<ISetupSupplierService, SetupSupplierService>();
            services.AddScoped<SetupSupplierDao>();

            services.AddScoped<ISetupSupplierRekeningService, SetupSupplierRekeningService>();
            services.AddScoped<SetupSupplierRekeningDao>();

            services.AddScoped<ISetupTipeSupplierService, SetupTipeSupplierService>();
            services.AddScoped<SetupTipeSupplierDao>();

            services.AddScoped<ISetupSatuanService, SetupSatuanService>();
            services.AddScoped<SetupSatuanDao>();

            services.AddScoped<ISetupKonversiSatuanService, SetupKonversiSatuanService>();
            services.AddScoped<SetupKonversiSatuanDao>();

            services.AddScoped<ISetupTemperaturItemService, SetupTemperaturItemService>();
            services.AddScoped<SetupTemperaturItemDao>();

            services.AddScoped<ISetupPabrikService, SetupPabrikService>();
            services.AddScoped<SetupPabrikDao>();

            services.AddScoped<ISetupItemService, SetupItemService>();
            services.AddScoped<SetupItemDao>();

            services.AddScoped<ISetupObatService, SetupObatService>();
            services.AddScoped<SetupObatDao>();

            services.AddScoped<ISetupItemSatuanService, SetupItemSatuanService>();
            services.AddScoped<SetupItemSatuanDao>();

            services.AddScoped<ISetupTipeItemService, SetupTipeItemService>();
            services.AddScoped<SetupTipeItemDao>();

            services.AddScoped<ISetupGrupItemService, SetupGrupItemService>();
            services.AddScoped<SetupGrupItemDao>();

            services.AddScoped<ISetupPerencanaanKategoriService, SetupPerencanaanKategoriService>();
            services.AddScoped<SetupPerencanaanKategoriDao>();

            services.AddScoped<ISetupGrupCoaService, SetupGrupCoaService>();
            services.AddScoped<SetupGrupCoaDao>();

            services.AddScoped<ISetupCoaService, SetupCoaService>();
            services.AddScoped<SetupCoaDao>();

            services.AddScoped<ISetupStokItemService, SetupStokItemService>();
            services.AddScoped<SetupStokItemDao>();

            services.AddScoped<ISetupUkuranDokumenService, SetupUkuranDokumenService>();
            services.AddScoped<SetupUkuranDokumenDao>();

            services.AddScoped<IKartuStokItemService, KartuStokItemService>();
            services.AddScoped<KartuStokItemDao>();

            services.AddScoped<ISetupJenisPenerimaanService, SetupJenisPenerimaanService>();
            services.AddScoped<SetupJenisPenerimaanDao>();

            services.AddScoped<ISetupMekanismeReturService, SetupMekanismeReturService>();
            services.AddScoped<SetupMekanismeReturDao>();

            services.AddScoped<ISetupPaymentTermService, SetupPaymentTermService>();
            services.AddScoped<SetupPaymentTermDao>();

            services.AddScoped<ISetupShippingMethodService, SetupShippingMethodService>();
            services.AddScoped<SetupShippingMethodDao>();

            services.AddScoped<IMasterCounterService, MasterCounterService>();
            services.AddScoped<MasterCounterDao>();

            #endregion

            #region Transaksi

            services.AddScoped<ITransAuditStokOpnameTanpaSettingService, TransAuditStokOpnameTanpaSettingService>();
            services.AddScoped<TransAuditStokOpnameTanpaSettingDao>();

            services.AddScoped<ITransAuditStokOpnameTanpaSettingNoEdService, TransAuditStokOpnameTanpaSettingNoEdService>();
            services.AddScoped<TransAuditStokOpnameTanpaSettingNoEdDao>();

            services.AddScoped<ITransAuditStokOpnameService, TransAuditStokOpnameService>();
            services.AddScoped<TransAuditStokOpnameDao>();

            services.AddScoped<ITransAuditStokOpnameNoEdService, TransAuditStokOpnameNoEdService>();
            services.AddScoped<TransAuditStokOpnameNoEdDao>();

            services.AddScoped<ITransSettingAuditStokOpnameService, TransSettingAuditStokOpnameService>();
            services.AddScoped<TransSettingAuditStokOpnameDao>();

            services.AddScoped<ITransPemusnahanStokService, TransPemusnahanStokService>();
            services.AddScoped<TransPemusnahanStokDao>();

            services.AddScoped<ITransRepackingService, TransRepackingService>();
            services.AddScoped<TransRepackingDao>();

            services.AddScoped<ITransRepackingNoEdService, TransRepackingNoEdService>();
            services.AddScoped<TransRepackingNoEdDao>();

            services.AddScoped<ITransAssemblyService, TransAssemblyService>();
            services.AddScoped<TransAssemblyDao>();

            services.AddScoped<ITransAssemblyNoEdService, TransAssemblyNoEdService>();
            services.AddScoped<TransAssemblyNoEdDao>();

            services.AddScoped<ITransKontrakSpjbService, TransKontrakSpjbService>();
            services.AddScoped<TransKontrakSpjbDao>();

            services.AddScoped<ITransSetHargaOrderService, TransSetHargaOrderService>();
            services.AddScoped<TransSetHargaOrderDao>();

            services.AddScoped<ITransPemesananService, TransPemesananService>();
            services.AddScoped<TransPemesananDao>();

            services.AddScoped<ITransPenerimaanService, TransPenerimaanService>();
            services.AddScoped<TransPenerimaanDao>();

            services.AddScoped<ITransPenerimaanNoEdService, TransPenerimaanNoEdService>();
            services.AddScoped<TransPenerimaanNoEdDao>();

            services.AddScoped<ITransReturPembelianNoEdService, TransReturPembelianNoEdService>();
            services.AddScoped<TransReturPembelianNoEdDao>();

            services.AddScoped<ITransReturPembelianService, TransReturPembelianService>();
            services.AddScoped<TransReturPembelianDao>();

            services.AddScoped<ITransMutasiService, TransMutasiService>();
            services.AddScoped<TransMutasiDao>();

            services.AddScoped<ITransMutasiNoEdService, TransMutasiNoEdService>();
            services.AddScoped<TransMutasiNoEdDao>();

            services.AddScoped<ITransPemakaianInternalService, TransPemakaianInternalService>();
            services.AddScoped<TransPemakaianInternalDao>();

            services.AddScoped<ITransPemakaianInternalNoEdService, TransPemakaianInternalNoEdService>();
            services.AddScoped<TransPemakaianInternalNoEdDao>();

            services.AddScoped<ITransReturPemakaianInternalService, TransReturPemakaianInternalService>();
            services.AddScoped<TransReturPemakaianInternalDao>();

            services.AddScoped<ITransReturPemakaianInternalNoEdService, TransReturPemakaianInternalNoEdService>();
            services.AddScoped<TransReturPemakaianInternalNoEdDao>();


            services.AddScoped<TransHutangSupplierDao>();
            services.AddScoped<TransPiutangSupplierDao>();

            #endregion
        }
    }
}
