using ChatApi.jwt;
using ChatApi.model.dbModel;
using ChatApi.model.query;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApi.handeler.query
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        public MongoDbService MongoDbServiceInst { get; set; }
        public JwtSetting JwtSetting { get; set; }

        public LoginQueryHandler(MongoDbService mongoDbServiceInjected,JwtSetting jwtSettingInjected) {
            MongoDbServiceInst = mongoDbServiceInjected;
            JwtSetting = jwtSettingInjected;
            
        }
        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<PersonDbModel>.Filter.And(
                Builders<PersonDbModel>.Filter.Eq("Name",request.Name),
                Builders<PersonDbModel>.Filter.Eq("password", request.Password)


                );


            var searchQuery = MongoDbServiceInst.PersonDb.Find(filter).FirstOrDefaultAsync();

            if (searchQuery != null)
            {
                var GetSymmetricCode = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    JwtSetting.key
                    ));

                var credintial = new SigningCredentials(GetSymmetricCode, SecurityAlgorithms.HmacSha256 );
                var Claims = new[]
                {
                    new Claim(ClaimTypes.Name,searchQuery.Result.Id.ToString()),                    

                };
                var NewSecurityKey = new JwtSecurityToken(
                    issuer : JwtSetting.Issuer,
                    audience : JwtSetting.Audiance,
                    claims : Claims,
                    expires : DateTime.Now.AddDays(1),
                    signingCredentials : credintial


                    );

                return new JwtSecurityTokenHandler().WriteToken(NewSecurityKey);
            }
            else {

                return "this user is not exist"; 
            }
        }
    }
}
