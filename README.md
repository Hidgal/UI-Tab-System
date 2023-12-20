<img src="Documentation/Images/tab_menu_gif.gif?raw=true" alt="Menu Example" width="280px" height="498px"/>

# UI-Tab-System
Simple animated tab menu system for Unity.

**!!! Requires DoTween and TextMeshPro !!!**

## Table Of Contents
<details>
<summary>Details</summary>
  
  - [Installation](#installation)
  - [Tab System Setup](#tab-system-setup)
  - [Tab Settings](#tab-settings)
  - [Available methods](#available-methods)
  - [Available Events](#available-events)

</details>

## Installation
You can download latest release [HERE](https://github.com/Hidgal/UI-Tab-System/releases).

Simply put this package in you project and import files.

Package contains Optional Extras folder with example of animated menu.

## Tab System Setup
1. Find **Tab** prefab in *Assets/Plugins/UI Tab System/Prefabs* and create your own prefab variant from it (or get example tab prefab variant from Assets/Plugins/UI Tab System/Optional Extras/Examples).
2. Add **Tab System** prefab (from *Assets/Plugins/UI Tab System/Prefabs*) to your Canvas.
3. Add your tab prefab variants to the child objects of **Tab System** prefab
4. *Optional: you can right click on the Tab System component and click on Get Tabs menu for preventing getting childs in Awake*

## Tab Settings
 - **Tab State** - current state of tab. You can switch it in edit mode to represent how it will look like.
    Available states:
      - **Active** - state of currently selected tab
      - **Inactive** - state of other non-selected tabs
      - **Locked** - state of locked tab. *In the **locked** state tab doesn`t respond to clicks*. You can mark tab as Locked via Inspector and it will be locked in playmode.
 - **State Settings** - settings for every available Tab State.
    Available settings:
      - **Animation Duration** - duration of tab animation in current state.        
      - **Is Button Active** - should button be active in current state.        
      - **Override Sorting** - should sorting of tab`s Canvas be overriden in current state.
      - **Additive Sorting** - if **Override Sorting** if enabled, adds this number to the sorting of root Canvas.
      - **...DurationCoefficient** - you can change duration of animation of each tab element by this coefficient.
      - **...Sprite** - change sprite of element in current state. If no sprite provided - don`t change sprite in current state.
      - **...Color** - overrides color of element in current state.
      - **...Ease** - easing of animation (provided by DoTween).
      - **Layout Min Size** - defines the min size of Layput Element on tab in current state. Animation of tab expansion is implemented through this parameter.
      - **ObjectToEnable/Disbale** - enables/disabled provided objects in current state.
 - **Target Object** - object which will be enabled when tab is active. Can be null.
 - **Update Layout On Enable** - force updates layout of target object on tab enable.

## Available Methods
- **Tab.SetState(State targetState, bool invokeEvents = true)** - you can manually change the Tab state to the **targetState** via script. If invokeEvents == false, makes it silently.

## Available Events
- **Tab.OnActivated(Tab tab)** - invokes on tab activation.
- **Tab.OnDeactivated(Tab tab)** - invokes on tab deactivation
- **Tab.OnStateChanged(State currentState)** - invokes on every state change.
      
