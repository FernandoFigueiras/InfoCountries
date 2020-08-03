using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APICountryComment.Controllers
{
    

    public class CommentsController : ApiController
    {
        DataClassesDataContext dc = new DataClassesDataContext();

        // GET: api/Comments
        /// <summary>
        /// gets all the comment for all countries
        /// </summary>
        /// <returns> list of comments</returns>
        public List<comment> Get()
        {
            var comment = from Comments in dc.comments select Comments; 

            return comment.ToList();
        }

        // GET: api/Comments/alphacode
        /// <summary>
        /// returns the comments for a specific country
        /// </summary>
        /// <param name="alphacode"></param>
        /// <returns>comments</returns>
        public IHttpActionResult Get(string alphacode)
        {
            List<comment> comment = dc.comments.Where(c => c.alphacode == alphacode).ToList();
            if (comment != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, comment));
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }

        // POST: api/Comments
        /// <summary>
        /// Post a new comment
        /// </summary>
        /// <param name="_comment"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody]comment _comment)
        {

            dc.comments.InsertOnSubmit(_comment);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e.Message));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }

    }
}
