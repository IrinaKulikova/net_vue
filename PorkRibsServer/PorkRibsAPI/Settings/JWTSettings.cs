namespace PorkRibsAPI.Settings
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public int MinutesLife { get; set; }
    }
}
