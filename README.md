# Chess

![chess icon](src/resurse/chess_icon.png)

A polished, WinForms-based chess application written in C# (.NET Framework 4.7.2). This project includes a user/login UI, timed games, move history, piece promotion, sound effects and resource images for pieces and UI icons.

## Highlights

- Built with Windows Forms (WinForms) targeting .NET Framework 4.7.2.
- Login / signup screens (runs `UserForm` by default).
- Timed matches with visual low-time warning and sound effects.
- Move list/history panel, restart and play controls.
- Pawn promotion via `PromotingForm` and end-of-game dialogs.
- All piece images and UI assets stored in `src/resurse/`.

> The app starts in `UserForm` (so it presents a login/user screen before the game).

## Requirements

- Windows 7 / 8 / 10 / 11
- Visual Studio 2017 or later (or any IDE that supports .NET Framework WinForms projects)
- .NET Framework 4.7.2

## Open & Build

1. Open `src/Chess.csproj` with Visual Studio.
2. Build (Ctrl+Shift+B) and run (F5).

Alternatively, from PowerShell you can build with msbuild (Visual Studio Developer Command Prompt or any environment where `msbuild` is available):

```powershell
msbuild src\Chess.csproj /p:Configuration=Debug
```

Then run the produced executable in `bin\Debug\`.

## How to Play

1. Launch the app — you'll first see the user/login screen.
2. Log in or sign up using the builtin forms.
3. Choose a time control from the dropdown and click Play.
4. Use the GUI to move pieces. The move list panel shows history.
5. When a pawn reaches the final rank, the promotion dialog will appear.

## Project layout

- `src/` — main source files and forms
  - `Cell.cs`, `ChessBoard.cs`, `ChessForm.cs` — game UI & board logic
  - `Pieces/` — piece classes (King, Queen, Rook, Bishop, Knight, Pawn, Piece)
  - `UserForm.*`, `SignUpForm.*`, `ForgotPassForm.*` — user management forms
  - `PromotingForm.*`, `GameEndForm.*` — promotion and end-of-game dialogs
- `resurse/` — assets (piece images, icons, sound files referenced by the app)

