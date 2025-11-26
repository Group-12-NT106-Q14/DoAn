# ♟️ Chess Online – Đồ án lập trình mạng căn bản NT106.Q14

> **Đề tài:** Trò chơi cờ vua chơi qua mạng (Chess Online)

---

## 🧾 Thông tin môn học

| Mục                | Thông tin              |
| ------------------ | ---------------------- |
| **Môn học**        | Lập trình mạng căn bản |
| **Lớp**            | NT106.Q14              |
| **Giảng viên**     | ThS. Lê Minh Khánh Hội |
| **Nhóm thực hiện** | Nhóm 12                |
| **Nhóm trưởng**    | Phạm Đức Tài           |

---

## 👨‍💻 Thành viên Nhóm 12

| STT | Họ và tên                    | MSSV     |
| --- | ---------------------------- | -------- |
| 1   | Phạm Đức Tài *(Nhóm trưởng)* | 24521557 |
| 2   | Trần Minh Đức                | 24520331 |
| 3   | Trần Văn Tài                 | 24521560 |
| 4   | Trần Sơn                     | 24521538 |
| 5   | Trần Thanh Nguyên            | 24521213 |

---

## 🔽 Tải game & trải nghiệm nhanh

> Nếu giảng viên hoặc người dùng chỉ muốn **tải về và chơi thử thực tế**, không cần quan tâm đến mã nguồn, hãy dùng **bản client online** bên dưới.

### 1️⃣ Bản client online (khuyên dùng để chơi thử)

* Tải bản **client ChessGame đã build sẵn** tại địa chỉ:

  ```text
  http://chessgame.ddns.net
  ```

* Cách dùng:

  * Tải file client về máy (Windows)
  * Cài đặt / giải nén và chạy chương trình
  * Client sẽ tự động kết nối tới **server đã được nhóm triển khai trên VPS** (đã public port)
  * Người dùng có thể:

    * Đăng ký / đăng nhập tài khoản
    * Tìm trận nhanh với người chơi khác
    * Tạo phòng, rủ bạn bè vào chơi
    * Trải nghiệm đầy đủ các chức năng online **mà không cần tự khởi chạy server**

> Đây là cách phù hợp nhất để **thầy/cô và bạn bè trải nghiệm game thực tế**.

### 2️⃣ Bản source code (chạy localhost từ GitHub)

* Dành cho những ai muốn **xem mã nguồn, chỉnh sửa, chạy thử trên localhost**.
* Bản này gồm đầy đủ:

  * Ứng dụng server (console) dùng làm máy chủ
  * Ứng dụng client (WinForms) dùng làm game cho người chơi

Chi tiết cách chạy từ source được mô tả ở phần **"🚀 Hướng dẫn chạy thử"** bên dưới.

---

## 📌 Giới thiệu đề tài

**Chess Online** là một ứng dụng cờ vua cho phép người chơi **đấu với nhau qua mạng Internet**.

Người dùng có thể:

* Đăng ký tài khoản và đăng nhập
* Quên mật khẩu và khôi phục bằng **mã OTP gửi qua email**
* Tìm trận nhanh với người chơi ngẫu nhiên trên hệ thống
* Tạo phòng riêng và mời người khác cùng chơi
* Chơi cờ thời gian thực trên bàn cờ đồ họa, sử dụng quân cờ **SVG** và có **hiệu ứng âm thanh**
* Xem lại lịch sử các ván đã chơi và bảng xếp hạng người chơi

Mục tiêu chính của đồ án:

* Vận dụng kiến thức **lập trình mạng với TCP Socket**
* Thiết kế **giao thức trao đổi dữ liệu** giữa client và server
* Xây dựng **kiến trúc client–server nhiều người dùng**
* Kết hợp **WinForms + SQLite** trong một ứng dụng có tính tương tác cao

---

## 🏛 Kiến trúc hệ thống

Hệ thống được chia thành **hai phần chính**:

### 🖥 Ứng dụng server (ChessServer)

* Chạy dưới dạng **ứng dụng console** trên máy chủ / VPS.
* Đóng vai trò **máy chủ trung tâm**, xử lý:

  * Tài khoản (đăng ký, đăng nhập, quên mật khẩu, cập nhật thông tin)
  * Tổ chức các trận đấu (tìm trận nhanh, tạo phòng, tham gia phòng)
  * Điều phối ván cờ giữa hai người chơi
  * Lưu lịch sử trận đấu và thống kê Elo
* Lưu trữ dữ liệu vào **cơ sở dữ liệu SQLite**.

