# Changelog

Semua perubahan penting pada proyek ini akan didokumentasikan di file ini.

Format mengacu pada [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
dan proyek ini mengikuti [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.0.0] — 2026-02-17

Rilis pertama backend sistem peminjaman ruangan kampus.

### Added

- **Room API**
  - `GET /api/room` — Endpoint untuk mengambil seluruh data ruangan
  - `GET /api/room/{id}` — Endpoint untuk mengambil detail ruangan berdasarkan ID

- **Room Booking API**
  - `GET /api/roombooking` — Endpoint untuk mengambil seluruh data peminjaman
  - `GET /api/roombooking/{id}` — Endpoint untuk mengambil detail peminjaman berdasarkan ID
  - `GET /api/roombooking/search` — Endpoint pencarian peminjaman berdasarkan nama pengguna, ruangan, atau tanggal
  - `POST /api/roombooking` — Endpoint untuk membuat peminjaman baru
  - `PUT /api/roombooking` — Endpoint untuk memperbarui data peminjaman yang ada
  - `DELETE /api/roombooking` — Endpoint untuk menghapus data peminjaman
  - `PUT /api/roombooking/{id}/status` — Endpoint untuk mengubah status peminjaman

- **Manajemen Status**
  - Alur status peminjaman: *Menunggu* → *Disetujui* / *Ditolak*
  - Penyimpanan riwayat setiap perubahan status

- **Database**
  - Integrasi SQL Server menggunakan Entity Framework Core
  - Migrasi awal skema database (initial migration)

---
