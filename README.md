# Developer WorkFlow

A desktop application for managing projects, sprints, work items, developers, and sprint progress using WPF and .NET.

## Overview

Developer WorkFlow provides a centralized workspace for managing software development activities across projects and sprints.

The application supports:

* Project management
* Sprint planning
* Backlog management
* Work item tracking
* Developer assignments
* Work item comments
* Sprint progress tracking
* Dashboard statistics
* Light and dark themes

The application follows the MVVM pattern and uses Entity Framework Core with SQLite for local persistence.

---

## Features

### Dashboard

The dashboard provides an overview of the current development workload, including:

* Total work items
* Work items currently in progress or review
* Completed work items
* Active sprints
* Recently created work items
* Projects

---

### Backlog

The backlog provides a centralized view of work items that can be planned into sprints.

Work items can contain:

* Title
* Description
* Type
* Priority
* Story points
* Project
* Sprint
* Assigned developer
* Comments

---

### Sprint Planning

Sprints can be created and managed with:

* Sprint name
* Start date
* End date
* Goal points
* Associated project
* Assigned work items

Work items can be associated with sprints as part of sprint planning.

---

### Work Items

The application supports three work item types:

* **User Story**
* **Defect**
* **Tech Task**

Each work item supports a workflow status:

```text
Backlog
   ↓
Ready
   ↓
In Progress
   ↓
In Review
   ↓
Done
```

A work item can also be marked as:

```text
Blocked
```

Supported priorities:

```text
Low
Medium
High
Critical
```

---

### Developer Management

Developers contain:

* Name
* Email
* Role

Developers can be associated with projects and assigned to work items.

---

### Comments

Work items support developer comments.

Each comment contains:

* Developer
* Message
* Creation timestamp

Comments are persisted with their associated work item.

---

### Projects

Projects contain:

* Project name
* Description
* Creation date
* Developers
* Sprints

Projects can have multiple developers and sprints associated with them.

---

### Theme Support

The application supports light and dark themes.

Theme resources are maintained using WPF `ResourceDictionary` files and `DynamicResource` bindings.

The theme can be changed from the Settings section.

---

## Technology Stack

| Technology            | Usage                         |
| --------------------- | ----------------------------- |
| C#                    | Application development       |
| .NET 10               | Application runtime           |
| WPF                   | Desktop UI                    |
| XAML                  | UI and styling                |
| MVVM                  | Application architecture      |
| Entity Framework Core | Data access                   |
| SQLite                | Local database                |
| LINQ                  | Data querying                 |
| `ICommand`            | UI command handling           |
| ResourceDictionary    | Themes and reusable resources |

---

## Architecture

The application is structured around the MVVM pattern with separate model, view, view-model, data, service, and command responsibilities.

```text
┌───────────────────────────────┐
│             WPF UI            │
│             XAML              │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│         ViewModels            │
│                               │
│ DashboardViewModel            │
│ BacklogViewModel              │
│ SprintPlanningViewModel       │
│ WorkItemsViewModel            │
│ ProjectsViewModel             │
│ SettingsViewModel             │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│           Services            │
│                               │
│ ProjectService                │
│ SprintService                 │
│ WorkItemService               │
│ ThemeService                  │
│ NavigationService             │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│        Data Access            │
│                               │
│ Repositories                  │
│ WorkFlowDbContext             │
│ Entity Configurations         │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│            SQLite             │
└───────────────────────────────┘
```

---

## MVVM

The application separates UI presentation from application logic using MVVM.

### Views

XAML views are responsible for the user interface and data binding.

Examples include:

```text
DashboardView
BacklogView
SprintPlanningView
WorkItemsView
ProjectsView
SettingsView
```

### ViewModels

ViewModels expose data and commands required by the views.

The main application ViewModel manages navigation between the different application sections.

### Commands

The project contains reusable command implementations:

* `RelayCommand`
* `AsyncRelayCommand`

These provide command binding between XAML controls and ViewModel operations.

---

## Data Access

Entity Framework Core is used for persistence.

The main database context is:

```text
WorkFlowDbContext
```

The context manages the application's entities and their relationships.

### Entities

The application contains entities for:

```text
Project
    │
    ├── Developers
    │
    └── Sprints
            │
            └── WorkItems
                    │
                    ├── Developer
                    └── Comments
```

### Database

SQLite is used as the local database.

Entity Framework Core handles:

* Database access
* Entity mapping
* Relationships
* Migrations
* Persistence

EF Core migrations are included in the project.

---

## Repository Layer

The application contains a repository abstraction for data access.

```text
IRepository<T>
      │
      ▼
Repository<T>
      │
      ├── DeveloperRepository
      ├── ProjectRepository
      ├── SprintRepository
      └── WorkItemRepository
```

