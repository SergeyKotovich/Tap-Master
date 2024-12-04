using Cube;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private ShakeAnimationController _shakeAnimationController;
    [SerializeField] private GameObject _flyEffectPrefab;
    [SerializeField] private LevelConfig _levelConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_shakeAnimationController);
        builder.RegisterInstance(_flyEffectPrefab);
        builder.RegisterInstance(_levelConfig);
        
        
        builder.Register<AsyncMessageBus>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<ObstacleDetector>(Lifetime.Singleton);
        builder.Register<EffectFactory>(Lifetime.Singleton);
        builder.Register<WonMoneyController>(Lifetime.Singleton);
        builder.Register<Wallet>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
    }
}