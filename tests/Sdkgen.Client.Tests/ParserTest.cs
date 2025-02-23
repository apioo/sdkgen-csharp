/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using Sdkgen.Client.Tests.Generated;

namespace Sdkgen.Client.Tests;

public class ParserTest
{
    [Test]
    public void TestUrl()
    {
        Parser parser = new Parser("https://api.acme.com/");

        Assert.That(parser.Url("/foo/bar", new Dictionary<string, object?>()), Is.EqualTo("https://api.acme.com/foo/bar"));
        Assert.That(parser.Url("/foo/:bar", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo"));
        Assert.That(parser.Url("/foo/$bar<[0-9]+>", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo"));
        Assert.That(parser.Url("/foo/$bar", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo"));
        Assert.That(parser.Url("/foo/{bar}/bar", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo/bar"));
        Assert.That(parser.Url("/foo/$bar<[0-9]+>/bar", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo/bar"));
        Assert.That(parser.Url("/foo/$bar/bar", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo/bar"));
        Assert.That(parser.Url("/foo/{bar}/bar", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo/bar"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", null)), Is.EqualTo("https://api.acme.com/foo/"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", 1337)), Is.EqualTo("https://api.acme.com/foo/1337"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", 13.37)), Is.EqualTo("https://api.acme.com/foo/13.37"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", true)), Is.EqualTo("https://api.acme.com/foo/1"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", false)), Is.EqualTo("https://api.acme.com/foo/0"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", "foo")), Is.EqualTo("https://api.acme.com/foo/foo"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", new DateOnly(2023, 2, 21))), Is.EqualTo("https://api.acme.com/foo/2023-02-21"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", new DateTime(2023, 2, 21, 19, 19, 0, DateTimeKind.Utc))), Is.EqualTo("https://api.acme.com/foo/2023-02-21T19:19:00Z"));
        Assert.That(parser.Url("/foo/{bar}", this.NewMap("bar", new TimeOnly(19, 19, 0))), Is.EqualTo("https://api.acme.com/foo/19:19:00"));
    }

    [Test]
    public void TestQuery()
    {
        Parser parser = new Parser("https://api.acme.com/");

        TestObject test = new TestObject();
        test.Name = "foo";

        Dictionary<string, object?> map = new();
        map.Add("null", null);
        map.Add("int", 1337);
        map.Add("float", 13.37);
        map.Add("true", true);
        map.Add("false", false);
        map.Add("string", "foo");
        map.Add("date", new DateOnly(2023, 2, 21));
        map.Add("datetime", new DateTime(2023, 2, 21, 19, 19, 0, DateTimeKind.Utc));
        map.Add("time", new TimeOnly(19, 19, 0));
        map.Add("args", test);

        RestRequest request = new RestRequest();
        parser.Query(request, map, new List<string>(){"args"});

        var queryParams = new List<Parameter>(request.Parameters);
        Assert.That(queryParams[0].Name, Is.EqualTo("int"));
        Assert.That(queryParams[0].Value, Is.EqualTo("1337"));
        Assert.That(queryParams[1].Name, Is.EqualTo("float"));
        Assert.That(queryParams[1].Value, Is.EqualTo("13.37"));
        Assert.That(queryParams[2].Name, Is.EqualTo("true"));
        Assert.That(queryParams[2].Value, Is.EqualTo("1"));
        Assert.That(queryParams[3].Name, Is.EqualTo("false"));
        Assert.That(queryParams[3].Value, Is.EqualTo("0"));
        Assert.That(queryParams[4].Name, Is.EqualTo("string"));
        Assert.That(queryParams[4].Value, Is.EqualTo("foo"));
        Assert.That(queryParams[5].Name, Is.EqualTo("date"));
        Assert.That(queryParams[5].Value, Is.EqualTo("2023-02-21"));
        Assert.That(queryParams[6].Name, Is.EqualTo("datetime"));
        Assert.That(queryParams[6].Value, Is.EqualTo("2023-02-21T19:19:00Z"));
        Assert.That(queryParams[7].Name, Is.EqualTo("time"));
        Assert.That(queryParams[7].Value, Is.EqualTo("19:19:00"));
        Assert.That(queryParams[8].Name, Is.EqualTo("name"));
        Assert.That(queryParams[8].Value, Is.EqualTo("foo"));
    }
    
    private Dictionary<string, object?> NewMap(string key, object? value) {
        Dictionary<string, object?> map = new();
        map.Add(key, value);
        return map;
    }
}
