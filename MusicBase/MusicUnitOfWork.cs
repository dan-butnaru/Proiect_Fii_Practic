using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase
{
    public class MusicUnitOfWork : IDisposable, IMusicUnitOfWork
    {
        private bool disposedValue;
        private readonly MusicContext musicContext;

        public MusicUnitOfWork(MusicContext musicContext)
        {
            this.musicContext = musicContext;
        }

        public void SaveChanges()
        {
            musicContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    musicContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
