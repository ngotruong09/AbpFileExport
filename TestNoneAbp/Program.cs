using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestNoneAbp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var collections = new ServiceCollection();
            collections.BuildServiceProvider();

        }
    }
}
