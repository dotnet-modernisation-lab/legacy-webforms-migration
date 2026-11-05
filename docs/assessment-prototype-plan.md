# Legacy Web Forms Modernisation Assessment Prototype Plan

Status: Proposed assessment prototype (revised: no unit-test dependency)
Date: 2026-11-05
Mode: Assessment first

## Revision note

The legacy estate is assumed to have **no automated unit tests**, as is typical for legacy systems. The plan therefore
never requires NUnit or any test project. Behaviour is proven by a **behaviour contract recorded by a deterministic
probe** (see section 9), not by a test suite. "Build passes" is established by MSBuild; "behaviour preserved" is
established by the contract evaluator.

## 1. Objective

Prove the Spring prototype's architecture generalises to a different stack:

    VB.NET + .NET Framework 4.8 + ASP.NET Web Forms + System.Web + CSLA .NET 2.0.3
    + CSLA DataPortal + WCF + legacy DAL + stored-procedure contracts

The objective is not to migrate. It is to deterministically discover architecture, dependencies, coupling and behaviour,
then use a read-only AI Strategy Agent to explain an evidence-grounded modernisation strategy.

## 2. Architectural principles

    FACTS     Deterministic C# analysis
    POLICY    Deterministic migration classification
    STRATEGY  Read-only AI reasoning
    HUMAN REVIEW
    MIGRATION Later phase

Do not ask AI to modernise the repository before the system knows what exists and which behaviours must survive.

## 3. Where AI is used

**3.1 Strategy Agent (read-only).** C# supplies projects, language/framework, Web Forms/System.Web usage, Session,
genuine CSLA assembly identity and API, `ReadOnlyBase(Of T)`/`DataPortal` usage, WCF contracts and config, DAL and
stored-procedure contracts, the dependency graph, baseline build result, behaviour-contract evidence and migration
characteristics. The agent produces: summary, workstreams, rationale/ordering, coupling implications, risks,
human decisions required, recommendations.

**3.2 Later scoped port/remediation agent.** Only after assessment, behaviour baseline and architecture decisions.
Receives one bounded problem (kind, file, compiler evidence, actual CSLA signatures, behaviour obligations). No
unrestricted PowerShell, MSBuild, Git or filesystem. Deterministic code applies changes and decides success.

**3.3 AI is not used for:** parsing `.sln`/`.vbproj`/`packages.config`; deciding whether Csla.dll is 2.0.3.0;
detecting `ReadOnlyBase(Of T)` inheritance, `DataPortal.Fetch` calls or WCF attributes; deciding whether MSBuild
succeeded; deciding whether behaviour was preserved; overriding a deterministic classification or human gate.

## 4. Phase 1: Known-good legacy baseline

Acceptance criteria:

- [ ] Solution builds from a clean checkout (Visual Studio MSBuild, NuGet restore, .NET Framework 4.8 targeting pack).
- [ ] VB projects compile; target framework is .NET Framework 4.8.
- [ ] Genuine `Csla.dll` 2.0.3.0 is referenced.
- [ ] WCF service compiles.
- [ ] Customer `1042` resolves to `Northwind Field Services` (verified by the behaviour probe, section 9).
- [ ] `DataPortal.Fetch(Of CustomerInfo)` is exercised by that probe.
- [ ] Retrieval crosses the intended WCF boundary.
- [ ] DAL retains the `dbo.CustomerInfo_SelectById` contract.
- [ ] No SQL Server, no Azure/cloud infrastructure.

CI: Windows runner, NuGet restore, Visual Studio MSBuild, .NET Framework 4.8 targeting pack, then the behaviour
probe. No test framework. Do not convert to SDK-style projects.

## 5. Phase 2: Solution and project inventory

Deterministic `LegacyDotNetRepositoryAnalyzer` over `.sln`, `.vbproj`, `.csproj`, `packages.config`, `Web.config`,
`Global.asax`, `.aspx`, `.ascx`, `.svc`, assembly references and source files. Extracts projects, project types,
language, target frameworks, project and assembly references, NuGet packages, legacy binary dependencies,
configuration files. Model: `LegacyDotNetProjectAssessment(RepositoryName, CommitSha, Platform, Projects,
Technologies, Dependencies)`.

## 6. Phase 3: Web Forms coupling

Detect `.aspx`, `.ascx`, `System.Web`, `System.Web.UI.Page`, `Page_Load`, `IsPostBack`, `Session`, `GridView`,
`DetailsView`, Master Pages, `Global.asax`. Each finding carries technology, evidence and location.

## 7. Phase 4: CSLA analysis

