using ApplicationLayer;
using Domain;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private readonly GameService _service;
        private GameState _state = null!;
        private readonly int _cell = 20;

        public Form1(GameService service)
        {
            InitializeComponent();

            _service = service;
            DoubleBuffered = true;
            Text = "Snake";
            KeyPreview = true;

            _service.StateChanged += s =>
            {
                _state = s;
                Invalidate();
            };
            _state = _service.GetState();

            ClientSize = new Size(_state.Width * _cell, (_state.Height * _cell) + 40);

            var restart = new Button
            {
                Text = "Reiniciar",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            restart.Click += (_, __) => _service.NewGame();
            Controls.Add(restart);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    _service.ChangeDirection(Direction.Up);
                    return true;
                case Keys.Down:
                    _service.ChangeDirection(Direction.Down);
                    return true;
                case Keys.Left:
                    _service.ChangeDirection(Direction.Left);
                    return true;
                case Keys.Right:
                    _service.ChangeDirection(Direction.Right);
                    return true;
                case Keys.Space:
                    if (_state.IsOver) _service.NewGame();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (_state.IsOver && e.KeyCode == Keys.Space)
            {
                _service.NewGame();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Up: _service.ChangeDirection(Direction.Up); break;
                case Keys.Down: _service.ChangeDirection(Direction.Down); break;
                case Keys.Left: _service.ChangeDirection(Direction.Left); break;
                case Keys.Right: _service.ChangeDirection(Direction.Right); break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_state == null) return;

            var g = e.Graphics;
            g.Clear(Color.Black);

            using var gridPen = new Pen(Color.FromArgb(40, Color.White));
            for (int x = 0; x <= _state.Width; x++)
                g.DrawLine(gridPen, x * _cell, 0, x * _cell, _state.Height * _cell);
            for (int y = 0; y <= _state.Height; y++)
                g.DrawLine(gridPen, 0, y * _cell, _state.Width * _cell, y * _cell);

            using var foodBrush = new SolidBrush(Color.OrangeRed);
            g.FillRectangle(foodBrush, _state.Food.X * _cell, _state.Food.Y * _cell, _cell, _cell);

            using var headBrush = new SolidBrush(Color.LimeGreen);
            using var bodyBrush = new SolidBrush(Color.Green);
            var segments = _state.Snake.ToList();
            if (segments.Count > 0)
            {
  
                var h = segments[0];
                g.FillRectangle(headBrush, h.X * _cell, h.Y * _cell, _cell, _cell);

       
                foreach (var s in segments.Skip(1))
                    g.FillRectangle(bodyBrush, s.X * _cell, s.Y * _cell, _cell, _cell);
            }

            using var hudBrush = new SolidBrush(Color.White);
            using var font = new Font("Consolas", 12);
            g.DrawString($"Score: {_state.Score}" + (_state.IsOver ? "   [SPACE] para reiniciar" : ""),
                font, hudBrush, new PointF(8, _state.Height * _cell + 8));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
