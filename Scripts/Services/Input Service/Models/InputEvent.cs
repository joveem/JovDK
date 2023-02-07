// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Services.Input
{
    public class InputEvent
    {
        public InputType InputType = InputType.UNDEFINED;
        public InputSubType InputSubType = InputSubType.UNDEFINED;
        public float Value = -2f;
    }

    public enum InputType
    {
        UNDEFINED = -1,
        Button,
        Axys,
        Trigger
    }

    public enum InputSubType
    {
        UNDEFINED = -1,
        Press,
        Release,
        ValueChange,
    }

    public class InputEmitter
    {
        public Action<InputEvent> InputCallback = null;
    }

    ////////////////

    public class InputDefinition
    {
        public Type InputType = null;
        public List<int> InputButtonsList = new List<int>();
        public List<int> InputAxysList = new List<int>();

        static InputDefinition DEBUGDefaultExampleGame()
        {
            InputDefinition value = new InputDefinition();

            value.InputType = typeof(ExampleGameInput);

            value.InputButtonsList.Add((int)ExampleGameInput.Jump);

            value.InputAxysList.Add((int)ExampleGameInput.MovementX);
            value.InputAxysList.Add((int)ExampleGameInput.MovementY);
            value.InputAxysList.Add((int)ExampleGameInput.CameraX);
            value.InputAxysList.Add((int)ExampleGameInput.CameraY);

            return value;
        }
    }

    public class InputHandlerDefinition
    {
        public Type InputType = null;
        public Type OutputType = null;

        List<InputRelationship> InputsRelationshipsList = new List<InputRelationship>();

        static InputHandlerDefinition DEBUGDefaultExampleGame()
        {
            InputHandlerDefinition value = new InputHandlerDefinition();

            value.InputType = typeof(ExampleGameKeyboardMouseInput);
            value.OutputType = typeof(ExampleGameInput);

            // jump
            InputRelationship jumpInputRelationship = new InputRelationship();
            jumpInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.Space;
            jumpInputRelationship.Output = (int)ExampleGameInput.Jump;
            jumpInputRelationship.InputRelationshipModifier = InputRelationshipModifier.NONE;

            // movement-x (negative)
            InputRelationship movementXNegativeInputRelationship = new InputRelationship();
            movementXNegativeInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.A;
            movementXNegativeInputRelationship.Output = (int)ExampleGameInput.MovementX;
            movementXNegativeInputRelationship.InputRelationshipModifier = InputRelationshipModifier.NegativeOnly;
            // movement-x (positive)
            InputRelationship movementXPositiveInputRelationship = new InputRelationship();
            movementXPositiveInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.D;
            movementXPositiveInputRelationship.Output = (int)ExampleGameInput.MovementX;
            movementXPositiveInputRelationship.InputRelationshipModifier = InputRelationshipModifier.PositiveOnly;

            // camera-y (negative)
            InputRelationship movementYNegativeInputRelationship = new InputRelationship();
            movementYNegativeInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.S;
            movementYNegativeInputRelationship.Output = (int)ExampleGameInput.MovementY;
            movementYNegativeInputRelationship.InputRelationshipModifier = InputRelationshipModifier.NegativeOnly;
            // movement-y (positive)
            InputRelationship movementYPositiveInputRelationship = new InputRelationship();
            movementYPositiveInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.W;
            movementYPositiveInputRelationship.Output = (int)ExampleGameInput.MovementY;
            movementYPositiveInputRelationship.InputRelationshipModifier = InputRelationshipModifier.PositiveOnly;

            // camera-x (negative)
            InputRelationship cameraXNegativeInputRelationship = new InputRelationship();
            cameraXNegativeInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.MouseLeft;
            cameraXNegativeInputRelationship.Output = (int)ExampleGameInput.CameraX;
            cameraXNegativeInputRelationship.InputRelationshipModifier = InputRelationshipModifier.NegativeOnly;
            // camera-x (positive)
            InputRelationship cameraXPositiveInputRelationship = new InputRelationship();
            cameraXPositiveInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.MouseRight;
            cameraXPositiveInputRelationship.Output = (int)ExampleGameInput.CameraX;
            cameraXPositiveInputRelationship.InputRelationshipModifier = InputRelationshipModifier.PositiveOnly;

            // camera-y (negative)
            InputRelationship cameraYNegativeInputRelationship = new InputRelationship();
            cameraYNegativeInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.MouseUp;
            cameraYNegativeInputRelationship.Output = (int)ExampleGameInput.CameraY;
            cameraYNegativeInputRelationship.InputRelationshipModifier = InputRelationshipModifier.NegativeOnly;
            // camera-y (positive)
            InputRelationship cameraYPositiveInputRelationship = new InputRelationship();
            cameraYPositiveInputRelationship.Input = (int)ExampleGameKeyboardMouseInput.MouseDown;
            cameraYPositiveInputRelationship.Output = (int)ExampleGameInput.CameraY;
            cameraYPositiveInputRelationship.InputRelationshipModifier = InputRelationshipModifier.PositiveOnly;


            value.InputsRelationshipsList.Add(jumpInputRelationship);
            // movement
            value.InputsRelationshipsList.Add(movementXNegativeInputRelationship);
            value.InputsRelationshipsList.Add(movementXPositiveInputRelationship);
            value.InputsRelationshipsList.Add(movementYNegativeInputRelationship);
            value.InputsRelationshipsList.Add(movementYPositiveInputRelationship);
            // camera
            value.InputsRelationshipsList.Add(cameraXNegativeInputRelationship);
            value.InputsRelationshipsList.Add(cameraXPositiveInputRelationship);
            value.InputsRelationshipsList.Add(cameraYNegativeInputRelationship);
            value.InputsRelationshipsList.Add(cameraYPositiveInputRelationship);

            return value;
        }
    }

    public class InputRelationship
    {
        public int Input;
        public int Output;
        public InputRelationshipModifier InputRelationshipModifier = InputRelationshipModifier.UNDEFINED;
    }

    public enum ExampleGameInput
    {
        UNDEFINED = -1,
        Jump,
        MovementX,
        MovementY,
        CameraX,
        CameraY,
    }

    public enum ExampleGameKeyboardMouseInput
    {
        UNDEFINED = -1,
        Space,
        W,
        A,
        S,
        D,
        MouseLeft,
        MouseRight,
        MouseUp,
        MouseDown,
    }

    public enum InputRelationshipModifier
    {
        UNDEFINED = -1,
        NONE,
        PositiveOnly,
        NegativeOnly,
    }
}

// // input definition
// {
//     "InputType": "ExampleGameInput",
//     "Buttons": [
//         1 // jump
//     ],
//     "Axys": [
//         2, // movement-x
//         3, // movement-y
//         4, // camera-x
//         5, // camera-y
//     ]
// }
// // input handler definition
// {
//     "InputType": "ExampleGameKeyboardMouseInput",
//     "OutputType": "ExampleGameOutput",
//     [
//         {
//             "input": 90, // "KeyCode.Space"
//             "output": 1 // jump
//         },
//         {
//             "input": 91, // "KeyCode.A"
//             "output": 2 // movement-x
//         },
//         {
//             "input": 92, // "KeyCode.D"
//             "output": 2 // movement-x
//         },
//     ]
// }
