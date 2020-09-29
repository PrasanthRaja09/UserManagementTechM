using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Model;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace UserManagement.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private ILoggerManager _logger;

        public UserController(IRepositoryWrapper repoWrapper, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("version")]
        public IActionResult Version()
        {
            return StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
        }

        [HttpPost, Route("AddAdminUser")]
        public IActionResult AddAdminUser([FromBody]Users userDetails)
        {

            var identity = User.Identity as ClaimsIdentity;

            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    userDetails.CreatedOn = DateTime.Now;
                    Users _userDetails = _repoWrapper.UserDetails.GetUserByUserName(userDetails.UserName);

                    
                    if (_userDetails == null)
                    {
                        int _userID = _repoWrapper.UserDetails.AddAdminUser(userDetails);
                        User_Roles _userRoles = new User_Roles()
                        {
                            UserID = _userID,
                            RoleID = 1
                        };
                        _repoWrapper.UserRoleDetails.AddUserRoles(_userRoles);

                       
                        Audit_logs _auditlog = new Audit_logs()
                        {
                            action = "Insert",
                            uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                            tablename = "Users",
                            datetime = DateTime.Now
                        };

                        int acty = _repoWrapper.ActivityDetails.InsertLog(_auditlog);
                        statusCode = StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
                    }
                    else
                    {
                        statusCode = StatusCode((int)Helper.HTTPStatus.Conflict, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.Conflict]));
                    }
                }
                else
                {
                    statusCode = StatusCode((int)Helper.HTTPStatus.BadRequest, Helper.HTTPStatusDescription[Helper.HTTPStatus.BadRequest]);
                }
            }
            catch (Exception exp)
            {
                statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
                _logger.LogError(exp.Message);
            }

            return statusCode;
        }

        [HttpPost, Route("AddUser")]
        public IActionResult AddUser([FromBody]Users userDetails)
        {
            var identity = User.Identity as ClaimsIdentity;
            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    userDetails.CreatedOn = DateTime.Now;
                    Users _userDetails = _repoWrapper.UserDetails.GetUserByUserName(userDetails.UserName);

                    if (_userDetails == null)
                    {
                        int _userID = _repoWrapper.UserDetails.AddUser(userDetails);
                        User_Roles _userRoles = new User_Roles()
                        {
                            UserID = _userID,
                            RoleID = 2
                        };
                        _repoWrapper.UserRoleDetails.AddUserRoles(_userRoles);
                        Audit_logs _auditlog = new Audit_logs()
                        {
                            action = "Insert",
                            uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                            tablename = "Users",
                            datetime = DateTime.Now
                        };

                        int acty = _repoWrapper.ActivityDetails.InsertLog(_auditlog);

                        statusCode = StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
                    }
                    else
                    {
                        statusCode = StatusCode((int)Helper.HTTPStatus.Conflict, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.Conflict]));
                    }
                }
                else
                {
                    statusCode = StatusCode((int)Helper.HTTPStatus.BadRequest, Helper.HTTPStatusDescription[Helper.HTTPStatus.BadRequest]);
                }
            }
            catch (Exception exp)
            {
                statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
                _logger.LogError(exp.Message);
            }

            return statusCode;
        }

        [HttpPut, Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody]Users userDetails)
        {
            var identity = User.Identity as ClaimsIdentity;
            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    Users _userDetails = _repoWrapper.UserDetails.GetUserByUserID(userDetails.UserID);

                    _userDetails.UserName = userDetails.UserName;
                    _userDetails.FirstName = userDetails.FirstName;
                    _userDetails.LastName = userDetails.LastName;
                    _userDetails.EmailID = userDetails.EmailID;
                    _userDetails.DateOfBirth = userDetails.DateOfBirth;
                    _userDetails.Age = userDetails.Age;
                    _userDetails.PhoneNumber = userDetails.PhoneNumber;
                    _userDetails.Active = userDetails.Active;
                    _userDetails.LastModifiedOn = DateTime.Now;
                    _repoWrapper.UserDetails.UpdateUser(_userDetails);
                    Audit_logs _auditlog = new Audit_logs()
                    {
                        action = "Update",
                        uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                        tablename = "Users",
                        datetime = DateTime.Now
                    };

                    int acty = _repoWrapper.ActivityDetails.InsertLog(_auditlog);

                    statusCode = StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
                }
                else
                {
                    statusCode = StatusCode((int)Helper.HTTPStatus.BadRequest, Helper.HTTPStatusDescription[Helper.HTTPStatus.BadRequest]);
                }
            }
            catch (Exception exp)
            {
                statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
                _logger.LogError(exp.Message);
            }

            return statusCode;
        }

        [HttpDelete, Route("DeleteUser/{userID}")]
        public IActionResult DeleteUser(int userID)
        {
            var identity = User.Identity as ClaimsIdentity;
            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    _repoWrapper.UserDetails.DeleteUser(userID);
                    Audit_logs _auditlog = new Audit_logs()
                    {
                        action = "Delete",
                        uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                        tablename = "Users",
                        datetime = DateTime.Now
                    };

                    int acty = _repoWrapper.ActivityDetails.InsertLog(_auditlog);
                    statusCode = StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
                }
                else
                {
                    statusCode = StatusCode((int)Helper.HTTPStatus.BadRequest, Helper.HTTPStatusDescription[Helper.HTTPStatus.BadRequest]);
                }
            }
            catch (Exception exp)
            {
                statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
                _logger.LogError(exp.Message);
            }

            return statusCode;
        }
    }
}