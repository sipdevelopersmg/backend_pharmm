using Newtonsoft.Json;
using Pharmm.API.Models.FromAPI;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.HTTP.Helper;
using Utility.OKResponse.Helper;

namespace Pharmm.API.Helper
{
    public class HttpDataPisHelper
    {
        private static Pasien_antrian pasien { get; set; }
        private static user user { get; set; }
        private static dokter_getall dokter { get; set; }


        //private static long last_id_register { get; set; }
        //private static int last_id_dokter { get; set; }
        //private static short last_id_user { get; set; }

        //untuk mereset data pasien dan dokter
        public static void RefreshObject()
        {
            pasien = null;
            user = null;
            dokter = null;
        }

        //untuk mengambil data dokter dari api pis
        public static async Task<dokter_getall> GetDataDokter(
            int id_dokter
            )
        {
            try
            {

                if (id_dokter != 0)
                {
                    if (dokter is null)
                    {
                        var paramApi = new List<ParameterSearchModel>
                        {
                            new ParameterSearchModel
                            {
                                columnName = "id_dokter",
                                filter = "equal",
                                searchText = id_dokter.ToString(),
                                searchText2 = ""
                            }
                        };

                        var response = await HttpHelper.PostDataFromAPI(
                            url: $"{UrlStaticHelper.GetDataDokter}",
                            param: paramApi);

                        if (response is not null)
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var responseAPI = JsonConvert.DeserializeObject<ResponseModel<List<dokter_getall>>>(response.Content);

                                //last_id_dokter = id_dokter;

                                if (responseAPI.responseResult)
                                {
                                    //return responseAPI.data.First().GetType().GetProperty(property_dokter).GetValue(responseAPI.data.First(), null);

                                    dokter = responseAPI.data.Count > 0 ? responseAPI.data.First() : null;

                                    return dokter;
                                }
                            }
                        }
                    }
                    else
                    {
                        ////hapus data sebelumnya
                        if (dokter.id_dokter != id_dokter)
                        {
                            RefreshObject();
                            await GetDataDokter(id_dokter);
                        }
                        else
                        {
                            return dokter;
                        }
                    }

                    //}
                    //else
                    //{
                    //    //return dokter.First().GetType().GetProperty(property_dokter).GetValue(dokter.First(), null);
                    //    return dokter.First();
                    //}
                }

                return dokter;
            }

            catch (Exception)
            {
                throw;
            }
        }

        //untuk mengambil data pasien dari api pis
        public static async Task<Pasien_antrian> GetDataPasien(
            long id_register,
            string jenis_rawat)
        {
            try
            {

                if (id_register != 0)
                {

                    if (pasien is null)
                    {
                        //untuk get data pasien di pis yang menentukan dari irja/irna/atau irda
                        var urlGetPasien = "";

                        switch (jenis_rawat.ToLower())
                        {
                            case "j": urlGetPasien = UrlStaticHelper.GetDataPasienIRJA; break;
                            case "i": urlGetPasien = UrlStaticHelper.GetDataPasienIRNA; break;
                            case "d": urlGetPasien = UrlStaticHelper.GetDataPasienIRDA; break;
                            default: urlGetPasien = UrlStaticHelper.GetDataPasienIRJA; break;
                        }

                        var response = await HttpHelper.GetDataFromAPI(
                            url: $"{urlGetPasien}/{id_register}",
                            param: null);

                        if (response is not null)
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var responseAPI = JsonConvert.DeserializeObject<ResponseModel<Pasien_antrian>>(response.Content);

                                //last_id_register = id_register;
                                if (responseAPI.responseResult)
                                {
                                    pasien = responseAPI.data;

                                    //var result = responseAPI.data.GetType().GetProperty(property_pasien).GetValue(responseAPI.data, null);

                                    return pasien;
                                }
                            }
                        }

                    }
                    else
                    {
                        //jika id reg terakhir tidak sama dgn curr id reg
                        if (pasien.id_register != id_register)
                        {
                            //reset data
                            RefreshObject();
                            await GetDataPasien(id_register, jenis_rawat);
                        }
                        else
                        {
                            return pasien;
                        }
                    }
                }

                return pasien;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //untuk mengambil data user dari api pis
        public static async Task<user> GetDataUser(
            short id_user)
        {
            try
            {

                if (id_user != 0)
                {

                    if (user is null)
                    {

                        var response = await HttpHelper.GetDataFromAPI(
                            url: $"{UrlStaticHelper.GetDataUser}/{id_user}",
                            param: null);

                        if (response is not null)
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var responseAPI = JsonConvert.DeserializeObject<ResponseModel<user>>(response.Content);

                                //last_id_user = id_user;
                                if (responseAPI.responseResult)
                                {
                                    user = responseAPI.data;

                                    //var result = responseAPI.data.GetType().GetProperty(property_user).GetValue(responseAPI.data, null);

                                    return user;
                                }
                            }
                        }

                    }
                    else
                    {
                        ////hapus data sebelumnya
                        if (user.id_user != id_user)
                        {
                            RefreshObject();
                            await GetDataUser(id_user);
                        }
                        else
                        {
                            return user;
                        }
                    }
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
