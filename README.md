# VR Vocabulary Trainer — Master Thesis Repository
## 1) Project Overview

- **Goal:** Demonstrate how VR supports learning models via an interactive **Vocabulary Trainer** that lets learners **see**, **hear**, and **manipulate** 3D word objects.
- **Hardware:** Meta Quest 2 (PC-VR via Link/Air Link).
- **Core idea:** Compare learning modes with increasing interactivity:
  - **M1** — Seeing & Hearing (baseline; no grabbing)
  - **M2** — Seeing, Hearing & **Controller** interaction
  - **M3** — Seeing, Hearing & **Hand-tracking** interaction

---

## 2) Key Features

- **Three Modes (M1–M3):** Toggle interaction depth from passive viewing to full hand-tracking.
- **Teleport Locomotion:** Predefined hotspots keep users oriented at each learning table (reduces cybersickness).
- **Audio Pronunciation:** Point to a speech-bubble on each object’s canvas to hear the word.
- **Languages:** English, German, Vimmi (fictional; ensures zero prior knowledge).
- **Basket Mechanic:** Drop an object into the basket to “mark as learned” — it despawns after a short delay.
- **Tutorial Scene:** Teaches locomotion and grabbing with controllers and hand-tracking.

---

## 3) General Architecture 

### 3.1 Scenes
- **StartMenu** → pick **Mode (M1/M2/M3)**, **Order (fixed/random)**, **Source→Target language** (default: EN→VIM).
- **Scene1 / Scene2 / Scene3** → neutral rooms with wall tables, teleport points, and object stations.
- **Tutorial** → basic movement + interaction walkthrough (controllers & hand-tracking).

### 3.2 Interaction & Locomotion
- **Locomotion:** Teleport hotspots + viewpoints (keeps the user facing each object).
- **Grabbing:** 
  - M2: Controller grab (Meta XR `Grab Interactable`)
  - M3: Hand-tracking grab (Meta XR `Hand Grab Interactable`)
- **Object UI:** Per-object canvas (title labels; speech-bubble triggers audio). Ray/pointer for controller; pinch/point for hands.

### 3.3 Data / Assets Layout

Each **learnable object** lives under `Resources/LearnObjects/<objectName>/`:

```
<objectName>.prefab      # 3D model prefab
eng.mp3                  # English audio
ger.mp3                  # German audio
vim.mp3                  # Vimmi audio
loc.txt                  # "German,English,Vimmi" (CSV-style single line)
```

At runtime, the backend loads these assets, creates `LearnObject` instances, and spawns canvases and prefabs (random or fixed) at predefined positions.

---

## 4) Getting Started

1. **Install**
   - Unity (version compatible with Meta XR Interaction SDK)
   - **Meta Quest Link** (for PC-VR) and **Meta XR All-in-One SDK** from the Asset Store
2. **Clone & Open**
   - `git clone https://github.com/CyberNord/MS_VR_SS2024`
   - Open in Unity; import Meta XR packages if prompted
3. **Target**
   - For testing, use **PC-VR**: connect Quest 2 via Link/Air Link
4. **Play**
   - Enter **Play Mode** in `StartMenu.unity`, choose mode and language, press **Start**
   - Or **Build & Run** for PC-VR

---

<img width="497" height="221" alt="sample_msc_combo-ger" src="https://github.com/user-attachments/assets/8bce4300-3276-4829-87ed-7ff93415fa0c" />

---

### 4.1) External Assets
This project requires several external 3D assets that are not included in this repository
due to license restrictions.  
Please refer to [`/Docs/ExternalAssets.md`](Docs/ExternalAssets.md) for a complete list
and download links.

---

## 5) Usage

- **Start Menu Options**
  - **Mode:** M1 (no grab) / M2 (controller) / M3 (hand-tracking)
  - **Order:** Fixed randomized vs. randomized
  - **Source→Target:** e.g., EN→VIM, DE→VIM
- **In-Scene**
  - **Teleport** to each station
  - **Read labels** (mother tongue + target)
  - **Point speech-bubble** to **play audio**
  - **M2/M3:** **Grab** objects; **drop into basket** when learned

---

## 6) Research Context & Results

- Built to probe embodied learning and VARK benefits in VR.
- **Two expert studies**: SUS (good→excellent), NASA-TLX, no cybersickness reported; tutorial scene improved UX.
- No long-term retention test included (future work).

---

## 7) Acknowledgements

Developed at **Johannes Kepler University Linz**. For theoretical background, design decisions, and study methodology, see the accompanying Master’s Thesis (Development and Evaluation of a VR Application in a Learning Environment).
