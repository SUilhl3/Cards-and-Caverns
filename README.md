# Cards & Caverns

A small Slay-the-Spire-like card roguelike built in Unity.

Quick facts
- Unity Editor: 6000.0.33f1 (see `ProjectSettings/ProjectVersion.txt`).
- Packages: see `Packages/manifest.json` (Input System, URP, TextMeshPro, Test Framework).
- Primary code: `Assets/_Project/Scripts` (DeckManager, Combat, Relics, Map, UI, Systems).

Getting started
1. Install the Unity Editor version listed in `ProjectSettings/ProjectVersion.txt` (use Unity Hub).
2. Open the project in Unity: select the repository folder as the project path.
3. Open `Assets/Scenes/BattleScene.unity` (or `Assets/Scenes/levelSelect.unity`) and enter Play mode to test gameplay flows.

Where to look first (for new contributors)
- Cards & deck: `Assets/_Project/Scripts/DeckManager/` (`Deck.cs`, `Card.cs`, `ScriptableCard.cs`, `CardCollection.cs`).
- Combat & resolution: `Assets/_Project/Scripts/Combat/` and `Assets/_Project/Scripts/Systems/CardResolver.cs`.
- Relics: `Assets/_Project/Scripts/Relics/` and `ScriptableObjects/`.

Testing
- The project includes the Unity Test Framework (package). Most runtime checks should be performed in the Editor Play mode. Add small EditMode/PlayMode tests when practical.

Safe-edit rules
- Preserve serialized fields and inspector wiring. If you rename serialized fields, provide an [Obsolete] shim or instructions to rewire scenes.
- Make small, focused changes and include manual Play-mode test steps in PRs.

More
- See `.github/copilot-instructions.md` for agent-specific guidance and PR templates.
