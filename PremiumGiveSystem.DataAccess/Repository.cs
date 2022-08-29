using DataAccessUtility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using ITUtility;
using PremiumGiveSystem.DataContact;

namespace PremiumGiveSystem.DataAccess
{
    public class Repository : RepositoryBaseManagedCore
    {
        public Repository(string ConnectionName) : base(ConnectionName)
        {

        }
        public Repository(OracleConnection connection) : base(connection)
        {

        }
        #region "Promotion"
        public BANC_PROMOTION[] GetPromotion(BANC_PROMOTION promotion)
        {
            BANC_PROMOTION[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select PROMOTION, PROM_ST_DT , PROM_END_DT ,DESCRIPTION , UPD_ID from BANC_PROMOTION WHERE 1 = 1 ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(promotion.PROMOTION))
            {
                sql.Append(@" AND PROMOTION = :promotion");
                oCmd.Parameters.Add(new OracleParameter("promotion", promotion.PROMOTION));
            }
            if (!string.IsNullOrEmpty(promotion.DESCRIPTION))
            {
                sql.Append(@" AND upper(DESCRIPTION) LIKE '%' || :description || '%'");
                oCmd.Parameters.Add(new OracleParameter("description", promotion.DESCRIPTION.ToUpper()));
            }
            sql.Append(@" ORDER BY UPD_DT DESC");
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PROMOTION>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public string GetPromotionSeq()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select POLICY.BANC_PROMOTION_SEQ.nextval from dual");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql.ToString();
            string returnValue = oCmd.ExecuteScalar().ToString();
            oCmd.Dispose();
            return returnValue;
        }

