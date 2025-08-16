# 🎮 Rock Paper Scissors Unity Game

A modern, feature-rich **Rock Paper Scissors** game built with Unity Engine using **UI Toolkit** and following clean architecture principles.

![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue)
![C#](https://img.shields.io/badge/C%23-9.0-green)
![UI Toolkit](https://img.shields.io/badge/UI%20Toolkit-Latest-orange)
![License](https://img.shields.io/badge/License-MIT-yellow)

## ✨ Features

- 🎯 **Classic Rock Paper Scissors Gameplay** - First to 5 wins!
- 🎨 **Modern UI Design** - Built with Unity's UI Toolkit
- 🏗️ **Clean Architecture** - SOLID principles and design patterns
- 🧩 **Atomic Design System** - Scalable UI component architecture
- 🔊 **Audio Integration** - Sound effects for enhanced experience
- 🤖 **AI Opponent** - Random strategy bot with extensible AI system
- 🎉 **Game End Celebration** - Victory popup with restart functionality
- 📱 **Responsive Design** - Adaptable to different screen sizes

## 🏗️ Architecture

This project demonstrates professional Unity development practices:

### Design Patterns Used
- **Observer Pattern** - Game state notifications
- **Strategy Pattern** - Bot AI implementations
- **Dependency Inversion** - Loose coupling between components
- **Atomic Design** - UI component hierarchy

### Project Structure
```
Assets/
├── Scripts/
│   ├── Core/           # Game logic and interfaces
│   ├── Managers/       # Game controllers
│   └── UI/            # UI components (Atoms, Molecules, Organisms)
├── UI/
│   ├── UXML/          # UI layout files
│   └── USS/           # Styling sheets
└── Sprites/           # Game assets
```

## 🎯 Core Components

### Game Logic
- **GameService** - Core game rules and scoring
- **GameState** - Current game state management
- **IBotStrategy** - Extensible AI system
- **IGameService** - Game logic abstraction

### UI Architecture (Atomic Design)
- **Atoms** - Basic UI elements (ScoreAtom, ChoiceDisplayAtom)
- **Molecules** - Component groups (ScoreDisplayMolecule, ChoiceButtonsMolecule)
- **Organisms** - Complex UI sections (GameUIOrganism)
- **Templates** - Complete game layout

## 🚀 Getting Started

### Prerequisites
- Unity 2022.3 LTS or newer
- Basic understanding of Unity UI Toolkit

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/rock-paper-scissors-unity.git
   ```

2. Open the project in Unity

3. Assign sprites to the game controller:
   - Rock sprite
   - Paper sprite
   - Scissors sprite
   - Default sprite

4. (Optional) Add audio clips for sound effects

5. Press Play to start the game!

## 🎮 How to Play

1. Choose your weapon: Rock 🪨, Paper 📄, or Scissors ✂️
2. The bot will make its choice simultaneously
3. Win conditions:
   - Rock beats Scissors
   - Scissors beats Paper
   - Paper beats Rock
4. First player to reach 5 points wins!
5. Click "New Game" to restart

## 🔧 Customization

### Adding New Bot Strategies
Implement the `IBotStrategy` interface:

```csharp
public class SmartBotStrategy : IBotStrategy
{
    public GameChoice MakeChoice()
    {
        // Your AI logic here
        return GameChoice.Rock;
    }
}
```

### Modifying UI
The UI is built with atomic design principles:
- Edit **Atoms** for basic elements
- Combine **Atoms** into **Molecules**
- Compose **Molecules** into **Organisms**

### Styling
Modify USS files in `Assets/UI/USS/` to change the visual appearance.

## 📂 File Structure Explained

### Core Scripts
- `GameEnums.cs` - Game enumerations
- `GameState.cs` - Game state data structure
- `GameService.cs` - Core game logic
- `Interfaces.cs` - System interfaces
- `RandomBotStrategy.cs` - Basic AI implementation

### UI Components
- `ScoreAtom.cs` - Individual score display
- `ChoiceDisplayAtom.cs` - Choice image display
- `ChoiceButtonAtom.cs` - Individual choice button
- `BattleDisplayMolecule.cs` - VS battle area
- `ScoreDisplayMolecule.cs` - Score board
- `ChoiceButtonsMolecule.cs` - Button group
- `GameUIOrganism.cs` - Complete game UI
- `GameEndPopup.cs` - Victory popup

### Controllers
- `RockPaperScissorsGameController.cs` - Main game controller

## 🎨 UI Toolkit Features Used

- **Visual Elements** - Custom UI components
- **USS Styling** - CSS-like styling system
- **UXML Layout** - Declarative UI layout
- **Event System** - Modern event handling
- **Flexbox Layout** - Responsive design
- **Custom Controls** - Reusable UI components

## 🔮 Future Enhancements

- [ ] Multiplayer support
- [ ] Different game modes (Best of 3, 7, etc.)
- [ ] Tournament bracket system
- [ ] Player statistics tracking
- [ ] Advanced AI opponents
- [ ] Animation system
- [ ] Particle effects
- [ ] Mobile touch controls
- [ ] Save/Load game progress
- [ ] Themes and customization

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### Development Guidelines
1. Follow SOLID principles
2. Maintain atomic design structure
3. Add unit tests for new features
4. Update documentation as needed
5. Follow C# coding standards

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Unity Technologies for the amazing UI Toolkit
- The community for feedback and suggestions
- Contributors who help improve this project

## 📞 Support

If you encounter any issues or have questions:
1. Check the [Issues](https://github.com/yourusername/rock-paper-scissors-unity/issues) page
2. Create a new issue with detailed description
3. Contact the maintainers

---

⭐ **Star this repository if you found it helpful!**

Made with ❤️ using Unity Engine
