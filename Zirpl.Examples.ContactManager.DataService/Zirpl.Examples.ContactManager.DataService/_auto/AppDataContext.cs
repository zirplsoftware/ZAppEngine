using System;
using System.Data.Entity;

namespace Zirpl.Examples.ContactManager.DataService
{
    public partial class AppDataContext : Zirpl.Examples.ContactManager.DataService.DataContextBase
    {
        public DbSet<Zirpl.Examples.ContactManager.Model.Common.Tag> Tags { get; set; }
        public DbSet<Zirpl.Examples.ContactManager.Model.Project> Projects { get; set; }
        public DbSet<Zirpl.Examples.ContactManager.Model.ProjectImage> ProjectImages { get; set; }
        public DbSet<Zirpl.Examples.ContactManager.Model.ProjectUrl> ProjectUrls { get; set; }

        protected override bool IsModifiable(Object obj)
        {
            if (obj is Zirpl.Examples.ContactManager.Model.Project) return false;
            return true;
        }
    }
}
