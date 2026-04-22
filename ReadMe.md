# InvestStartup — Investment & Startup Networking Platform

> **Note:** This repository contains the backend, domain, data access, shared infrastructure, and real-time hub layers of the platform. The WebUI layer is excluded as the application is currently in production preparation.

---

## Overview

InvestStartup is a multi-layered, enterprise-grade investment and startup networking platform built with **ASP.NET Core 9**. It connects investors and entrepreneurs through a secure, auditable, and scalable backend architecture.

The platform is designed with a strong emphasis on **application security**, **data privacy**, and **operational observability** — implementing industry-standard cryptographic practices, policy-based access control, and real-time session management throughout.

---

## Solution Structure

```
Investigation/
├── Investigation.Domain          # Core entities, enums, base classes
├── Investigation.DataAccess      # EF Core DbContext, repositories, migrations
├── Investigation.Business        # Services, security layer, background services, constants
├── Investigation.Shared          # DTOs, helpers, shared utilities
└── Investigation.ServerHub       # SignalR hub — real-time gateway for desktop client connections
```

> `Investigation.WebUI` is excluded from this repository (production deployment in progress).

---

## Technology Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 9 (MVC / Razor Pages) |
| ORM | Entity Framework Core (Code First) |
| Database | MS SQL Server |
| Identity | ASP.NET Core Identity |
| Real-Time | SignalR (Investigation.ServerHub) |
| Encryption | AES-GCM 256-bit |
| Hashing | HMAC-SHA256 |
| Authorization | Policy-Based + Role-Based (RBAC) |
| Background Services | .NET Hosted Services |
| Hosting Target | IIS / Plesk Panel |

---

## Security Architecture

Security is a first-class concern in this platform. The following mechanisms are implemented across the stack:

### Encryption (AES-GCM 256-bit)
All sensitive user data is encrypted at rest using AES-GCM via a custom `EncryptionService`:
- `NameSurname`, `PhoneNumber`, `Country` → encrypted in `AspNetUsers`
- `Email`, `UserName`, `Birthdate` → stored in dedicated encrypted columns (`EncryptedEmail`, `EncryptedUserName`, `EncryptedBirthdate`)
- Message fields (`MessageTitle`, `MessageSubject`, `MessageContent`) → encrypted before persistence; sanitized via HtmlSanitizer before encryption
- Encryption key managed via environment variables (never stored in source)

### Pseudonymization (HMAC-SHA256)
A dedicated `PseudonymizationService` provides deterministic pseudonymization:
- `Email` → `u_xxx@internal.local` (used as ASP.NET Core Identity login identifier)
- `UserName` → `u_xxx` (16-character hex prefix)
- Real values preserved only in encrypted columns, never exposed to Identity internals

### Authorization — Policy + Role Based (RBAC)
```csharp
options.AddPolicy("CompanyOwnerOnly", policy => {
    policy.RequireAuthenticatedUser();
    policy.RequireRole("CompanyUsers");
    policy.Requirements.Add(new ProfileOwnerRequirement());
});

options.AddPolicy("InvestorOwnerOnly", policy => {
    policy.RequireAuthenticatedUser();
    policy.RequireRole("InvestorUsers");
    policy.Requirements.Add(new ProfileOwnerRequirement());
});
```
`ProfileOwnerRequirement` validates resource ownership on every protected action — comparing session-bound identity against route parameters to prevent IDOR attacks.

### IDOR & URL Manipulation Protection
- All sensitive routes validated against session `userId`, `companyId`, `investorId`
- `[SkipOwnershipCheck]` attribute used explicitly and sparingly for public/admin endpoints
- Unauthorized access attempts return controlled error responses without data leakage

### OWASP Compliance

| OWASP Control | Implementation |
|---|---|
| Authentication | ASP.NET Core Identity + 2FA confirm-code flow |
| Access Control | Policy-based authorization + ProfileOwnerRequirement |
| Cryptographic Failures | AES-GCM at rest, HMAC-SHA256 pseudonymization, BCrypt passwords |
| Injection | EF Core parameterized queries, HtmlSanitizer on all user content |
| Security Misconfiguration | All secrets via environment variables, no sensitive data in source |
| Security Logging & Monitoring | AuditLog with encrypted username, background cleanup service |

### Google reCAPTCHA
Integrated on registration and public-facing forms to prevent automated abuse.

---

## Session & Real-Time Architecture

### UserSession Management
- Session stores: `userId`, `UserType`, `UserRole`, `Email` (real, decrypted), `OriginalIP`, `OriginalUA`
- `IsOnline` status tracked per session with `LastHeartbeat` timestamp

### Heartbeat System
- JavaScript client pings `/AuthUser/Heartbeat` every **30 seconds**
- `HeartbeatCleanupService` runs every **2 minutes** — marks sessions offline if `LastHeartbeat` has not been updated
- `BeaconLogout` endpoint handles browser close / tab kill via `navigator.sendBeacon`
- `OnlineDurationSeconds` calculated on logout

### SignalR — Investigation.ServerHub
A dedicated SignalR hub layer (`Investigation.ServerHub`) provides real-time connectivity for **desktop client applications**, enabling live notifications, messaging, and event broadcasting outside of the web interface.

---

## Background Services

| Service | Schedule | Action |
|---|---|---|
| `AuditLogCleanupService` | Daily 03:00 | Hard deletes audit records older than 45 days |
| `UserSessionCleanupService` | Daily 03:00 | Soft deletes sessions older than 90 days |
| `HeartbeatCleanupService` | Every 2 minutes | Marks stale online sessions as offline |

Retention periods are configurable via `appsettings.json`:
```json
"AuditLogSettings": { "RetentionDays": 45 },
"UserSessionSettings": { "RetentionDays": 90 }
```

---

## Audit Logging

- Every significant action is logged to the `Audits` table
- `UserName` stored encrypted in audit records
- View layer decrypts on render: `item.UserName == "Anonymous" ? "Anonymous" : Decrypt(item.UserName)`
- Automated cleanup prevents unbounded log growth

---

## Data Migration

A `DataMigrationService` handles encryption of pre-existing plaintext records:
- Idempotent — safe to run multiple times
- Skips admin accounts (by design)
- Returns `DataMigrationResultDto` with operation summary
- Triggered via admin panel: `HomeAdminController → MigrateUserData`

---

## Environment Variables (Production)

All sensitive configuration is externalized. No secrets exist in source code.

| Variable | Purpose |
|---|---|
| `Encryption__Key` | AES-GCM encryption key |
| `HashSettings__Salt` | HMAC-SHA256 salt |
| `ConnectionStrings__DefaultConnection` | Database connection string |
| `ReCaptcha__SecretKey` | Google reCAPTCHA secret |
| `EmailSettings__SenderPassword` | SMTP credentials |

---

## Author

**Kurtuluş Öcal** — Senior .NET Backend Developer  
[LinkedIn](https://linkedin.com/in/ocalkurtulus) · [GitHub](https://github.com/kurtulusocL)
