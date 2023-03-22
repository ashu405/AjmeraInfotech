using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using AjmeraPracticalAssessment.Models;
using AjmeraPracticalAssessment.Classes.DAL;

namespace AjmeraPracticalAssessment.Controllers
{

    public class AjmeraInfotechController : ApiController
    {
        AjmeraInfotechDBEntities Db = new AjmeraInfotechDBEntities();
        DataSet DS;
        DataAccessLayer DAL = new DataAccessLayer();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [HttpGet]
        public IHttpActionResult GetBook([FromUri] tblBookMaster tblBookMasters)
        {
            try
            {
                string BookID = null;

                if (tblBookMasters.BookID > 0)
                {
                    BookID = tblBookMasters.BookID.ToString().Trim();
                }
                DS = DAL.GetBookData("SelectAll", BookID);

                if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    return Ok(DS);
                }
                else
                {
                    return Content(HttpStatusCode.OK, "No Data Found");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult SetBook([FromBody] tblBookMaster tblBookMasters)
        {
            try
            {

                var tblBookMaster = new tblBookMaster()
                {
                    BookName = tblBookMasters.BookName,
                    AuthorName = tblBookMasters.AuthorName,
                    CreatedDate = indianTime.ToLocalTime()
                };
                Db.tblBookMasters.Add(tblBookMaster);
                int affectedrows = Db.SaveChanges();
                if (affectedrows > 0)
                {
                    return Content(HttpStatusCode.OK, "Successfully Inserted");
                }
                else
                {
                    return Content(HttpStatusCode.OK, "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
