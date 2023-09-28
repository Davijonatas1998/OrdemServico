using Microsoft.AspNetCore.Identity;
using System.Text;
using Xunit;

namespace OrdemServico.Services
{
    public static class ServicesManager
    {
        public static string Key = "TEST-1818529873385732-092618-fa08bf26cd7f9787e6a7d6b0653af207-345299141";

        [Fact]
        public static string TokenSystem(int tamanho)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        [Fact]
        public static int? IdAction(string base64Id)
        {
            if (!string.IsNullOrEmpty(base64Id))
            {
                byte[] bytes = Convert.FromBase64String(base64Id);
                if (bytes.Length == sizeof(int))
                {
                    return BitConverter.ToInt32(bytes, 0);
                }
            }
            return null;
        }

        [Fact]
        public static string External(int tamanho)
        {
            var chars = "0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
