using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ThomasRobinSlidingBlockPuzzle
{
    internal class DataModel
    {
        // Creates a 2 dimensional array ([,]).
        private int[,] puzzleTiles;
        
        // Save the completed puzzle in a separate variable to be able to modify and compare the puzzle with the final state.
        private int[,] puzzleCompleted =
        {
            {0, 1, 2},
            {3, 4, 5},
            {6, 7, 8},
        };

        public DataModel ()
        {
            // Fills the 2 dimensions puzzlesTiles with 9 positions.
            // 0 represents the empty tile.
            puzzleTiles = puzzleCompleted;
        }

        public bool isPuzzleComplete (int[,] puzzle)
        {
            bool isPuzzleComplete = false;

            if (puzzle == puzzleCompleted)
            {
                isPuzzleComplete = true;
            }

            return isPuzzleComplete;
        }

        public int GetTile (int row, int column)
        {
            int tile;

            tile = puzzleTiles[row, column];

            return tile;
        }

    }
}
