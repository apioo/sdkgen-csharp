/*
 * SDKgen is a powerful code generator to automatically build client SDKs for your REST API.
 * For the current version and information visit <https://sdkgen.app>
 *
 * Copyright (c) Christoph Kappestein <christoph.kappestein@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using RestSharp;

namespace Sdkgen.Client;

public class Multipart
{
    private Dictionary<string, Part> parts;

    public Multipart()
    {
        this.parts = new Dictionary<string, Part>();
    }

    public void Add(string name, string path, ContentType? contentType = null, FileParameterOptions? options = null)
    {
        this.parts.Add(name, new Part(path, contentType, options));
    }

    public void Add(string name, byte[] bytes, string fileName, ContentType? contentType = null, FileParameterOptions? options = null)
    {
        this.parts.Add(name, new Part(bytes, fileName, contentType, options));
    }

    public void Add(string name, Func<Stream> getFile, string fileName, ContentType? contentType = null, FileParameterOptions? options = null)
    {
        this.parts.Add(name, new Part(getFile, fileName, contentType, options));
    }

    public Dictionary<string, Part> GetParts()
    {
        return this.parts;
    }

    public class Part
    {
        public string? Path { get; set; }
        public byte[]? Bytes { get; set; }
        public Func<Stream>? GetFile { get; set; }
        public ContentType? ContentType { get; set; }
        public FileParameterOptions? Options { get; set; }
        public string? FileName { get; set; }

        public Part(string path, ContentType? contentType = null, FileParameterOptions? options = null)
        {
            this.Path = path;
            this.ContentType = contentType;
            this.Options = options;
        }

        public Part(byte[] bytes, string fileName, ContentType? contentType = null, FileParameterOptions? options = null)
        {
            this.Bytes = bytes;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.Options = options;
        }

        public Part(Func<Stream> getFile, string fileName, ContentType? contentType = null, FileParameterOptions? options = null)
        {
            this.GetFile = getFile;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.Options = options;
        }
    }
}
