using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PremiumGiveSystem.DataContact;
using PremiumGiveSystemAPI.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumGiveSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumGiveSystemController : BaseController
    {
        public PremiumGiveSystemController(IConfiguration Configuration) : base(Configuration)
        {

        }

        [HttpPost]
        [Route("GetPromotion")]
        public IActionResult GetPromotion([FromBody] BANC_PROMOTION requset)
        {
            try
            {
                var data = serviceAction.GetPromotion(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("ManagePromotion")]
        public IActionResult ManagePromotion([FromBody] BANC_PROMOTION requset)
        {
            try
            {
                var data = serviceAction.ManagePromotion(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetProduct")]
        public IActionResult GetProduct([FromBody] BANC_PRODUCT requset)
        {
            try
            {
                var data = serviceAction.GetProduct(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("ManageProduct")]
        public IActionResult ManageProduct([FromBody] BANC_PRODUCT requset)
        {
            try
            {
                var data = serviceAction.ManageProduct(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetProductGain")]
        public IActionResult GetProductGain([FromBody] BANC_PRODUCT_GAIN requset)
        {
            try
            {
                var data = serviceAction.GetProductGain(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("ManageProductGain")]
        public IActionResult ManageProductGain([FromBody] BANC_PRODUCT_GAIN requset)
        {
            try
            {
                var data = serviceAction.ManageProductGain(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetCreateTable")]
        public IActionResult GetCreateTable([FromBody] BANC_CREATE_TABLE requset)
        {
            try
            {
                var data = serviceAction.GetCreateTable(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("ManageCreateTable")]
        public IActionResult ManageCreateTable([FromBody] BANC_CREATE_TABLE requset)
        {
            try
            {
                var data = serviceAction.ManageCreateTable(requset);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetBancGroup")]
        public IActionResult GetBancGroup()
        {
            try
            {
                var data = serviceAction.GetBancGroup();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetInsurance")]
        public IActionResult GetInsurance([FromBody] BANC_GROUP bancGroup)
        {
            try
            {
                var data = serviceAction.GetInsurance(bancGroup);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetPolicyStatus")]
        public IActionResult GetPolicyStatus()
        {
            try
            {
                var data = serviceAction.GetPolicyStatus();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("GetPremiumGift")]
        public IActionResult GetPremiumGift([FromBody] PREMIUM_GIFT_REQUEST premiumGift)
        {
            try
            {
                var data = serviceAction.GetPremiumGift(premiumGift);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("SavePremiumGift")]
        public IActionResult SavePremiumGift([FromBody] BANC_PREMIUM_GIFT[] premiumGift)
        {
            try
            {
                var data = serviceAction.SavePremiumGift(premiumGift);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetPremiumGiftEndProcess")]
        public IActionResult GetPremiumGiftEndProcess([FromBody] BANC_PREMIUM_GIFT_DATA request)
        {
            try
            {
                var data = serviceAction.GetPremiumGiftEndProcess(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetProductNextLevel")]
        public IActionResult GetProductNextLevel([FromBody] BANC_PRODUCT_GAIN request)
        {
            try
            {
                var data = serviceAction.GetProductNextLevel(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("AddBancPremiumGiftSubPolicy")]
        public IActionResult AddBancPremiumGiftSubPolicy([FromBody] BANC_PREMIUM_GIFT_SUBPOLICY request)
        {
            try
            {
                var data = serviceAction.AddBancPremiumGiftSubPolicy(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetExtraPolicy")]
        public IActionResult GetExtraPolicy([FromBody] POLICY_IDENTITY request)
        {
            try
            {
                var data = serviceAction.GetExtraPolicy(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("SavePremiumGiftExtra")]
        public IActionResult SavePremiumGiftExtra([FromBody] BANC_PREMIUM_GIFT premiumGift)
        {
            try
            {
                var data = serviceAction.SavePremiumGiftExtra(premiumGift);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("DeleteBancPremiumGift")]
        public IActionResult DeleteBancPremiumGift([FromBody] BANC_PREMIUM_GIFT[] premiumGift)
        {
            try
            {
                var data = serviceAction.DeleteBancPremiumGift(premiumGift);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("EndProcessBancCreateTable")]
        public IActionResult EndProcessBancCreateTable([FromBody] BANC_CREATE_TABLE request)
        {
            try
            {
                var data = serviceAction.EndProcessBancCreateTable(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetBranch")]
        public IActionResult GetBranch()
        {
            try
            {
                var data = serviceAction.GetBranch();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GetPremiumGiftSearchEdit")]
        public IActionResult GetPremiumGiftSearchEdit([FromBody] BANC_PREMIUM_GIFT_DATA request)
        {
            try
            {
                var data = serviceAction.GetPremiumGiftSearchEdit(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("EditBancPremiumGiftDeliveryDt")]
        public IActionResult EditBancPremiumGiftDeliveryDt([FromBody] BANC_PREMIUM_GIFT request)
        {
            try
            {
                var data = serviceAction.EditBancPremiumGiftDeliveryDt(request);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("PrintListOfAwardeesReport")]
        public IActionResult PrintListOfAwardeesReport([FromBody] BANC_PREMIUM_GIFT_DATA[] request)
        {
            try
            {
                BANC_PRODUCT_GAIN reqPrdGain = new BANC_PRODUCT_GAIN();
                BANC_PRODUCT_GAIN[] prdGain = null;
                reqPrdGain.PROMOTION = request[0].PROMOTION;
                prdGain = serviceAction.GetProductGain(reqPrdGain);
                BANC_CREATE_TABLE reqCreateTable = new BANC_CREATE_TABLE();
                BANC_CREATE_TABLE[] createTable = null;
                reqCreateTable.PROMOTION = request[0].PROMOTION;
                createTable = serviceAction.GetCreateTable(reqCreateTable);
                if (prdGain != null)
                {
                    int i = 1;
                    prdGain = prdGain.OrderBy(x => Convert.ToInt32(x.MIN_PREMIUM)).Select(x => { x.ID = i++; return x; }).ToArray();
                }
                
                foreach (var req in request)
                {
                    var group = prdGain.Where(x => Convert.ToInt32(req.TOTAL_PREMIUM) >= Convert.ToInt32(x.MIN_PREMIUM) && Convert.ToInt32(req.TOTAL_PREMIUM) <= Convert.ToInt32(x.MAX_PREMIUM)).FirstOrDefault();
                    if (group != null)
                    {
                        req.GroupId = group.ID;
                        req.PremiumRange = string.Format("{0:#,##0}", Convert.ToDecimal(group.MIN_PREMIUM)) + " - " + string.Format("{0:#,##0}", Convert.ToDecimal(group.MAX_PREMIUM));
                        foreach (var ct in createTable)
                        {
                            req.START_INSTALL_DT = ct.ST_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                            req.END_INSTALL_DT = ct.END_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                        }
                    }
                    else
                    {
                        if (prdGain != null)
                        {
                            var lastCondition = prdGain[prdGain.Length - 1];
                            if (Convert.ToInt32(req.TOTAL_PREMIUM) > Convert.ToInt32(lastCondition.MIN_PREMIUM))
                            {
                                req.GroupId = prdGain.Length;
                                req.PremiumRange = string.Format("{0:#,##0}", Convert.ToDecimal(lastCondition.MIN_PREMIUM)) + " - " + string.Format("{0:#,##0}", Convert.ToDecimal(lastCondition.MAX_PREMIUM));
                                foreach (var ct in createTable)
                                {
                                    req.START_INSTALL_DT = ct.ST_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                                    req.END_INSTALL_DT = ct.END_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                                }
                            }
                        }
                       
                    }
                }
                byte[] filereport = null;
                var dateNow = DateTime.Now;
                using (MemoryStream ms = new MemoryStream())
                {
                    var rpt = new Report.ListofAwardessReport(request);
                    rpt.CreateDocument();
                    rpt.ExportToPdf(ms);
                    filereport = ms.ToArray();
                }
                var fileName = "ListofAwardessReport" + dateNow.ToString() + ".pdf";
                var file = File(filereport, "application/pdf", fileName);

                return file;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("PrintDataForDelivery")]
        public IActionResult PrintDataForDelivery([FromBody] BANC_PREMIUM_GIFT_DATA[] request)
        {
            try
            {
                BANC_CREATE_TABLE reqCreateTable = new BANC_CREATE_TABLE();
                BANC_CREATE_TABLE[] createTable = null;
                reqCreateTable.PROMOTION = request[0].PROMOTION;
                createTable = serviceAction.GetCreateTable(reqCreateTable);
                List<BANC_PREMIUM_GIFT_DATA> dataToReport = new List<BANC_PREMIUM_GIFT_DATA>();
                foreach (var req in request)
                {
                    req.CREATED_DT = req.CREATED_DT.Split(' ')[0].ToDateReportFormat();
                    foreach (var ct in createTable)
                    {
                        req.START_INSTALL_DT = ct.ST_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                        req.END_INSTALL_DT = ct.END_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                    }
                    req.NumberOfProduct = 1;

                    var result = dataToReport.Where(x => x.BBL_CODE == req.BBL_CODE && x.PRODUCT == req.PRODUCT);
                    if (result.Count() == 0)
                    {
                        dataToReport.Add(req);
                    }
                    else
                    {
                        var dupData = dataToReport.Where(x => x.BBL_CODE == req.BBL_CODE && x.PRODUCT == req.PRODUCT).Select(x => x).FirstOrDefault();
                        dupData.NumberOfProduct = dupData.NumberOfProduct + 1;
                    }
                
                }

                byte[] filereport = null;
                var dateNow = DateTime.Now;
                using (MemoryStream ms = new MemoryStream())
                {
                    var rpt = new Report.DataForDelivery(dataToReport.ToArray());
                    rpt.CreateDocument();
                    rpt.ExportToPdf(ms);
                    filereport = ms.ToArray();
                }
                var fileName = "DataForDelivery" + dateNow.ToString() + ".pdf";
                var file = File(filereport, "application/pdf", fileName);

                return file;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("PrintStickerSendBank")]
        public IActionResult PrintStickerSendBank([FromBody] BANC_PREMIUM_GIFT_DATA[] request)
        {
            try
            {
                byte[] filereport = null;
                var dateNow = DateTime.Now;
                using (MemoryStream ms = new MemoryStream())
                {
                    var rpt = new Report.PrintStickerSendBank(request.ToArray());
                    rpt.CreateDocument();
                    rpt.ExportToPdf(ms);
                    filereport = ms.ToArray();
                }
                var fileName = "PrintStickerSendBank" + dateNow.ToString() + ".pdf";
                var file = File(filereport, "application/pdf", fileName);

                return file;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("PrintDetailOfPremium")]
        public IActionResult PrintDetailOfPremium([FromBody] BANC_PREMIUM_GIFT_DATA[] request)
        {
            try
            {
                BANC_PRODUCT_GAIN reqPrdGain = new BANC_PRODUCT_GAIN();
                BANC_PRODUCT_GAIN[] prdGain = null;
                reqPrdGain.PROMOTION = request[0].PROMOTION;
                prdGain = serviceAction.GetProductGain(reqPrdGain);
                BANC_CREATE_TABLE reqCreateTable = new BANC_CREATE_TABLE();
                BANC_CREATE_TABLE[] createTable = null;
                reqCreateTable.PROMOTION = request[0].PROMOTION;
                createTable = serviceAction.GetCreateTable(reqCreateTable);
                if (prdGain != null)
                {
                    int i = 1;
                    prdGain = prdGain.OrderBy(x => Convert.ToInt32(x.MIN_PREMIUM)).Select(x => { x.ID = i++; return x; }).ToArray();
                }
              
                foreach (var req in request)
                {
                    var group = prdGain?.Where(x => Convert.ToInt32(req.TOTAL_PREMIUM) >= Convert.ToInt32(x.MIN_PREMIUM) && Convert.ToInt32(req.TOTAL_PREMIUM) <= Convert.ToInt32(x.MAX_PREMIUM)).FirstOrDefault();
                    if (group != null)
                    {
                        req.GroupId = group.ID;
                        req.PremiumRange = string.Format("{0:#,##0}", Convert.ToDecimal(group.MIN_PREMIUM)) + " - " + string.Format("{0:#,##0}", Convert.ToDecimal(group.MAX_PREMIUM));
                        foreach (var ct in createTable)
                        {
                            req.START_INSTALL_DT = ct.ST_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                            req.END_INSTALL_DT = ct.END_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                        }
                    }
                    else
                    {
                        if (prdGain != null)
                        {
                            var lastCondition = prdGain[prdGain.Length - 1];
                            if (Convert.ToInt32(req.TOTAL_PREMIUM) > Convert.ToInt32(lastCondition.MIN_PREMIUM))
                            {
                                req.GroupId = prdGain.Length;
                                req.PremiumRange = string.Format("{0:#,##0}", Convert.ToDecimal(lastCondition.MIN_PREMIUM)) + " - " + string.Format("{0:#,##0}", Convert.ToDecimal(lastCondition.MAX_PREMIUM)) + " ขึ้นไป";
                                foreach (var ct in createTable)
                                {
                                    req.START_INSTALL_DT = ct.ST_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                                    req.END_INSTALL_DT = ct.END_INSTALL_DT.Split(' ')[0].ToDateReportFormat();
                                }
                            }
                        }
                    }
                    req.TOTAL_PREMIUM = string.Format("{0:#,##0}", Convert.ToDecimal(req.TOTAL_PREMIUM));

                }

                byte[] filereport = null;
                var dateNow = DateTime.Now;
                using (MemoryStream ms = new MemoryStream())
                {
                    var rpt = new Report.PrintDetailOfPremium(request.ToArray());
                    rpt.CreateDocument();
                    rpt.ExportToPdf(ms);
                    filereport = ms.ToArray();
                }
                var fileName = "PrintDetailOfPremium" + dateNow.ToString() + ".pdf";
                var file = File(filereport, "application/pdf", fileName);

                return file;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("GetZTB_OFFICE")]
        public IActionResult GetZTB_OFFICE()
        {
            try
            {
                var data = serviceAction.GetZTB_OFFICE();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("GetDeleviryAddress")]
        public IActionResult GetDeleviryAddress()
        {
            try
            {
                List<DropDown> data = new List<DropDown>();
                data.Add(new DropDown
                {
                    Id = "B",
                    Text = "ที่อยู่ธนาคาร"
                });
                data.Add(new DropDown
                {
                    Id = "C",
                    Text = "ที่อยู่ลูกค้า"
                });
                data.Add(new DropDown { 
                    Id = "BLA",
                    Text = "ที่อยู่สาขา BLA"
                });
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
    }
}
