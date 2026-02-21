project-orbit (Unity)

A functional memory card-matching game prototype built from scratch in Unity.
The focus is on gameplay systems, scalability, clean architecture, and a team-style git workflow.

🎮 Features

Multiple grid layouts: 2×2, 2×3, 4×4, 5×6

Smooth card flip animations (Animator-based)

Non-blocking interaction: players can keep flipping while comparisons resolve

Dynamic UI scaling for desktop & mobile (GridLayoutGroup + Canvas Scaler)

Object pooling for cards (ID-based pool for performance)

Scoring system: turns + matches

Sound FX: flip, match, mismatch, game over (via centralized Audio Manager)

Restart flow with proper pool reset

Clean separation of concerns: UI, Grid, Game, Card, Audio managers

🛠 Tech Stack

Engine: Unity 2021 LTS

Language: C#

UI: UGUI (Canvas, GridLayoutGroup, Layout Groups)

Animation: Animator

Version Control: Git (frequent, meaningful commits)

🚀 How to Run (Desktop)

Clone the repo:

git clone <your-repo-url>

Open the project in Unity 2021 LTS.

Open the main scene (e.g., Main).

Press Play.

📱 Mobile Build

Orientation is locked as configured in Player Settings.

Build and run on Android or iOS from Unity’s Build Settings.

🧩 Controls

Mouse / Touch: Tap a card to flip

Home UI: Select grid size

Retry/Home: Restart or go back to grid selection

📐 Supported Grid Layouts

2×2

2×3

4×4

5×6

The grid automatically scales to fit the available screen/container area.

🔊 Audio

Basic SFX are integrated for:

Card flip

Match

Mismatch

Game over

Audio is managed via a centralized AudioManager to avoid cutoff during UI transitions.

🧪 QA Checklist

No console errors or warnings

Shuffle verified on restart

Pool resets correctly

Desktop window resizing supported

Mobile orientation locked as intended

📁 Project Structure (High Level)
/Managers
  - GameManager.cs
  - GridManager.cs
  - CardManager.cs
  - AudioManager.cs
/UI
  - UIManager.cs
/Cards
  - CardView.cs
🔁 Git Workflow

The repo was created from an empty Unity project.

Work progressed via frequent, meaningful commits to reflect team collaboration.

Final submission is merged into main.

⚠️ Known Limitations

This is a gameplay-focused prototype; UI polish and additional modes can be extended.

Art assets are placeholders and can be swapped easily.

📬 Notes

This prototype is built for evaluation and learning purposes.
Happy to explain design decisions or extend features if needed.