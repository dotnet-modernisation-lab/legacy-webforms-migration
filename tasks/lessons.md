# Lessons

- Visual Studio can keep stale project-type metadata for a legacy ASP.NET Web Forms project even after the source file is corrected.
- The project file must include the ASP.NET Web project type GUIDs and the WebApplication targets import to avoid the "application which this project type is based on was not found" error.
- When that error persists after the fix, close Visual Studio, remove the `.vs` cache folder and any generated `UpgradeLog*.htm` files, then reopen the solution.
- A clean build is not enough by itself; the IDE must be reopened from a clean project state before it will recognize old Web Forms projects reliably.
- A Web Forms parser error like `Could not load type 'LegacyWebFormsSample.LoginPage'` can be caused by stale IIS Express or Visual Studio debug hosts, not just a code mismatch. If the source and build are valid, stop old host processes, delete bin/obj output, and relaunch the application before chasing page wiring again.
- Legacy classic ASP.NET apps must be launched through the correct IIS Express / Visual Studio runtime host, not by directly loading the assembly or by reusing an earlier site instance.
