using GFATicketing.Data.DbContext;
using GFATicketing.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFATicketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UssdController : ControllerBase
    {
        Dictionary<string, UssdResponse> ussdResponses = null;

        public GFATicketingDbContext DbContext { get; set; }
        public AppSettings AppSettings { get; set; }
        public UssdController(GFATicketingDbContext _dbContext, IOptions<AppSettings> _appSettings)
        {
            DbContext = _dbContext;
            AppSettings = _appSettings.Value;

            ussdResponses = new Dictionary<string, UssdResponse>()
            {
                #region Level 0
                {
                    "null",new UssdResponse{
                        Message = "Welcome to the GFA ticketing platform\n1. Buy a ticket\n2. Check fixtures\n3. Sign up for loyalty",
                        Type="Response",
                        ClientState = "1"
                    }
                },
                #endregion

                #region Level 1
                {
                    "1",new UssdResponse
                    {
                        Message = "Which type of ticket\n1. VIP\n2. Popular stand",
                        Type= "Response",
                        ClientState = "1-1"
                    }

                },
                {
                    "2",new UssdResponse
                    {
                        Message = "Choose Club\n1. Kotoko\n2. Hearts\n3. Brekum Chelsea\n4. Aduana Stars\n5. Elmina Sharks\n6.Medeama Fc",
                        Type = "Response",
                        ClientState = "2-1"
                    }
                },
                {
                    "3",new UssdResponse
                    {
                        Message = "Under Construction",
                        Type = "Response",
                        ClientState = "3-1"
                    }
                },
                #endregion

                #region Level 2
                {
                    "1-1",new UssdResponse
                    {
                        Message="Choose ticket\n1. Kotoko Vs Hearts - 50 GHS\n2. Brekum Chelsea Vs Aduana Stars - 60 GHS\n3. Elmina Sharks Vs Medeama Fc - 50 GHS",
                        Type = "Response",
                        ClientState = "1-1-1"

                    }
                },
                {
                    "1-2",new UssdResponse
                    {
                        Message="Choose ticket\n1. Kotoko Vs Hearts - 20 GHS\n2. Brekum Chelsea Vs Aduana Stars - 20 GHS\n3. Elmina Sharks Vs Medeama Fc - 20 GHS",
                        Type = "Response",
                        ClientState = "1-1-1"

                    }
                },
                {
                    "2-1",new UssdResponse
                    {
                        Message="Fixtures\n1. Kotoko Vs Hearts 2/2/19\n2. Kotoko Vs Aduana Stars 2/2/19\n3. Kotoko Vs Medeama Fc 2/2/19",
                        Type = "Response",
                        ClientState = "2-1-1"

                    }
                }
                #endregion

            };

        }

        [HttpPost]
        public async Task<IActionResult> Index(UssdRequest ussdRequest)
        {
            string state = null;
            if (ussdRequest.Sequence == "1")
            {
            }
            else if (ussdRequest.Sequence == "2")
            {
                state = ussdRequest.Message;
            }
            else
            {
                state = ussdRequest.ClientState;
            }

            try
            {
                switch (state)
                {
                    case null:
                        return Json(ussdResponses["null"]);
                    case "1":
                        return Json(ussdResponses["1"]);
                    case "2":
                        return Json(ussdResponses["2"]);
                    case "3":
                        return Json(ussdResponses["3"]);
                    case "1-1":
                        return Json(ussdResponses["1-1"]);
                    case "1-2":
                        return Json(ussdResponses["1-2"]);
                    case "1-3":
                        return Json(ussdResponses["1-3"]);
                    case "2-1":
                        return Json(ussdResponses["2-1"]);
                    case "2-2":
                        return Json(ussdResponses["2-1"]);
                    case "2-3":
                        return Json(ussdResponses["2-1"]);
                    default:
                        return Json(ussdResponses["null"]);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        JsonResult Json(object result)
        {
            return new JsonResult(result, Misc.getDefaultResolverJsonSettings());
        }

    }

    public class UssdRequest
    {
        public string Type { get; set; }

        public string Mobile { get; set; }

        public string SessionId { get; set; }

        public string ServiceCode { get; set; }

        public string Message { get; set; }

        public string Operator { get; set; }

        public string Sequence { get; set; }

        public string ClientState { get; set; }


    }

    public class UssdResponse
    {
        public string Message { get; set; }

        public string Type { get; set; }

        public string ClientState { get; set; }
    }

}