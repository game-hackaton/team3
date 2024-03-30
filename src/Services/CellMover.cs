using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using thegame.Models;

namespace thegame.Services
{
    public class CellMover
    {
        public static CellDto[] MoveLeft(Guid gameId)
        {
            var game = GamesRepository.Games[gameId];
            var field = TryMoveLeft(game.GameCells.Values);

            field = TryJoinLeft(field.Where(x => x.ZIndex > 0).OrderByDescending(x => x.Pos.X).ToList());

            return TryMoveLeft(field).ToArray();

        }

        public static List<Models.CellDto> TryMoveLeft(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderBy(x => x.Pos.X).ToList();

            for (var i = 0; i < field.Count; i++)
            {
                while (true)
                {
                    if (field[i].Pos.X - 1 >= 0 && !field.Any(x => x.Pos.Y == field[i].Pos.Y && x.Pos.X == field[i].Pos.X - 1))
                    {
                        field[i].Pos.X -= 1;
                    }
                    else { break; }
                }
            }

            return field;
        }

        public static List<Models.CellDto> TryJoinLeft(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderByDescending(x => x.Pos.X).ToList();
            var usedCelld = new List<Tuple<int, int>>();
            for (var i = 0; i < field.Count; i++)
            {
                if (field.Any(x => x.Pos.Y == field[i].Pos.Y && x.Pos.X == field[i].Pos.X - 1))
                {
                    var conflictCell = field.Find(x => x.Pos.Y == field[i].Pos.Y && x.Pos.X == field[i].Pos.X - 1);
                    if (conflictCell.Content == field[i].Content && !usedCelld.Contains(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y)))
                    {
                        conflictCell.Content = (int.Parse(conflictCell.Content) * 2).ToString();
                        conflictCell.Type = $"tile-{(int.Parse(conflictCell.Content) * 2)}";
                        usedCelld.Add(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y));
                        field.RemoveAt(i);
                    }
                }
            }

            return field;
        }

        public static CellDto[] MoveRight(Guid gameId)
        {
            var game = GamesRepository.Games[gameId];
            var field = TryMoveRight(game.GameCells.Values);

            field = TryJoinRight(field.Where(x => x.ZIndex > 0).OrderBy(x => x.Pos.X).ToList());

            return TryMoveRight(field).ToArray();

        }

        public static List<Models.CellDto> TryMoveRight(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderByDescending(x => x.Pos.X).ToList();

            for (var i = 0; i < field.Count; i++)
            {
                while (true)
                {
                    if (field[i].Pos.X + 1 < 4 && !field.Any(x => x.Pos.Y == field[i].Pos.Y && x.Pos.X == field[i].Pos.X + 1))
                    {
                        field[i].Pos.X += 1;
                    }
                    else { break; }
                }
            }

            return field;
        }

        public static List<Models.CellDto> TryJoinRight(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderBy(x => x.Pos.X).ToList();
            var usedCelld = new List<Tuple<int, int>>();

            for (var i = 0; i < field.Count; i++)
            {
                if (field.Any(x => x.Pos.Y == field[i].Pos.Y && x.Pos.X == field[i].Pos.X + 1))
                {
                    var conflictCell = field.Find(x => x.Pos.Y == field[i].Pos.Y && x.Pos.X == field[i].Pos.X + 1);
                    if (conflictCell.Content == field[i].Content && !usedCelld.Contains(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y)))
                    {
                        conflictCell.Content = (int.Parse(conflictCell.Content) * 2).ToString();
                        conflictCell.Type = $"tile-{(int.Parse(conflictCell.Content) * 2)}";
                        usedCelld.Add(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y));
                        field.RemoveAt(i);
                    }
                }
            }

            return field;
        }

        public static CellDto[] MoveDown(Guid gameId)
        {
            var game = GamesRepository.Games[gameId];
            var field = TryMoveDown(game.GameCells.Values);

            field = TryJoinUp(field.Where(x => x.ZIndex > 0).OrderBy(x => x.Pos.Y).ToList());

            return TryMoveDown(field).ToArray();

        }

        public static List<Models.CellDto> TryMoveDown(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderByDescending(x => x.Pos.Y).ToList();

            for (var i = 0; i < field.Count; i++)
            {
                while (true)
                {
                    if (field[i].Pos.Y - 1 >= 0 && !field.Any(x => x.Pos.Y == field[i].Pos.Y - 1 && x.Pos.X == field[i].Pos.X))
                    {
                        field[i].Pos.Y -= 1;
                    }
                    else { break; }
                }
            }

            return field;
        }

        public static List<Models.CellDto> TryJoinDown(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderBy(x => x.Pos.Y).ToList();
            var usedCelld = new List<Tuple<int, int>>();

            for (var i = 0; i < field.Count; i++)
            {
                if (field.Any(x => x.Pos.Y == field[i].Pos.Y - 1 && x.Pos.X == field[i].Pos.X))
                {
                    var conflictCell = field.Find(x => x.Pos.Y == field[i].Pos.Y - 1 && x.Pos.X == field[i].Pos.X);
                    if (conflictCell.Content == field[i].Content && !usedCelld.Contains(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y)))
                    {
                        conflictCell.Content = (int.Parse(conflictCell.Content) * 2).ToString();
                        conflictCell.Type = $"tile-{(int.Parse(conflictCell.Content) * 2)}";
                        usedCelld.Add(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y));
                        field.RemoveAt(i);
                    }
                }
            }

            return field;
        }

        public static CellDto[] MoveUp(Guid gameId)
        {
            var game = GamesRepository.Games[gameId];
            var field = TryMoveUp(game.GameCells.Values);

            field = TryJoinUp(field.Where(x => x.ZIndex > 0).OrderByDescending(x => x.Pos.Y).ToList());

            return TryMoveUp(field).ToArray();

        }

        public static List<Models.CellDto> TryMoveUp(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderBy(x => x.Pos.Y).ToList();

            for (var i = 0; i < field.Count; i++)
            {
                while (true)
                {
                    if (field[i].Pos.Y + 1 < 4 && !field.Any(x => x.Pos.Y == field[i].Pos.Y + 1 && x.Pos.X == field[i].Pos.X))
                    {
                        field[i].Pos.Y += 1;
                    }
                    else { break; }
                }
            }

            return field;
        }


        public static List<Models.CellDto> TryJoinUp(IEnumerable<Models.CellDto> gameCells)
        {
            var field = gameCells.Where(x => x.ZIndex > 0).OrderByDescending(x => x.Pos.Y).ToList();
            var usedCelld = new List<Tuple<int, int>>();

            for (var i = 0; i < field.Count; i++)
            {
                if (field.Any(x => x.Pos.Y == field[i].Pos.Y + 1 && x.Pos.X == field[i].Pos.X))
                {
                    var conflictCell = field.Find(x => x.Pos.Y == field[i].Pos.Y + 1 && x.Pos.X == field[i].Pos.X);
                    if (conflictCell.Content == field[i].Content && !usedCelld.Contains(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y)))
                    {
                        conflictCell.Content = (int.Parse(conflictCell.Content) * 2).ToString();
                        conflictCell.Type = $"tile-{(int.Parse(conflictCell.Content) * 2)}";
                        usedCelld.Add(new Tuple<int, int>(conflictCell.Pos.X, conflictCell.Pos.Y));
                        field.RemoveAt(i);
                    }
                }
            }

            return field;
        }
    }
}