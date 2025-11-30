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

        // Màu cho panel người chơi & đồng hồ
        private readonly Color PlayerPanelBaseColor = Color.FromArgb(118, 74, 61);
        private readonly Color PlayerInactiveTimeBackColor = Color.FromArgb(230, 230, 230);
        private readonly Color PlayerInactiveTimeForeColor = Color.Gray;

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

        // Trạng thái highlight lượt đi
        private bool _isPlayer1Active;
        private bool _isPlayer2Active;

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

            // vẽ viền vàng cho panel người chơi khi tới lượt
            if (pnlPlayer1 != null)
                pnlPlayer1.Paint += PlayerPanel_Paint;
            if (pnlPlayer2 != null)
                pnlPlayer2.Paint += PlayerPanel_Paint;
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

            ClearMoveHistory();
            UpdateBoardUi();
            UpdateTimeLabels();
            UpdateTurnHighlight();
        }

        private void PlayerPanel_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not Panel panel) return;

            bool isActive = panel == pnlPlayer1
                ? _isPlayer1Active
                : panel == pnlPlayer2 && _isPlayer2Active;

            if (!isActive) return;

            using (var pen = new Pen(SelectedSquareColor, 3))
            {
                var rect = panel.ClientRectangle;
                rect.Inflate(-2, -2);
                e.Graphics.DrawRectangle(pen, rect);
            }
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
            int labelSize = 16;

            // ===== HÀNG NGANG: file a–h =====
            for (int col = 0; col < BoardSize; col++)
            {
                char fileChar;

                if (_localIsWhite)
                {
                    fileChar = (char)('a' + col);
                }
                else
                {
                    fileChar = (char)('a' + (BoardSize - 1 - col));
                }

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

                int x = pnlChessBoard.Left + col * squareSize + (squareSize - labelSize) / 2;
                int y = pnlChessBoard.Bottom + 2;

                lbl.Location = new Point(x, y);
                this.Controls.Add(lbl);
                lbl.BringToFront();
            }

            // ===== HÀNG DỌC: rank 1–8 =====
            for (int row = 0; row < BoardSize; row++)
            {
                int rankNumber;

                if (_localIsWhite)
                {
                    rankNumber = BoardSize - row;
                }
                else
                {
                    rankNumber = row + 1;
                }

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

                int y = pnlChessBoard.Top + row * squareSize + (squareSize - labelSize) / 2;
                int x = pnlChessBoard.Left - labelSize - 4;

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

            // 2) En passant
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

        // ========== THỰC HIỆN NƯỚC ĐI LOCAL (có SAN) ==========
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
            // Nếu đây là nước phong cấp
            if (move.IsPromotion)
            {
                bool isWhite = _board.Turn == PieceColor.White;

                // Hiện dialog. Nếu người chơi đóng X → không đi nước này.
                if (!TryShowPromotionDialog(isWhite, out var selectedType))
                {
                    // Không thực hiện nước đi, quân vẫn nguyên chỗ, selection vẫn giữ
                    return false;
                }

                var promotionMove = candidateMoves
                    .FirstOrDefault(m => m.Promotion != null && m.Promotion.Type == selectedType);

                if (promotionMove != null)
                    move = promotionMove;

                promo = PieceTypeToPromotionChar(selectedType);
            }


            if (!move.HasValue) return false;

            PieceColor sideMoved = _board.Turn;

            var fromPos = move.OriginalPosition;
            var movingPiece = _board[fromPos];
            if (movingPiece == null) return false;

            bool isCapture = IsCaptureMove(fromPos.X, fromPos.Y, targetX, targetY);

            bool isCastleKingSide =
                movingPiece.Type == PieceType.King &&
                (targetX - fromPos.X) == 2;

            bool isCastleQueenSide =
                movingPiece.Type == PieceType.King &&
                (fromPos.X - targetX) == 2;

            string disambiguation = GetDisambiguation(legalMoves, move, movingPiece);

            _lastMoveFrom = new Point(fromPos.X, fromPos.Y);
            _lastMoveTo = new Point(targetX, targetY);

            _board.Move(move);
            ApplyIncrement(sideMoved);

            bool isCheck = IsCheckForOpponent(sideMoved);
            bool isMate = isCheck &&
                          _board.IsEndGame &&
                          _board.EndGame != null &&
                          _board.EndGame.WonSide == sideMoved;

            string sanText = FormatMoveSan(
                movingPiece,
                fromPos.X, fromPos.Y,
                targetX, targetY,
                isCapture,
                isCastleKingSide,
                isCastleQueenSide,
                promo,
                disambiguation,
                isCheck,
                isMate);

            PlayMoveSound(isCapture, isCheck);

            AddMoveToHistory(sideMoved, sanText);
            UpdateTimeLabels();

            try
            {
                LocalMovePlayed?.Invoke(fromSq, toSq, promo);
            }
            catch { }

            return true;
        }

        /// <summary>
        /// Áp dụng nước đi từ server gửi xuống (ghi SAN).
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

            var fromPos = move.OriginalPosition;
            var movingPiece = _board[fromPos];
            if (movingPiece == null) return;

            bool isCapture = IsCaptureMove(sx, sy, tx, ty);

            bool isCastleKingSide =
                movingPiece.Type == PieceType.King &&
                (tx - fromPos.X) == 2;

            bool isCastleQueenSide =
                movingPiece.Type == PieceType.King &&
                (fromPos.X - tx) == 2;

            string disambiguation = GetDisambiguation(legal, move, movingPiece);

            _lastMoveFrom = new Point(sx, sy);
            _lastMoveTo = new Point(tx, ty);

            _board.Move(move);
            ApplyIncrement(sideMoved);

            bool isCheck = IsCheckForOpponent(sideMoved);
            bool isMate = isCheck &&
                          _board.IsEndGame &&
                          _board.EndGame != null &&
                          _board.EndGame.WonSide == sideMoved;

            string sanText = FormatMoveSan(
                movingPiece,
                fromPos.X, fromPos.Y,
                tx, ty,
                isCapture,
                isCastleKingSide,
                isCastleQueenSide,
                promotion,
                disambiguation,
                isCheck,
                isMate);

            PlayMoveSound(isCapture, isCheck);

            AddMoveToHistory(sideMoved, sanText);
            UpdateTimeLabels();

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

            // highlight nước đi cuối
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

            if (_selectedBoardX != -1)
            {
                var selectedBtn = GetButtonAt(_selectedBoardX, _selectedBoardY);
                if (selectedBtn != null)
                {
                    selectedBtn.BackColor = SelectedSquareColor;
                }
            }

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

        private void ClearMoveHistory()
        {
            if (lvMoveHistory == null) return;
            lvMoveHistory.Items.Clear();
        }

        // ===== Helper SAN =====

        // Ký tự quân cho SAN (Vua, Hậu, Xe, Tượng, Mã). Tốt thì rỗng.
        private static string PieceTypeToSanLetter(PieceType type)
        {
            if (type == PieceType.King) return "K";
            if (type == PieceType.Queen) return "Q";
            if (type == PieceType.Rook) return "R";
            if (type == PieceType.Bishop) return "B";
            if (type == PieceType.Knight) return "N";
            return string.Empty; // Tốt hoặc kiểu khác
        }

        // Disambiguation: khi có 2+ quân cùng loại có thể tới cùng ô.
        private string GetDisambiguation(IEnumerable<Move> legalMoves, Move move, Piece movingPiece)
        {
            if (movingPiece == null || movingPiece.Type == PieceType.Pawn)
                return string.Empty;

            var origin = move.OriginalPosition;
            var target = move.NewPosition;

            var samePieceMoves = legalMoves
                .Where(m =>
                {
                    if (m.NewPosition.X != target.X || m.NewPosition.Y != target.Y)
                        return false;
                    if (m.OriginalPosition.X == origin.X && m.OriginalPosition.Y == origin.Y)
                        return false;

                    var p = _board[m.OriginalPosition];
                    return p != null &&
                           p.Type == movingPiece.Type &&
                           p.Color == movingPiece.Color;
                })
                .ToList();

            if (samePieceMoves.Count == 0)
                return string.Empty;

            char fileChar = (char)('a' + origin.X);
            char rankChar = (char)('1' + origin.Y);

            bool anySameFile = samePieceMoves.Any(m => m.OriginalPosition.X == origin.X);
            bool anySameRank = samePieceMoves.Any(m => m.OriginalPosition.Y == origin.Y);

            if (!anySameFile) return fileChar.ToString();
            if (!anySameRank) return rankChar.ToString();
            return fileChar.ToString() + rankChar.ToString();
        }

        // Format 1 nước đi theo chuẩn SAN
        private string FormatMoveSan(
            Piece movingPiece,
            int fromX, int fromY,
            int toX, int toY,
            bool isCapture,
            bool isCastleKingSide,
            bool isCastleQueenSide,
            string promotion,
            string disambiguation,
            bool isCheck,
            bool isCheckmate)
        {
            if (movingPiece == null) return string.Empty;

            // Nhập thành
            if (isCastleKingSide)
                return "O-O" + (isCheckmate ? "#" : isCheck ? "+" : string.Empty);

            if (isCastleQueenSide)
                return "O-O-O" + (isCheckmate ? "#" : isCheck ? "+" : string.Empty);

            var sb = new StringBuilder();
            bool isPawn = movingPiece.Type == PieceType.Pawn;

            if (!isPawn)
            {
                sb.Append(PieceTypeToSanLetter(movingPiece.Type));
                if (!string.IsNullOrEmpty(disambiguation))
                    sb.Append(disambiguation);
            }
            else
            {
                // Tốt ăn quân: ghi file xuất phát
                if (isCapture)
                {
                    char fromFile = (char)('a' + fromX);
                    sb.Append(fromFile);
                }
            }

            if (isCapture)
                sb.Append("x");

            // Ô đích
            char toFileChar = (char)('a' + toX);
            char toRankChar = (char)('1' + toY);
            sb.Append(toFileChar);
            sb.Append(toRankChar);

            // Phong cấp
            if (!string.IsNullOrEmpty(promotion))
            {
                sb.Append("=");
                sb.Append(char.ToUpperInvariant(promotion[0])); // q/r/b/n -> Q/R/B/N
            }

            // Chiếu / chiếu bí
            if (isCheckmate)
                sb.Append("#");
            else if (isCheck)
                sb.Append("+");

            return sb.ToString();
        }

        // Lưu SAN vào ListView
        private void AddMoveToHistory(PieceColor sideMoved, string sanText)
        {
            if (lvMoveHistory == null) return;
            if (string.IsNullOrWhiteSpace(sanText)) return;

            bool whiteMove = sideMoved == PieceColor.White;

            if (whiteMove)
            {
                int moveNumber = lvMoveHistory.Items.Count + 1;
                var item = new ListViewItem(moveNumber.ToString());
                item.SubItems.Add(sanText);      // cột Trắng
                item.SubItems.Add(string.Empty); // cột Đen
                lvMoveHistory.Items.Add(item);
            }
            else
            {
                if (lvMoveHistory.Items.Count == 0)
                {
                    var item = new ListViewItem("1");
                    item.SubItems.Add(string.Empty);
                    item.SubItems.Add(sanText);
                    lvMoveHistory.Items.Add(item);
                }
                else
                {
                    var lastItem = lvMoveHistory.Items[lvMoveHistory.Items.Count - 1];
                    while (lastItem.SubItems.Count < 3)
                    {
                        lastItem.SubItems.Add(string.Empty);
                    }

                    if (string.IsNullOrEmpty(lastItem.SubItems[2].Text))
                    {
                        lastItem.SubItems[2].Text = sanText;
                    }
                    else
                    {
                        int moveNumber = lvMoveHistory.Items.Count + 1;
                        var item = new ListViewItem(moveNumber.ToString());
                        item.SubItems.Add(string.Empty);
                        item.SubItems.Add(sanText);
                        lvMoveHistory.Items.Add(item);
                    }
                }
            }

            if (lvMoveHistory.Items.Count > 0)
            {
                lvMoveHistory.EnsureVisible(lvMoveHistory.Items.Count - 1);
            }
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
                    UpdateTimeLabels();
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
                    UpdateTimeLabels();
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

        private static string FormatTimeSpan(TimeSpan t)
        {
            if (t < TimeSpan.Zero) t = TimeSpan.Zero;
            return $"{(int)t.TotalMinutes:00}:{t.Seconds:00}";
        }

        private void UpdateTimeLabels()
        {
            if (_board == null) return;
            if (lblPlayer1Time == null || lblPlayer2Time == null) return;

            if (_localIsWhite)
            {
                lblPlayer1Time.Text = FormatTimeSpan(_whiteTime); // player1 = Trắng
                lblPlayer2Time.Text = FormatTimeSpan(_blackTime); // player2 = Đen
            }
            else
            {
                lblPlayer1Time.Text = FormatTimeSpan(_blackTime); // player1 = Đen
                lblPlayer2Time.Text = FormatTimeSpan(_whiteTime); // player2 = Trắng
            }
        }

        private void SetTimeLabelActive(Label label, bool isActive)
        {
            if (label == null) return;

            label.Enabled = isActive;
            if (isActive)
            {
                label.BackColor = Color.White;
                label.ForeColor = Color.Black;
            }
            else
            {
                label.BackColor = PlayerInactiveTimeBackColor;
                label.ForeColor = PlayerInactiveTimeForeColor;
            }
        }

        private void UpdateTurnHighlight()
        {
            if (_board == null) return;

            // panel & label của bên trắng / đen
            Panel whitePanel, blackPanel;
            Label whiteTimeLabel, blackTimeLabel;

            if (_localIsWhite)
            {
                whitePanel = pnlPlayer1;
                whiteTimeLabel = lblPlayer1Time;
                blackPanel = pnlPlayer2;
                blackTimeLabel = lblPlayer2Time;
            }
            else
            {
                whitePanel = pnlPlayer2;
                whiteTimeLabel = lblPlayer2Time;
                blackPanel = pnlPlayer1;
                blackTimeLabel = lblPlayer1Time;
            }

            bool whiteTurn = _board.Turn == PieceColor.White;

            _isPlayer1Active = false;
            _isPlayer2Active = false;

            if (whitePanel == pnlPlayer1) _isPlayer1Active = whiteTurn;
            if (whitePanel == pnlPlayer2) _isPlayer2Active = whiteTurn;
            if (blackPanel == pnlPlayer1) _isPlayer1Active = !whiteTurn;
            if (blackPanel == pnlPlayer2) _isPlayer2Active = !whiteTurn;

            SetTimeLabelActive(whiteTimeLabel, whiteTurn);
            SetTimeLabelActive(blackTimeLabel, !whiteTurn);

            pnlPlayer1?.Invalidate();
            pnlPlayer2?.Invalidate();
        }

        private void UpdateTurnLabel()
        {
            // Không còn label "Lượt của", chỉ cập nhật highlight + enable clock
            UpdateTurnHighlight();
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

            Close();
        }

        public void NotifyOpponentDisconnectedWin()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            PieceColor localColor = _localIsWhite ? PieceColor.White : PieceColor.Black;
            string resultStr = localColor == PieceColor.White ? "white" : "black";
            string reasonStr = "disconnect";

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

            _lastResignWasDisconnect = false;

            try
            {
                LocalResignRequested?.Invoke();
            }
            catch
            {
            }

            FinishGameByResign(localResigned: true);
        }

        private async void btnOfferDraw_Click(object sender, EventArgs e)
        {
                if (_board == null || _isGameOver) return;

            //Cho nut sleep khoan 120s
            btnOfferDraw.Enabled = false;

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

                MessageBox.Show(this,
                    "Bạn đã gửi lời đề nghị hòa. Vui lòng chờ đối thủ trả lời.",
                    "Cầu hòa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            await Task.Delay(120000); // Chờ 5 giây, tùy chỉnh
            btnOfferDraw.Enabled = true; // Bật lại
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
                if (isCapture && isCheck && _captureSound != null && _checkSound != null)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            _captureSound.PlaySync();
                            System.Threading.Thread.Sleep(100);
                            _checkSound.Play();
                        }
                        catch
                        {
                        }
                    });
                    return;
                }

                if (isCapture && _captureSound != null)
                {
                    _captureSound.Play();
                    return;
                }

                if (isCheck && _checkSound != null)
                {
                    _checkSound.Play();
                    return;
                }

                _moveSound?.Play();
            }
            catch
            {
            }
        }

        private void PlayMoveSound()
        {
            PlayMoveSound(false, false);
        }

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
            rtbChatMessages.ScrollToCaret();
        }

        public void AppendNetworkChat(string username, string content, string time, bool isLocalSender)
        {
            if (rtbChatMessages == null) return;

            if (isLocalSender)
            {
                return;
            }

            string sideText = GetSideTextForUsername(username);
            string line = $"{username} ({sideText}) - {content} [{time}]{Environment.NewLine}";
            rtbChatMessages.AppendText(line);
            rtbChatMessages.SelectionStart = rtbChatMessages.TextLength;
            rtbChatMessages.ScrollToCaret();
        }

        private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isGameOver || _board == null) return;

            _isGameOver = true;
            _clockTimer?.Stop();
            UpdateTimeLabels();

            string resultStr;
            string reasonStr = "disconnect";

            if (_localIsWhite)
            {
                resultStr = "black";
            }
            else
            {
                resultStr = "white";
            }

            _lastResignWasDisconnect = true;

            try
            {
                LocalResignRequested?.Invoke();
            }
            catch { }

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

            Close();
        }

        // =========== Phong cấp: chọn quân ===========
        // Trả về true nếu người chơi chọn quân (OK),
        // trả về false nếu người chơi đóng X / Cancel → hủy nước đi
        private bool TryShowPromotionDialog(bool isWhite, out PieceType selectedType)
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
                    selectedType = dlg.SelectedPieceType;
                    return true; // người chơi đã chọn quân
                }
            }

            // Người chơi bấm X đóng dialog → hủy
            selectedType = PieceType.Queen; // giá trị này không dùng khi return false
            return false;
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
                ControlBox = true;
                BackColor = _basePanelColor;
                ClientSize = new Size(520, 260);

                var title = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Text = "Hãy chọn 1 trong 4 quân bên dưới"
                };

                var container = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = _basePanelColor
                };

                Controls.Add(container);
                Controls.Add(title);

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

                if (_panels.Count > 0)
                {
                    HighlightPanel(_panels[_panels.Count - 1]);
                    _hoverLabel.Text = "Phong thành Hậu";
                }
            }
        }
    }
}