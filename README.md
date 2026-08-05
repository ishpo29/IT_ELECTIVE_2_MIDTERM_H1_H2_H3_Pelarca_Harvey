# it-elective-2-prelim-assignment-1


# ☕ Munchi Cafe POS

A web-based **Point of Sale (POS)** system developed using **ASP.NET Core MVC** for the IT Elective 2 Midterm Project.

**Theme:** Coffee Shop / Cafe

Munchi Cafe POS is a simple cashier system that allows staff to browse menu items, manage a shopping cart, process customer orders, and view completed transactions. The application follows the MVC architecture and uses static in-memory repositories instead of a database, as required by the project.

---

## Features

### Menu Browsing
- View all available food and beverage items
- Display item name, price, and available stock
- Out-of-stock items are automatically disabled

### Shopping Cart
- Add menu items to the cart
- Prevent adding quantities beyond available stock
- Automatically update quantities for existing cart items

### Cart Management
- Update item quantities
- Remove items from the cart
- Automatically calculate line totals and grand total

### Checkout
- Enter customer name before completing an order
- Optional customer email
- Server-side validation using DTOs
- Automatically deduct purchased stock
- Clear the shopping cart after a successful transaction

### Order History
- View completed customer orders
- Display transaction ID, customer name, purchase date, and total amount
- View all purchased items for each transaction

---

## Technologies Used

- ASP.NET Core MVC
- C#
- .NET
- Bootstrap 5
- Static In-Memory Repository
- Visual Studio 2022

---

## Project Structure

```
POS
│
├── Controllers
├── Models
├── DTOs
├── ViewModels
├── Views
├── Repositories
├── wwwroot
├── Program.cs
└── POS.sln
```

---

## Validation

The application includes server-side validation using Data Annotations.

- Customer name is required
- Quantity must be greater than zero
- Quantity cannot exceed available stock
- Checkout is not allowed when the cart is empty

---

## Business Process

1. Browse the cafe menu.
2. Add food and drinks to the shopping cart.
3. Update or remove cart items.
4. Proceed to checkout.
5. Enter customer information.
6. Confirm payment.
7. Record the completed order.
8. Update product inventory.
9. Clear the shopping cart for the next customer.

---

## Git Workflow

This project follows a feature branch workflow.

```
main
feature/shopping-cart
feature/checkout
```

Both feature branches were merged into the `main` branch after development.

---

## Getting Started

### Requirements

- Visual Studio 2022
- .NET SDK
- Git

### Installation

```bash
git clone https://github.com/ishpo29/IT_ELECTIVE_2_MIDTERM_H1_H2_H3_Pelarca_Harvey.git
```

1. Open **POS.sln** in Visual Studio.
2. Restore the NuGet packages.
3. Build the solution.
4. Run the application.

The application uses **static in-memory repositories**, so no SQL database or Entity Framework setup is required.

---

## Project Constraints

- ASP.NET Core MVC
- No SQL Database
- No Entity Framework
- Static in-memory repositories
- DTOs for all form submissions
- Server-side validation and business logic

---


### Menu
> Displays the available drinks and pastries, their prices, stock quantities, and allows the cashier to add items to the shopping cart.


---

## Author

**Harvey Pelarca**  
BSIT-31E3

