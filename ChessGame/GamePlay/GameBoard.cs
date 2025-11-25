using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using Chess;
using Svg;
using System.Threading.Tasks;

namespace ChessGame
{
    public partial class GameBoard : Form
    {
        private const int BoardSize = 8;

        private readonly Color LightSquareColor = Color.FromArgb(240, 217, 181);
        private readonly Color DarkSquareColor = Color.FromArgb(181, 136, 99);
        private readonly Color SelectedSquareColor = Color.FromArgb(246, 246, 105);
        private readonly Color LegalMoveColor = Color.FromArgb(118, 150, 86);
        private readonly Color LastMoveColor = Color.FromArgb(246, 246, 105);

        private Color BlendColor(Color baseColor, Color overlayColor, double amount)
        {
            if (amount < 0) amount = 0;
            if (amount > 1) amount = 1;

            int r = (int)(baseColor.R + (overlayColor.R - baseColor.R) * amount);
            int g = (int)(baseColor.G + (overlayColor.G - baseColor.G) * amount);
            int b = (int)(baseColor.B + (overlayColor.B - baseColor.B) * amount);

            return Color.FromArgb(r, g, b);
        }


        private ChessBoard _board;
        private Button[,] _buttons = new Button[BoardSize, BoardSize];

        private int _selectedBoardX = -1;
        private int _selectedBoardY = -1;
        private readonly List<Point> _legalTargets = new List<Point>();
        private bool _isGameOver;
        private Point? _lastMoveFrom;
        private Point? _lastMoveTo;
        private bool _lastResignWasDisconnect;
        public bool LastResignWasDisconnect => _lastResignWasDisconnect;


        // Info người chơi / thời gian
        private string _localUsername;
        private string _localDisplayName;
        private string _opponentUsername;
        private string _opponentDisplayName;
        private bool _localIsWhite = true;
        private int? _roomId;
        private int _baseMinutes = 10;
        private int _incrementSeconds = 0;

        private TimeSpan _whiteTime;
        private TimeSpan _blackTime;
        private System.Windows.Forms.Timer _clockTimer;

        private readonly Dictionary<string, Image> _pieceImageCache = new Dictionary<string, Image>();
        private SoundPlayer _moveSound;
        private SoundPlayer _captureSound;
        private SoundPlayer _checkSound;

        private readonly string[] _emoticons = new string[]
        {
            "😀","😃","😄","😁","😆","😅","😂","🤣","🥲","😊","😇",
            "🙂","🙃","😉","😌","😍","🥰","😘","😗","😙","😚","😋",
            "😜","😝","😛","🤑","🤗","🤭","🤫","🤔","🤐","😐","😑",
            "😶","😶‍🌫️","🙄","😏","😒","😞","😔","😟","😕","🙁",
            "☹️","😣","😖","😫","😩","🥺","😢","😭","😤","😠","😡",
            "🤬","🤯","😳","🥵","🥶","😱","😨","😰","😥","😓","🤤",
            "😪","😴","😬","😮‍💨","🫠","😵","😵‍💫","🤐","🥴","😷",
            "🤒","🤕","🤢","🤮","🤧","😇","🥳","🥸","😎","🤓","🧐",
            "😕","😟","🙁","☹️","😮","😯","😲","😳","🥺","🥹","😦",
            "😧","😨","😩","😰","😱","😪","😵","🤐","🥴","😷","🤒",
            "🤕","🤢","🤮","🤧","😇","🥳","🥸","😎","🤓","🧐",
            // Gestures/People
            "👋","🤚","🖐️","✋","🖖","👌","🤌","🤏","✌️","🤞",
            "🤟","🤘","🤙","👈","👉","👆","🖕","👇","☝️","👍","👎",
            "✊","👊","🤛","🤜","👏","🙌","🫶","👐","🤲","🙏",
            "💪","🦾","🦵","🦿","🦶","👂","🦻","👃","👣","👀","👁️",
            "🫦","👄","🦷","🦴","👅",
            // Relations/Love
            "💋","👄","💘","💝","💖","💗","💓","💞","💕","💌","💟",
            "❣️","💔","❤️","🧡","💛","💚","💙","💜","🤎","🖤","🤍",
            // Animals/Nature
            "🐶","🐱","🐭","🐹","🐰","🦊","🐻","🐼","🐨","🐯","🦁",
            "🐮","🐷","🐸","🐵","🐔","🐧","🐦","🐤","🐣","🐥","🦆",
            "🦅","🦉","🦇","🐺","🐗","🐴","🦄","🐝","🐛","🦋","🐌",
            "🐞","🐜","🦟","🦗","🕷️","🦂","🐢","🐍","🦎","🦖","🦕",
            "🐙","🦑","🦐","🦞","🦀","🐠","🐟","🐡","🐬","🐳","🐋",
            "🦈","🐊","🐅","🐆","🦓","🦍","🦧","🐘","🦣","🦛","🦏",
            "🐪","🐫","🦒","🦘","🦥","🦦","🦨","🦡","🐁","🐀","🐇",
            "🦔",
            // Food/Drinks
            "🍏","🍎","🍐","🍊","🍋","🍌","🍉","🍇","🍓","🫐",
            "🍈","🍒","🍑","🥭","🍍","🥥","🥝","🍅","🍆","🥑","🥦",
            "🥬","🥒","🌶️","🫑","🌽","🥕","🧄","🧅","🥔","🍠",
            "🥐","🥯","🍞","🥖","🥨","🧀","🥚","🍳","🥞","🧇",
            "🥓","🥩","🍗","🍖","🦴","🌭","🍔","🍟","🍕","🫓",
            "🥪","🥙","🧆","🌮","🌯","🫔","🥗","🥘","🫕","🥫",
            "🍝","🍜","🍲","🍛","🍣","🍱","🥟","🦪","🍤","🍙",
            "🍚","🍘","🍥","🥠","🥮","🍢","🍡","🍧","🍨","🍦",
            "🥧","🧁","🍰","🎂","🍮","🍭","🍬","🍫","🍿","🧃",
            "🥤","🧋","🫖","☕","🍵","🧉","🍶","🍺","🍻","🥂",
            "🍷","🥃","🍸","🍹","🍾",
            // Activities/Objects
            "⚽","🏀","🏈","⚾","🥎","🎾","🏐","🏉","🥏","🎱",
            "🏓","🏸","🥅","🏒","🏑","🥍","🏏","🪃","🏹","🎣",
            "🤿","🥊","🥋","🎽","🛹","🛷","⛸️","🥌","🥇","🥈",
            "🥉","🏆","🏅","🎖️","🥫","🏵️","🎗️","🎫","🎟️",
            "🎪","🤹‍♂️","🤹‍♀️","🎭","🩰","🎨","🎬","🎤","🎧","🎼",
            "🎹","🥁","🎷","🎺","🎸","🪕",
            // Travel/Places
            "🚗","🚕","🚙","🚌","🚎","🏎️","🚓","🚑","🚒","🚐",
            "🚚","🚛","🚜","🛵","🏍️","🚲","🛴","🚏","🛣️","🛤️",
            "🗺️","🗿","🗽","🗼","🏰","🏯","🏟️","🎡","🎢","🎠",
            "⛲","⛱️","🏖️","🏝️","🛶","⛵","🚤","🛥️","🛳️","⛴️",
            "🚀","🛸","✈️","🛫","🛬",
            // Symbols/Flags
            "🏁","🚩","🎌","🏴","🏳️","🏳️‍🌈","🏳️‍⚧️","🏴‍☠️","🇦🇺",
            "🇨🇦","🇫🇷","🇩🇪","🇨🇳","🇯🇵","🇰🇷","🇷🇺","🇬🇧","🇺🇸"
        };