`CslaAnalyzer` (source: `ReadOnlyBase(Of T)`, `BusinessBase(Of T)`, `DataPortal.Fetch/Create/Update`,
`DataPortal_Fetch`, CSLA config) and `LegacyAssemblyAnalyzer` (binary: assembly name, version, CLR metadata version,
public key token, referenced assemblies, relevant public types and signatures). For the sample: `Csla.dll`
2.0.3.0, CLR v2.0.50727, and dependencies such as System.Web, System.Runtime.Remoting, System.EnterpriseServices,
System.Transactions, all derived from the supplied binary, not assumed.

## 8. Phases 5-6: WCF and data-access analysis

`WcfAnalyzer`: ServiceContract, OperationContract, `.svc`, implementations, bindings, endpoints, clients,
configuration; chain `ILegacyCustomerService -> LegacyCustomerService -> LegacyCustomerDal`.

`LegacyDataAccessAnalyzer`: DAL classes, stored-procedure names/parameters, result models, call sites. Sample:
`dbo.CustomerInfo_SelectById`, `@CustomerId`. Record: stored-procedure contract, implemented by
`LegacyCustomerDal`, physically backed by a JSON fixture. The fixture is not a persistence architecture.

## 9. Phase 7-9: Graph, characteristics, behaviour baseline

**Graph.** Edges (`Uses, Inherits, Invokes, Hosts, References, DelegatesTo, ImplementsContract, ReadsConfiguration,
UsesSession`) for: `DataAccess.aspx -> CustomerInfo -> CSLA DataPortal -> WCF service -> LegacyCustomerDal ->
Stored Procedure Contract`. It answers: if X changes, what else and which behaviour contracts are affected?

**Migration characteristics.** `Portable, RequiresRecompile, RequiresLibraryUpgrade, RequiresFrameworkReplacement,
RequiresArchitecturalReplacement, RequiresBehaviourPreservation, RequiresHumanDecision`, assigned by deterministic
policy (Web Forms pages: framework replacement; Session: state-management redesign; GridView/DetailsView: UI
replacement; CSLA 2.0.3: library/architecture decision; DataPortal: architectural assessment; WCF boundary: service
migration decision; stored-procedure contract: preserve; JSON fixture: test/fixture infrastructure only).

**Behaviour baseline (no unit tests).** A deterministic **behaviour probe** exercises the built legacy application as
it is, either in-process against the built assemblies or over HTTP against the hosted Web Forms and WCF apps, and
records observed results as contracts in `Artifacts/behaviour-baseline.json`. The probe is a recording tool, not a
test suite, and adds nothing to the legacy solution.

- B-CUST-001 Customer 1042 returns "Northwind Field Services".
- B-CUST-002 Customer 1042 status is "Active".
- B-CUST-003 Unknown customer produces the defined not-found behaviour.
- B-DATA-001 CustomerId input semantics preserved.
- B-DATA-002 Stored-procedure contract remains `dbo.CustomerInfo_SelectById` unless redesigned.
- B-PAGE-001 Initial request differs correctly from postback.
- B-SESSION-001 Representative Session-backed behaviour preserved.
- B-ARCH-001 Retrieval crosses the business/service boundary; the UI never reads the fixture directly.

Static contracts (B-DATA-002, B-ARCH-001) are derived from analysis; dynamic ones are recorded by the probe on the
unmodified baseline. The contract evaluator, not AI, decides pass/fail later.

## 10. Phase 10: Migration problem classification

| Problem kind | Route |
|---|---|
| Mechanical syntax/project change | deterministic transformation |
| Known API migration | known mapping / scoped AI port |
| Web Forms replacement | architectural/human decision |
| CSLA DataPortal replacement | architectural/human decision |
| Known WCF API move | mapping / scoped port |
| Behaviour-contract question | human decision |
| Unknown compiler/runtime issue | AI diagnosis first |

AI does not decide whether a problem is safe to automate.

## 11. Phase 11: Read-only Strategy Agent

Input: `LegacyMigrationAnalysisInput` = assessment + graph + migration characteristics + behaviour baseline +
baseline build evidence. Explains what the estate contains, which concerns are independent, which decisions constrain
others, what to migrate first, what to carry forward, what needs replacement, where behavioural risk sits, and which
human architecture decisions are required. Returned facts and topology are validated against the deterministic input;
AI cannot alter authoritative facts.

## 12. Phase 12: Deliverables

    Artifacts/
      legacy-project-assessment.json
      legacy-dependency-graph.json
      behaviour-baseline.json
      migration-analysis.json
      legacy-modernisation-strategy.docx

Word strategy sections: executive summary, technology inventory, legacy assembly evidence, architectural graph,
Web Forms coupling, CSLA coupling, WCF/service topology, data-access contracts, portable components, migration
characteristics, behaviour baseline, workstreams, risks, human decisions, AI contribution/control boundaries, next steps.

