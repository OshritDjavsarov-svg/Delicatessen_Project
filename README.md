# 🛒 Delicatessen - E-Commerce Platform

> A Full-Stack E-Commerce Web Application for Gourmet Food & Product Ordering built with Angular, C# .NET Core (N-Tier Architecture), and Microsoft SQL Server.

[![Angular](https://img.shields.io/badge/Frontend-Angular%20%7C%20TypeScript-dd0031?style=for-the-badge&logo=angular)](https://angular.io/)
[![.NET Core](https://img.shields.io/badge/Backend-.NET%20Core%20%7C%20C%23-512bd4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/Database-Microsoft%20SQL%20Server-cc292b?style=for-the-badge&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)

---

## ⚖️ Legal Disclaimer & Attribution

> **Important Notice:** This project was built strictly for educational, portfolio, and technical demonstration purposes. The UI/UX layout, product categories, and overall concept were inspired by the official **[Delicatessen TLV](https://www.delitlv.co.il)** platform. 
>
> All original brand assets, trademarks, and design concepts belong exclusively to **Delicatessen TLV**. This project represents an independent, full-stack implementation created entirely from scratch to demonstrate backend architecture, database modeling, and frontend integration.

---

<img width="1280" height="648" alt="1-ezgif com-video-to-gif-converter" src="https://github.com/user-attachments/assets/ef159639-ec69-4a12-ac09-0d5e20f787fd" />

## 📌 Project Overview

**Delicatessen** is an end-to-end e-commerce platform designed for online food ordering and catalog management. The application features a responsive frontend, a layered RESTful API, and a robust relational database schema to handle product management, customer profiles, shopping cart state, and order processing.

---

## 🌟 Key Features

* **Interactive Catalog & Filtering:** Browse products by categories with real-time filtering options.
* **Rich Product Details:** Detailed item views including ingredients, nutritional values, allergen warnings, and specific heating instructions.
* **Nutritional Health Flags:** Visual indicators for products high in sodium (salt), sugar, or saturated fats (`HighQuantity` model).
* **User Accounts & Authentication:** Secure customer registration and login with address management and password hashing.
* **Shopping Cart & Checkout:** Dynamic cart management supporting multi-item orders and total calculation.
* **Order History Tracking:** Relational order tracking system pairing customer profiles with transactional line items.

---

## 🏗 System Architecture & Repository Structure

The project is structured as a **Monorepo** divided into three decoupled tiers:

```text
Delicatessen_Project/
├── frontend/             # Angular Single Page Application (SPA)
├── backend/              # C# .NET Core Web API (N-Tier Architecture)
└── database/             # Microsoft SQL Server DDL Schema Scripts
```

### 1. 🎨 Frontend Architecture & Features (`frontend/`)

The client-side is built as a responsive, modern **Single Page Application (SPA)** using **Angular** and **TypeScript**, focused on modularity, clean UI/UX, and strict end-to-end type safety:

* **Component-Driven Modular Design:** Structured into clean, reusable Angular components (Product Cards, Category Navigation, Product Details Modal, Cart Drawer, Checkout, and User Profile).
* **Strongly-Typed TypeScript Interfaces:** Strictly typed client models (`Product`, `Order`, `OrderItem`, `User`) mirroring backend DTOs to guarantee compile-time safety and seamless JSON payload handling.
* **Dynamic Cart & State Management:** Client-side cart logic managing item additions, quantity adjustments, real-time total price recalculations, and dynamic nutritional warning badges (`HighQuantity`).
* **Reactive Forms & Validation:** Leverages Angular Reactive Forms for customer registration, authentication, and shipping address inputs with real-time field validation.
* **HTTP Services Integration:** Dedicated Angular Services utilizing `HttpClient` to encapsulate RESTful API communication with the C# .NET Core API.
  
### 2. 🧱 Backend Layered Architecture (`backend/`)

The C# backend strictly follows an N-Tier Layered Architecture to enforce separation of concerns, scalability, and maintainability:

* **`Delicatessen` (Presentation Layer):** RESTful API Controllers handling incoming HTTP requests, routing, and HTTP responses.
* **`BLL` (Business Logic Layer):** Core domain logic, validation rules, order total calculations, and business process execution.
* **`DAL` (Data Access Layer):** Direct database integration managing entity persistence, CRUD operations, and SQL execution.
* **`DTOs` (Data Transfer Objects):** Decoupled data contracts ensuring secure and efficient communication between the client and API layers without exposing raw database entities.

---

### 3. 🗄️ Database Design & Schema (`database/`)

The application utilizes Microsoft SQL Server with strict relational integrity, foreign key constraints, and cascading deletions (`DelicatessenDB.sql`).

```text
Categories (1) ───< Products (N) ───< HighQuantity (1:1)
                       │
                       └───< OrderItems (N) >─── Orders (N) ───< Customers (1)

```

**Key Relational Entities:**

* **`Categories` & `Products`:** Manages product hierarchies, pricing, heating instructions, allergens, ingredients, and image URLs.
* **`HighQuantity`:** Tracks health flags for products (`HighSalt`, `HighSugar`, `HighFat`).
* **`Customers`:** Stores user profile info, shipping details (city, street, house number, floor, apartment), and `PasswordHash`.
* **`Orders` & `OrderItems`:** Transactional entities linking purchases to customer accounts with line-item pricing and status tracking.

---

## 🛠️ Tech Stack

| Layer | Technology / Tools | Highlights |
| --- | --- | --- |
| **Frontend** | Angular, TypeScript, HTML5, CSS3/SCSS | Component-driven architecture, Reactive Forms, Strongly-typed models (`Product`, `Order`, `User`) |
| **Backend** | C# .NET Core Web API | N-Tier Layered Design (`API`, `BLL`, `DAL`, `DTOs`) |
| **Database** | Microsoft SQL Server | Relational Schema, Foreign Keys, `ON DELETE CASCADE` rules |
| **Tools** | Visual Studio 2022, VS Code, Git/GitHub | Monorepo structure, Git version control |

---

## 🚀 Getting Started

### Prerequisites

* Node.js (v18+) & Angular CLI
* .NET SDK (v8.0+)
* Microsoft SQL Server & SSMS

### Local Setup & Installation

#### 1. Database Setup

1. Open SQL Server Management Studio (SSMS).
2. Open and execute the `database/DelicatessenDB.sql` script to create the `DelicatessenProject` database and all required tables.

#### 2. Backend Setup

```bash
cd backend
dotnet restore
dotnet run --project Delicatessen/Delicatessen.csproj

```

#### 3. Frontend Setup

```bash
cd frontend
npm install
ng serve

```

Open your browser and navigate to `http://localhost:4200/`.

---

## 👩‍💻 Author

> **Oshrit Djavsarov**
> *Software Engineering Student | Navat Israel Seminar, Jerusalem*
