using Cmt.WebApi.Infrastructure.Configuration;
using Cmt.WebApi.Infrastructure.Constants;
using Microsoft.Extensions.Configuration;

namespace Cmt.WebApi.Infrastructure.Providers
{
    public static class ConfigurationsProvider
    {
        public static PasswordConfig GetPasswordSettings(IConfiguration configuration)
        {
            var passwordSettings = new PasswordConfig();
            var configPassword = configuration.GetSection(WebApiConstants.PasswordConfigurationSection);
            configPassword.Bind(passwordSettings);

            return passwordSettings;
        }

        public static LockoutConfig GetLockoutSettings(IConfiguration configuration)
        {
            var lockoutSettings = new LockoutConfig();
            var configLockout = configuration.GetSection(WebApiConstants.LockoutConfigurationSection);
            configLockout.Bind(configLockout);

            return lockoutSettings;
        }

        public static JwtConfig GetJwtSettings(IConfiguration configuration)
        {
            var jwtSettings = new JwtConfig();
            var configJwt = configuration.GetSection(WebApiConstants.JwtConfigurationSection);
            configJwt.Bind(jwtSettings);

            return jwtSettings;
        }

        public static UserConfig GetUserSettings(IConfiguration configuration)
        {
            var userSettings = new UserConfig();
            var configUser = configuration.GetSection(WebApiConstants.UserConfigurationSection);
            configUser.Bind(userSettings);

            return userSettings;
        }

        public static string GetConnectionString(string sectionName, IConfiguration configuration)
        {
            return configuration.GetConnectionString(sectionName);
        }
    }
}