### 💻 Ứng dụng client (ChessGame)

* Chạy dưới dạng **ứng dụng WinForms trên Windows**.
* Đóng vai trò **ứng dụng người chơi**:

  * Cho phép người dùng thao tác thông qua giao diện đồ họa
  * Kết nối tới server qua TCP
  * Gửi yêu cầu (đăng nhập, tìm trận, đi quân, ...) và hiển thị kết quả trả về

### Mô hình tổng quan

```text
Người chơi  ⇄  Ứng dụng ChessGame (Client)  ⇄  Kết nối TCP  ⇄  Ứng dụng ChessServer (Server)  ⇄  CSDL SQLite
```

---

## 🧑‍🎓 Trải nghiệm người dùng & các chức năng

### 1. Tài khoản & bảo mật

Hệ thống cung cấp đầy đủ các chức năng về tài khoản:

* **Đăng ký tài khoản mới**

  * Nhập email, tên hiển thị, tên đăng nhập và mật khẩu
  * Hệ thống kiểm tra trùng email / tên đăng nhập
  * Tài khoản hợp lệ được lưu trong cơ sở dữ liệu với điểm Elo khởi tạo

* **Đăng nhập**

  * Đăng nhập bằng tên đăng nhập và mật khẩu
  * Nếu đúng thông tin, người dùng được chuyển vào giao diện chính

* **Quên mật khẩu & khôi phục bằng OTP**

  * Nhập email đã đăng ký
  * Hệ thống gửi mã OTP tới email
  * Nhập lại mã OTP và đặt mật khẩu mới

* **Chỉnh sửa thông tin tài khoản**

  * Cho phép thay đổi email, tên hiển thị, mật khẩu
  * Thay đổi được đồng bộ lên server và lưu vào cơ sở dữ liệu

---

### 2. Dashboard – sảnh chính

Sau khi đăng nhập, người dùng được đưa đến **Dashboard**, nơi:

* Hiển thị thông tin cơ bản của tài khoản
* Cung cấp các nút/chức năng điều hướng tới:

  * Tìm trận nhanh (Quick Match)
  * Danh sách phòng chơi (Room List)
  * Cài đặt tài khoản (Account Setting)
  * Lịch sử trận đấu (History)
  * Bảng xếp hạng (Ranking)

Dashboard hoạt động như **"sảnh chờ"** trong các trò chơi online, là điểm xuất phát cho mọi hoạt động.

---

### 3. Tìm trận nhanh (Quick Match)

Chức năng dành cho người chơi muốn **vào trận nhanh nhất**:

* Người dùng bấm tìm trận nhanh
* Hệ thống đưa người dùng vào **hàng chờ ghép trận**
* Khi có hai người cùng chờ:

  * Hệ thống tự động ghép cặp
  * Tạo một ván đấu mới
  * Gán màu cờ cho mỗi bên (Trắng / Đen)
  * Mở bàn cờ để bắt đầu chơi
* Trong thời gian chờ, người chơi có thể **hủy tìm trận** nếu không muốn tiếp tục.

---

### 4. Phòng chơi (Room)

Đối với những ai thích tùy chỉnh nhiều hơn, hệ thống cung cấp **chế độ chơi qua phòng**:

* **Tạo phòng mới**

  * Người dùng đặt tên phòng
  * Chọn thời gian ván đấu (số phút, thời gian cộng thêm)
  * Trở thành chủ phòng

* **Danh sách phòng**

  * Hiển thị danh sách các phòng đang mở
  * Mỗi phòng có thể hiển thị tên phòng, chủ phòng, số người tham gia
  * Người chơi có thể chọn phòng phù hợp để tham gia

* **Bên trong phòng**

  * Hiển thị danh sách người chơi trong phòng
  * Hỗ trợ chat giữa các thành viên
  * Người chơi bấm **"sẵn sàng"** khi đã chuẩn bị xong
  * Khi điều kiện đủ, **chủ phòng có thể bấm bắt đầu trận** → hệ thống tạo ván đấu và chuyển hai người vào bàn cờ

---

### 5. Bàn cờ (GameBoard)

Đây là điểm nhấn chính của ứng dụng – nơi diễn ra ván cờ giữa hai người chơi.

* Bàn cờ chuẩn 8x8 với màu ô rõ ràng
* Quân cờ được hiển thị bằng **hình ảnh SVG**, sắc nét trên mọi độ phân giải
* Người dùng có thể chọn quân, đi quân theo luật cờ vua, xem phản ứng của đối thủ theo thời gian thực

