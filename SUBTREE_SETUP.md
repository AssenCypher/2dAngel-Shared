# Subtree setup

## Add Shared into a repo for the first time

```bash
cd <path-to-repo-root>
git remote add shared <YOUR_SHARED_REPO_URL>
git fetch shared
git subtree add --prefix=Assets/2dAngel/Shared shared main --squash
```

## Pull updates from Shared later

```bash
cd <path-to-repo-root>
git fetch shared
git subtree pull --prefix=Assets/2dAngel/Shared shared main --squash
```

## Where to run these commands

Run them in the root folder of each consumer repository:
- AngelPanel repo root
- AngelProbeTools repo root
- AngelLightTools repo root

Do not run them inside `Assets/2dAngel/Shared`.
