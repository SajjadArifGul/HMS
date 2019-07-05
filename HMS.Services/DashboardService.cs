using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class DashboardService
    {
        public bool SavePicture(Picture picture)
        {
            var context = new HMSContext();

            context.Pictures.Add(picture);

            return context.SaveChanges() > 0;
        }
        
        public IEnumerable<Picture> GetPicturesByIDs(List<int> pictureIDs)
        {
            var context = new HMSContext();

            return pictureIDs.Select(x => context.Pictures.Find(x)).ToList();
        }
    }
}
