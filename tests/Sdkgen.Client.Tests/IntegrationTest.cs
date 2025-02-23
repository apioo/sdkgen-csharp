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
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;
using Sdkgen.Client.Tests.Generated;
using System.Web;
using System.IO;

namespace Sdkgen.Client.Tests;

public class IntegrationTest
{
    [SetUp]
    public void SetUp()
    {
        Assert.That(this.PortIsOpen(), Is.True);
    }

    [Test]
    public async Task TestClientGetAll()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestResponse response = await client.Product().GetAll(8, 16, "foobar");

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("GET"));
        Assert.That(response.Args["startIndex"], Is.EqualTo("8"));
        Assert.That(response.Args["count"], Is.EqualTo("16"));
        Assert.That(response.Args["search"], Is.EqualTo("foobar"));
        Assert.That(response.Json, Is.Null);
    }

    [Test]
    public async Task TestClientCreate()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestRequest payload = this.NewPayload();
        TestResponse response = await client.Product().Create(payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Args.Count, Is.EqualTo(0));
        //Assert.AreEqual(JsonSerializer.Serialize(payload), JsonSerializer.Serialize(response.Json));
    }

    [Test]
    public async Task TestClientUpdate()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestRequest payload = this.NewPayload();
        TestResponse response = await client.Product().Update(1, payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("PUT"));
        Assert.That(response.Args.Count, Is.EqualTo(0));
        //Assert.AreEqual(JsonSerializer.Serialize(payload), JsonSerializer.Serialize(response.Json));
    }

    [Test]
    public async Task TestClientPatch()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestRequest payload = this.NewPayload();
        TestResponse response = await client.Product().Patch(1, payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("PATCH"));
        Assert.That(response.Args.Count, Is.EqualTo(0));
        //Assert.AreEqual(JsonSerializer.Serialize(payload), JsonSerializer.Serialize(response.Json));
    }

    [Test]
    public async Task TestClientDelete()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestResponse response = await client.Product().Delete(1);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("DELETE"));
        Assert.That(response.Args.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task TestClientBinary()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        var payload = new byte[] {0x66, 0x6F, 0x6F, 0x62, 0x61, 0x72};

        TestResponse response = await client.Product().Binary(payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Data, Is.EqualTo("foobar"));
    }

    [Test]
    public async Task TestClientForm()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        var payload = new System.Collections.Specialized.NameValueCollection
        {
            { "foo", "bar" }
        };

        TestResponse response = await client.Product().Form(payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Form["foo"], Is.EqualTo("bar"));
    }

    [Test]
    public async Task TestClientJson()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        var payload = new Dictionary<string, string>
        {
            { "string", "bar" }
        };

        TestResponse response = await client.Product().Json(payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Json.String, Is.EqualTo("bar"));
    }

    [Test]
    public async Task TestClientMultipart()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        var payload = new Multipart();
        payload.Add("foo", new byte[] {0x66, 0x6F, 0x6F, 0x62, 0x61, 0x72}, "upload.txt");

        TestResponse response = await client.Product().Multipart(payload);

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Files["foo"], Is.EqualTo("foobar"));
    }

    [Test]
    public async Task TestClientText()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestResponse response = await client.Product().Text("foobar");

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Data, Is.EqualTo("foobar"));
    }

    [Test]
    public async Task TestClientXml()
    {
        Generated.Client client = Generated.Client.Build("my_token");

        TestResponse response = await client.Product().Text("<foo>bar</foo>");

        Assert.That(response.Headers["Authorization"], Is.EqualTo("Bearer my_token"));
        Assert.That(response.Headers["Accept"], Is.EqualTo("application/json"));
        Assert.That(response.Headers["User-Agent"], Is.EqualTo("SDKgen/0.1.0"));
        Assert.That(response.Method, Is.EqualTo("POST"));
        Assert.That(response.Data, Is.EqualTo("<foo>bar</foo>"));
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

        List<string> arrayScalar = new List<string>();
        arrayScalar.Add("foo");
        arrayScalar.Add("bar");

        List<TestObject> arrayObject = new List<TestObject>();
        arrayObject.Add(objectFoo);
        arrayObject.Add(objectBar);

        TestRequest payload = new TestRequest();
        payload.Int = 1337;
        payload.Float = (float) 13.37;
        payload.String = "foobar";
        payload.Bool = true;
        payload.DateString = new DateOnly(2024, 9, 22);
        payload.DateTimeString = new DateTime(2024, 9, 22, 10, 9, 0);
        payload.TimeString = new TimeOnly(10, 9, 0);
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
