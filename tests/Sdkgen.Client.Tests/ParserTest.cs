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

        Assert.AreEqual("https://api.acme.com/foo/bar", parser.Url("/foo/bar", new Dictionary<string, object>()));
        Assert.AreEqual("https://api.acme.com/foo/foo", parser.Url("/foo/:bar", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo", parser.Url("/foo/$bar<[0-9]+>", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo", parser.Url("/foo/$bar", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo", parser.Url("/foo/{bar}", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo/bar", parser.Url("/foo/{bar}/bar", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo/bar", parser.Url("/foo/$bar<[0-9]+>/bar", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo/bar", parser.Url("/foo/$bar/bar", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/foo/bar", parser.Url("/foo/{bar}/bar", this.NewMap("bar", "foo")));

        Assert.AreEqual("https://api.acme.com/foo/", parser.Url("/foo/{bar}", this.NewMap("bar", null)));
        Assert.AreEqual("https://api.acme.com/foo/1337", parser.Url("/foo/{bar}", this.NewMap("bar", 1337)));
        Assert.AreEqual("https://api.acme.com/foo/13.37", parser.Url("/foo/{bar}", this.NewMap("bar", 13.37)));
        Assert.AreEqual("https://api.acme.com/foo/1", parser.Url("/foo/{bar}", this.NewMap("bar", true)));
        Assert.AreEqual("https://api.acme.com/foo/0", parser.Url("/foo/{bar}", this.NewMap("bar", false)));
        Assert.AreEqual("https://api.acme.com/foo/foo", parser.Url("/foo/{bar}", this.NewMap("bar", "foo")));
        Assert.AreEqual("https://api.acme.com/foo/2023-02-21", parser.Url("/foo/{bar}", this.NewMap("bar", new DateOnly(2023, 2, 21))));
        Assert.AreEqual("https://api.acme.com/foo/2023-02-21T19:19:00Z", parser.Url("/foo/{bar}", this.NewMap("bar", new DateTime(2023, 2, 21, 19, 19, 0, DateTimeKind.Utc))));
        Assert.AreEqual("https://api.acme.com/foo/19:19:00", parser.Url("/foo/{bar}", this.NewMap("bar", new TimeOnly(19, 19, 0))));
    }

    [Test]
    public void TestQuery()
    {
        Parser parser = new Parser("https://api.acme.com/");

        TestObject test = new TestObject();
        test.Name = "foo";

        Dictionary<string, object> map = new Dictionary<string, object>();
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
        Assert.AreEqual("int", queryParams[0].Name);
        Assert.AreEqual("1337", queryParams[0].Value);
        Assert.AreEqual("float", queryParams[1].Name);
        Assert.AreEqual("13.37", queryParams[1].Value);
        Assert.AreEqual("true", queryParams[2].Name);
        Assert.AreEqual("1", queryParams[2].Value);
        Assert.AreEqual("false", queryParams[3].Name);
        Assert.AreEqual("0", queryParams[3].Value);
        Assert.AreEqual("string", queryParams[4].Name);
        Assert.AreEqual("foo", queryParams[4].Value);
        Assert.AreEqual("date", queryParams[5].Name);
        Assert.AreEqual("2023-02-21", queryParams[5].Value);
        Assert.AreEqual("datetime", queryParams[6].Name);
        Assert.AreEqual("2023-02-21T19:19:00Z", queryParams[6].Value);
        Assert.AreEqual("time", queryParams[7].Name);
        Assert.AreEqual("19:19:00", queryParams[7].Value);
        Assert.AreEqual("name", queryParams[8].Name);
        Assert.AreEqual("foo", queryParams[8].Value);
    }
    
    private Dictionary<string, object> NewMap(string key, object value) {
        Dictionary<string, object> map = new Dictionary<string, object>();
        map.Add(key, value);
        return map;
    }
}
