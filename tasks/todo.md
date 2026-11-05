# Revised plan for the legacy Web Forms sample

## Current verified status
- [x] The project builds cleanly from a clean checkout.
- [x] The ASPX Inherits declarations match their code-behind classes.
- [x] The app is configured for .NET Framework 4.8 and classic ASP.NET Web Forms hosting.
- [ ] The remaining issue is a stale ASP.NET runtime host (IIS Express / Visual Studio / MSBuild / PowerShell process state), not a source-code mismatch.

## Immediate execution plan
- [ ] Stop all stale Visual Studio, IIS Express, MSBuild, and PowerShell processes that may still be holding the app assembly.
- [ ] Remove the stale generated artifacts from the Web Forms project bin/obj folders.
- [ ] Reopen the solution in Visual Studio from the repo root and confirm the startup project is LegacyWebFormsSample.
- [ ] Launch the app with IIS Express using F5 and confirm the login page loads without the parser error.
- [ ] Validate that each ASPX page resolves its code-behind class under the ASP.NET runtime.
- [ ] Capture final evidence and document the exact run steps for future migration checks.

## Exit criteria
- [ ] No `Could not load type` errors for page classes.
- [ ] No stale DLL lock during rebuild.
- [ ] The legacy Web Forms sample runs successfully in the browser under IIS Express.
- [ ] The sample can be used as a migration-assessment baseline for legacy ASP.NET Web Forms work.
