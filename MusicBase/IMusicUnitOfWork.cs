using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBase.Repositories;

namespace MusicBase
{
    public interface IMusicUnitOfWork
    {
        void SaveChanges();
    }
}
