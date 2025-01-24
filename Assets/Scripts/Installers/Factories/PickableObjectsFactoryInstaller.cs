using System.Collections.Generic;
using Configs;
using Factories;
using Items;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PickableObjectsFactoryInstaller : MonoInstaller<PickableObjectsFactoryInstaller>
    {
        [SerializeReference] private List<PickableObjectConfig> pickableObjects;
        [SerializeField] private ItemsSpawner itemsSpawner;
        [SerializeField] private Transform parent;
        
        public override void InstallBindings()
        {
            Container
                .Bind<List<PickableObjectConfig>>()
                .FromInstance(pickableObjects)
                .AsSingle();
            
            Container
                .Bind<Transform>()
                .FromInstance(parent)
                .AsSingle();
            
            Container
                .Bind<ICreatePickableObjects>()
                .To<PickableObjectsFactory>()
                .AsSingle()
                .WithArguments(parent)
                .NonLazy();
            
            Container
                .Bind<IInitializable>()
                .To<ItemsSpawner>()
                .FromInstance(itemsSpawner)
                .AsSingle();

        }
    }
}