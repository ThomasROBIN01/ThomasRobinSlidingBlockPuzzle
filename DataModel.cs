using System.Diagnostics;


namespace ThomasRobinSlidingBlockPuzzle
{
    internal class DataModel
    {
        // Creates a 2 dimensional array ([,]), that stores the current positions of the puzzle tiles.
        private int[,] puzzleTiles;

        // Tracks the current position of the empty tile (0).
        private int emptyRow;
        private int emptyColumn;

        // Stores the solved puzzle configuration for comparison.
        // 0 represents the empty tile.
        private int[,] puzzleCompleted =
        {
            {0, 1, 2},
            {3, 4, 5},
            {6, 7, 8},
        };

        // Contructor:
        public DataModel ()
        {
            // Initialise the puzzle with an independent copy of the solved configuration:
            // puzzleTiles = puzzleCompleted;       // This doesn't create a new array, instead, both variables reference the same array in memory.
            puzzleTiles = (int[,])puzzleCompleted.Clone();      // Need to clone the puzzleCompleted, as the above doesn't work
                                                                // The cast (int[,]) is necessary because Clone() returns an object, and we want to assign it to an int[,].

            // The empty tile initially occupies the top-left position at 0,0:
            emptyRow = 0;
            emptyColumn = 0;
        }

        public bool IsPuzzleComplete ()
        {
            // bool isPuzzleComplete = false;

            //if (puzzle == puzzleCompleted)      // Arrays cannot be compared by their contents using '==', as this checks
            //                                    // whether they reference the same object in memory.
            //                                    // Therefore, each element must be compared individually.
            //{
            //    isPuzzleComplete = true;
            //}


            // Compare each tile's position with the completed puzzle configuration.
            // If any tile is in the wrong position, this means the puzzle is not complete.

            // Iterate through each row...
            for (int row = 0; row < 3; row++)
            {
                // ... Iterate through each column within the current row.
                for (int column = 0; column < 3; column++)
                {
                    if (puzzleTiles[row, column] != puzzleCompleted[row, column])
                    {
                        // If a mismatch is found, the puzzle is not complete.
                        // Return immediately, as there is no need to check the remaining tiles.
                        return false;
                    }
                }
            }

            // Will get here only if all tiles are the same as in the solved puzzle
            return true;
        }

        public int GetTile (int selectedRow, int selectedColumn)
        {
            int tile;

            tile = puzzleTiles[selectedRow, selectedColumn];

            return tile;
        }

        public void MoveTile (int selectedRow, int selectedColumn)
        {
            // Calculate the 'Manhattan distance' between the selected tile and the empty tile.
            int manhattanDistance = CalculateManhattanDistance(selectedRow, selectedColumn);

            if (manhattanDistance == 1)      // the selected tile is adjacent to the empty tile
            {
                int selectedTile = GetTile (selectedRow, selectedColumn);

                // Swap the selected tile with the empty position.
                puzzleTiles[selectedRow, selectedColumn] = 0;

                puzzleTiles[emptyRow, emptyColumn] = selectedTile;

                // Update the recorded position of the empty tile.
                emptyRow = selectedRow;
                emptyColumn = selectedColumn;
            }
            else
            {
                Debug.WriteLine("This tile is not adjacent to the empty tile and cannot be moved.");
            }
        }

        public void ShufflePuzzle ()
        {
            // Reset the puzzle to its solved configuration.
            puzzleTiles = (int[,])puzzleCompleted.Clone();      // same as in the constructor

            // Reset the empty tile's position to the top-left corner.
            emptyRow = 0;
            emptyColumn = 0;


            // Randomly choose between 20 and 40 shuffle moves.
            int randomIterations = Random.Shared.Next(20, 41);

            // Store the previous empty position to prevent immediately reversing a move.
            // Initialise to (-1, -1), so no valid position is excluded on the first move.
            int previousTurnRow = -1;
            int previousTurnColumn = -1;


            // Keep shuffling until the puzzle is no longer in its solved configuration.
            while (IsPuzzleComplete())
            {
                // Shuffle the puzzle for a number of iterations = randomIterations
                for (int i = 0; i < randomIterations; i++)
                {
                    // Check what are the possible adjacent tiles where the empty tile (0) could move and count them

                    // Store the coordinates of all valid adjacent tiles as a list of tuples:
                    List<(int row, int column)> adjacentTiles = new();


                    // Iterate through each row...
                    for (int row = 0; row < 3; row++)
                    {
                        // ... Iterate through each column within the current row.
                        for (int column = 0; column < 3; column++)
                        {
                            // Calculate the 'Manhattan distance' between the selected tile and the empty tile.
                            int manhattanDistance = CalculateManhattanDistance(row, column); ;

                            // Only include tiles adjacent to the empty position,
                            // excluding the previous empty position to prevent immediate reversals.
                            if (manhattanDistance == 1 && (row != previousTurnRow || column != previousTurnColumn))      // manhattanDistance == 1: adjacent to the empty tile
                            {
                                adjacentTiles.Add((row, column));
                            }
                        }
                    }

                    // Select a random position among the adjacent tiles available:
                    int randomAdjacentTile = Random.Shared.Next(0, adjacentTiles.Count);

                    // Save the current empty position before moving it:
                    previousTurnRow = emptyRow;
                    previousTurnColumn = emptyColumn;

                    // Move the selected tile into the empty position:
                    MoveTile(adjacentTiles[randomAdjacentTile].row, adjacentTiles[randomAdjacentTile].column);
                }
            }
        }

        private int CalculateManhattanDistance(int row, int column)
        {
            // Calculate the 'Manhattan distance' between the selected tile and the empty tile.
            // Manhattan distance is the sum of the absolute differences between their row and column coordinates,
            // representing the distance when moving only horizontally or vertically (no diagonal movement).
            // A distance of 1 means the tiles are directly adjacent and can be swapped.

            int rowGapWith0 = Math.Abs(row - emptyRow);
            int columnGapWith0 = Math.Abs(column - emptyColumn);

            int manhattanDistance = rowGapWith0 + columnGapWith0;

            return manhattanDistance;
        }

    }
}
