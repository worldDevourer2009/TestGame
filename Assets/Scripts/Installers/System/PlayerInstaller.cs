using Character;
using Components;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private Transform pickTransfrom;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private CameraConfig cameraConfig;
        [SerializeField] private Joystick joystick;
        public override void InstallBindings()
        {
            Container
                .Bind<Joystick>()
                .FromInstance(joystick)
                .AsSingle();

            Container
                .Bind<PlayerConfig>()
                .FromInstance(playerConfig)
                .AsSingle();
            
            Container
                .BindInterfacesTo<InputManager>()
                .AsSingle()
                .WithArguments(joystick)
                .NonLazy();

            Container
                .Bind<CharacterController>()
                .FromInstance(characterController)
                .AsSingle();

            Container
                .BindInterfacesTo<MoveCustomComponent>()
                .AsSingle()
                .WithArguments(characterController);

            Container
                .Bind<Camera>()
                .FromInstance(playerCamera)
                .AsSingle();

            Container
                .Bind<CameraConfig>()
                .FromInstance(cameraConfig)
                .AsSingle();

            Container
                .BindInterfacesTo<CameraFollowCustomComponent>()
                .AsSingle()
                .WithArguments(cameraConfig, playerCamera, characterController.transform);

            Container
                .BindInterfacesTo<CameraController>()
                .FromInstance(cameraController)
                .AsSingle();

            Container
                .BindInterfacesTo<PickUpComponent>()
                .AsSingle()
                .WithArguments(playerConfig, pickTransfrom, playerCamera);

            Container
                .BindInterfacesTo<PlayerController>()
                .FromInstance(playerController)
                .AsSingle();
        }
    }
}