using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationPackagesService
    {
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackages()
        {
            var context = new HMSContext();

            return context.AccomodationPackages.ToList();
        }

        public IEnumerable<AccomodationPackage> GetAllAccomodationPackagesByAccomodationType(int accomodationTypeID)
        {
            var context = new HMSContext();

            return context.AccomodationPackages.Where(x=>x.AccomodationTypeID == accomodationTypeID).ToList();
        }

        public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchTerm, int? accomodationTypeID, int page, int recordSize)
        {
            var context = new HMSContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }

            var skip = (page - 1) * recordSize;
            //  skip = (1    -  1) = 0 * 3 = 0
            //  skip = (2    -  1) = 1 * 3 = 3
            //  skip = (3    -  1) = 2 * 3 = 6

            return accomodationPackages.OrderBy(x=>x.AccomodationTypeID).Skip(skip).Take(recordSize).ToList();
        }
        
        public int SearchAccomodationPackagesCount(string searchTerm, int? accomodationTypeID)
        {
            var context = new HMSContext();

            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }
            
            return accomodationPackages.Count();
        }
        
        public AccomodationPackage GetAccomodationPackageByID(int ID)
        {
            var context = new HMSContext();

            return context.AccomodationPackages.Find(ID);
        }

        public bool SaveAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();

            context.AccomodationPackages.Add(accomodationPackage);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();

            var exitingAccomodationPackage = context.AccomodationPackages.Find(accomodationPackage.ID);

            context.AccomodationPackagePictures.RemoveRange(exitingAccomodationPackage.AccomodationPackagePictures);

            context.Entry(exitingAccomodationPackage).CurrentValues.SetValues(accomodationPackage);

            context.AccomodationPackagePictures.AddRange(accomodationPackage.AccomodationPackagePictures);

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();

            context.Entry(accomodationPackage).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

        public List<AccomodationPackagePicture> GetPicturesByAccomodationPackageID(int accomodationPackageID)
        {
            var context = new HMSContext();

            return context.AccomodationPackages.Find(accomodationPackageID).AccomodationPackagePictures.ToList();
        }
    }
}
