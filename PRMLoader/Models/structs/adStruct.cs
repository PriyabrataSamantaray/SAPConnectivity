using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRMLoader.Models.structs
{
    public struct adStruct
    {
        public string adName;
        public string adRole;
        public string adArea;
        public adStruct(string _adName, string _adRole, string _adArea)
        {
            adName = _adName;
            adRole = _adRole;
            adArea = _adArea;
        }
    }
}