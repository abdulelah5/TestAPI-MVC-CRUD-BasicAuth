using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using TestAPI.Filters;
using TestAPI.Helpers;
using TestAPI.Models;
using TestAPI_BIZ;
using TestAPI_DTO;
//using TestAPI_DAC;

namespace TestAPI.Controllers
{
    //[Authorize]
    [BasicAuthentication]
    public class CRUDController : ApiController
    {
        #region API Config
        private static APIConfiguration apiConfiguration = null;
        public static APIConfiguration APIConfiguration
        {
            get
            {
                try
                {
                    if (apiConfiguration == null)
                    {
                        apiConfiguration = (APIConfiguration)ConfigurationManager.GetSection("apiConfiguration");
                    }
                }
                catch
                {
                }
                return apiConfiguration;
            }
        }
        #endregion

        private PersonBIZ personBIZ = new PersonBIZ();
        
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(BaseResponse))]
        [Route("api/CRUD/personName/{id}")]
        public IHttpActionResult personName(int id)
        {
            Person some = personBIZ.PersonName(id);
            pName name = new pName { name = some.name};
            //Person some = personDAC.FetchPersonName(id);
            if (!string.IsNullOrEmpty(name.name))
            {
                return Json(new Models.BaseResponse
                {
                    Code = "200",
                    IsSuccess = true,
                    Result = "Success",
                    Data = name
                });
            }  
            else
                return Json(new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                });
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(BaseResponse))]
        [Route("api/CRUD/personInfo/{id}")]
        public IHttpActionResult personInfo(int id)
        {
            Person some = personBIZ.PersonInfo(id);
            //Person some = personDAC.FetchPersonName(id);
            if (!string.IsNullOrWhiteSpace(some.name))
            {
                return Json(new Models.BaseResponse
                {
                    Code = "200",
                    IsSuccess = true,
                    Result = "Success",
                    Data = some
                });
            }
            else
                return Json(new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                });
        }        
        public string personInfoMesg(int id)
        {
            Person some = personBIZ.PersonInfo(id);
            //Person some = personDAC.FetchPersonName(id);
            if (!string.IsNullOrWhiteSpace(some.name))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(some);
            }
            else
            {
               var error =  new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(error);
            }
        }


        [HttpGet]
        [ResponseType(typeof(BaseResponse))]
        [Route("api/CRUD/getAllName")]
        public IHttpActionResult getAllName()
        {
            List<Person> some = personBIZ.AllName();    //فيها كل بيانات الاشخاص وانا ابي اعرض الاسم فقط فلازم افرغها في مودل غير ال DTO
            List<pName> pNames = new List<pName>();
            foreach(var item in some)
            {
                pName temp = new pName();
                temp.name = item.name;
                pNames.Add(temp);
            }
            if (pNames.Count() > 0)
            {
                return Json(new Models.BaseResponse
                {
                    Code = "200",
                    IsSuccess = true,
                    Result = "Success",
                    Data = pNames
                });
            }
            else
                return Json(new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                });
        }

        [HttpPost]
        [ResponseType(typeof(PostPerson))]
        [Route("api/CRUD/insertPersonInfo")]
        public IHttpActionResult insertPersonInfo(PostPerson info)
        {
            Person person = new Person { name = info.name, phone = info.phone };
            int insertedID = personBIZ.InsertPersonInfo(person);
            Person some = personBIZ.PersonInfo(insertedID);
            if (!string.IsNullOrWhiteSpace(some.name))
            {
                some.name = some.name + "\n" + "Added sucessfuly";
                return Json(new Models.BaseResponse
                {
                    Code = "200",
                    IsSuccess = true,
                    Result = "Success",
                    Data = some
                });
            }
            else
                return Json(new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                });
        }

        [HttpPost]
        [ResponseType(typeof(Person))]
        [Route("api/CRUD/updatePersonInfo")]
        public IHttpActionResult updatePersonInfo(Person person)
        {
            bool isUpdated = personBIZ.UpdatePersonInfo(person);
            
            Person some = new Person();
            
            if (isUpdated)
                some = personBIZ.PersonInfo(person.id);

            if (!string.IsNullOrWhiteSpace(some.name))
            {
                some.name = some.name + "\n" + "Updated sucessfuly";
                return Json(new Models.BaseResponse
                {
                    Code = "200",
                    IsSuccess = true,
                    Result = "Success",
                    Data = some
                });
            }
            else
                return Json(new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                });
        }

        [HttpDelete]
        [ResponseType(typeof(Person))]
        [Route("api/CRUD/deletePersonInfo")]
        public IHttpActionResult deletePersonInfo(int id)
        {
            Person some = personBIZ.PersonInfo(id);
            bool isDeleted = personBIZ.DeletePersonInfo(id);            
            if (isDeleted)
            {
                some.name = some.name + "\n" + "Deleted sucessfuly";
                return Json(new Models.BaseResponse
                {
                    Code = "200",
                    IsSuccess = true,
                    Result = "Success",
                    Data = some
                });
            }
            else
                return Json(new Models.BaseResponse
                {
                    Code = "404",
                    IsSuccess = true,
                    Result = "Success",
                    Data = null,
                    Error = "No data found"
                });
        }
    }
}