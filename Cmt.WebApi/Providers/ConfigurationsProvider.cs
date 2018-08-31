using Cmt.WebApi.Configuration;
using Microsoft.Extensions.Configuration;

namespace Cmt.WebApi.Providers
{
    public static class ConfigurationsProvider
    {
        public static PasswordSettigs GetPasswordSettings(IConfiguration configuration)
        {
            var passwordSettings = new PasswordSettigs();
            var configPassword = configuration.GetSection(Constants.PasswordConfigurationSection);
            configPassword.Bind(passwordSettings);

            return passwordSettings;
        }

        public static LockoutSettings GetLockoutSettings(IConfiguration configuration)
        {
            var lockoutSettings = new LockoutSettings();
            var configLockout = configuration.GetSection(Constants.LockoutConfigurationSection);
            configLockout.Bind(configLockout);

            return lockoutSettings;
        }

        public static JwtSettings GetJwtSettings(IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            var configJwt = configuration.GetSection(Constants.JwtConfigurationSection);
            configJwt.Bind(jwtSettings);

            return jwtSettings;
        }

        public static UserSettings GetUserSettings(IConfiguration configuration)
        {
            var userSettings = new UserSettings();
            var configUser = configuration.GetSection(Constants.UserConfigurationSection);
            configUser.Bind(configUser);

            return userSettings;
        }

        public static string GetConnectionString(string sectionName, IConfiguration configuration)
        {
            return configuration.GetConnectionString(sectionName);
        }
    }
}
