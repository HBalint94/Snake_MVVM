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

                    // Kígyó fejének beolvasása
                                             
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');
                        Snake snake = new Snake(Int32.Parse(numbers[0]), Int32.Parse(numbers[1]));
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');
                        snake.DirectionX = Int32.Parse(numbers[0]);
                        snake.DirectionY = Int32.Parse(numbers[1]);


                    // Tábla létrehozása és a kígyó többi részének beolvasása
                    SnakeGameTable table = new SnakeGameTable(tableSize,gameScore);
                    


                        for (Int32 i = 0; i < tableSize; i++)
                        {
                            line = await reader.ReadLineAsync();
                            numbers = line.Split(' ');
                            for (Int32 j = 0; j < tableSize; j++)
                            {    
                                if(Int32.Parse(numbers[j])== 1)
                                {
                                snake.AddElementToSnake(new SnakeBodyPart(i, j));
                                }
                                table.SetValue(i, j, Int32.Parse(numbers[j]));
                            }
                        }

                    snake.StepTheTailOfTheSnake();
                    snake.DirectionX = 0;
                    snake.DirectionY = 1;
                    table.setSnake(snake);
                    table.Score = gameScore;
                    


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
                        await writer.WriteLineAsync(table.Size+ " " +table.Score); // kiírjuk a pályaméretet, kiírjuk az aktuális pontszámot
                        await writer.WriteLineAsync(snake.getHead()._posX + " "+snake.getHead()._posY); //melyik mező a kígyó feje
                        await writer.WriteLineAsync(snake.DirectionX + " " + snake.DirectionY); // Kiírom, hogy a kígyó épp milyen irányba készült haladni 
                        
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

