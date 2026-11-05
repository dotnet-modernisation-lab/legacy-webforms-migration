# Migration Notes

This project intentionally preserves legacy architecture and technical debt boundaries for assessment tooling.

- ASP.NET Web Forms pages use the `System.Web` lifecycle.
- Web Forms server controls are included to demonstrate classic UI patterns.
- CSLA `ReadOnlyBase(Of T)` is used to represent a business object.
- The Web Forms client and WCF service each use a CSLA `DataPortal.Fetch`; the service-side fetch reaches the fixture-backed DAL.
- The DAL preserves a legacy stored-procedure contract name: `dbo.CustomerInfo_SelectById`.
- JSON fixtures stand in for the historical database result set.
- `LegacyCustomerService.svc` provides a separate local WCF `basicHttpBinding` endpoint hosted by its own IIS Express Web Application.
- `CustomerInfo.DataPortal_Fetch` crosses the remote WCF endpoint through a `ChannelFactory` client facade.
- The customer fixture path is controlled by the `LegacyCustomerFixturePath` application setting.
- The Web Forms client endpoint is controlled by the `LegacyCustomerServiceUrl` application setting.
- CSLA `CustomerInfo` declares `AuthorizationRules` for its readable properties and performs an explicit Admin authorization check during `DataPortal_Fetch` because this CSLA version has no modern `CanGetObject` API.
- The Web Forms/CSLA layer requires an authenticated Admin user before starting the remote fetch; the WCF service validates a basic trusted-application request and the service-side CSLA object enforces its own property authorization rules.
- The trusted-subsystem request uses a configured application name/secret plus the forwarded website username/role for this deliberately basic sample; production code should use HTTPS and signed or framework-managed service credentials rather than a plain shared secret.
- The Web Forms pages use `Site.Master`, preserving the classic master-page/content-page pattern.
- The sample is not intended to be a modern architecture; it is intentionally a migration baseline.
- This repository is intentionally build-focused rather than test-driven because legacy project baselines often do not include automated test suites.
