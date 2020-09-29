using Contracts;
using Entities;
using Entities.DTO;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    class ActivityRepository : RepositoryBase<Audit_logs>, IActivity
    {
        public ActivityRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public int InsertLog(Audit_logs _audit)
        {
            Create(_audit);
            Save();
            return FindAll().OrderByDescending(a => a.uid).Take(1).FirstOrDefault().uid;
        }
    }
}