## 13. Workstreams

1. Preserve behavioural baseline (contracts, customer scenarios, configuration behaviour).
2. Isolate portable logic from System.Web, DataPortal and WCF.
3. Define CSLA/DataPortal target architecture (human decision).
4. Define WCF replacement strategy (contract behaviour separate from technology).
5. Preserve the data contract (CustomerId, CustomerRecord, stored procedure).
6. Replatform Web Forms (System.Web, lifecycle, Session, server controls, rendering).
7. Remove residual .NET Framework dependencies (Remoting, EnterpriseServices, System.Web).

## 14. Phase 13: Stop and review

Assessment, behaviour model and strategy complete; **migration implementation not started.** Review and agree target
architecture, CSLA/DataPortal/WCF/UI targets, data-contract preservation, acceptable behaviour changes, workstream
ordering. Only then define port plans.

## 15. Later migration execution

    Approved task -> isolated workspace -> deterministic transformation where possible
    -> scoped AI where genuinely needed -> MSBuild -> behaviour probe vs contract -> deterministic verdict

Future port agent tools: `ReadProjectFile, ReadSourceFile, SearchSource, DescribeAssembly, DescribeType,
GetCompilerDiagnostics, GetBehaviourContract, Revalidate`. No arbitrary PowerShell, MSBuild args, file writes or Git.
`DescribeAssembly`/`DescribeType` keep CSLA reasoning tied to the real 2.0.3 binary.

## 16. Execution adapters

Legacy .NET adapter: SolutionProjectAnalyzer, AssemblyAnalyzer, WebFormsAnalyzer, CslaAnalyzer, WcfAnalyzer, MSBuild
execution, **behaviour probe**. Spring adapter unchanged; do not refactor the Spring prototype prematurely.

## 17. Implementation sequence

- **A Known-good sample:** [ ] actual CSLA binaries; [ ] Web Forms -> CSLA -> WCF -> DAL -> JSON proven; [ ] clean MSBuild; [ ] Windows CI that builds only.
- **B Inventory:** [ ] solution, old-style projects, `packages.config`, references, framework/language.
- **C Analysers:** [ ] Web Forms; [ ] CSLA source; [ ] assembly; [ ] WCF; [ ] DAL/stored procedure.
- **D Architecture:** [ ] graph; [ ] cross-framework boundaries; [ ] portable areas; [ ] characteristics.
- **E Behaviours:** [ ] contract model; [ ] probe; [ ] customer retrieval; [ ] not-found; [ ] Session/postback; [ ] service/data contracts; [ ] persist baseline.
- **F Strategy:** [ ] `LegacyMigrationAnalysisInput`; [ ] read-only agent; [ ] validate facts on return; [ ] JSON; [ ] Word.
- **G Architecture review:** [ ] Web Forms, CSLA, DataPortal, WCF targets; [ ] behaviour obligations; [ ] workstreams approved.
- **H Migration prototype:** [ ] first approved task; [ ] isolated Windows workspace; [ ] MSBuild; [ ] probe vs contract; [ ] scoped AI only for unresolved problems.

## 18. Definition of done

- [ ] Legacy repository builds with MSBuild.
- [ ] VB.NET and .NET Framework 4.8 identified.
- [ ] Web Forms/System.Web coupling identified.
- [ ] Genuine CSLA 2.0.3 binary and source usage identified, including `ReadOnlyBase(Of T)` and `DataPortal.Fetch`.
- [ ] WCF contracts/services, DAL and stored-procedure contracts identified.
- [ ] Customer retrieval represented as an architectural graph.
- [ ] Portable vs replacement-required components distinguished.
- [ ] Behaviour baseline recorded by the probe and persisted.
- [ ] Strategy Agent produces useful workstreams; AI cannot alter detected facts.
- [ ] Human-readable `.docx` generated.
- [ ] No production source migration required.

## 19. AI contribution and control boundaries

| Area | Deterministic code | AI |
|---|---|---|
| Discovery, framework, CSLA, WCF, graph | Authoritative | Explains implications |
| Behaviour baseline | Authoritative (probe + analysis) | Highlights risk |
| Migration classification | Deterministic policy | Cannot override route |
| Strategy/workstreams | Supplies hard constraints | Narrative, rationale, recommendations |
| Later scoped port | Controls scope and writes | Proposes targeted edit |
| Build | MSBuild decides | May explain failures |
| Behaviour verdict | Contract evaluator decides | Cannot override |

## 20. Guiding principle

Understand, record behaviour, classify, strategise, human architecture decisions, only then migrate. Never let
"it compiles" become the definition of preserved behaviour.
