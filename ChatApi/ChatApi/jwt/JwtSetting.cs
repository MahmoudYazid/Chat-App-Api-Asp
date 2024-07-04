namespace ChatApi.jwt
{
    public class JwtSetting
    {
        public string Issuer { get; set; } = "https://localhost:7216";
        public string Audiance { get; set; } = "https://localhost:7216";

        public string key { get; set; } = "7593b0f9fe19c1bb3391964e1b17d433";
        public JwtSetting() {
            Issuer = "https://localhost:7216";
            Audiance = "https://localhost:7216";
            key = "7593b0f9fe19c1bb3391964e1b17d433";
        }
    }
}
