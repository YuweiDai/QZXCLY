using QZCHY.Core;
using QZCHY.Core.Domain.AccountUsers;
using QZCHY.Services.Security;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.AccountUsers
{
    public class AccountUserRegistrationService : IAccountUserRegistrationService
    {
        private readonly IAccountUserService _customerService;
        private readonly IEncryptionService _encryptionService;
        
        private readonly AccountUserSettings _customerSettings;

        public AccountUserRegistrationService(IAccountUserService customerService, IEncryptionService encryptionService,
        AccountUserSettings customerSettings)
        {
            _customerService = customerService;
            _encryptionService = encryptionService;
            _customerSettings = customerSettings;
        }

        /// <summary>
        /// 验证用户的正确性
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<AccountUser> ValidateAccountUserAsync(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("密码不能为空");

            var accountUser = _customerService.GetAccountUserByUsername(account);
            if (accountUser != null)
            {
                bool passwordCorrect = false;
                switch (accountUser.PasswordFormat)
                {
                    case PasswordFormat.Clear:
                        {
                            passwordCorrect = password == accountUser.Password;
                        }
                        break;
                    case PasswordFormat.Encrypted:
                        {
                            passwordCorrect = _encryptionService.EncryptText(password) == accountUser.Password;
                        }
                        break;
                    case PasswordFormat.Hashed:
                        {
                            string saltKey = _encryptionService.CreateSaltKey(5);
                            passwordCorrect = _encryptionService.CreatePasswordHash(password, saltKey, _customerSettings.HashedPasswordFormat) == accountUser.Password;
                        }
                        break;
                    default:
                        break;
                }

                if (passwordCorrect)
                {
                    accountUser.LastLoginDate = DateTime.Now;

                    _customerService.UpdateAccountUser(accountUser);

                    return Task.FromResult(accountUser);
                }
            }

            return Task.FromResult<AccountUser>(null); ;
        }

        /// <summary>
        /// 验证用户的正确性
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<AccountUser> ValidateWechatAppAccountUserAsync(string code, string ivAndEncryptedData)
        {
            var authUrl = ConfigurationManager.AppSettings["authUrl"];
            var appid=  ConfigurationManager.AppSettings["appid"];
            var secret =  ConfigurationManager.AppSettings["secret"];

            string url = "{0}?appid={1}&secret={2}&js_code={3}&grant_type=authorization_code";
            var result = string.Empty;
            url = string.Format(url, authUrl, appid, secret,code);
            System.Net.WebRequest wReq = System.Net.WebRequest.Create(url);
            // Get the response instance.  
            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("utf-8")))
            {
                result = reader.ReadToEnd();
            }

            var wechatAuthorizeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<WechatAuthorizeResponse>(result);
            if(!string.IsNullOrEmpty(wechatAuthorizeResponse.openid))
            {
                var accountUser = _customerService.GetAccountUserByWechatOpenId(wechatAuthorizeResponse.openid);

                var encryptedData= ivAndEncryptedData.Split(';')[0];
                var iv = ivAndEncryptedData.Split(';')[1];

                if (accountUser == null)
                {
                    var wechatUserInfoString = _encryptionService.AESDecrypt(encryptedData, wechatAuthorizeResponse.session_key, iv);

                    var wechatUserInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<WechatUserInfo>(wechatUserInfoString);

                    accountUser = new AccountUser
                    {
                        Active = true,
                        AccountUserGuid = Guid.NewGuid(),
                        AvatarUrl = wechatUserInfo.avatarUrl,
                        City = wechatUserInfo.city,
                        Country = wechatUserInfo.country,
                        Gender = wechatUserInfo.gender,
                        IsSystemAccount = false,
                        PasswordFormat = PasswordFormat.Clear,
                        Province = wechatUserInfo.province,
                        UnionId = wechatUserInfo.unionId,
                        WechatOpenId = wechatUserInfo.openId,
                        UserName = wechatUserInfo.nickName
                    };

                    var role = _customerService.GetAccountUserRoleBySystemName(SystemAccountUserRoleNames.Registered);
                    if (role != null)
                        accountUser.AccountUserRoles.Add(role);

                    _customerService.InsertAccountUser(accountUser);
                }

                return Task.FromResult(accountUser);
            }
 

            return Task.FromResult<AccountUser>(null); ;
        }


        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountUserLoginResults ValidateAccountUser(string account, string password)
        {
            var accountUser = _customerService.GetAccountUserByUsername(account);

            if (accountUser == null)
                return AccountUserLoginResults.AccountUserNotExist;
            if (accountUser.Deleted)
                return AccountUserLoginResults.Deleted;
            if (!accountUser.Active)
                return AccountUserLoginResults.NotActive;
            //only registered can login
            if (!accountUser.IsRegistered())
                return AccountUserLoginResults.NotRegistered;

            string pwd = "";
            switch (accountUser.PasswordFormat)
            {
                case PasswordFormat.Encrypted:
                    pwd = _encryptionService.EncryptText(password);
                    break;
                case PasswordFormat.Hashed:
                    pwd = _encryptionService.CreatePasswordHash(password, accountUser.PasswordSalt, _customerSettings.HashedPasswordFormat);
                    break;
                default:
                    pwd = password;
                    break;
            }

            bool isValid = pwd == accountUser.Password;
            if (!isValid)
                return AccountUserLoginResults.WrongPassword;

            //save last login date
            accountUser.LastLoginDate = DateTime.Now;
            _customerService.UpdateAccountUser(accountUser);
            return AccountUserLoginResults.Successful;

        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual AccountUserRegistrationResult RegisterAccountUser(AccountUserRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.AccountUser == null)
                throw new ArgumentNullException("无法加载当前用户");

            var result = new AccountUserRegistrationResult();
            if (request.AccountUser.IsSearchEngineAccount())
            {
                result.AddError("搜索引擎用户无法注册");
                return result;
            }
            if (request.AccountUser.IsBackgroundTaskAccount())
            {
                result.AddError("Background task account can't be registered");
                return result;
            }

            if (String.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError("密码不能为空");
                return result;
            }

            //暂时
            if (_customerSettings.UsernamesEnabled)
            {
                if (String.IsNullOrEmpty(request.Username))
                {
                    result.AddError("用户名不能为空");
                    return result;
                }
            }

            if (_customerService.GetAccountUserByUsername(request.Username) != null)
            {
                result.AddError(string.Format("用户名已经注册用户"));
                return result;
            }

            //at this point request is valid
            request.AccountUser.UserName = request.Username; 
            request.AccountUser.PasswordFormat = request.PasswordFormat;

            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    {
                        request.AccountUser.Password = request.Password;
                    }
                    break;
                case PasswordFormat.Encrypted:
                    {
                        request.AccountUser.Password = _encryptionService.EncryptText(request.Password);
                    }
                    break;
                case PasswordFormat.Hashed:
                    {
                        string saltKey = _encryptionService.CreateSaltKey(5);
                        request.AccountUser.PasswordSalt = saltKey;
                        request.AccountUser.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, _customerSettings.HashedPasswordFormat);
                    }
                    break;
                default:
                    break;
            }

            request.AccountUser.Active = request.IsApproved;

            //add to 'Registered' role
            var registeredRole = _customerService.GetAccountUserRoleBySystemName(SystemAccountUserRoleNames.Registered);
            if (registeredRole == null)
                throw new QZCHYException("'Registered' role could not be loaded");
            request.AccountUser.AccountUserRoles.Add(registeredRole);
            //remove from 'Guests' role
            var guestRole = request.AccountUser.AccountUserRoles.FirstOrDefault(cr => cr.SystemName == SystemAccountUserRoleNames.Guests);
            if (guestRole != null)
                request.AccountUser.AccountUserRoles.Remove(guestRole);

            _customerService.UpdateAccountUser(request.AccountUser);
            return result;
        }


    }
}
