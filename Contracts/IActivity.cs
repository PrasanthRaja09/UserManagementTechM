using Entities.DTO;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IActivity : IRepositoryBase<Audit_logs>
    {
        int InsertLog(Audit_logs activity);
    }
}
