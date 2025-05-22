
# 🌍 Travel Package API

This is a simple ASP.NET Core Web API that provides full CRUD operations and a search endpoint to manage travel packages.

---

## ✅ Features

- Add a new travel package
- Get all packages or a specific one by ID
- Update an existing package
- Delete a package
- Search packages by destination and price range
- Swagger UI enabled for easy testing

---

## 📂 API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET    | `/api/packages` | Get all travel packages |
| GET    | `/api/packages/{id}` | Get a package by ID |
| POST   | `/api/packages` | Create a new package |
| PUT    | `/api/packages/{id}` | Update an existing package |
| DELETE | `/api/packages/{id}` | Delete a package |
| GET    | `/api/packages/search?destination=paris&minPrice=100&maxPrice=500` | Search packages |

---

## 🚀 How to Run

1. Open the project in Visual Studio.
2. Build and run the solution (`F5` or "Run").
3. Navigate to: `https://localhost:{port}/swagger` to test the API.

---

## 🛠 Tech Stack

- ASP.NET Core 6+
- Swagger (Swashbuckle)

---

## 📌 Notes
✅ It uses in-memory storage for data, meaning all data is temporarily stored during runtime.

✅ The codebase is clean and modular, making it easy to extend or connect to a real customized database in the future.
