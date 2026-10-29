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
            // We are using a counter to check in which tile we are, starting in tile 0:
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

                    // Increment to next tile
                    gridCounter++;
                }
            }
        }

        private ImageSource GetTileImageSource(int tileNumber)
        {
            switch (tileNumber)
            {
                // Reference to visualise where each tile normally is 
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

        private void ImageSliding (object sender, PanUpdatedEventArgs e)        // PanUpdatedEventArgs provides the information specific to a pan gesture,
                                                                                // including the total movement along both axes (e.TotalY and e.TotalX).
        {
            // Cast the sender as an image as the function is only linked to images
            Image tileImage = (Image)sender;

            // Get the row and column of the selected tile:
            int selectedRow = Grid.GetRow(tileImage);
            int selectedColumn = Grid.GetColumn(tileImage);

            // Use the CalculateManhattanDistance function to check if the selected image is adjacent to the empty tile.
            if (puzzle.CalculateManhattanDistance(selectedRow, selectedColumn) == 1)
            {
                // Calculate the direction for the selected tile to move
                int rowDirection = puzzle.EmptyRow - selectedRow;           // >0 : the selected tile has to move down   /   <0: the selected tile has to move up
                int columnDirection = puzzle.EmptyColumn - selectedColumn;  // >0 : the selected tile has to move right   /   <0: the selected tile has to left

                // if rowDirection = - 1, the empty tile is above the selected tile, so the selected tile can slide up.
                if (rowDirection == - 1)
                {
                    tileImage.TranslationY = Math.Clamp(e.TotalY, (- tileImage.Height - PuzzleGrid.ColumnSpacing), 0);      // Math.Clamp(value, minimum, maximum) ensures that the value stays within the specified range.

                    if (tileImage.TranslationY < 0.75 * (-tileImage.Height - PuzzleGrid.ColumnSpacing))
                    {
                        puzzle.MoveTile(selectedRow, selectedColumn);
                        UpdatePuzzle(puzzle);
                    }
                }
                // if rowDirection = 1, the empty tile is below the selected tile, so the selected tile can slide down.
                else if (rowDirection == 1)
                {                    
                    tileImage.TranslationY = Math.Clamp(e.TotalY, 0, (tileImage.Height + PuzzleGrid.ColumnSpacing));    // Math.Clamp(value, minimum, maximum) ensures that the value stays within the specified range.

                    if (tileImage.TranslationY > 0.75 * (tileImage.Height + PuzzleGrid.ColumnSpacing))
                    {
                        puzzle.MoveTile(selectedRow, selectedColumn);
                        UpdatePuzzle(puzzle);
                    }
                }
                // if rowDirection = 0 or 2/-2, no vertical movement is allowed.
                else
                {
                    tileImage.TranslationY = 0;
                }


                // if columnDirection = -1, the empty tile is to the left of the selected tile, so the selected tile can slide left.
                if (columnDirection == -1)
                {                    
                    tileImage.TranslationX = Math.Clamp(e.TotalX, (-tileImage.Width - PuzzleGrid.RowSpacing), 0);   // Math.Clamp(value, minimum, maximum) ensures that the value stays within the specified range.

                    if (tileImage.TranslationX < 0.75 * (-tileImage.Width - PuzzleGrid.RowSpacing))
                    {
                        puzzle.MoveTile(selectedRow, selectedColumn);
                        UpdatePuzzle(puzzle);
                    }
                }
                // if columnDirection = 1, the empty tile is to the right of the selected tile, so the selected tile can slide right.
                else if (columnDirection == 1)
                {
                    tileImage.TranslationX = Math.Clamp(e.TotalX, 0, (tileImage.Width + PuzzleGrid.RowSpacing));    // Math.Clamp(value, minimum, maximum) ensures that the value stays within the specified range.


                    if (tileImage.TranslationX > 0.75 * (tileImage.Width + PuzzleGrid.RowSpacing))
                    {
                        puzzle.MoveTile(selectedRow, selectedColumn);
                        UpdatePuzzle(puzzle);
                    }
                }
                // if columnDirection = 0 or 2/-2, no horizontal movement is allowed.
                else
                {
                    tileImage.TranslationX = 0;
                }

                // if >0 => allow to slide to the left
                // if <0 => slide to the right
                // calculate selectedColumn - emptyColumn
                // if >0 => allow to slide to the bottom
                // if <0 => slide up
            }

            // if the image is not adjacent to the empty tile, it cannot be moved / slided
            else
            {

            }
        }
        // Add a sliding function on each image which will do:

        
            // if not, the image cannot be moved / slided
            // if it is adjacent:


        // for the sliding: allow to slide until the end of the next image (ditance width or height // should be the same as they're square)
        // Only launch the MoveTile function if the slided image has been released and if it is like more than 50% (or more) on the next image
        // then launch MoveTile function followed by Update, and followed by IsPuzzleComplete check

        // Need also a Win function, that will use the IsPuzzleComplete function to check when the puzzle is complete after each move, and will display a win message, probably on top of the UI.
    }
}
