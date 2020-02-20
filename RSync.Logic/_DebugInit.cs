using RSync.Core.Enumerations;
using RSync.Domain.Model;

namespace RSync.Logic
{
#if DEBUG
    public class _DebugInit
    {
        private static readonly string Login = "Harry";
        public static void Init()
        {
            AddUser();
            AddSettings();
        }

        private static void AddSettings()
        {
            SettingsLogic.AddSettings(new Settings(UserLogic.GetUser(Login).UserId, AppLanguage.English));
        }

        private static void AddUser()
        {
            string password = "123";
            string RsaPublicKey = "{\"D\": null,\"DP\": null,\"DQ\": null,\"Exponent\": \"AQAB\",\"InverseQ\": null," +
                "\"Modulus\": \"0NUpZ6nZonSc4mF5TiuUUzP1YkrO7lIBC6fvJdBD7+eJbgp1EE15IHXkFVhcF54fUwJKcdnp6wSb6texGKCM/ggccIQHFC1lscsxNgOPE57gRFhV7dv91gaOQJEsPF9SqPplzsb2jPjTidcZQuW4SHRG1Je4R3MAdQHFQXH6+pDPzrLcBcqtpnWIfPdonySR7dCGb/34vDNROdCffXfOeHq4rxdf946WSEmibq2vhAWaRuPwh/npvDBDIeg7XC8pps5eiGaQbg8SKz9pc8bfX0rGV4ua1JK17pL/Y+dU85D4LvlVVIBZIZu4zyFV2HVxGCstV6yzFeNPb1ZSHXjfLp7G4m8SdKNEfBxzlVCN1DloZzrHEW5brwqEu4cWn98M3Wn/NOttzDc+Xk+Omb0HIIA9xvE/yMhqK9Bbw0BDO5YB5ULUhN/J7bbatuK7sQ6AxUD9hW6wPYbU/MoIrft9o7Fm66xGzhYlaSasPJjsuQCoRpZ44UHnXURLrl9pa6laFrT4IIC0xAaY6NFmfNTNMGZy9siJid4l0xZWCfX4HdK1JwNrygKiD2PjwmQKgl/5ruGTavB2Fg2XVQLdI4fk50gLTG/lL2Fjwjd1qPbEfrLvNmOjr2ZRfqSFlwK5+IxCTjmVttJFv9jxxZfiFQPW1N76G5fCa+6WbeZX+Pi6EouiX6b3XA5EEkzBFUNrophNvkghTHMd2P0UWyWrRhm6bVdU91JNsrEyybltxgUapiYQwbrxjV2OOzucx0ektEebJu6gI+QXaR9sUoTYdwdtIqNLPcp/uK8mENw+NSGnKPRcX7uoQiPBVq1fAU+7pkeos0R7w7pgKx4YFpzE/r78OOOPfphSRcQsxeXpg0Hu/NQKcBRu3qmPzrPWc7RxUdWnhZSHv9Cs/qkCkeenu77fvEfw5ruLdYx5lTonCzA9qh2QiovgEx1qklWOpTsxCLLFXaBMAzwOAEaHnMQkz6baFhP4hyeaLTF7IYGWYJRpcwMBq2mB0DOGgXwFQTWjGHbNcvrWjfkYy/pzkihlLqticJVFrCShl25EPFp5fpm9h4E4Z3cP2K5u5Atzqt2kf1X5qyFohlLpWx1NvJeEK9Wmxo70Hd85iCdPSpXL1yMm2N/2ulM/Fo05OHnjZ6Io9BsooLpDIHExWjYny1BpPfOL/uSaU35EC0JSDyhHdjCkHGuXAfOcocIPxfHajI6SlNFip2LPxXN6NpLAgyaVmFp0OKPctmScfqM0aUhbR4xubBy14ea6JhTEmbe+M08blZ4sXyszYowZg2frrgkxVlYq4n3NJjEjcDg34FkeI60cNQkWVhOJtLzeFjR6dcw0f7YRH2+dsyRT3yISUVHJIC4bQQ==\"," +
                "\"P\": null,\"Q\": null}";

            UserLogic.AddUser(new User(Login, password, RsaPublicKey));
        }
    }
#endif
}
