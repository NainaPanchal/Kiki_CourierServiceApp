using System;
using System.Collections.Generic;
using Kiki_CourierServiceApp.Models;    

namespace Kiki_CourierServiceApp.Services;

public interface IOffersService
{
    List<Offer> GetOffers();
}
public class InMemoryOfferProvider : IOffersService
    {
        public List<Offer> GetOffers() =>
            new List<Offer>
            {
                new Offer { Code = "OFR001", DiscountPercent = 10, MinWeight = 70, MaxWeight = 200, MinDistance = 0, MaxDistance = 200 } ,
                new Offer { Code = "OFR002", DiscountPercent = 7, MinWeight = 100, MaxWeight = 250, MinDistance = 50, MaxDistance = 150 } ,
                new Offer { Code = "OFR003", DiscountPercent = 5, MinWeight = 10, MaxWeight = 150, MinDistance = 50, MaxDistance = 250 }
            };
    }

     