The repository layer keeps data-access operations separate from the ViewModels and application services.

---

## Service Layer

Business operations are organized into service interfaces and implementations.

```text
IProjectService
      └── ProjectService

ISprintService
      └── SprintService

IWorkItemService
      └── WorkItemService

IThemeService
      └── ThemeService

INavigationService
      └── NavigationService
```

Services coordinate application operations and interact with the data-access layer.

---

## Work Item Model

`WorkItem` is the base model for the application's development tasks.

```text
                WorkItem
                   │
        ┌──────────┼──────────┐
        │          │          │
        ▼          ▼          ▼
   UserStory    Defect     TechTask
```

### User Story

Contains acceptance criteria in addition to the common work item properties.

### Defect

Represents a software defect within the workflow.

### Tech Task

Represents a technical task and includes estimated hours.

---

## WPF Components

The project also contains reusable WPF-specific components.

### WatermarkTextBox

A custom `TextBox` control provides watermark support through dependency properties.

Available properties include:

```text
Watermark
WatermarkForeground
```

### AutoScrollBehavior

An attached behavior provides automatic scrolling functionality for supported controls.

### Converters

The application uses value converters where required for UI presentation and binding.

### Resource Dictionaries

Reusable styles, control resources, and theme resources are maintained through XAML resource dictionaries.

---

## Project Structure

```text
WorkflowProjectManager/
│
├── WorkFlow/
│   │
│   ├── Commands/
│   │   ├── RelayCommand.cs
│   │   └── AsyncRelayCommand.cs
│   │
│   ├── Controls/
│   │   └── WatermarkTextBox.cs
│   │
│   ├── Data/
│   │   ├── Configurations/
│   │   ├── Repositories/
│   │   └── WorkFlowDbContext.cs
│   │
│   ├── Models/
│   │   ├── Developer.cs
│   │   ├── Project.cs
│   │   ├── Sprint.cs
│   │   ├── WorkItem.cs
│   │   ├── UserStory.cs
│   │   ├── Defect.cs
│   │   ├── TechTask.cs
│   │   ├── WorkItemComment.cs
│   │   └── Enums/
│   │
│   ├── Services/
│   │   ├── ProjectService.cs
│   │   ├── SprintService.cs
│   │   ├── WorkItemService.cs
│   │   ├── ThemeService.cs
│   │   └── NavigationService.cs
│   │
│   ├── ViewModels/
│   │   ├── MainViewModel.cs
│   │   ├── DashboardViewModel.cs
│   │   ├── BacklogViewModel.cs
│   │   ├── SprintPlanningViewModel.cs
│   │   ├── WorkItemsViewModel.cs
│   │   ├── ProjectsViewModel.cs
│   │   └── SettingsViewModel.cs
│   │
│   ├── Views/
│   │   ├── DashboardView.xaml
│   │   ├── BacklogView.xaml
│   │   ├── SprintPlanningView.xaml
│   │   ├── WorkItemsView.xaml
│   │   ├── ProjectsView.xaml
│   │   └── SettingsView.xaml
│   │
│   └── Resources/
│       └── XAML resource dictionaries
│
├── Migrations/
│
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
└── WorkflowProjectManager.csproj
```

---

## Database Model

The main relationships are structured as follows:

```text
Project
   │
   ├───────────────┐
   │               │
   ▼               ▼
Developers       Sprints
                   │
                   ▼
                WorkItems
                   │
                   ▼
                Comments
```

A project can contain multiple developers and sprints.

A sprint contains work items.

A work item can have an assigned developer and multiple comments.

---

## Seed Data

The database contains initial seed data for development purposes, including:

* Developers
* Project
* Sprint
* Work items

This allows the application to display meaningful data when first started.

---

## Getting Started

### Prerequisites

* Visual Studio 2026 or compatible .NET IDE
* .NET 10 SDK
* Windows
* Git

### Clone the Repository

```bash
git clone <repository-url>
cd WorkflowProjectManager
```

### Open the Solution

Open the project in Visual Studio.

Restore NuGet packages and build the solution.

### Run

Start the `WorkflowProjectManager` project from Visual Studio.

The application will initialize the SQLite database through Entity Framework Core and load the configured seed data.

---

## Database

The application uses a local SQLite database.

```text
SQLite
   │
   ▼
WorkFlowDbContext
   │
   ├── Projects
   ├── Sprints
   ├── WorkItems
   ├── Developers
   └── WorkItemComments
```

Database schema changes are managed through EF Core migrations.

---

## Application Navigation

The main navigation contains:

```text
Dashboard
Backlog
Sprint Planning
Work Items
Projects
Settings
```

Navigation is handled through the application's ViewModel and view composition rather than directly coupling navigation logic to individual controls.

---

## License

This project is available under the license included in this repository.
