# ProductApp

## Содержание

- [Описание](#описание)
- [Стек](#стек)
- [Установка](#установка)

## Описание

Веб-сервис, который предоставляет RESTful API для управления
товарами и категориями товаров

## Стек:

•	C# (.NET SDK 8.0)

•	ASP.NET Core (6.6.2)

•	SQLite

•	Entity Framework Core (8.0.10)

## Установка

1. Клонируйте репозиторий:
   
   ```bash
   git clone https://github.com/cyber1311/ProductApp.git

2. Перейдите в директорию проекта:

   ```bash
   cd ProductApp/ProductApp

3. Необходимо установить инструменты для работы с EF Core:

   ```bash
   dotnet tool install --global dotnet-ef

4. Необходимо установить инструменты для работы с EF Core:

   ```bash
   dotnet ef dbcontext scaffold "Data Source=products.db" Microsoft.EntityFrameworkCore.Sqlite

5. Запустите приложение:

   ```bash
   dotnet run

