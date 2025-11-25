# Beat Invasion

**Beat Invasion** is a Unity-based rhythm game demonstrating core gameplay mechanics such as timing-based note hits, movement logic, and audio synchronization. The build included in this repository allows players to experience the full game and test the rhythm mechanics.

### Gameplay Demo
[![Gameplay Video](http://img.youtube.com/vi/mFOqnpyjhA8/0.jpg)](https://youtu.be/mFOqnpyjhA8)
*> Click the image above to watch the gameplay video!*

## Downloads & Resources
* **[Download Windows PC Build (.zip)](BeatInvasion_PC.zip)** - Download, extract, and run `My project.exe` to play.
* **[Google Drive (Game Files & Build)](https://drive.google.com/drive/folders/1U0XsKV5qSrTZACtotFhodXx0Y0XIt5l7?usp=sharing)** - Use this if the repository files are not accessible.

## Game Features
* **Rhythm Gameplay:** Hit notes in sync with the track to score points and damage enemies.
* **Multiple Note Types:** Includes Single hits, Double hits, and Hold notes.
* **Accuracy System:** Scoring based on timing (Perfect, Good, Miss) tracked by the `AccuracySystem`.
* **Boss Battles:** Challenging levels featuring boss mechanics.
* **Dynamic Visuals:** Space-themed backgrounds and particle effects on impact.

## System Architecture
The game utilizes an Object-Oriented architecture to manage game states and interactions. Key classes include:
* **`BeatSpawner`**: Handles reading `BeatmapData` and spawning notes at the correct intervals.
* **`ScoreManager`**: Tracks current combos, final scores, and effect application.
* **`NoteBase`**: A parent class for different note types (`NoteSingleHit`, `NoteDoubleHit`, `NoteHoldHit`) to share common logic like miss detection and visual effects.

## UML Diagram
<img width="1701" height="2281" alt="OOPUMLNEW" src="https://github.com/user-attachments/assets/a5918cd3-445e-4eb4-8afb-3586339a8e99" />

## Technologies
* **Engine:** Unity 2022 (C#)
* **Version Control:** Git & GitHub (LFS enabled)
* **IDE:** Visual Studio / VS Code

## How to Run the Project
1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Kheshwen/Beat-Invasion_Rhythm-Unity.git](https://github.com/Kheshwen/Beat-Invasion_Rhythm-Unity.git)
    ```
2.  **Open in Unity:**
    * Launch **Unity Hub**.
    * Click **Add** -> **Add project from disk**.
    * Select the project folder.
3.  **Play:**
    * Open the `Assets/Scenes firrst mode/LevelSelectMenu.unity` scene.
    * Press the **Play** button.

## Credits
* **Developer:** --
* **Music & Assets:** Original

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
