using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApi.Core
{
    public interface IUnitOfWork
    {

        IContactRepository contacts { get; }

        Task<bool> Done();
    }
}
