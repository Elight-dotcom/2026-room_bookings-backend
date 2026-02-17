# 🏛️ Room Bookings — Backend

> **Version:** `1.0.0`  
> **Platform:** ASP.NET Core  
> **Database:** SQL Server (via Entity Framework Core)

Layanan backend untuk sistem peminjaman ruangan kampus.

---

## Gambaran Umum

Sistem ini dibangun untuk menggantikan pencatatan peminjaman ruangan kampus yang sebelumnya dilakukan secara manual (via chat, catatan, atau spreadsheet). Backend menyediakan tiga fitur inti:

1. **Pencatatan Peminjaman** — CRUD lengkap untuk data peminjaman ruangan.
2. **Pengelolaan Status** — Alur status peminjaman (menunggu → disetujui / ditolak) beserta riwayat perubahannya.
3. **Riwayat & Penelusuran** — Pencarian dan filter data peminjaman berdasarkan nama pengguna, ruangan, atau tanggal.

---

## Arsitektur Sistem

```
┌─────────────────────┐     ┌─────────────────────┐
│  Frontend           │     │  Mobile             │
│  React + TypeScript │     │  Flutter            │
└────────┬────────────┘     └──────────┬──────────┘
         │                             │
         └──────────────┬──────────────┘
                        ▼
              ┌─────────────────┐
              │  Backend        │  ◀── Anda berada di sini
              │  ASP.NET Core   │
              └────────┬────────┘
                       ▼
              ┌─────────────────┐
              │  Database       │
              │  SQL Server     │
              └─────────────────┘
```

---

## Teknologi

| Komponen | Teknologi |
|---|---|
| Framework | ASP.NET Core |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Bahasa | C# |

---

## Prasyarat

Pastikan perangkat Anda telah terinstal:

- [.NET SDK](https://dotnet.microsoft.com/download) (versi sesuai proyek)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) atau SQL Server Express
- [EF Core CLI Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

---

## Instalasi & Menjalankan Proyek

### 1. Clone repositori

```bash
git clone https://github.com/Elight-dotcom/2026-room_bookings-backend.git
cd RoomBookingsApi/
```

### 2. Konfigurasi koneksi database

Buka file `appsettings.json` dan sesuaikan connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RoomBookingsDb;Trusted_Connection=True;"
  }
}
```

### 3. Jalankan migrasi database

```bash
dotnet ef database update
```

### 4. Jalankan aplikasi

```bash
dotnet run
```

---

## API Reference

Base URL: `/api`

### 🚪 Room

| Method | Endpoint | Deskripsi |
|:---:|:---|:---|
| `GET` | `/api/room` | Mengambil seluruh data ruangan |
| `GET` | `/api/room/{id}` | Mengambil detail ruangan berdasarkan ID |

### 📅 Room Booking

| Method | Endpoint | Deskripsi |
|:---:|:---|:---|
| `GET` | `/api/roombooking` | Mengambil seluruh data peminjaman |
| `GET` | `/api/roombooking/{id}` | Mengambil detail peminjaman berdasarkan ID |
| `GET` | `/api/roombooking/search` | Mencari peminjaman berdasarkan nama pengguna, ruangan, atau tanggal |
| `POST` | `/api/roombooking` | Membuat peminjaman baru |
| `PUT` | `/api/roombooking` | Memperbarui data peminjaman |
| `DELETE` | `/api/roombooking` | Menghapus data peminjaman |
| `PUT` | `/api/roombooking/{id}/status` | Mengubah status peminjaman |

### Status Peminjaman

```
Menunggu  ───▶  Disetujui
     │
     ├───────▶  Ditolak
     │
     └───────▶  Dicancel
```

---

## Struktur Proyek

```
/
├── Controllers/          # Endpoint API
├── Models/               # Entity & DTO
├── Data/                 # DbContext & migrasi EF Core
├── DTOs/                 # Data Transfer Object
├── Services/             # Business logic
├── Middleware/           # Global error handling
├── appsettings.json      # Konfigurasi aplikasi
└── Program.cs            # Entry point
```

---

## Error Handling

Backend mengimplementasikan **global error handling middleware** agar setiap exception ditangkap secara terpusat dan menghasilkan respons yang konsisten.

Contoh format respons error:

```json
{
  "status": 400,
  "message": "Data peminjaman tidak valid."
}
```

---

## Catatan Versi

### v1.0.0 — Rilis Pertama

**Fitur yang tersedia:**

- CRUD peminjaman ruangan
- Manajemen status peminjaman beserta riwayat perubahan
- Pencarian dan filter peminjaman berdasarkan nama pengguna, ruangan, dan tanggal

**Tantangan yang ditemui & solusi:**

- *Error handling di .NET* → Diselesaikan dengan global middleware

---

> Dibuat sebagai tugas individu Product Base Learning.
