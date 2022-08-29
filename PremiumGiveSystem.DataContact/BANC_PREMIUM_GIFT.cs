using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumGiveSystem.DataContact
{
    public class BANC_PREMIUM_GIFT
    {
        public string PROMOTION { get; set; }
        public string CREATED_DT { get; set; }
        public string BBL_CODE { get; set; }
        public string POLICY_ID { get; set; }
        public string APP_NO { get; set; }
        public string PREMIUM { get; set; }
        public string TOTAL_PREMIUM { get; set; }
        public string PACKAGE_DT { get; set; }
        public string DELIVERY_DT { get; set; }
        public string REMARK { get; set; }
        public string UPD_DT { get; set; }
        public string UPD_ID { get; set; }
        public string PRODUCT { get; set; }
        public string PRODUCT_EXTRA { get; set; }
        public string FLG_RECEIVE { get; set; }
        public string TMN { get; set; }
        public string TMN_DT { get; set; }
        public string BANC_TYPE { get; set; }
    }

    public class BANC_PREMIUM_GIFT_DATA : BANC_PREMIUM_GIFT
    {
        public string BBL_NAME { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_EXTRA_NAME { get; set; }
        public string INSTALL_DT { get; set; }
        public string POLICY_NUMBER { get; set; }
        public string CERT_NUMBER { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string IDCARD_NO { get; set; }
        public string NUMBER_OF_POLICY { get; set; }
        public string PROMOTION_NAME { get; set; }
        public string START_INSTALL_DT { get; set; }
        public string END_INSTALL_DT { get; set; }
        public string MIN_PREMIUM { get; set; }
        public string MAX_PREMIUM { get; set; }
        public string BANK_ADDRESS { get; set; }
        public string CUS_ADDRESS { get; set; }
        public string TITLE { get; set; }
        public string PL_BLOCK { get; set; }
        public string PL_CODE { get; set; }
        public string PL_CODE2 { get; set; }
        public int GroupId { get; set; }
        public string PremiumRange { get; set; }
        public string BANK_PROVINCE { get; set; }
        public string BANK_PROVINCE_GROUP { get; set; }

        public int NumberOfProduct { get; set; }
        public bool NeedAddress { get; set; }
        public string BlaAdr { get; set; }
        public string DeliAdr { get; set; }

    }

}
