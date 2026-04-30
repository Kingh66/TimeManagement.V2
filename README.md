# ⏱️ Student Time Management System

A comprehensive, multi-tier C# application designed to help university students organize their study schedules based on module credits and available time. Originally developed as a WPF desktop application, this project evolved through a full Software Development Lifecycle (SDLC) to include secure SQL database persistence and a final migration to an ASP.NET Core web application.

## 📖 Project Background

**Based on a real-world scenario:** University students often struggle to manage their workloads, leading to last-minute cramming and burnout. This program calculates exactly how many hours of self-study a student must complete per week for each module, tracks their actual study time, and displays the remaining hours required to stay on track.

## 🏗️ Architecture & Tech Stack

This project was developed in three distinct phases to demonstrate progressive technical skills:

*   **Languages:** C#, SQL
*   **Desktop UI:** Windows Presentation Foundation (WPF), XAML
*   **Web UI:** ASP.NET Core MVC / Razor Pages
*   **Database:** Microsoft SQL Server (LocalDB)
*   **Architecture:** Custom Class Library (Reusable Logic), LINQ, Multi-threading (Task-based Asynchronous Pattern)
*   **Security:** Password Hashing, Role-Based Data Isolation

## ✨ Key Features

### Phase 1: Core Logic (WPF & In-Memory)
*   **Module Management:** Add semester modules with Code, Name, Credits, and weekly Class Hours.
*   **Dynamic Calculation:** Automatically calculates required weekly self-study hours using the standard formula: `(Credits * 10 / Weeks) - Class Hours`.
*   **Study Logging:** Record the exact number of hours spent studying a specific module on a specific date.
*   **Remaining Hours Tracker:** Displays how much self-study time is left for the current week based on logged hours.
*   **LINQ Integration:** Extensive use of LINQ to query and manipulate the in-memory module collections.

### Phase 2: Data Persistence & Security
*   **SQL Database Integration:** Migrated from in-memory storage to a robust relational SQL database.
*   **User Authentication:** Implemented a secure registration and login system.
*   **Data Security:** Passwords are hashed before storage; the application never stores plain-text passwords.
*   **Data Isolation:** Implemented strict query filtering ensuring a logged-in user can only see and modify their own semester data.
*   **Responsive UI:** Utilized multi-threading (`async`/`await`) for all database operations to prevent the WPF UI from freezing during data retrieval.

### Phase 3: Web Migration
*   **ASP.NET Core Web App:** Rebuilt the application as a web platform to allow access from any device (phones, tablets, lab computers).
*   **Class Library Reusability:** Successfully ported the core custom class library from Phase 2 to power the web application's backend logic.

## 🚀 Getting Started 

### Prerequisites
*   Visual Studio 2022 with the following workloads:
    *   ASP.NET and web development
    *   .NET desktop development
*   SQL Server LocalDB (Usually installed with Visual Studio)

### Installation Steps (Phase 3 - ASP.NET Core Web App)

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Kingh66/TimeManagement.V2.git](https://github.com/Kingh66/TimeManagement.V2.git)
    ```
2.  **Open the Solution:** Open the `.sln` file in Visual Studio.
3.  **Database Setup:**
    *   If the project uses Entity Framework Core Code-First, open the **Package Manager Console** and run:
        ```powershell
        Update-Database
        ```
    *   If it uses raw SQL scripts, locate the `.sql` file in the project directory and execute it against your LocalDB instance.
4.  **Run the Web App:**
    *   Right-click the ASP.NET Core Web project in the Solution Explorer.
    *   Select **"Set as Startup Project"**.
    *   Press `F5` to launch the web app in your browser.

### Running the Desktop Version (Phase 2)

If you want to run the WPF desktop version instead:
1.  Right-click the WPF project in the Solution Explorer.
2.  Select **"Set as Startup Project"**.
3.  Ensure your connection strings in `app.config` point to your LocalDB instance.
4.  Press `F5`.

## 👨‍💻 Developer Notes

This project was built with a heavy emphasis on **SOLID principles** and clean architecture:
*   By utilizing a **Custom Class Library** for all calculations and data models, the logic remained entirely decoupled from the UI. This made the transition from WPF (Phase 1/2) to ASP.NET Core (Phase 3) incredibly smooth, as the web app simply consumed the existing library.
*   Focus was placed on **user experience**, ensuring that heavy database reads/writes were pushed to background threads, maintaining a fluid UI on the desktop client.

## 📜 License

This project is for academic portfolio purposes.
