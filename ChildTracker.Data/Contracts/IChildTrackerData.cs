namespace ChildTracker.Data.Contracts
{
    using ChildTracker.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IChildTrackerData
    {
        IRepository<Geolocation> Locations { get; }

        IRepository<T> GetRepository<T>() where T : class;

        int SaveChanges();
    }
}
