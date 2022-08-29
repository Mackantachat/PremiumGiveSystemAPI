using PremiumGiveSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PremiumGiveSystem.DataContract;
using PremiumGiveSystem.DataContact;
using System.Globalization;
using System.Linq;

namespace PremiumGiveSystem.BusinessLogic
{
    public class ServiceAction
    {
        private readonly string connectionString;
        public ServiceAction(string ConnectionName) => this.connectionString = ConnectionName;

        #region "Promotion"
        public BANC_PROMOTION[] GetPromotion(BANC_PROMOTION request) => GetPromotion(request, null);
        private BANC_PROMOTION[] GetPromotion(BANC_PROMOTION request, Repository repository)
        {
            BANC_PROMOTION[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetPromotion(request);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PROMOTION ManagePromotion(BANC_PROMOTION request) => ManagePromotion(request, null);
        private BANC_PROMOTION ManagePromotion(BANC_PROMOTION request, Repository repository)
        {
            BANC_PROMOTION data = new BANC_PROMOTION();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                if (request.Process.ToUpper().Equals("INSERT") && string.IsNullOrEmpty(request.PROMOTION))
                {
                    string seq = repository.GetPromotionSeq();
                    request.PROMOTION = SeqFormat(seq);
                    repository.AddPromotion(request);
                }
                else if (request.Process.ToUpper().Equals("EDIT"))
                {
                    repository.EditPromotion(request);
                }
                data.PROMOTION = request.PROMOTION;

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }
        #endregion "Promotion"

        #region "Product"
        public BANC_PRODUCT[] GetProduct(BANC_PRODUCT request) => GetProduct(request, null);
        private BANC_PRODUCT[] GetProduct(BANC_PRODUCT request, Repository repository)
        {
            BANC_PRODUCT[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetProduct(request);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PRODUCT ManageProduct(BANC_PRODUCT request) => ManageProduct(request, null);
        private BANC_PRODUCT ManageProduct(BANC_PRODUCT request, Repository repository)
        {
            BANC_PRODUCT data = new BANC_PRODUCT();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                if (request.Process.ToUpper().Equals("INSERT") && string.IsNullOrEmpty(request.PRODUCT))
                {
                    string seq = repository.GetProductSeq();
                    request.PRODUCT = SeqFormat(seq);
                    repository.AddProduct(request);
                    
                }
                else if (request.Process.ToUpper().Equals("EDIT"))
                {
                    repository.EditProduct(request);
                }
                data.PRODUCT = request.PRODUCT;
                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }
        #endregion "Product"

        #region"ProductGain"
        public BANC_PRODUCT_GAIN[] GetProductGain(BANC_PRODUCT_GAIN request) => GetProductGain(request, null);
        private BANC_PRODUCT_GAIN[] GetProductGain(BANC_PRODUCT_GAIN request, Repository repository)
        {
            BANC_PRODUCT_GAIN[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetProductGain(request);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PRODUCT_GAIN ManageProductGain(BANC_PRODUCT_GAIN request) => ManageProductGain(request, null);
        private BANC_PRODUCT_GAIN ManageProductGain(BANC_PRODUCT_GAIN request, Repository repository)
        {
            BANC_PRODUCT_GAIN data = new BANC_PRODUCT_GAIN();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                if (request.Process.ToUpper().Equals("INSERT"))
                {
                    var checkProductGain = repository.GetProductGain(request);
                    if (checkProductGain != null)
                    {
                        throw new Exception("มีเงื่อนไขการจัดรายการโปรโมชัน " + checkProductGain[0].PROMOTION_NAME + " สินค้า " + checkProductGain[0].PRODUCT_NAME + " อยู่แล้ว");
                    }
                    repository.AddProductGain(request);
                    data.PRODUCT = request.PRODUCT;
                }
                else if (request.Process.ToUpper().Equals("EDIT"))
                {
                    repository.EditProductGain(request);
                }
                else if (request.Process.ToUpper().Equals("DEL"))
                {
                    repository.DeleteProductGain(request);
                }

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        #endregion"ProductGain"

        #region"Create Table"
        public BANC_CREATE_TABLE[] GetCreateTable(BANC_CREATE_TABLE request) => GetCreateTable(request, null);
        private BANC_CREATE_TABLE[] GetCreateTable(BANC_CREATE_TABLE request, Repository repository)
        {
            BANC_CREATE_TABLE[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetCreateTable(request);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_CREATE_TABLE ManageCreateTable(BANC_CREATE_TABLE request) => ManageCreateTable(request, null);
        private BANC_CREATE_TABLE ManageCreateTable(BANC_CREATE_TABLE request, Repository repository)
        {
            BANC_CREATE_TABLE data = new BANC_CREATE_TABLE();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                if (request.Process.ToUpper().Equals("INSERT"))
                {
                    var checkCreateTable = repository.GetCreateTable(request);
                    if (checkCreateTable != null)
                    {
                        throw new Exception("มีเงื่อนไขการจัดรายการโปรโมชัน " + checkCreateTable[0].PROMOTION_NAME + " สินค้า " + checkCreateTable[0].CREATE_DT + " อยู่แล้ว");
                    }
                    repository.AddCreateTable(request);
                    var newCreateTable = repository.GetCreateTable(request);
                    data.CREATE_DT = newCreateTable[0].CREATE_DT;
                    data.PROMOTION_NAME = newCreateTable[0].PROMOTION_NAME;
                }
                else if (request.Process.ToUpper().Equals("EDIT"))
                {
                    repository.EditCreateTable(request);
                }
                else if (request.Process.ToUpper().Equals("DEL"))
                {
                    repository.DeleteCreateTable(request);
                }

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }
        #endregion"End Create Table"

        #region"Premium Gift"
        public BANC_GROUP[] GetBancGroup() => GetBancGroup(null);
        private BANC_GROUP[] GetBancGroup(Repository repository)
        {
            BANC_GROUP[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetBancGroup();
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }


        public INSURANCE[] GetInsurance(BANC_GROUP bancGroup) => GetInsurance(bancGroup, null);
        private INSURANCE[] GetInsurance(BANC_GROUP bancGroup, Repository repository)
        {
            INSURANCE[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetInsurance(bancGroup);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public ZTB_CONSTANT2[] GetPolicyStatus() => GetPolicyStatus(null);
        private ZTB_CONSTANT2[] GetPolicyStatus(Repository repository)
        {
            ZTB_CONSTANT2[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetPolicyStatus();
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public PREMIUM_GIFT_RESPOND[] GetPremiumGift(PREMIUM_GIFT_REQUEST premiumGift) => GetPremiumGift(premiumGift, null);
        private PREMIUM_GIFT_RESPOND[] GetPremiumGift(PREMIUM_GIFT_REQUEST premiumGift, Repository repository)
        {
            PREMIUM_GIFT_RESPOND[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetPremiumGift(premiumGift);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }


        public BANC_PREMIUM_GIFT SavePremiumGift(BANC_PREMIUM_GIFT[] request) => SavePremiumGift(request, null);
        private BANC_PREMIUM_GIFT SavePremiumGift(BANC_PREMIUM_GIFT[] request, Repository repository)
        {
            BANC_PREMIUM_GIFT data = new BANC_PREMIUM_GIFT();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                foreach (var req in request)
                {
                    var product = repository.GetProductName(req.PROMOTION, req.TOTAL_PREMIUM);
                    req.PRODUCT = string.IsNullOrEmpty(product.PRODUCT) ? null : product.PRODUCT;
                    req.PRODUCT_EXTRA = string.IsNullOrEmpty(product.PRODUCT_EXTRA) ? null : product.PRODUCT_EXTRA;
                    repository.AddPremiumGift(req);
                }

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        #endregion"End Premium Gift"

        #region"EndProcess"
        public BANC_PREMIUM_GIFT_DATA[] GetPremiumGiftEndProcess(BANC_PREMIUM_GIFT_DATA request) => GetPremiumGiftEndProcess(request, null);
        private BANC_PREMIUM_GIFT_DATA[] GetPremiumGiftEndProcess(BANC_PREMIUM_GIFT_DATA request, Repository repository)
        {
            BANC_PREMIUM_GIFT_DATA[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetPremiumGiftEndProcess(request);
                if (data != null)
                {
                    foreach (var row in data)
                    {
                        var premiumSub = repository.GetPremiumGiftSubPolicy(row);
                        var product = repository.GetProductName(row.PROMOTION , row.TOTAL_PREMIUM);
                        row.PRODUCT = product.PRODUCT;
                        row.PRODUCT_NAME = product.PRODUCT_NAME;
                        if (premiumSub != null)
                        {
                            foreach (var pSub in premiumSub)
                            {
                                var identity = repository.GetPolicyIdentity(pSub.SUB_POLICY_ID);
                                row.NUMBER_OF_POLICY += identity.CERT_NUMBER == null ? identity.POLICY_NUMBER + " ," : identity.POLICY_NUMBER + "/" + identity.CERT_NUMBER + " ,";
                            }
                            row.NUMBER_OF_POLICY = row.NUMBER_OF_POLICY.Remove(row.NUMBER_OF_POLICY.Length - 2);
                        }
                        else
                        {
                            row.NUMBER_OF_POLICY = null;
                        }
                    }
                }
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PRODUCT_GAIN GetProductNextLevel(BANC_PRODUCT_GAIN request) => GetProductNextLevel(request, null);
        private BANC_PRODUCT_GAIN GetProductNextLevel(BANC_PRODUCT_GAIN request, Repository repository)
        {
            BANC_PRODUCT_GAIN data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetProductName(request.PROMOTION, request.MAX_PREMIUM);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PREMIUM_GIFT_SUBPOLICY AddBancPremiumGiftSubPolicy(BANC_PREMIUM_GIFT_SUBPOLICY request) => AddBancPremiumGiftSubPolicy(request, null);
        private BANC_PREMIUM_GIFT_SUBPOLICY AddBancPremiumGiftSubPolicy(BANC_PREMIUM_GIFT_SUBPOLICY request, Repository repository)
        {
            BANC_PREMIUM_GIFT_SUBPOLICY data = new BANC_PREMIUM_GIFT_SUBPOLICY();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                BANC_PREMIUM_GIFT premiumGift = new BANC_PREMIUM_GIFT();
                premiumGift.PROMOTION = request.PROMOTION;
                premiumGift.UPD_ID = request.UPD_ID;
                premiumGift.POLICY_ID = request.POLICY_ID;
                premiumGift.TOTAL_PREMIUM = request.PREMIUM;

                repository.UpdateBancPremiumGift(premiumGift);
                repository.AddBancPremiumGiftSubPolicy(request);
                data.POLICY_ID = request.POLICY_ID;
                data.SUB_POLICY_ID = request.SUB_POLICY_ID;

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PREMIUM_GIFT_DATA GetExtraPolicy(POLICY_IDENTITY request) => GetExtraPolicy(request, null);
        private BANC_PREMIUM_GIFT_DATA GetExtraPolicy(POLICY_IDENTITY request, Repository repository)
        {
            BANC_PREMIUM_GIFT_DATA data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetExtraPolicy(request);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PREMIUM_GIFT SavePremiumGiftExtra(BANC_PREMIUM_GIFT request) => SavePremiumGiftExtra(request, null);
        private BANC_PREMIUM_GIFT SavePremiumGiftExtra(BANC_PREMIUM_GIFT request, Repository repository)
        {
            BANC_PREMIUM_GIFT data = new BANC_PREMIUM_GIFT();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                repository.AddPremiumGift(request);
                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PREMIUM_GIFT DeleteBancPremiumGift(BANC_PREMIUM_GIFT[] request) => DeleteBancPremiumGift(request, null);
        private BANC_PREMIUM_GIFT DeleteBancPremiumGift(BANC_PREMIUM_GIFT[] request, Repository repository)
        {
            BANC_PREMIUM_GIFT data = new BANC_PREMIUM_GIFT();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                foreach (var req in request)
                {
                    var formatDate = req.CREATED_DT.Split('/');
                    req.CREATED_DT =  $"{formatDate[0].PadLeft(2,'0')}/{formatDate[1].PadLeft(2, '0')}/{formatDate[2]}";
                    repository.DeleteBancPremiumGift(req);
                }

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_CREATE_TABLE EndProcessBancCreateTable(BANC_CREATE_TABLE request) => EndProcessBancCreateTable(request, null);
        private BANC_CREATE_TABLE EndProcessBancCreateTable(BANC_CREATE_TABLE request, Repository repository)
        {
            BANC_CREATE_TABLE data = new BANC_CREATE_TABLE();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
              
                repository.EndProcessBancCreateTable(request);
                

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        #endregion"End EndProcess"

        #region"Search And Edit"
        public GBBL_STRUCT[] GetBranch() => GetBranch(null);
        private GBBL_STRUCT[] GetBranch(Repository repository)
        {
            GBBL_STRUCT[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetBranch();
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PREMIUM_GIFT_DATA[] GetPremiumGiftSearchEdit(BANC_PREMIUM_GIFT_DATA request) => GetPremiumGiftSearchEdit(request, null);
        private BANC_PREMIUM_GIFT_DATA[] GetPremiumGiftSearchEdit(BANC_PREMIUM_GIFT_DATA request, Repository repository)
        {
            BANC_PREMIUM_GIFT_DATA[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetPremiumGiftSearchEdit(request);
                if (data != null)
                {
                    foreach (var row in data)
                    {
                        var premiumSub = repository.GetPremiumGiftSubPolicy(row);
                        var product = repository.GetProductName(row.PROMOTION, row.TOTAL_PREMIUM);
                        row.PRODUCT = product.PRODUCT;
                        row.PRODUCT_NAME = product.PRODUCT_NAME;
                        if (premiumSub != null)
                        {
                            foreach (var pSub in premiumSub)
                            {
                                var identity = repository.GetPolicyIdentity(pSub.SUB_POLICY_ID);
                                row.NUMBER_OF_POLICY += identity.CERT_NUMBER == null ? identity.POLICY_NUMBER + " ," : identity.POLICY_NUMBER + "/" + identity.CERT_NUMBER + " ,";
                            }
                            row.NUMBER_OF_POLICY = row.NUMBER_OF_POLICY.Remove(row.NUMBER_OF_POLICY.Length - 2);
                        }
                        else
                        {
                            row.NUMBER_OF_POLICY = null;
                        }
                    }
                }
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public BANC_PREMIUM_GIFT EditBancPremiumGiftDeliveryDt(BANC_PREMIUM_GIFT request) => EditBancPremiumGiftDeliveryDt(request, null);
        private BANC_PREMIUM_GIFT EditBancPremiumGiftDeliveryDt(BANC_PREMIUM_GIFT request, Repository repository)
        {
            BANC_PREMIUM_GIFT data = new BANC_PREMIUM_GIFT();
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }

            try
            {
                var formatDate = request.CREATED_DT.Split(' ')[0].Split('/');
                request.CREATED_DT = $"{formatDate[0].PadLeft(2, '0')}/{formatDate[1].PadLeft(2, '0')}/{formatDate[2]}";
                repository.EditBancPremiumGiftDeliveryDt(request);
                data.POLICY_ID = request.POLICY_ID;

                if (internalConnection)
                {
                    repository.commitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (internalConnection)
                {
                    repository.rollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public ZTB_OFFICE[] GetZTB_OFFICE() => GetZTB_OFFICE( null);
        private ZTB_OFFICE[] GetZTB_OFFICE( Repository repository)
        {
            ZTB_OFFICE[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(connectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetZTB_OFFICE();
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }
        #endregion"End Search And Edit"

        #region"Helper"
        public string SeqFormat(string seq)
        {
            DateTime now = DateTime.Now;
            string thaiYear = new ThaiBuddhistCalendar().GetYear(now).ToString();
            return thaiYear + now.Month.ToString().PadLeft(2, '0') + seq.PadLeft(4, '0');
        }
        #endregion"Helper"
    }
}
