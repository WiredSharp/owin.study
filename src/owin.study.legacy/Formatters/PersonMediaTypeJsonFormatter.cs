using Owin.Study.Legacy.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Owin.Study.Legacy.Formatters
{
    internal class PersonMediaTypeJsonFormatter : JsonMediaTypeFormatter
    {
        public override bool CanWriteType(Type type)
        {
            return type == typeof(Person);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
        {
            return WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var serializer = new JsonSerializer() { ContractResolver = ShouldSerializeContractResolver.Instance };
            using (var writer = new StreamWriter(writeStream, Encoding.UTF8))
            {
                serializer.Serialize(writer, value);
            }
            return Task.CompletedTask;
            //return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
        {
            var serializer = new JsonSerializer() { ContractResolver = ShouldSerializeContractResolver.Instance };
            using (var writer = new StreamWriter(writeStream, effectiveEncoding))
            {
                serializer.Serialize(writer, value);
            }
            //base.WriteToStream(type, value, writeStream, effectiveEncoding);
        }
    }

    internal class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType == typeof(Person) && property.PropertyName == "LastName")
            {
                property.ShouldSerialize = instance => false;
            }
            return property;
        }
    }
}
