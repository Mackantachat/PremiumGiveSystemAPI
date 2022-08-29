using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumGiveSystem.DataContact
{
    public class PREMIUM_GIFT_REQUEST
    {
        public string PROMOTION { get; set; }
        public string START_INSTALL_DT { get; set; }
        public string END_INSTALL_DT { get; set; }
        public string STATUS { get; set; }
        public string START_APP_DT { get; set; }
        public string END_APP_DT { get; set; }
        public string MIN_PREMIUM { get; set; }
        public string MAX_PREMIUM { get; set; }
        public INSURANCE[] INSURANCE { get; set; }
        public bool RECAPTURE { get; set; }

    }
}
