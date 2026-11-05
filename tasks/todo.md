# Revised plan for the legacy Web Forms sample

## Verified status (2026-11-05)
- [x] The solution builds cleanly from a clean checkout (`msbuild LegacyWebFormsMigration.sln -restore -p:RestorePackagesConfig=true`).
- [x] The ASPX Inherits declarations match their code-behind classes.
- [x] The app is configured for .NET Framework 4.8 and classic ASP.NET Web Forms hosting.
- [x] The earlier stale-host problem is gone: no stale Visual Studio / IIS Express / MSBuild processes were running, and an in-place rebuild succeeded with no DLL lock.

## Execution plan
- [x] Stop stale Visual Studio, IIS Express, MSBuild and PowerShell processes (none were running).
- [x] Remove stale generated artifacts: not needed; an in-place rebuild and run worked.
- [x] Startup configuration confirmed: the local launch profile (`LegacyWebFormsMigration.slnLaunch.user`) starts both `LegacyWebFormsSample` and `LegacyDataServiceHost`. That file is gitignored (`*.user`), so a fresh checkout must start both projects itself (see Run steps). Pressing F5 in Visual Studio was not exercised; the same IIS Express hosting was exercised directly.
- [x] Launch both apps under IIS Express (Web Forms on 60171, WCF host on 60172); `Login.aspx` loads and the `.svc?wsdl` is served, both HTTP 200.
- [x] Each ASPX page resolves its code-behind class: Default, Login, Compatibility and AjaxToolkit return 200; Profile and DataAccess return 302 to login when unauthenticated; none show `Could not load type`, a parser error or a server error.
- [x] Capture final evidence and document the run steps (below).

## Exit criteria
- [x] No `Could not load type` errors for page classes.
- [x] No stale DLL lock during rebuild.
- [x] The sample runs successfully in the browser stack under IIS Express (checked over HTTP with curl).
- [x] The sample can be used as a migration-assessment baseline for legacy ASP.NET Web Forms work.

## Observed behaviour (recorded for the behaviour baseline)
Signed in as `admin` (POST to `/Pages/Login.aspx`, role `Admin`):

| Request | Observed |
|---|---|
| `DataAccess.aspx?customerId=1042` | `Northwind Field Services`, status `Active`, "Loaded customer 1042 from the legacy CSLA/WCF/DAL path." |
| `DataAccess.aspx?customerId=9999` (unknown) | "Customer retrieval failed: DataPortal.Fetch failed" (the service's not-found fault is wrapped by CSLA, so the specific message is lost) |
| `DataAccess.aspx?customerId=-5` | "Customer ID must be a positive number." |
| `DataAccess.aspx?customerId=1042`, not signed in | 302 to `/Pages/Login.aspx?ReturnUrl=...` |
| `Profile.aspx` after sign-in | "Current session user: admin. Role: Admin. Last page: /Pages/Login.aspx" (Session-backed) |

## Run steps
1. `msbuild LegacyWebFormsMigration.sln -restore -p:RestorePackagesConfig=true`
2. Start the WCF host: `iisexpress.exe /path:<repo>\src\LegacyDataServiceHost /port:60172`
3. Start the Web Forms app: `iisexpress.exe /path:<repo>\src\LegacyWebFormsSample /port:60171`
4. Browse to `http://localhost:60171/Pages/Login.aspx`, sign in as `admin`, then open `/Pages/DataAccess.aspx?customerId=1042`.
