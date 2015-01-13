namespace ChildTracker.Data
{
    using ChildTracker.Data.Contracts;
    using ChildTracker.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChildTrackerData : IChildTrackerData
    {
        private DbContext context;
        private Dictionary<Type, object> repositories;
             
        public ChildTrackerData(ApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Geolocation> Locations
        {
            get { return this.GetRepository<Geolocation>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }


        public IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
