using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    /// <summary>
    /// An experiment to measure performance implications of using Open Generics and dynamic execution of querying part presented here:
    /// https://cuttingedge.it/blogs/steven/pivot/entry.php?id=92
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");

            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("============================== Finished ==============================");
            Console.ReadKey();
        }

        private static void Run()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IQueryHandler<FirstQuery, FirstResult>, FirstQueryHandler>();
            serviceCollection.AddTransient<IQueryHandler<SecondQuery, SecondResult>, SecondQueryHandler>();
            serviceCollection.AddSingleton<IQueryProcessor, QueryProcessor>();

            var serviceProvider = serviceCollection.BuildServiceProvider();


            var processor = serviceProvider.GetRequiredService<IQueryProcessor>();

            const int iterationsCount = 10_000_000;

            var watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < iterationsCount; i++)
            {
                var result1 = processor.Process<FirstQuery, FirstResult>(new FirstQuery(11)).GetAwaiter().GetResult().Value;
                var result2 = processor.Process<SecondQuery, SecondResult>(new SecondQuery(777)).GetAwaiter().GetResult().Value;
            }

            watch.Stop();
            Console.WriteLine($"Static iterations run: {iterationsCount}; Time elapsed {watch.ElapsedMilliseconds} ms");

            watch.Reset();
            watch.Start();

            for (int i = 0; i < iterationsCount; i++)
            {
                var result1 = processor.Process(new FirstQuery(11)).GetAwaiter().GetResult().Value;
                var result2 = processor.Process(new SecondQuery(777)).GetAwaiter().GetResult().Value;
            }

            watch.Stop();
            Console.WriteLine($"Dynamic iterations run: {iterationsCount}; Time elapsed {watch.ElapsedMilliseconds} ms");

        }
    }


    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }

    public interface IQueryProcessor
    {
        Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }

    public class QueryProcessor : IQueryProcessor
    {
        private IServiceProvider ServiceProvider { get; }

        public QueryProcessor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return handler.HandleAsync(query);
        }

        public Task<TResult> Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = ServiceProvider.GetRequiredService(handlerType);
            return handler.HandleAsync((dynamic)query);
        }
    }

    public class FirstQuery : IQuery<FirstResult>
    {
        public FirstQuery(int param)
        {
            Param = param;
        }

        public int Param { get; }
    }

    public class FirstResult
    {
        public FirstResult(string value)
        {
            Value = value;
        }

        public string Value { get; }

    }

    public class FirstQueryHandler : IQueryHandler<FirstQuery, FirstResult>
    {
        public Task<FirstResult> HandleAsync(FirstQuery query)
        {
            return Task.FromResult(new FirstResult($"Query value of: {query.Param} + 42 = {query.Param + 42}"));
        }
    }

    public class SecondQuery : IQuery<SecondResult>
    {
        public SecondQuery(int param)
        {
            Param = param;
        }

        public int Param { get; }
    }

    public class SecondResult
    {
        public SecondResult(string value)
        {
            Value = value;
        }

        public string Value { get; }

    }

    public class SecondQueryHandler : IQueryHandler<SecondQuery, SecondResult>
    {
        public Task<SecondResult> HandleAsync(SecondQuery query)
        {
            return Task.FromResult(new SecondResult($"Query value of: {query.Param} + 11 = {query.Param + 11}"));
        }
    }
}
