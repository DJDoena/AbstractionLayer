# DoenaSoft.AbstractionLayer.UI

Common UI abstractions used by the WinForms and WPF projects. This library provides small interface definitions that mirror important surface area of framework UI types so that application code can depend on interfaces instead of concrete types.

Package Id: `DoenaSoft.AbstractionLayer.UI`

Target: netstandard2.0

Interfaces and types in this project

The `AbstractionLayer.UI` project exposes the following interfaces and supporting types in the `Contracts` folder. These are the actual interfaces implemented and used by the platform-specific adapters.

- `IUIServices` (namespace `DoenaSoft.AbstractionLayer.UIServices`) — the primary UI service interface. Provides methods to show message boxes and common file/folder dialogs (`ShowMessageBox`, `ShowOpenFileDialog`, `ShowSaveFileDialog`, `ShowFolderBrowserDialog`).
- `IClipboardServices` — clipboard operations (`ContainsText`, `GetText`, `SetText`, `SetDataObject`).
- `ISynchronizer` (namespace `DoenaSoft.AbstractionLayer.UI.Contracts`) — invoke work on the UI thread (`Invoke`, `Invoke<T>`, `BeginInvoke`).
- `IDispatcherOperation` — represents a posted dispatcher operation and exposes its `Result`, `Status` and `Task`.

Supporting data types

- `FileDialogOptions`, `OpenFileDialogOptions`, `SaveFileDialogOptions`, `FolderBrowserDialogOptions` — configuration objects for dialogs.
- `Buttons`, `Icon`, `Result` — enums used by message box and dialog APIs.

Usage

Program against these interfaces (for example `IUIServices` and `ISynchronizer`) and use the platform-specific adapter implementations in the WinForms or WPF projects. In unit tests provide fakes or mocks for these interfaces to avoid showing UI and to verify interactions.

Usage

Reference this project from UI-specific packages (WinForms/WPF) or your application and program against the interfaces. Concrete adapter/extension types in the platform-specific projects map the real framework objects to these interfaces at runtime.

License: MIT
