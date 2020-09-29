using Entities.DTO;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManagement.Utils
{
    public sealed class Helper
    {
      //  internal enum APIStatus { AppFail = 0, AppSuccess = 1, EngineFail = 2, EngineWarning = 3, DataExist = 4 };

        internal enum HTTPStatus
        {
            OK = 200, Created = 201, BadRequest = 400, Unauthorized = 401, Forbidden = 403, NotFound = 404,
            MethodNotAllowed = 405, Conflict = 409, InternalServerError = 500, ServiceUnavailable = 503
        };

        internal static readonly Dictionary<HTTPStatus, string> HTTPStatusDescription = new Dictionary<HTTPStatus, string>()
        {
            { HTTPStatus.OK,"The request was successfully completed"},
            { HTTPStatus.Created,"A new resource was successfully created" },
            { HTTPStatus.BadRequest,"The request was invalid" },
            { HTTPStatus.Unauthorized,"The request did not include an authentication token or the authentication token was expired"  },
            { HTTPStatus.Forbidden, "The client did not have permission to access the requested resource"  },
            { HTTPStatus.NotFound, "The requested resource was not found"  },
            { HTTPStatus.MethodNotAllowed,"The HTTP method in the request was not supported by the resource. For example, the DELETE method cannot be used with the Agent API"},
            { HTTPStatus.Conflict,"The request could not be completed due to a conflict. For example,  POST ContentStore Folder API cannot complete if the given file or folder name already exists in the parent location"  } ,
            { HTTPStatus.InternalServerError,"The request was not completed due to an internal error on the server side"  } ,
            { HTTPStatus.ServiceUnavailable,"The server was unavailable"}
        };

        internal static string GenerateJSONWebToken(Users user, IConfiguration config)
        {
            string _key = config["JWT:key"];
            string _issuer = config["JWT:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, Convert.ToString(user.UserName)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, Convert.ToString(user.UserID))
            };

            var token = new JwtSecurityToken(
              _issuer,
              _issuer,
              claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        internal static string APIResponseFormatter(object data = null, Exception exp = null)
        {
            object _apiOutput = null;

            try
            {
                if (exp != null)
                {
                    Error _objError = new Error()
                    {
                        Message = exp.Message,
                        Exception = Convert.ToString(exp.InnerException)
                    };

                    _apiOutput = new APIFormat() { Data = data, Error = _objError };
                }

                else
                {
                    _apiOutput = new APIFormat() { Data = data };
                }
            }
            catch
            {
                throw;
            }
            return JsonConvert.SerializeObject(_apiOutput);
        }
    }
}
