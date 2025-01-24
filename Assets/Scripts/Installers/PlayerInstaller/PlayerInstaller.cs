using Character;
using Components;
using Configs;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [Header("Essentials")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PickUpController pickUpController;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Joystick joystick;

        [Header("Configs")]
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private CameraConfig cameraConfig;
        
        [Header("Pick Up Comp")]
        [SerializeField] private float searchDistance;
        [SerializeField] private float searchRadius;
        [SerializeField] private float height;
        
        public override void InstallBindings()
        {
            BindJoystick();
            BindPlayerConfig();
            BindInputManager();
            BindCharacterControlle();
            BindMoveComponent();
            BindCamera();
            BindCameraConfig();
            BindCameraFollowComponent();
            BindPickUpComponent();
            BindPlayerController();
        }

        private void BindPlayerController()
        {
            Container
                .BindInterfacesTo<PlayerController>()
                .FromInstance(playerController)
                .AsSingle();
        }

        private void BindPickUpComponent()
        {
            // Container
            //     .BindInterfacesTo<PickUpComponent>()
            //     .AsSingle()
            //     .WithArguments(playerCamera.transform, playerCamera, searchRadius, searchDistance, height);

            Container
                .BindInterfacesTo<PickUpController>()
                .FromInstance(pickUpController)
                .AsSingle();
        }

        private void BindCameraFollowComponent()
        {
            Container
                .BindInterfacesTo<CameraFollowCustomComponent>()
                .AsSingle()
                .WithArguments(cameraConfig, playerCamera, playerCamera.transform);
        }

        private void BindCameraConfig()
        {
            Container
                .Bind<CameraConfig>()
                .FromInstance(cameraConfig)
                .AsSingle();
        }

        private void BindCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(playerCamera)
                .AsSingle();
        }

        private void BindMoveComponent()
        {
            Container
                .BindInterfacesTo<MoveCustomComponent>()
                .AsSingle()
                .WithArguments(characterController, playerConfig);
        }

        private void BindCharacterControlle()
        {
            Container
                .Bind<CharacterController>()
                .FromInstance(characterController)
                .AsSingle();
        }

        private void BindInputManager()
        {
            Container
                .BindInterfacesTo<InputManager>()
                .AsSingle()
                .WithArguments(joystick)
                .NonLazy();
        }

        private void BindPlayerConfig()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(playerConfig)
                .AsSingle();
        }

        private void BindJoystick()
        {
            Container
                .Bind<Joystick>()
                .FromInstance(joystick)
                .AsSingle();
        }
    }
}