# Job Application Tracker — Backend API

A RESTful API built with ASP.NET Core Web API (.NET 8) following 
Clean Architecture principles to help developers track their 
job applications, interview rounds, and job search progress.

## Tech Stack

- **Framework** — ASP.NET Core Web API (.NET 8)
- **Database** — SQL Server with Entity Framework Core
- **Authentication** — JWT Tokens + ASP.NET Identity
- **Architecture** — Clean Architecture (4 layers)
- **Documentation** — Swagger / OpenAPI
- **Frontend** — Angular 17 (separate repository)

## Features

- JWT Authentication (Register, Login)
- Job Application CRUD with pagination and filtering
- Interview Rounds management per job
- Dashboard statistics and weekly chart data
- Status tracking (Applied, Interview, Offer, Rejected)

## API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register new user |
| POST | /api/auth/login | Login and get JWT token |
| GET | /api/auth/me | Get current user info |

### Jobs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/jobs | Get all jobs (paginated) |
| GET | /api/jobs/{id} | Get job by ID |
| POST | /api/jobs | Create new job |
| PUT | /api/jobs/{id} | Update job |
| DELETE | /api/jobs/{id} | Delete job |

### Interviews
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/jobs/{jobId}/interviews | Get all rounds |
| POST | /api/jobs/{jobId}/interviews | Add interview round |
| PUT | /api/jobs/{jobId}/interviews/{id} | Update round |
| DELETE | /api/jobs/{jobId}/interviews/{id} | Delete round |

### Dashboard
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/dashboard | Get dashboard stats |

## Project Structure# JobTracker
