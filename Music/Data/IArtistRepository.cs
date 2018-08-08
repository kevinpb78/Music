using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Data
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAll();

        Artist GetById(Guid id);

        void Create(Artist entity);

        void Update(Artist entity);

        void Delete(Artist entity);
    }
}
