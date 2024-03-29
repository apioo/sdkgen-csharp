﻿/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

namespace Sdkgen.Client;

public class AccessToken
{
    public AccessToken(string token, string tokenType, long expiresIn, string refreshToken, string scope)
    {
        Token = token;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
        Scope = scope;
    }

    public string Token { get; set; }
    public string TokenType { get; set; }
    public long ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public string Scope { get; set; }

    public bool HasRefreshToken()
    {
        return !String.IsNullOrEmpty(this.RefreshToken);
    }

    public long GetExpiresInTimestamp()
    {
        long nowTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        long expiresIn = this.ExpiresIn;
        if (expiresIn < 529196400) {
            // in case the expires in is lower than 1986-10-09 we assume that the field represents the duration in seconds
            // otherwise it is probably a timestamp
            expiresIn = nowTimestamp + expiresIn;
        }

        return expiresIn;
    }
}
