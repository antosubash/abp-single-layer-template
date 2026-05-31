# CLAUDE.md

Guidance for Claude Code in this repository. See `AGENTS.md` for full project
documentation; this file documents the shared local database setup.

## Local database (shared dev-services stack)

**Do NOT spin up a new Postgres container for local dev.** All repos in `~/Repos`
share one stack defined in `~/Repos/dev-services` (one PostGIS + Redis + MinIO +
Adminer on the `devnet` Docker network). Start it once with `make up` there.

- This repo's database is **`AbpTemplate`** on the shared server
  (`localhost:5432`, `postgres`/`postgres`) — already created by the shared
  stack's init SQL. The connection string in `abp/AbpTemplate/appsettings.json`
  (`Host=localhost;Port=5432;Database=AbpTemplate;User ID=postgres;Password=postgres`)
  already points at it.
- This repo's `abp/docker-compose.yml` starts its own `postgres:16-alpine`
  container (db `AbpTemplate`) — prefer the shared stack and skip it; running both
  conflicts on port 5432.

### Run locally

`dotnet run --migrate-database` from `abp/AbpTemplate/` (auto-migrates on startup,
serves on https://localhost:44351). Make sure the shared stack is up first.

If 5432 is taken by an old per-project container, stop that container rather than
remapping ports. To add a database, edit `dev-services/init/01-databases.sql`.
