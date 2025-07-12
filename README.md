# TradeWise
A modern stock portfolio tracker with social features

# TradeWise Setup Guide

## Database Setup
1. **Run migrations**:
   ```bash
   dotnet ef database update
   ```
2. **Seed test data** (optional):
   ```bash
   dotnet run SeedData
   ```

## Configuration
- Rename `appsettings.Template.json` to `appsettings.json`
- Update the connection string:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TradeWise;Trusted_Connection=True;TrustServerCertificate=True"
  }
  ```
