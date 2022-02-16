using Pharmm.API.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Helper
{
    public class LocalizationHelper
    {
        private LocalizationDao _localizationDao;

        public LocalizationHelper(LocalizationDao localizationDao)
        {
            this._localizationDao = localizationDao;
        }

        public async Task<DateTime> GetDate()
        {
            return await _localizationDao.GetDate();
        }

        public bool IsDateBeforeOrToday(DateTime date)
        {
            var now = GetDate().Result.Date;
            var range = DateTime.Compare(date.Date,now);

            //jika negatif maka tgl < sekarang
            if (range > 0)
                return false;
            else
                return true;
        }


    }
}