        private bool _boardUiCreated = false;

        // ==== Sự kiện để InRoom/Match bắt và gửi qua server ====
        public event Action<string, string, string> LocalMovePlayed; // from,to,promo
        public event Action LocalResignRequested;
        public event Action LocalOfferDrawRequested;
        public event Action<string> LocalChatSent;
        public event Action<string, string> LocalGameEnded;


        public GameBoard()
        {
            InitializeComponent();
            SetupEmojiPickerPanel();
            LoadMoveSound();
            this.FormClosing += GameBoard_FormClosing;
        }

        /// <summary>
        /// Gọi từ InRoom/Match trước khi ShowDialog.
        /// </summary>
        public void InitGameSession(
            string localUsername,
            string localDisplayName,
            string opponentUsername,
            string opponentDisplayName,
            bool localIsWhite,
            int minutes,
            int incrementSeconds,
            int? roomId = null)
        {
            _localUsername = localUsername;
            _localDisplayName = localDisplayName;
            _opponentUsername = opponentUsername;
            _opponentDisplayName = opponentDisplayName;
            _localIsWhite = localIsWhite;
            _baseMinutes = minutes;
            _incrementSeconds = incrementSeconds;
            _roomId = roomId;

            if (lblPlayer1Name != null) lblPlayer1Name.Text = _localDisplayName;
            if (lblPlayer2Name != null) lblPlayer2Name.Text = _opponentDisplayName;

            if (lblPlayer1Status != null)
                lblPlayer1Status.Text = _localIsWhite ? "Bạn - Trắng" : "Bạn - Đen";
            if (lblPlayer2Status != null)
                lblPlayer2Status.Text = _localIsWhite ? "Đối thủ - Đen" : "Đối thủ - Trắng";

            if (!_boardUiCreated)
            {
                CreateBoardUi();
                _boardUiCreated = true;
            }

            InitializeGame();
            InitClockTimer();
            UpdateTimeLabels();
            UpdateTurnLabel();
        }

        private string AppendReturnHint(string baseMessage)
        {
            string hint = _roomId.HasValue
                ? "Nhấn OK để về phòng."
                : "Nhấn OK để về sảnh ghép trận.";

            return baseMessage + "\n\n" + hint;
        }

        // ================== BÀN CỜ & GERA.CHESS ==================

        private void InitializeGame()
        {
            _board = new ChessBoard
            {
                AutoEndgameRules = AutoEndgameRules.All
            };

            _isGameOver = false;
            _selectedBoardX = -1;
            _selectedBoardY = -1;
            _legalTargets.Clear();
            _lastMoveFrom = null;
            _lastMoveTo = null;

            _whiteTime = TimeSpan.FromMinutes(_baseMinutes);
            _blackTime = TimeSpan.FromMinutes(_baseMinutes);

            UpdateBoardUi();
        }

