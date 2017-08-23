using Akavache;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;


namespace FootballClient.DataAccess
{
    static class Utility
    {
        public static class Md5Calculator
        {
            public static string ComputeMd5(string strValue)
            {
                return Guid.NewGuid().ToString();
                //var algorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
                //var buffer = CryptographicBuffer.ConvertStringToBinary(strValue, BinaryStringEncoding.Utf8);
                //var hashed = algorithm.HashData(buffer);
                //return CryptographicBuffer.EncodeToHexString(hashed);
            }
        }
    }

    public static class AkavacheExtensions
    {
        //public static IObservable<T> GetAndUpdateObject<T> (this IBlobCache This, string key, Func<IObservable<T>> fetchFunc, DateTimeOffset? absoluteExpiration = null)
        //{
        //    var result = fetchFunc().
        //        Do(v => This.InsertObject(key, v, absoluteExpiration));

        //    return result;
        //}

        //public static IObservable<T> GetOrFetchAndUpdateLatest<T>(this IBlobCache This,
        //    string key,
        //    Func<IObservable<T>> fetchFunc,
        //    Func<DateTimeOffset, bool> fetchPredicate = null,
        //    bool shouldInvalidateOnError = false)
        //{
        //    var fetch = Observable.Defer(() => This.GetObjectCreatedAt<T>(key))
        //        .Select(x => fetchPredicate == null || x == null || fetchPredicate(x.Value))
        //        .Where(x => x != false)
        //        .SelectMany(_ =>
        //        {
        //            var fetchObs = fetchFunc().Catch<T, Exception>(ex =>
        //            {
        //                var shouldInvalidate = shouldInvalidateOnError ?
        //                    This.InvalidateObject<T>(key) :
        //                    Observable.Return(System.Reactive.Unit.Default);
        //                return shouldInvalidate.SelectMany(__ => Observable.Throw<T>(ex));
        //            });

        //            return fetchObs
        //                .SelectMany(x => This.InsertObject<T>(key, x).Select(__ => x));
        //        });

        //    var result = This.GetObject<T>(key).Select(x => new Tuple<T, bool>(x, true))
        //        .Catch(Observable.Return(new Tuple<T, bool>(default(T), false)));

        //    return result.SelectMany(x =>
        //    {
        //        return x.Item2 ?
        //            Observable.Return(x.Item1) :
        //            Observable.Empty<T>();
        //    }).Concat(fetch).Multicast(new ReplaySubject<T>()).RefCount();
        //}

        //public static IObservable<T> FetchOrGetObject<T>(this IBlobCache This, string key, Func<IObservable<T>> fetchFunc, DateTimeOffset? absoluteExpiration = null)
        //{
        //    var result = fetchFunc().
        //        Do(v => This.InsertObject(key, v, absoluteExpiration))
        //        .Catch<T, Exception>(ex =>
        //        {
        //            return This.GetObject<T>(key).Catch(Observable.Return(default(T)));
        //        });
        //
        //    
        //    return result;
        //}
    }
}
