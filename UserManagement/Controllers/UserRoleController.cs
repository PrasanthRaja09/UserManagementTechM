using System;
using System.Security.Claims;
using Contracts;
using Entities.Model;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Utils;

namespace UserManagement.Controllers
{
    [Route("api/userrole")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private ILoggerManager _logger;

        public UserRoleController(IRepositoryWrapper repoWrapper, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("version")]
        public IActionResult Version()
        {
            return StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
        }

        [HttpPost, Route("AddUserRole")]
        public IActionResult AddUserRole([FromBody]User_Roles roleDetails)
        {
            var identity = User.Identity as ClaimsIdentity;
            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    User_Roles _userRoleDetails = _repoWrapper.UserRoleDetails.GetRoleByUserID(roleDetails.UserID);

                    if (_userRoleDetails == null)
                    {
                        _repoWrapper.UserRoleDetails.AddUserRoles(roleDetails);
                        Audit_logs _auditlog = new Audit_logs()
                        {
                            action = "Insert",
                            uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                            tablename = "Roles",
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

        [HttpPut, Route("UpdateUserRole")]
        public IActionResult UpdateUserRole([FromBody]User_Roles roleDetails)
        {
            var identity = User.Identity as ClaimsIdentity;
            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    _repoWrapper.UserRoleDetails.UpdateUserRoles(roleDetails);

                    Audit_logs _auditlog = new Audit_logs()
                    {
                        action = "Update",
                        uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                        tablename = "Roles",
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

        [HttpDelete, Route("DeleteUserRole/{userID}")]
        public IActionResult DeleteUserRole(int userID)
        {
            var identity = User.Identity as ClaimsIdentity;

            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    _repoWrapper.UserRoleDetails.DeleteUserRoles(userID);
                    Audit_logs _auditlog = new Audit_logs()
                    {
                        action = "Delete",
                        uid = Convert.ToInt32(identity.FindFirst("Sid").Value),
                        tablename = "Roles",
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