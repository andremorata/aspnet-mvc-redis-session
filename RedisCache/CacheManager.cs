using ServiceStack.Redis;
using System;

namespace RedisCache
{
    public class CacheManager
    {
        private static IRedisClient cache;
        private static readonly string host = "127.0.0.1";
        private static readonly int port = 6379;
        private static readonly string password = "qdy4V3gxS!usjA=";
        private static readonly bool useSsl = false;

        private static IRedisClient Cache
        {
            get
            {
                if (cache == null)
                    cache = GetConnection();
                return cache;

            }
        }

        private static IRedisClient GetConnection()
        {
            var redisConfig = new RedisEndpoint
            {
                Host = host,
                Ssl = useSsl,
                Port = port,
                Password = password
            };

            RedisClient client = new RedisClient(redisConfig);

            return client;
        }

        public static bool Set<T>(string key, T value, DateTime? expireAt = null)
        {
            if (expireAt.HasValue)
                return Cache.Set<T>(key, value, expireAt.Value);

            return Cache.Set<T>(key, value);
        }

        public static T Get<T>(string key)
        {
            return Cache.Get<T>(key);
        }

        public static bool Contains(string key)
        {
            try
            {
                return Cache.ContainsKey(key);
            }
            catch
            {
                return false;
            }
        }

        public static bool Invalidate(string key)
        {
            try
            {
                return Cache.Remove(key);
            }
            catch
            {
                return false;
            }
        }

        public static void Expire(string key, DateTime expireAt)
        {
            Cache.ExpireEntryAt(key, expireAt);
        }
    }
}