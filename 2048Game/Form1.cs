namespace _2048Game
{
    public partial class Form1 : Form
    {
        GameFunctions game;
        private string direction;
        public Form1()
        {
            InitializeComponent();
            game = new GameFunctions();
            this.KeyDown += Form1_KeyDown;
            AddRandomTile();
            LoadBoard();
        }
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                direction = "Left";
                game.PlayMove(direction);
                AddRandomTile();
                LoadBoard();
            }
            else if (e.KeyCode == Keys.Right)
            {
                direction = "Right";
                game.PlayMove(direction);
                AddRandomTile();
                LoadBoard();
            }
            else if (e.KeyCode == Keys.Up)
            {
                direction = "Up";
                game.PlayMove(direction);
                AddRandomTile();
                LoadBoard();
            }
            else if (e.KeyCode == Keys.Down)
            {
                direction = "Down";
                game.PlayMove(direction);
                AddRandomTile();
                LoadBoard();
            }
        }

        public void AddRandomTile()
        {
            string[,] board = game.AddRandomTile();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((board[i, j] != ""))
                    {
                        string buttonName = $"btn{i}_{j}";

                        Control[] controls = this.Controls.Find(buttonName, true);

                        if (controls.Length > 0 && controls[0] is Button button)
                        {
                            int score = Convert.ToInt32(board[i, j]);
                            Color buttonColor = ColorTranslator.FromHtml(game.GetColor(score));
                            //Color buttonColor = Color.FromName(game.GetColor(score));
                            button.Text = board[i, j];
                            button.BackColor = buttonColor;
                            button.ForeColor = buttonColor;
                        }
                    }
                }
            }
        }

        public void LoadBoard()
        {
            string[,] board = game.getBoard();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string buttonName = $"btn{i}_{j}";
                    Control[] controls = this.Controls.Find(buttonName, true);

                    if (controls.Length > 0 && controls[0] is Button button)
                    {
                        int score = 0;
                        if (board[i, j] != "")
                        {
                            button.Text = board[i, j];
                            score = Convert.ToInt32(board[i, j]);
                        }
                        else
                        {
                            button.Text = "";
                        }
                        Color buttonColor = ColorTranslator.FromHtml(game.GetColor(score));
                        //Color buttonColor = Color.FromName(game.GetColor(score));
                        button.BackColor = buttonColor;
                        button.ForeColor = buttonColor;
                    }
                }
            }
            lblScore.Text = $"Your score is: {game.GetScore()}";
        }
    }
}