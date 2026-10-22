namespace ThomasRobinSlidingBlockPuzzle
{
    public partial class MainPage : ContentPage
    {
        private DataModel puzzle;

        public MainPage()
        {
            InitializeComponent();

            puzzle = new DataModel();

            UpdatePuzzle(puzzle);
        }

        private void UpdatePuzzle(DataModel puzzle)
        {
            int gridCounter = 0;

            // Iterate through each row...
            for (int row = 0; row < 3; row++)
            {
                // ... Iterate through each column within the current row.
                for (int column = 0; column < 3; column++)
                {
                    int tileNumber = puzzle.GetTile(row, column);

                    switch (gridCounter)
                    {
                        case 0:

                            break;

                    }

                    gridCounter++;
                }
            }
        }

        private void ChangeTileImage (int tileNumber)
        {
            switch (tileNumber)
            {
                case 0:
                    break;
            }
        }
    }
}
