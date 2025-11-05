<!-- Short, practical instructions for AI coding agents working on this repository -->
# Repo quick-start for AI agents

This is a Unity game project (card roguelike). Aim: make small, local, compile-safe code changes to C# scripts under Assets and prefer edits that keep scene wiring intact.

Key facts (discoverable):
- Unity editor version: see `ProjectSettings/ProjectVersion.txt` (this project: 6000.0.33f1).
- Package list: `Packages/manifest.json` (Input System, TextMeshPro via package cache, URP, Test Framework, etc.).
- Code lives under `Assets/_Project/Scripts` organized by feature: DeckManager, Combat, Relics, Map, UI, Systems.

Project conventions & patterns to rely on
- MonoBehaviours are scene components and expect fields wired in the Inspector via [SerializeField]. Don’t remove serialized fields — instead preserve inspector-visible fields or update reference lookups carefully.
- Singletons: managers expose static Instance properties set in Awake (example: `Assets/_Project/Scripts/DeckManager/Deck.cs` uses `public static Deck Instance`). Use these when interacting with global systems.
- ScriptableObjects hold data (cards, collections, relic templates). Examples: `ScriptableCard.cs`, `CardCollection.cs`, `RelicTemplate.cs`. To change card data prefer creating/updating ScriptableObject assets in the Editor rather than hard-coding values.
- UI/prefab wiring: many systems instantiate prefabs and rely on serialized prefab references (e.g., `_cardPrefab` and `_cardCanvas` in `Deck.cs`). When changing instantiation, confirm prefab fields are still assigned in scenes.
- No namespaces are used widely — code edits should avoid introducing broad namespace changes without updating references.

Architecture notes (big-picture)
- Manager layer: `GameManager`, `Deck`, `CombatManager`, `RelicManager`, etc. They coordinate gameplay and are tightly coupled to scene objects.
- Data layer: ScriptableObjects (cards, card collections, relic templates) live as assets and are the authoritative source for card properties.
- Presentation layer: Card UI and movement components (e.g., `CardUI`, `CardMovement`) separate visual behaviour from card data objects.
- Scene-driven wiring: scenes connect managers, prefabs and UI. Many runtime invariants depend on correct objects being present in the active scene.

Developer workflows & how I (an agent) should behave
- Local compile & test: open the project in the Unity Editor that matches `ProjectVersion.txt` and use Play mode to exercise changes. You can open the editor via Unity Hub or by running the local Unity executable for the installed editor.
  - Example (Windows) — replace <projectPath> with repo path and ensure the editor version exists locally:
    "C:\\Program Files\\Unity\\Hub\\Editor\\6000.0.33f1\\Editor\\Unity.exe" -projectPath "C:\\path\\to\\Cards-and-Caverns"
- Debugging: attach an IDE debugger (Rider/Visual Studio/VS Code with the Unity extensions) to the running Unity Editor and use Play mode.
- Avoid making changes that require scene re-wiring. If a change requires reassigning serialized fields, update the scene or document which GameObjects need that assignment.

What to look at first for common tasks (examples)
- Find where card logic lives: `Assets/_Project/Scripts/DeckManager/Deck.cs`, `Card.cs`, `ScriptableCard.cs`, `CardCollection.cs`, `CardUI.cs`.
- Card resolution & combat effects: `Assets/_Project/Scripts/Systems/CardResolver.cs` and `Assets/_Project/Scripts/Combat/*`.
- Starting decks and card library: `StartingDecks.cs`, `CardLibrary.cs`.
- Relic system: `Assets/_Project/Scripts/Relics/` (uses Scriptable templates and a manager pattern).

Safe-edit rules for agents (concrete)
1. Preserve serialized private fields. If you rename a field, update scenes or keep the old field as an [Obsolete] shim to avoid breaking serialized data.
2. Prefer non-destructive changes: add new helper methods, small refactors, or unit-style logic in separate classes when possible.
3. When creating new ScriptableObject types or public serialized fields, list the affected scenes/prefabs in the PR description so the human maintainer can re-wire inspector references when they open Unity.
4. For behaviour changes that touch manager singletons (e.g., `Deck`, `GameManager`, `CombatManager`), include short manual test steps in your PR describing a Play-mode scenario to verify (which scene, what to click/trigger).

Files to reference when making edits
- `ProjectSettings/ProjectVersion.txt` — Editor version
- `Packages/manifest.json` — required packages
- `Assets/_Project/Scripts/DeckManager/Deck.cs` (singleton + deck lifecycle)
- `Assets/_Project/Scripts/DeckManager/ScriptableCard.cs` (card data shape)
- `Assets/_Project/Scripts/Systems/CardResolver.cs` (how effects are applied)

If you need more context
- Ask for the scene name(s) to run (Build Settings / Scenes in project) or a short recording of the failing behaviour in Play mode. Many behaviours only reproduce in the Editor with specific scenes loaded.

If an existing `.github/copilot-instructions.md` is present, preserve human-written sections about non-code processes before editing; this file replaces none (none found).

PR checklist & manual test template (add to PR body):

- Summary: short, 1-line description of the change and motivation.
- Files changed: list of edited files and any new assets/prefabs.
- Risks: list any scene or prefab wiring that must be re-assigned in the Editor.
- Manual Play-mode test steps (template):
  1. Open Unity Editor listed in `ProjectSettings/ProjectVersion.txt`.
  2. Open scene: `Assets/Scenes/BattleScene.unity` (or `Assets/_Project/Scenes/BattleScene.unity` if present).
  3. Enter Play mode.
  4. Reproduce the flow (example: start battle, draw hand, play card X) and verify expected behaviour.
  5. If the change touched serialized fields, confirm inspector references are still assigned.

Small PR guidance for agents
- Keep changes minimal and local: prefer adding helper methods or small test-only classes instead of refactoring large systems.
- When altering public behavior of managers (e.g., `Deck`, `GameManager`, `CombatManager`) include a short manual test scenario in the PR using the template above.
- When introducing new ScriptableObject types or serialized fields, include a note which scenes/prefabs must be updated and why.

— End of agent guidance —