Các tính năng hỗ trợ trên bàn cờ:

* **Thời gian thi đấu**

  * Mỗi bên có một đồng hồ đếm ngược
  * Có thể cài đặt thêm thời gian cộng thêm mỗi nước đi (increment)

* **Âm thanh trong game**

  * Âm thanh khi di chuyển quân
  * Âm thanh khi ăn quân
  * Âm thanh khi chiếu tướng
  * Âm thanh thông báo đã tìm được trận đấu

* **Kết thúc ván đấu**

  * Ván đấu kết thúc khi một trong các bên thắng, thua, hòa hoặc hết thời gian
  * Hệ thống tính toán kết quả
  * Điểm Elo của mỗi người được cập nhật
  * Thông tin ván đấu được lưu vào lịch sử

---

### 6. Lịch sử trận đấu (History)

Phần **lịch sử** giúp người chơi:

* Xem lại danh sách các ván đã chơi trước đó
* Mỗi dòng lịch sử có thể hiển thị:

  * Đối thủ
  * Kết quả (thắng/thua/hòa)
  * Thời gian ván đấu
  * Thay đổi Elo (tùy mức triển khai)

Qua đó, người chơi có thể **theo dõi tiến bộ của bản thân** theo thời gian.

---

### 7. Bảng xếp hạng (Ranking)

Hệ thống lưu lại **điểm Elo và thống kê** cho từng tài khoản.

Trong mục **bảng xếp hạng**, người dùng có thể:

* Xem danh sách những người chơi có Elo cao nhất
* So sánh thành tích của mình với người khác

Bảng xếp hạng tạo nên **tính cạnh tranh và động lực** để người chơi tiếp tục luyện tập.

---

## 🛠 Công nghệ sử dụng

* **Ngôn ngữ:** C#
* **Nền tảng:** .NET 8
* **Giao diện:** Windows Forms (WinForms)
* **Giao tiếp mạng:** TCP Socket
* **Cơ sở dữ liệu:** SQLite

Ngoài ra, nhóm còn sử dụng:

* Thư viện xử lý **logic cờ vua**
* Thư viện gửi **email** phục vụ chức năng quên mật khẩu
* Thư viện **SVG** để hiển thị quân cờ rõ nét

---

## 🚀 Hướng dẫn chạy thử từ source (localhost)

Phần này dành cho những ai muốn **tự chạy server + client từ mã nguồn GitHub**.

1. **Chuẩn bị môi trường**

   * Cài đặt .NET 8 SDK / Runtime
   * Cài đặt Visual Studio 2022 (hoặc IDE tương đương)

2. **Mở solution**

   * Clone project từ GitHub
   * Mở solution trong Visual Studio

3. **Chạy server**

   * Chọn dự án server làm startup project
   * Chạy chương trình

4. **Chạy client**

   * Chọn dự án client làm startup project
   * Chạy chương trình
   * Sử dụng nhiều instance để mô phỏng nhiều người chơi

5. **Thử nghiệm chức năng**

   * Đăng ký & đăng nhập tài khoản
   * Thử tìm trận nhanh
   * Thử tạo phòng, vào phòng, bấm sẵn sàng, bắt đầu ván cờ
   * Thử chơi cờ, xem lịch sử, xem bảng xếp hạng

> Gợi ý: Khi trình bày đồ án, có thể vừa **mở bản client online để demo chơi thật**, vừa mở **source code trên Visual Studio** để giải thích kiến trúc và cách cài đặt.

---

## 🎯 Mục đích & ý nghĩa đồ án

Đồ án **"Trò chơi cờ vua chơi qua mạng"** giúp nhóm:

* Áp dụng kiến thức **lập trình mạng** vào một bài toán thực tế, có tương tác người dùng rõ ràng
* Hiểu cách thiết kế và triển khai **hệ thống client–server** nhiều người dùng
* Kết hợp nhiều mảng kiến thức:

  * Giao diện người dùng (WinForms)
  * Logic game (cờ vua)
  * Lưu trữ và truy vấn dữ liệu (SQLite)
  * Truyền thông qua mạng (TCP Socket)
* Rèn luyện kỹ năng **làm việc nhóm, phân chia công việc, tích hợp và kiểm thử**

Sản phẩm được thực hiện với mục đích **học tập** trong khuôn khổ môn học *Lập trình mạng căn bản – NT106.Q14*.
Nhóm rất mong nhận được góp ý từ giảng viên và các bạn để hệ thống ngày càng hoàn thiện hơn.