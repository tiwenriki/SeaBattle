using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class MainForm : Form
    {
        private readonly Player _humanPlayer;
        private readonly Player _computerPlayer;

        private readonly Board _humanBoard;
        private readonly Board _computerBoard;
        
        private readonly GameController _controller;

        private readonly ScoreBoard _scoreboard;

        public readonly Button _logoButton;
        private readonly Button _backToMenuButton;
        private readonly Button _infoButton;
        private readonly Button _settingsButton;
        private readonly Button _exitButton;
        private readonly Button _playButton;
        private readonly Button _shuffleButton;
        private readonly Button _startGameButton;
        private readonly Button _newGameButton;
        private readonly Button _historyButton;
        private readonly Button _backFromHistoryButton;

        private readonly PictureBox _infoPicBox;

        private TextBox _textBox;

        private static readonly Color ButtonBackColor = Color.FromArgb(65, 133, 243);

        private const char LogoCharacter = ' ';
        private const char BackToMenuCharacter = (char)0x33;
        private const char InfoCharacter = (char)0x69; //0x69
        private const char SettingsCharacter = (char)0x40;
        private const char ExitCharacter = (char)0x33;
        private const char PlayCharacter = (char)0x34;
        private const char ShuffleCharacter = (char)0x60; 
        private const char StartGameCharacter = (char)0x55;
        private const char NewGameCharacter = (char)0x6C;
        private const char HistoryCharacter = (char)0x71;

        
        public MainForm()
        {
            SuspendLayout();

            _humanBoard = new Board();
            _computerBoard = new Board(false);

            _humanPlayer = new HumanPlayer("You", _computerBoard);
            _computerPlayer = new ComputerPlayer("Computer");

            _scoreboard = new ScoreBoard(_humanPlayer, _computerPlayer, 10, 100);
            _controller = new GameController(_humanPlayer, _computerPlayer, _humanBoard, _computerBoard, _scoreboard);
            
            _logoButton = CreateLogoButton(LogoCharacter.ToString(), Color.Transparent );
            _logoButton.Enabled = false;
            _backToMenuButton = CreateButton(BackToMenuCharacter.ToString(), ButtonBackColor);
            _infoButton = CreateStartButton(InfoCharacter.ToString(), ButtonBackColor);
            _settingsButton = CreateStartButton(SettingsCharacter.ToString(), ButtonBackColor);
            _playButton = CreateStartButton(PlayCharacter.ToString(), ButtonBackColor);
            _exitButton = CreateButton(ExitCharacter.ToString(), ButtonBackColor);
            _shuffleButton = CreateButton(ShuffleCharacter.ToString(), ButtonBackColor);
            _newGameButton = CreateButton(NewGameCharacter.ToString(), ButtonBackColor);
            _startGameButton = CreateButton(StartGameCharacter.ToString(), ButtonBackColor);
            _historyButton = CreateButton(HistoryCharacter.ToString(), ButtonBackColor);
            _backFromHistoryButton = CreateButton(BackToMenuCharacter.ToString(), ButtonBackColor);

            _infoPicBox = CreatePicBox("E:/3 курс/C#/3.0kurs/3.0kurs/image/inf.png");

            _textBox = CreateTextBox();

            SetupWindow();
            LayoutControls();
            
            _scoreboard.GameEnded += OnGameEnded;

            _backToMenuButton.Click += OnBackToMenuButtonClick;
            _infoButton.Click += OnInfoButtonClick;
            _settingsButton.Click += OnSettingsButtonClick;
            _exitButton.Click += OnExitButtonClick;
            _playButton.Click += OnPlayButtonClick;
            _shuffleButton.Click += OnShuffleButtonClick;
            _startGameButton.Click += OnStartGameButtonClick;
            _newGameButton.Click += OnNewGameButtonClick;
            _historyButton.Click += OnHistoryButtonClick;
            _backFromHistoryButton.Click += OnBackFromHistoryButtonClick;

            ResumeLayout();

            StartNewGame();

            _shuffleButton.Visible = false;
            _startGameButton.Visible = false;
            _humanBoard.Visible = false;
            _computerBoard.Visible = false;
            _scoreboard.Visible = false;
            _backToMenuButton.Visible = false;
            _infoPicBox.Visible = false;
            _historyButton.Visible = false;
            _backFromHistoryButton.Visible = false;

            
        }

        private void OnNewGameButtonClick(object sender, System.EventArgs e)
        {
            StartNewGame();
        }


        private void StartNewGame()
        {
            _shuffleButton.Visible = true;
            _startGameButton.Visible = true;
            _newGameButton.Visible = false;
            _controller.NewGame();
        }

        private void ShowMenu()
        {
            _playButton.Visible = true;
            _exitButton.Visible = true;
            _infoButton.Visible = true;
            _settingsButton.Visible = true;
        }

        private void OnHistoryButtonClick(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = false;
            _startGameButton.Visible = false;
            _exitButton.Visible = false;
            _scoreboard.Visible = false;
            _humanBoard.Visible = false;
            _computerBoard.Visible = false;
            _backFromHistoryButton.Visible = true;
            _historyButton.Visible = false;
            _newGameButton.Visible = false;

            loadStat();
            _textBox.Visible = true;

        }

        private void loadStat()
        {
            string path = "E:/3 курс/C#/3.0kurs/3.0kurs/image/records.txt";
            StreamReader f = new StreamReader(path);
            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                _textBox.Text += s + "\n";
            }
            f.Close();
        }

        private void OnBackFromHistoryButtonClick(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = false;
            _startGameButton.Visible = false;
            _exitButton.Visible = true;
            _scoreboard.Visible = true;
            _humanBoard.Visible = true;
            _computerBoard.Visible = true;
            _backFromHistoryButton.Visible = false;
            _newGameButton.Visible = true;

            _textBox.Visible = false;
        }

        private void OnExitButtonClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void OnInfoButtonClick(object sender, System.EventArgs e)
        {
            _playButton.Visible = false;
            _exitButton.Visible = false;
            _infoButton.Visible = false;
            _settingsButton.Visible = false;
            _backToMenuButton.Visible = true;
            _infoPicBox.Visible = true;
            _logoButton.Visible = false;
        }

        private void OnSettingsButtonClick(object sender, System.EventArgs e)
        {
            _playButton.Visible = false;
            _exitButton.Visible = false;
            _infoButton.Visible = false;
            _settingsButton.Visible = false;
            _backToMenuButton.Visible = true;
            _infoPicBox.Visible = false;
            _logoButton.Visible = false;
        }

        private void OnBackToMenuButtonClick(object sender, System.EventArgs e)
        {
            ShowMenu();
            _backToMenuButton.Visible = false;
            _infoPicBox.Visible = false;
            _logoButton.Visible = true;
        }

        private void OnStartGameButtonClick(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = false;
            _newGameButton.Visible = false;
            _startGameButton.Visible = false;
            _controller.StartGame();
        }

        private void OnPlayButtonClick(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = true;
            _newGameButton.Visible = false;
            _startGameButton.Visible = true;
            _scoreboard.Visible = true;
            _humanBoard.Visible = true;
            _computerBoard.Visible = true;
            _playButton.Visible = false;
            this.BackgroundImage = Image.FromFile("E:/3 курс/C#/3.0kurs/3.0kurs/image/gray.jpg");
            _infoButton.Visible = false;
            _settingsButton.Visible = false;
        }

        private void OnShuffleButtonClick(object sender, System.EventArgs e)
        {
            _humanBoard.AddRandomShips();

        }

        private void OnGameEnded(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = false;
            _startGameButton.Visible = false;
            _newGameButton.Visible = true;
            _historyButton.Visible = true;
            _computerBoard.ShowShips();
        }




        private void SetupWindow()
        {
            AutoScaleDimensions = new SizeF(8, 19);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point, 186);
            Margin = Padding.Empty;
            Text = "SeaBattle.CSharp";
            BackColor = Color.FromArgb(235, 235, 235);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
        }

        private static TextBox CreateTextBox()
        {
            var textbox = new TextBox
            {
                Size = new Size(200, 100),
                BackColor = Color.Red
            };
            return textbox;

        }

        private static PictureBox CreatePicBox(string path)
        {
            var picbox = new PictureBox
            {
                Size = new Size(500, 300),
                BackgroundImage = Image.FromFile(path),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent
            };
            return picbox;

        }

        private static Button CreateButton(string text, Color backColor)
        {
            var button = new Button
                             {
                                 FlatStyle = FlatStyle.Flat,
                                 ForeColor = Color.White,
                                 BackColor = backColor,
                                 UseVisualStyleBackColor = false,
                                 Size = new Size(40, 40),
                                 Text = text,
                                 Font = new Font("Webdings", 24, FontStyle.Regular, GraphicsUnit.Point),
                                 TextAlign = ContentAlignment.TopCenter,
                             };
            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private static Button CreateLogoButton(string text, Color backColor)
        {
            var button = new Button
            {
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = backColor,
                UseVisualStyleBackColor = false,
                Size = new Size(500, 140),
                Text = text,
                Font = new Font("Webdings", 24, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.TopCenter,
            };
            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private static Button CreateStartButton(string text, Color backColor)
        {
            var button = new Button
            {
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = backColor,
                UseVisualStyleBackColor = false,
                Size = new Size(280, 40),
                Text = text,
                Font = new Font("Webdings", 24, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.TopCenter,
            };
            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private void LayoutControls()
        {
            _humanBoard.Location = new Point(0, 0);
            _computerBoard.Location = new Point(_humanBoard.Right, 0);
            _scoreboard.Location = new Point(25, _humanBoard.Bottom );
            _scoreboard.Width = _computerBoard.Right - 25; 
            _newGameButton.Location = new Point(_computerBoard.Right - _newGameButton.Width, _scoreboard.Bottom);
            _startGameButton.Location = _newGameButton.Location;
            _shuffleButton.Location = new Point(_newGameButton.Location.X - _shuffleButton.Width - 25, _newGameButton.Location.Y);
            _historyButton.Location = _shuffleButton.Location;

            _infoPicBox.Location = new Point(ClientSize.Width - 250, ClientSize.Height - 225);

            
            

            Controls.AddRange(new Control[]
                                  {
                                      _humanBoard,
                                      _computerBoard,
                                      _playButton,
                                      _exitButton,
                                      _infoButton,
                                      _settingsButton,
                                      _backToMenuButton,
                                      _scoreboard,
                                      _newGameButton,
                                      _startGameButton,
                                      _shuffleButton,
                                      _logoButton,
                                      _infoPicBox,
                                      _backFromHistoryButton,
                                      _historyButton
                                      
                                  });

            ClientSize = new Size(_computerBoard.Right + 25, _startGameButton.Bottom + 25);

            _playButton.Location = new Point(ClientSize.Width / 4, _humanBoard.Location.Y + 175);
            _settingsButton.Location = new Point(ClientSize.Width / 4, _humanBoard.Location.Y + 250);
            _infoButton.Location = new Point(ClientSize.Width / 4, _humanBoard.Location.Y + 325);
            _exitButton.Location = new Point(25, _newGameButton.Location.Y);
            _backFromHistoryButton.Location = _exitButton.Location;
            _backToMenuButton.Location = _exitButton.Location;

            this.BackgroundImage = Image.FromFile("E:/3 курс/C#/3.0kurs/3.0kurs/image/bg.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
             _logoButton.Image = Image.FromFile("E:/3 курс/C#/3.0kurs/3.0kurs/image/logo.png");
             _logoButton.BackgroundImageLayout = ImageLayout.Stretch;
             _logoButton.Location = new Point((ClientSize.Width / 2) - (_logoButton.Width / 2), 15);

            _textBox.Location = new Point(10, _logoButton.Height + 10);

            System.Console.WriteLine(_computerBoard.Right - _newGameButton.Width + " " + _scoreboard.Bottom);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
