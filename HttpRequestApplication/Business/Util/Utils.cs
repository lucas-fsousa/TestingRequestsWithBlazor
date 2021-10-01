using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace App.WebApi.Business.Util {
  public static class Utils {
    public static string EncodePassword(this string str) {
      byte[] salt = new byte[128 / 8];
      byte[] codify = KeyDerivation.Pbkdf2(str, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8);
      return Convert.ToBase64String(codify);
    }
  }
}
