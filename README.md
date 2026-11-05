# Legacy Web Forms Migration Assessment Sample

This repository provides a deliberately legacy but fully self-contained Visual Basic .NET ASP.NET Web Forms sample targeting .NET Framework 4.8. It intentionally includes representative legacy technologies such as ASP.NET Web Forms, `System.Web`, CSLA 2.0.3.0, and WCF while substituting the physical SQL Server dependency with checked-in JSON fixtures.

## Goals

- Build from a clean checkout using MSBuild.
- Compile against the supplied CSLA 2.0.3.0 binaries.
- Use a realistic CSLA `ReadOnlyBase(Of T)` and `DataPortal.Fetch` flow.
- Route retrieval through a WCF-style legacy service boundary.
- Keep the sample self-contained without Azure, Docker, SQL Server, or cloud services.
- Remain representative of a legacy project baseline without assuming automated test infrastructure.

## Runtime Path

The representative customer request follows the complete legacy path:

```text
DataAccess.aspx
  -> CustomerInfo
  -> CSLA DataPortal.Fetch
  -> LegacyCustomerService.svc
  -> LegacyCustomerService
  -> LegacyCustomerDal
  -> dbo.CustomerInfo_SelectById
  -> App_Data/SampleData/Customers.json
```

The WCF endpoint is hosted by a separate IIS Express Web Application on port `60172`. The Web Forms application runs on port `60171` and calls the service through the configured `LegacyCustomerServiceUrl`. The JSON file replaces only the physical database dependency; it is not accessed by the Web Forms pages or CSLA consumers directly.

Start both applications for the complete scenario:

```text
Web Forms: http://localhost:60171/Pages/Login.aspx
WCF:      http://localhost:60172/LegacyCustomerService.svc
```

## Manual Verification

With `LegacyWebFormsSample` selected as the startup project, browse to `/Pages/Login.aspx`, sign in as `admin`, and open `/Pages/DataAccess.aspx?customerId=1042`. The page should display Northwind Field Services through the CSLA, WCF, and fixture-backed DAL path. Automated test projects are intentionally out of scope for this legacy baseline.

## Solution Layout

- `src/LegacyWebFormsSample` - ASP.NET Web Forms sample app.
- `src/LegacyDataService` - WCF service contract and fixture-backed DAL.
- `csla` - supplied CSLA 2.0.3.0 assembly set.
- `docs` - migration notes and assessment context.
