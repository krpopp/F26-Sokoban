# AH / Three redesigned rooms

## Import into the class project

1. Stop Play Mode and save your work. Keep a backup of the current AH folder.
2. Merge this package's Assets/AH into the class project's Assets/AH. Allow replacement of the two existing scripts and AH_SHIFT_01 scene. Do not delete AH or create AH/AH.
3. Return to Unity, wait for compilation, then open Assets/AH/Scenes/AH_SHIFT_01.unity directly. No generator/menu command is needed.
4. Test movement, pushing, R restart, winning, and Enter to continue through all three rooms.
5. An Editor helper appends/enables these three scene paths in Build Settings and preserves all other entries. If loading reports a missing scene, manually add all three in Build Profiles > Scene List, including in any active profile with an overridden scene list.

The original uploaded prototype is retained as Scenes/AH_Prototype_Backup.unity with a different GUID. The three new rooms reuse your Kenney character model, its transform setup, materials, lighting and the unpacked teacher wall/player components. Teacher files are not included or edited. Every new/updated asset is within Assets/AH.

R restarts at any time. All goals must hold crates; the player does not count. After every actor stops moving, a completion cue and a small prompt appear; Enter loads the next scene. The third room shows a final completion message. Default progression waits for confirmation, matching the assignment. In the AHPuzzle Inspector, enable Auto Advance for delayed automatic progression if desired. No HUD during ordinary play.

Audio: two original short synthesized WAV cues for pushes and completion. Normal blue crates follow the teacher's Block rules, including chain pushing. Orange crate bars show the NEXT allowed direction.

## Puzzle intent

1. THE DETOUR: one horizontal-start axis crate. The wall above the direct route forces a side route and deliberate alternation; moving toward the goal every turn does not work.
2. SHARED WORKSPACE: one vertical-start axis crate and one ordinary crate. The upper goal is tempting to occupy early, but space around it is also useful for repositioning. Decide how the ordinary crate participates in the route; chain pushes are legal.
3. THE FALSE FINISH: two axis crates (one vertical, one horizontal) and one ordinary crate. Nearby goals invite premature parking, but the axis rule makes them costly. One optimal route occupies a goal temporarily and then moves that crate away. Keeping every occupied goal filled is still solvable, but takes at least 22 push actions instead of 18.

## Verification and limits

A breadth-first search over push states used the supplied alternating-axis and recursive chain-push rules. Walking regions were collapsed, preserving reachability. Goal assignment is unrestricted, as in the runtime. Minimum successful push actions are 6, 12 and 18; a chain push counts as one input action even when several crates move. The provided routes are push-optimal, not necessarily walk-optimal. Relaxing the axis constraint reduces the minima to 4, 7 and 4, so the axis mechanic materially changes every puzzle.

All three routes were replayed independently step by step. Scene YAML IDs, local references, hierarchy links, crate/goal bindings and asset GUIDs were checked. The C# files passed a syntax parser. The model, materials and existing meta GUIDs are preserved. No Unity executable is available here: Unity compilation, actual rendering, audio and input timing must still be checked in your editor before submission.

Solvability is proven in a sequential grid model; subjective difficulty remains a playtest question. Win detection waits for movement completion. The runtime adds no physics queries or physics events. Only the supplied GridManager retains its original initialization SphereCast.

## Spoilers / test routes

U/D/L/R refer to screen up/down/left/right with the supplied camera. One letter is one grid step. Use individual key taps; do not hold keys while replaying.

### Room 1

Minimum 6 push actions. This route walks 24 steps.

`DDDRRDRURULDLUDLLUURRDRU`

```text
########
#      #
# @##. #
#      #
#  #   #
#   H  #
#      #
########
```

### Room 2

Minimum 12 push actions. This route walks 59 steps.

`LDDRDDRURULRUUULLDDLDDDRRULDLURRULDLLULURDDRULUURLDDRRULDLU`

```text
########
#      #
# . @# #
#   B# #
#      #
#    V #
#      #
########
```

### Room 3

Minimum 18 push actions. This route walks 61 steps.

`LLUURRDLULDLDRURDLDDRRULULDLDRURDLDRDRULURDRUDRUUDLLURDRURUUL`

```text
#########
#       #
#  HV.  #
#   @#. #
#       #
#     B #
#       #
#       #
#########
```

Map legend: # wall, . goal, H horizontal-start axis crate, V vertical-start axis crate, B ordinary crate, @ player. The player in room 3 starts on a goal.
