using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWindowsFormsApplication.Persistence
{
    /// <summary>
    /// Snake fájl kezelő felülete.
    /// </summary>
    public interface ISnakeDataAccess
    {
            /// <summary>
            /// Fájl betöltése.
            /// </summary>
            /// <param name="path">Elérési útvonal.</param>
            /// <returns>A fájlból beolvasott játéktábla.</returns>
            Task<SnakeGameTable> LoadAsync(String path);

            /// <summary>
            /// Fájl mentése.
            /// </summary>
            /// <param name="fileName">Elérési útvonal.</param>
            /// <param name="path">A fájlba kiírandó játéktábla.</param>
            Task SaveAsync(String path, SnakeGameTable table,Snake snake);



    }
}
