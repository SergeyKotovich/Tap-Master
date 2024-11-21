using Cube;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private ShakeAnimationController _shakeAnimationController;
    [SerializeField] private GameObject _flyEffectPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_shakeAnimationController);
        builder.RegisterInstance(_flyEffectPrefab);
        
        builder.Register<AsyncMessageBus>(Lifetime.Singleton);
        builder.Register<ObstacleDetector>(Lifetime.Singleton);
        builder.Register<EffectFactory>(Lifetime.Singleton);
    }
}