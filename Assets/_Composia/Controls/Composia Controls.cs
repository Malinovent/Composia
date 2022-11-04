// GENERATED AUTOMATICALLY FROM 'Assets/_Composia/Gameplays/All Gameplays/Scripts/Composia Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ComposiaControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ComposiaControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Composia Controls"",
    ""maps"": [
        {
            ""name"": ""Platforming"",
            ""id"": ""919e4b39-21c6-49c6-82a3-d3f89e9334ce"",
            ""actions"": [
                {
                    ""name"": ""Jump_Press"",
                    ""type"": ""Button"",
                    ""id"": ""f090c568-a058-438b-9ff5-75c309f8718e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump_Release"",
                    ""type"": ""Button"",
                    ""id"": ""65d92202-4a5d-4bde-a3f9-227c464bd0b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump_Hold"",
                    ""type"": ""Button"",
                    ""id"": ""0a80b43d-dbbf-4815-88a8-f8d1d0bc6b6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pows"",
                    ""type"": ""Button"",
                    ""id"": ""a96c60ae-66bd-44f2-a318-053d53a7c58a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""da0494a1-ffae-41d3-80cc-1d1c66370b69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch Modes"",
                    ""type"": ""Button"",
                    ""id"": ""86032b26-f8f5-45ad-88c2-e23df6c0ee0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""1a36da38-9f2c-46e5-8094-d8bc3e1eca19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Charge_Press"",
                    ""type"": ""Button"",
                    ""id"": ""0d663129-1867-4f2b-a6be-0c622d9ec640"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Charge_Hold"",
                    ""type"": ""Button"",
                    ""id"": ""4107abd1-3f3c-494e-9306-c3fd062f4d1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd483d1f-894b-4239-bdbd-3b071b6ce2b0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d7db1a2-cfc0-45f4-96e5-dc18cba6ac38"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3499ed06-4cf7-49af-8da9-0618eaf1984d"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b03cf08-68bd-4460-974e-427b10f9549c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ba51e8b-63bf-407b-b059-594dece753b3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f2bd346-bf1f-4c01-a4f8-9b18f85b6c91"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42111ecb-b041-4c74-aa54-b6c40844c4e9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Switch Modes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55859468-fb8e-4009-bd75-5e74cd43925a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch Modes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a5f8392-dd17-4743-a607-72f5f646f08a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4527c5a5-d655-4fdd-b9a9-826fb891a10c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38805b72-9063-43b6-82b2-0f2ee11e9613"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump_Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d86dad2f-6339-4411-a1ce-d8751a54732f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump_Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4109a5e-5db7-4c8e-9db0-e5270d844262"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump_Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""811f63e7-fa06-45ed-8e20-b3ccc6d6d2f0"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge_Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbeaea30-8e6b-44e4-b793-dc3aa3aae4ba"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge_Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Creating"",
            ""id"": ""22c0117d-3877-44c7-be2e-db0850aeb7c0"",
            ""actions"": [
                {
                    ""name"": ""Place Piece"",
                    ""type"": ""Button"",
                    ""id"": ""9e65b96a-fcaf-4e1d-8451-a3ff64698ebe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Choose Piece"",
                    ""type"": ""Button"",
                    ""id"": ""165e5093-7874-4bfd-9532-251d1b051be5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Erase Piece"",
                    ""type"": ""Button"",
                    ""id"": ""bf9b6bc9-ff15-4f42-907e-51d1643b2e4d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Next Role"",
                    ""type"": ""Button"",
                    ""id"": ""a7314f68-6d42-4680-938b-5c782b9120ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Previous Role"",
                    ""type"": ""Button"",
                    ""id"": ""bdd6d265-7213-4c86-933b-4e2dbb55a507"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rotate Measure Clockwise"",
                    ""type"": ""Button"",
                    ""id"": ""a151c19c-28ee-4731-9fa1-0b44564cc1d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rotate Measure Counter-Clockwise"",
                    ""type"": ""Button"",
                    ""id"": ""e141e459-9d00-4bbe-b04d-908ffa2e2d8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Previous Measure"",
                    ""type"": ""Button"",
                    ""id"": ""7bb2d9a8-1473-4bfc-be8c-fcc7731d7a31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Next Measure"",
                    ""type"": ""Button"",
                    ""id"": ""3eb11ea2-3a36-4353-bd27-7b7a79021a14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Switch Modes"",
                    ""type"": ""Button"",
                    ""id"": ""be465498-68ae-40ac-90a8-0e1b1878a32c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Move Cursor"",
                    ""type"": ""Value"",
                    ""id"": ""971cb8dd-b214-4e97-a012-6bcc6e7f76b0"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch Scales"",
                    ""type"": ""Button"",
                    ""id"": ""5d989ed5-32c5-4bd5-b893-a7cd3573fdf3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Return To World"",
                    ""type"": ""Button"",
                    ""id"": ""a211b6d8-e507-46b4-bd95-87a416f6c8a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cece1554-ae24-4ddc-9f14-cc633b623803"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Place Piece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""095660b0-aab6-4061-b347-67f3dd9ef44a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Place Piece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ea959fc-a292-403d-8791-8323349c6c1a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Choose Piece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eaef729-45d7-4c6f-8579-38855b8d4dcc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Choose Piece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79057646-7efe-4b75-9f40-ab38295fc019"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate Measure Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""519a633d-6f40-40c4-9bcc-42ef2b0e6de7"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rotate Measure Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a32013f-1369-4d43-a454-9075aa24057b"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate Measure Counter-Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff10738f-46c7-4eb9-91ad-af6534a98ecd"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rotate Measure Counter-Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""041c8eb1-3ba4-4061-9dd6-38405b4f1d2e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Previous Role"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17b0285d-0752-45bb-a516-76800f9303c1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Next Role"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22c08058-2fa1-4c0d-9c00-022d6e7571bb"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Next Role"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86545d4d-1b5a-40d6-9878-00aeb7c5975b"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Previous Measure"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""576bd9b2-8c7c-4145-bf33-ad38d28544da"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Next Measure"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07e445a4-66ca-4539-96a1-ae96a2d696b1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Erase Piece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7796ced-781f-408b-98af-d9ec7b5f0e7f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Erase Piece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9076ab3-b0b5-4e92-ad32-55a590f54a7b"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Switch Modes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3f45117-4ca1-4487-9194-5062a9138686"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch Modes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d138543e-c1bc-4cd8-a458-0bc3b206f82d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d96d5c4-f698-41bc-8423-8717fd42994e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch Scales"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30cff4a6-0f3a-4b27-9690-413d6d793e60"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Return To World"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Performance"",
            ""id"": ""8ac12c59-b689-4eef-a0a6-ff471ca96528"",
            ""actions"": [
                {
                    ""name"": ""Retry"",
                    ""type"": ""Button"",
                    ""id"": ""ec2f52e0-0fc6-4cb7-8c18-d54ac02a9a52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""8a35afef-754e-4b76-be6b-0926ab8f68b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Press Repeat"",
                    ""type"": ""Button"",
                    ""id"": ""b75f8697-d1b1-4b41-8593-ec6b540264f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down/Up"",
                    ""type"": ""Value"",
                    ""id"": ""8b475417-0b48-469d-9fed-3e131f038ae5"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SetBPM"",
                    ""type"": ""Value"",
                    ""id"": ""870cc5e0-fd7a-4517-aa99-166fdc5e3785"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Play"",
                    ""type"": ""Button"",
                    ""id"": ""e81015b9-d7aa-47ff-99c2-0a1558f182e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2a849690-4972-4c40-bc4c-dc65ff233ecf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Retry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6009873-d999-4ac6-8fdc-b2fe67f53c62"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62cc55dc-752d-41fb-adf2-44496ca17fa0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Press Repeat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72e70196-7ebc-48e6-a6ec-488772d77194"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Down/Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f513d0b-bb1c-474d-8dd1-c8a7aadebc28"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SetBPM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2356cf0-4091-4583-a905-2ad21b2d087e"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Play"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Arrangement"",
            ""id"": ""d8ac5e11-79f0-48f4-a462-edf387739ca8"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""2f115813-8261-4537-b759-852525fed601"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8f7bf4d3-eeb9-461a-889b-fe04a645ece0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Exploration"",
            ""id"": ""14cdc8ab-162f-4d1e-b989-4f85aef27bfe"",
            ""actions"": [
                {
                    ""name"": ""Pow"",
                    ""type"": ""Button"",
                    ""id"": ""4d6a2da8-a3fc-4c73-b787-3d497e1873ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f339b2f3-308a-4909-9b6d-2bff2d00f9a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7c9b7d4b-ec9a-46b1-b807-4bbcd94757b0"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""472ed380-4eb1-40fb-b7ed-efade46a56a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1,behavior=2)""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""3a58d3a3-e091-4aa2-908a-0032514b9246"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c2f4ccd9-cd54-4d0f-b8dd-755abf9e867b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2d15317-ab9f-4be5-a473-482c3355274a"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eefa3b39-1725-481b-a0fc-722c58fde5c9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""869af55c-c164-4b4e-aee1-28d5f474f165"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""9b62c5b4-d27d-4883-89f7-6c0f2b057693"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a691be2-cd1e-4219-8742-2dd24ce6840a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0d82c6d9-c586-4634-a533-2e48d928fafd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7e388b24-f2b3-431f-9ef6-8bf862b182ab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""605ed4db-00fb-4475-af2b-37cf34b508eb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1857bb2c-6dc4-403d-b777-f4dcb19f8592"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a89b83c4-ff86-4c82-a894-c1955ba8f041"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b148656-f011-4d98-b0e4-5b1c67e7eb5a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eee4eaa2-73ea-4332-96ba-0f70b0a0dee5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e413e6e-44ed-4037-a4ec-17543668f8f0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Puzzle"",
            ""id"": ""f790a668-0ffd-415e-a4ca-c70b7a50fb9e"",
            ""actions"": [
                {
                    ""name"": ""Confirmation"",
                    ""type"": ""Button"",
                    ""id"": ""e0391e86-d0b2-4d96-b956-2e0cc2e5c98e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Previous"",
                    ""type"": ""Button"",
                    ""id"": ""a29beb20-0a95-4faa-a0d3-57dbed96df07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""dfc734b1-7d92-4710-97e9-4f59c3638d86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""5c2ad9cd-ce72-4fa0-82a0-1f656a733bcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cfe3c6c2-ca4b-46e9-b75d-bb60e16699e9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirmation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c10171a-ccd4-4fcd-ae23-e5d9ea9c713b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirmation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0a0d5ab-0ffd-40db-bf7a-27429a4de269"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Previous"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e322cd1-7505-44c9-9efe-d12d453a7e41"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Previous"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55503675-e459-406f-b060-af664318890f"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42c90318-800e-4a2e-8a1d-c81d71c8e4a1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf11cdd9-aa1d-4c7e-aa56-140d9a2aa2f5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""8f7640db-5b8a-474b-b304-1edf725a3b11"",
            ""actions"": [
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""bb68b94d-64ab-41fa-8bf5-a6236bde9a51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""0355539b-e093-4809-af5c-48e1c587942b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""30b57847-f946-471a-994d-546d06047dcf"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2425a3ae-5a01-47d2-b5ed-1fd0624ac790"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ac05018-1426-4ddb-a41b-7860bdd1f44a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""216dfafd-d684-4f3a-b826-f999396fe38c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bd5c700-3efc-441b-ba05-ab48aa60d2b0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""980192dc-8273-47c0-8cda-0f59e780a40c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3155bbe2-2bf5-4e68-a47c-ea8361a59ae2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Movement"",
                    ""id"": ""cc53d975-2386-4f25-85ff-72d4a8d39e1e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""968fc9f7-40da-4692-8ef1-8798c567da4a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f0baef9d-a415-4b65-b23e-7822f1cde83c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""de96f150-30ad-42db-a2be-88859ac266d8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""27f90209-4cec-414d-8e7c-352c4d650589"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard Movement"",
                    ""id"": ""777f4506-0cb7-448e-8ef9-af173ecce15c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fadd83b5-8f82-45b8-a531-283bae0d5761"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ff217578-9e4b-4c99-b03c-34bc559b44b2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""57eceb6e-e886-4d6e-bdfa-be280841944d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b9d42382-3d7f-4044-ab23-56b043dd7585"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Platforming
        m_Platforming = asset.FindActionMap("Platforming", throwIfNotFound: true);
        m_Platforming_Jump_Press = m_Platforming.FindAction("Jump_Press", throwIfNotFound: true);
        m_Platforming_Jump_Release = m_Platforming.FindAction("Jump_Release", throwIfNotFound: true);
        m_Platforming_Jump_Hold = m_Platforming.FindAction("Jump_Hold", throwIfNotFound: true);
        m_Platforming_Pows = m_Platforming.FindAction("Pows", throwIfNotFound: true);
        m_Platforming_Start = m_Platforming.FindAction("Start", throwIfNotFound: true);
        m_Platforming_SwitchModes = m_Platforming.FindAction("Switch Modes", throwIfNotFound: true);
        m_Platforming_Restart = m_Platforming.FindAction("Restart", throwIfNotFound: true);
        m_Platforming_Charge_Press = m_Platforming.FindAction("Charge_Press", throwIfNotFound: true);
        m_Platforming_Charge_Hold = m_Platforming.FindAction("Charge_Hold", throwIfNotFound: true);
        // Creating
        m_Creating = asset.FindActionMap("Creating", throwIfNotFound: true);
        m_Creating_PlacePiece = m_Creating.FindAction("Place Piece", throwIfNotFound: true);
        m_Creating_ChoosePiece = m_Creating.FindAction("Choose Piece", throwIfNotFound: true);
        m_Creating_ErasePiece = m_Creating.FindAction("Erase Piece", throwIfNotFound: true);
        m_Creating_NextRole = m_Creating.FindAction("Next Role", throwIfNotFound: true);
        m_Creating_PreviousRole = m_Creating.FindAction("Previous Role", throwIfNotFound: true);
        m_Creating_RotateMeasureClockwise = m_Creating.FindAction("Rotate Measure Clockwise", throwIfNotFound: true);
        m_Creating_RotateMeasureCounterClockwise = m_Creating.FindAction("Rotate Measure Counter-Clockwise", throwIfNotFound: true);
        m_Creating_PreviousMeasure = m_Creating.FindAction("Previous Measure", throwIfNotFound: true);
        m_Creating_NextMeasure = m_Creating.FindAction("Next Measure", throwIfNotFound: true);
        m_Creating_SwitchModes = m_Creating.FindAction("Switch Modes", throwIfNotFound: true);
        m_Creating_MoveCursor = m_Creating.FindAction("Move Cursor", throwIfNotFound: true);
        m_Creating_SwitchScales = m_Creating.FindAction("Switch Scales", throwIfNotFound: true);
        m_Creating_ReturnToWorld = m_Creating.FindAction("Return To World", throwIfNotFound: true);
        // Performance
        m_Performance = asset.FindActionMap("Performance", throwIfNotFound: true);
        m_Performance_Retry = m_Performance.FindAction("Retry", throwIfNotFound: true);
        m_Performance_Quit = m_Performance.FindAction("Quit", throwIfNotFound: true);
        m_Performance_PressRepeat = m_Performance.FindAction("Press Repeat", throwIfNotFound: true);
        m_Performance_DownUp = m_Performance.FindAction("Down/Up", throwIfNotFound: true);
        m_Performance_SetBPM = m_Performance.FindAction("SetBPM", throwIfNotFound: true);
        m_Performance_Play = m_Performance.FindAction("Play", throwIfNotFound: true);
        // Arrangement
        m_Arrangement = asset.FindActionMap("Arrangement", throwIfNotFound: true);
        m_Arrangement_Newaction = m_Arrangement.FindAction("New action", throwIfNotFound: true);
        // Exploration
        m_Exploration = asset.FindActionMap("Exploration", throwIfNotFound: true);
        m_Exploration_Pow = m_Exploration.FindAction("Pow", throwIfNotFound: true);
        m_Exploration_Jump = m_Exploration.FindAction("Jump", throwIfNotFound: true);
        m_Exploration_Move = m_Exploration.FindAction("Move", throwIfNotFound: true);
        m_Exploration_Interact = m_Exploration.FindAction("Interact", throwIfNotFound: true);
        m_Exploration_Cancel = m_Exploration.FindAction("Cancel", throwIfNotFound: true);
        // Puzzle
        m_Puzzle = asset.FindActionMap("Puzzle", throwIfNotFound: true);
        m_Puzzle_Confirmation = m_Puzzle.FindAction("Confirmation", throwIfNotFound: true);
        m_Puzzle_Previous = m_Puzzle.FindAction("Previous", throwIfNotFound: true);
        m_Puzzle_Next = m_Puzzle.FindAction("Next", throwIfNotFound: true);
        m_Puzzle_Cancel = m_Puzzle.FindAction("Cancel", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Confirm = m_Menu.FindAction("Confirm", throwIfNotFound: true);
        m_Menu_Back = m_Menu.FindAction("Back", throwIfNotFound: true);
        m_Menu_Navigate = m_Menu.FindAction("Navigate", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Platforming
    private readonly InputActionMap m_Platforming;
    private IPlatformingActions m_PlatformingActionsCallbackInterface;
    private readonly InputAction m_Platforming_Jump_Press;
    private readonly InputAction m_Platforming_Jump_Release;
    private readonly InputAction m_Platforming_Jump_Hold;
    private readonly InputAction m_Platforming_Pows;
    private readonly InputAction m_Platforming_Start;
    private readonly InputAction m_Platforming_SwitchModes;
    private readonly InputAction m_Platforming_Restart;
    private readonly InputAction m_Platforming_Charge_Press;
    private readonly InputAction m_Platforming_Charge_Hold;
    public struct PlatformingActions
    {
        private @ComposiaControls m_Wrapper;
        public PlatformingActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump_Press => m_Wrapper.m_Platforming_Jump_Press;
        public InputAction @Jump_Release => m_Wrapper.m_Platforming_Jump_Release;
        public InputAction @Jump_Hold => m_Wrapper.m_Platforming_Jump_Hold;
        public InputAction @Pows => m_Wrapper.m_Platforming_Pows;
        public InputAction @Start => m_Wrapper.m_Platforming_Start;
        public InputAction @SwitchModes => m_Wrapper.m_Platforming_SwitchModes;
        public InputAction @Restart => m_Wrapper.m_Platforming_Restart;
        public InputAction @Charge_Press => m_Wrapper.m_Platforming_Charge_Press;
        public InputAction @Charge_Hold => m_Wrapper.m_Platforming_Charge_Hold;
        public InputActionMap Get() { return m_Wrapper.m_Platforming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlatformingActions set) { return set.Get(); }
        public void SetCallbacks(IPlatformingActions instance)
        {
            if (m_Wrapper.m_PlatformingActionsCallbackInterface != null)
            {
                @Jump_Press.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Press;
                @Jump_Press.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Press;
                @Jump_Press.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Press;
                @Jump_Release.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Release;
                @Jump_Release.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Release;
                @Jump_Release.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Release;
                @Jump_Hold.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Hold;
                @Jump_Hold.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Hold;
                @Jump_Hold.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump_Hold;
                @Pows.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPows;
                @Pows.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPows;
                @Pows.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPows;
                @Start.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnStart;
                @SwitchModes.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnSwitchModes;
                @SwitchModes.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnSwitchModes;
                @SwitchModes.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnSwitchModes;
                @Restart.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnRestart;
                @Charge_Press.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCharge_Press;
                @Charge_Press.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCharge_Press;
                @Charge_Press.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCharge_Press;
                @Charge_Hold.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCharge_Hold;
                @Charge_Hold.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCharge_Hold;
                @Charge_Hold.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCharge_Hold;
            }
            m_Wrapper.m_PlatformingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump_Press.started += instance.OnJump_Press;
                @Jump_Press.performed += instance.OnJump_Press;
                @Jump_Press.canceled += instance.OnJump_Press;
                @Jump_Release.started += instance.OnJump_Release;
                @Jump_Release.performed += instance.OnJump_Release;
                @Jump_Release.canceled += instance.OnJump_Release;
                @Jump_Hold.started += instance.OnJump_Hold;
                @Jump_Hold.performed += instance.OnJump_Hold;
                @Jump_Hold.canceled += instance.OnJump_Hold;
                @Pows.started += instance.OnPows;
                @Pows.performed += instance.OnPows;
                @Pows.canceled += instance.OnPows;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @SwitchModes.started += instance.OnSwitchModes;
                @SwitchModes.performed += instance.OnSwitchModes;
                @SwitchModes.canceled += instance.OnSwitchModes;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @Charge_Press.started += instance.OnCharge_Press;
                @Charge_Press.performed += instance.OnCharge_Press;
                @Charge_Press.canceled += instance.OnCharge_Press;
                @Charge_Hold.started += instance.OnCharge_Hold;
                @Charge_Hold.performed += instance.OnCharge_Hold;
                @Charge_Hold.canceled += instance.OnCharge_Hold;
            }
        }
    }
    public PlatformingActions @Platforming => new PlatformingActions(this);

    // Creating
    private readonly InputActionMap m_Creating;
    private ICreatingActions m_CreatingActionsCallbackInterface;
    private readonly InputAction m_Creating_PlacePiece;
    private readonly InputAction m_Creating_ChoosePiece;
    private readonly InputAction m_Creating_ErasePiece;
    private readonly InputAction m_Creating_NextRole;
    private readonly InputAction m_Creating_PreviousRole;
    private readonly InputAction m_Creating_RotateMeasureClockwise;
    private readonly InputAction m_Creating_RotateMeasureCounterClockwise;
    private readonly InputAction m_Creating_PreviousMeasure;
    private readonly InputAction m_Creating_NextMeasure;
    private readonly InputAction m_Creating_SwitchModes;
    private readonly InputAction m_Creating_MoveCursor;
    private readonly InputAction m_Creating_SwitchScales;
    private readonly InputAction m_Creating_ReturnToWorld;
    public struct CreatingActions
    {
        private @ComposiaControls m_Wrapper;
        public CreatingActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlacePiece => m_Wrapper.m_Creating_PlacePiece;
        public InputAction @ChoosePiece => m_Wrapper.m_Creating_ChoosePiece;
        public InputAction @ErasePiece => m_Wrapper.m_Creating_ErasePiece;
        public InputAction @NextRole => m_Wrapper.m_Creating_NextRole;
        public InputAction @PreviousRole => m_Wrapper.m_Creating_PreviousRole;
        public InputAction @RotateMeasureClockwise => m_Wrapper.m_Creating_RotateMeasureClockwise;
        public InputAction @RotateMeasureCounterClockwise => m_Wrapper.m_Creating_RotateMeasureCounterClockwise;
        public InputAction @PreviousMeasure => m_Wrapper.m_Creating_PreviousMeasure;
        public InputAction @NextMeasure => m_Wrapper.m_Creating_NextMeasure;
        public InputAction @SwitchModes => m_Wrapper.m_Creating_SwitchModes;
        public InputAction @MoveCursor => m_Wrapper.m_Creating_MoveCursor;
        public InputAction @SwitchScales => m_Wrapper.m_Creating_SwitchScales;
        public InputAction @ReturnToWorld => m_Wrapper.m_Creating_ReturnToWorld;
        public InputActionMap Get() { return m_Wrapper.m_Creating; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CreatingActions set) { return set.Get(); }
        public void SetCallbacks(ICreatingActions instance)
        {
            if (m_Wrapper.m_CreatingActionsCallbackInterface != null)
            {
                @PlacePiece.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPlacePiece;
                @PlacePiece.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPlacePiece;
                @PlacePiece.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPlacePiece;
                @ChoosePiece.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnChoosePiece;
                @ChoosePiece.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnChoosePiece;
                @ChoosePiece.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnChoosePiece;
                @ErasePiece.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnErasePiece;
                @ErasePiece.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnErasePiece;
                @ErasePiece.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnErasePiece;
                @NextRole.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnNextRole;
                @NextRole.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnNextRole;
                @NextRole.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnNextRole;
                @PreviousRole.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPreviousRole;
                @PreviousRole.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPreviousRole;
                @PreviousRole.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPreviousRole;
                @RotateMeasureClockwise.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnRotateMeasureClockwise;
                @RotateMeasureClockwise.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnRotateMeasureClockwise;
                @RotateMeasureClockwise.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnRotateMeasureClockwise;
                @RotateMeasureCounterClockwise.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnRotateMeasureCounterClockwise;
                @RotateMeasureCounterClockwise.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnRotateMeasureCounterClockwise;
                @RotateMeasureCounterClockwise.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnRotateMeasureCounterClockwise;
                @PreviousMeasure.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPreviousMeasure;
                @PreviousMeasure.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPreviousMeasure;
                @PreviousMeasure.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnPreviousMeasure;
                @NextMeasure.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnNextMeasure;
                @NextMeasure.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnNextMeasure;
                @NextMeasure.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnNextMeasure;
                @SwitchModes.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnSwitchModes;
                @SwitchModes.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnSwitchModes;
                @SwitchModes.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnSwitchModes;
                @MoveCursor.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnMoveCursor;
                @SwitchScales.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnSwitchScales;
                @SwitchScales.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnSwitchScales;
                @SwitchScales.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnSwitchScales;
                @ReturnToWorld.started -= m_Wrapper.m_CreatingActionsCallbackInterface.OnReturnToWorld;
                @ReturnToWorld.performed -= m_Wrapper.m_CreatingActionsCallbackInterface.OnReturnToWorld;
                @ReturnToWorld.canceled -= m_Wrapper.m_CreatingActionsCallbackInterface.OnReturnToWorld;
            }
            m_Wrapper.m_CreatingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlacePiece.started += instance.OnPlacePiece;
                @PlacePiece.performed += instance.OnPlacePiece;
                @PlacePiece.canceled += instance.OnPlacePiece;
                @ChoosePiece.started += instance.OnChoosePiece;
                @ChoosePiece.performed += instance.OnChoosePiece;
                @ChoosePiece.canceled += instance.OnChoosePiece;
                @ErasePiece.started += instance.OnErasePiece;
                @ErasePiece.performed += instance.OnErasePiece;
                @ErasePiece.canceled += instance.OnErasePiece;
                @NextRole.started += instance.OnNextRole;
                @NextRole.performed += instance.OnNextRole;
                @NextRole.canceled += instance.OnNextRole;
                @PreviousRole.started += instance.OnPreviousRole;
                @PreviousRole.performed += instance.OnPreviousRole;
                @PreviousRole.canceled += instance.OnPreviousRole;
                @RotateMeasureClockwise.started += instance.OnRotateMeasureClockwise;
                @RotateMeasureClockwise.performed += instance.OnRotateMeasureClockwise;
                @RotateMeasureClockwise.canceled += instance.OnRotateMeasureClockwise;
                @RotateMeasureCounterClockwise.started += instance.OnRotateMeasureCounterClockwise;
                @RotateMeasureCounterClockwise.performed += instance.OnRotateMeasureCounterClockwise;
                @RotateMeasureCounterClockwise.canceled += instance.OnRotateMeasureCounterClockwise;
                @PreviousMeasure.started += instance.OnPreviousMeasure;
                @PreviousMeasure.performed += instance.OnPreviousMeasure;
                @PreviousMeasure.canceled += instance.OnPreviousMeasure;
                @NextMeasure.started += instance.OnNextMeasure;
                @NextMeasure.performed += instance.OnNextMeasure;
                @NextMeasure.canceled += instance.OnNextMeasure;
                @SwitchModes.started += instance.OnSwitchModes;
                @SwitchModes.performed += instance.OnSwitchModes;
                @SwitchModes.canceled += instance.OnSwitchModes;
                @MoveCursor.started += instance.OnMoveCursor;
                @MoveCursor.performed += instance.OnMoveCursor;
                @MoveCursor.canceled += instance.OnMoveCursor;
                @SwitchScales.started += instance.OnSwitchScales;
                @SwitchScales.performed += instance.OnSwitchScales;
                @SwitchScales.canceled += instance.OnSwitchScales;
                @ReturnToWorld.started += instance.OnReturnToWorld;
                @ReturnToWorld.performed += instance.OnReturnToWorld;
                @ReturnToWorld.canceled += instance.OnReturnToWorld;
            }
        }
    }
    public CreatingActions @Creating => new CreatingActions(this);

    // Performance
    private readonly InputActionMap m_Performance;
    private IPerformanceActions m_PerformanceActionsCallbackInterface;
    private readonly InputAction m_Performance_Retry;
    private readonly InputAction m_Performance_Quit;
    private readonly InputAction m_Performance_PressRepeat;
    private readonly InputAction m_Performance_DownUp;
    private readonly InputAction m_Performance_SetBPM;
    private readonly InputAction m_Performance_Play;
    public struct PerformanceActions
    {
        private @ComposiaControls m_Wrapper;
        public PerformanceActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Retry => m_Wrapper.m_Performance_Retry;
        public InputAction @Quit => m_Wrapper.m_Performance_Quit;
        public InputAction @PressRepeat => m_Wrapper.m_Performance_PressRepeat;
        public InputAction @DownUp => m_Wrapper.m_Performance_DownUp;
        public InputAction @SetBPM => m_Wrapper.m_Performance_SetBPM;
        public InputAction @Play => m_Wrapper.m_Performance_Play;
        public InputActionMap Get() { return m_Wrapper.m_Performance; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PerformanceActions set) { return set.Get(); }
        public void SetCallbacks(IPerformanceActions instance)
        {
            if (m_Wrapper.m_PerformanceActionsCallbackInterface != null)
            {
                @Retry.started -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnRetry;
                @Retry.performed -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnRetry;
                @Retry.canceled -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnRetry;
                @Quit.started -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnQuit;
                @PressRepeat.started -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnPressRepeat;
                @PressRepeat.performed -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnPressRepeat;
                @PressRepeat.canceled -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnPressRepeat;
                @DownUp.started -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnDownUp;
                @DownUp.performed -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnDownUp;
                @DownUp.canceled -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnDownUp;
                @SetBPM.started -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnSetBPM;
                @SetBPM.performed -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnSetBPM;
                @SetBPM.canceled -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnSetBPM;
                @Play.started -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnPlay;
                @Play.performed -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnPlay;
                @Play.canceled -= m_Wrapper.m_PerformanceActionsCallbackInterface.OnPlay;
            }
            m_Wrapper.m_PerformanceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Retry.started += instance.OnRetry;
                @Retry.performed += instance.OnRetry;
                @Retry.canceled += instance.OnRetry;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @PressRepeat.started += instance.OnPressRepeat;
                @PressRepeat.performed += instance.OnPressRepeat;
                @PressRepeat.canceled += instance.OnPressRepeat;
                @DownUp.started += instance.OnDownUp;
                @DownUp.performed += instance.OnDownUp;
                @DownUp.canceled += instance.OnDownUp;
                @SetBPM.started += instance.OnSetBPM;
                @SetBPM.performed += instance.OnSetBPM;
                @SetBPM.canceled += instance.OnSetBPM;
                @Play.started += instance.OnPlay;
                @Play.performed += instance.OnPlay;
                @Play.canceled += instance.OnPlay;
            }
        }
    }
    public PerformanceActions @Performance => new PerformanceActions(this);

    // Arrangement
    private readonly InputActionMap m_Arrangement;
    private IArrangementActions m_ArrangementActionsCallbackInterface;
    private readonly InputAction m_Arrangement_Newaction;
    public struct ArrangementActions
    {
        private @ComposiaControls m_Wrapper;
        public ArrangementActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Arrangement_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Arrangement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArrangementActions set) { return set.Get(); }
        public void SetCallbacks(IArrangementActions instance)
        {
            if (m_Wrapper.m_ArrangementActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_ArrangementActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_ArrangementActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_ArrangementActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_ArrangementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public ArrangementActions @Arrangement => new ArrangementActions(this);

    // Exploration
    private readonly InputActionMap m_Exploration;
    private IExplorationActions m_ExplorationActionsCallbackInterface;
    private readonly InputAction m_Exploration_Pow;
    private readonly InputAction m_Exploration_Jump;
    private readonly InputAction m_Exploration_Move;
    private readonly InputAction m_Exploration_Interact;
    private readonly InputAction m_Exploration_Cancel;
    public struct ExplorationActions
    {
        private @ComposiaControls m_Wrapper;
        public ExplorationActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pow => m_Wrapper.m_Exploration_Pow;
        public InputAction @Jump => m_Wrapper.m_Exploration_Jump;
        public InputAction @Move => m_Wrapper.m_Exploration_Move;
        public InputAction @Interact => m_Wrapper.m_Exploration_Interact;
        public InputAction @Cancel => m_Wrapper.m_Exploration_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Exploration; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ExplorationActions set) { return set.Get(); }
        public void SetCallbacks(IExplorationActions instance)
        {
            if (m_Wrapper.m_ExplorationActionsCallbackInterface != null)
            {
                @Pow.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnPow;
                @Pow.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnPow;
                @Pow.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnPow;
                @Jump.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnInteract;
                @Cancel.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_ExplorationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pow.started += instance.OnPow;
                @Pow.performed += instance.OnPow;
                @Pow.canceled += instance.OnPow;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public ExplorationActions @Exploration => new ExplorationActions(this);

    // Puzzle
    private readonly InputActionMap m_Puzzle;
    private IPuzzleActions m_PuzzleActionsCallbackInterface;
    private readonly InputAction m_Puzzle_Confirmation;
    private readonly InputAction m_Puzzle_Previous;
    private readonly InputAction m_Puzzle_Next;
    private readonly InputAction m_Puzzle_Cancel;
    public struct PuzzleActions
    {
        private @ComposiaControls m_Wrapper;
        public PuzzleActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirmation => m_Wrapper.m_Puzzle_Confirmation;
        public InputAction @Previous => m_Wrapper.m_Puzzle_Previous;
        public InputAction @Next => m_Wrapper.m_Puzzle_Next;
        public InputAction @Cancel => m_Wrapper.m_Puzzle_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Puzzle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PuzzleActions set) { return set.Get(); }
        public void SetCallbacks(IPuzzleActions instance)
        {
            if (m_Wrapper.m_PuzzleActionsCallbackInterface != null)
            {
                @Confirmation.started -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnConfirmation;
                @Confirmation.performed -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnConfirmation;
                @Confirmation.canceled -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnConfirmation;
                @Previous.started -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnPrevious;
                @Previous.performed -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnPrevious;
                @Previous.canceled -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnPrevious;
                @Next.started -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnNext;
                @Cancel.started -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_PuzzleActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_PuzzleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Confirmation.started += instance.OnConfirmation;
                @Confirmation.performed += instance.OnConfirmation;
                @Confirmation.canceled += instance.OnConfirmation;
                @Previous.started += instance.OnPrevious;
                @Previous.performed += instance.OnPrevious;
                @Previous.canceled += instance.OnPrevious;
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public PuzzleActions @Puzzle => new PuzzleActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Confirm;
    private readonly InputAction m_Menu_Back;
    private readonly InputAction m_Menu_Navigate;
    public struct MenuActions
    {
        private @ComposiaControls m_Wrapper;
        public MenuActions(@ComposiaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm => m_Wrapper.m_Menu_Confirm;
        public InputAction @Back => m_Wrapper.m_Menu_Back;
        public InputAction @Navigate => m_Wrapper.m_Menu_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Confirm.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Back.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Navigate.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlatformingActions
    {
        void OnJump_Press(InputAction.CallbackContext context);
        void OnJump_Release(InputAction.CallbackContext context);
        void OnJump_Hold(InputAction.CallbackContext context);
        void OnPows(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnSwitchModes(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
        void OnCharge_Press(InputAction.CallbackContext context);
        void OnCharge_Hold(InputAction.CallbackContext context);
    }
    public interface ICreatingActions
    {
        void OnPlacePiece(InputAction.CallbackContext context);
        void OnChoosePiece(InputAction.CallbackContext context);
        void OnErasePiece(InputAction.CallbackContext context);
        void OnNextRole(InputAction.CallbackContext context);
        void OnPreviousRole(InputAction.CallbackContext context);
        void OnRotateMeasureClockwise(InputAction.CallbackContext context);
        void OnRotateMeasureCounterClockwise(InputAction.CallbackContext context);
        void OnPreviousMeasure(InputAction.CallbackContext context);
        void OnNextMeasure(InputAction.CallbackContext context);
        void OnSwitchModes(InputAction.CallbackContext context);
        void OnMoveCursor(InputAction.CallbackContext context);
        void OnSwitchScales(InputAction.CallbackContext context);
        void OnReturnToWorld(InputAction.CallbackContext context);
    }
    public interface IPerformanceActions
    {
        void OnRetry(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
        void OnPressRepeat(InputAction.CallbackContext context);
        void OnDownUp(InputAction.CallbackContext context);
        void OnSetBPM(InputAction.CallbackContext context);
        void OnPlay(InputAction.CallbackContext context);
    }
    public interface IArrangementActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IExplorationActions
    {
        void OnPow(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IPuzzleActions
    {
        void OnConfirmation(InputAction.CallbackContext context);
        void OnPrevious(InputAction.CallbackContext context);
        void OnNext(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnConfirm(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
    }
}
