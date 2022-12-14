namespace CouponOps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using CouponOps.Models;
    using Interfaces;

    public class CouponOperations : ICouponOperations
    {
        private Dictionary<string, Website> websitesByName = new Dictionary<string, Website>();
        private Dictionary<string, Coupon> couponsByCode = new Dictionary<string, Coupon>();

        public void AddCoupon(Website website, Coupon coupon)
        {
            if (!websitesByName.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }

            coupon.Website = website;
            couponsByCode.Add(coupon.Code, coupon);
            websitesByName[website.Domain].Coupons.Add(coupon);
        }

        public bool Exist(Website website)
        {
            return websitesByName.ContainsKey(website.Domain) ? true : false;
        }

        public bool Exist(Coupon coupon)
        {
            return couponsByCode.ContainsKey(coupon.Code) ? true : false;
        }

        public IEnumerable<Coupon> GetCouponsForWebsite(Website website)
        {
            if (!websitesByName.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }
            return couponsByCode.Values.Where(w => w.Website.Domain == website.Domain);
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
        {
            return couponsByCode.Values
                .OrderByDescending(c => c.Validity)
                .ThenByDescending(c => c.DiscountPercentage);
        }

        public IEnumerable<Website> GetSites()
        {
            return websitesByName.Values;
        }

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
        {
            var res =  websitesByName.Values
                .OrderBy(w => w.UsersCount)
                .ThenByDescending(w => w.Coupons.Count).ToList();

            return res;
        }

        public void RegisterSite(Website website)
        {
            if (websitesByName.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }

            websitesByName.Add(website.Domain, website);
        }

        public Coupon RemoveCoupon(string code)
        {
            if (!couponsByCode.ContainsKey(code))
            {
                throw new ArgumentException();
            }
            var deletedCoupon = couponsByCode[code];
            couponsByCode.Remove(code);

            return deletedCoupon;
        }

        public Website RemoveWebsite(string domain)
        {
            if (!websitesByName.ContainsKey(domain))
            {
                throw new ArgumentException();
            }
            var deletedWebsite = websitesByName[domain];
            websitesByName.Remove(domain);
            foreach (var coupon in deletedWebsite.Coupons)
            {
                couponsByCode.Remove(coupon.Code);
            }

            return deletedWebsite;
        }

        public void UseCoupon(Website website, Coupon coupon)
        {
            if (!Exist(website) || !Exist(coupon))
            {
                throw new ArgumentException();
            }

            if (!website.Coupons.Contains(coupon))
            {
                throw new ArgumentException();
            }

            RemoveCoupon(coupon.Code);
        }
    }
}
