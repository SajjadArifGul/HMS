using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}