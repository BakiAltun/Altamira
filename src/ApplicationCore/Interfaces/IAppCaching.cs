using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vimo.ApplicationCore.Interfaces
{
    public interface IAppCaching
    {
        bool Set<T>(T model, int id = 0) where T : class, new();
        T Get<T>(int id = 0) where T : class, new();
        T Get<T>(int id = 0, Func<Task<T>> getFromSource = null) where T : class, new();
        IList<T> Get<T>() where T : class, new();
        void Delete<T>(int id = 0) where T : class, new();

    }
}