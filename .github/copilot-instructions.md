# Copilot Instructions

## Project Guidelines
- For this legacy Web Forms migration sample, remove testing requirements and do not add test projects or automated tests unless explicitly requested later.
- For this legacy migration sample, preserve CSLA on both sides of the WCF boundary: the Web Forms application should call a client-side CSLA DataPortal fetch, which invokes WCF; the WCF service should then invoke its own CSLA DataPortal fetch, which uses the DAL and JSON fixture. Do not replace the Web Forms CSLA object with a plain DTO-backed model.