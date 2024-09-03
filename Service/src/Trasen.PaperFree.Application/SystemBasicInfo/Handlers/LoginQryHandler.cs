using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers
{
    internal sealed class LoginQryHandler : IRequestHandler<LoginQry, string>
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<TrasenBasePlatformSetting> options;
        private readonly IOptions<JwtSetting> JwtSetting;
        private readonly Validate<LoginQry> validate;

        public LoginQryHandler(IHttpClientFactory httpClientFactory, IOptions<TrasenBasePlatformSetting> options, IOptions<JwtSetting> jwtSetting, Validate<LoginQry> validate)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options;
            JwtSetting = jwtSetting;
            this.validate = validate;
        }

        public async Task<string> Handle(LoginQry request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            StringContent strcontent = new StringContent(JsonConvert.SerializeObject(new
            {
                MEMBER_CODE = request.loginName,
                PASSWORD = request.loginpassword,
                ORG_CODE = request.orgCode,
                HOSP_CODE = request.hospCode
            }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{options.Value.domainName}{TrasenApiConst.Login}", strcontent);
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResultJson<List<LoginDto>>>(result);
            if (data == null || data.Code != (int)System.Net.HttpStatusCode.OK || !data.Success) throw new BusinessException(MessageType.Warn, "登录失败!", data?.Msg ?? "");
            LoginDto models = data?.Data?.FirstOrDefault()!;
            TokenModelJwt tokenModelJwt = new TokenModelJwt()
            {
                UserId = models.MEMBER_CODE,
                UserLoginName = models.MEMBER_CODE,
                UserNickName = string.Empty,
                HospCode = models.HOSP_CODE,
                OrgCode = models.ORG_CODE
            };
            return $"Bearer {JwtHelper.IssueJwt(JwtSetting.Value, tokenModelJwt)}";
        }
    }
}