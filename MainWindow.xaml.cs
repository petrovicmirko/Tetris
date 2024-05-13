using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/bezbojna-01.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/III.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LLL.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/JJJ.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/OOO.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/SSS.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TTT.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/ZZZ.png", UriKind.Relative))
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-III.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-LLL.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-JJJ.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-OOO.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-SSS.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-TTT.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-ZZZ.png", UriKind.Relative))
        };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 200;
        private readonly int delayDecrease = 25;

        private int highScore { get; set; }

        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);


        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for(int r = 0; r < grid.Rows; r++)
            {
                for(int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for(int r = 0; r < grid.Rows; r++)
            {
                for(int c = 0; c < grid.Columns; c++)
                {
                    int id  = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }
        private void DrawBlock(Block block)
        {
            foreach(Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach(Position p in block.TilePositions())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private void WriteHighScoreToFile(int highScore)
        {
            string filePath = "C:/Users/AMG_Computers/source/repos/Tetris/Tetris/High score.txt";

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(highScore);
            }
        }

        public void UpdateHighScore(int score)
        { 
            if (score > highScore)
            {
                highScore = score;
                WriteHighScoreToFile(highScore);
                textBoxHighScore.Text = highScore.ToString();
                NewHighScore.Text = $"New highest score: {highScore}";
                NewHighScore.Visibility = Visibility.Visible;
            }
        }

        public void ReadHighScoreFromFile()
        {
            string filePath = "C:/Users/AMG_Computers/source/repos/Tetris/Tetris/High score.txt";
            
            using (StreamReader sr = new StreamReader(filePath))
            {
                string highScoreString = sr.ReadLine();
                int.TryParse(highScoreString, out int readHighScore);
                {
                    highScore = readHighScore;
                    textBoxHighScore.Text = highScore.ToString();
                }
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(gameState.GameOver)
            {
                return;
            }

            switch(e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                case Key.Space:
                    gameState.DropBlock();
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private async Task GameLoop()
        {
            Draw(gameState);
            ReadHighScoreFromFile();
            NewHighScore.Visibility = Visibility.Hidden;

            while(!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            UpdateHighScore(gameState.Score);                               
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Your score: {gameState.Score}";
            HighScoreText.Text = $"Highest score: {highScore}";
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }
    }
}
