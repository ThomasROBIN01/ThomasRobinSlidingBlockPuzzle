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

                    // We are using here a counter to check in which tile we are:
                    // { 0, 1, 2 }
                    // { 3, 4, 5 }
                    // { 6, 7, 8 }

                    switch (gridCounter)
                    {
                        case 0:
                            TopLeft.Source = GetTileImageSource(tileNumber);
                            break;

                        case 1:
                            TopCentre.Source = GetTileImageSource(tileNumber);
                            break;

                        case 2:
                            TopRight.Source = GetTileImageSource(tileNumber);
                            break;

                        case 3:
                            MiddleLeft.Source = GetTileImageSource(tileNumber);
                            break;

                        case 4:
                            MiddleCentre.Source = GetTileImageSource(tileNumber);
                            break;

                        case 5:
                            MiddleRight.Source = GetTileImageSource(tileNumber);
                            break;

                        case 6:
                            BottomLeft.Source = GetTileImageSource(tileNumber);
                            break;

                        case 7:
                            BottomCentre.Source = GetTileImageSource(tileNumber);
                            break;

                        case 8:
                            BottomRight.Source = GetTileImageSource(tileNumber);
                            break;
                    }

                    gridCounter++;
                }
            }
        }

        private ImageSource GetTileImageSource (int tileNumber)
        {
            switch (tileNumber)
            {
                // { 0, 1, 2 }
                // { 3, 4, 5 }
                // { 6, 7, 8 }

                case 1:
                    return "top_middle.png";
                    break;

                case 2:
                    return "top_right.png";
                    break;

                case 3:
                    return "centre_left.png";
                    break;

                case 4:
                    return "centre_middle.png";
                    break;

                case 5:
                    return "centre_right.png";
                    break;

                case 6:
                    return "bottom_left.png";
                    break;

                case 7:
                    return "bottom_middle.png";
                    break;

                case 8:
                    return "bottom_right.png";
                    break;

                default:
                    return null;
            }
        }

        private void Shuffle(object sender, EventArgs e)
        {
            puzzle.ShufflePuzzle();
            UpdatePuzzle(puzzle);
        }
    }
}
