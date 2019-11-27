using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Vtc_Freelancer.Models
{
    public class GigsViewModel
    {
        public List<Service> Service { get; set; }
        public List<ImageService> ImageService { get; set; }
    }
}