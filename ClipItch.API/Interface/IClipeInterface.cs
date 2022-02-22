using System.Collections.Generic;
using System.Threading.Tasks;
using ClipItch.API.Models;
using Refit;

namespace ClipItch.API.Interface
{
    public interface IClipeInterface
    {
        [Get("/helix/clips")]
         Task<IEnumerable<Clipe>> GetClipes();
    }
}