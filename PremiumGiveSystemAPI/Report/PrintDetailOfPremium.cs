using System;
using DevExpress.XtraReports.UI;
using PremiumGiveSystem.DataContact;

namespace PremiumGiveSystemAPI.Report
{
    public partial class PrintDetailOfPremium
    {
        public PrintDetailOfPremium(BANC_PREMIUM_GIFT_DATA[] request)
        {
            InitializeComponent();
            this.DataSource = request;

        }
    }
}
