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

        public DataModel ()
        {
            // Initialise the puzzle with an independent copy of the solved configuration.         
            puzzleTiles = puzzleCompleted;

            // The empty tile initially occupies the top-left position at 0,0:
            emptyRow = 0;
            emptyColumn = 0;
        }

        public bool IsPuzzleComplete (int[,] puzzle)
        {
            bool isPuzzleComplete = false;

            if (puzzle == puzzleCompleted)
            {
                isPuzzleComplete = true;
            }

            return isPuzzleComplete;
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
            // Manhattan distance is the sum of the absolute differences between their row and column coordinates,
            // representing the distance when moving only horizontally or vertically (no diagonal movement).
            // A distance of 1 means the tiles are directly adjacent and can be swapped.

            int rowGapWith0 = Math.Abs(selectedRow - emptyRow);
            int columnGapWith0 = Math.Abs(selectedColumn - emptyColumn);

            int manhattanDistance = rowGapWith0 * columnGapWith0;


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

    }
}
