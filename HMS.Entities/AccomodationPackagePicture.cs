using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class AccomodationPackagePicture
    {
        public int ID { get; set; }

        public int AccomodationPackageID { get; set; }

        public int PictureID { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