        public void AddPromotion(BANC_PROMOTION promotion)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO BANC_PROMOTION (PROMOTION , 
                                                    PROM_ST_DT ,
                                                    PROM_END_DT , 
                                                    DESCRIPTION, 
                                                    FSYSTEM_DT , 
                                                    UPD_DT , 
                                                    UPD_ID,
                                                    TMN)
                        VALUES (:PROMOTIONs ,  TO_DATE(:PROM_ST_DTs,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') , TO_DATE(:PROM_END_DTs,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') 
                                , :DESCRIPTIONs , SYSDATE ,SYSDATE , :UPD_IDs , 'N')");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTIONs", promotion.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("PROM_ST_DTs", promotion.PROM_ST_DT));
            oCmd.Parameters.Add(new OracleParameter("PROM_END_DTs", promotion.PROM_END_DT));
            oCmd.Parameters.Add(new OracleParameter("DESCRIPTIONs", promotion.DESCRIPTION));
            oCmd.Parameters.Add(new OracleParameter("UPD_IDs", promotion.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void EditPromotion(BANC_PROMOTION promotion)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE BANC_PROMOTION SET  PROM_ST_DT = TO_DATE(:PROM_ST_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') , 
                                                    PROM_END_DT = TO_DATE(:PROM_END_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') ,
                                                    DESCRIPTION = :DESCRIPTION,
                                                    UPD_DT = SYSDATE,
                                                    UPD_ID = :UPD_ID,
                                                    TMN = 'N'
                        WHERE PROMOTION = :PROMOTION");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", promotion.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("PROM_ST_DT", promotion.PROM_ST_DT));
            oCmd.Parameters.Add(new OracleParameter("PROM_END_DT", promotion.PROM_END_DT));
            oCmd.Parameters.Add(new OracleParameter("DESCRIPTION", promotion.DESCRIPTION));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", promotion.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }
        #endregion "Promotion"


        #region "Product"
        public BANC_PRODUCT[] GetProduct(BANC_PRODUCT promotion)
        {
            BANC_PRODUCT[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select PRODUCT, MODEL, PRICE, DESCRIPTION, COMPANY, UPD_DT, UPD_ID, PRODUCT_EXTRA from POLICY.BANC_PRODUCT WHERE 1 = 1 ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(promotion.PRODUCT))
            {
                sql.Append(@" AND PRODUCT = :product");
                oCmd.Parameters.Add(new OracleParameter("product", promotion.PRODUCT));
            }
            if (!string.IsNullOrEmpty(promotion.DESCRIPTION))
            {
                sql.Append(@" AND upper(DESCRIPTION) LIKE '%' || :description || '%'");
                oCmd.Parameters.Add(new OracleParameter("description", promotion.DESCRIPTION.ToUpper()));
            }
            if (!string.IsNullOrEmpty(promotion.COMPANY))
            {
                sql.Append(@" AND upper(COMPANY) LIKE '%' || :company || '%'");
                oCmd.Parameters.Add(new OracleParameter("company", promotion.COMPANY.ToUpper()));
            }
            sql.Append(@" ORDER BY UPD_DT DESC");

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PRODUCT>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public string GetProductSeq()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select POLICY.BANC_PRODUCT_SEQ.nextval from dual");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql.ToString();
            string returnValue = oCmd.ExecuteScalar().ToString();
            oCmd.Dispose();
            return returnValue;
        }

        public void AddProduct(BANC_PRODUCT product)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO BANC_PRODUCT (PRODUCT , 
                                                    MODEL ,
                                                    PRICE , 
                                                    DESCRIPTION, 
                                                    COMPANY , 
                                                    UPD_DT , 
                                                    UPD_ID,
                                                    PRODUCT_EXTRA,
                                                    TMN)
                        VALUES (:PRODUCT , :MODEL ,:PRICE ,:DESCRIPTION, :COMPANY ,  SYSDATE ,:UPD_ID ,:PRODUCT_EXTRA ,'N' )");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PRODUCT", product.PRODUCT));
            oCmd.Parameters.Add(new OracleParameter("MODEL", product.MODEL));
            oCmd.Parameters.Add(new OracleParameter("PRICE", product.PRICE));
            oCmd.Parameters.Add(new OracleParameter("DESCRIPTION", product.DESCRIPTION));
            oCmd.Parameters.Add(new OracleParameter("COMPANY", product.COMPANY));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", product.UPD_ID));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT_EXTRA", product.PRODUCT_EXTRA));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void EditProduct(BANC_PRODUCT product)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE BANC_PRODUCT SET  MODEL = :MODEL,
                                                  PRICE = :PRICE,
                                                  DESCRIPTION = :DESCRIPTION,
                                                  COMPANY = :COMPANY,
                                                  UPD_DT = SYSDATE,
                                                  UPD_ID = :UPD_ID,
                                                  PRODUCT_EXTRA = :PRODUCT_EXTRA,
                                                  TMN = 'N'
                        WHERE PRODUCT = :PRODUCT ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PRODUCT", product.PRODUCT));
            oCmd.Parameters.Add(new OracleParameter("MODEL", product.MODEL));
            oCmd.Parameters.Add(new OracleParameter("PRICE", product.PRICE));
            oCmd.Parameters.Add(new OracleParameter("DESCRIPTION", product.DESCRIPTION));
            oCmd.Parameters.Add(new OracleParameter("COMPANY", product.COMPANY));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", product.UPD_ID));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT_EXTRA", product.PRODUCT_EXTRA));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        #endregion "Product"

        #region"ProductGain"
        public BANC_PRODUCT_GAIN[] GetProductGain(BANC_PRODUCT_GAIN productGain)
        {
            BANC_PRODUCT_GAIN[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select bpg.PROMOTION ,bpm.DESCRIPTION AS PROMOTION_NAME , bpg.PRODUCT, bpd.DESCRIPTION AS PRODUCT_NAME, MIN_PREMIUM, MAX_PREMIUM, bpg.UPD_DT, bpg.UPD_ID, bpg.TMN  
                                from BANC_PRODUCT_GAIN  bpg
                                LEFT JOIN BANC_PROMOTION  bpm ON bpm.PROMOTION = bpg.PROMOTION
                                LEFT JOIN BANC_PRODUCT  bpd ON bpd.PRODUCT = bpg.PRODUCT 
                                WHERE 1 = 1 AND bpg.TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(productGain.PROMOTION))
            {
                sql.Append(@" AND bpg.PROMOTION =  :promotion ");
                oCmd.Parameters.Add(new OracleParameter("promotion", productGain.PROMOTION));
            }
            if (!string.IsNullOrEmpty(productGain.PRODUCT))
            {
                sql.Append(@" AND bpg.PRODUCT = :product");
                oCmd.Parameters.Add(new OracleParameter("product", productGain.PRODUCT));
            }
            sql.Append(@" ORDER BY bpg.UPD_DT DESC");
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PRODUCT_GAIN>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public string GetProductGainSeq()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select POLICY.BANC_PRODUCT_GAIN_SEQ.nextval from dual");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql.ToString();
            string returnValue = oCmd.ExecuteScalar().ToString();
            oCmd.Dispose();
            return returnValue;
        }

        public void AddProductGain(BANC_PRODUCT_GAIN product)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO BANC_PRODUCT_GAIN (PROMOTION,
                                                    PRODUCT , 
                                                    MIN_PREMIUM ,
                                                    MAX_PREMIUM , 
                                                    UPD_DT , 
                                                    UPD_ID,
                                                    TMN)
                        VALUES (:PROMOTION ,:PRODUCT , :MIN_PREMIUM ,:MAX_PREMIUM ,  SYSDATE ,:UPD_ID , 'N')");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", product.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT", product.PRODUCT));
            oCmd.Parameters.Add(new OracleParameter("MIN_PREMIUM", product.MIN_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("MAX_PREMIUM", product.MAX_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", product.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void EditProductGain(BANC_PRODUCT_GAIN product)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE BANC_PRODUCT_GAIN SET MIN_PREMIUM = :MIN_PREMIUM,
                                                      MAX_PREMIUM = :MAX_PREMIUM,
                                                      UPD_DT = SYSDATE,
                                                      UPD_ID = :UPD_ID
                        WHERE PRODUCT = :PRODUCT AND PROMOTION = :PROMOTION AND TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", product.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT", product.PRODUCT));
            oCmd.Parameters.Add(new OracleParameter("MIN_PREMIUM", product.MIN_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("MAX_PREMIUM", product.MAX_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", product.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void DeleteProductGain(BANC_PRODUCT_GAIN product)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE BANC_PRODUCT_GAIN SET  TMN = 'Y',
                                                       UPD_DT = SYSDATE,
                                                       UPD_ID = :UPD_ID
                        WHERE PRODUCT = :PRODUCT AND PROMOTION = :PROMOTION AND TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", product.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT", product.PRODUCT));
            oCmd.Parameters.Add(new OracleParameter("MIN_PREMIUM", product.MIN_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("MAX_PREMIUM", product.MAX_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", product.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        #endregion"EndProductGain"

        #region "Create Table"
        public BANC_CREATE_TABLE[] GetCreateTable(BANC_CREATE_TABLE createTable)
        {
            BANC_CREATE_TABLE[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT bct.PROMOTION , bp.DESCRIPTION AS PROMOTION_NAME, bct.CREATE_DT AS CREATE_DT, bct.ST_INSTALL_DT AS ST_INSTALL_DT , bct.END_INSTALL_DT  AS END_INSTALL_DT
                                , bct.UPD_DT AS UPD_DT , bct.UPD_ID , bct.END_FLG , bct.TMN 
                            FROM POLICY.BANC_CREATE_TABLE  bct
                            LEFT JOIN POLICY.banc_promotion bp on bct.PROMOTION = bp.PROMOTION
                            WHERE 1 = 1 AND bct.TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(createTable.PROMOTION))
            {
                sql.Append(@" AND bct.PROMOTION =  :promotion ");
                oCmd.Parameters.Add(new OracleParameter("promotion", createTable.PROMOTION));
            }
            if (!string.IsNullOrEmpty(createTable.CREATE_DT))
            {
                sql.Append(@" AND bct.CREATE_DT = TO_DATE(:createDate,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''')");
                oCmd.Parameters.Add(new OracleParameter("createDate", createTable.CREATE_DT));
            }
            sql.Append(@" ORDER BY bct.CREATE_DT DESC");
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_CREATE_TABLE>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public void AddCreateTable(BANC_CREATE_TABLE createTable)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO BANC_CREATE_TABLE (PROMOTION,
                                                    CREATE_DT , 
                                                    ST_INSTALL_DT ,
                                                    END_INSTALL_DT , 
                                                    UPD_DT , 
                                                    UPD_ID,
                                                    TMN,
                                                    END_FLG)
                        VALUES (:PROMOTION ,TO_DATE(:CREATE_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''),  TO_DATE(:ST_INSTALL_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') ,
                                 TO_DATE(:END_INSTALL_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''),  SYSDATE ,:UPD_ID , 'N' , 'N')");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", createTable.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("CREATE_DT", createTable.CREATE_DT));
            oCmd.Parameters.Add(new OracleParameter("ST_INSTALL_DT", createTable.ST_INSTALL_DT));
            oCmd.Parameters.Add(new OracleParameter("END_INSTALL_DT", createTable.END_INSTALL_DT));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", createTable.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void EditCreateTable(BANC_CREATE_TABLE createTable)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE BANC_CREATE_TABLE SET ST_INSTALL_DT =  TO_DATE(:ST_INSTALL_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''),
                                                      END_INSTALL_DT = TO_DATE(:END_INSTALL_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''),
                                                      UPD_DT = SYSDATE,
                                                      UPD_ID = :UPD_ID
                        WHERE CREATE_DT = TO_DATE(:CREATE_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') AND PROMOTION = :PROMOTION AND TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", createTable.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("CREATE_DT", createTable.CREATE_DT));
            oCmd.Parameters.Add(new OracleParameter("ST_INSTALL_DT", createTable.ST_INSTALL_DT));
            oCmd.Parameters.Add(new OracleParameter("END_INSTALL_DT", createTable.END_INSTALL_DT));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", createTable.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void DeleteCreateTable(BANC_CREATE_TABLE createTable)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE BANC_CREATE_TABLE SET  TMN = 'Y',
                                                       UPD_DT = SYSDATE,
                                                       UPD_ID = :UPD_ID
                        WHERE CREATE_DT = TO_DATE(:CREATE_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') AND PROMOTION = :PROMOTION AND TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", createTable.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("CREATE_DT", createTable.CREATE_DT));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", createTable.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }
        #endregion "End Create Table"

        #region"Premium Gift"
        public BANC_GROUP[] GetBancGroup()
        {
            BANC_GROUP[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT BANC_TYPE ,DESCRIPTION ,DESCRIPTION_ENG, POLICY_TYPE, TMN, FSYSTEM_DT , TMN_DT FROM POLICY.ZTB_BANC_GROUP WHERE TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;


            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_GROUP>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public INSURANCE[] GetInsurance(BANC_GROUP bancGroup)
        {
            INSURANCE[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT ZP.TITLE,A.PL_BLOCK,A.PL_CODE,A.PL_CODE2  
                    FROM  POLICY.ZTB_PLAN_BANC_GROUP A, POLICY.ZTB_PLAN ZP 
                    WHERE ZP.PL_BLOCK=A.PL_BLOCK AND ZP.PL_CODE=A.PL_CODE
                        AND ZP.PL_CODE2=A.PL_CODE2 AND A.TMN='N' AND ZP.TMN_DT > SYSDATE ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(bancGroup.BANC_TYPE))
            {
                sql.Append(@" AND A.BANC_TYPE = :bancType ");
                oCmd.Parameters.Add(new OracleParameter("bancType", bancGroup.BANC_TYPE));
            }
            sql.Append(@" ORDER BY TITLE   ");

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<INSURANCE>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public ZTB_CONSTANT2[] GetPolicyStatus()
        {
            ZTB_CONSTANT2[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT DESCRIPTION,CODE2 FROM ZTB_CONSTANT2 WHERE COL_NAME='STATUS' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<ZTB_CONSTANT2>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }


        public PREMIUM_GIFT_RESPOND[] GetPremiumGift(PREMIUM_GIFT_REQUEST premiumGift)
        {
            PREMIUM_GIFT_RESPOND[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (premiumGift.RECAPTURE)
            {
                sql.Append(@"select DECODE(HOL.POLICY_ID,NULL,PID.POLICY,(HOL.POLICY_HOLDING || '/' || PID.POLICY)) AS POLOICY_NO,PNI.NAME,PNI.SURNAME,INS.INSTALL_DT,BBL.BBL_NAME,PW.T_PREMIUM, PID.POLICY_ID,BBL.BBL_CODE ,uappid.app_no
                        from policy.p_policy_id pid , policy.p_policyholding hol,policy.p_pol_name ppn,policy.p_name_id pni,policy.p_install ins,policy.p_agent ag,policy.pw_life_summ pw ,
                             policy.p_life_id lf, policy.p_appl_id apl , policy.u_application_id uappid,policy.u_application uapp, GBBL_STRUCT BBL ,U_APP_REMARK uar 
                        where pid.policy_id=hol.policy_id(+) and pid.policy_id=ppn.policy_id and ppn.name_id=pni.name_id and pni.tmn='N' 
                              and pid.policy_id=ins.policy_id and pid.policy_id=ag.policy_id and pid.policy_id=pw.policy_id
                              and lf.policy_id= pid.policy_id and pid.policy_id=apl.policy_id and apl.app_no=uappid.app_no and uappid.tmn='N' and uappid.app_id=uapp.app_id AND AG.SALE_AGENT = BBL.BBL_AGENTCODE   
                              and uar.app_id = uapp.app_id
                              and uar.problem_Clearing in ('R','B') 
                              ");
            }
            else
            {
                sql.Append(@"select DECODE(HOL.POLICY_ID,NULL,PID.POLICY,(HOL.POLICY_HOLDING || '/' || PID.POLICY)) AS POLOICY_NO,PNI.NAME,PNI.SURNAME,INS.INSTALL_DT,BBL.BBL_NAME,PW.T_PREMIUM, PID.POLICY_ID,BBL.BBL_CODE ,uappid.app_no
                        from policy.p_policy_id pid , policy.p_policyholding hol,policy.p_pol_name ppn,policy.p_name_id pni,policy.p_install ins,policy.p_agent ag,policy.pw_life_summ pw ,
                             policy.p_life_id lf, policy.p_appl_id apl , policy.u_application_id uappid,policy.u_application uapp, GBBL_STRUCT BBL 
                        where pid.policy_id=hol.policy_id(+) and pid.policy_id=ppn.policy_id and ppn.name_id=pni.name_id and pni.tmn='N' 
                              and pid.policy_id=ins.policy_id and pid.policy_id=ag.policy_id and pid.policy_id=pw.policy_id
                              and lf.policy_id= pid.policy_id and pid.policy_id=apl.policy_id and apl.app_no=uappid.app_no and uappid.tmn='N' and uappid.app_id=uapp.app_id AND AG.SALE_AGENT = BBL.BBL_AGENTCODE 
                               ");
            }
            
            if (!string.IsNullOrEmpty(premiumGift.START_INSTALL_DT) && !string.IsNullOrEmpty(premiumGift.END_INSTALL_DT))
            {
                sql.Append(@" and ins.install_dt between TO_DATE(:START_INSTALL_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') and TO_DATE(:END_INSTALL_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') ");
                oCmd.Parameters.Add(new OracleParameter("START_INSTALL_DT", premiumGift.START_INSTALL_DT));
                oCmd.Parameters.Add(new OracleParameter("END_INSTALL_DT", premiumGift.END_INSTALL_DT));
            }

            if (!string.IsNullOrEmpty(premiumGift.STATUS))
            {
                sql.Append(@"  and lf.status = :STATUS ");
                oCmd.Parameters.Add(new OracleParameter("STATUS", premiumGift.STATUS));
            }

            if (!string.IsNullOrEmpty(premiumGift.START_APP_DT) && !string.IsNullOrEmpty(premiumGift.END_APP_DT))
            {
                sql.Append(@" and uapp.app_dt between TO_DATE(:START_APP_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') and TO_DATE(:END_APP_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') ");
                oCmd.Parameters.Add(new OracleParameter("START_APP_DT", premiumGift.START_APP_DT));
                oCmd.Parameters.Add(new OracleParameter("END_APP_DT", premiumGift.END_APP_DT));
            }

            if (!string.IsNullOrEmpty(premiumGift.MIN_PREMIUM) && !string.IsNullOrEmpty(premiumGift.MAX_PREMIUM))
            {
                sql.Append(@" and pw.t_premium between :MIN_PREMIUM and :MAX_PREMIUM ");
                oCmd.Parameters.Add(new OracleParameter("MIN_PREMIUM", premiumGift.MIN_PREMIUM));
                oCmd.Parameters.Add(new OracleParameter("MAX_PREMIUM", premiumGift.MAX_PREMIUM));
            }
            string insurance = string.Empty;
            if (premiumGift.INSURANCE != null)
            {
                foreach (var insur in premiumGift.INSURANCE)
                {
                    insurance += "'" + insur.PL_BLOCK + insur.PL_CODE + insur.PL_CODE2 + "',";
                }
                insurance = insurance.Remove(insurance.Length - 1);
                sql.Append(@" and lf.pl_block||lf.pl_code||lf.pl_code2 in (" + insurance + ") ");
            }

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<PREMIUM_GIFT_RESPOND>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public BANC_PRODUCT_GAIN GetProductName(string promotion , string premium)
        {
            BANC_PRODUCT_GAIN returnValue = new BANC_PRODUCT_GAIN();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select bpg.PROMOTION ,bpm.DESCRIPTION AS PROMOTION_NAME , bpg.PRODUCT, bpd.DESCRIPTION AS PRODUCT_NAME, MIN_PREMIUM, MAX_PREMIUM, bpg.UPD_DT, bpg.UPD_ID, bpg.TMN  
                                from BANC_PRODUCT_GAIN  bpg
                                LEFT JOIN BANC_PROMOTION  bpm ON bpm.PROMOTION = bpg.PROMOTION
                                LEFT JOIN BANC_PRODUCT  bpd ON bpd.PRODUCT = bpg.PRODUCT 
                                WHERE 1 = 1 AND bpg.TMN = 'N'   ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(promotion))
            {
                sql.Append(@" AND bpg.PROMOTION =  :promotion ");
                oCmd.Parameters.Add(new OracleParameter("promotion", promotion));
            }
            if (!string.IsNullOrEmpty(premium))
            {
                sql.Append(@" and :premium between MIN_PREMIUM and MAX_PREMIUM ");
                oCmd.Parameters.Add(new OracleParameter("premium", premium));
            }

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PRODUCT_GAIN>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public void AddPremiumGift(BANC_PREMIUM_GIFT premiumGift)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO POLICY.BANC_PREMIUM_GIFT (PROMOTION , 
                                      CREATED_DT,
                                      BBL_CODE,
                                      PRODUCT,
                                      POLICY_ID,
                                      APP_NO,
                                      PREMIUM,
                                      TOTAL_PREMIUM,
                                      PACKAGE_DT,
                                      DELIVERY_DT,
                                      REMARK,
                                      UPD_DT,
                                      UPD_ID,
                                      PRODUCT_EXTRA,
                                      FLG_RECEIVE,
                                      TMN,
                                      TMN_DT,
                                      BANC_TYPE)
                        VALUES ( :PROMOTION , SYSDATE, :BBL_CODE , :PRODUCT , :POLICY_ID , :APP_NO , :PREMIUM , :TOTAL_PREMIUM , TO_DATE(:PACKAGE_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''), 
                                 TO_DATE(:DELIVERY_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''), :REMARK , SYSDATE , :UPD_ID , :PRODUCT_EXTRA , null , 'N' , null ,:BANC_TYPE)");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", premiumGift.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("BBL_CODE", premiumGift.BBL_CODE));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT", premiumGift.PRODUCT));
            oCmd.Parameters.Add(new OracleParameter("POLICY_ID", Convert.ToInt64(premiumGift.POLICY_ID)));
            oCmd.Parameters.Add(new OracleParameter("APP_NO", premiumGift.APP_NO));
            oCmd.Parameters.Add(new OracleParameter("PREMIUM", Convert.ToInt64(premiumGift.PREMIUM)));
            oCmd.Parameters.Add(new OracleParameter("TOTAL_PREMIUM", Convert.ToInt64(premiumGift.TOTAL_PREMIUM)));
            oCmd.Parameters.Add(new OracleParameter("PACKAGE_DT", premiumGift.PACKAGE_DT));
            oCmd.Parameters.Add(new OracleParameter("DELIVERY_DT", premiumGift.DELIVERY_DT));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", premiumGift.UPD_ID));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT_EXTRA", premiumGift.PRODUCT_EXTRA));
            oCmd.Parameters.Add(new OracleParameter("BANC_TYPE", premiumGift.BANC_TYPE));
            oCmd.Parameters.Add(new OracleParameter("REMARK", premiumGift.REMARK));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        #endregion"End Premium Gift"

        #region"EndProcess"
        public BANC_PREMIUM_GIFT_DATA[] GetPremiumGiftEndProcess(BANC_PREMIUM_GIFT_DATA request)
        {
            BANC_PREMIUM_GIFT_DATA[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select bpg.PROMOTION , bpg.CREATED_DT , BBL.BBL_NAME , bpg.BBL_CODE ,bpg.PRODUCT , bpd.DESCRIPTION AS PRODUCT_NAME ,bpg.POLICY_ID ,bpg.APP_NO ,bpg.PREMIUM , bpg.TOTAL_PREMIUM ,bpg.PACKAGE_DT 
                                , bpg.DELIVERY_DT , bpg.REMARK , bpg.UPD_DT , bpg.UPD_ID  , bpg.PRODUCT_EXTRA , bpd2.DESCRIPTION AS PRODUCT_EXTRA_NAME , bpg.FLG_RECEIVE , bpg.TMN ,bpg.BANC_TYPE
                                , bpg.TMN_DT ,pi.policy_number , pi.cert_number , ins.INSTALL_DT , nm.name , nm.surname

                            from POLICY.BANC_PREMIUM_GIFT bpg 
                            INNER JOIN AGENT.GBBL_STRUCT BBL ON BBL.BBL_CODE = bpg.BBL_CODE
                            LEFT JOIN POLICY.BANC_PRODUCT bpd ON bpd.PRODUCT = bpg.PRODUCT
                            LEFT JOIN POLICY.BANC_PRODUCT bpd2 ON bpd2.PRODUCT = bpg.PRODUCT_EXTRA
                            INNER JOIN POLICY.P_POLICY_IDENTITY pi ON pi.policy_id = bpg.policy_id
                            INNER JOIN POLICY.p_install ins ON ins.policy_id = bpg.policy_id 
                            INNER JOIN POLICY.P_POL_NAME PNM ON pnm.policy_id = bpg.policy_id
                            INNER JOIN POLICY.P_NAME_ID NM ON nm.name_id = pnm.name_id
                            where 1 = 1 AND bpg.TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(request.PROMOTION))
            {
                sql.Append(@" AND bpg.PROMOTION = :PROMOTION ");
                oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            }
            if (!string.IsNullOrEmpty(request.POLICY_NUMBER))
            {
                sql.Append(@" AND pi.policy_number = :POLICY_NUMBER ");
                oCmd.Parameters.Add(new OracleParameter("POLICY_NUMBER", request.POLICY_NUMBER));
            }
            if (!string.IsNullOrEmpty(request.CERT_NUMBER))
            {
                sql.Append(@" AND pi.cert_number = :CERT_NUMBER ");
                oCmd.Parameters.Add(new OracleParameter("CERT_NUMBER", request.CERT_NUMBER));
            }
            sql.Append(@" ORDER BY bpg.PROMOTION  ");

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PREMIUM_GIFT_DATA>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public BANC_PREMIUM_GIFT_SUBPOLICY[] GetPremiumGiftSubPolicy(BANC_PREMIUM_GIFT_DATA request)
        {
            BANC_PREMIUM_GIFT_SUBPOLICY[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PROMOTION , CREATED_DT , POLICY_ID , SUB_POLICY_ID , BBL_CODE , PREMIUM , REMARK , UPD_DT, UPD_ID , PRODUCT_EXTRA , TMN , TMN_DT
                            FROM  POLICY.BANC_PREMIUM_GIFT_SUBPOLICY 
                            WHERE 1 = 1   AND TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(request.PROMOTION))
            {
                sql.Append(@" AND PROMOTION = :PROMOTION ");
                oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            }
            //if (!string.IsNullOrEmpty(request.CREATED_DT))
            //{
            //    sql.Append(@"  AND  CREATED_DT =  TO_DATE(:CREATED_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') ");
            //    oCmd.Parameters.Add(new OracleParameter("CREATED_DT", request.CREATED_DT.Split(' ')[0] ));
            //}
            if (!string.IsNullOrEmpty(request.POLICY_ID))
            {
                sql.Append(@" AND POLICY_ID = :POLICY_ID ");
                oCmd.Parameters.Add(new OracleParameter("POLICY_ID", request.POLICY_ID));
            }
            sql.Append(@" ORDER BY PROMOTION  ");

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PREMIUM_GIFT_SUBPOLICY>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public POLICY_IDENTITY GetPolicyIdentity(string POLICY_ID)
        {
            POLICY_IDENTITY returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select POLICY_ID , CHANNEL_TYPE , POLICY_NUMBER , CERT_NUMBER from POLICY.P_POLICY_IDENTITY WHERE POLICY_ID = :POLICY_ID ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("POLICY_ID", POLICY_ID));

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<POLICY_IDENTITY>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }


        public void AddBancPremiumGiftSubPolicy(BANC_PREMIUM_GIFT_SUBPOLICY request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"INSERT INTO POLICY.BANC_PREMIUM_GIFT_SUBPOLICY (PROMOTION ,
                                                                         CREATED_DT ,
                                                                         POLICY_ID,
                                                                         SUB_POLICY_ID , 
                                                                         BBL_CODE ,
                                                                         PREMIUM,
                                                                         REMARK,
                                                                         UPD_DT ,
                                                                         UPD_ID,
                                                                         PRODUCT_EXTRA,
                                                                         TMN,
                                                                         TMN_DT)
                        VALUES (:PROMOTION , TO_DATE(:CREATED_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha'''), :POLICY_ID, :SUB_POLICY_ID , :BBL_CODE, :PREMIUM , :REMARK , SYSDATE ,:UPD_ID , :PRODUCT_EXTRA  , 'N' , null)");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("CREATED_DT", request.CREATED_DT));
            oCmd.Parameters.Add(new OracleParameter("POLICY_ID", request.POLICY_ID));
            oCmd.Parameters.Add(new OracleParameter("SUB_POLICY_ID", request.SUB_POLICY_ID));
            oCmd.Parameters.Add(new OracleParameter("BBL_CODE", request.BBL_CODE));
            oCmd.Parameters.Add(new OracleParameter("PREMIUM", request.PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("REMARK", request.REMARK));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", request.UPD_ID));
            oCmd.Parameters.Add(new OracleParameter("PRODUCT_EXTRA", request.PRODUCT_EXTRA));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void UpdateBancPremiumGift(BANC_PREMIUM_GIFT request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE POLICY.BANC_PREMIUM_GIFT  SET TOTAL_PREMIUM = :TOTAL_PREMIUM,
                                     UPD_DT = SYSDATE,
                                     UPD_ID = :UPD_ID
                        WHERE POLICY_ID = :POLICY_ID AND PROMOTION = :PROMOTION ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("POLICY_ID", request.POLICY_ID));
            oCmd.Parameters.Add(new OracleParameter("TOTAL_PREMIUM", request.TOTAL_PREMIUM));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", request.UPD_ID));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public BANC_PREMIUM_GIFT_DATA GetExtraPolicy(POLICY_IDENTITY request)
        {
            BANC_PREMIUM_GIFT_DATA returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select DECODE(pid.POLICY_ID,NULL,pid.CERT_NUMBER,(pid.POLICY_NUMBER || '/' || PID.CERT_NUMBER)) AS POLICY_NUMBER , pid.POLICY_ID 
                                , ni.NAME , ni.surname , BBL.BBL_CODE ,BBL.BBL_NAME , PW.T_PREMIUM AS PREMIUM  ,uappid.app_no 
                            from POLICY.P_POLICY_IDENTITY pid
                            INNER JOIN POLICY.P_POL_NAME pn ON   pn.policy_id = pid.policy_id
                            INNER JOIN POLICY.P_NAME_ID ni ON pn.name_id = ni.name_id
                            INNER JOIN policy.p_agent ag ON pid.policy_id = ag.policy_id
                            INNER JOIN GBBL_STRUCT BBL ON AG.SALE_AGENT = BBL.BBL_AGENTCODE
                            INNER JOIN policy.pw_life_summ pw ON pid.policy_id=pw.policy_id
                            INNER JOIN policy.p_appl_id apl ON pid.policy_id = apl.policy_id
                            INNER JOIN policy.u_application_id uappid ON apl.app_no = uappid.app_no
                            where 1=1   ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(request.POLICY_NUMBER))
            {
                sql.Append(@" AND pid.POLICY_NUMBER = :POLICY_NUMBER ");
                oCmd.Parameters.Add(new OracleParameter("POLICY_NUMBER", request.POLICY_NUMBER));
            }
            if (!string.IsNullOrEmpty(request.CERT_NUMBER))
            {
                sql.Append(@" AND pid.cert_number = :CERT_NUMBER ");
                oCmd.Parameters.Add(new OracleParameter("CERT_NUMBER", request.CERT_NUMBER));
            }

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PREMIUM_GIFT_DATA>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public void DeleteBancPremiumGift(BANC_PREMIUM_GIFT request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE POLICY.BANC_PREMIUM_GIFT SET TMN = 'Y'
                         WHERE TMN = 'N' AND POLICY_ID = :POLICY_ID AND PROMOTION = :PROMOTION  AND TO_CHAR(CREATED_DT,'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') = :CREATED_DT");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("POLICY_ID", request.POLICY_ID));
            oCmd.Parameters.Add(new OracleParameter("CREATED_DT", request.CREATED_DT));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }

        public void EndProcessBancCreateTable(BANC_CREATE_TABLE request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE POLICY.BANC_CREATE_TABLE SET END_FLG = 'Y'
                         WHERE END_FLG = 'N' AND PROMOTION = :PROMOTION ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }
        #endregion"End EndProcess"

        #region"Search And Edit"
        public GBBL_STRUCT[] GetBranch()
        {
            GBBL_STRUCT[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select description,bbl_branch from GBBL_STRUCT order by DESCRIPTION ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<GBBL_STRUCT>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public BANC_PREMIUM_GIFT_DATA[] GetPremiumGiftSearchEdit(BANC_PREMIUM_GIFT_DATA request)
        {
            BANC_PREMIUM_GIFT_DATA[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select  bpg.PROMOTION, prm.DESCRIPTION AS PROMOTION_NAME , bpg.CREATED_DT , BBL.BBL_NAME , bpg.BBL_CODE ,bpg.PRODUCT , bpd.DESCRIPTION AS PRODUCT_NAME 
                                , bpg.POLICY_ID ,bpg.APP_NO ,bpg.PREMIUM , bpg.TOTAL_PREMIUM ,bpg.PACKAGE_DT 
                                , bpg.DELIVERY_DT , bpg.REMARK , bpg.UPD_DT , bpg.UPD_ID  , bpg.PRODUCT_EXTRA , bpd2.DESCRIPTION AS PRODUCT_EXTRA_NAME , bpg.FLG_RECEIVE , bpg.TMN ,bpg.BANC_TYPE
                                , bpg.TMN_DT ,pi.policy_number , pi.cert_number , ins.INSTALL_DT , nm.name , nm.surname , nm.IDCARD_NO
                                , bbl.adr1 || ' ' || bbl.adr2 || ' ' || bbl.adr3 || ' ' || bbl.POST AS BANK_ADDRESS
                                , bbl.ADDRESS_PROVINCE AS BANK_PROVINCE 
                                , decode(bbl.ADDRESS_PROVINCE,'กรุงเทพฯ','กรุงเทพและปริมณฑล','สมุทรปราการ','กรุงเทพและปริมณฑล','นนทบุรี','กรุงเทพและปริมณฑล' ,'นครปฐม' ,'กรุงเทพและปริมณฑล' ,
                                'ปทุมธานี' ,'กรุงเทพและปริมณฑล' , 'สมุทรสาคร' , 'กรุงเทพและปริมณฑล' ,bbl.ADDRESS_PROVINCE)BANK_PROVINCE_GROUP
                                , pai.address_number || ' ' || pai.address_name || ' หมู่ ' || pai.mooh || ' ' || pai.soi || ' ' || pai.road || ' ' || pai.tambol || ' ' || pai.amphur || ' ' || pai.province || ' ' || pai.zip_code  AS CUS_ADDRESS
                                , zp.TITLE , pli.PL_BLOCK , pli.PL_CODE , pli.PL_CODE2 
                            from POLICY.BANC_PREMIUM_GIFT bpg 
                            INNER JOIN AGENT.GBBL_STRUCT BBL ON BBL.BBL_CODE = bpg.BBL_CODE
                            INNER JOIN POLICY.BANC_PROMOTION prm ON prm.PROMOTION  = bpg.PROMOTION  
                            LEFT JOIN POLICY.BANC_PRODUCT bpd ON bpd.PRODUCT = bpg.PRODUCT
                            LEFT JOIN POLICY.BANC_PRODUCT bpd2 ON bpd2.PRODUCT = bpg.PRODUCT_EXTRA
                            INNER JOIN POLICY.P_POLICY_IDENTITY pi ON pi.policy_id = bpg.policy_id
                            INNER JOIN POLICY.p_install ins ON ins.policy_id = bpg.policy_id 
                            INNER JOIN POLICY.P_POL_NAME PNM ON pnm.policy_id = bpg.policy_id
                            INNER JOIN POLICY.P_NAME_ID NM ON nm.name_id = pnm.name_id
                            INNER JOIN POLICY.P_POL_ADDRESS ppa ON ppa.policy_id = bpg.policy_id
                            INNER JOIN POLICY.p_address_id pai ON pai.address_id = ppa.address_id
                            INNER JOIN POLICY.P_LIFE_ID pli ON pli.policy_id = bpg.policy_id
                            INNER JOIN POLICY.ZTB_PLAN  zp ON zp.PL_BLOCK || zp.PL_CODE || zp.PL_CODE2 = pli.PL_BLOCK || pli.PL_CODE || pli.PL_CODE2 
                            where 1 = 1 AND bpg.TMN = 'N' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(request.BANC_TYPE))
            {
                sql.Append(@" AND bpg.BANC_TYPE = :BANC_TYPE ");
                oCmd.Parameters.Add(new OracleParameter("BANC_TYPE", request.BANC_TYPE));
            }
            if (!string.IsNullOrEmpty(request.PROMOTION))
            {
                sql.Append(@" AND bpg.PROMOTION = :PROMOTION ");
                oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            }
            if (!string.IsNullOrEmpty(request.BBL_CODE))
            {
                sql.Append(@"   AND bpg.BBL_CODE = :BBL_CODE ");
                oCmd.Parameters.Add(new OracleParameter("BBL_CODE", request.BBL_CODE));
            }
            if (!string.IsNullOrEmpty(request.PRODUCT))
            {
                sql.Append(@"   AND bpg.PRODUCT = :PRODUCT ");
                oCmd.Parameters.Add(new OracleParameter("PRODUCT", request.PRODUCT));
            }
            if (!string.IsNullOrEmpty(request.START_INSTALL_DT) && !string.IsNullOrEmpty(request.END_INSTALL_DT))
            {
                sql.Append(@"    AND TO_CHAR(ins.INSTALL_DT,'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') between :START_INSTALL_DT AND  :END_INSTALL_DT ");
                oCmd.Parameters.Add(new OracleParameter("START_INSTALL_DT", request.START_INSTALL_DT));
                oCmd.Parameters.Add(new OracleParameter("END_INSTALL_DT", request.END_INSTALL_DT));
            }
            if (!string.IsNullOrEmpty(request.POLICY_NUMBER))
            {
                sql.Append(@" AND pi.policy_number = :POLICY_NUMBER ");
                oCmd.Parameters.Add(new OracleParameter("POLICY_NUMBER", request.POLICY_NUMBER));
            }
            if (!string.IsNullOrEmpty(request.CERT_NUMBER))
            {
                sql.Append(@" AND pi.cert_number = :CERT_NUMBER ");
                oCmd.Parameters.Add(new OracleParameter("CERT_NUMBER", request.CERT_NUMBER));
            }
            if (!string.IsNullOrEmpty(request.MIN_PREMIUM) && !string.IsNullOrEmpty(request.MAX_PREMIUM))
            {
                sql.Append(@"  AND bpg.TOTAL_PREMIUM  between :MIN_PREMIUM and :MAX_PREMIUM ");
                oCmd.Parameters.Add(new OracleParameter("MIN_PREMIUM", request.MIN_PREMIUM));
                oCmd.Parameters.Add(new OracleParameter("MAX_PREMIUM", request.MAX_PREMIUM));
            }
            sql.Append(@" ORDER BY bpg.PROMOTION  ");

            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<BANC_PREMIUM_GIFT_DATA>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public void EditBancPremiumGiftDeliveryDt(BANC_PREMIUM_GIFT request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE POLICY.BANC_PREMIUM_GIFT  SET DELIVERY_DT = TO_DATE(:DELIVERY_DT,'DD/MM/YYYY','NLS_CALENDAR=''Thai Buddha''') ,
                                     UPD_DT = SYSDATE,
                                     UPD_ID = :UPD_ID
                        WHERE POLICY_ID = :POLICY_ID AND PROMOTION = :PROMOTION AND TO_CHAR(CREATED_DT,'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') = :CREATED_DT");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Add(new OracleParameter("PROMOTION", request.PROMOTION));
            oCmd.Parameters.Add(new OracleParameter("POLICY_ID", request.POLICY_ID));
            oCmd.Parameters.Add(new OracleParameter("CREATED_DT", request.CREATED_DT));
            oCmd.Parameters.Add(new OracleParameter("UPD_ID", request.UPD_ID));
            oCmd.Parameters.Add(new OracleParameter("DELIVERY_DT", request.DELIVERY_DT));
            oCmd.CommandText = sql.ToString();
            oCmd.ExecuteNonQuery();
            oCmd.Dispose();
        }


        public ZTB_OFFICE[] GetZTB_OFFICE()
        {
            ZTB_OFFICE[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select office , description || ' ' || adr1 || ' ' || adr2 || ' ' || adr3 || ' ' ||  post AS DESCRIPTION from ztb_office ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
           
           
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<ZTB_OFFICE>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        #endregion"End Search And Edit"
    }
}
