using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;

namespace CQWELogger
{
    
    public class CQWEContact
    {
        public long QSONumber { get; set; }
        public DateTime QSODate { get; set; }
        public string QSOBand { get; set; }
        public string QSOMode { get; set; }
        public string QSOStation { get; set; }
        public string QSOName { get; set; }
        public string QSOLocation { get; set; }
        public int QSOYearsOfService { get; set; }
        public bool QSOValidated { get; set; }
    }
}
