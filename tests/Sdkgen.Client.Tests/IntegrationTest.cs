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
using System.Net.Sockets;
using System.Text.Json;
using NUnit.Framework;
using Sdkgen.Client.Tests.Generated;

namespace Sdkgen.Client.Tests;

public class IntegrationTest
{
    [SetUp]
    public void SetUp()
    {
        Assert.IsTrue(this.PortIsOpen());
    }

    [Test]
    public void TestClientGetAll()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestResponse response = client.product().getAll(8, 16, "foobar");

        Assert.AreEqual("Bearer my_token", response.Headers["Authorization"]);
        Assert.AreEqual("application/json", response.Headers["Accept"]);
        Assert.AreEqual("SDKgen Client v1.0", response.Headers["User-Agent"]);
        Assert.AreEqual("GET", response.Method);
        Assert.AreEqual("8", response.Args["startIndex"]);
        Assert.AreEqual("16", response.Args["count"]);
        Assert.AreEqual("foobar", response.Args["search"]);
        Assert.IsNull(response.Json);
    }

    [Test]
    public void TestClientCreate()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestRequest payload = this.NewPayload();
        TestResponse response = client.product().create(payload);

        Assert.AreEqual("Bearer my_token", response.Headers["Authorization"]);
        Assert.AreEqual("application/json", response.Headers["Accept"]);
        Assert.AreEqual("SDKgen Client v1.0", response.Headers["User-Agent"]);
        Assert.AreEqual("POST", response.Method);
        Assert.AreEqual(0, response.Args.Count);
        //Assert.AreEqual(JsonSerializer.Serialize(payload), JsonSerializer.Serialize(response.Json));
    }

    [Test]
    public void TestClientUpdate()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestRequest payload = this.NewPayload();
        TestResponse response = client.product().update(1, payload);

        Assert.AreEqual("Bearer my_token", response.Headers["Authorization"]);
        Assert.AreEqual("application/json", response.Headers["Accept"]);
        Assert.AreEqual("SDKgen Client v1.0", response.Headers["User-Agent"]);
        Assert.AreEqual("PUT", response.Method);
        Assert.AreEqual(0, response.Args.Count);
        //Assert.AreEqual(JsonSerializer.Serialize(payload), JsonSerializer.Serialize(response.Json));
    }

    [Test]
    public void TestClientPatch()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestRequest payload = this.NewPayload();
        TestResponse response = client.product().patch(1, payload);

        Assert.AreEqual("Bearer my_token", response.Headers["Authorization"]);
        Assert.AreEqual("application/json", response.Headers["Accept"]);
        Assert.AreEqual("SDKgen Client v1.0", response.Headers["User-Agent"]);
        Assert.AreEqual("PATCH", response.Method);
        Assert.AreEqual(0, response.Args.Count);
        //Assert.AreEqual(JsonSerializer.Serialize(payload), JsonSerializer.Serialize(response.Json));
    }

    [Test]
    public void TestClientDelete()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestResponse response = client.product().delete(1);

        Assert.AreEqual("Bearer my_token", response.Headers["Authorization"]);
        Assert.AreEqual("application/json", response.Headers["Accept"]);
        Assert.AreEqual("SDKgen Client v1.0", response.Headers["User-Agent"]);
        Assert.AreEqual("DELETE", response.Method);
        Assert.AreEqual(0, response.Args.Count);
    }

    public TestRequest NewPayload() {

        TestObject objectFoo = new TestObject();
        objectFoo.Id = 1;
        objectFoo.Name = "foo";

        TestObject objectBar = new TestObject();
        objectBar.Id = 2;
        objectBar.Name = "bar";

        TestMapScalar mapScalar = new TestMapScalar();
        mapScalar.Add("foo", "bar");
        mapScalar.Add("bar", "foo");

        TestMapObject mapObject = new TestMapObject();
        mapObject.Add("foo", objectFoo);
        mapObject.Add("bar", objectBar);

        string[] arrayScalar = {"foo", "bar"};
        TestObject[] arrayObject = {objectFoo, objectBar};

        TestRequest payload = new TestRequest();
        payload.Int = 1337;
        payload.Float = (float) 13.37;
        payload.String = "foobar";
        payload.Bool = true;
        payload.ArrayScalar = arrayScalar;
        payload.ArrayObject = arrayObject;
        payload.MapScalar = mapScalar;
        payload.MapObject = mapObject;
        payload.Object = objectFoo;
        return payload;
    }

    private bool PortIsOpen() {
        try
        {
            TcpClient client = new TcpClient();
            IAsyncResult result = client.BeginConnect("127.0.0.1", 8081, null, null);
            client.EndConnect(result);
            return true;
        } catch {
            return false;
        }
    }
}