        private void CreateBoardUi()
        {
            pnlChessBoard.Controls.Clear();
            int squareSize = Math.Min(pnlChessBoard.Width, pnlChessBoard.Height) / BoardSize;
            if (squareSize <= 0) squareSize = 64;

            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    var btn = new Button
                    {
                        Width = squareSize,
                        Height = squareSize,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                        FlatStyle = FlatStyle.Flat,
                        ImageAlign = ContentAlignment.MiddleCenter,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        UseVisualStyleBackColor = false,
                        Tag = new Point(x, y)
                    };
                    btn.FlatAppearance.BorderSize = 0;

                    var (row, col) = BoardToUiCoord(x, y);
                    btn.Location = new Point(col * squareSize, row * squareSize);

                    btn.Click += Square_Click;

                    _buttons[x, y] = btn;
                    pnlChessBoard.Controls.Add(btn);
                }
            }
            CreateCoordinateLabels(squareSize);
        }

        private void CreateCoordinateLabels(int squareSize)
        {
            // Kích thước chữ
            int labelSize = 16;

            // ===== HÀNG NGANG: a b c d e f g h (từ trái qua phải) =====
            for (int i = 0; i < BoardSize; i++)
            {
                char fileChar = (char)('a' + i);

                var lbl = new Label
                {
                    AutoSize = false,
                    Width = labelSize,
                    Height = labelSize,
                    Text = fileChar.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(78, 49, 41),
                    BackColor = Color.Transparent
                };

                int x = pnlChessBoard.Left + i * squareSize + (squareSize - labelSize) / 2;
                int y = pnlChessBoard.Bottom + 2; // ngay dưới bàn cờ

                lbl.Location = new Point(x, y);
                this.Controls.Add(lbl);
                lbl.BringToFront();
            }

            // ===== HÀNG DỌC: 1 2 3 4 5 6 7 8 (từ dưới lên trên) =====
            for (int i = 0; i < BoardSize; i++)
            {
                int rankNumber = i + 1; // 1..8

                var lbl = new Label
                {
                    AutoSize = false,
                    Width = labelSize,
                    Height = labelSize,
                    Text = rankNumber.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(78, 49, 41),
                    BackColor = Color.Transparent
                };

                // i = 0 là hàng dưới cùng (1), i = 7 là hàng trên cùng (8)
                int rowUi = BoardSize - 1 - i; // 7..0
                int y = pnlChessBoard.Top + rowUi * squareSize + (squareSize - labelSize) / 2;
                int x = pnlChessBoard.Left - labelSize - 4; // bên trái bàn cờ

                lbl.Location = new Point(x, y);
                this.Controls.Add(lbl);
                lbl.BringToFront();
            }
        }

        private (int row, int col) BoardToUiCoord(int boardX, int boardY)
        {
            if (_localIsWhite)
            {
                int row = 7 - boardY;
                int col = boardX;
                return (row, col);
            }
            else
            {
                int row = boardY;
                int col = 7 - boardX;
                return (row, col);
            }
        }

        private Button GetButtonAt(int boardX, int boardY)
        {
            if (boardX < 0 || boardX >= BoardSize || boardY < 0 || boardY >= BoardSize)
                return null;
            return _buttons[boardX, boardY];
        }

        private void Square_Click(object sender, EventArgs e)
        {
            if (_board == null || _isGameOver) return;
            if (sender is not Button btn || btn.Tag is not Point p) return;

            int boardX = p.X;
            int boardY = p.Y;

            // Nếu đang chọn và click vào ô hợp lệ -> đi quân
            if (_selectedBoardX != -1 && _legalTargets.Any(t => t.X == boardX && t.Y == boardY))
            {
                if (TryExecuteMove(boardX, boardY))
                {
                    ClearSelectionInternal();
                    UpdateBoardUi();
                    CheckEndGameFromLogic();
                    UpdateTurnLabel();
                }
                return;
            }

            // Click lại ô đang chọn -> bỏ chọn
            if (_selectedBoardX == boardX && _selectedBoardY == boardY)
            {
                ClearSelection();
                return;
            }

            TrySelectSquare(boardX, boardY);
        }

        private void TrySelectSquare(int boardX, int boardY)
        {
            if (_board == null || _isGameOver) return;

            var pos = new Position((short)boardX, (short)boardY);
            var piece = _board[pos];

            // bấm ô trống => unselect
            if (piece == null)
            {
                ClearSelection();
                return;
            }

            bool pieceIsWhite = piece.Color == PieceColor.White;

            // chỉ cho điều khiển quân CỦA MÌNH
            if ((_localIsWhite && !pieceIsWhite) ||
                (!_localIsWhite && pieceIsWhite))
            {
                ClearSelection();
                return;
            }

            // phải đúng lượt trong engine
            var turn = _board.Turn;
            if ((turn == PieceColor.White && !pieceIsWhite) ||
                (turn == PieceColor.Black && pieceIsWhite))
            {
                ClearSelection();
                return;
            }

            var legalMoves = _board.Moves() ?? Array.Empty<Move>();
            var movesFromSquare = legalMoves
                .Where(m => m.OriginalPosition.X == pos.X && m.OriginalPosition.Y == pos.Y)
                .ToList();

            if (movesFromSquare.Count == 0)
            {
                ClearSelection();
                return;
            }

            // Bỏ chọn mọi ô trước đó + redraw lại bàn cờ
            ClearSelection();

            _selectedBoardX = boardX;
            _selectedBoardY = boardY;

            var selectedBtn = GetButtonAt(boardX, boardY);
            if (selectedBtn != null)
            {
                selectedBtn.BackColor = SelectedSquareColor;
            }

            // Lưu lại các ô đích hợp lệ
            foreach (var move in movesFromSquare)
            {
                var target = new Point(move.NewPosition.X, move.NewPosition.Y);
                _legalTargets.Add(target);
            }

            // Tô màu các ô đích hợp lệ
            foreach (var target in _legalTargets)
            {
                var targetBtn = GetButtonAt(target.X, target.Y);
                if (targetBtn != null)
                {
                    targetBtn.BackColor = LegalMoveColor;
                }
            }
        }

        private string CoordToAlgebraic(int x, int y)
        {
            char file = (char)('a' + x);
            char rank = (char)('1' + y);
            return new string(new[] { file, rank });
        }

        private void AlgebraicToCoord(string sq, out int x, out int y)
        {
            if (string.IsNullOrEmpty(sq) || sq.Length != 2)
            {
                x = y = -1;
                return;
            }
            x = sq[0] - 'a';
            y = sq[1] - '1';
        }

        private bool IsCaptureMove(int fromX, int fromY, int toX, int toY)
        {
            if (_board == null) return false;

            // 1) Ăn quân bình thường: ô đích đang có quân đối phương
            var targetPos = new Position((short)toX, (short)toY);
            var targetPiece = _board[targetPos];
            if (targetPiece != null)
                return true;

            // 2) En passant: tốt đi chéo đến ô trống nhưng ăn tốt ở ô phía sau
            var fromPos = new Position((short)fromX, (short)fromY);
            var movingPiece = _board[fromPos];

            if (movingPiece != null &&
                movingPiece.Type == PieceType.Pawn &&
                fromX != toX) // đi chéo
            {
                int direction = movingPiece.Color == PieceColor.White ? 1 : -1;
                int capturedY = toY - direction;

                if (capturedY >= 0 && capturedY < BoardSize)
                {
                    var capturedPos = new Position((short)toX, (short)capturedY);
                    var capturedPiece = _board[capturedPos];

                    if (capturedPiece != null &&
                        capturedPiece.Type == PieceType.Pawn &&
                        capturedPiece.Color != movingPiece.Color)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsCheckForOpponent(PieceColor sideMoved)
        {
            if (_board == null) return false;

            if (sideMoved == PieceColor.White)
                return _board.BlackKingChecked;
            else
                return _board.WhiteKingChecked;
        }

        private bool TryExecuteMove(int targetX, int targetY)
        {
            if (_board == null) return false;
            if (_selectedBoardX == -1 || _selectedBoardY == -1) return false;

            var legalMoves = _board.Moves() ?? Array.Empty<Move>();

            var candidateMoves = legalMoves
                .Where(m =>
                    m.OriginalPosition.X == _selectedBoardX &&
                    m.OriginalPosition.Y == _selectedBoardY &&
                    m.NewPosition.X == targetX &&
                    m.NewPosition.Y == targetY)
                .ToList();

            if (candidateMoves.Count == 0) return false;

            string fromSq = CoordToAlgebraic(_selectedBoardX, _selectedBoardY);
            string toSq = CoordToAlgebraic(targetX, targetY);
            string promo = null;

            Move move = candidateMoves[0];

            // Nếu đây là nước phong cấp
            if (move.IsPromotion)
            {
                bool isWhite = _board.Turn == PieceColor.White;
                var selectedType = ShowPromotionDialog(isWhite);

                // Chọn đúng Move ứng với quân đã chọn
                var promotionMove = candidateMoves
                    .FirstOrDefault(m => m.Promotion != null && m.Promotion.Type == selectedType);

                if (promotionMove != null)
                    move = promotionMove;

                promo = PieceTypeToPromotionChar(selectedType);
            }

            if (!move.HasValue) return false;

            PieceColor sideMoved = _board.Turn;
            bool isCapture = IsCaptureMove(_selectedBoardX, _selectedBoardY, targetX, targetY);

            _lastMoveFrom = new Point(_selectedBoardX, _selectedBoardY);
            _lastMoveTo = new Point(targetX, targetY);

            _board.Move(move);
            ApplyIncrement(sideMoved);

            bool isCheck = IsCheckForOpponent(sideMoved);
            PlayMoveSound(isCapture, isCheck);

            try
            {
                LocalMovePlayed?.Invoke(fromSq, toSq, promo);
            }
            catch { }

            return true;
        }

        /// <summary>
        /// Áp dụng nước đi từ server gửi xuống.
        /// </summary>
        public void ApplyNetworkMove(string fromSq, string toSq, string promotion)
        {
            if (_board == null || _isGameOver) return;

            AlgebraicToCoord(fromSq, out int sx, out int sy);
            AlgebraicToCoord(toSq, out int tx, out int ty);
            if (sx < 0 || sy < 0 || tx < 0 || ty < 0) return;

            var legal = _board.Moves() ?? Array.Empty<Move>();
            var candidateMoves = legal
                .Where(m =>
                    m.OriginalPosition.X == sx &&
                    m.OriginalPosition.Y == sy &&
                    m.NewPosition.X == tx &&
                    m.NewPosition.Y == ty)
                .ToList();

            if (candidateMoves.Count == 0) return;

            Move move = candidateMoves[0];

            // Nếu server gửi kèm thông tin phong cấp (q/r/b/n)
            if (!string.IsNullOrEmpty(promotion))
            {
                var requestedType = PromotionCharToPieceType(promotion[0]);
                if (requestedType != null)
                {
                    var targetType = requestedType;
                    var promoMove = candidateMoves
                        .FirstOrDefault(m => m.Promotion != null && m.Promotion.Type == targetType);
                    if (promoMove != null)
                        move = promoMove;
                }
            }

            if (!move.HasValue) return;

            PieceColor sideMoved = _board.Turn;
            bool isCapture = IsCaptureMove(sx, sy, tx, ty);

            _lastMoveFrom = new Point(sx, sy);
            _lastMoveTo = new Point(tx, ty);

            _board.Move(move);
            ApplyIncrement(sideMoved);

            bool isCheck = IsCheckForOpponent(sideMoved);
            PlayMoveSound(isCapture, isCheck);

            ClearSelectionInternal();
            UpdateBoardUi();
            CheckEndGameFromLogic();
            UpdateTurnLabel();
        }

        private void UpdateBoardUi()
        {
            if (_board == null) return;

            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    var btn = GetButtonAt(x, y);
                    if (btn == null) continue;

                    bool isDark = (x + y) % 2 == 1;
                    btn.BackColor = isDark ? DarkSquareColor : LightSquareColor;

                    var pos = new Position((short)x, (short)y);
                    var piece = _board[pos];

                    if (piece == null)
                    {
                        btn.Image = null;
                        btn.Text = "";
                    }
                    else
                    {
                        char fenChar = piece.ToFenChar();
                        btn.Image = GetPieceImage(fenChar, btn.Width, btn.Height);
                        btn.Text = "";
                    }
                }
            }

            // highlight nước đi cuối (from & to) bằng cách pha màu -> cảm giác trong suốt
            if (_lastMoveFrom.HasValue)
            {
                var fromBtn = GetButtonAt(_lastMoveFrom.Value.X, _lastMoveFrom.Value.Y);
                if (fromBtn != null)
                {
                    fromBtn.BackColor = BlendColor(fromBtn.BackColor, LastMoveColor, 0.35);
                }
            }

            if (_lastMoveTo.HasValue)
            {
                var toBtn = GetButtonAt(_lastMoveTo.Value.X, _lastMoveTo.Value.Y);
                if (toBtn != null)
                {
                    toBtn.BackColor = BlendColor(toBtn.BackColor, LastMoveColor, 0.35);
                }
            }

            // selection hiện tại sẽ override màu nước cuối nếu trùng
            if (_selectedBoardX != -1)
            {
                var selectedBtn = GetButtonAt(_selectedBoardX, _selectedBoardY);
                if (selectedBtn != null)
                {
                    selectedBtn.BackColor = SelectedSquareColor;
                }
            }

            // các ô nước đi hợp lệ (xanh) cũng override nếu trùng
            foreach (var target in _legalTargets)
            {
                var targetBtn = GetButtonAt(target.X, target.Y);
                if (targetBtn != null)
                {
                    targetBtn.BackColor = LegalMoveColor;
                }
            }
        }

        private Image GetPieceImage(char fenChar, int width, int height)
        {
            bool isWhite = char.IsUpper(fenChar);
            char kind = char.ToUpperInvariant(fenChar);
            string colorPrefix = isWhite ? "w" : "b";
            string fileName = $"{colorPrefix}{kind}.svg";

            string key = $"{fileName}_{width}x{height}";
            if (_pieceImageCache.TryGetValue(key, out Image cached))
            {
                return cached;
            }

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string piecesDir = Path.Combine(baseDir, "Pieces");
            string fullPath = Path.Combine(piecesDir, fileName);

            if (!File.Exists(fullPath))
            {
                return null;
            }

            SvgDocument svgDoc = SvgDocument.Open(fullPath);
            Bitmap bmp = svgDoc.Draw(width, height);

            _pieceImageCache[key] = bmp;
            return bmp;
        }

        private void ClearSelection()
        {
            ClearSelectionInternal();
            UpdateBoardUi();
        }

        private void ClearSelectionInternal()
        {
            _selectedBoardX = -1;
            _selectedBoardY = -1;
            _legalTargets.Clear();
        }

        // ================== ĐỒNG HỒ & LƯỢT ==================

        private void InitClockTimer()
        {
            if (_clockTimer == null)
            {
                _clockTimer = new System.Windows.Forms.Timer();
                _clockTimer.Interval = 1000;
                _clockTimer.Tick += ClockTimer_Tick;
            }
            _clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            if (_board == null || _isGameOver) return;

            if (_board.Turn == PieceColor.White)
            {
                _whiteTime = _whiteTime.Add(TimeSpan.FromSeconds(-1));
                if (_whiteTime <= TimeSpan.Zero)
                {
                    _whiteTime = TimeSpan.Zero;
                    HandleTimeOut(PieceColor.White);
                    return;
                }
            }
            else
            {
                _blackTime = _blackTime.Add(TimeSpan.FromSeconds(-1));
                if (_blackTime <= TimeSpan.Zero)
                {
                    _blackTime = TimeSpan.Zero;
                    HandleTimeOut(PieceColor.Black);
                    return;
                }
            }

            UpdateTimeLabels();
        }

        private void ApplyIncrement(PieceColor sideMoved)
        {
            if (_incrementSeconds <= 0) return;

            if (sideMoved == PieceColor.White)
                _whiteTime = _whiteTime.Add(TimeSpan.FromSeconds(_incrementSeconds));
            else
                _blackTime = _blackTime.Add(TimeSpan.FromSeconds(_incrementSeconds));

            UpdateTimeLabels();
        }

        private void UpdateTimeLabels()
        {
            if (lblTime == null || _board == null) return;

            bool whiteTurn = _board.Turn == PieceColor.White;
            TimeSpan t = whiteTurn ? _whiteTime : _blackTime;
            string timeStr = $"{(int)t.TotalMinutes:00}:{t.Seconds:00}";

            // Chỉ hiển thị thời gian bên ĐANG TỚI LƯỢT
            lblTime.Text = timeStr;
        }


        private void UpdateTurnLabel()
        {
            if (lblTurnValue == null || _board == null) return;

            bool whiteTurn = _board.Turn == PieceColor.White;
            string playerName = whiteTurn
                ? (_localIsWhite ? _localDisplayName : _opponentDisplayName)
                : (_localIsWhite ? _opponentDisplayName : _localDisplayName);

            string sideText = whiteTurn ? "Trắng" : "Đen";
            lblTurnValue.Text = $"{playerName} ({sideText})";
        }

        // ================== KẾT THÚC VÁN CỜ ==================

        private void CheckEndGameFromLogic()
        {
            if (_board == null) return;

            if (_board.IsEndGame)
            {
                HandleEndGameFromEngine();
            }
        }

        private void HandleEndGameFromEngine()
        {
            _isGameOver = true;
            _clockTimer?.Stop();

            var info = _board.EndGame;
            string msg;

            string resultStr;
            string reasonStr = info?.EndgameType.ToString().ToLowerInvariant() ?? null;

            if (info?.WonSide == null)
            {
                resultStr = "draw";
                msg = $"Ván đấu hòa ({info?.EndgameType})";
            }
            else if (info.WonSide == PieceColor.White)
            {
                resultStr = "white";
                string winnerName = _localIsWhite ? _localDisplayName : _opponentDisplayName;
                msg = $"Trắng thắng ({info.EndgameType})\nNgười thắng: {winnerName}";
            }
            else
            {
                resultStr = "black";
                string winnerName = _localIsWhite ? _opponentDisplayName : _localDisplayName;
                msg = $"Đen thắng ({info.EndgameType})\nNgười thắng: {winnerName}";
            }

            try
            {
                LocalGameEnded?.Invoke(resultStr, reasonStr);
            }
            catch
            {
            }

            string fullMsg = AppendReturnHint(msg);
            MessageBox.Show(this, fullMsg, "Kết thúc ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Sau khi người chơi bấm OK thì tự đóng bàn cờ
            Close();
        }

        private void HandleTimeOut(PieceColor sideFlagged)
        {
            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            PieceColor winner = sideFlagged == PieceColor.White ? PieceColor.Black : PieceColor.White;
            bool localWins =
                (winner == PieceColor.White && _localIsWhite) ||
                (winner == PieceColor.Black && !_localIsWhite);

            string winnerName = localWins ? _localDisplayName : _opponentDisplayName;
            string loserSide = sideFlagged == PieceColor.White ? "Trắng" : "Đen";

            string resultStr = winner == PieceColor.White ? "white" : "black";
            string reasonStr = "time";

            try
            {
                LocalGameEnded?.Invoke(resultStr, reasonStr);
            }
            catch
            {
            }

            string msg = $"Hết thời gian cho bên {loserSide}.\nNgười thắng: {winnerName}";
            string fullMsg = AppendReturnHint(msg);
            MessageBox.Show(this, fullMsg, "Kết thúc ván đấu (hết giờ)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Sau khi hết giờ, đóng luôn bàn cờ
            Close();
        }

        public void NotifyOpponentDisconnectedWin()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            // Mình là bên thắng
            PieceColor localColor = _localIsWhite ? PieceColor.White : PieceColor.Black;
            string resultStr = localColor == PieceColor.White ? "white" : "black";
            string reasonStr = "disconnect";

            // Báo cho InRoom/Match để gửi GAME_RESULT (Elo/history)
            try
            {
                LocalGameEnded?.Invoke(resultStr, reasonStr);
            }
            catch
            {
            }

            string baseMsg = "Trò chơi kết thúc. Đối thủ đã thoát khỏi ván đấu hoặc bị mất kết nối.\nBạn đã thắng.";
            string fullMsg = AppendReturnHint(baseMsg);

            try
            {
                MessageBox.Show(
                    this,
                    fullMsg,
                    "Kết thúc ván đấu (đối thủ thoát)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch
            {
            }

            // Đóng bàn cờ sau khi xem thông báo
            Close();
        }

        // ================== ĐẦU HÀNG & CẦU HÒA ==================

        private void btnSurrender_Click(object sender, EventArgs e)
        {
            if (_board == null || _isGameOver) return;

            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đầu hàng không?",
                "Xác nhận đầu hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            // THÊM DÒNG NÀY
            _lastResignWasDisconnect = false;

            try
            {
                LocalResignRequested?.Invoke();
            }
            catch
            {
            }

            // Người bấm nút này đầu hàng -> thua
            FinishGameByResign(localResigned: true);
        }

        private void btnOfferDraw_Click(object sender, EventArgs e)
        {
            if (_board == null || _isGameOver) return;

            var confirm = MessageBox.Show(
                "Bạn có muốn đề nghị hòa ván đấu này không?",
                "Cầu hòa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                LocalOfferDrawRequested?.Invoke();
            }
            catch
            {
            }

            // Chỉ gửi đề nghị lên server, chờ phản hồi từ đối thủ
            MessageBox.Show(this,
                "Bạn đã gửi lời đề nghị hòa. Vui lòng chờ đối thủ trả lời.",
                "Cầu hòa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ================== ÂM THANH ==================

        private void LoadMoveSound()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string soundsDir = Path.Combine(baseDir, "Sounds");
                string movePath = Path.Combine(soundsDir, "move.wav");
                string capturePath = Path.Combine(soundsDir, "capture.wav");
                string checkPath = Path.Combine(soundsDir, "check.wav");

                if (File.Exists(movePath))
                {
                    _moveSound = new SoundPlayer(movePath);
                    _moveSound.Load();
                }

                if (File.Exists(capturePath))
                {
                    _captureSound = new SoundPlayer(capturePath);
                    _captureSound.Load();
                }

                if (File.Exists(checkPath))
                {
                    _checkSound = new SoundPlayer(checkPath);
                    _checkSound.Load();
                }
            }
            catch
            {
                _moveSound = null;
                _captureSound = null;
                _checkSound = null;
            }
        }

        private void PlayMoveSound(bool isCapture, bool isCheck)
        {
            try
            {
                // Vừa bắt quân vừa chiếu
                if (isCapture && isCheck && _captureSound != null && _checkSound != null)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            // Phát capture đồng bộ
                            _captureSound.PlaySync();

                            // Đợi thêm 0.1s rồi phát check
                            System.Threading.Thread.Sleep(100);
                            _checkSound.Play();
                        }
                        catch
                        {
                        }
                    });
                    return;
                }

                // Chỉ bắt quân
                if (isCapture && _captureSound != null)
                {
                    _captureSound.Play();
                    return;
                }

                // Chỉ chiếu (không bắt)
                if (isCheck && _checkSound != null)
                {
                    _checkSound.Play();
                    return;
                }

                // Nước đi bình thường
                _moveSound?.Play();
            }
            catch
            {
            }
        }

        // Overload cũ, phòng trường hợp chỗ khác vẫn gọi không truyền tham số
        private void PlayMoveSound()
        {
            PlayMoveSound(false, false);
        }

        // Overload cũ (1 tham số) để code cũ không bị lỗi compile
        private void PlayMoveSound(bool isCapture)
        {
            PlayMoveSound(isCapture, false);
        }

        // ================== CHAT + STICKER ==================

        private void SetupEmojiPickerPanel()
        {
            if (pnlEmojiPicker == null) return;
            pnlEmojiPicker.Visible = false;
            pnlEmojiPicker.Controls.Clear();
            pnlEmojiPicker.AutoScroll = true;
        }

        private void ShowEmojiPicker()
        {
            if (pnlEmojiPicker == null) return;

            if (pnlEmojiPicker.Visible && pnlEmojiPicker.Controls.Count > 0)
            {
                pnlEmojiPicker.Visible = false;
                return;
            }

            pnlEmojiPicker.Visible = true;
            pnlEmojiPicker.BringToFront();
            pnlEmojiPicker.Controls.Clear();

            int btnSize = 32;
            int cols = 8;
            int spacing = 4;

            for (int i = 0; i < _emoticons.Length; i++)
            {
                var btn = new Button();
                btn.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Regular);
                btn.Text = _emoticons[i];
                btn.Width = btn.Height = btnSize;
                int col = i % cols;
                int row = i / cols;
                btn.Left = col * (btnSize + spacing);
                btn.Top = row * (btnSize + spacing);
                btn.Margin = new Padding(0);
                btn.Padding = new Padding(0);

                btn.Click += (s, e) =>
                {
                    txtChatInput.Text += ((Button)s).Text;
                    txtChatInput.SelectionStart = txtChatInput.Text.Length;
                    txtChatInput.Focus();
                };

                pnlEmojiPicker.Controls.Add(btn);
            }
        }

        private void btnEmoji_Click(object sender, EventArgs e)
        {
            ShowEmojiPicker();
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            string text = txtChatInput.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            string time = DateTime.Now.ToString("HH:mm:ss");
            // local luôn dùng username (để tính màu)
            AppendLocalChat(_localUsername, text, time);

            try
            {
                LocalChatSent?.Invoke(text);
            }
            catch { }

            txtChatInput.Clear();
            pnlEmojiPicker.Visible = false;
        }

        private void txtChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendChat_Click(null, null);
                e.SuppressKeyPress = true;
                pnlEmojiPicker.Visible = false;
            }
        }

        // Xác định bên Trắng/Đen theo username
        private string GetSideTextForUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return "?";

            if (string.Equals(username, _localUsername, StringComparison.OrdinalIgnoreCase))
                return _localIsWhite ? "Trắng" : "Đen";

            if (!string.IsNullOrEmpty(_opponentUsername) &&
                string.Equals(username, _opponentUsername, StringComparison.OrdinalIgnoreCase))
                return _localIsWhite ? "Đen" : "Trắng";

            return "?";
        }

        private void AppendLocalChat(string username, string content, string time)
        {
            if (rtbChatMessages == null) return;

            string sideText = GetSideTextForUsername(username);
            string line = $"{username} ({sideText}) - {content} [{time}]{Environment.NewLine}";
            rtbChatMessages.AppendText(line);
            rtbChatMessages.SelectionStart = rtbChatMessages.TextLength;
            rtbChatMessages.ScrollToCaret(); // auto cuộn xuống cuối
        }

        public void AppendNetworkChat(string username, string content, string time, bool isLocalSender)
        {
            if (rtbChatMessages == null) return;

            if (isLocalSender)
            {
                // Đã hiển thị ở LocalChat rồi, khỏi hiển thị lại
                return;
            }

            string sideText = GetSideTextForUsername(username);
            string line = $"{username} ({sideText}) - {content} [{time}]{Environment.NewLine}";
            rtbChatMessages.AppendText(line);
            rtbChatMessages.SelectionStart = rtbChatMessages.TextLength;
            rtbChatMessages.ScrollToCaret(); // auto cuộn xuống cuối
        }

        private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu ván cờ đã kết thúc rồi hoặc _board chưa sẵn thì thôi
            if (_isGameOver || _board == null) return;

            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            // Local thoát = local thua, đối thủ thắng
            string resultStr;
            string reasonStr = "disconnect";

            if (_localIsWhite)
            {
                // Local là Trắng -> Trắng thua, Đen thắng
                resultStr = "black";
            }
            else
            {
                // Local là Đen -> Đen thua, Trắng thắng
                resultStr = "white";
            }

            // THÊM: đánh dấu đây là thoát/disconnect
            _lastResignWasDisconnect = true;

            // Gửi tín hiệu "đầu hàng/thoát" để server broadcast cho đối thủ (GAME_RESIGN)
            try
            {
                LocalResignRequested?.Invoke();
            }
            catch { }

            // Gửi kết quả để Match/InRoom gửi GAME_RESULT lên server (cập nhật Elo + history)
            try
            {
                LocalGameEnded?.Invoke(resultStr, reasonStr);
            }
            catch { }

            try
            {
                MessageBox.Show(
                    this,
                    "Trò chơi kết thúc. Bạn đã thua do thoát khỏi ván đấu hoặc bị mất kết nối.",
                    "Kết thúc ván đấu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch { }
        }

        public void FinishGameByResign(bool localResigned)
        {
            if (_isGameOver) return;

            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            PieceColor localColor = _localIsWhite ? PieceColor.White : PieceColor.Black;
            PieceColor winnerColor = localResigned
                ? (localColor == PieceColor.White ? PieceColor.Black : PieceColor.White)
                : localColor;

            string resultStr = winnerColor == PieceColor.White ? "white" : "black";
            string reasonStr = "resign";

            try
            {
                LocalGameEnded?.Invoke(resultStr, reasonStr);
            }
            catch
            {
            }

            string baseMsg = localResigned
                ? "Trận đấu kết thúc, bạn đã thua vì đầu hàng."
                : "Trận đấu kết thúc, bạn đã thắng vì đối thủ đầu hàng.";

            string fullMsg = AppendReturnHint(baseMsg);

            MessageBox.Show(this, fullMsg, "Kết thúc ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Sau khi người chơi bấm OK thì đóng bàn cờ
            Close();
        }

        public void FinishGameByAgreedDraw()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            string resultStr = "draw";
            string reasonStr = "agreement";

            try
            {
                LocalGameEnded?.Invoke(resultStr, reasonStr);
            }
            catch
            {
            }

            string baseMsg = "Trận đấu kết thúc. Ván đấu này hòa (thỏa thuận).";
            string fullMsg = AppendReturnHint(baseMsg);

            MessageBox.Show(this,
                fullMsg,
                "Hòa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Sau khi người chơi bấm OK thì đóng bàn cờ
            Close();
        }

        // =========== Phong cấp: chọn quân ===========
        private PieceType ShowPromotionDialog(bool isWhite)
        {
            int imageSize = 80;

            Image rook = GetPieceImage(isWhite ? 'R' : 'r', imageSize, imageSize);
            Image knight = GetPieceImage(isWhite ? 'N' : 'n', imageSize, imageSize);
            Image bishop = GetPieceImage(isWhite ? 'B' : 'b', imageSize, imageSize);
            Image queen = GetPieceImage(isWhite ? 'Q' : 'q', imageSize, imageSize);

            using (var dlg = new PromotionDialog(isWhite, rook, knight, bishop, queen, LegalMoveColor))
            {
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    return dlg.SelectedPieceType;
                }
            }

            // Nếu vì lý do gì đó hộp thoại đóng mà không chọn được, mặc định phong Hậu
            return PieceType.Queen;
        }

        private static PieceType? PromotionCharToPieceType(char c)
        {
            c = char.ToLowerInvariant(c);
            return c switch
            {
                'q' => PieceType.Queen,
                'r' => PieceType.Rook,
                'b' => PieceType.Bishop,
                'n' => PieceType.Knight,
                _ => null
            };
        }

        private static string PieceTypeToPromotionChar(PieceType type)
        {
            if (type == PieceType.Queen) return "q";
            if (type == PieceType.Rook) return "r";
            if (type == PieceType.Bishop) return "b";
            if (type == PieceType.Knight) return "n";
            return null;
        }

        private class PromotionDialog : Form
        {
            public PieceType SelectedPieceType { get; private set; }

            private readonly Label _hoverLabel;
            private readonly List<Panel> _panels = new List<Panel>();
            private readonly Color _basePanelColor;
            private readonly Color _highlightColor;

            public PromotionDialog(bool isWhite, Image rookImage, Image knightImage, Image bishopImage, Image queenImage, Color highlightColor)
            {
                _highlightColor = highlightColor;
                _basePanelColor = Color.FromArgb(118, 74, 61);
                SelectedPieceType = PieceType.Queen;

                Text = "Phong cấp";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ControlBox = false;
                BackColor = _basePanelColor;
                ClientSize = new Size(520, 260); // rộng + cao hơn chút cho thoáng

                // Tiêu đề trên cùng
                var title = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Text = "Hãy chọn 1 trong 4 quân bên dưới"
                };

                // Panel trung tâm chứa 4 quân
                var container = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = _basePanelColor
                };

                Controls.Add(container);
                Controls.Add(title);

                // Label lớn ở dưới cùng để hiển thị tên quân khi hover
                _hoverLabel = new Label
                {
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 223, 186),
                    Text = "Di chuột tới quân muốn phong cấp"
                };
                container.Controls.Add(_hoverLabel);

                // Tạo 4 panel quân cờ
                var images = new[] { rookImage, knightImage, bishopImage, queenImage };
                var names = new[] { "Xe", "Mã", "Tượng", "Hậu" };
                var types = new[] { PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen };

                int panelWidth = 100;
                int panelHeight = 130;
                int spacing = 12;

                int totalWidth = panelWidth * 4 + spacing * 3;
                int startX = (ClientSize.Width - totalWidth) / 2;
                int top = 10;

                for (int i = 0; i < 4; i++)
                {
                    int left = startX + i * (panelWidth + spacing);
                    AddPieceOption(container, left, top, names[i], images[i], types[i], panelWidth, panelHeight);
                }
            }

            private void AddPieceOption(
                Panel container,
                int left,
                int top,
                string name,
                Image img,
                PieceType pieceType,
                int panelWidth,
                int panelHeight)
            {
                var panel = new Panel
                {
                    Width = panelWidth,
                    Height = panelHeight,
                    Left = left,
                    Top = top,
                    BackColor = _basePanelColor,
                    Cursor = Cursors.Hand
                };
                int picWidth = 72;
                int picHeight = 72;
                int picTop = 10;
                var picture = new PictureBox
                {
                    Width = picWidth,
                    Height = picHeight,
                    Left = (panelWidth - picWidth) / 2,
                    Top = picTop,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = img,
                    Cursor = Cursors.Hand
                };

                // Tên quân (Xe / Mã / Tượng / Hậu) ở dưới
                var label = new Label
                {
                    AutoSize = false,
                    Width = panelWidth,
                    Height = 24,
                    Left = 0,
                    Top = panelHeight - 24 - 6,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 223, 186),
                    Text = name,
                    Cursor = Cursors.Hand
                };

                panel.Controls.Add(picture);
                panel.Controls.Add(label);
                container.Controls.Add(panel);

                _panels.Add(panel);

                void handleHover(object sender, EventArgs e)
                {
                    HighlightPanel(panel);
                    _hoverLabel.Text = $"Phong thành {name}";
                }

                void handleClick(object sender, EventArgs e)
                {
                    SelectedPieceType = pieceType;
                    DialogResult = DialogResult.OK;
                    Close();
                }

                panel.MouseEnter += handleHover;
                picture.MouseEnter += handleHover;
                label.MouseEnter += handleHover;

                panel.Click += handleClick;
                picture.Click += handleClick;
                label.Click += handleClick;
            }


            private void HighlightPanel(Panel selected)
            {
                foreach (var p in _panels)
                {
                    p.BackColor = p == selected ? _highlightColor : _basePanelColor;
                }
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);

                // Mặc định highlight Hậu (ô cuối cùng) cho quen tay
                if (_panels.Count > 0)
                {
                    HighlightPanel(_panels[_panels.Count - 1]);
                    _hoverLabel.Text = "Phong thành Hậu";
                }
            }
        }
    }
}