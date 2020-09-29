using System;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UserManagement.Utils;

namespace UserManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private static IConfiguration _configuration;
        private ILoggerManager _logger;

        public AuthController(IRepositoryWrapper repoWrapper, IConfiguration iConfig, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _configuration = iConfig;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("version")]
        public IActionResult Version()
        {
            return StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(Helper.HTTPStatusDescription[Helper.HTTPStatus.OK]));
        }

        [AllowAnonymous]
        [HttpPost, Route("validateuser")]
        public IActionResult ValidateUser([FromBody]Auth authUser)
        {
            var statusCode = StatusCode((int)Helper.HTTPStatus.InternalServerError, Helper.HTTPStatusDescription[Helper.HTTPStatus.InternalServerError]);
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _repoWrapper.AuthDetails.ValidateUser(authUser);

                    if (user != null && authUser.Password != null)
                    {
                        var _authToken = Helper.GenerateJSONWebToken(user, _configuration);
                        statusCode = StatusCode((int)Helper.HTTPStatus.OK, Helper.APIResponseFormatter(_authToken));
                    }
                    else
                    {
                        statusCode = StatusCode((int)Helper.HTTPStatus.Unauthorized, Helper.HTTPStatusDescription[Helper.HTTPStatus.Unauthorized]);
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
    }
}