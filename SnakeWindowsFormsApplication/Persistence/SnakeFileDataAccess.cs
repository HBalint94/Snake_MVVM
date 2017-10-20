using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWindowsFormsApplication.Persistence
{
    /// <summary>
    /// Snake fájlkezelő típusa.
    /// </summary>
    public class SnakeFileDataAccess : ISnakeDataAccess
    {
       
            /// <summary>
            /// Fájl betöltése.
            /// </summary>
            /// <param name="path">Elérési útvonal.</param>
            /// <returns>A fájlból beolvasott játéktábla.</returns>
            public async Task<SnakeGameTable> LoadAsync(String path)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                    {
                        String line = await reader.ReadLineAsync();
                        String[] numbers = line.Split(' '); // beolvasunk egy sort, és a szóköz mentén széttöredezzük
                        Int32 tableSize = Int32.Parse(numbers[0]); // beolvassuk a tábla méretét
                        Int32 gameScore = Int32.Parse(numbers[1]); // beolvassuk az aktuális pontszámot.

                    // Kígyó beolvasása
                        LinkedList<SnakeBodyPart> loadedSnake = new LinkedList<SnakeBodyPart>();
                        
                        for(Int32 i = 0; i < gameScore; i++)
                        {
                        line = await reader.ReadLineAsync();
                         numbers = line.Split(' ');
                        loadedSnake.AddFirst(new SnakeBodyPart(Int32.Parse(numbers[0]), Int32.Parse(numbers[1]), Int32.Parse(numbers[2]),Int32.Parse(numbers[3])));

                        }
                        Snake snake = new Snake(loadedSnake);
                     // Tábla létrehozása
                        SnakeGameTable table = new SnakeGameTable(tableSize,gameScore);


                        for (Int32 i = 0; i < tableSize; i++)
                        {
                            line = await reader.ReadLineAsync();
                            numbers = line.Split(' ');
                            for (Int32 j = 0; j < tableSize; j++)
                            {
                                table.SetValue(i, j, Int32.Parse(numbers[j]));
                            }
                        }
                        
                          
                        return table;
                    }
                }
                catch
                {
                    throw new SnakeDataException();
                }
            }

            /// <summary>
            /// Fájl mentése.
            /// </summary>
            /// <param name="path">Elérési útvonal.</param>
            /// <param name="table">A fájlba kiírandó játéktábla.</param>
            public async Task SaveAsync(String path, SnakeGameTable table,Snake snake)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                    {
                        writer.WriteAsync(table.Size+ " " +table.Score); // kiírjuk a pályaméretet, kiírjuk az aktuális pontszámot
                        
                    foreach (SnakeBodyPart val in snake.getSnake().Reverse())
                    {
                        writer.WriteAsync(val._posX + " " + val._posY + " " + val._directionX + " " + val._directionX);
                    }
                   
                         for (Int32 i = 0; i < table.Size; i++)
                        {
                            for (Int32 j = 0; j < table.Size; j++)
                            {
                                await writer.WriteAsync(table[i, j] + " "); // kiírjuk az értékeket
                            }
                            await writer.WriteLineAsync();
                        }
                    }
                }
                catch
                {
                    throw new SnakeDataException();
                }
            }
        }
    }

