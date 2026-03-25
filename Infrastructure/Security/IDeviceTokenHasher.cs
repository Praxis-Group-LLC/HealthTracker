using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security;

public interface IDeviceTokenHasher
{
    string HashToken(string token);
}

public sealed class DeviceTokenHasher : IDeviceTokenHasher
{
    public string HashToken(string token)
    {
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToHexString(hashBytes);
    }
}