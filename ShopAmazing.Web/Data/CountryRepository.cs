﻿using ShopAmazing.Web.Data.Entities;

namespace ShopAmazing.Web.Data
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {

        public CountryRepository(DataContext context) : base(context)
        {
        }
    }
}