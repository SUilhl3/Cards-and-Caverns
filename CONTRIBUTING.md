# Contributing

Thank you for contributing! This file contains lightweight, project-specific rules to keep the Unity project stable and easy to run.

Before you start
- Use the Unity Editor version in `ProjectSettings/ProjectVersion.txt`.
- Run the project locally in the Unity Editor and exercise your change in Play mode.

Coding & PR guidance
- Keep changes small and local. Avoid large refactors that require scene re-wiring.
- Preserve `[SerializeField]` fields and their names. If you must rename a serialized field, keep the old field as an [Obsolete] shim and document required scene updates.
- When changing singletons (e.g., `Deck.Instance`, `GameManager.Instance`), include Play-mode test steps in the PR body.
- ScriptableObjects are the canonical data source for cards, relics, and collections. Prefer editing/creating ScriptableObject assets instead of hard-coded data.

PR checklist (use in PR description)
- Summary: one-line description of the change.
- Files changed: list changed files and any scene/prefab assets touched.
- Manual Play-mode test steps (explicit): which scene, which buttons/actions to click.
- Risk: note whether scenes need inspector re-wiring after the change.

Manual Play-mode test template (paste into PR body)
1. Open Unity Editor version: `ProjectSettings/ProjectVersion.txt`.
2. Open scene: `Assets/Scenes/BattleScene.unity`.
3. Enter Play mode.
4. Steps to reproduce and verify: (e.g., start battle -> draw -> play card X -> expected result).
5. Confirm no missing inspector references.

If you're unsure about a change that touches scenes or serialized fields, open an issue first and describe the intended change.
