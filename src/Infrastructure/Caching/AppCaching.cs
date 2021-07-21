using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServiceStack.Redis;
using Vimo.ApplicationCore.Interfaces;

namespace Vimo.Infrastructure.Caching
{
    public class AppCaching : IAppCaching
    {
        private readonly string _connectionString;

        public AppCaching(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Set<T>(T model, int id = 0) where T : class, new()
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                return client.Set(typeof(T).Name + "-" + id, Serialize(model));
            }
        }

        public T Get<T>(int id = 0) where T : class, new()
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                return Deserialize<T>(client.Get<string>(typeof(T).Name + "-" + id));
            }
        }
        public T Get<T>(int id, Func<Task<T>> getFromSource = null) where T : class, new()
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                if (client.ContainsKey(typeof(T).Name + "-" + id))
                    return Deserialize<T>(client.Get<string>(typeof(T).Name + "-" + id));

                var sourceData = getFromSource.Invoke().GetAwaiter().GetResult();
                Set(sourceData, id);
                return sourceData;

            }
        }
        public IList<T> Get<T>() where T : class, new()
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                string keyPrefix = typeof(T).Name + "-";
                var allKeys = client.GetAllKeys().Where(x => x.StartsWith(keyPrefix));
                return client.GetAll<string>(allKeys).Select(x => Deserialize<T>(x.Value)).ToList();
            }
        }

        public void Delete<T>(int id = 0) where T : class, new()
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                client.Remove(typeof(T).Name + "-" + id);
            }
        }

        private string Serialize<T>(T model)
        {

            return JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new IncludePrivateStateContractResolver()

            });

        }
        private T Deserialize<T>(string json)
        {

            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ContractResolver = new IncludePrivateStateContractResolver()
            });
        }
    }
}


public class IncludePrivateStateContractResolver : DefaultContractResolver
{
    protected override List<MemberInfo> GetSerializableMembers(Type objectType)
    {
        const BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        var properties = objectType.GetProperties(BindingFlags);//.Where(p => p.HasSetter() && p.HasGetter());
        var fields = objectType.GetFields(BindingFlags);

        var allMembers = properties.Cast<MemberInfo>().Union(fields);
        return allMembers.ToList();
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);

        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            if (property != null)
            {
                prop.Writable = property.CanWrite;
            }
            else
            {
                var field = member as FieldInfo;
                if (field != null)
                {
                    prop.Writable = true;
                }
            }
        }

        if (!prop.Readable)
        {
            var field = member as FieldInfo;
            if (field != null)
            {
                prop.Readable = true;
            }
        }

        return prop;
    }
}
